using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MouseHover
{
    using ps = Properties.Settings;
    public partial class Tiles : Form
    {
        //System.Windows.Forms.Screen.PrimaryScreen.Bounds
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        //int screenWidth = SystemInformation.PrimaryMonitorSize.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        const int WS_EX_TRANSPARENT = 0x20;
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }
        public Tiles()
        {
            InitializeComponent();
            this.Opacity = (double)ps.Default.tileOpacity;
        }

        private void Tiles_Load(object sender, EventArgs e)
        {
            Screen scr = Screen.FromPoint(this.Location);
            Point pt = new Point(scr.Bounds.Right, scr.Bounds.Top);

            Console.WriteLine(ps.Default.tileMode);
            switch (ps.Default.tileMode)
            {
                default:
                    this.Opacity = 0;
                    this.Hide();
                    return;
                case 1: //Top
                    CreateView(screenWidth, screenHeight / 2, 0, 0);
                    return;
                case 2: //Bottom
                    CreateView(screenWidth, screenHeight / 2, 0, screenHeight / 2);
                    return;
                case 3: //Left
                    CreateView(screenWidth / 2, screenHeight, 0, 0);
                    return;
                case 4: //Right
                    CreateView(screenWidth / 2, screenHeight, pt.X - (screenWidth / 2), pt.Y);
                    return;
            }
        }

        private void CreateView(int Width, int Height, int X, int Y)
        {
            this.Width = Width;
            this.Height = Height;
            this.BackColor = ps.Default.tileColor;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(X, Y);
        }

        private void Tiles_Shown(object sender, EventArgs e)
        {

        }
    }
}
