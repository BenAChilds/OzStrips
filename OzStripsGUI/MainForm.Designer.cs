﻿using System.Windows.Forms;

namespace MaxRumsey.OzStripsPlugin.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pl_controlbar = new System.Windows.Forms.Panel();
            pl_atis = new System.Windows.Forms.Panel();
            lb_atis = new System.Windows.Forms.Label();
            bt_pdc = new System.Windows.Forms.Button();
            bt_cross = new System.Windows.Forms.Button();
            bt_force = new System.Windows.Forms.Button();
            bt_inhibit = new System.Windows.Forms.Button();
            pl_ad = new System.Windows.Forms.Panel();
            lb_ad = new System.Windows.Forms.Label();
            pl_stat = new System.Windows.Forms.Panel();
            lb_stat = new System.Windows.Forms.Label();
            tb_Time = new System.Windows.Forms.TextBox();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            ts_ad = new System.Windows.Forms.ToolStripMenuItem();
            toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            ts_mode = new System.Windows.Forms.ToolStripMenuItem();
            aCDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sMCACDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aDCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aDCSMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            changelogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            stripToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            flp_main = new System.Windows.Forms.FlowLayoutPanel();
            tt_metar = new System.Windows.Forms.ToolTip(components);
            tinyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pl_controlbar.SuspendLayout();
            pl_atis.SuspendLayout();
            pl_ad.SuspendLayout();
            pl_stat.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pl_controlbar
            // 
            pl_controlbar.BackColor = System.Drawing.Color.FromArgb(160, 170, 170);
            pl_controlbar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pl_controlbar.Controls.Add(pl_atis);
            pl_controlbar.Controls.Add(bt_pdc);
            pl_controlbar.Controls.Add(bt_cross);
            pl_controlbar.Controls.Add(bt_force);
            pl_controlbar.Controls.Add(bt_inhibit);
            pl_controlbar.Controls.Add(pl_ad);
            pl_controlbar.Controls.Add(pl_stat);
            pl_controlbar.Controls.Add(tb_Time);
            pl_controlbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            pl_controlbar.Location = new System.Drawing.Point(0, 916);
            pl_controlbar.Name = "pl_controlbar";
            pl_controlbar.Size = new System.Drawing.Size(1784, 45);
            pl_controlbar.TabIndex = 0;
            // 
            // pl_atis
            // 
            pl_atis.BackColor = System.Drawing.Color.DarkGray;
            pl_atis.Controls.Add(lb_atis);
            pl_atis.Location = new System.Drawing.Point(349, 3);
            pl_atis.Name = "pl_atis";
            pl_atis.Size = new System.Drawing.Size(42, 37);
            pl_atis.TabIndex = 3;
            // 
            // lb_atis
            // 
            lb_atis.BackColor = System.Drawing.Color.Silver;
            lb_atis.Dock = System.Windows.Forms.DockStyle.Fill;
            lb_atis.Font = new System.Drawing.Font("Terminus (TTF)", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lb_atis.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            lb_atis.Location = new System.Drawing.Point(0, 0);
            lb_atis.Name = "lb_atis";
            lb_atis.Size = new System.Drawing.Size(42, 37);
            lb_atis.TabIndex = 0;
            lb_atis.Text = "Z";
            lb_atis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_pdc
            // 
            bt_pdc.BackColor = System.Drawing.Color.FromArgb(140, 150, 150);
            bt_pdc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            bt_pdc.Font = new System.Drawing.Font("Terminus (TTF)", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bt_pdc.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            bt_pdc.Location = new System.Drawing.Point(737, 3);
            bt_pdc.Name = "bt_pdc";
            bt_pdc.Size = new System.Drawing.Size(96, 37);
            bt_pdc.TabIndex = 6;
            bt_pdc.TabStop = false;
            bt_pdc.Text = "PDC";
            bt_pdc.UseVisualStyleBackColor = false;
            bt_pdc.Click += new System.EventHandler(Bt_pdc_Click);
            // 
            // bt_cross
            // 
            bt_cross.BackColor = System.Drawing.Color.RosyBrown;
            bt_cross.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bt_cross.Font = new System.Drawing.Font("Terminus (TTF)", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bt_cross.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            bt_cross.Location = new System.Drawing.Point(592, 3);
            bt_cross.Name = "bt_cross";
            bt_cross.Size = new System.Drawing.Size(142, 37);
            bt_cross.TabIndex = 5;
            bt_cross.TabStop = false;
            bt_cross.Text = "XX CROSS XX";
            bt_cross.UseVisualStyleBackColor = false;
            bt_cross.Click += new System.EventHandler(Bt_cross_Click);
            // 
            // bt_force
            // 
            bt_force.BackColor = System.Drawing.Color.FromArgb(140, 150, 150);
            bt_force.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            bt_force.Font = new System.Drawing.Font("Terminus (TTF)", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bt_force.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            bt_force.Location = new System.Drawing.Point(494, 3);
            bt_force.Name = "bt_force";
            bt_force.Size = new System.Drawing.Size(96, 37);
            bt_force.TabIndex = 4;
            bt_force.TabStop = false;
            bt_force.Text = "FOR STP";
            bt_force.UseVisualStyleBackColor = false;
            bt_force.Click += new System.EventHandler(Bt_force_Click);
            // 
            // bt_inhibit
            // 
            bt_inhibit.BackColor = System.Drawing.Color.FromArgb(140, 150, 150);
            bt_inhibit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            bt_inhibit.Font = new System.Drawing.Font("Terminus (TTF)", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bt_inhibit.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            bt_inhibit.Location = new System.Drawing.Point(396, 3);
            bt_inhibit.Name = "bt_inhibit";
            bt_inhibit.Size = new System.Drawing.Size(96, 37);
            bt_inhibit.TabIndex = 3;
            bt_inhibit.TabStop = false;
            bt_inhibit.Text = "INHIBIT";
            bt_inhibit.UseVisualStyleBackColor = false;
            bt_inhibit.Click += new System.EventHandler(Bt_inhibit_Click);
            // 
            // pl_ad
            // 
            pl_ad.BackColor = System.Drawing.Color.DarkGray;
            pl_ad.Controls.Add(lb_ad);
            pl_ad.Location = new System.Drawing.Point(247, 3);
            pl_ad.Name = "pl_ad";
            pl_ad.Size = new System.Drawing.Size(96, 37);
            pl_ad.TabIndex = 2;
            // 
            // lb_ad
            // 
            lb_ad.BackColor = System.Drawing.Color.Silver;
            lb_ad.Dock = System.Windows.Forms.DockStyle.Fill;
            lb_ad.Font = new System.Drawing.Font("Terminus (TTF)", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lb_ad.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            lb_ad.Location = new System.Drawing.Point(0, 0);
            lb_ad.Name = "lb_ad";
            lb_ad.Size = new System.Drawing.Size(96, 37);
            lb_ad.TabIndex = 0;
            lb_ad.Text = "????";
            lb_ad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pl_stat
            // 
            pl_stat.BackColor = System.Drawing.Color.OrangeRed;
            pl_stat.Controls.Add(lb_stat);
            pl_stat.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            pl_stat.Location = new System.Drawing.Point(4, 3);
            pl_stat.Name = "pl_stat";
            pl_stat.Size = new System.Drawing.Size(96, 37);
            pl_stat.TabIndex = 1;
            // 
            // lb_stat
            // 
            lb_stat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lb_stat.AutoSize = true;
            lb_stat.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            lb_stat.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            lb_stat.Location = new System.Drawing.Point(7, 11);
            lb_stat.Name = "lb_stat";
            lb_stat.Size = new System.Drawing.Size(80, 17);
            lb_stat.TabIndex = 0;
            lb_stat.Text = "CONN STAT";
            // 
            // tb_Time
            // 
            tb_Time.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            tb_Time.BackColor = System.Drawing.SystemColors.Info;
            tb_Time.Enabled = false;
            tb_Time.Font = new System.Drawing.Font("Terminus (TTF)", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            tb_Time.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            tb_Time.Location = new System.Drawing.Point(106, 3);
            tb_Time.Name = "tb_Time";
            tb_Time.ReadOnly = true;
            tb_Time.Size = new System.Drawing.Size(137, 37);
            tb_Time.TabIndex = 0;
            tb_Time.Text = "Time";
            tb_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.Color.FromArgb(160, 170, 170);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ts_ad, ts_mode, debugToolStripMenuItem, aboutToolStripMenuItem, helpToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1784, 25);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // ts_ad
            // 
            ts_ad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripTextBox1, toolStripSeparator1 });
            ts_ad.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            ts_ad.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            ts_ad.Name = "ts_ad";
            ts_ad.Size = new System.Drawing.Size(92, 21);
            ts_ad.Text = "Aerodrome";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            toolStripTextBox1.MaxLength = 4;
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            toolStripTextBox1.ToolTipText = "Aerodrome";
            toolStripTextBox1.KeyPress += new KeyPressEventHandler(ToolStripTextBox1_KeyPress);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // ts_mode
            // 
            ts_mode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aCDToolStripMenuItem, sMCToolStripMenuItem, sMCACDToolStripMenuItem, aDCToolStripMenuItem, aDCSMCToolStripMenuItem, allToolStripMenuItem });
            ts_mode.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            ts_mode.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            ts_mode.Name = "ts_mode";
            ts_mode.Size = new System.Drawing.Size(92, 21);
            ts_mode.Text = "View Mode";
            // 
            // aCDToolStripMenuItem
            // 
            aCDToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            aCDToolStripMenuItem.Name = "aCDToolStripMenuItem";
            aCDToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            aCDToolStripMenuItem.Text = "ACD";
            aCDToolStripMenuItem.Click += new System.EventHandler(ACDToolStripMenuItem_Click);
            // 
            // sMCToolStripMenuItem
            // 
            sMCToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            sMCToolStripMenuItem.Name = "sMCToolStripMenuItem";
            sMCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            sMCToolStripMenuItem.Text = "SMC";
            sMCToolStripMenuItem.Click += new System.EventHandler(SMCToolStripMenuItem_Click);
            // 
            // sMCACDToolStripMenuItem
            // 
            sMCACDToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            sMCACDToolStripMenuItem.Name = "sMCACDToolStripMenuItem";
            sMCACDToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            sMCACDToolStripMenuItem.Text = "SMC+ACD";
            sMCACDToolStripMenuItem.Click += new System.EventHandler(SMCACDToolStripMenuItem_Click);
            // 
            // aDCToolStripMenuItem
            // 
            aDCToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            aDCToolStripMenuItem.Name = "aDCToolStripMenuItem";
            aDCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            aDCToolStripMenuItem.Text = "ADC";
            aDCToolStripMenuItem.Click += new System.EventHandler(ADCToolStripMenuItem_Click);
            // 
            // aDCSMCToolStripMenuItem
            // 
            aDCSMCToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            aDCSMCToolStripMenuItem.Name = "aDCSMCToolStripMenuItem";
            aDCSMCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            aDCSMCToolStripMenuItem.Text = "ADC+SMC";
            aDCSMCToolStripMenuItem.Click += new System.EventHandler(ADCSMCToolStripMenuItem_Click);
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            allToolStripMenuItem.Text = "All";
            allToolStripMenuItem.Click += new System.EventHandler(AllToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1 });
            debugToolStripMenuItem.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            debugToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            debugToolStripMenuItem.Text = "Debug";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            toolStripMenuItem1.Text = "SocketIO Log";
            toolStripMenuItem1.Click += new System.EventHandler(ToolStripMenuItem1_Click);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += new System.EventHandler(AboutToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { gitHubToolStripMenuItem, documentationToolStripMenuItem, changelogToolStripMenuItem, toolStripSeparator2, settingsToolStripMenuItem });
            helpToolStripMenuItem.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            helpToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            helpToolStripMenuItem.Text = "Help";
            // 
            // gitHubToolStripMenuItem
            // 
            gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            gitHubToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            gitHubToolStripMenuItem.Text = "GitHub";
            gitHubToolStripMenuItem.Click += new System.EventHandler(GitHubToolStripMenuItem_Click);
            // 
            // documentationToolStripMenuItem
            // 
            documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            documentationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            documentationToolStripMenuItem.Text = "Documentation";
            documentationToolStripMenuItem.Click += new System.EventHandler(DocumentationToolStripMenuItem_Click);
            // 
            // changelogToolStripMenuItem
            // 
            changelogToolStripMenuItem.Name = "changelogToolStripMenuItem";
            changelogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            changelogToolStripMenuItem.Text = "Changelog";
            changelogToolStripMenuItem.Click += new System.EventHandler(ChangelogToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            settingsToolStripMenuItem.Text = "Settings";
            settingsToolStripMenuItem.Click += new System.EventHandler(SettingsToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { stripToolStripMenuItem });
            viewToolStripMenuItem.Font = new System.Drawing.Font("Terminus (TTF)", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            viewToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 96);
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            viewToolStripMenuItem.Text = "View";
            // 
            // stripToolStripMenuItem
            // 
            stripToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { normalToolStripMenuItem, smallToolStripMenuItem, tinyToolStripMenuItem });
            stripToolStripMenuItem.Name = "stripToolStripMenuItem";
            stripToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            stripToolStripMenuItem.Text = "Strip";
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.Checked = true;
            normalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            normalToolStripMenuItem.Text = "Normal";
            normalToolStripMenuItem.Click += new System.EventHandler(NormalToolStripMenuItem_Click);
            // 
            // smallToolStripMenuItem
            // 
            smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            smallToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            smallToolStripMenuItem.Text = "Small";
            smallToolStripMenuItem.Click += new System.EventHandler(SmallToolStripMenuItem_Click);
            // 
            // flp_main
            // 
            flp_main.AutoScroll = true;
            flp_main.BackColor = System.Drawing.Color.FromArgb(160, 170, 170);
            flp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            flp_main.Location = new System.Drawing.Point(0, 25);
            flp_main.Margin = new System.Windows.Forms.Padding(0);
            flp_main.Name = "flp_main";
            flp_main.Size = new System.Drawing.Size(1784, 891);
            flp_main.TabIndex = 2;
            flp_main.WrapContents = false;
            // 
            // tt_metar
            // 
            tt_metar.ToolTipTitle = "METAR";
            // 
            // tinyToolStripMenuItem
            // 
            tinyToolStripMenuItem.Name = "tinyToolStripMenuItem";
            tinyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            tinyToolStripMenuItem.Text = "Tiny";
            tinyToolStripMenuItem.Click += new System.EventHandler(TinyToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Gray;
            ClientSize = new System.Drawing.Size(1784, 961);
            Controls.Add(flp_main);
            Controls.Add(pl_controlbar);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "OzStrips";
            FormClosed += new FormClosedEventHandler(MainForm_FormClosed);
            SizeChanged += new System.EventHandler(MainFormSizeChanged);
            Load += new System.EventHandler(MainForm_Load);
            pl_controlbar.ResumeLayout(false);
            pl_controlbar.PerformLayout();
            pl_atis.ResumeLayout(false);
            pl_ad.ResumeLayout(false);
            pl_stat.ResumeLayout(false);
            pl_stat.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel pl_controlbar;
        private System.Windows.Forms.TextBox tb_Time;
        private System.Windows.Forms.Panel pl_stat;
        private System.Windows.Forms.Label lb_stat;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.FlowLayoutPanel flp_main;
        private System.Windows.Forms.ToolStripMenuItem ts_ad;
        private System.Windows.Forms.Panel pl_ad;
        private System.Windows.Forms.Label lb_ad;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.Button bt_inhibit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button bt_force;
        private System.Windows.Forms.ToolStripMenuItem ts_mode;
        private System.Windows.Forms.ToolStripMenuItem aCDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMCACDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aDCSMCToolStripMenuItem;
        private System.Windows.Forms.Button bt_cross;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button bt_pdc;
        private System.Windows.Forms.ToolTip tt_metar;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.Panel pl_atis;
        private System.Windows.Forms.Label lb_atis;
        private System.Windows.Forms.ToolStripMenuItem changelogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stripToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tinyToolStripMenuItem;
    }
}
