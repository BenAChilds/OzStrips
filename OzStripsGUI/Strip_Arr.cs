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
    public partial class Strip_Arr : StripBaseGUI
    {
        public Strip_Arr(StripController controller)
        {
            this.fdr = controller.fdr;
            InitializeComponent();

            pickToggleControl = pl_acid;

            base.lb_eobt = lb_eobt;
            base.lb_acid = lb_acid;
            base.lb_ssr = lb_ssr;
            base.lb_type = lb_type;
            base.lb_frul = lb_frul;
            base.lb_route = lb_route;
            base.lb_ades = lb_ades;
            base.lb_alt = lb_alt;
            base.lb_hdg = lb_hdg;
            base.lb_rwy = lb_rwy;
            base.lb_wtc = lb_wtc;
            base.lb_std = lb_std;
            base.lb_clx = lb_clx;
            this.cockColourControls = new Panel[] {
                this.pl_eobt,
                this.pl_multi
                };

            this.stripController = controller;
            UpdateStrip();

        }

        private void lb_sid_Click(object sender, EventArgs e)
        {
            stripController.SIDTrigger();
        }

        private void lb_eobt_Click(object sender, EventArgs e)
        {
            Cock(-1);
        }


        private void lb_eobt_DoubleClick(object sender, EventArgs e)
        {
            Cock(-1);
        }

        private void lb_std_Click(object sender, EventArgs e)
        {

        }


        // open hdg-alt box
        private void pl_multi3_Click(object sender, EventArgs e)
        {
            
        }

        private void lb_hdg_Click(object sender, EventArgs e)
        {
            OpenHdgAltModal();
        }

        private void lb_alt_Click(object sender, EventArgs e)
        {
            OpenHdgAltModal();

        }

        private void lb_route_Click(object sender, EventArgs e)
        {
            OpenVatsysFDRModMenu();
        }

        private void lb_ades_Click(object sender, EventArgs e)
        {
            OpenVatsysFDRModMenu();
        }

        private void Strip_Load_1(object sender, EventArgs e)
        {

        }

        private void lb_acid_Click(object sender, EventArgs e)
        {
            TogglePick();
        }
    }
}
