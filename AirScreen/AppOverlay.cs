using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = Properties.Settings;

    public partial class AppOverlay : Form
    {
        public string AppName { get; set; }

        public AppOverlay()
        {
            InitializeComponent();
            if (ps.Default.AO_OnStart)
            {
                AO_InvertTimer.Interval = ps.Default.AO_InvertTime * 1000;
                if (ps.Default.AO_Invert)
                    AO_InvertTimer.Start();
                else
                    AO_InvertTimer.Stop();

                AO_AttatchTimer.Start();
            }
            else
            {
                AO_AttatchTimer.Stop();
                AO_InvertTimer.Stop();
                this.Visible = false;
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

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private void AppOverlay_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ps.Default.AO_Color;
            this.Opacity = (double)ps.Default.AO_Opacity;
        }

        private Point OldLocation = new Point(0, 0);

        private void Attatch(bool ForceInvert = false)
        {
            Process[] processes = null;
            string Name = ps.Default.AO_SavedName;
            if (!ps.Default.AO_ProcessByName || String.IsNullOrEmpty(Name))
                Name = GetActiveProcessFileName() + ".exe";

            processes = Process.GetProcessesByName(Name.Substring(0, Name.Length - 4));

            Process p = processes.FirstOrDefault();
            OldLocation = this.Location;
            if (p != null && GetActiveProcessFileName() != Process.GetCurrentProcess().ProcessName && (GetActiveProcessFileName() + ".exe" == Name))
            {
                IntPtr windowHandle;
                windowHandle = p.MainWindowHandle;
                RECT rect = new RECT();
                _ = GetWindowRect(windowHandle, ref rect);

                if (!ps.Default.AO_Invert)
                {
                    this.Location = new Point(rect.Left, rect.Top);
                    this.Size = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                    this.TopMost = true;
                    this.Show();
                }
            }
            else
                this.Visible = false;
        }

        private void Invert(Point Location, Size size)
        {
            this.Hide();
            Console.WriteLine("Inverting AO");
            this.Opacity = 0.99; //Form must be even slightly opaque inorder to pass through inputs
            this.BackgroundImage = Transform(CaptureScreen(Location, Size));
            Application.DoEvents();
            this.Show();
        }

        private void AO_InvertTimer_Tick(object sender, EventArgs e)
        {
            Attatch(true);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private Bitmap CaptureScreen(Point Location, Size size)
        {
            Bitmap b = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(Location.X, Location.Y, 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        private Bitmap Transform(Bitmap source)
        {
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);
            Graphics g = Graphics.FromImage(newBitmap);
            ColorMatrix colorMatrix = new ColorMatrix(
            new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
             });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height),
                        0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        public string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Attatch();
        }

        private void AppOverlay_Shown(object sender, EventArgs e)
        {
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int wl = GetWindowLong(this.Handle, GWL.ExStyle);
            wl = wl | 0x80000 | 0x20;
            SetWindowLong(this.Handle, GWL.ExStyle, wl);
            SetLayeredWindowAttributes(this.Handle, 0, 128, LWA.Alpha);
            this.Opacity = (double)ps.Default.AO_Opacity;
        }
    }
}