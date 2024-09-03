﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaxRumsey.OzStripsPlugin.Gui.DTO;

using vatsys;
using static vatsys.FDP2;

// todo: separate GUI components into separate class
namespace MaxRumsey.OzStripsPlugin.Gui;

/// <summary>
/// Handles the bays.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="BayManager"/> class.
/// </remarks>
/// <param name="main">The flow layout for the bay.</param>
/// <param name="layoutMethod">The current layout caller.</param>
public class BayManager(FlowLayoutPanel main, Action<object, EventArgs> layoutMethod)
{
    private readonly List<FlowLayoutPanel> _flpVerticalBoards = [];

    private readonly Action<object, EventArgs> _currentLayout = layoutMethod;

    private int _currentLayoutIndex;

    /// <summary>
    /// Gets or sets the picked controller.
    /// </summary>
    public StripController? PickedController { get; set; }

    /// <summary>
    /// Gets or sets the number of present bays.
    /// </summary>
    public int BayNum { get; set; }

    /// <summary>
    /// Gets or sets the aerodrome name.
    /// </summary>
    public string AerodromeName { get; set; } = "????";

    /// <summary>
    /// Gets the list of bays.
    /// </summary>
    public List<Bay> Bays { get; } = [];

    /// <summary>
    /// Sets the last selected track's FDR in vatSys.
    /// </summary>
    public string? PickedCallsign
    {
        set
        {
            if (value is not null)
            {
                var sc = StripController.GetController(value);
                if (sc is not null)
                {
                    SetPicked(sc.FDR);
                }
            }
            else
            {
                SetPicked();
            }
        }
    }

    /// <summary>
    /// Updates the bay based on the bay data.
    /// </summary>
    /// <param name="bayDTO">The bay data.</param>
    public void UpdateOrder(BayDTO bayDTO)
    {
        Bay? bay = null;
        foreach (var currentBay in Bays)
        {
            if (currentBay.BayTypes.Contains(bayDTO.bay))
            {
                bay = currentBay;
            }
        }

        if (bay == null)
        {
            return;
        }

        var list = new List<StripListItem>();

        foreach (var dtoItem in bayDTO.list)
        {
            var listItem = bay.GetListItemByStr(dtoItem);
            if (listItem != null)
            {
                list.Add(listItem);
            }
        }

        // incase of dodgy timing
        foreach (var oldListItem in bay.Strips)
        {
            if (!list.Contains(oldListItem) && oldListItem.Type == StripItemType.STRIP)
            {
                list.Add(oldListItem);
            }
        }

        bay.Strips.Clear();
        bay.Strips.AddRange(list);
        bay.Orderstrips();
    }

    /// <summary>
    /// Forces a track into the first bay.
    /// </summary>
    /// <param name="socketConn">The socket connection.</param>
    public void ForceStrip(SocketConn socketConn)
    {
        if (MMI.SelectedTrack != null)
        {
            var fdr = MMI.SelectedTrack.GetFDR();
            if (fdr is null)
            {
                return;
            }

            var controller = StripController.UpdateFDR(fdr, this, socketConn);

            if (controller != null && Bays[0] != null)
            {
                controller.CurrentBay = Bays[0].BayTypes[0];
                controller.SyncStrip();
                controller.FDR = fdr;
                UpdateBay(controller);
            }
        }
    }

    /// <summary>
    /// SidTriggers the selected strip.
    /// </summary>
    public void SidTrigger()
    {
        if (PickedController is not null)
        {
            PickedController.SIDTrigger();
        }
    }

    /// <summary>
    /// Cocks the selected strip.
    /// </summary>
    public void CockStrip()
    {
        if (PickedController is not null)
        {
            PickedController.CockStrip();
        }
    }

    /// <summary>
    /// Inhibit the strip.
    /// </summary>
    public void Inhibit()
    {
        if (PickedController != null)
        {
            PickedController.CurrentBay = StripBay.BAY_DEAD;
            PickedController.SyncStrip();
            UpdateBay(PickedController);
            SetPicked(true);
        }
    }

    /// <summary>
    /// Send the PDC.
    /// </summary>
    public void SendPDC()
    {
        if (PickedController != null)
        {
            MMI.OpenCPDLCWindow(PickedController.FDR, null, CPDLC.MessageCategories.FirstOrDefault(m => m.Name == "PDC"));
            SetPicked(true);
        }
    }

    /// <summary>
    /// Toggles crossing highlight on a strip.
    /// </summary>
    public void CrossStrip()
    {
        if (PickedController != null)
        {
            PickedController.Crossing = !PickedController.Crossing;
            SetPicked(true);
        }
    }

    /// <summary>
    /// Drop the strip to the specified bay.
    /// </summary>
    /// <param name="bay">The bay.</param>
    public void DropStrip(Bay bay)
    {
        if (PickedController != null)
        {
            var newBay = bay.BayTypes.FirstOrDefault();
            if (newBay == PickedController.CurrentBay)
            {
                return;
            }

            PickedController.CurrentBay = newBay;
            PickedController.SyncStrip();
            UpdateBay(PickedController);
            SetPicked(true);
        }
    }

    /// <summary>
    /// Deletes the specified strip.
    /// </summary>
    /// <param name="strip">The strip to delete.</param>
    public void DeleteStrip(StripController strip)
    {
        strip.SendDeleteMessage();
        FindBay(strip)?.RemoveStrip(strip, true);
        StripController.StripControllers.Remove(strip);
    }

    /// <summary>
    /// Sets the aerodrome. Reinitialises various classes.
    /// </summary>
    /// <param name="name">The name of the aerodrome.</param>
    /// <param name="socketConn">The socket connection.</param>
    public void SetAerodrome(string name, SocketConn socketConn)
    {
        AerodromeName = name;
        WipeStrips();

        for (var i = StripController.StripControllers.Count - 1; i >= 0; i--)
        {
            StripController.StripControllers[i].Dispose();
        }

        StripController.StripControllers.Clear();

        foreach (var fdr in FDP2.GetFDRs)
        {
            StripController.UpdateFDR(fdr, this, socketConn, true);
        }

        var instance = MainForm.MainFormInstance;
        if (instance is not null)
        {
            LockWindowUpdate(instance.Handle);

            foreach (var bay in Bays)
            {
                bay.Orderstrips();
            }

            LockWindowUpdate(IntPtr.Zero);
        }
    }

    /// <summary>
    /// Sets a controller to be picked.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <param name="sendToVatsys">Selects relevant track in vatSys.</param>
    public void SetPicked(StripController controller, bool sendToVatsys = false)
    {
        PickedController?.SetHMIPicked(false);
        PickedController = controller;
        controller.SetHMIPicked(true);

        if (sendToVatsys)
        {
            var rTrack = RDP.RadarTracks.FirstOrDefault(x => x.ActualAircraft.Callsign == controller.FDR.Callsign);
            var track = MMI.FindTrack(rTrack);
            if (track is not null)
            {
                if (MMI.SelectedTrack != track)
                {
                    MMI.SelectOrDeselectTrack(track);
                }

                if (MMI.SelectedGroundTrack != track)
                {
                    MMI.SelectOrDeselectGroundTrack(track);
                }
            }
        }
    }

    /// <summary>
    /// Sets a controller to be picked, from an FDR.
    /// </summary>
    /// <param name="fdr">The fdr.</param>
    public void SetPicked(FDR? fdr)
    {
        if (fdr is not null)
        {
            StripController? foundSC = null;
            foreach (var controller in StripController.StripControllers)
            {
                if (controller.FDR.Callsign == fdr.Callsign)
                {
                    foundSC = controller;
                }
            }

            if (foundSC is not null)
            {
                SetPicked(foundSC);
            }
        }
        else
        {
            SetPicked();
        }
    }

    /// <summary>
    /// Sets the picked controller to be empty.
    /// </summary>
    /// <param name="sendToVatsys">Deselect ground track in vatSys.</param>
    public void SetPicked(bool sendToVatsys = false)
    {
        PickedController?.SetHMIPicked(false);
        PickedController = null;

        if (sendToVatsys)
        {
            MMI.SelectOrDeselectGroundTrack(MMI.SelectedGroundTrack);
            MMI.SelectOrDeselectTrack(MMI.SelectedTrack);
        }
    }

    /// <summary>
    /// Wipe the strips.
    /// </summary>
    public void WipeStrips()
    {
        PickedController = null;
        foreach (var bay in Bays)
        {
            bay.WipeStrips();
        }
    }

    /// <summary>
    /// Adds a strip.
    /// </summary>
    /// <param name="stripController">The strip controller to add.</param>
    /// <param name="save">If the strip controller should be saved.</param>
    /// <param name="inhibitreorders">Whether or not to inhibit strip reodering.</param>
    public void AddStrip(StripController stripController, bool save = true, bool inhibitreorders = false)
    {
        if (!stripController.DetermineSCValidity())
        {
            return;
        }

        foreach (var bay in Bays)
        {
            if (bay.ResponsibleFor(stripController.CurrentBay))
            {
                bay.AddStrip(stripController, inhibitreorders);
            }
        }

        if (save)
        {
            StripController.StripControllers.Add(stripController);
        }
    }

    /// <summary>
    /// Finds the specified bay.
    /// </summary>
    /// <param name="stripController">The strip.</param>
    /// <returns>The bay if the name matches.</returns>
    public Bay? FindBay(StripController stripController)
    {
        foreach (var bay in Bays)
        {
            if (bay.OwnsStrip(stripController))
            {
                return bay;
            }
        }

        return null;
    }

    /// <summary>
    /// Updates the bay from the controller.
    /// </summary>
    /// <param name="stripController">The strip controller.</param>
    public void UpdateBay(StripController stripController)
    {
        foreach (var bay in Bays)
        {
            if (bay.OwnsStrip(stripController))
            {
                bay.RemoveStrip(stripController);
            }
        }

        stripController.ClearStripControl();
        stripController.CreateStripObj();
        AddStrip(stripController);

        if (stripController.CurrentBay >= StripBay.BAY_PUSHED)
        {
            stripController.CoordinateStrip();
        }
    }

    /// <summary>
    /// Adds the bay to the vertical board.
    /// </summary>
    /// <param name="bay">The bay.</param>
    /// <param name="verticalBoardNumber">The vertical board number.</param>
    public void AddBay(Bay bay, int verticalBoardNumber)
    {
        if (verticalBoardNumber >= _flpVerticalBoards.Count)
        {
            verticalBoardNumber = _flpVerticalBoards.Count - 1;
        }

        if (verticalBoardNumber < 0)
        {
            Errors.Add(new InvalidOperationException("No vertical board flow layout panels exist"), "OzStrips");
            return;
        }

        if (_currentLayoutIndex != 3)
        {
            var maxflpnum = BayNum / _currentLayoutIndex;
            if (_flpVerticalBoards[verticalBoardNumber].Controls.Count >= maxflpnum)
            {
                verticalBoardNumber = _currentLayoutIndex - 1;
                for (var i = 0; i < _flpVerticalBoards.Count; i++)
                {
                    if (_flpVerticalBoards[i].Controls.Count < maxflpnum)
                    {
                        verticalBoardNumber = i;
                    }
                }
            }
        }

        bay.VerticalBoardNumber = verticalBoardNumber;

        Bays.Add(bay);
        _flpVerticalBoards[verticalBoardNumber].Controls.Add(bay.ChildPanel);
    }

    /// <summary>
    /// Wipes the bays.
    /// </summary>
    public void WipeBays()
    {
        Bays.Clear();
        foreach (var flpVerticalBoard in _flpVerticalBoards)
        {
            flpVerticalBoard.SuspendLayout();
            flpVerticalBoard.Controls.Clear();
        }
    }

    /// <summary>
    /// Reloads the strips. Called when stripboard layout is changed.
    /// </summary>
    public void ReloadStrips()
    {
        try
        {
            foreach (var strip in StripController.StripControllers)
            {
                foreach (var bay in Bays)
                {
                    if (bay.OwnsStrip(strip))
                    {
                        bay.RemoveStrip(strip);
                    }
                }

                strip.ClearStripControl();
                strip.CreateStripObj();
                AddStrip(strip, false, true);
            }

            foreach (var bay in Bays)
            {
                bay.Orderstrips();
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex, "OzStrips");
        }
    }

    /// <summary>
    /// Forces a rerender.
    /// </summary>
    public void ForceRerender()
    {
        try
        {
            foreach (var bay in Bays)
            {
                bay.ForceRerender();
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex, "OzStrips");
        }
    }

    /// <summary>
    /// Resizes the control.
    /// </summary>
    public void Resize()
    {
        if (main == null)
        {
            return;
        }

        if (_currentLayoutIndex == 0)
        {
            AddVertBoard();
            AddVertBoard();
            AddVertBoard();
            _currentLayoutIndex = 3;
            _currentLayout(this, new EventArgs());
        }

        var y_main = main.Size.Height;

        if (main.Size.Width <= 840 && _currentLayoutIndex != 1)
        {
            ClearVertBoards();
            AddVertBoard();
            _currentLayoutIndex = 1;
            _currentLayout(this, new EventArgs());
            return;
        }
        else if (main.Size.Width > 840 && main.Size.Width <= 1250 && _currentLayoutIndex != 2)
        {
            ClearVertBoards();
            AddVertBoard();
            AddVertBoard();
            _currentLayoutIndex = 2;
            _currentLayout(this, new EventArgs());
            return;
        }
        else if (main.Size.Width > 1250 && _currentLayoutIndex != 3)
        {
            ClearVertBoards();
            AddVertBoard();
            AddVertBoard();
            AddVertBoard();
            _currentLayoutIndex = 3;
            _currentLayout(this, new EventArgs());
            return;
        }

        var x_each = (main.Size.Width - (main.VerticalScroll.Visible ? 20 : 0)) / _currentLayoutIndex;

        foreach (var panel in _flpVerticalBoards)
        {
            panel.Size = new(x_each, y_main);
            panel.Margin = default;
            panel.Padding = new(2);
            panel.ResumeLayout();
        }

        foreach (var bay in Bays)
        {
            var childnum = _flpVerticalBoards[bay.VerticalBoardNumber].Controls.Count;
            var height = (y_main - 4) / childnum;

            if (height < 300)
            {
                height = 300;
            }

            bay.ChildPanel.Size = new(x_each - 4, height);
        }
    }

    /// <summary>
    /// Positions the key.
    /// </summary>
    /// <param name="relPos">The relative position.</param>
    public void PositionKey(int relPos)
    {
        try
        {
            if (PickedController != null)
            {
                FindBay(PickedController)?.ChangeStripPosition(PickedController, relPos);
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex, "OzStrips");
        }
    }

    /// <summary>
    /// Queues up the selected strip.
    /// </summary>
    public void QueueUp()
    {
        try
        {
            if (PickedController != null)
            {
                FindBay(PickedController)?.QueueUp();
            }
        }
        catch (Exception ex)
        {
            Errors.Add(ex, "OzStrips");
        }
    }

    [DllImport("user32.dll")]
    private static extern long LockWindowUpdate(IntPtr handle);

    /// <summary>
    /// Clears all vertical boards.
    /// </summary>
    private void ClearVertBoards()
    {
        for (var i = main.Controls.Count - 1; i >= 0; i--)
        {
            main.Controls[i].Dispose();
        }

        main.Controls.Clear();
        _flpVerticalBoards.Clear();
    }

    /// <summary>
    /// Adds a vertical board.
    /// </summary>
    private void AddVertBoard()
    {
        var flp = new FlowLayoutPanel
        {
            AutoScroll = false,
            Margin = new(0),
            Padding = new(0),
            Size = new(100, 100),
            Location = new(0, 0),
            AutoSize = true,
            WrapContents = false,
            FlowDirection = FlowDirection.TopDown,
        };

        main.Controls.Add(flp);
        _flpVerticalBoards.Add(flp);
    }
}
