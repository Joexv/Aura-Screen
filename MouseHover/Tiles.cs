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
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AirScreen
{
    using ps = Properties.Settings;
    public partial class Tiles : Form
    {
        //System.Windows.Forms.Screen.PrimaryScreen.Bounds
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        //int screenWidth = SystemInformation.PrimaryMonitorSize.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public enum GWL
        {
            ExStyle = -20
        }

        public enum WS_EX
        {
            Transparent = 0x20,
            Layered = 0x80000
        }

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
            this.Opacity = (double)ps.Default.tileOpacity;
        }

        public Tiles()
        {
            InitializeComponent();
        }

        private void Tiles_Load(object sender, EventArgs e)
        {

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
            numericUpDown1.Value = ps.Default.tileOpacity;
            Screen scr = Screen.FromPoint(this.Location);
            Point pt = new Point(scr.Bounds.Right, scr.Bounds.Top);

            Console.WriteLine(ps.Default.tileMode);
            switch (ps.Default.tileMode)
            {
                default:
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
                case 5: //Manual
                    CreateView(ps.Default.tilesMW, ps.Default.tilesMH, ps.Default.tilesMX, ps.Default.tilesMY);
                    return;
            }
        }

        private void Tiles_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ps.Default.tilesMW = this.Width;
                ps.Default.tilesMH = this.Height;
                ps.Default.tilesMX = this.Location.X;
                ps.Default.tilesMY = this.Location.Y;
                ps.Default.Save();
            }
        }

        private void Tiles_LocationChanged(object sender, EventArgs e)
        {

        }

        System.Timers.Timer aTimer = new System.Timers.Timer();
        private void button1_Click(object sender, EventArgs e)
        {
            ps.Default.tilesMW = this.Width;
            ps.Default.tilesMH = this.Height;
            ps.Default.tilesMX = this.Location.X;
            ps.Default.tilesMY = this.Location.Y;
            ps.Default.Save();

            this.Opacity = (double)ps.Default.tileOpacity;
            previewTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ps.Default.tilesMW = this.Width;
            ps.Default.tilesMH = this.Height;
            ps.Default.tilesMX = this.Location.X;
            ps.Default.tilesMY = this.Location.Y;
            ps.Default.Save();

            this.Opacity = (double)ps.Default.tileOpacity;
            this.FormBorderStyle = FormBorderStyle.None;
            PreviewButton.Visible = false;
            SaveButton.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            numericUpDown1.Visible = false;
        }

        private void previewTimer_Tick(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            previewTimer.Stop();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.tileColor = colorDialog1.Color;
                ps.Default.Save();

                this.BackColor = colorDialog1.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.tileOpacity = numericUpDown1.Value;
            ps.Default.Save();
        }
    }
}
