using EventHook;
using NegativeScreen;
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
        private int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        //public Configurator conf { get; set; }
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (ps.Default.BF_Invert)
            { StartMag(); }
            else if(!ps.Default.EnterEditMode)
            {
                int wl = GetWindowLong(this.Handle, GWL.ExStyle);
                wl = wl | 0x80000 | 0x20;
                SetWindowLong(this.Handle, GWL.ExStyle, wl);
                SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
                this.Opacity = (double)ps.Default.BF_Opacity;
            }
            
        }
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
            InvertTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
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
            if (ps.Default.useAltInvert)
            {
                mouseWatcher = eventHookFactory.GetMouseWatcher();
                mouseWatcher.OnMouseInput += (s, f) => MouseEvent(s, f);
            }
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
            if (ps.Default.useAltInvert)
            {
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
            if (ps.Default.BF_Invert && !ps.Default.EnterEditMode)
            {
                Console.WriteLine("Inverting block filter");
                StartMag();
            }

            if (ps.Default.EnterEditMode)
            {
                Console.WriteLine("Entering Edit Mode");
                EnterEditMode();
            }
        }

        private void Tiles_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                InvertTimer.Enabled = false;
                ps.Default.BF_Width = this.Width;
                ps.Default.BF_Height = this.Height;
                ps.Default.BF_X = this.Location.X;
                ps.Default.BF_Y = this.Location.Y;
                ps.Default.Save();
                if (ps.Default.BF_Invert && ps.Default.BF_Scroll && ps.Default.useAltInvert)
                    mouseWatcher.Start();
            }
            else
            {
               if (ps.Default.BF_Invert && !SaveButton.Visible && ps.Default.BF_Scroll && ps.Default.useAltInvert)
                    mouseWatcher.Start();
               else if(ps.Default.useAltInvert)
                    mouseWatcher.Stop();

                if (ps.Default.BF_Invert && !ps.Default.EnterEditMode)
                    InvertTimer.Enabled = true;
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

            ps.Default.EnterEditMode = false;
            ps.Default.Save();

            if (ps.Default.BF_Invert)
            {
                if (ps.Default.useAltInvert)
                {
                    DoInvert();
                    InvertKeyChecker.Start();
                    InvertTimer.Start();
                }
                else
                    StartMag();
            }
            else
            {
                if (initialized)
                    RemoveMagnifier();
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
            if (ps.Default.useAltInvert)
            {
                InvertTimer.Interval = (int)(time.Value * 1000);
                ps.Default.BF_InvertTime = (int)time.Value;
                ps.Default.Save();
            }
        }

        private void DoInvert()
        {
            isHeld = false;
            //Cheap way of checking if in edit mode
            if (!SaveButton.Visible && this.Visible && ps.Default.useAltInvert)
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
            else if (!ps.Default.EnterEditMode)
            {
                UpdateMag();
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

        public void EnterEditMode()
        {
            this.Refresh();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.PreviewButton.Visible = true;
            this.SaveButton.Visible = true;
            this.label1.Visible = true;
            this.button1.Visible = true;
            this.numericUpDown1.Visible = true;
            this.groupBox1.Visible = true;
            this.AllowTransparency = false;
            this.Opacity = 1;
        }

        public void ExitEditMode()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.PreviewButton.Visible = false;
            this.SaveButton.Visible = false;
            this.label1.Visible = false;
            this.button1.Visible = false;
            this.numericUpDown1.Visible = false;
            this.groupBox1.Visible = false;
            this.Opacity = (double)ps.Default.BF_Opacity;
            this.AllowTransparency = true;
            ps.Default.EnterEditMode = false;
            ps.Default.Save();
        }

        private void InvertTimer_Tick(object sender, EventArgs e)
        {
            if(ps.Default.useAltInvert)
                DoInvert();
            else if(ps.Default.BF_Invert && !SaveButton.Visible)
                UpdateMag();
        }

        private bool isHeld = false;

        private void InvertKeyChecker_Tick(object sender, EventArgs e)
        {
            if (ps.Default.useAltInvert)
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
        }

        private void invert_CheckedChanged(object sender, EventArgs e)
        {

            ps.Default.BF_Invert = invert.Checked;
            ps.Default.Save();
            /*
            if (invert.Checked)
            {
                if (ps.Default.useAltInvert)
                {
                    InvertKeyChecker.Start();
                    InvertTimer.Start();
                }
                else
                {
                    StartMag();
                }
                
            }
            else
            {
                InvertKeyChecker.Stop();
                InvertTimer.Stop();
                RemoveMagnifier();
            }
            */
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
            if(eventHookFactory != null)
                eventHookFactory.Dispose();
            InvertTimer.Enabled = false;

            ps.Default.EnterEditMode = false;
            ps.Default.Save();
        }

        private IntPtr hwndMag;
        private bool initialized;
        RECT magWindowRect = new RECT();
        RECT source = new RECT();

        public void StartMag()
        {
            if (ps.Default.FilterInUse && ps.Default.FilterNum != 2)
            {
                MessageBox.Show("Another filter is currently using Aura Screen's Filterting. Please diable that before enabling another.");
                this.Close();
            }
            else if(!ps.Default.EnterEditMode)
            {
                Console.WriteLine("Starting Magnification API");
                initialized = NativeMethods.MagInitialize();
                if (initialized)
                {
                    ps.Default.FilterInUse = true;
                    ps.Default.FilterNum = 2;
                    ps.Default.Save();

                    Console.WriteLine("Magnification initialized");
                    InvertTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
                    SetupMagnifier();
                }
            }
        }
        protected virtual void ResizeMagnifier()
        {
            if (initialized && (hwndMag != IntPtr.Zero))
            {
                NativeMethods.GetClientRect(this.Handle, ref magWindowRect);
                // Resize the control to fill the window.
                NativeMethods.SetWindowPos(hwndMag, IntPtr.Zero,
                    magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, 0);
            }
        }
        public void UpdateMag()
        {
            //AdjustLocation();
            if (!initialized || hwndMag == IntPtr.Zero)
                return;

            int width = (int)((magWindowRect.right - magWindowRect.left));
            int height = (int)((magWindowRect.bottom - magWindowRect.top));

            source.left = this.Location.X;
            source.top = this.Location.Y;

            if (source.left < 0)
                source.left = 0;
            if (source.left > Screen.PrimaryScreen.Bounds.Width - width)
                source.left = Screen.PrimaryScreen.Bounds.Width - width;

            source.right = source.left + width;
            if (source.top < 0)
                source.top = 0;
            if (source.top > Screen.PrimaryScreen.Bounds.Height - height)
                source.top = Screen.PrimaryScreen.Bounds.Height - height;

            source.bottom = source.top + height;

            NativeMethods.MagSetWindowSource(hwndMag, source);
            NativeMethods.SetWindowPos(this.Handle,
               NativeMethods.HWND_TOPMOST, magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom,
               (int)SetWindowPosFlags.SWP_NOACTIVATE |
               (int)SetWindowPosFlags.SWP_NOMOVE |
               (int)SetWindowPosFlags.SWP_NOSIZE);
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
            //this.Location = new Point(source.left, source.top);
        }

        public void SetupMagnifier()
        {
            DoubleBuffered = true;
            if (!initialized)
                return;

            IntPtr hInst;

            hInst = NativeMethods.GetModuleHandle(null);

            // Make the window opaque.
            this.AllowTransparency = true;
            this.TransparencyKey = Color.Empty;
            this.BackColor = Color.Empty;
            this.Opacity = 0.99;

            // Create a magnifier control that fills the client area.
            NativeMethods.GetClientRect(this.Handle, ref magWindowRect);
            hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_TRANSPARENT, NativeMethods.WC_MAGNIFIER,
                "BlockFilter", (int)WindowStyles.WS_CHILD | (int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR |
                (int)WindowStyles.WS_VISIBLE,
                magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, this.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }

            ColorEffect colorEffect = new ColorEffect(BuiltinMatrices.Negative);
            NativeMethods.MagSetColorEffect(hwndMag, ref colorEffect);
            InvertTimer.Start();
        }

        protected void RemoveMagnifier()
        {
            if (initialized)
                NativeMethods.MagUninitialize();
        }

        private void Tiles_Resize(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Location = new Point(0, this.Location.Y);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, this.Location.Y);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X, Screen.PrimaryScreen.Bounds.Height - this.Height);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.Bounds.Height;
        }
    }
}