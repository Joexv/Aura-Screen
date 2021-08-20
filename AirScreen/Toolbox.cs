using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuraScreen
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
                foreach (var button in this.Controls.OfType<Button>())
                {
                    if (button.BackColor == default)
                        button.BackColor = Color.FromArgb(245, 105, 85);
                    else
                        button.BackColor = Color.FromArgb(201, 0, 222);
                    button.ForeColor = Color.Black;
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(25, 18, 72);
                }
            }
            else
            {
                foreach (var button in this.Controls.OfType<Button>())
                {
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 237, 183);
                    button.FlatAppearance.BorderColor = Color.FromArgb(41, 53, 65);
                }
                ButtonPopulation();
            }

            //Adjust button locations
            //FlowLayoutPanel p = flowLayoutPanel1;
            //p.Controls.SetChildIndex(button3, 7);
        }

        private void cursorToggle_Click(object sender, EventArgs e)
        {
            MF.Toggle();
            ButtonPopulation();
        }

        private void cursorInvert_Click(object sender, EventArgs e)
        {
            MF.Invert();
            ButtonPopulation();
        }

        private void lockCursor_Click(object sender, EventArgs e)
        {
            ps.Default.cursorLock = !ps.Default.cursorLock; 
            ps.Default.Save();
            ButtonPopulation();
        }

        private void borderToggle_Click(object sender, EventArgs e)
        {
            MF.checkBox2.Checked = true;
            ButtonPopulation();
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
            ButtonPopulation();
        }

        private void AO_Active_Click(object sender, EventArgs e)
        {
            ps.Default.AO_ProcessByName = !ps.Default.AO_ProcessByName;
            ps.Default.Save();
            MF.AO_ByName.Checked = ps.Default.AO_ProcessByName;
            MF.AO_TopMost.Checked = !ps.Default.AO_ProcessByName;
            ButtonPopulation();
        }

        private void SF_Toggle_Click(object sender, EventArgs e)
        {
            MF.ToggleFilter();
            ButtonPopulation();
        }

        private void SF_Cycle_Click(object sender, EventArgs e)
        {
            MF.CycleFilter();
        }

        private void SF_Program_Click(object sender, EventArgs e)
        {
            MF.Filter_OnActive.Checked = !MF.Filter_OnActive.Checked;
            ButtonPopulation();
        }

        private void BF_Toggle_Click(object sender, EventArgs e)
        {
            MF.ToggleBlockFilter();
            ButtonPopulation();
        }

        private void BF_Cycle_Click(object sender, EventArgs e)
        {
            MF.CycleTiles();
            ButtonPopulation();
        }

        private void BF_Top_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(1);
            ButtonPopulation();
        }

        private void BF_Bottom_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(2);
            ButtonPopulation();
        }

        private void BF_Left_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(3);
            ButtonPopulation();
        }

        private void BF_Right_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(4);
            ButtonPopulation();
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
            if (ps.Default.HideToolBox)
                this.MouseLeave += new EventHandler(Form_LostFocus);
        }

        private void BF_Manual_Click(object sender, EventArgs e)
        {
            MF.CycleTiles(5);
        }

        private void Toolbox_Shown(object sender, EventArgs e)
        {
            if (ps.Default.doAdjust)
            {
                foreach (var button in flowLayoutPanel1.Controls.OfType<Button>())
                {
                    double Percentage;

                    Percentage = Convert.ToDouble(ps.Default.tbWidth) / Convert.ToDouble(button.Width);
                    Console.WriteLine($"{Percentage * 100}% {ps.Default.tbWidth}::{button.Width}");

                    button.Width = ps.Default.tbWidth;
                    button.Height = ps.Default.tbHeight;

                    Size newSize = new Size((int)(button.Image.Width * Percentage), (int)(button.Image.Height * Percentage));

                    if (button.Image != null)
                        button.Image = (Image)(new Bitmap(button.Image, newSize));
                    float FontSize = (float)(button.Font.Size * Percentage);
                    button.Font = new Font(button.Font.FontFamily, FontSize);
                }

                int ControlHeight = flowLayoutPanel1.Controls.OfType<Button>().Count() / ps.Default.tbPad + 1;
                this.Width = BF_Top.Width * ps.Default.tbPad + (BF_Top.Margin.All * (ps.Default.tbPad + 20));
                Console.WriteLine($"{BF_Top.Height} * {ControlHeight} + ({BF_Top.Margin.All} * ({(ControlHeight - 1)} * 9))");
                this.Height = BF_Top.Height * ControlHeight + (BF_Top.Margin.All * (ControlHeight + 60));
            }

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

            ButtonPopulation();
        }

        Color Clicked = Color.FromArgb(17, 209, 20);
        Color Default = Color.FromArgb(10, 150, 170);
        private void ButtonPopulation()
        {
            if (MF.frm2 != null &&  MF.frm2.Visible)
                cursorToggle.BackColor = Clicked;
            else
                cursorToggle.BackColor = Default;

            if(ps.Default.invert)
                cursorInvert.BackColor = Clicked;
            else
                cursorInvert.BackColor = Default;

            if(MF.appO != null && MF.appO.Visible)
                AO_Toggle.BackColor = Clicked;
            else
                AO_Toggle.BackColor = Default;

            if(ps.Default.AO_ProcessByName)
                AO_Active.BackColor = Clicked;
            else
                AO_Active.BackColor = Default;

            if(ps.Default.Filter_OnActive)
                SF_Program.BackColor = Clicked;
            else
                SF_Program.BackColor = Default;

            if(MF.tile != null && MF.tile.Visible)
                BF_Toggle.BackColor = Clicked;
            else
                BF_Toggle.BackColor = Default;

            switch (ps.Default.tileMode)
            {
                default:
                    break;

                case 1: //Top
                    BF_Top.BackColor = Clicked;
                    BF_Bottom.BackColor = Default;
                    BF_Left.BackColor = Default;
                    BF_Right.BackColor = Default;
                    BF_Manual.BackColor = Default;
                    break;

                case 2: //Bottom
                    BF_Top.BackColor = Default;
                    BF_Bottom.BackColor = Clicked;
                    BF_Left.BackColor = Default;
                    BF_Right.BackColor = Default;
                    BF_Manual.BackColor = Default;
                    break;

                case 3: //Left
                    BF_Top.BackColor = Default;
                    BF_Bottom.BackColor = Default;
                    BF_Left.BackColor = Clicked;
                    BF_Right.BackColor = Default;
                    BF_Manual.BackColor = Default;
                    break;

                case 4: //Right
                    BF_Top.BackColor = Default;
                    BF_Bottom.BackColor = Default;
                    BF_Left.BackColor = Default;
                    BF_Right.BackColor = Clicked;
                    BF_Manual.BackColor = Default;
                    break;

                case 5: //Manual
                    BF_Top.BackColor = Default;
                    BF_Bottom.BackColor = Default;
                    BF_Left.BackColor = Default;
                    BF_Right.BackColor = Default;
                    BF_Manual.BackColor = Clicked;
                    break;
            }

            foreach (var button in flowLayoutPanel1.Controls.OfType<Button>())
            {
                if (button.BackColor == Clicked)
                    button.ForeColor = Color.Black;
                else
                    button.ForeColor = Color.White;
            }
        }

        private void BF_Edit_Click(object sender, EventArgs e)
        {
            MF.EditTiles();
            ButtonPopulation();
        }

        private void Cursor_Cycle_Click(object sender, EventArgs e)
        {
            if (MF.styleBox.SelectedIndex < 2)
                MF.styleBox.SelectedIndex += 1;
            else
                MF.styleBox.SelectedIndex = 0;

            MF.ReloadCursorOverlay();
        }
    }
}
