using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void PopulateControls()
        {
            if (ps.Default.onStartup)
                Toggle();

            width.Text = ps.Default.width.ToString();
            height.Text = ps.Default.height.ToString();

            styleBox.Text = ps.Default.style;

            checkBox1.Checked = ps.Default.onStartup;

            checkBox2.Checked = ps.Default.border;
            borderThicccccc.Value = ps.Default.borderThicc;

            checkBox3.Checked = ps.Default.keepInTray;

            enableHotKey.Text = ps.Default.enableHK;
            disableHotKey.Text = ps.Default.disableHK;
            invertHotKey.Text = ps.Default.invertHK;
            enlargeHotKey.Text = ps.Default.enlargeHK;
            shrinkHotKey.Text = ps.Default.shirnkHK;
            cylceHotKey.Text = ps.Default.cycleHK;
            if (ps.Default.opacity > 1) ps.Default.opacity = 1;
            opacityBar.Value = ps.Default.opacity;
        }

        private void SaveHotkeys()
        {
            ps.Default.enableHK = enableHotKey.Text;
            ps.Default.disableHK = disableHotKey.Text;
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
            try{ tile.Close(); }
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
    }
}
