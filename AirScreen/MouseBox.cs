using AuraScreen.Magnification;
using Microsoft.Win32;
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
            LocationTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
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
            this.BackgroundImage = ColorMatrix.Transform(CaptureScreen(AppPosition, Height, Width), ColorMatrix.Negative);
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

            this.Opacity = (double)ps.Default.CF_Opacity;
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
            if (DoInvert)
                Invert();
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
            if (this.BackgroundImage != null)
                this.BackgroundImage.Dispose();

            this.Opacity = 0.99; //Form must be even slightly opaque inorder to pass through inputs
            inversionPT = Cursor.Position;
            this.Hide();
            this.BackgroundImage = ColorMatrix.Transform(CaptureScreen(), ColorMatrix.Negative);
            this.Show();
            DoInvert = false;
        }

        private void MouseBox_Shown(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!ps.Default.CF_DoInvert && !ps.Default.CF_Lock)
                AdjustLocation();

            if (this.Width != ps.Default.CF_Width || this.Height != ps.Default.CF_Height)
                CreateView();

            if (this.Opacity != (double)ps.Default.CF_Opacity && !ps.Default.CF_DoInvert)
                CreateView();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (ps.Default.CF_DoInvert)
                {
                    if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        ShiftHeld = true;
                    if (InversionPositionCheck() && !ShiftHeld && !ps.Default.CF_Lock)
                    {
                        ShiftHeld = false;
                        DoInvert = true;
                        AdjustLocation();
                    }
                    else if (ShiftHeld && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                    {
                        ShiftHeld = false;
                        DoInvert = true;
                        //AdjustLocation();
                        Invert();
                    }

                    if (this.Width != ps.Default.CF_Width || this.Height != ps.Default.CF_Height)
                    {
                        if (!ps.Default.AppInvertLock)
                            CreateView();
                    }
                }
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            UpdateMaginifier();
        }

        private IntPtr hwndMag;
        private float magnification;
        private bool initialized;
        internal RECT magWindowRect = new RECT();

        public void StartMag()
        {
            initialized = NativeMethods.MagInitialize();
            if (initialized)
            {
                SetupMagnifier();
                MagTimer.Interval = NativeMethods.USER_TIMER_MINIMUM;
                MagTimer.Enabled = true;
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

        public virtual void UpdateMaginifier()
        {
            if ((!initialized) || (hwndMag == IntPtr.Zero))
                return;

            POINT mousePoint = new POINT();
            RECT sourceRect = new RECT();

            NativeMethods.GetCursorPos(ref mousePoint);

            int width = (int)((magWindowRect.right - magWindowRect.left) / magnification);
            int height = (int)((magWindowRect.bottom - magWindowRect.top) / magnification);

            sourceRect.left = mousePoint.x - width / 2;
            sourceRect.top = mousePoint.y - height / 2;


            // Don't scroll outside desktop area.
            if (sourceRect.left < 0)
            {
                sourceRect.left = 0;
            }
            if (sourceRect.left > NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width)
            {
                sourceRect.left = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) - width;
            }
            sourceRect.right = sourceRect.left + width;

            if (sourceRect.top < 0)
            {
                sourceRect.top = 0;
            }
            if (sourceRect.top > NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height)
            {
                sourceRect.top = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) - height;
            }
            sourceRect.bottom = sourceRect.top + height;

            // Set the source rectangle for the magnifier control.
            NativeMethods.MagSetWindowSource(hwndMag, sourceRect);

            // Reclaim topmost status, to prevent unmagnified menus from remaining in view. 
            NativeMethods.SetWindowPos(this.Handle, NativeMethods.HWND_TOPMOST, 0, 0, 0, 0,
                (int)SetWindowPosFlags.SWP_NOACTIVATE | (int)SetWindowPosFlags.SWP_NOMOVE | (int)SetWindowPosFlags.SWP_NOSIZE);

            // Force redraw.
            NativeMethods.InvalidateRect(hwndMag, IntPtr.Zero, true);
        }

        public float Magnification
        {
            get { return magnification; }
            set
            {
                if (magnification != value)
                {
                    magnification = value;
                    // Set the magnification factor.
                    Transformation matrix = new Transformation(magnification);
                    NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);
                }
            }
        }

        public void SetupMagnifier()
        {
            if (!initialized)
                return;

            IntPtr hInst;

            hInst = NativeMethods.GetModuleHandle(null);

            // Make the window opaque.
            this.AllowTransparency = true;
            this.TransparencyKey = Color.Empty;
            this.Opacity = 255;

            // Create a magnifier control that fills the client area.
            NativeMethods.GetClientRect(this.Handle, ref magWindowRect);
            hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_CLIENTEDGE, NativeMethods.WC_MAGNIFIER,
                "MagnifierWindow", (int)WindowStyles.WS_CHILD | (int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR |
                (int)WindowStyles.WS_VISIBLE,
                magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, this.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }

            // Set the magnification factor.
            Transformation matrix = new Transformation(magnification);
            NativeMethods.MagSetWindowTransform(hwndMag, ref matrix);

            NativeMethods.MagSetColorEffect(hwndMag, ref Color)
        }

        protected void RemoveMagnifier()
        {
            if (initialized)
                NativeMethods.MagUninitialize();
        }
    }
}