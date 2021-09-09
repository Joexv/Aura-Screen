using Microsoft.Win32;
using Magnifier;
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
            this.BackgroundImage = Matrices.Transform(CaptureScreen(AppPosition, Height, Width), Matrices.Negative);
            this.Show();
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

            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
            this.Opacity = (double)ps.Default.CF_Opacity;

            if (ps.Default.CF_DoInvert && !ps.Default.useAltInvert)
                StartMag();
        }

        private void AdjustLocation()
        {
            Point pt = Cursor.Position;
            pt.Offset(-1 * this.Width / 2, -1 * this.Height / 2);
            this.Location = pt;
        }

        private void CreateView()
        {
            GP.Reset();
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
            switch (ps.Default.CF_Style)
            {
                case "Rectangle":
                    GP.AddRectangle(this.ClientRectangle);
                    break;
                case "Ellipse":
                    GP.AddEllipse(this.ClientRectangle);
                    break;
                case "Circle":
                    ps.Default.CF_Width = ps.Default.CF_Height;
                    ps.Default.Save();
                    this.Width = this.Height;
                    GP.AddEllipse(this.ClientRectangle);
                    break;

                case "Triangle":
                    DrawTriangle();
                    break;
                case "Triangle - Hollow":
                    DrawTriangleHollow(true);
                    break;
                case "Triangle - Filled":
                    DrawTriangleHollow(false);
                    break;
                case "Triangle - Flipped":
                    DrawTriangleFlipped();
                    break;

                case "Pentagon":
                    DrawPolygon(5);
                    break;
                case "Hexagon":
                    DrawPolygon(6);
                    break;
                case "Octagon":
                    DrawPolygon(8);
                    break;
            }

            NewRegion = new Region(GP);
            this.Region = NewRegion;
            //this.Refresh();
        }

        private Point[] Polygon;
        private void DrawPolygon(int Sides = 5)
        {
            switch (Sides)
            {
                case 8:
                    Polygon = new Point[] 
                    { 
                        //Top Left Corners
                        new Point(this.Width / 5, 0),
                        new Point(0, this.Height / 5),
                        //Bottom Left Corners
                        new Point(0, this.Height - this.Height / 5),
                        new Point(this.Width / 5, this.Height),
                        //Bottom Right Corners
                        new Point(this.Width - this.Width / 5, this.Height),
                        new Point(this.Width, this.Height - this.Height / 5),
                         //Top Right Corners
                        new Point(this.Width, this.Height / 5),
                        new Point(this.Width - this.Width / 5, 0), 
                    };
                    break;
                case 6:
                    Polygon = new Point[]
                    {
                        new Point(this.Width / 5, 0),
                        new Point(0, this.Height / 2),
                        new Point(this.Width / 5, this.Height),
                        new Point(this.Width - this.Width / 5, this.Height),
                        new Point(this.Width, this.Height / 2),
                        new Point(this.Width - this.Width / 5, 0)
                    };
                    break;
                case 5:
                default:
                    Polygon = new Point[]
                    {
                        //Top Middle
                        new Point(this.Width / 2, 0),
                        //Left Middle
                        new Point(0, this.Height / 3),
                        //Left Corner
                        new Point(this.Width / 6, this.Height),
                        //Right Corner
                        new Point(this.Width - this.Width / 6, this.Height),
                        //Right Middle
                        new Point(this.Width, this.Height / 3),
                    };
                    break;
            }

            GP.AddPolygon(Polygon);
        }


        private Region NewRegion;
        private Point[] Triangle;
        private Point[] InnerTriangle;
        public void DrawTriangle()
        {
            Console.WriteLine("Drawing triangle");
            Point top = new Point(this.Width / 2, 0);
            Point right = new Point(this.Width, this.Height);
            Point left = new Point(0, this.Height);
            if (ps.Default.CF_Flip)
            {
                top = new Point(this.Width / 2, this.Height);
                right = new Point(this.Width, 0);
                left = new Point(0, 0);
            }

            Triangle = new Point[] { top, right, left };
            GP.AddPolygon(Triangle);
        }
        System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath();
        public void DrawTriangleHollow(bool isHollow)
        {
            Console.WriteLine("Drawing triangle");
            Point top = new Point(this.Width / 2, 0);
            Point right = new Point(this.Width, this.Height);
            Point left = new Point(0, this.Height);
            if (ps.Default.CF_Flip)
            {
                top = new Point(this.Width / 2, this.Height);
                right = new Point(this.Width, 0);
                left = new Point(0, 0);
            }

            Triangle = new Point[] { 
                top, 
                right, 
                left,
            };

            InnerTriangle = new Point[]
            {
                new Point(this.Width / 2, (this.Height / 5)),
                new Point(this.Width / 5, this.Height - (this.Height / 10)),
                new Point(this.Width - (this.Width / 5), this.Height - (this.Height / 10))
            };

            if (ps.Default.CF_Flip)
            {
                InnerTriangle[0] = new Point(this.Width / 2, this.Height - this.Height / 5);
                InnerTriangle[1] = new Point(this.Width - this.Width / 5, this.Height / 10);
                InnerTriangle[2] = new Point(this.Width / 5, this.Height / 10);
            }

            if (isHollow)
            {
                GP.AddPolygon(InnerTriangle);
                GP.FillMode = System.Drawing.Drawing2D.FillMode.Alternate;
            }
            
            GP.AddPolygon(Triangle);
        }

        public void DrawTriangleFlipped()
        {
            Console.WriteLine("Drawing triangle");
            Point top = new Point(this.Width / 2, this.Height);
            Point right = new Point(this.Width, 0);
            Point left = new Point(0, 0);
            Triangle = new Point[] { top, right, left };
            GP.AddPolygon(Triangle);
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Cursor.Position.X - this.Width  / 2, Cursor.Position.Y - this.Height / 2);

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
                this.BackgroundImage = Matrices.Transform(CaptureScreen(), Matrices.Negative);
                this.Show();
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
                if(!ps.Default.CF_Lock)
                    AdjustLocation();
                if (this.Width != ps.Default.CF_Width && ps.Default.CF_Style != "Circle" || this.Height != ps.Default.CF_Height)
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
                }
                //Alternate inversion method
                else if (ps.Default.CF_DoInvert && ps.Default.useAltInvert)
                {
                    if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        ShiftHeld = true;
                    if (InversionPositionCheck() && !ShiftHeld && !ps.Default.CF_Lock)
                    {
                        ShiftHeld = false;
                        AdjustLocation();
                    }
                    if (ShiftHeld && !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                    {
                        ShiftHeld = false;
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
                MessageBox.Show($"Another filter is currently using Aura Screen's Filterting System. Please diable that before enabling another. Filter with the ID {ps.Default.FilterNum}");
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

            int width = (int)(magWindowRect.right - magWindowRect.left);
            int height = (int)(magWindowRect.bottom - magWindowRect.top);

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
            this.Refresh();
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
            //Removed the Magnifier cursor so it Looks better
            //(int)MagnifierStyle.MS_SHOWMAGNIFIEDCURSOR
            hwndMag = NativeMethods.CreateWindow((int)ExtendedWindowStyles.WS_EX_TRANSPARENT, NativeMethods.WC_MAGNIFIER,
                        "MouseBox", (int)WindowStyles.WS_CHILD |
                        (int)WindowStyles.WS_VISIBLE,
                        magWindowRect.left, magWindowRect.top, magWindowRect.right, magWindowRect.bottom, this.Handle, IntPtr.Zero, hInst, IntPtr.Zero);

            if (hwndMag == IntPtr.Zero)
            {
                return;
            }
            ColorEffect colorEffect = new ColorEffect(Matrices.Negative);
            NativeMethods.MagSetColorEffect(hwndMag, ref colorEffect);
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("User32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            const int WM_NCPAINT = 0x85;
            if (m.Msg == WM_NCPAINT)
            {
                IntPtr hdc = GetWindowDC(m.HWnd);
                if ((int)hdc != 0)
                {
                    //This Currently doesn't work. It will draw the border fine, but as soon as the magnifier is enabled it kills it.
                    Graphics g = Graphics.FromHdc(hdc);
                    g.FillRectangle(Brushes.Green, new Rectangle(0, 0, 4800, 23));
                    g.Flush();
                    ReleaseDC(m.HWnd, hdc);
                }
            }
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
            {
                MagTimer.Enabled = false;
            }
        }

        private void MouseBox_Paint(object sender, PaintEventArgs e)
        {
            //SolidBrush brush = new SolidBrush(ps.Default.CF_Color);
            //e.Graphics.FillPath(brush, GP);
            if (ps.Default.CF_Style == "Triangle - Filled")
            {
                SolidBrush brush2 = new SolidBrush(ps.Default.CF_BorderColor);
                e.Graphics.FillPolygon(brush2, InnerTriangle);
            }
            PaintBorder(e.Graphics);
        }

        private void PaintBorder(Graphics g)
        {
            // && !ps.Default.CF_DoInvert
            if (ps.Default.CF_DoBorder && !ps.Default.CF_DoInvert)
            {
                Pen pen = new Pen(ps.Default.CF_BorderColor, ps.Default.CF_BorderSize);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                switch (ps.Default.CF_Style)
                {
                    case "Rectangle":
                        g.DrawRectangle(pen, rect);
                        break;
                    case "Circle":
                        this.Width = this.Height;
                        g.DrawEllipse(pen, rect);
                        break;
                    case "Triangle":
                    case "Triangle - Flipped":
                    case "Triangle - Hollow":
                    case "Triangle - Filled":
                        if (Triangle == null)
                            CreateView();
                        g.DrawPolygon(pen, Triangle);
                        break;
                    case "Pentagon":
                    case "Octagon":
                    case "Hexagon":
                        g.DrawPolygon(pen, Polygon);
                        break;
                    default:
                        g.DrawEllipse(pen, rect);
                        break;
                }
            }
        }
    }
}