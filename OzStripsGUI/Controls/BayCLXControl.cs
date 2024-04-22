﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

using vatsys;

namespace MaxRumsey.OzStripsPlugin.Gui.Controls;

/// <summary>
/// Represents a bay clearance.
/// </summary>
public partial class BayCLXControl : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BayCLXControl"/> class.
    /// </summary>
    /// <param name="controller">The strip controller.</param>
    public BayCLXControl(StripController controller)
    {
        InitializeComponent();

        tb_clx.Text = controller.CLX;
        tb_bay.Text = controller.Gate;
        tb_remark.Text = controller.Remark;
        tb_glop.Text = controller.FDR.GlobalOpData;
    }

    /// <summary>
    /// Gets or sets the base modal.
    /// </summary>
    public BaseModal? BaseModal { get; set; }

    /// <summary>
    /// Gets the clearance text.
    /// </summary>
    public string CLX => tb_clx.Text;

    /// <summary>
    /// Gets the gate text.
    /// </summary>
    public string Gate => tb_bay.Text;

    /// <summary>
    /// Gets the remarks text.
    /// </summary>
    public string Remark => tb_remark.Text;

    /// <summary>
    /// Gets the glip text.
    /// </summary>
    public string Glop => tb_glop.Text;

    private void AltHdgControl_Load(object sender, EventArgs e)
    {
        if (BaseModal is null)
        {
            return;
        }

        if (tb_bay.Text.Length == 0)
        {
            BaseModal.ActiveControl = tb_bay;
        }
        else
        {
            BaseModal.ActiveControl = tb_clx;
        }
    }

    private void ClearButtonTextCleared(object sender, EventArgs e)
    {
        tb_clx.Text = string.Empty;
    }

    private void BayButtonClearTextClicked(object sender, EventArgs e)
    {
        tb_bay.Text = string.Empty;
    }

    private void AcceptKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData == Keys.Enter)
        {
            BaseModal?.ExitModal(true);
        }
        else if (e.KeyData == Keys.Escape)
        {
            BaseModal?.ExitModal();
        }
    }

    private void RemarkButtonClearClicked(object sender, EventArgs e)
    {
        tb_remark.Text = string.Empty;
    }

    private void GlopButtonClearClicked(object sender, EventArgs e)
    {
        tb_glop.Text = string.Empty;
    }
}
