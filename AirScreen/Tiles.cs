using EventHook;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AuraScreen
{
    using keyboard = System.Windows.Input.Keyboard;
    using ps = Properties.Settings;

    public partial class Tiles : Form
    {
        //System.Windows.Forms.Screen.PrimaryScreen.Bounds
        private int screenWidth = Screen.PrimaryScreen.Bounds.Width;

        //int screenWidth = SystemInformation.PrimaryMonitorSize.Width;
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;

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

        private const int WS_EX_TRANSPARENT = 0x20;

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
            this.Opacity = (double)ps.Default.BF_Opacity;

            if (invert.Checked)
            {
                InvertKeyChecker.Start();
                DoInvert();
            }
        }

        public Tiles()
        {
            InitializeComponent();
        }

        private void MouseEvent(object sender, EventHook.MouseEventArgs f)
        {
            if (ps.Default.BF_Invert && !SaveButton.Visible && this.Visible)
            {
                if (f.Message == EventHook.Hooks.MouseMessages.WM_MOUSEWHEEL)
                    Invoke((MethodInvoker)delegate { Scrollin(); });
            }
        }

        private void Scrollin()
        {
            ScrollingTimer.Stop();
            wasScrolling = true;
            this.Opacity = (double)ps.Default.BF_Opacity;
            this.BackColor = ps.Default.BF_Color;
            this.BackgroundImage = null;
            ScrollingTimer.Start();
        }

        private bool wasScrolling = false;
        private EventHookFactory eventHookFactory = new EventHookFactory();
        private MouseWatcher mouseWatcher;

        private void Tiles_Load(object sender, EventArgs e)
        {
            mouseWatcher = eventHookFactory.GetMouseWatcher();
            mouseWatcher.OnMouseInput += (s, f) => MouseEvent(s, f);
        }

        private void CreateView(int Width, int Height, int X, int Y)
        {
            this.Width = Width;
            this.Height = Height;
            this.BackColor = ps.Default.BF_Color;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Location = new Point(X, Y);
        }

        private void Tiles_Shown(object sender, EventArgs e)
        {
            numericUpDown1.Value = ps.Default.BF_Opacity;
            Screen scr = Screen.FromPoint(this.Location);
            Point pt = new Point(scr.Bounds.Right, scr.Bounds.Top);

            Console.WriteLine(ps.Default.BF_Location);
            switch (ps.Default.BF_Location)
            {
                default:
                    this.Hide();
                    break;

                case 1: //Top
                    CreateView(screenWidth, screenHeight / 2, 0, 0);
                    break;

                case 2: //Bottom
                    CreateView(screenWidth, screenHeight / 2, 0, screenHeight / 2);
                    break;

                case 3: //Left
                    CreateView(screenWidth / 2, screenHeight, 0, 0);
                    break;

                case 4: //Right
                    CreateView(screenWidth / 2, screenHeight, pt.X - (screenWidth / 2), pt.Y);
                    break;

                case 5: //Manual
                    CreateView(ps.Default.BF_Width, ps.Default.BF_Height, ps.Default.BF_X, ps.Default.BF_Y);
                    break;
            }

            //Inversion Settings
            invert.Checked = ps.Default.BF_Invert;
            time.Value = ps.Default.BF_InvertTime;
            switch (ps.Default.tileKey)
            {
                case 0:
                    shift.Checked = true;
                    return;

                case 1:
                    r.Checked = true;
                    return;

                case 2:
                    squwiggly.Checked = true;
                    return;

                case 3:
                    f1.Checked = true;
                    return;
            }
        }

        private void Tiles_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                ps.Default.BF_Width = this.Width;
                ps.Default.BF_Height = this.Height;
                ps.Default.BF_X = this.Location.X;
                ps.Default.BF_Y = this.Location.Y;
                ps.Default.Save();
                if (ps.Default.BF_Invert && ps.Default.BF_Scroll)
                    mouseWatcher.Start();
            }
            else
            {
                if (ps.Default.BF_Invert && !SaveButton.Visible && ps.Default.BF_Scroll)
                    mouseWatcher.Start();
                else
                    mouseWatcher.Stop();
            }
        }

        private void Tiles_LocationChanged(object sender, EventArgs e)
        {
        }

        private System.Timers.Timer aTimer = new System.Timers.Timer();

        private void button1_Click(object sender, EventArgs e)
        {
            ps.Default.BF_Width = this.Width;
            ps.Default.BF_Height = this.Height;
            ps.Default.BF_X = this.Location.X;
            ps.Default.BF_Y = this.Location.Y;
            ps.Default.Save();

            this.Opacity = (double)ps.Default.BF_Opacity;
            previewTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ps.Default.BF_Width = this.Width;
            ps.Default.BF_Height = this.Height;
            ps.Default.BF_X = this.Location.X;
            ps.Default.BF_Y = this.Location.Y;
            ps.Default.Save();

            this.Opacity = (double)ps.Default.BF_Opacity;
            this.FormBorderStyle = FormBorderStyle.None;
            PreviewButton.Visible = false;
            SaveButton.Visible = false;
            label1.Visible = false;
            button1.Visible = false;
            numericUpDown1.Visible = false;
            groupBox1.Visible = false;

            if (invert.Checked)
            {
                DoInvert();
                InvertKeyChecker.Start();
                InvertTimer.Start();
            }
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
                ps.Default.BF_Color = colorDialog1.Color;
                ps.Default.Save();

                this.BackColor = colorDialog1.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Opacity = numericUpDown1.Value;
            ps.Default.Save();
        }

        private void time_ValueChanged(object sender, EventArgs e)
        {
            InvertTimer.Interval = (int)(time.Value * 1000);
            ps.Default.BF_InvertTime = (int)time.Value;
            ps.Default.Save();
        }

        private void DoInvert()
        {
            isHeld = false;
            //Cheap way of checking if in edit mode
            if (!SaveButton.Visible && this.Visible)
            {
                if(this.BackgroundImage != null)
                    this.BackgroundImage.Dispose();
                InvertTimer.Stop();
                this.Opacity = 0.99;
                this.Hide();
                Application.DoEvents();
                this.BackgroundImage = 
                    (CaptureScreen());
                Application.DoEvents();
                this.Show();
                InvertTimer.Interval = ps.Default.BF_InvertTime * 1000;
                InvertTimer.Start();
            }
        }

        public Bitmap CaptureScreen()
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        public Bitmap Transform(Bitmap source)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the negative color matrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
            new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
             });

            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height),
                        0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();

            return newBitmap;
        }

        private void InvertTimer_Tick(object sender, EventArgs e)
        {
            DoInvert();
        }

        private bool isHeld = false;

        private void InvertKeyChecker_Tick(object sender, EventArgs e)
        {
            if (shift.Checked)
            {
                if (keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                    isHeld = true;
            }
            else if (f1.Checked)
            {
                if (keyboard.IsKeyDown(System.Windows.Input.Key.F1))
                    isHeld = true;
            }
            else if (r.Checked)
            {
                if (keyboard.IsKeyDown(System.Windows.Input.Key.R))
                    isHeld = true;
            }
            else if (squwiggly.Checked)
            {
                if (keyboard.IsKeyDown(System.Windows.Input.Key.OemTilde))
                    isHeld = true;
            }

            if (isHeld)
            {
                if (shift.Checked)
                {
                    if (!keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        DoInvert();
                }
                else if (f1.Checked)
                {
                    if (!keyboard.IsKeyDown(System.Windows.Input.Key.F1))
                        DoInvert();
                }
                else if (r.Checked)
                {
                    if (!keyboard.IsKeyDown(System.Windows.Input.Key.R))
                        DoInvert();
                }
                else if (squwiggly.Checked)
                {
                    if (!keyboard.IsKeyDown(System.Windows.Input.Key.OemTilde))
                        DoInvert();
                }
            }
        }

        private void invert_CheckedChanged(object sender, EventArgs e)
        {
            if (invert.Checked)
            {
                InvertKeyChecker.Start();
                InvertTimer.Start();
            }
            else
            {
                InvertKeyChecker.Stop();
                InvertTimer.Stop();
            }

            ps.Default.BF_Invert = invert.Checked;
            ps.Default.Save();
        }

        private void groupBox1_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void ScrollingTimer_Tick(object sender, EventArgs e)
        {
            if (wasScrolling)
            {
                Console.WriteLine("Scrolling Ended, inverting");
                wasScrolling = false;
                DoInvert();
                ScrollingTimer.Stop();
            }
        }

        private void shift_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 0;
            ps.Default.Save();
        }

        private void r_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 1;
            ps.Default.Save();
        }

        private void f1_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 3;
            ps.Default.Save();
        }

        private void squwiggly_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 2;
            ps.Default.Save();
        }

        private void Tiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            eventHookFactory.Dispose();
        }
    }
}