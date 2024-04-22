﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using MaxRumsey.OzStripsPlugin.Gui.Controls;
using MaxRumsey.OzStripsPlugin.Gui.DTO;

using vatsys;

using static vatsys.FDP2;

namespace MaxRumsey.OzStripsPlugin.Gui;

/// <summary>
/// Responsible for strip logic, represents a Vatsys FDR.
/// </summary>
public sealed class StripController : IDisposable
{
    private static readonly Regex _headingRegex = new(@"H(\d{3})");

    private readonly BayManager _bayManager;
    private readonly SocketConn _socketConn;
    ////private readonly StripLayoutTypes StripType;

    private StripBaseGUI? _stripControl;

    private bool _crossing;

    /// <summary>
    /// Initializes a new instance of the <see cref="StripController"/> class.
    /// </summary>
    /// <param name="fdr">The flight data record.</param>
    /// <param name="bayManager">Gets or sets the bay manager.</param>
    /// <param name="socketConn">The socket connection.</param>
    public StripController(FDP2.FDR fdr, BayManager bayManager, SocketConn socketConn)
    {
        FDR = fdr;
        _bayManager = bayManager;
        _socketConn = socketConn;
        CurrentBay = StripBay.BAY_PREA;
        if (ArrDepType == StripArrDepType.ARRIVAL)
        {
            CurrentBay = StripBay.BAY_ARRIVAL;
        }

        CreateStripObj();
    }

    /// <summary>
    /// Gets a list of strip controllers.
    /// </summary>
    public static List<StripController> StripControllers { get; } = [];

    /// <summary>
    /// Gets a dictionary which contains the departure next state for a given state.
    /// </summary>
    public static Dictionary<StripBay, StripBay> NextBayDep { get; } = new()
    {
        { StripBay.BAY_PREA, StripBay.BAY_CLEARED },
        { StripBay.BAY_CLEARED, StripBay.BAY_PUSHED },
        { StripBay.BAY_PUSHED, StripBay.BAY_TAXI },
        { StripBay.BAY_TAXI, StripBay.BAY_HOLDSHORT },
        { StripBay.BAY_RUNWAY, StripBay.BAY_OUT },
        { StripBay.BAY_OUT, StripBay.BAY_DEAD },
    };

    /// <summary>
    /// Gets a dictionary which contains the arrival next state for a given state.
    /// </summary>
    public static Dictionary<StripBay, StripBay> NextBayArr { get; } = new()
    {
        { StripBay.BAY_TAXI, StripBay.BAY_DEAD },
        { StripBay.BAY_RUNWAY, StripBay.BAY_TAXI },
    };

    /// <summary>
    /// Gets or sets the strip holder control.
    /// </summary>
    public Control? StripHolderControl { get; set; }

    /// <summary>
    /// Gets or sets the take off time.
    /// </summary>
    public DateTime? TakeOffTime { get; set; }

    /// <summary>
    /// Gets the flight data record.
    /// </summary>
    public FDP2.FDR FDR { get; }

    /// <summary>
    /// Gets or sets the current strip bay.
    /// </summary>
    public StripBay CurrentBay { get; set; }

    /// <summary>
    /// Gets or sets the current cock level.
    /// </summary>
    public int CockLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the strip is in crossing mode.
    /// </summary>
    public bool Crossing
    {
        get => _crossing;
        set
        {
            _crossing = value;
            _stripControl?.SetCross();
        }
    }

    /// <summary>
    /// Gets the arrival or departure type.
    /// </summary>
    public StripArrDepType ArrDepType
    {
        get
        {
            if (_bayManager == null)
            {
                return StripArrDepType.UNKNOWN;
            }

            if (FDR.DesAirport.Equals(_bayManager.AerodromeName, StringComparison.InvariantCultureIgnoreCase))
            {
                return StripArrDepType.ARRIVAL;
            }
            else
            {
                return FDR.DepAirport.Equals(_bayManager.AerodromeName, StringComparison.CurrentCultureIgnoreCase) ?
                    StripArrDepType.DEPARTURE :
                    StripArrDepType.UNKNOWN;
            }
        }
    }

    /// <summary>
    /// Gets or sets the CFL.
    /// </summary>
    public string CFL
    {
        get => FDR.CFLString;

        set
        {
            if (Network.Me.IsRealATC || MainForm.IsDebug)
            {
                SetCFL(FDR, value);
            }
        }
    }

    /// <summary>
    /// Gets or sets the clearance.
    /// </summary>
    public string CLX { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the remarks.
    /// </summary>
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the gate.
    /// </summary>
    public string Gate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the heading.
    /// </summary>
    public string HDG
    {
        get
        {
            var hdgMatch = _headingRegex.Match(FDR.GlobalOpData);
            return hdgMatch.Success ? hdgMatch.Value.Replace("H", string.Empty) : string.Empty;
        }

        set
        {
            if ((Network.Me.IsRealATC || MainForm.IsDebug) && !string.IsNullOrWhiteSpace(value))
            {
                SetGlobalOps(FDR, "H" + value);
            }
        }
    }

    /// <summary>
    /// Gets the current time.
    /// </summary>
    public string Time
    {
        get
        {
            if (ArrDepType == StripArrDepType.DEPARTURE && FDR.ATD == DateTime.MaxValue)
            {
                return FDR.ETD.ToString("HHmm", CultureInfo.InvariantCulture);
            }
            else
            {
                return ArrDepType == StripArrDepType.DEPARTURE ?
                    FDR.ATD.ToString("HHmm", CultureInfo.InvariantCulture) :
                    string.Empty;
            }
        }
    }

    /// <summary>
    /// Gets or sets the runway.
    /// </summary>
    public string RWY
    {
        get
        {
            if (ArrDepType == StripArrDepType.DEPARTURE && FDR.DepartureRunway != null)
            {
                return FDR.DepartureRunway.Name;
            }
            else
            {
                return ArrDepType == StripArrDepType.ARRIVAL && FDR.ArrivalRunway != null ? FDR.ArrivalRunway.Name : string.Empty;
            }
        }

        set
        {
            if (ArrDepType == StripArrDepType.DEPARTURE)
            {
                var aerodrome = FDR.DepAirport;
                var runways = Airspace2.GetRunways(aerodrome);
                foreach (var runway in runways)
                {
                    if (runway.Name == value)
                    {
                        if (Network.Me.IsRealATC || MainForm.IsDebug)
                        {
                            SetDepartureRunway(FDR, runway);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the SID.
    /// </summary>
    public string SID
    {
        get
        {
            return FDR.SIDSTARString;
        }

        set
        {
            var found = false;
            foreach (var possibleSID in FDR.DepartureRunway.SIDs)
            {
                if (possibleSID.sidStar.Name == value)
                {
                    if (value == FDR.SIDSTARString)
                    {
                        return; // dont needlessly set sid
                    }

                    if (Network.Me.IsRealATC || MainForm.IsDebug)
                    {
                        SetSID(FDR, possibleSID.sidStar);
                    }

                    found = true;
                }
            }

            if (!found)
            {
                CreateError("Attempted to set invalid SID");
            }
        }
    }

    /// <summary>
    /// Gets a list of possible departure runways.
    /// </summary>
    public List<Airspace2.SystemRunway> PossibleDepRunways
    {
        get
        {
            var aerodrome = FDR.DepAirport;
            return Airspace2.GetRunways(aerodrome);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the squawk code is correct.
    /// </summary>
    public bool SquawkCorrect
    {
        get
        {
            var rtrack = GetRadarTrack();
            return rtrack?.ActualAircraft.TransponderModeC == true && rtrack.ActualAircraft.TransponderCode == FDR.AssignedSSRCode;
        }
    }

    /// <summary>
    /// Converts a strip controller to the data object.
    /// </summary>
    /// <param name="sc">The strip controller.</param>
    public static implicit operator StripControllerDTO(StripController sc)
    {
        var scDTO = new StripControllerDTO { Acid = sc.FDR.Callsign, Bay = sc.CurrentBay, CLX = sc.CLX, Gate = sc.Gate, CockLevel = sc.CockLevel, Crossing = sc.Crossing, Remark = sc.Remark };
        if (sc.TakeOffTime is not null)
        {
            scDTO.TOT = sc.TakeOffTime!.ToString();
        }
        else
        {
            scDTO.TOT = "\0";
        }

        return scDTO;
    }

    /// <summary>
    /// Adds a error string to the VATSYS error system.
    /// </summary>
    /// <param name="error">The error.</param>
    public static void CreateError(string error)
    {
        CreateError(new Exception(error));
    }

    /// <summary>
    /// Adds a exception to the VATSYS error system.
    /// </summary>
    /// <param name="error">The error exception.</param>
    public static void CreateError(Exception error)
    {
        Errors.Add(error, "OzStrips");
    }

    /// <summary>
    /// Receives a fdr, updates according SC.
    /// </summary>
    /// <param name="fdr">The flight data record.</param>
    /// <param name="bayManager">The bay manager.</param>
    /// <param name="socketConn">The socket connection.</param>
    /// <returns>The appropriate strip controller for the FDR.</returns>
    public static StripController? UpdateFDR(FDP2.FDR fdr, BayManager bayManager, SocketConn socketConn)
    {
        var found = false;
        foreach (var controller in StripControllers)
        {
            if (controller.FDR.Callsign == fdr.Callsign)
            {
                found = true;

                if (GetFDRIndex(fdr.Callsign) == -1)
                {
                    bayManager.DeleteStrip(controller);
                }

                controller.UpdateFDR();
                return controller;
            }
        }

        if (!found)
        {
            // todo: add this logic into separate static function
            var stripController = new StripController(fdr, bayManager, socketConn);
            bayManager.AddStrip(stripController);
            return stripController;
        }

        return null;
    }

    /// <summary>
    /// Loads in a cacheDTO object received from server, sets SCs accordingly.
    /// </summary>
    /// <param name="cacheData">The cache data.</param>
    /// <param name="bayManager">The bay manager.</param>
    public static void LoadCache(CacheDTO cacheData, BayManager bayManager)
    {
        foreach (var stripDTO in cacheData.Strips)
        {
            UpdateFDR(stripDTO, bayManager);
        }
    }

    /// <summary>
    /// Receives a SC DTO object, updates relevant SC.
    /// </summary>
    /// <param name="stripControllerData">The strip controller data.</param>
    /// <param name="bayManager">The bay manager.</param>
    public static void UpdateFDR(StripControllerDTO stripControllerData, BayManager bayManager)
    {
        foreach (var controller in StripControllers)
        {
            if (controller.FDR.Callsign == stripControllerData.Acid)
            {
                var changeBay = false;
                controller.CLX = stripControllerData.CLX != null ? stripControllerData.CLX : string.Empty;
                controller.Gate = stripControllerData.Gate ?? string.Empty;
                if (controller.CurrentBay != stripControllerData.Bay)
                {
                    changeBay = true;
                }

                controller.CurrentBay = stripControllerData.Bay;
                controller._stripControl?.Cock(stripControllerData.CockLevel, false);
                if (stripControllerData.TOT == "\0")
                {
                    controller.TakeOffTime = DateTime.MaxValue;
                }
                else
                {
                    controller.TakeOffTime = DateTime.Parse(stripControllerData.TOT, CultureInfo.InvariantCulture);
                }

                controller.Remark = stripControllerData.Remark != null ? stripControllerData.Remark : string.Empty;
                controller._crossing = stripControllerData.Crossing;
                controller._stripControl?.SetCross(false);

                if (changeBay)
                {
                    bayManager.UpdateBay(controller); // prevent unessesscary reshufles
                }

                return;
            }
        }
    }

    /// <summary>
    /// Clears the strip controllers.
    /// </summary>
    public static void ClearControllers()
    {
        StripControllers.Clear();
    }

    /// <summary>
    /// Sets the HMI picked state.
    /// </summary>
    /// <param name="picked">True if picked, false otherwise.</param>
    public void SetHMIPicked(bool picked)
    {
        _stripControl?.HMI_TogglePick(picked);
    }

    /// <summary>
    /// That the strip has taken off.
    /// </summary>
    public void TakeOff()
    {
        if (TakeOffTime == DateTime.MaxValue)
        {
            TakeOffTime = DateTime.UtcNow;
            CoordinateStrip();
        }
        else
        {
            TakeOffTime = DateTime.MaxValue;
        }

        SyncStrip();
    }

    /// <summary>
    /// Coordinates the strip with the server.
    /// </summary>
    public void CoordinateStrip()
    {
        if (FDR.State == FDR.FDRStates.STATE_PREACTIVE && (Network.Me.IsRealATC || MainForm.IsDebug))
        {
            MMI.EstFDR(FDR);
        }

        if (CurrentBay == StripBay.BAY_PREA)
        {
            Util.ShowWarnBox("You have coordinated this strip while it is in your Preactive Bay.\nYou will no longer be able make changes to the flight plan!\nOpen the vatSys Flight Plan window and deactivate the strip if you still need to make changes to SID, RWY or Altitude.");
        }
    }

    /// <summary>
    /// Creates control for strip.
    /// </summary>
    public void CreateStripObj()
    {
        StripHolderControl = new Panel
        {
            BackColor = Color.FromArgb(193, 230, 242),
        };
        if (ArrDepType == StripArrDepType.ARRIVAL)
        {
            StripHolderControl.BackColor = Color.FromArgb(255, 255, 160);
        }

        StripHolderControl.Padding = new Padding(3);
        StripHolderControl.Margin = new Padding(0);

        ////stripHolderControl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        StripHolderControl.Size = new Size(100, 100);

        _stripControl = new Strip(this);

        _stripControl.Initialise();
        StripHolderControl.Size = new Size(_stripControl.Size.Width, _stripControl.Size.Height + 6);
        StripHolderControl.Controls.Add(_stripControl);
    }

    /// <summary>
    /// Removes items from the strip holder control.
    /// </summary>
    public void ClearStripControl()
    {
        StripHolderControl?.Controls.Clear();
    }

    /// <summary>
    /// Refreshes strip properties, determines if strip should be removed.
    /// </summary>
    public void UpdateFDR()
    {
        _stripControl?.UpdateStrip();

        var distance = GetDistToAerodrome(_bayManager.AerodromeName);

        if ((distance == -1 || distance > 50) && ArrDepType == StripArrDepType.DEPARTURE)
        {
            _bayManager.DeleteStrip(this);
        }
    }

    /// <summary>
    /// Determines which bay to move strip to.
    /// </summary>
    public void SIDTrigger()
    {
        Dictionary<StripBay, StripBay> stripBayResultDict;
        if (ArrDepType == StripArrDepType.ARRIVAL)
        {
            stripBayResultDict = NextBayArr;
        }
        else if (ArrDepType == StripArrDepType.DEPARTURE)
        {
            stripBayResultDict = NextBayDep;
        }
        else
        {
            return;
        }

        if (stripBayResultDict.TryGetValue(CurrentBay, out var nextBay))
        {
            CurrentBay = nextBay;
            _bayManager.UpdateBay(this);
            SyncStrip();
        }
    }

    /// <summary>
    /// Toggles the pick state.
    /// </summary>
    public void TogglePick()
    {
        if (_bayManager.PickedController == this)
        {
            _bayManager.SetPicked();
        }
        else
        {
            _bayManager.SetPicked(this);
        }
    }

    /// <summary>
    /// Determines if the strip is applicable to the passed aerodrome.
    /// </summary>
    /// <param name="name">The name of the aerodrome to check.</param>
    /// <returns>True if it is applicable false otherwise.</returns>
    public bool ApplicableToAerodrome(string name)
    {
        return FDR.DepAirport == name || FDR.DesAirport == name;
    }

    /// <summary>
    /// Gets the distance to the specified aerodrome.
    /// </summary>
    /// <param name="aerodrome">The aerodrome to check.</param>
    /// <returns>The distance, or -1 if it is unable to be determined.</returns>
    public double GetDistToAerodrome(string aerodrome)
    {
        try
        {
            var adCoord = Airspace2.GetAirport(aerodrome)?.LatLong;
            var planeCoord = FDR.PredictedPosition.Location;
            var radarTracks = (from radarTrack in RDP.RadarTracks
                               where radarTrack.ActualAircraft.Callsign == FDR.Callsign
                               select radarTrack).ToList();

            if (radarTracks.Count > 0)
            {
                foreach (var rTrack in radarTracks)
                {
                    planeCoord = rTrack.ActualAircraft.Position;
                }
            }

            if (adCoord != null)
            {
                return Conversions.CalculateDistance(adCoord, planeCoord);
            }
        }
        catch
        {
        }

        return -1;
    }

    /// <summary>
    /// Gets the radar track.
    /// </summary>
    /// <returns>The tradar track or null if none available.</returns>
    public RDP.RadarTrack? GetRadarTrack()
    {
        var radarTracks = RDP.RadarTracks
            .Where(radarTrack => radarTrack.ActualAircraft.Callsign.Equals(FDR.Callsign))
            .ToList();
        return radarTracks.Count > 0 ? radarTracks[0] : null;
    }

    /// <summary>
    /// Opens the VATSYS flight data record.
    /// </summary>
    public void OpenVatsysFDR()
    {
        MMI.OpenFPWindow(FDR);
    }

    /// <summary>
    /// Sync the strip with the server.
    /// </summary>
    public void SyncStrip()
    {
        _socketConn.SyncSC(this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _stripControl?.Dispose();
        StripHolderControl?.Dispose();
    }
}
