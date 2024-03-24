﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maxrumsey.ozstrips.gui
{
    public partial class BayControl : UserControl
    
    {
        public FlowLayoutPanel ChildPanel;
        private Bay OwnerBay;
        private BayManager BayManager;
        public BayControl(BayManager bm, String name, Bay bay)
        {
            InitializeComponent();
            lb_bay_name.Text = name;
            ChildPanel = flp_stripbay;
            flp_stripbay.VerticalScroll.Visible = true;
            this.BayManager = bm;
            OwnerBay = bay;
        }

        private void lb_bay_name_Click(object sender, EventArgs e)
        {
            BayManager.DropStrip(OwnerBay);
        }

        private void flp_stripbay_Paint(object sender, PaintEventArgs e)
        {
            BayManager.DropStrip(OwnerBay);
        }
    }
}
