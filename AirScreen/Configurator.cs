using IniParser;
using IniParser.Model;
using Magnifier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AuraScreen
{
    using ps = AuraScreen.Properties.Settings;

    public partial class Configurator : Form
    {
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
            { "Green", Matrices.Green },
            { "Blue", Matrices.Blue },
            { "Greyscale", Matrices.GrayScale },
            { "Polaroid", Matrices.Polaroid },
            { "Dim Screen", Matrices.DB_Step1 },
            { "Darken Screen", Matrices.DB_Step2 },
            { "My Morning Coffee", Matrices.DB_Step3 },
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
            Process.Start("Error.txt");
        }

        private void CheckForConfigErrors()
        {
            if (ps.Default.CF_Opacity > (decimal)0.99)
                ps.Default.CF_Opacity = (decimal)0.99;

            if (ps.Default.BF_Opacity > (decimal)0.99)
                ps.Default.BF_Opacity = (decimal)0.99;

            if (ps.Default.AO_Opacity > (decimal)0.99)
                ps.Default.AO_Opacity = (decimal)0.99;

            if (ps.Default.CF_DoInvert && ps.Default.BF_Invert)
            {
                ps.Default.CF_DoInvert = false;
                ps.Default.BF_Invert = false;
            }

            if (!File.Exists(ps.Default.BF_Texture))
                ps.Default.BF_Texture = "";
            if (!File.Exists(ps.Default.CF_Texture))
                ps.Default.CF_Texture = "";
            if (!File.Exists(ps.Default.AO_Texture))
                ps.Default.AO_Texture = "";

            if (ps.Default.CF_Width > Screen.PrimaryScreen.Bounds.Width)
                ps.Default.CF_Width = 200;
            if (ps.Default.CF_Height > Screen.PrimaryScreen.Bounds.Height)
                ps.Default.CF_Height = 200;
        }

        private void ReloadTB()
        {
            toolbox.Close();

            toolbox = new Toolbox();
            toolbox.MF = this;
        }

        private FileIniDataParser fileIniData = new FileIniDataParser();
        private string colorINI = "Toolbox_Themes.ini";

        public string ReadColor(string Theme, string color)
        {
            string results;
            fileIniData.Parser.Configuration.CommentString = @"#";
            fileIniData.Parser.Configuration.AllowDuplicateKeys = true;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(colorINI);
            results = data[Theme][color];
            switch (results)
            {
                case "%ACCENT%":
                    return ColorToHex(Color.FromArgb(GetAccentColor().a, GetAccentColor().r, GetAccentColor().g, GetAccentColor().b));
            }

            if (results.Length != 6)
                return "FFFFFF";

            return results;
        }

        //https://stackoverflow.com/questions/50840395/c-sharp-console-get-windows-10-accent-color
        public static (Byte r, Byte g, Byte b, Byte a) GetAccentColor()
        {
            const String DWM_KEY = @"Software\Microsoft\Windows\DWM";
            using (Microsoft.Win32.RegistryKey dwmKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(DWM_KEY, Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree))
            {
                const String KEY_EX_MSG = "The \"HKCU\\" + DWM_KEY + "\" registry key does not exist.";
                if (dwmKey is null) throw new InvalidOperationException(KEY_EX_MSG);

                Object accentColorObj = dwmKey.GetValue("AccentColor");
                if (accentColorObj is Int32 accentColorDword)
                {
                    return ParseDWordColor(accentColorDword);
                }
                else
                {
                    const String VALUE_EX_MSG = "The \"HKCU\\" + DWM_KEY + "\\AccentColor\" registry key value could not be parsed as an ABGR color.";
                    throw new InvalidOperationException(VALUE_EX_MSG);
                }
            }
        }

        private static (Byte r, Byte g, Byte b, Byte a) ParseDWordColor(Int32 color)
        {
            Byte
                a = (byte)((color >> 24) & 0xFF),
                b = (byte)((color >> 16) & 0xFF),
                g = (byte)((color >> 8) & 0xFF),
                r = (byte)((color >> 0) & 0xFF);

            return (r, g, b, a);
        }

        private static String ColorToHex(Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            fileIniData.Parser.Configuration.CommentString = @"#";
            fileIniData.Parser.Configuration.AllowDuplicateKeys = true;
            Directory.CreateDirectory("Textures");
            Directory.CreateDirectory("Cursors");
            Directory.CreateDirectory("ColorMatricies");
            Application.DoEvents();
            CheckForConfigErrors();
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

            if (ps.Default.CF_OnStartup)
                ToggleCF();

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Width += 30;
            foreach (TabPage tab in tabControl1.TabPages)
            {
                //tab.Text = "";
            }

            foreach (var button in panel1.Controls.OfType<Button>())
            {
                int size = (int)(button.Height / 1.5);
                Size newSize = new Size(size, size);
                if (button.Image != null)
                    button.Image = (Image)(new Bitmap(button.Image, newSize));
            }

            ApplyColors();
            button22.BackColor = Selected;

            if (!File.Exists(colorINI))
                WriteDefaultColors();

            //Gets theme names from INI file
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(colorINI);
            comboBox1.Items.Clear();
            foreach (var section in data.Sections)
                comboBox1.Items.Add(section.SectionName);

            if (!comboBox1.Items.Contains("Default"))
                WriteDefaultColors();

            if (comboBox1.Items.Contains(ps.Default.TB_Theme))
                comboBox1.SelectedItem = ps.Default.TB_Theme;
            else
                comboBox1.SelectedItem = "Default";
            //this.Width = this.Width - tabControl1.ItemSize.Width;
        }

        private void WriteDefaultColors()
        {
            Write("Default", "BaseButton", "0A96AA");
            Write("Default", "EnabledButton", "11D114");
            Write("Default", "TextColor", "000000");
            Write("Default", "EnabledTextColor", "FFFFFF");
            Write("Default", "BackgroundColor", "000000");
            Write("Default", "DM_BaseButton", "B84600");
            Write("Default", "DM_EnabledButton", "11D114");
            Write("Default", "DM_TextColor", "FFFFFF");
            Write("Default", "DM_EnabledTextColor", "000000");
            Write("Default", "DM_BackgroundColor", "000000");
        }

        public void Write(string Theme, string Color, string Value)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(colorINI);
            data[Theme][Color] = Value;
            parser.WriteFile(colorINI, data);
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private Color Clicked = Color.FromArgb(230, 237, 183);
        private Color Default = Color.FromArgb(10, 150, 170);

        private Color Selected = Color.FromArgb(6, 84, 96);
        private Color Button = Color.FromArgb(10, 150, 170);
        private Color TextColor = Color.Black;
        private Color AltTextColor = Color.White;
        private Color ClickedColor = Color.FromArgb(119, 119, 119);
        private Color BackgroundColor = Color.White;
        private Color GroupBoxColor = Color.White;
        private Color TextBoxColor = Color.White;
        private Color BorderColor = Color.Black;

        private void ApplyColors()
        {
            if (ps.Default.DarkMode)
            {
                Button = ColorTranslator.FromHtml("#B84600"); //Color.FromArgb(109, 109, 109);
                TextColor = ColorTranslator.FromHtml("#dfe0e2"); //Color.White;
                ClickedColor = ColorTranslator.FromHtml("#F8a145");  //Color.FromArgb(119, 119, 119);
                BackgroundColor = Color.Black;
                GroupBoxColor = ColorTranslator.FromHtml("#151515"); //Color.FromArgb(75, 75, 75);
                TextBoxColor = ColorTranslator.FromHtml("#151515");  //Color.FromArgb(110, 110, 110);
                BorderColor = Color.Black;
                Selected = Color.Black;
            }
            else
            {
                Selected = Color.FromArgb(6, 84, 96);
                Button = Color.FromArgb(10, 150, 170);
                TextColor = Color.Black;
                AltTextColor = Color.White;
                ClickedColor = Color.FromArgb(119, 119, 119);
                BackgroundColor = Color.White;
                GroupBoxColor = Color.White;
                TextBoxColor = Color.White;
                BorderColor = Color.Black;
            }

            foreach (TextBox textbox in GetAll(this, typeof(TextBox)))
            {
                textbox.BackColor = TextBoxColor;
                textbox.ForeColor = TextColor;
            }

            foreach (Button button in GetAll(this, typeof(Button)))
            {
                if (button.BackColor != Color.Green && button.BackColor != Color.DarkRed)
                {
                    button.BackColor = Button;
                    button.ForeColor = AltTextColor;
                    button.FlatAppearance.MouseDownBackColor = ClickedColor;
                    button.FlatAppearance.BorderColor = Color.Black;
                    button.FlatAppearance.BorderSize = 0;
                    button.FlatStyle = FlatStyle.Flat;
                    System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath();
                    GP = GetRoundPath(button.ClientRectangle, ps.Default.Button_Rounding);
                    button.Region = new Region(GP);
                }
            }

            foreach (GroupBox groupbox in GetAll(this, typeof(GroupBox)))
            {
                groupbox.BackColor = GroupBoxColor;
                groupbox.ForeColor = TextColor;
            }

            foreach (Label label in GetAll(this, typeof(Label)))
            {
                label.ForeColor = TextColor;
            }

            foreach (ComboBox combobox in GetAll(this, typeof(ComboBox)))
            {
                combobox.BackColor = BackgroundColor;
                combobox.ForeColor = TextColor;
            }

            foreach (NumericUpDown num in GetAll(this, typeof(NumericUpDown)))
            {
                num.ForeColor = TextColor;
                num.BackColor = BackgroundColor;
            }

            foreach (TabPage tp in GetAll(this, typeof(TabPage)))
            {
                tp.BackColor = BackgroundColor;
            }

            this.BackColor = BackgroundColor;
            panel1.BackColor = BackgroundColor;
        }

        public bool OnlyOnStart = false;

        public void PopulateControls()
        {
            if (!OnlyOnStart)
                ps.Default.FilterInUse = false;
            else
                OnlyOnStart = true;

            #region MainPage

            Console.WriteLine("Cursor Controls");
            width.Text = ps.Default.CF_Width.ToString();
            height.Text = ps.Default.CF_Height.ToString();
            styleBox.Text = ps.Default.CF_Style;
            checkBox2.Checked = ps.Default.CF_DoBorder;
            borderThicccccc.Value = ps.Default.CF_BorderSize;
            toTray.Checked = ps.Default.keepInTray;

            textureCombo.Items.Clear();
            textureCombo.Items.AddRange(GetFilesFrom(Application.StartupPath + "\\Textures", new String[] { "png", "jpg", "jpeg" }));

            if (textureCombo.Items.Count == 0)
            {
                textureBox.Enabled = false;
                textureCombo.Enabled = false;
            }

            Tile_Texture.Items.Clear();
            Tile_Texture.Items.AddRange(GetFilesFrom(Application.StartupPath + "\\Textures", new String[] { "png", "jpg", "jpeg" }));

            if (Tile_Texture.Items.Count == 0)
            {
                Tile_TextureBox.Enabled = false;
                Tile_Texture.Enabled = false;
            }

            AO_Texture.Items.Clear();
            AO_Texture.Items.AddRange(GetFilesFrom(Application.StartupPath + "\\Textures", new String[] { "png", "jpg", "jpeg" }));

            if (AO_Texture.Items.Count == 0)
            {
                AO_TextureBox.Enabled = false;
                AO_Texture.Enabled = false;
            }

            textureBox.Checked = ps.Default.CF_DoTexture;
            opacityBar.Value = ps.Default.CF_Opacity;
            flipBox.Checked = ps.Default.CF_Flip;
            inversionBox.Checked = ps.Default.CF_DoInvert;
            checkBox4.Checked = ps.Default.CF_Lock;
            Console.WriteLine("Tile Controls");
            tileInvert.Checked = ps.Default.BF_Invert;

            tileHeight.Value = ps.Default.BF_Height;
            tileWidth.Value = ps.Default.BF_Width;
            tileX.Value = ps.Default.BF_X;
            tileY.Value = ps.Default.BF_Y;

            darkmode.Checked = ps.Default.DarkMode;

            #endregion MainPage

            #region hotKeys

            Console.WriteLine("Hotkeys");
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

            TB_To_Cursor.Checked = ps.Default.TB_Cursor;

            tilesManualHK.Text = ps.Default.HK_EditBF;

            #endregion hotKeys

            #region AppOverlay

            AO_ByName.Checked = ps.Default.AO_ByName;
            AO_TopMost.Checked = !ps.Default.AO_ByName;
            AO_TextBox.Text = ps.Default.AO_SavedName;
            AO_Opacity.Value = ps.Default.AO_Opacity;
            AO_TextureBox.Checked = ps.Default.AO_DoTexture;
            ao_AS.Checked = ps.Default.AO_DontAttatchToAS;

            #endregion AppOverlay

            #region Screen Filters

            Filter_Programs.DataSource = ps.Default.SF_Programs.Split(';');
            Filter_OnActive.Checked = ps.Default.SF_OnActive;
            matrixBox.Text = ps.Default.SF_LastUsed;
            groupBox1.Enabled = ps.Default.SF_OnActive;

            customMatrixBox.Checked = ps.Default.SF_DoCustom;
            customMatrix.Enabled = ps.Default.SF_DoCustom;

            customMatrix.Items.Clear();
            customMatrix.Items.AddRange(GetFilesFrom(Application.StartupPath + "\\ColorMatricies", new String[] { "ini" }));

            if (customMatrix.Items.Count == 0)
                customMatrix.Enabled = false;
            else
                customMatrix.SelectedIndex = 0;

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

            overrideTB.Checked = ps.Default.doAdjust;
            tbWidth.Value = ps.Default.tbWidth;
            tbHeight.Value = ps.Default.tbHeight;
            tbRows.Value = ps.Default.tbPad;
            groupBox10.Enabled = ps.Default.doAdjust;

            toTray.Checked = ps.Default.keepInTray;
            TB_AutoHide.Checked = !ps.Default.TB_AutoHide;

            altInverse.Checked = ps.Default.useAltInvert;

            if (ps.Default.BF_Location != 0)
                tileSelect.SelectedIndex = ps.Default.BF_Location - 1;

            #endregion Other
        }

        public static String[] GetFilesFrom(String searchFolder, String[] filters)
        {
            List<String> filesFound = new List<String>();
            if (!Directory.Exists(searchFolder))
                return filesFound.ToArray();

            foreach (var filter in filters)
                foreach (string file in Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), SearchOption.TopDirectoryOnly))
                    if (!filesFound.Contains(Path.GetFileName(file)))
                        filesFound.Add(Path.GetFileName(file));

            return filesFound.ToArray();
        }

        //https://stackoverflow.com/questions/5977445/how-to-get-windows-display-settings/14283331
        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            base.OnPaint(e);
        }

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }

        private float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
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
            GlobalHotKey.RegisterHotKey("Control + Shift + " + ps.Default.HK_InvertCF, () =>
            {
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

            tileSelect.SelectedIndex = Mode - 1;
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
            if (mousebox.IsDisposed || ps.Default.CF_DoInvert && !mousebox.Visible || mousebox == null)
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
            ps.Default.Save();
            width.Text = ps.Default.CF_Width.ToString();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Width = ps.Default.CF_Width - 25;
            if (ps.Default.CF_Width < 30)
                ps.Default.CF_Width = 30;
            ps.Default.Save();
            width.Text = ps.Default.CF_Width.ToString();
        }

        //Height
        public void button6_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Height = ps.Default.CF_Height + 25;
            if (ps.Default.CF_Height > 10001)
                ps.Default.CF_Height = 10000;
            ps.Default.Save();
            height.Text = ps.Default.CF_Height.ToString();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            ps.Default.CF_Height = ps.Default.CF_Height - 25;
            if (ps.Default.CF_Height < 30)
                ps.Default.CF_Height = 30;
            ps.Default.Save();
            height.Text = ps.Default.CF_Height.ToString();
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
                if (!mousebox.IsDisposed)
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
            ps.Default.keepInTray = toTray.Checked;
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
            notifyIcon1.Visible = false;
            notifyIcon1.Dispose();

            try
            {
                if (CursorHasChanged)
                    SystemParametersInfo(0x0057, 0, null, 0);
            }
            catch { Console.WriteLine("Cursor failed to revert!"); }
            //Fuck error messages
            try
            {
                mousebox.Dispose();
            }
            catch { }
            try
            {
                blockfilter.Dispose();
            }
            catch { }
            try
            {
                appO.Dispose();
            }
            catch { }

            NativeMethods.MagUninitialize();
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
            //ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
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
            ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
            ps.Default.Save();
            ReloadTiles();
        }

        public void ToggleBlockFilter()
        {
            //ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
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
            ps.Default.BF_Location = tileSelect.SelectedIndex + 1;
            ps.Default.Save();
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

        public void StartSFActive()
        {
            if (!SF_FilterInUse && !Filter_Timer.Enabled)
            {
                //ps.Default.FilterInUse = true;
                //ps.Default.FilterNum = 3;
                SF_FilterInUse = true;
                Filter_Timer.Enabled = true;
            }
            else if (Filter_Timer.Enabled)
            {
                SF_FilterInUse = false;
                //ps.Default.FilterInUse = false;
                //ps.Default.FilterNum = 0;
                Matrices.ChangeColorEffect(Matrices.Identity);
                Filter_Timer.Enabled = false;
            }
        }

        public void ToggleFilter()
        {
            if (customMatrixBox.Checked)
            {
                if (!String.IsNullOrWhiteSpace(customMatrix.Text) && File.Exists(Application.StartupPath + "\\ColorMatricies\\" + customMatrix.Text))
                {
                    if (SF_FilterInUse)
                        SF_FilterInUse = !Matrices.ChangeColorEffect(Matrices.Identity);
                    else
                        SF_FilterInUse = Matrices.ChangeColorEffect(Matrices.StringToMatrix(File.ReadAllText(Application.StartupPath + "\\ColorMatricies\\" + customMatrix.Text)));
                }
                else if (!File.Exists(Application.StartupPath + "\\ColorMatricies\\" + customMatrix.Text))
                {
                    MessageBox.Show($"The matrix file {customMatrix.Text}\ndoes not exist. Please make sure it wasn't deleted or renamed.");
                }
            }
            else
            {
                ps.Default.SF_LastUsed = matrixBox.Text;
                ps.Default.Save();

                if (ps.Default.SF_OnActive)
                    Filter_Timer.Enabled = !Filter_Timer.Enabled;

                if (matrixBox.Text == "None" || SF_FilterInUse)
                {
                    Console.WriteLine($"MatrixBox {matrixBox.Text} :: FilterInUse {SF_FilterInUse}");
                    SF_FilterInUse = false;
                    //ps.Default.FilterInUse = false;
                    //ps.Default.FilterNum = 0;
                    Matrices.ChangeColorEffect(Matrices.Identity);
                }
                else
                    SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
            }

            /*
            else if(!CheckFilter(3))
            {
                Console.WriteLine("Attempting to apply filter");
                ps.Default.FilterInUse = true;
                ps.Default.FilterNum = 3;
                SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
            }
            else
            {
                FilterError();
            }
            */

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

            if (customMatrixBox.Checked)
                pictureBox2.Image = Matrices.Transform((Bitmap)pictureBox1.Image, Matrices.StringToMatrix(File.ReadAllText(Application.StartupPath + "\\ColorMatricies\\" + customMatrix.Text)));
            else
                pictureBox2.Image = Matrices.Transform((Bitmap)pictureBox1.Image, Matrix[matrixBox.Text]);

            label20.Text = matrixBox.Text;
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
                ReloadAO();
        }

        public void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.SF_OnActive = Filter_OnActive.Checked;
            groupBox1.Enabled = ps.Default.SF_OnActive;
            StartSFActive();
            ps.Default.Save();
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
                    Console.WriteLine($"{AppName} is active and is selected");
                    if (!SF_FilterInUse)
                        SF_FilterInUse = Matrices.ChangeColorEffect(Matrix[ps.Default.SF_LastUsed]);
                }
                else
                {
                    Console.WriteLine($"{AppName} is Active and NOT selected");
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
                //Cursor result = new Cursor(fs);
                //cursorPreview.Cursor = result;

                Cursor mycursor = new Cursor(Cursor.Current.Handle);
                IntPtr colorcursorhandle = LoadCursorFromFile(filePath);
                mycursor.GetType().InvokeMember("handle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, mycursor, new object[] { colorcursorhandle });
                cursorPreview.Cursor = mycursor;
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
            inversionBox.CheckedChanged -= inversionBox_CheckedChanged;
            if (!inversionBox.Checked)
            {
                if (ps.Default.FilterNum == 1)
                    ps.Default.FilterNum = 0;
            }
            else
            {
                if (ps.Default.FilterNum == 2)
                {
                    inversionBox.Checked = false;
                    MessageBox.Show($"Sorry! But mixing the Cursor Filter Inversion and the Tile Filter Inversion causes insane slow down and freezing even on high end systems! Please disable one of them before continuing.");
                    this.Close();
                }
                else
                    ps.Default.FilterNum = 1;
            }
            ps.Default.CF_DoInvert = inversionBox.Checked;
            ps.Default.Save();
            inversionBox.CheckedChanged += inversionBox_CheckedChanged;
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
            ps.Default.TB_AutoHide = !TB_AutoHide.Checked;
            ps.Default.Save();
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            ps.Default.keepInTray = toTray.Checked;
            ps.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.TB_Cursor = TB_To_Cursor.Checked;
            ps.Default.Save();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to restore default settings for Air Screen? This cannot be undone.", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                ps.Default.Reset();
        }

        private void tileInvert_CheckedChanged(object sender, EventArgs e)
        {
            tileInvert.CheckedChanged -= tileInvert_CheckedChanged;
            if (!tileInvert.Checked)
            {
                if (ps.Default.FilterNum == 2)
                    ps.Default.FilterNum = 0;
                if (blockfilter.Visible)
                    ReloadTiles();
            }
            else
            {
                if (ps.Default.FilterNum == 1)
                {
                    tileInvert.Checked = false;
                    MessageBox.Show($"Sorry! But mixing the Cursor Filter Inversion and the Tile Filter Inversion causes insane slow down and freezing even on high end systems! Please disable one of them before continuing.");
                    this.Close();
                }
                else
                    ps.Default.FilterNum = 2;
            }

            ps.Default.BF_Invert = tileInvert.Checked;
            ps.Default.Save();
            tileInvert.CheckedChanged += tileInvert_CheckedChanged;
            if (blockfilter.Visible)
                ReloadTiles();
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
            ps.Default.doAdjust = overrideTB.Checked;
            ps.Default.Save();

            groupBox10.Enabled = overrideTB.Checked;

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
            ps.Default.useAltInvert = altInverse.Checked;
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
            ApplyColors();
            button22.BackColor = Selected;
            button22.ForeColor = AltTextColor;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = idleTab;
            ApplyColors();
            button23.BackColor = Selected;
            button23.ForeColor = AltTextColor;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tileTab;
            ApplyColors();
            button24.BackColor = Selected;
            button24.ForeColor = AltTextColor;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = aoTab;
            ApplyColors();
            button25.BackColor = Selected;
            button25.ForeColor = AltTextColor;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = filterTab;
            ApplyColors();
            button26.BackColor = Selected;
            button26.ForeColor = AltTextColor;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = settingsTab;
            ApplyColors();
            button27.BackColor = Selected;
            button27.ForeColor = AltTextColor;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = hkTab;
            ApplyColors();
            button28.BackColor = Selected;
            button28.ForeColor = AltTextColor;
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            titleBar.Text = tabControl1.SelectedTab.Text;
        }

        private void tileEditButton_Click(object sender, EventArgs e)
        {
            tileSelect.SelectedIndex = 4;
            CycleTiles(5);
            EditTiles();
        }

        private void tileHeight_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Height = (int)tileHeight.Value;
            ps.Default.Save();
        }

        private void tileWidth_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Width = (int)tileWidth.Value;
            ps.Default.Save();
        }

        private void tileX_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_X = (int)tileX.Value;
            ps.Default.Save();
        }

        private void tileY_ValueChanged(object sender, EventArgs e)
        {
            ps.Default.BF_Y = (int)tileY.Value;
            ps.Default.Save();
        }

        private void ao_AS_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.DarkMode = darkmode.Checked;
            ps.Default.Save();

            //this.Hide();
            ApplyColors();
            //this.Show();
            ReloadTB();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {
        }

        private void button29_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would you like to upgrade/import the settings from a previous version of Aura Screen? This will overwrite your current settings and CANNOT BE UNDONE. Proceed?", "Upgrade Old Settings", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ps.Default.Upgrade();
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
        }

        private void AO_ByName_CheckedChanged(object sender, EventArgs e)
        {
            AO_TextBox.Enabled = AO_ByName.Checked;
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void label30_Click(object sender, EventArgs e)
        {
        }

        private void flipBox_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_Flip = flipBox.Checked;
            ps.Default.Save();
        }

        private void textureBox_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.CF_DoTexture = textureBox.Checked;
            ps.Default.Save();
        }

        private void textureCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textureCombo.SelectedItem.ToString()))
            {
                ps.Default.CF_Texture = textureCombo.SelectedItem.ToString();
                ps.Default.Save();
            }
        }

        private void Tile_TextureBox_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.BF_DoTexture = Tile_TextureBox.Checked;
            ps.Default.Save();
        }

        private void Tile_Texture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Tile_Texture.SelectedItem.ToString()))
            {
                ps.Default.BF_Texture = Tile_Texture.SelectedItem.ToString();
                ps.Default.Save();
            }
        }

        private void AO_Texture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(AO_Texture.SelectedItem.ToString()))
            {
                ps.Default.AO_Texture = AO_Texture.SelectedItem.ToString();
                ps.Default.Save();
            }
        }

        private void AO_TextureBox_CheckedChanged(object sender, EventArgs e)
        {
            ps.Default.AO_DoTexture = AO_TextureBox.Checked;
            ps.Default.Save();
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            float r2 = radius / 2f;
            System.Drawing.Drawing2D.GraphicsPath GraphPath = new System.Drawing.Drawing2D.GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
            GraphPath.AddArc(Rect.X + Rect.Width - radius,
                             Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            File.Copy(config.FilePath, $"Settings_{DateTime.Now.ToString("dd/MM/yyyy")}.config", true);
            if (File.Exists($"Settings_{DateTime.Now.ToString("dd/MM/yyyy")}.config"))
                MessageBox.Show("Settings backed up to " + $"Settings_{DateTime.Today}.config");
            else
                MessageBox.Show("Failed to backup settings!");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);
            DialogResult dialogResult = MessageBox.Show("This will overwrite all current settings and cannot be undone. Continue?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var fileContent = string.Empty;
                var filePath = string.Empty;

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Application.StartupPath;
                    openFileDialog.Filter = "config files (*.config)|*.config";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filePath = openFileDialog.FileName;
                        File.Delete(config.FilePath);
                        File.Copy(filePath, config.FilePath);
                    }
                }

                MessageBox.Show(fileContent, "Settings have been restored!");
            }
        }

        private void customMatrixBox_CheckedChanged(object sender, EventArgs e)
        {
            customMatrix.Enabled = customMatrixBox.Checked;
            matrixBox.Enabled = !customMatrixBox.Checked;
            ps.Default.SF_DoCustom = customMatrixBox.Checked;
            ps.Default.Save();
        }

        private void customMatrix_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private MatrixCreator mc = new MatrixCreator();

        private void button33_Click(object sender, EventArgs e)
        {
            if (mc == null || mc.IsDisposed)
                mc = new MatrixCreator();

            if (!mc.Visible)
            {
                mc.Matrix = Matrix;
                mc.VisibleChanged += mcClosed;
                mc.FormClosed += mcClosed;
                mc.Show();
            }
        }

        private void mcClosed(object sender, EventArgs e)
        {
            PopulateControls();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://zerowidthjoiner.net/");
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ps.Default.TB_Theme = comboBox1.Text;
            ps.Default.Save();

            ReloadTB();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}