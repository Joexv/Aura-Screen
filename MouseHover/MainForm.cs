using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NegativeScreen;
using System.Security.Principal;

namespace AirScreen
{
    using ps = Properties.Settings;
    public partial class MainForm : Form
    {
        MouseBox frm2 = new MouseBox();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadHotKeys();
            PopulateControls();
            foreach(string item in Matrix.Keys)
                matrixBox.Items.Add(item);
        }

        private void PopulateControls()
        {
            if (ps.Default.onStartup)
                Toggle();

            #region MainPage
            width.Text = ps.Default.width.ToString();
            height.Text = ps.Default.height.ToString();
            styleBox.Text = ps.Default.style;
            checkBox1.Checked = ps.Default.onStartup;
            checkBox2.Checked = ps.Default.border;
            borderThicccccc.Value = ps.Default.borderThicc;
            checkBox3.Checked = ps.Default.keepInTray;
            //This is only here because my config was stupid and I didn't want to find it and fix it :)
            if (ps.Default.opacity > 1) ps.Default.opacity = 1;
            opacityBar.Value = ps.Default.opacity;

            inversionBox.Checked = ps.Default.invert;
            inversionToggle.Checked = ps.Default.InversionToggle;
            checkBox4.Checked = ps.Default.cursorLock;
            #endregion

            #region hotKeys
            enableHotKey.Text = ps.Default.enableHK;
            invertHotKey.Text = ps.Default.invertHK;
            enlargeHotKey.Text = ps.Default.enlargeHK;
            shrinkHotKey.Text = ps.Default.shrinkHK;
            cylceHotKey.Text = ps.Default.cycleHK;
            cursorLock.Text = ps.Default.cursorLockHK;

            numericUpDown1.Value = ps.Default.ESSize;

            SF_CycleHK.Text = ps.Default.SF_CycleHK;
            SF_ProgramHK.Text = ps.Default.SF_ProgramHK;
            SF_ToggleHK.Text = ps.Default.SF_ToggleHK;

            AO_ToggleHK.Text = ps.Default.AO_ToggleHK;

            BF_ToggleHK.Text = ps.Default.BF_ToggleHK;

            SettingsHK.Text = ps.Default.SettingsHK;
            killswitchHK.Text = ps.Default.KillHK;
            #endregion

            #region AppOverlay
            AO_ByName.Checked = ps.Default.AO_ProcessByName;
            AO_TopMost.Checked = !ps.Default.AO_ProcessByName;
            AO_TextBox.Text = ps.Default.AO_SavedName;
            AO_Opacity.Value = ps.Default.AO_Opacity;
            #endregion

            #region Screen Filters
            Filter_Programs.DataSource = ps.Default.Filter_Programs.Split(';');
            filterStartup.Checked = ps.Default.Filter_OnStartup;
            Filter_OnActive.Checked = ps.Default.Filter_OnActive;
            matrixBox.Text = ps.Default.Filter_LastUsed;
            groupBox1.Enabled = ps.Default.Filter_OnActive;
            if (Filter_OnActive.Checked && filterStartup.Checked)
                Filter_Timer.Start();
            else
                Filter_Timer.Stop();

            if (filterStartup.Checked)
                ToggleFilter();

            #endregion

            #region Cursor

            if (ps.Default.CursorIdle && ps.Default.CursorStartup && !String.IsNullOrEmpty(ps.Default.CursorFile))
                CursorTimer.Start();

            if(!String.IsNullOrEmpty(ps.Default.CursorFile))
                label30.Text = ps.Default.CursorFile;
            cursorIdle.Value = ps.Default.CursorIdleTime;
            cursorStartup.Checked = ps.Default.CursorStartup;
            cursorChange.Checked = ps.Default.CursorIdle;
            #endregion
        }

        private void SaveHotkeys()
        {
            ps.Default.enableHK = enableHotKey.Text;
            ps.Default.invertHK = invertHotKey.Text;
            ps.Default.enlargeHK = enlargeHotKey.Text;
            ps.Default.shrinkHK = shrinkHotKey.Text;
            ps.Default.cycleHK = cylceHotKey.Text;
            ps.Default.cursorLockHK = cursorLock.Text;
            ps.Default.ESSize = numericUpDown1.Value;

            ps.Default.SF_CycleHK = SF_CycleHK.Text;
            ps.Default.SF_ProgramHK = SF_ProgramHK.Text;
            ps.Default.SF_ToggleHK = SF_ToggleHK.Text;

            ps.Default.AO_ToggleHK = AO_ToggleHK.Text;

            ps.Default.BF_ToggleHK = BF_ToggleHK.Text;

            ps.Default.SettingsHK = SettingsHK.Text;
            ps.Default.KillHK = killswitchHK.Text;
            ps.Default.Save();
            ReloadHotKeys();
        }

        private void ReloadHotKeys()
        {
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.enableHK, () => Toggle());
            //GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.disableHK, () => Disable());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.invertHK, () => Invert());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.cursorLockHK, () => { ps.Default.cursorLock = !ps.Default.cursorLock; ps.Default.Save(); });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.enlargeHK, () => { 
                ps.Default.width += (int)ps.Default.ESSize; 
                ps.Default.height += (int)ps.Default.ESSize; 
                ps.Default.Save();});
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.shrinkHK, () => { 
                ps.Default.width -= (int)ps.Default.ESSize; 
                ps.Default.height -= (int)ps.Default.ESSize; 
                ps.Default.Save(); });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.cycleHK, () => CycleTiles());

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.SF_CycleHK, () => {
                if (matrixBox.SelectedIndex < matrixBox.Items.Count - 1)
                    matrixBox.SelectedIndex += 1;
                else
                    matrixBox.SelectedIndex = 0;
                ps.Default.Filter_LastUsed = matrixBox.Text;
                ps.Default.Save();
                FilterUsed = BuiltinMatrices.ChangeColorEffect(Matrix[ps.Default.Filter_LastUsed], FilterUsed);
            });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.SF_ProgramHK, () =>
            {
                Filter_OnActive.Checked = true;
            });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.SF_ToggleHK, () => ToggleFilter());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.AO_ToggleHK, () => ToggleAppOverlay());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.BF_ToggleHK, () => ToggleBlockFilter());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.SettingsHK, () => {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.KillHK, () => {
                Application.Restart();
                Environment.Exit(0);
            });
        }

        public string[] TileModes = { "None", "Top", "Bottom", "Left", "Right" };
        Tiles tile = new Tiles();
        private void CycleTiles()
        {
            if (ps.Default.tileMode < 4)
                ps.Default.tileMode += 1;
            else
                ps.Default.tileMode = 0;
            ps.Default.Save();
            try { tile.Close(); }
            catch { }

            tile = new Tiles();
            tile.Show();
        }

        private void Invert()
        {
            ps.Default.invert = !ps.Default.invert;
            ps.Default.Save();

            inversionBox.CheckedChanged -= new System.EventHandler(inversionBox_CheckedChanged);
            inversionBox.Checked = ps.Default.invert;
            inversionBox.CheckedChanged += new System.EventHandler(inversionBox_CheckedChanged);

            if (ps.Default.invert && ps.Default.InversionToggle && !frm2.Visible)
                Toggle();

            if (!ps.Default.invert && !ps.Default.InversionToggle)
                ReloadCursorOverlay();
            else if (!ps.Default.invert && ps.Default.InversionToggle)
            {
                frm2.Hide();
            }
        }

        private void Toggle()
        {
            if (frm2.Visible)
                frm2.Hide();
            else
                frm2.Show();
        }

        //Enable
        private void button1_Click(object sender, EventArgs e)
        {
            Toggle();
        }
        //Disable
        private void button2_Click(object sender, EventArgs e)
        {
            frm2.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.color = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        //Width
        private void button3_Click(object sender, EventArgs e)
        {
            ps.Default.width = ps.Default.width + 10;
            width.Text = ps.Default.width.ToString();
            ps.Default.Save();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ps.Default.width = ps.Default.width - 10;
            width.Text = ps.Default.width.ToString();
            ps.Default.Save();
        }

        //Height
        private void button6_Click(object sender, EventArgs e)
        {
            ps.Default.height = ps.Default.height + 10;
            height.Text = ps.Default.height.ToString();
            ps.Default.Save();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ps.Default.height = ps.Default.height - 10;
            height.Text = ps.Default.height.ToString();
            ps.Default.Save();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReloadCursorOverlay();
        }

        private void ReloadCursorOverlay()
        {
            frm2.Close();
            frm2 = new MouseBox();
            frm2.Show();
            SaveHotkeys();
        }

        private void styleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps.Default.style = styleBox.Text;
            ps.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.onStartup = checkBox1.Checked;
            ps.Default.Save();
        }

        //Border
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.border = checkBox2.Checked;
            ps.Default.Save();
        }

        private void borderThicccccc_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.borderThicc = (int)borderThicccccc.Value;
            ps.Default.Save();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.borderColor = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.keepInTray = checkBox3.Checked;
            ps.Default.Save();

            //notifyIcon1.Visible = checkBox3.Checked;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && ps.Default.keepInTray)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else
            {
                notifyIcon1.Visible = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ps.Default.invert = false;
            ps.Default.Save();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(CursorHasChanged)
                SystemParametersInfo(0x0057, 0, null, 0);
            frm2.Dispose();
            tile.Dispose();
            notifyIcon1.Dispose();
            NativeMethods.MagUninitialize();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void opacityBar_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.opacity = opacityBar.Value;
            ps.Default.Save();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ToggleBlockFilter();
        }

        private void ToggleBlockFilter()
        {
            ps.Default.tileMode = tileSelect.SelectedIndex + 1;
            ps.Default.Save();
            try { tile.Close(); }
            catch { }
            tile = new Tiles();
            tile.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (tile.Visible)
                tile.Hide();
            else
                tile.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.tileColor = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        private void L_Opacity_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.tileOpacity = L_Opacity.Value;
            ps.Default.Save();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        bool FilterUsed = false;
        private void button14_Click(object sender, EventArgs e)
        {
            ToggleFilter();
        }

        private void ToggleFilter()
        {
            ps.Default.Filter_LastUsed = matrixBox.Text;
            ps.Default.Save();

            if (FilterUsed)
            {
                FilterUsed = false;
                BuiltinMatrices.ChangeColorEffect(BuiltinMatrices.Identity, FilterUsed);
            }
            else
                FilterUsed = BuiltinMatrices.ChangeColorEffect(Matrix[ps.Default.Filter_LastUsed], FilterUsed);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://zerowidthjoiner.net/negativescreen");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps.Default.Filter_LastUsed = matrixBox.Text;
            ps.Default.Save();
        }

        Dictionary<string, float[,]> Matrix = new Dictionary<string, float[,]> {
            { "None", BuiltinMatrices.Identity },
            { "Negative", BuiltinMatrices.Negative },
            { "Negative Greyscale", BuiltinMatrices.NegativeGrayScale },
            { "Negative Hue Shift 180", BuiltinMatrices.NegativeHueShift180 },
            { "Negative Red", BuiltinMatrices.NegativeRed },
            { "Negative Sepia", BuiltinMatrices.NegativeSepia },
            { "Sepia", BuiltinMatrices.Sepia },
            { "Red", BuiltinMatrices.Red },
            { "Greyscale", BuiltinMatrices.GrayScale },
            { "Hue Shift 180", BuiltinMatrices.HueShift180 }
        };

        public Bitmap Transform(Bitmap original, float[,] matrix)
        {
            Bitmap newBmp = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBmp);
            ColorMatrix colorMatrix = new ColorMatrix(matrix.ToJaggedArray());
            ImageAttributes img = new ImageAttributes();
            img.SetColorMatrix(colorMatrix);
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, img);
            g.Dispose();
            return newBmp;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if(pictureBox2.Image != null)
                pictureBox2.Image.Dispose();
            pictureBox2.Image = Transform((Bitmap)pictureBox1.Image, Matrix[matrixBox.Text]);
        }

        private void filterStartup_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.Filter_OnStartup = filterStartup.Checked;
            ps.Default.Save();
        }
        AppOverlay appO = new AppOverlay();

        public static string[] ListProcesses()
        {
            List<string> Results = new List<string>();
            ManagementClass MgmtClass = new ManagementClass("Win32_Process");
            foreach (ManagementObject mo in MgmtClass.GetInstances())
            {
                if (!BlacklistedApps.Contains(mo["Name"]) && !Results.Contains(mo["Name"]))
                {
                    Results.Add((string)mo["Name"]);
                }
            }
                
            return Results.ToArray();
        }

        public static string[] BlacklistedApps = {"RuntimeBroker.exe", "LockApp.exe", "SettingSyncHost.exe", "ShellExperienceHost.exe", "RuntimeBroker.exe", "SearchApp.exe", "RuntimeBroker.exe", "StartMenuExperienceHost.exe", "ctfmon.exe", "asus_framework.exe", "AcPowerNotification.exe", "taskhostw.exe", "sihost.exe", "MsMpEng.exe", "SRService.exe", "ROGLiveService.exe", "sshd.exe", "SessionService.exe", "RtkAndUService64.exe", "SSUService.exe", "RefreshRateService.exe", "LightingService.exe", "taskhostw.exe", "Intel_PIE_Service.exe", "wlanext.exe", "spoolsv.exe", "WmiPrvSE.exe", "Memory Compression", "atieclxx.exe", "atiesrxx.exe", "amdlogsr.exe", "fontdrvhost.exe", "winlogon.exe", "lsass.exe", "wininit.exe", "csrss.exe", "smss.exe", "System Idle Process", "System", "svchost.exe", "system", "conhost.exe", "crss.exe", "dasHost.exe", "CompPkgSrv.exe", "dwm.exe", "dllhost.exe", "rundll32.exe" };

        private void button16_Click(object sender, EventArgs e)
        {
            appO.Close();
            appO = new AppOverlay();
            if (AO_ByName.Checked && AO_TextBox.Text != string.Empty)
            {
                appO.AppName = AO_TextBox.Text;
                ps.Default.AO_SavedName = AO_TextBox.Text;
            }
            else 
            { 
                appO.AppName = String.Empty; 
            }
            appO.Show();
            appO.AO_AttatchTimer.Start();

            ps.Default.AO_ProcessByName = AO_ByName.Checked;
            ps.Default.Save();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.AO_Opacity = AO_Opacity.Value;
            ps.Default.Save();
        }

        private void AO_ColorChange_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.AO_Color = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        private void AO_Refresh_Click(object sender, EventArgs e)
        {
            AO_ComboBox.DataSource = ListProcesses();
        }

        private void AO_Select_Click(object sender, EventArgs e)
        {
            AO_TextBox.Text = AO_ComboBox.Text;
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            ToggleAppOverlay();
        }

        private void ToggleAppOverlay()
        {
            if (appO.AO_AttatchTimer.Enabled)
            {
                appO.AO_AttatchTimer.Stop();
                appO.Hide();
            }
            else
            {
                appO.AO_AttatchTimer.Start();
                appO.Show();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.Filter_OnActive = Filter_OnActive.Checked;
            groupBox1.Enabled = ps.Default.Filter_OnActive;
            ps.Default.Save();

            if (ps.Default.Filter_OnActive)
                Filter_Timer.Start();
            else
            {
                Filter_Timer.Stop();
                BuiltinMatrices.ChangeColorEffect(BuiltinMatrices.Identity, true);
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            Filter_ComboBox.DataSource = ListProcesses();
        }

        private void Filter_Timer_Tick(object sender, EventArgs e)
        {
            if (ps.Default.Filter_OnActive)
            {
                Process[] processes = null;
                string AppName = appO.GetActiveProcessFileName() + ".exe";

                processes = Process.GetProcessesByName(AppName.Substring(0, AppName.Length - 4));
                Process p = processes.FirstOrDefault();
                
                if (p != null && ps.Default.Filter_Programs.Split(';').Contains(AppName, StringComparer.OrdinalIgnoreCase))
                {
                    if(!FilterUsed)
                        FilterUsed = BuiltinMatrices.ChangeColorEffect(Matrix[ps.Default.Filter_LastUsed], false);
                }
                else
                {
                    BuiltinMatrices.ChangeColorEffect(BuiltinMatrices.Identity, true);
                    FilterUsed = false;
                }
            }                
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //Add to Program list
            string joined = "";
            foreach(string item in Filter_Programs.Items)
            {
                if ( !String.IsNullOrWhiteSpace(item))
                    joined += item + ";";
            }
            ps.Default.Filter_Programs = joined + Filter_ComboBox.Text + ";";
            ps.Default.Save();
            Filter_Programs.DataSource = ps.Default.Filter_Programs.Split(';');
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //Add to Program list
            string joined = "";
            foreach (string item in Filter_Programs.Items)
            {
                if(item != Filter_ComboBox.Text && !String.IsNullOrWhiteSpace(item))
                    joined += item + ";";
            }
            ps.Default.Filter_Programs = joined;
            ps.Default.Save();
            Filter_Programs.DataSource = ps.Default.Filter_Programs.Split(';');
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SaveHotkeys();
        }

        private void cursorSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath,
                Title = "Select cursor file",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "cur",
                Filter = "cur files (*.cur)|*.cur",
                FilterIndex = 2,

                ReadOnlyChecked = true,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label30.Text = openFileDialog1.FileName;
                LoadCursor(openFileDialog1.FileName);
            }
        }

        private Cursor IdleCursor;
        public void LoadCursor(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open,FileAccess.Read))
            {
                Cursor result = new Cursor(fs);
                cursorPreview.Cursor = result;
            }
        }

        private void cursorChange_CheckedChanged(object sender, EventArgs e)
        {

        }

        [DllImport("user32.dll")]
        static extern bool SetSystemCursor(IntPtr hcur, uint id);
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction,
            UInt32 uiParam, String pvParam, UInt32 fWinIni);

        //Normal cursor
        private static uint OCR_NORMAL = 32512;

        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string lpFileName);
        private void CursorTimer_Tick(object sender, EventArgs e)
        {
            if(ps.Default.CursorIdle && !String.IsNullOrEmpty(ps.Default.CursorFile))
            {
                if (!CursorHasChanged && CursorIdlePoint == Cursor.Position)
                {
                    CursorHasChanged = true;
                    SetSystemCursor(LoadCursorFromFile(ps.Default.CursorFile), OCR_NORMAL); //(ps.Default.CursorFile);
                    if (filterHide.Checked)
                        frm2.Hide();
                }
            }
        }
        private bool CursorHasChanged = true;
        private Point CursorIdlePoint;

        private void cursorApply_Click(object sender, EventArgs e)
        {
            IdleCursor = cursorPreview.Cursor;
            ps.Default.CursorFile = label30.Text;
            ps.Default.CursorIdleTime = (int)cursorIdle.Value;
            ps.Default.CursorStartup = cursorStartup.Checked;
            ps.Default.CursorIdle = cursorChange.Checked;
            ps.Default.Save();

            CursorTimer.Interval = ps.Default.CursorIdleTime * 1000;

            if (ps.Default.CursorIdle)
                CursorTimer.Start();
            else
                CursorTimer.Stop();
        }

        private void CursorTimer2_Tick(object sender, EventArgs e)
        {
            if (ps.Default.CursorIdle && !String.IsNullOrEmpty(ps.Default.CursorFile))
            {
                if (Cursor.Position != CursorIdlePoint)
                {
                    SystemParametersInfo(0x0057, 0, null, 0);
                    //SetSystemCursor(LoadCursor(IntPtr.Zero, (int)OCR_NORMAL), OCR_CROSS);
                    CursorIdlePoint = Cursor.Position;
                    CursorHasChanged = false;
                    if(filterHide.Checked)
                        frm2.Show();
                }
            }
        }

        private void label30_TextChanged(object sender, EventArgs e)
        {
            LoadCursor(label30.Text);
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
        }

        private void inversionBox_CheckedChanged(object sender, EventArgs e)
        {
            inversionBox.CheckedChanged -= new System.EventHandler(inversionBox_CheckedChanged);
            ps.Default.invert = inversionBox.Checked;
            ps.Default.Save();


            if (ps.Default.invert && ps.Default.InversionToggle && !frm2.Visible)
                Toggle();
            else if (!ps.Default.invert && !ps.Default.InversionToggle)
                ReloadCursorOverlay();
            else if (!ps.Default.invert && ps.Default.InversionToggle)
            {
                frm2.Hide();
            }
            inversionBox.CheckedChanged += new System.EventHandler(inversionBox_CheckedChanged);
        }

        private void inversionToggle_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.InversionToggle = inversionToggle.Checked;
            ps.Default.Save();
        }

        private void checkBox4_CheckedChanged_2(object sender, EventArgs e)
        {
            ps.Default.cursorLock = checkBox4.Checked; 
            ps.Default.Save();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
    internal static class ExtensionMethods
    {
        internal static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}
