using Magnifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = AuraScreen.Properties.Settings;

    public partial class Configurator : Form
    {
        public static bool initialized { get; set; } = false;
        public MouseBox mousebox = new MouseBox();
        public Dictionary<string, float[,]> Matrix = new Dictionary<string, float[,]> {
            { "None", Matrices.Identity },
            { "Negative", Matrices.Negative },
            { "Negative Greyscale", Matrices.NegativeGrayScale },
            { "Negative Hue Shift 180", Matrices.NegativeHueShift180 },
            { "Negative Red", Matrices.NegativeRed },
            { "Negative Sepia", Matrices.NegativeSepia },
            { "Sepia", Matrices.Sepia },
            { "Red", Matrices.Red },
            { "Greyscale", Matrices.GrayScale },
            { "Hue Shift 180", Matrices.HueShift180 }
        };

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public Configurator()
        {
            InitializeComponent();
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
                Process.GetCurrentProcess().Kill();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
        }

        private void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            File.WriteAllText("Error.txt", e.ToString());
            SystemCleanup();
            MessageBox.Show("You bafoon, you absolute ignoramous. You broke it. Now what? Gonna cry? Send that error.txt over to the devs and they will take a look.");
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            PopulateControls();
            ReloadHotKeys();
            foreach (string item in Matrix.Keys)
                matrixBox.Items.Add(item);

            notifyIcon1.Visible = true;

            toolbox.MF = this;
            if (ps.Default.ShowWelcomeScreen)
            {
                Welcome welcome = new Welcome
                {
                    conf = this
                };
                welcome.Show();
                welcome.Activate();
                this.Hide();
            }


            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            foreach (TabPage tab in tabControl1.TabPages)
            {
                //tab.Text = "";
            }

            foreach (var button in flowLayoutPanel1.Controls.OfType<Button>())
            {
                Size newSize = new Size(50, 50);
                if (button.Image != null)
                    button.Image = (Image)(new Bitmap(button.Image, newSize));
            }
        }
        Color Clicked = Color.FromArgb(230, 237, 183);
        Color Default = Color.FromArgb(10, 150, 170);

        public void PopulateControls()
        {
            ps.Default.FilterInUse = false;
            if (ps.Default.CF_OnStartup)
                ToggleCF();

            #region MainPage

            width.Text = ps.Default.CF_Width.ToString();
            height.Text = ps.Default.CF_Height.ToString();
            styleBox.Text = ps.Default.CF_Style;
            checkBox1.Checked = ps.Default.CF_OnStartup;
            checkBox2.Checked = ps.Default.CF_DoBorder;
            borderThicccccc.Value = ps.Default.CF_BorderSize;
            checkBox3.Checked = ps.Default.keepInTray;
            //This is only here because my config was stupid and I didn't want to find it and fix it :)
            if (ps.Default.CF_Opacity > (decimal)0.99)
            {
                ps.Default.CF_Opacity = (decimal)0.99;
                ps.Default.Save();
            }

            opacityBar.Value = ps.Default.CF_Opacity;

            inversionBox.Checked = ps.Default.CF_DoInvert;
            inversionToggle.Checked = ps.Default.CF_InversionToggle;
            checkBox4.Checked = ps.Default.CF_Lock;

            tileInvert.Checked = ps.Default.BF_Invert;
            time.Value = ps.Default.BF_InvertTime;
            tileScrollDisable.Checked = ps.Default.BF_Scroll;

            #endregion MainPage

            #region hotKeys

            enableHotKey.Text = ps.Default.HK_ToggleCF;
            invertHotKey.Text = ps.Default.HK_InvertCF;
            enlargeHotKey.Text = ps.Default.HK_EnlargeCF;
            shrinkHotKey.Text = ps.Default.HK_ShrinkCF;
            cylceHotKey.Text = ps.Default.HK_CycleBF;
            cursorLock.Text = ps.Default.HK_LockCF;

            numericUpDown1.Value = ps.Default.CF_SizeIncrement;

            SF_CycleHK.Text = ps.Default.HK_CycleSF;
            SF_ProgramHK.Text = ps.Default.HK_SFOnActive;
            SF_ToggleHK.Text = ps.Default.HK_ToggleSF;

            AO_ToggleHK.Text = ps.Default.HK_ToggleAO;

            BF_ToggleHK.Text = ps.Default.HK_ToggleBF;

            SettingsHK.Text = ps.Default.HK_ShowConfig;
            killswitchHK.Text = ps.Default.HK_KillSwitch;
            toolboxHK.Text = ps.Default.HK_ShowTB;

            checkBox6.Checked = ps.Default.TB_Cursor;

            tilesManualHK.Text = ps.Default.HK_EditBF;

            #endregion hotKeys

            #region AppOverlay

            AO_ByName.Checked = ps.Default.AO_ByName;
            AO_TopMost.Checked = !ps.Default.AO_ByName;
            AO_TextBox.Text = ps.Default.AO_SavedName;
            AO_Opacity.Value = ps.Default.AO_Opacity;

            //AO_Invert.Checked = ps.Default.AO_Invert;
            //AO_Time.Value = ps.Default.AO_InvertTime;

            AO_Invert.Checked = false;
            AO_Time.Value = ps.Default.AO_InvertTime;

            AO_Start.Checked = ps.Default.AO_OnStart;

            #endregion AppOverlay

            #region Screen Filters

            Filter_Programs.DataSource = ps.Default.SF_Programs.Split(';');
            filterStartup.Checked = ps.Default.SF_OnStartup;
            Filter_OnActive.Checked = ps.Default.SF_OnActive;
            matrixBox.Text = ps.Default.SF_LastUsed;
            groupBox1.Enabled = ps.Default.SF_OnActive;
            if (Filter_OnActive.Checked && filterStartup.Checked)
                Filter_Timer.Start();
            else
                Filter_Timer.Stop();

            if (filterStartup.Checked)
                ToggleFilter();

            #endregion Screen Filters

            #region Cursor

            if (ps.Default.CI_Enabled && ps.Default.CI_OnStartup && !String.IsNullOrEmpty(ps.Default.CI_File))
                CursorTimer.Start();

            if (!String.IsNullOrEmpty(ps.Default.CI_File))
                label30.Text = ps.Default.CI_File;
            cursorIdle.Value = ps.Default.CI_Interval;
            cursorStartup.Checked = ps.Default.CI_OnStartup;
            cursorChange.Checked = ps.Default.CI_Enabled;

            #endregion Cursor

            #region Other

            checkBox7.Checked = ps.Default.doAdjust;
            tbWidth.Value = ps.Default.tbWidth;
            tbHeight.Value = ps.Default.tbHeight;
            tbRows.Value = ps.Default.tbPad;
            groupBox10.Enabled = ps.Default.doAdjust;

            checkBox3.Checked = ps.Default.keepInTray;
            checkBox5.Checked = !ps.Default.TB_AutoHide;

            checkBox8.Checked = ps.Default.useAltInvert;

            if (ps.Default.BF_Location != 0)
                tileSelect.SelectedIndex = ps.Default.BF_Location - 1;

            switch (ps.Default.tileKey)
            {
                case 0:
                    shift.Checked = true;
                    break;

                case 1:
                    r.Checked = true;
                    break;

                case 2:
                    squwiggly.Checked = true;
                    break;

                case 3:
                    f1.Checked = true;
                    break;
            }

            #endregion Other
        }

        public void SaveHotkeys()
        {
            ps.Default.HK_ToggleCF = enableHotKey.Text;
            ps.Default.HK_InvertCF = invertHotKey.Text;
            ps.Default.HK_EnlargeCF = enlargeHotKey.Text;
            ps.Default.HK_ShrinkCF = shrinkHotKey.Text;
            ps.Default.HK_CycleBF = cylceHotKey.Text;
            ps.Default.HK_LockCF = cursorLock.Text;
            ps.Default.CF_SizeIncrement = numericUpDown1.Value;

            ps.Default.HK_CycleSF = SF_CycleHK.Text;
            ps.Default.HK_SFOnActive = SF_ProgramHK.Text;
            ps.Default.HK_ToggleSF = SF_ToggleHK.Text;

            ps.Default.HK_ToggleAO = AO_ToggleHK.Text;

            ps.Default.HK_ToggleBF = BF_ToggleHK.Text;

            ps.Default.HK_ShowConfig = SettingsHK.Text;
            ps.Default.HK_KillSwitch = killswitchHK.Text;
            ps.Default.HK_ShowTB = toolboxHK.Text;
            ps.Default.HK_EditBF = tilesManualHK.Text;
            ps.Default.Save();
            ReloadHotKeys();
        }

        public void ReloadHotKeys()
        {
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ToggleCF, () => ToggleCF());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_InvertCF, () => {
                inversionBox.Checked = !inversionBox.Checked;
                ReloadCF();
            });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_LockCF, () => { ps.Default.CF_Lock = !ps.Default.CF_Lock; ps.Default.Save(); });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_EnlargeCF, () =>
            {
                if (ps.Default.CF_Height < 10001)
                {
                    ps.Default.CF_Width += (int)ps.Default.CF_SizeIncrement;
                    ps.Default.CF_Height += (int)ps.Default.CF_SizeIncrement;
                    ps.Default.Save();
                    ReloadCF();
                }
            });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ShrinkCF, () =>
            {
                if (ps.Default.CF_Width > 30)
                {
                    ps.Default.CF_Width -= (int)ps.Default.CF_SizeIncrement;
                    ps.Default.CF_Height -= (int)ps.Default.CF_SizeIncrement;
                    ps.Default.Save();
                    ReloadCF();
                }
            });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_CycleBF, () => CycleTiles());

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_CycleSF, () => CycleFilter());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_SFOnActive, () =>
            {
                Filter_OnActive.Checked = true;
            });
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ToggleSF, () => ToggleFilter());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ToggleAO, () => ToggleAppOverlay());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ToggleBF, () => ToggleBlockFilter());
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ShowConfig, () =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_KillSwitch, () =>
            {
                Application.Restart();
                Environment.Exit(0);
            });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_ShowTB, () =>
            {
                if (toolbox.Visible)
                    toolbox.Hide();
                else
                {
                    toolbox.Show();
                    toolbox.WindowState = FormWindowState.Normal;
                    if (ps.Default.TB_Cursor)
                        toolbox.Location = Cursor.Position;
                }
            });

            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_EditBF, () =>
            {
                EditTiles();
            });
        }

        public void CycleFilter()
        {
            if (matrixBox.SelectedIndex < matrixBox.Items.Count - 1)
                matrixBox.SelectedIndex += 1;
            else
                matrixBox.SelectedIndex = 0;
            ps.Default.SF_LastUsed = matrixBox.Text;
            ps.Default.Save();
            SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
        }

        public string[] TileModes = { "None", "Top", "Bottom", "Left", "Right" };
        public Tiles blockfilter = new Tiles();

        public void CycleTiles(int Mode = 0)
        {
            if (Mode != 0)
            {
                ps.Default.BF_Location = Mode;
            }
            else
            {
                if (ps.Default.BF_Location < 5)
                    ps.Default.BF_Location += 1;
                else
                    ps.Default.BF_Location = 0;
            }
            ps.Default.Save();

            ReloadTiles();
        }

        public void DisableInvert()
        {
            inversionBox.Checked = false;
            ReloadCF();
        }

        public void EnableInvert()
        {
            inversionBox.Checked = true;
            ReloadCF();
        }

        public void HideCursor()
        {
            mousebox.Hide();
        }

        public void ShowCursor()
        {
            mousebox.Show();
        }

        public void ToggleCF()
        {
            if (mousebox.IsDisposed || ps.Default.CF_DoInvert && !mousebox.Visible)
            {
                ReloadCF();
                return;
            }
                
            if (mousebox.Visible)
                mousebox.Hide();
            else
                mousebox.Show();
        }

        //Enable
        public void button1_Click(object sender, EventArgs e)
        {
            ToggleCF();
        }

        //Disable
        public void button2_Click(object sender, EventArgs e)
        {
            mousebox.Hide();
        }

        public void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ps.Default.CF_Color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.CF_Color = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        //Width
        public void button3_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Width = ps.Default.CF_Width + 25;
            if (ps.Default.CF_Width > 10001)
                ps.Default.CF_Width = 10000;
            width.Text = ps.Default.CF_Width.ToString();
            ps.Default.Save();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Width = ps.Default.CF_Width - 25;
            if (ps.Default.CF_Width < 30)
                ps.Default.CF_Width = 30;
            width.Text = ps.Default.CF_Width.ToString();
            ps.Default.Save();
        }

        //Height
        public void button6_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Height = ps.Default.CF_Height + 25;
            if (ps.Default.CF_Height > 10001)
                ps.Default.CF_Height = 10000;
            height.Text = ps.Default.CF_Height.ToString();
            ps.Default.Save();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Height = ps.Default.CF_Height - 25;
            if (ps.Default.CF_Height < 30)
                ps.Default.CF_Height = 30;
            height.Text = ps.Default.CF_Height.ToString();
            ps.Default.Save();
        }

        public void button8_Click(object sender, EventArgs e)
        {
            ReloadCF();
        }

        public bool FilterInUse(int Override = 0)
        {
            if (ps.Default.CF_DoInvert && mousebox.Visible && Override != 1)
            {
                tileInvert.Checked = false;
                return true;
            }

            if (ps.Default.BF_Invert && blockfilter.Visible && Override != 2)
            {
                inversionBox.Checked = false;
                return true;
            }
            if (SF_FilterInUse && Override != 3)
            {
                inversionBox.Checked = false;
                tileInvert.Checked = false;
                return true;
            }

            return false;
        }

        public void ReloadCF()
        {
            try
            {
                if(!mousebox.IsDisposed)
                    mousebox.Dispose();
                Application.DoEvents();

                mousebox = new MouseBox();
                mousebox.Show();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void EditTiles()
        {
            if (ps.Default.BF_Location == 5 && blockfilter.Visible)
            {
                if (!ps.Default.EnterEditMode)
                {
                    ps.Default.EnterEditMode = true;
                    ps.Default.Save();
                    ReloadTiles();
                }
                else
                {
                    ps.Default.EnterEditMode = false;
                    ps.Default.Save();

                    if (ps.Default.BF_Invert)
                        ReloadTiles();
                    else
                        blockfilter.ExitEditMode();
                }
            }
        }

        public void styleBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps.Default.CF_Style = styleBox.Text;
            ps.Default.Save();
        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_OnStartup = checkBox1.Checked;
            ps.Default.Save();
        }

        //Border
        public void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_DoBorder = checkBox2.Checked;
            ps.Default.Save();
        }

        public void borderThicccccc_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.CF_BorderSize = (int)borderThicccccc.Value;
            ps.Default.Save();
        }

        public void button9_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ps.Default.CF_BorderColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.CF_BorderColor = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        public void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.keepInTray = checkBox3.Checked;
            ps.Default.Save();

            //notifyIcon1.Visible = checkBox3.Checked;
        }

        public void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && ps.Default.keepInTray)
                this.ShowInTaskbar = false;
            else
                PopulateControls();
        }

        public void button10_Click(object sender, EventArgs e)
        {
            ps.Default.CF_DoInvert = false;
            ps.Default.Save();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemCleanup();
        }

        public void SystemCleanup()
        {
            try
            {
                if (CursorHasChanged)
                    SystemParametersInfo(0x0057, 0, null, 0);
                mousebox.Dispose();
                blockfilter.Dispose();
            }
            catch { Console.WriteLine("Something happened! Shit!"); }

            try
            {
                notifyIcon1.Visible = false;
                notifyIcon1.Dispose();
                NativeMethods.MagUninitialize();
            }
            catch { }
        }

        private Toolbox toolbox = new Toolbox();

        public void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"Icon {e.Button} Clicked");
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!toolbox.Visible || toolbox.WindowState == FormWindowState.Minimized)
                    {
                        toolbox.Show();
                        toolbox.WindowState = FormWindowState.Normal;
                        Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
                        int left = workingArea.Width - toolbox.Width;
                        int top = workingArea.Height - toolbox.Height;
                        toolbox.Location = new Point(left, top);
                        toolbox.Activate();
                    }
                    else
                    {
                        toolbox.Hide();
                    }
                    break;

                case MouseButtons.Right:
                    break;
            }
        }

        public void opacityBar_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.CF_Opacity = opacityBar.Value;
            ps.Default.Save();
        }

        public void tabPage4_Click(object sender, EventArgs e)
        {
        }

        public void ReloadTiles()
        {
            Console.WriteLine("Reloading Block Filter");
            ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
            ps.Default.BF_Opacity = tileOpacity.Value;
            ps.Default.BF_Invert = tileInvert.Checked;
            ps.Default.Save();
            try
            {
                if (!blockfilter.IsDisposed)
                    blockfilter.Dispose();
                Application.DoEvents();
                blockfilter = new Tiles();
                blockfilter.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            ReloadTiles();
        }

        public void ToggleBlockFilter()
        {
            ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
            ps.Default.BF_Opacity = tileOpacity.Value;
            ps.Default.Save();

            if (blockfilter.IsDisposed)
            {
                ReloadTiles();
                return;
            }
                
            if (blockfilter.Visible)
                blockfilter.Hide();
            else
                blockfilter.Show();
        }

        public void button12_Click(object sender, EventArgs e)
        {
            ToggleBlockFilter();
        }

        public void button11_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ps.Default.BF_Color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.BF_Color = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        public void L_Opacity_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Opacity = tileOpacity.Value;
            ps.Default.Save();
        }

        public void label13_Click(object sender, EventArgs e)
        {
        }

        public bool SF_FilterInUse = false;

        public void button14_Click(object sender, EventArgs e)
        {
            ToggleFilter();
        }

        public void ToggleFilter()
        {
            ps.Default.SF_LastUsed = matrixBox.Text;
            ps.Default.Save();
            if (matrixBox.Text == "None" || SF_FilterInUse)
            {
                SF_FilterInUse = false;
                ps.Default.FilterInUse = false;
                ps.Default.FilterNum = 0;
                Matrices.ChangeColorEffect(Matrices.Identity);
            }
            else if(!CheckFilter(3))
            {
                    ps.Default.FilterInUse = true;
                    ps.Default.FilterNum = 3;
                    SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
            }
            else
            {
                FilterError();
            }

            ps.Default.Save();
        }

        public void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://zerowidthjoiner.net/negativescreen");
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ps.Default.SF_LastUsed = matrixBox.Text;
            ps.Default.Save();
        }

        public void button15_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
                pictureBox2.Image.Dispose();
            pictureBox2.Image = Matrices.Transform((Bitmap)pictureBox1.Image, Matrix[matrixBox.Text]);
        }

        public void filterStartup_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.SF_OnStartup = filterStartup.Checked;
            ps.Default.Save();
        }

        public AppOverlay appO = new AppOverlay();

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

        public static string[] BlacklistedApps = { "RuntimeBroker.exe", "LockApp.exe", "SettingSyncHost.exe", "ShellExperienceHost.exe", "RuntimeBroker.exe", "SearchApp.exe", "RuntimeBroker.exe", "StartMenuExperienceHost.exe", "ctfmon.exe", "asus_framework.exe", "AcPowerNotification.exe", "taskhostw.exe", "sihost.exe", "MsMpEng.exe", "SRService.exe", "ROGLiveService.exe", "sshd.exe", "SessionService.exe", "RtkAndUService64.exe", "SSUService.exe", "RefreshRateService.exe", "LightingService.exe", "taskhostw.exe", "Intel_PIE_Service.exe", "wlanext.exe", "spoolsv.exe", "WmiPrvSE.exe", "Memory Compression", "atieclxx.exe", "atiesrxx.exe", "amdlogsr.exe", "fontdrvhost.exe", "winlogon.exe", "lsass.exe", "wininit.exe", "csrss.exe", "smss.exe", "System Idle Process", "System", "svchost.exe", "system", "conhost.exe", "crss.exe", "dasHost.exe", "CompPkgSrv.exe", "dwm.exe", "dllhost.exe", "rundll32.exe" };

        public void button16_Click(object sender, EventArgs e)
        {
            ReloadAO();
        }

        private void ReloadAO()
        {
            ps.Default.AO_ByName = AO_ByName.Checked;
            ps.Default.AO_Opacity = AO_Opacity.Value;
            ps.Default.AO_Invert = AO_Invert.Checked;
            ps.Default.AO_InvertTime = (int)AO_Time.Value;
            ps.Default.Save();

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
        }

        public void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.AO_Opacity = AO_Opacity.Value;
            ps.Default.Save();
        }

        public void AO_ColorChange_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = ps.Default.AO_Color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ps.Default.AO_Color = colorDialog1.Color;
                ps.Default.Save();
            }
        }

        public void AO_Refresh_Click(object sender, EventArgs e)
        {
            AO_ComboBox.DataSource = ListProcesses();
        }

        public void AO_Select_Click(object sender, EventArgs e)
        {
            AO_TextBox.Text = AO_ComboBox.Text;
        }

        public void button16_Click_1(object sender, EventArgs e)
        {
            ToggleAppOverlay();
        }

        public void ToggleAppOverlay()
        {
            if (appO.AO_AttatchTimer.Enabled || appO.Visible)
            {
                appO.AO_AttatchTimer.Stop();
                appO.Hide();
            }
            else
            {
                ReloadAO();
            }
        }

        public void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.SF_OnActive = Filter_OnActive.Checked;
            groupBox1.Enabled = ps.Default.SF_OnActive;
            ps.Default.Save();

            if (ps.Default.SF_OnActive)
                Filter_Timer.Start();
            else
            {
                Filter_Timer.Stop();
                Matrices.ChangeColorEffect(Matrices.Identity);
            }
        }

        public void button10_Click_1(object sender, EventArgs e)
        {
            Filter_ComboBox.DataSource = ListProcesses();
        }

        public void Filter_Timer_Tick(object sender, EventArgs e)
        {
            if (ps.Default.SF_OnActive)
            {
                Process[] processes = null;
                string AppName = appO.GetActiveProcessFileName() + ".exe";

                processes = Process.GetProcessesByName(AppName.Substring(0, AppName.Length - 4));
                Process p = processes.FirstOrDefault();

                if (p != null && ps.Default.SF_Programs.Split(';').Contains(AppName, StringComparer.OrdinalIgnoreCase))
                {
                    if (!SF_FilterInUse)
                        SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
                }
                else
                {
                    Matrices.ChangeColorEffect(Matrices.Identity);
                    SF_FilterInUse = false;
                }
            }
        }

        public void button17_Click(object sender, EventArgs e)
        {
            //Add to Program list
            string joined = "";
            foreach (string item in Filter_Programs.Items)
            {
                if (!String.IsNullOrWhiteSpace(item))
                    joined += item + ";";
            }
            ps.Default.SF_Programs = joined + Filter_ComboBox.Text + ";";
            ps.Default.Save();
            Filter_Programs.DataSource = ps.Default.SF_Programs.Split(';');
        }

        public void button18_Click(object sender, EventArgs e)
        {
            //Add to Program list
            string joined = "";
            foreach (string item in Filter_Programs.Items)
            {
                if (item != Filter_ComboBox.Text && !String.IsNullOrWhiteSpace(item))
                    joined += item + ";";
            }
            ps.Default.SF_Programs = joined;
            ps.Default.Save();
            Filter_Programs.DataSource = ps.Default.SF_Programs.Split(';');
        }

        public void button13_Click(object sender, EventArgs e)
        {
            SaveHotkeys();
        }

        public void cursorSelect_Click(object sender, EventArgs e)
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
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                Cursor result = new Cursor(fs);
                cursorPreview.Cursor = result;
            }
        }

        public void cursorChange_CheckedChanged(object sender, EventArgs e)
        {
        }

        [DllImport("user32.dll")]
        private static extern bool SetSystemCursor(IntPtr hcur, uint id);

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction,
            UInt32 uiParam, String pvParam, UInt32 fWinIni);

        //Normal cursor
        private static uint OCR_NORMAL = 32512;

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursorFromFile(string lpFileName);

        public void CursorTimer_Tick(object sender, EventArgs e)
        {
            if (ps.Default.CI_Enabled && !String.IsNullOrEmpty(ps.Default.CI_File))
            {
                if (!CursorHasChanged && CursorIdlePoint == Cursor.Position)
                {
                    CursorHasChanged = true;
                    SetSystemCursor(LoadCursorFromFile(ps.Default.CI_File), OCR_NORMAL); //(ps.Default.CursorFile);
                    if (filterHide.Checked)
                        mousebox.Hide();
                }
            }
        }

        private bool CursorHasChanged = true;
        private Point CursorIdlePoint;

        public void cursorApply_Click(object sender, EventArgs e)
        {
            IdleCursor = cursorPreview.Cursor;
            ps.Default.CI_File = label30.Text;
            ps.Default.CI_Interval = (int)cursorIdle.Value;
            ps.Default.CI_OnStartup = cursorStartup.Checked;
            ps.Default.CI_Enabled = cursorChange.Checked;
            ps.Default.Save();

            CursorTimer.Interval = ps.Default.CI_Interval * 1000;

            if (ps.Default.CI_Enabled)
                CursorTimer.Start();
            else
                CursorTimer.Stop();
        }

        public void CursorTimer2_Tick(object sender, EventArgs e)
        {
            if (ps.Default.CI_Enabled && !String.IsNullOrEmpty(ps.Default.CI_File))
            {
                if (Cursor.Position != CursorIdlePoint)
                {
                    SystemParametersInfo(0x0057, 0, null, 0);
                    //SetSystemCursor(LoadCursor(IntPtr.Zero, (int)OCR_NORMAL), OCR_CROSS);
                    CursorIdlePoint = Cursor.Position;
                    CursorHasChanged = false;
                    if (filterHide.Checked)
                        mousebox.Show();
                }
            }
        }

        public void label30_TextChanged(object sender, EventArgs e)
        {
            LoadCursor(label30.Text);
        }

        public void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
        }

        public void inversionBox_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_DoInvert = inversionBox.Checked;
            ps.Default.Save();

            if (!inversionBox.Checked)
            {
                if(CheckFilter(1))
                {
                    ps.Default.FilterInUse = false;
                    ps.Default.FilterNum = 0;
                    ps.Default.Save();
                }
                mousebox.Close();
            }
            else 
            {
                if (CheckFilter(1))
                {
                    FilterError();
                    inversionBox.Checked = false;
                    ps.Default.CF_DoInvert = false;
                    ps.Default.Save();
                }
                else
                {
                    ps.Default.FilterInUse = true;
                    ps.Default.FilterNum = 1;
                    ps.Default.Save();
                }
            }

            if (mousebox.Visible)
                ReloadCF();
        }

        public void FilterError()
        {
            string Filter = "";
            switch (ps.Default.FilterNum)
            {
                case 0:
                    Filter = "Hol up. There isn't currently a filter using it. Try applying it again.";
                    break;
                case 1:
                    Filter = "Currently being used by the Cursor Filter";
                    break;
                case 2:
                    Filter = "Currently being used by the Block/Tile Filter";
                    break;
                case 3:
                    Filter = "Currently being usd by the Screen Filter";
                    break;
            }
            MessageBox.Show($"Another filter is currently using Aura Screen's Filterting. Please diable that before enabling another.\n\n{Filter}");
        }

        //Returns False if either no filter is running or current filter is the one checked against
        private bool CheckFilter(int Filter = 0)
        {
            if (Filter == ps.Default.FilterNum)
                return false;
            return ps.Default.FilterInUse;
        }

        public void inversionToggle_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_InversionToggle = inversionToggle.Checked;
            ps.Default.Save();
        }

        public void checkBox4_CheckedChanged_2(object sender, EventArgs e)
        {
            ps.Default.CF_Lock = checkBox4.Checked;
            ps.Default.Save();
        }

        public void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        public void toolTip1_Popup(object sender, PopupEventArgs e)
        {
        }

        public void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        public void contextMenuStrip1_Click(object sender, EventArgs e)
        {
        }

        //Show Main
        public void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        //Show Toolbox
        public void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolbox.Show();
            toolbox.WindowState = FormWindowState.Normal;
        }

        //Close
        public void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AO_TopMost_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.AO_ByName = !AO_TopMost.Checked;
            ps.Default.Save();
        }

        public void AppInvert(Point AppPosition, int Height, int Width)
        {
            mousebox.InvertApp(AppPosition, Height, Width);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.TB_AutoHide = !checkBox5.Checked;
            ps.Default.Save();
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            ps.Default.keepInTray = checkBox3.Checked;
            ps.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.TB_Cursor = checkBox6.Checked;
            ps.Default.Save();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to restore default settings for Air Screen? This cannot be undone.", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                ps.Default.Reset();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.AO_Invert = AO_Invert.Checked;
            ps.Default.Save();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.AO_InvertTime = (int)AO_Time.Value;
            ps.Default.Save();
        }

        private void AO_Start_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.AO_OnStart = AO_Start.Checked;
            ps.Default.Save();
        }

        private void tileScrollDisable_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Scroll = tileScrollDisable.Checked;
            ps.Default.Save();
        }

        private void tileInvert_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Invert = tileInvert.Checked;
            ps.Default.Save();

            if (!tileInvert.Checked)
            {
                if (CheckFilter(2))
                {
                    ps.Default.FilterInUse = false;
                    ps.Default.FilterNum = 0;
                    ps.Default.Save();
                }
                blockfilter.Close();
            }
            else
            {
                if (CheckFilter(2))
                {
                    FilterError();
                    tileInvert.Checked = false;
                    ps.Default.BF_Invert = false;
                    ps.Default.Save();
                }
                else
                {
                    ps.Default.FilterInUse = true;
                    ps.Default.FilterNum = 2;
                    ps.Default.Save();
                }
            }

            if (blockfilter.Visible)
                ReloadTiles();
        }

        private void time_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_InvertTime = (int)time.Value;
            ps.Default.Save();
        }

        private void shift_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 0;
            ps.Default.Save();
        }

        private void r_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 1;
            ps.Default.Save();
        }

        private void f1_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 3;
            ps.Default.Save();
        }

        private void squwiggly_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.tileKey = 2;
            ps.Default.Save();
        }

        private void tileSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            ps.Default.tbWidth = (int)tbWidth.Value;
            ps.Default.tbHeight = (int)tbHeight.Value;
            ps.Default.tbPad = (int)tbRows.Value;
            ps.Default.Save();

            toolbox.Close();
            toolbox = new Toolbox();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ps.Default.doAdjust = !ps.Default.doAdjust;
            ps.Default.Save();
        }

        private void checkBox7_CheckedChanged_1(object sender, EventArgs e)
        {
            ps.Default.doAdjust = checkBox7.Checked;
            ps.Default.Save();

            groupBox10.Enabled = checkBox7.Checked;

            toolbox.Close();
            toolbox = new Toolbox();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://aurascreen.com");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://aurascreen.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://youtube.com");
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.useAltInvert = checkBox8.Checked;
            ps.Default.Save();
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button21_Click_2(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = cursorTab;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = idleTab;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tileTab;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = aoTab;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = filterTab;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = settingsTab;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = hkTab;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleBar.Text = tabControl1.SelectedTab.Text;
        }
    }
}