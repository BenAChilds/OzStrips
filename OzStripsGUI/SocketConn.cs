﻿using maxrumsey.ozstrips.gui.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Timers;
using vatsys;

namespace maxrumsey.ozstrips.gui
{
    public class SocketConn
    {
        SocketIOClient.SocketIO io;
        private BayManager bayManager;
        private bool isDebug = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition"));
        public List<string> Messages = new List<string>();
        private bool versionShown = false;
        private bool freshClient = false;
        private System.Timers.Timer fifteensecTimer;
        private MainForm mainForm;
        public SocketConn(BayManager bayManager, MainForm mf)
        {
            mainForm = mf;
            this.bayManager = bayManager;
            io = new SocketIOClient.SocketIO(Config.socketioaddr);
            io.OnAny((sender, e) =>
            {
                MetadataDTO metaDTO = e.GetValue<MetadataDTO>(1);
                if (metaDTO.version != Config.version && !versionShown)
                {
                    versionShown = true;
                    Util.ShowInfoBox("New Update Available: " + metaDTO.version);
                }
                if (metaDTO.apiversion != Config.apiversion)
                {
                    Util.ShowErrorBox("OzStrips incompatible with current API version!");
                    mf.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
                    {
                        mf.Close();
                        mf.Dispose();
                    });
                }
            });

            io.OnConnected += async (sender, e) =>
            {
                Messages.Add("c: conn established");
                await io.EmitAsync("client:aerodrome_subscribe", bayManager.AerodromeName, Network.Me.RealName);
                mf.SetConnStatus(true);
            };
            io.OnDisconnected += (sender, e) =>
            {
                Messages.Add("c: conn lost");
                mf.SetConnStatus(false);
            };

            io.OnError += (sender, e) =>
            {
                mf.SetConnStatus(false);
                Errors.Add(new Exception(e), "OzStrips");
            };
            io.OnReconnected += async (sender, e) =>
            {
                await io.EmitAsync("client:aerodrome_subscribe", bayManager.AerodromeName);
                mf.SetConnStatus(true);
            };

            io.On("server:sc_change", sc =>
            {
                StripControllerDTO scDTO = sc.GetValue<StripControllerDTO>();
                Messages.Add("s:sc_change: " + JsonSerializer.Serialize(scDTO));

                mf.Invoke((System.Windows.Forms.MethodInvoker)delegate () { StripController.UpdateFDR(scDTO, bayManager); });

            });
            io.On("server:sc_cache", sc =>
            {
                CacheDTO scDTO = sc.GetValue<CacheDTO>();
                Messages.Add("s:sc_cache: " + JsonSerializer.Serialize(scDTO));

                mf.Invoke((System.Windows.Forms.MethodInvoker)delegate () { StripController.LoadCache(scDTO); });

            });
            io.On("server:order_change", bdto =>
            {
                BayDTO bayDTO = bdto.GetValue<BayDTO>();
                Messages.Add("s:order_change: " + JsonSerializer.Serialize(bayDTO));

                mf.Invoke((System.Windows.Forms.MethodInvoker)delegate () { bayManager.UpdateOrder(bayDTO); });
            });
            io.On("server:update_cache", (args) =>
            {
                Messages.Add("s:update_cache: ");
                SendCache();
            });
            if (Network.IsConnected) Connect();
            bayManager.socketConn = this;
        }

        public void SyncSC(StripController sc)
        {
            StripControllerDTO scDTO = CreateStripDTO(sc);
            Messages.Add("c:sc_change: " + JsonSerializer.Serialize(scDTO));

            if (CanSendDTO) io.EmitAsync("client:sc_change", scDTO);

        }
        public void SyncBay(Bay bay)
        {
            BayDTO bayDTO = CreateBayDTO(bay);
            Messages.Add("c:order_change: " + JsonSerializer.Serialize(bayDTO));

            if (CanSendDTO) io.EmitAsync("client:order_change", bayDTO);
        }
        public void SetAerodrome()
        {
            if (io.Connected) io.EmitAsync("client:aerodrome_subscribe", bayManager.AerodromeName);
        }

        public BayDTO CreateBayDTO(Bay bay)
        {
            BayDTO bayDTO = new BayDTO { bay = bay.BayTypes.First() };
            List<string> childList = new List<string>();
            foreach (StripListItem item in bay.Strips)
            {
                if (item.Type == StripItemType.STRIP) childList.Add(item.StripController.fdr.Callsign);
                else if (item.Type == StripItemType.QUEUEBAR) childList.Add("\a"); // indicates q-bar
            }
            bayDTO.list = childList;
            return bayDTO;
        }
        public StripControllerDTO CreateStripDTO(StripController sc)
        {
            StripControllerDTO scDTO = new StripControllerDTO { ACID = sc.fdr.Callsign, bay = sc.currentBay, CLX = sc.CLX, GATE = sc.GATE, StripCockLevel = sc.cockLevel, Crossing = sc.Crossing, remark = sc.Remark };
            if (sc.TakeOffTime != DateTime.MaxValue)
            {
                scDTO.TOT = sc.TakeOffTime.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                scDTO.TOT = "\0";
            }
            return scDTO;
        }
        public CacheDTO CreateCacheDTO()
        {
            List<StripControllerDTO> strips = new List<StripControllerDTO>();

            foreach (StripController strip in StripController.stripControllers)
            {
                strips.Add(CreateStripDTO(strip));
            }

            return new CacheDTO() { strips = strips };
        }

        public async void SendCache()
        {
            CacheDTO cacheDTO = CreateCacheDTO();
            Messages.Add("c:sc_cache: " + JsonSerializer.Serialize(cacheDTO));
            if (CanSendDTO) await io.EmitAsync("client:sc_cache", cacheDTO);
        }

        public void Close()
        {
            io.DisconnectAsync();
            io.Dispose();
        }
        
        /// <summary>
        /// Whether the user has permission to send data to server
        /// </summary>
        private bool CanSendDTO
        {
            get {
                if (!(Network.Me.IsRealATC || isDebug)) Messages.Add("c: DTO Rejected!");
                return io.Connected && (Network.Me.IsRealATC || isDebug); 
            }
        }

        /// <summary>
        /// Starts a fifteen second timer, ensures FDRs have loaded in before requesting SCs from server.
        /// </summary>
        public void Connect()
        {
            fifteensecTimer = new Timer();
            fifteensecTimer.AutoReset = false;
            fifteensecTimer.Interval = 15000;
            fifteensecTimer.Elapsed += ConnectIO;
            fifteensecTimer.Start();
            mainForm.SetAerodrome(bayManager.AerodromeName);
        }

        private void ConnectIO(object sender, ElapsedEventArgs e)
        {
            Messages.Add("c: Attempting connection");
            io.ConnectAsync();
        }

        public void Disconnect()
        {
            io.DisconnectAsync();
        }
    }
}
