﻿using maxrumsey.ozstrips.gui.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using vatsys;

namespace maxrumsey.ozstrips.gui
{
    public class SocketConn
    {
        SocketIOClient.SocketIO io;
        private BayManager bayManager;
        private bool isDebug =  !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition"));
        public SocketConn(BayManager bayManager, MainForm mf)
        {
            this.bayManager = bayManager;
            io = new SocketIOClient.SocketIO("http://localhost:8069");
            io.OnConnected += async (sender, e) =>
            {
                await io.EmitAsync("client:aerodrome_subscribe", bayManager.AerodromeName);
                mf.SetConnStatus(true);
            };
            io.OnDisconnected += (sender, e) =>
            {
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
                mf.Invoke((System.Windows.Forms.MethodInvoker)delegate () { StripController.UpdateFDR(scDTO, bayManager); });

            });
            io.On("server:order_change", bdto =>
            {
                BayDTO bayDTO = bdto.GetValue<BayDTO>();
                mf.Invoke((System.Windows.Forms.MethodInvoker)delegate () { bayManager.UpdateOrder(bayDTO); });
            });

            io.ConnectAsync();

            bayManager.socketConn = this;
        }

        public void SyncSC(StripController sc)
        {
            StripControllerDTO scDTO = CreateStripDTO(sc);
            if (io.Connected && (Network.Me.IsRealATC || isDebug)) io.EmitAsync("client:sc_change", scDTO);
        }
        public void SyncBay(Bay bay)
        {
            BayDTO bayDTO = CreateBayDTO(bay);
            if (io.Connected && (Network.Me.IsRealATC || isDebug)) io.EmitAsync("client:order_change", bayDTO);
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
            StripControllerDTO scDTO = new StripControllerDTO { ACID = sc.fdr.Callsign, bay = sc.currentBay, CLX = sc.CLX, GATE = sc.GATE, StripCockLevel = sc.cockLevel, Crossing = sc.Crossing };
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
        public CacheDTO CreateCachePacket()
        {
            List<BayDTO> bays = new List<BayDTO> ();
            List<StripControllerDTO> strips = new List<StripControllerDTO>();

            foreach (Bay bay in bayManager.Bays)
            {
                bays.Add(CreateBayDTO(bay));
            }
            foreach (StripController strip in StripController.stripControllers)
            {
                strips.Add(CreateStripDTO(strip));
            }

            return new CacheDTO() { bays = bays, strips = strips };
        }

        public async void SendCache()
        {
            CacheDTO cacheDTO = CreateCachePacket();
            if (io.Connected && (Network.Me.IsRealATC || isDebug)) await io.EmitAsync("client:send_cache", cacheDTO);
        }
    }
}
