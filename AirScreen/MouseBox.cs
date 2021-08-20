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
        public MouseBox()
        {
            InitializeComponent();
            CreateView();
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

        private void CreateView()
        {
            //Adjust Settings
            this.Width = ps.Default.width;
            this.Height = ps.Default.height;
            this.BackColor = ps.Default.color;
            this.ControlBox = false;
            this.Opacity = (double)ps.Default.opacity;
            this.TopMost = true;
            this.BackColor = ps.Default.color;

            Application.DoEvents();
            //this.FormBorderStyle = FormBorderStyle.None;

            // Makes the form circular:
            System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath();

            switch (ps.Default.style)
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

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (!ps.Default.invert && !ps.Default.cursorLock)
                AdjustLocation();

            if (this.Width != ps.Default.width || this.Height != ps.Default.height)
                CreateView();

            if (this.Opacity != (double)ps.Default.opacity && !ps.Default.invert)
                CreateView();
        }

        private void AdjustLocation()
        {
            Point pt = Cursor.Position;
            if (ps.Default.style == "Circle")
                pt.Offset(-1 * this.Width / 2, -1 * this.Width / 2);
            else
                pt.Offset(-1 * this.Width / 2, -1 * this.Height / 2);
            this.Location = pt;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ps.Default.border)
            {
                base.OnPaint(e);
                Pen pen = new Pen(ps.Default.borderColor, ps.Default.borderThicc);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                switch (ps.Default.style)
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

        public Bitmap Transform(Bitmap source)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the negative color matrix
            ColorMatrix colorMatrix = new ColorMatrix(
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

        private bool ShiftHeld = false;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (ps.Default.invert)
                {
                    if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift))
                        ShiftHeld = true;
                    if (InversionPositionCheck() && !ShiftHeld && !ps.Default.cursorLock)
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

                    if (this.Width != ps.Default.width || this.Height != ps.Default.height)
                    {
                        if (!ps.Default.AppInvertLock)
                            CreateView();
                    }
                }
            }
        }

        private bool DoInvert = false;

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

        private Point inversionPT;

        private void Form2_LocationChanged(object sender, EventArgs e)
        {
            if (DoInvert)
                Invert();
        }

        private void Invert()
        {
            if(this.BackgroundImage != null)
                this.BackgroundImage.Dispose();

            this.Opacity = 0.99; //Form must be even slightly opaque inorder to pass through inputs
            inversionPT = Cursor.Position;
            this.Hide();
            this.BackgroundImage = Transform(CaptureScreen());
            this.Show();
            DoInvert = false;
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
            this.BackgroundImage = Transform(CaptureScreen(AppPosition, Height, Width));
            this.Show();
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

        public static int GetWindowsScaling()
        {
            var currentDPI = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96);
            return (int)(96 / (float)currentDPI); ;
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (!ps.Default.invert)
                CreateView();
        }

        private void Form2_MouseLeave(object sender, EventArgs e)
        {
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            //if(!ps.Default.invert)
            //CreateView();
        }

        private void MouseBox_Shown(object sender, EventArgs e)
        {

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);

            this.Opacity = (double)ps.Default.opacity;
        }
    }
}