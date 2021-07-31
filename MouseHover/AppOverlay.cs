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

namespace MouseHover
{
    public partial class AppOverlay : Form
    {
        public string AppName { get; set; }
        public AppOverlay()
        {
            InitializeComponent();
        }
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
            if (String.IsNullOrEmpty(AppName))
                processes = Process.GetProcessesByName(GetActiveProcessFileName());
            else
                processes = Process.GetProcessesByName(AppName.Substring(0, AppName.Length - 4)); //(AppName);//

            if (GetActiveProcessFileName() != Process.GetCurrentProcess().ProcessName)
            {
                Process p = processes.FirstOrDefault();
                IntPtr windowHandle;
                if (p != null)
                {
                    this.Visible = true;
                    windowHandle = p.MainWindowHandle;
                    RECT rect = new RECT();
                    _ = GetWindowRect(windowHandle, ref rect);

                    this.Location = new Point(rect.Left, rect.Top - 5);
                    this.Size = new Size(rect.Right - rect.Left + 10, rect.Bottom - rect.Top);

                    this.BackColor = Color.LimeGreen;
                    this.Opacity = 0.45;
                    this.TopMost = true;
                    this.FormBorderStyle = FormBorderStyle.None;
                }
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

        private string GetActiveProcessFileName()
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
    }
}
