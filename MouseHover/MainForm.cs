using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NegativeScreen;

namespace MouseHover
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
            #endregion

            #region hotKeys
            enableHotKey.Text = ps.Default.enableHK;
            invertHotKey.Text = ps.Default.invertHK;
            enlargeHotKey.Text = ps.Default.enlargeHK;
            shrinkHotKey.Text = ps.Default.shirnkHK;
            cylceHotKey.Text = ps.Default.cycleHK;
            #endregion

            #region AppOverlay
            AO_ByName.Checked = ps.Default.AO_ProcessByName;
            AO_TopMost.Checked = !ps.Default.AO_ProcessByName;
            AO_TextBox.Text = ps.Default.AO_SavedName;
            AO_Opacity.Value = ps.Default.AO_Opacity;
            #endregion
        }

        private void SaveHotkeys()
        {
            ps.Default.enableHK = enableHotKey.Text;
            //ps.Default.disableHK = disableHotKey.Text;
            ps.Default.invertHK = invertHotKey.Text;
            ps.Default.enlargeHK = enlargeHotKey.Text;
            ps.Default.shirnkHK = shrinkHotKey.Text;
            ps.Default.Save();
        }

        private void ReloadHotKeys()
        {
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.enableHK, () => Toggle());
            //GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.disableHK, () => Disable());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.invertHK, () => Invert());

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.enlargeHK, () => { ps.Default.width += 10; ps.Default.height += 10; ps.Default.Save(); });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.shirnkHK, () => { ps.Default.width -= 10; ps.Default.height -= 10; ps.Default.Save(); });

            GlobalHotKey.RegisterHotKey("Control + Alt + " + ps.Default.cycleHK, () => CycleTiles());
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

            if (!ps.Default.invert)
                Reload();

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
            Reload();
        }

        private void Reload()
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
            frm2.Dispose();
            tile.Dispose();
            notifyIcon1.Dispose();
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
            if (FilterUsed) 
            {
                FilterUsed = false;
                BuiltinMatrices.ChangeColorEffect(BuiltinMatrices.Identity, FilterUsed);
            }
            else
                FilterUsed = BuiltinMatrices.ChangeColorEffect(Matrix[matrixBox.Text], FilterUsed);

            ps.Default.lastFiler = matrixBox.Text;
            ps.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://zerowidthjoiner.net/negativescreen");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
            pictureBox2.Image.Dispose();
            pictureBox2.Image = Transform((Bitmap)pictureBox1.Image, Matrix[matrixBox.Text]);
        }

        private void filterStartup_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.filterStartup = filterStartup.Checked;
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
                    Console.WriteLine(mo["Name"]);
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
            if (appO.AO_AttatchTimer.Enabled)
            {
                appO.AO_AttatchTimer.Stop();
                appO.Hide();
            }
            else{
                appO.AO_AttatchTimer.Start();
                appO.Show();
            }
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
