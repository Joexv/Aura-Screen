﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = AuraScreen.Properties.Settings;
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }
        public Configurator conf { get; set; }
        private void Welcome_Load(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                tab.Text = "";
            }

            changelog.Text = beta0020;
        }
        private string beta0020 = 
            "Started work on initial welcome screen\n" +
            "Bug fixes regarding the ToolBox and Cursor Filters\n" +
            "Started work on redesigning UI\n" +
            "Started work on adding logging of errors for simpler bug reporting";

        private void button1_Click(object sender, EventArgs e)
        {
#if !DEBUG
            ps.Default.ShowWelcomeScreen = false;
            ps.Default.Save();
#endif
            this.Hide();
            conf.Show();
        }
    }
}
