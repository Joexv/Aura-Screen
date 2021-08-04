using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScreen
{
    using ps = Properties.Settings;
    public partial class AppOverlay : Form
    {
        public string AppName { get; set; }
        public AppOverlay()
        {
            InitializeComponent();
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
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
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

        }

        private void Attatch()
        {
            Process[] processes = null;
            string Name = ps.Default.AO_SavedName;
            if (!ps.Default.AO_ProcessByName || String.IsNullOrEmpty(Name))
                Name = GetActiveProcessFileName() + ".exe";

            processes = Process.GetProcessesByName(Name.Substring(0, Name.Length - 4));

            Console.WriteLine(Name);
            Process p = processes.FirstOrDefault();

            if (p != null && GetActiveProcessFileName() != Process.GetCurrentProcess().ProcessName && (GetActiveProcessFileName() + ".exe" == Name))
            {
                IntPtr windowHandle;
                windowHandle = p.MainWindowHandle;
                RECT rect = new RECT();
                _ = GetWindowRect(windowHandle, ref rect);

                this.Location = new Point(rect.Left, rect.Top - 5);
                this.Size = new Size(rect.Right - rect.Left + 10, rect.Bottom - rect.Top);

                this.BackColor = ps.Default.AO_Color;
                this.Opacity = (double)ps.Default.AO_Opacity;
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Visible = true;
            }
            else
                this.Visible = false;
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
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
        }
    }
}
