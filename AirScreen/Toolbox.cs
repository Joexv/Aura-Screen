using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirScreen
{
    using ps = Properties.Settings;
    public partial class Toolbox : Form
    {
        public MainForm MF { get; set; }
        public Tiles tiles { get; set; }
        public Toolbox()
        {
            InitializeComponent();
        }

        private bool InversionToggle;
        void Form_LostFocus(object sender, EventArgs e)
        {
            this.MouseLeave -= new System.EventHandler(Form_LostFocus);
            this.Hide();
            if (wasInverted)
            {
                MF.EnableInvert();
                wasInverted = false;
            }
        }

        private void Toolbox_Leave(object sender, EventArgs e)
        {

        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void Toolbox_Load(object sender, EventArgs e)
        {
            if (ps.Default.invert && ps.Default.HideToolBox)
            {
                wasInverted = true;
                MF.DisableInvert();
                foreach (var button in this.flowLayoutPanel1.Controls.OfType<Button>())
                {
                    button.BackColor = Color.FromArgb(245, 105, 85);
                    button.ForeColor = Color.Black;
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(25, 18, 72);
                }
            }
            else
            {
                foreach (var button in this.flowLayoutPanel1.Controls.OfType<Button>())
                {
                    button.BackColor = Color.FromArgb(10, 150, 170);
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 237, 183);
                    button.FlatAppearance.BorderColor = Color.FromArgb(41, 53, 65);
                }
            }

            //Adjust button locations
            FlowLayoutPanel p = flowLayoutPanel1;
            p.Controls.SetChildIndex(button3, 7);
            int ControlHeight = flowLayoutPanel1.Controls.Count / 4 + 1;
            this.Width = button1.Width * 4 + button1.Padding.Left * 6;
            this.Height = button1.Height * ControlHeight + button1.Padding.Top * (ControlHeight * 2);
        }

        private void cursorToggle_Click(object sender, EventArgs e)
        {
            MF.Toggle();
        }

        private void cursorInvert_Click(object sender, EventArgs e)
        {
            MF.Invert();
        }

        private void lockCursor_Click(object sender, EventArgs e)
        {
            ps.Default.cursorLock = !ps.Default.cursorLock; 
            ps.Default.Save();
        }

        private void borderToggle_Click(object sender, EventArgs e)
        {
            MF.checkBox2.Checked = true;
        }

        private void shrinkCursor_Click(object sender, EventArgs e)
        {
            ps.Default.width -= (int)ps.Default.ESSize;
            ps.Default.height -= (int)ps.Default.ESSize;
            ps.Default.Save();
        }

        private void enlargeCursor_Click(object sender, EventArgs e)
        {
            ps.Default.width += (int)ps.Default.ESSize;
            ps.Default.height += (int)ps.Default.ESSize;
            ps.Default.Save();
        }

        private void lowerOpacity_Click(object sender, EventArgs e)
        {
            if(ps.Default.opacity > (decimal)0.10)
                ps.Default.opacity -= (decimal)0.10;
            if (ps.Default.opacity < (decimal)0.10)
                ps.Default.opacity = (decimal)0.10;
            ps.Default.Save();
        }

        private void raiseOpacity_Click(object sender, EventArgs e)
        {
            if (ps.Default.opacity < (decimal)1)
                ps.Default.opacity += (decimal)0.10;
            if (ps.Default.opacity >= 1)
                ps.Default.opacity = (decimal)0.90;
            ps.Default.Save();
        }

        private void AO_Toggle_Click(object sender, EventArgs e)
        {
            MF.ToggleAppOverlay();
        }

        private void AO_Active_Click(object sender, EventArgs e)
        {
            ps.Default.AO_ProcessByName = !ps.Default.AO_ProcessByName;
            ps.Default.Save();
        }

        private void SF_Toggle_Click(object sender, EventArgs e)
        {
            MF.ToggleFilter();
        }

        private void SF_Cycle_Click(object sender, EventArgs e)
        {
            MF.CycleFilter();
        }

        private void SF_Program_Click(object sender, EventArgs e)
        {
            MF.Filter_OnActive.Checked = !MF.Filter_OnActive.Checked;
        }

        private void BF_Toggle_Click(object sender, EventArgs e)
        {
            MF.ToggleBlockFilter();
        }

        private void BF_Cycle_Click(object sender, EventArgs e)
        {
            MF.CycleTiles();
        }

        private void BF_Top_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(1);
        }

        private void BF_Bottom_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(2);
        }

        private void BF_Left_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(3);
        }

        private void BF_Right_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(4);
        }

        private void Toolbox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                if (wasInverted)
                {
                    wasInverted = false;
                    MF.EnableInvert();
                }
            }
        }

        private void Toolbox_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private bool wasInverted = false;
        private bool LockCursor;
        private bool DoneOnce = false;
        private void flowLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            /*
            if (ps.Default.invert)
            {
                wasInverted = true;
                Console.WriteLine("Inverting Toolbox");
                //Backup Settings
                InversionToggle = ps.Default.InversionToggle;
                LockCursor = ps.Default.cursorLock;

                //Adjust Settings to fit use case
                ps.Default.cursorLock = true;
                ps.Default.InversionToggle = false;
                ps.Default.AppInvertLock = true;
                ps.Default.Save();
                
                //Inverting
                MF.AppInvert(this.Location, this.Height, this.Width);
                DoneOnce = true;
            }
            */
            //For some reason this didn't work, 
            //it would just show a grey box over half of the Toolbox screen
            if (ps.Default.HideToolBox)
                this.MouseLeave += new EventHandler(Form_LostFocus);
        }

        private void BF_Manual_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(5);
        }

        private void Toolbox_Shown(object sender, EventArgs e)
        {
            Rectangle workingArea = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            int left = workingArea.Width - this.Width;
            int top = workingArea.Height - this.Height - 15;
            if (ps.Default.toolboxCursor)
            {
                this.Location = Cursor.Position;
                if(this.Location.X > left || this.Location.Y > top)
                    this.Location = new Point(left, top);
            }
            else
                this.Location = new Point(left, top);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MF.EditTiles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MF.styleBox.SelectedIndex < 2)
                MF.styleBox.SelectedIndex += 1;
            else
                MF.styleBox.SelectedIndex = 0;

            MF.ReloadCursorOverlay();
        }
    }
}
