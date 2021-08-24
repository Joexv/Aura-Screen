using Microsoft.Win32;
using NegativeScreen;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = Properties.Settings;

    public partial class MouseBox : Form
    {
        private const int WS_EX_TRANSPARENT = 0x20;

        private bool DoInvert = false;

        private Point inversionPT;

        private bool ShiftHeld = false;

        public MouseBox()
        {
            InitializeComponent();
            CreateView();
            MagTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
            LocationTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
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


        public Bitmap CaptureScreen()
        {
            Bitmap b = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        public Bitmap CaptureScreen(Point Location, int Height, int Width)
        {
            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(Location.X, Location.Y, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        public void InvertApp(Point AppPosition, int Height, int Width)
        {
            if (this.BackgroundImage != null)
                this.BackgroundImage.Dispose();

            Console.WriteLine("Inverting App");
            this.Location = AppPosition;
            this.Width = Width;
            this.Height = Height;
            this.Opacity = 0.99; //Form must be even slightly opaque inorder to pass through inputs
            this.Hide();
            this.BackgroundImage = BuiltinMatrices.Transform(CaptureScreen(AppPosition, Height, Width), BuiltinMatrices.Negative);
            this.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ps.Default.CF_DoBorder)
            {
                base.OnPaint(e);
                Pen pen = new Pen(ps.Default.CF_BorderColor, ps.Default.CF_BorderSize);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                switch (ps.Default.CF_Style)
                {
                    case "Rectangle":
                        e.Graphics.DrawRectangle(pen, rect);
                        return;

                    case "Circle":
                        e.Graphics.DrawEllipse(pen, new Rectangle(0, 0, this.Width, this.Width));
                        return;

                    case "Ellipse":
                        e.Graphics.DrawEllipse(pen, rect);
                        return;

                    case "Invert Rectangle":
                        return;
                }
            }
        }
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int initialStyle = GetWindowLong(this.Handle, GWL.ExStyle);
            SetWindowLong(this.Handle, GWL.ExStyle, initialStyle | 0x80000 | 0x20);

            this.Opacity = (double)ps.Default.CF_Opacity;
            if (ps.Default.CF_DoInvert && !ps.Default.useAltInvert)
                StartMag();
        }

        private void AdjustLocation()
        {
            Point pt = Cursor.Position;
            if (ps.Default.CF_Style == "Circle")
                pt.Offset(-1 * this.Width / 2, -1 * this.Width / 2);
            else
                pt.Offset(-1 * this.Width / 2, -1 * this.Height / 2);

            this.Location = pt;
        }

        private void CreateView()
        {
            //Adjust Settings
            this.Width = ps.Default.CF_Width;
            this.Height = ps.Default.CF_Height;
            this.BackColor = ps.Default.CF_Color;
            this.ControlBox = false;
            this.Opacity = (double)ps.Default.CF_Opacity;
            this.TopMost = true;
            this.BackColor = ps.Default.CF_Color;

            Application.DoEvents();
            //this.FormBorderStyle = FormBorderStyle.None;

            // Makes the form circular:
            System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath();

            switch (ps.Default.CF_Style)
            {
                case "Rectangle":
                    GP.AddRectangle(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;

                case "Ellipse":
                    GP.AddEllipse(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;

                case "Circle":
                    //centerX - radius, centerY - radius,
                    //radius + radius, radius + radius
                    GP.AddEllipse(0, 0, this.Width, this.Width);
                    this.Region = new Region(GP);
                    return;

                default:
                    GP.AddRectangle(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;
            }
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!ps.Default.CF_DoInvert)
                CreateView();
        }

        private void Form2_LocationChanged(object sender, EventArgs e)
        {
            //if (DoInvert)
            //    Invert();
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
        }

        private bool InversionPositionCheck()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            if (X < inversionPT.X - this.Width / 2 || X > inversionPT.X + this.Width / 2)
            {
                inversionPT = Cursor.Position;
                return true;
            }
            if (Y < inversionPT.Y - this.Height / 2 || Y > inversionPT.Y + this.Height / 2)
            {
                inversionPT = Cursor.Position;
                return true;
            }

            return false;
        }

        private void Invert()
        {
            if (ps.Default.useAltInvert)
            {
                if (this.BackgroundImage != null)
                    this.BackgroundImage.Dispose();

                this.Opacity = 0.99; //Form must be even slightly opaque inorder to pass through inputs
                inversionPT = Cursor.Position;
                this.Hide();
                this.BackgroundImage = BuiltinMatrices.Transform(CaptureScreen(), BuiltinMatrices.Negative);
                this.Show();
                DoInvert = false;
            }
            else StartMag();
        }

        private void MouseBox_Shown(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if(!ps.Default.CF_DoInvert)
            {
                //RemoveMagnifier();
                if(!ps.Default.CF_Lock)
                    AdjustLocation();
                if (this.Width != ps.Default.CF_Width || this.Height != ps.Default.CF_Height)
                    CreateView();
                if (this.Opacity != (double)ps.Default.CF_Opacity && !ps.Default.CF_DoInvert)
                    CreateView();
            }
        }
        private void InvertTimer_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (ps.Default.CF_DoInvert && !ps.Default.useAltInvert)
                {
                    if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        ShiftHeld = true;

                    if(!ShiftHeld && !ps.Default.CF_Lock)
                        ShiftHeld = false;

                    if (ShiftHeld && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                    {
                        ShiftHeld = false;
                        source.left = Cursor.Position.X - this.Width / 2;
                        source.top = Cursor.Position.Y - this.Height / 2;
                        this.Location = new Point(source.left, source.top);
                    }

                    if (this.Width != ps.Default.CF_Width || this.Height != ps.Default.CF_Height)
                    {
                        CreateView();
                    }
                }
                //Alternate inversion method
                else if (ps.Default.CF_DoInvert && ps.Default.useAltInvert)
                {
                    if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        ShiftHeld = true;
                    if (InversionPositionCheck() && !ShiftHeld && !ps.Default.CF_Lock)
                    {
                        ShiftHeld = false;
                        DoInvert = true;
                        AdjustLocation();
                    }
                    if (ShiftHeld && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                    {
                        ShiftHeld = false;
                        DoInvert = true;
                        Invert();
                    }

                    if (this.Width != ps.Default.CF_Width || this.Height != ps.Default.CF_Height)
                    {
                        CreateView();
                    }
                }
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            UpdateMag();
        }

        private void MouseBox_Resize(object sender, EventArgs e)
        {
            ResizeMagnifier();
        }

        private IntPtr hwndMag;
        private bool initialized;
        RECT magWindowRect = new RECT();
        RECT source = new RECT();

        public void StartMag()
        {
            if (ps.Default.FilterInUse && ps.Default.FilterNum != 1)
            {
                MessageBox.Show("Another filter is currently using Aura Screen's Filterting System. Please diable that before enabling another.");
                this.Close();
            }
            else
            {
                initialized = NativeMethods.MagInitialize();
                if (initialized)
                {
                    ps.Default.FilterInUse = true;
                    ps.Default.FilterNum = 1;
                    ps.Default.Save();

                    SetupMagnifier();
                    MagTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
                    MagTimer.Enabled = true;
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

            if (!ps.Default.CF_Lock)
            {
                source.left = Cursor.Position.X - width / 2;
                source.top = Cursor.Position.Y - height / 2;
            }
            else
            {
                source.left = this.Location.X;
                source.top = this.Location.Y;
            }
            

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
            if (!ShiftHeld && !ps.Default.CF_Lock)
                this.Location = new Point(source.left, source.top);
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }

        public void SetupMagnifier()
        {
            //MagTimer.Start();

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
                "MouseBox", (int)WindowStyles.WS_CHILD | (int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR |
                (int)WindowStyles.WS_VISIBLE,
                magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, this.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }
            ColorEffect colorEffect = new ColorEffect(BuiltinMatrices.Negative);
            NativeMethods.MagSetColorEffect(hwndMag, ref colorEffect);
        }

        protected void RemoveMagnifier()
        {
            if (initialized)
            {
                NativeMethods.MagUninitialize();
            }  
        }

        private void MouseBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            MagTimer.Enabled = false;
            RemoveMagnifier();
        }

        private void MouseBox_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                MagTimer.Enabled = false;
            else if (ps.Default.CF_DoInvert && this.Visible)
                MagTimer.Enabled = true;
        }
    }
}