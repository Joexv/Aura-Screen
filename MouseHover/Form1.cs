using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHover
{
    using ps = Properties.Settings;
    public partial class Form1 : Form
    {
        Form2 frm2 = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Enable
        private void button1_Click(object sender, EventArgs e)
        {
            frm2.Show();
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
            frm2.Close();
            frm2 = new Form2();
            frm2.Show();
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

            notifyIcon1.Visible = checkBox3.Checked;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ps.Default.invert = false;
            ps.Default.Save();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (ps.Default.onStartup)
                frm2.Show();

            width.Text = ps.Default.width.ToString();
            height.Text = ps.Default.height.ToString();

            styleBox.Text = ps.Default.style;

            checkBox1.Checked = ps.Default.onStartup;

            checkBox2.Checked = ps.Default.border;
            borderThicccccc.Value = ps.Default.borderThicc;
        }
    }
}
