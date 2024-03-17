﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vatsys;

namespace maxrumsey.ozstrips.gui
{
    public partial class Strip : StripBaseGUI
    {
        public Strip(StripController controller)
        {
            this.fdr = controller.fdr;
            InitializeComponent();
            UpdateStrip();

            this.cockColourControls = new Panel[] {
                this.pl_eobt,
                this.pl_multi,
                this.pl_multi2
                };

            this.stripController = controller;
        }

        public new void UpdateStrip()
        {
            if (fdr == null) return;
            System.Console.WriteLine(fdr.Callsign + ":" + fdr.AssignedSSRCode);
            lb_eobt.Text = fdr.ETD.ToString("HHmm");
            lb_acid.Text = fdr.Callsign;
            lb_ssr.Text = (fdr.AssignedSSRCode == -1) ? "XXXX" : Convert.ToString(fdr.AssignedSSRCode, 8);
            lb_type.Text = fdr.AircraftType;
            lb_frul.Text = fdr.FlightRules;
            lb_route.Text = fdr.Route;
            lb_sid.Text = fdr.SIDSTARString;
            lb_ades.Text = fdr.DesAirport;


        }

        private void lb_sid_Click(object sender, EventArgs e)
        {

        }

        private void lb_eobt_Click(object sender, EventArgs e)
        {
            Cock(-1);
        }

        private void pl_route_MouseHover(object sender, EventArgs e)
        {
            tp_rte.SetToolTip(pl_route, fdr.Route);
        }

        private void lb_eobt_DoubleClick(object sender, EventArgs e)
        {
            Cock(-1);
        }

        private void lb_std_Click(object sender, EventArgs e)
        {
            Label child = new Label();
            child.Text = "abcd";

            BaseModal bm = new BaseModal(child, "a");
            bm.ShowDialog();
        }
    }
}
