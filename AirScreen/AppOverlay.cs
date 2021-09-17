using Magnifier;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = Properties.Settings;

    public partial class AppOverlay : Form
    {
        private const int WS_EX_TRANSPARENT = 0x20;

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

        public enum LWA
        {
            ColorKey = 0x1,
            Alpha = 0x2
        }

        public string AppName { get; set; }
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hWnd, int crKey, byte alpha, LWA dwFlags);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, GWL nIndex, int dwNewLong);
        public string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private void AO_InvertTimer_Tick(object sender, EventArgs e)
        {
            Attatch(true);
        }

        private void AppOverlay_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ps.Default.AO_Color;
            this.Opacity = (double)ps.Default.AO_Opacity;

            if (ps.Default.AO_DoTexture && !String.IsNullOrWhiteSpace(ps.Default.AO_Texture))
            {
                if (File.Exists(Application.StartupPath + $"\\Textures\\{ps.Default.AO_Texture}"))
                {
                    Image image = Image.FromFile(Application.StartupPath + $"\\Textures\\{ps.Default.AO_Texture}");
                    this.BackgroundImage = TextureFilter(image, (float)ps.Default.AO_Opacity);
                }
            }
        }

        private Image TextureFilter(Image pImage, float pColorOpacity)
        {
            Image mResult = null;
            Image tempImage = null; //we will set the opacity of pImage to pColorOpacity and copy
                                    //it to tempImage 
            if (pImage != null)
            {
                Graphics g;
                ColorMatrix matrix = new ColorMatrix(new float[][]{
                     new float[] {1F, 0, 0, 0, 0},
                     new float[] {0, 1F, 0, 0, 0},
                     new float[] {0, 0, 1F, 0, 0},
                     new float[] {0, 0, 0, pColorOpacity, 0}, //opacity in rage [0 1]
                     new float[] {0, 0, 0, 0, 1F}});

                ImageAttributes imageAttributes = new ImageAttributes();
                imageAttributes.SetColorMatrix(matrix);
                imageAttributes.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                tempImage = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

                g = Graphics.FromImage(tempImage);
                g.DrawImage(pImage, this.ClientRectangle, 0, 0, pImage.Width, pImage.Height, GraphicsUnit.Pixel, imageAttributes);

                g.Dispose();
                g = null;
                TextureBrush texture = new TextureBrush(tempImage);
                mResult = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);

                g = Graphics.FromImage(mResult);
                g.Clear(ps.Default.CF_Color);
                g.FillRectangle(texture, this.ClientRectangle);
                g.Dispose();
                g = null;

                tempImage.Dispose();
                tempImage = null;
            }

            return mResult;
        }

        private void AppOverlay_Shown(object sender, EventArgs e)
        {
        }

        private void Attatch(bool ForceInvert = false)
        {
            Process[] processes = null;
            string Name = ps.Default.AO_SavedName;

            //Sets Program Name to Currently Running progra, allows for running the overlay over active program rather than predefined
            if (!ps.Default.AO_ByName || String.IsNullOrEmpty(Name))
                Name = GetActiveProcessFileName() + ".exe";

            processes = Process.GetProcessesByName(Name.Substring(0, Name.Length - 4));
            Process p = processes.FirstOrDefault();

            if (ps.Default.AO_DontAttatchToAS && GetActiveProcessFileName() == Process.GetCurrentProcess().ProcessName)
                return;

            if (p != null)
            {
                IntPtr windowHandle;
                windowHandle = p.MainWindowHandle;
                RECT rect = new RECT();
                GetWindowRect(windowHandle, ref rect);

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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Attatch();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}