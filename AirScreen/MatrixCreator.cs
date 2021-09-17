using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Magnifier;

namespace AuraScreen
{
    using ps = AuraScreen.Properties.Settings;
    public partial class MatrixCreator : Form
    {
        public Dictionary<string, float[,]> Matrix { get; set; }
        public MatrixCreator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ApplyColors();
            AddMethod();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(Matrix.Keys.ToArray());
        }

        private void AddMethod()
        {
            foreach (NumericUpDown num in GetAll(this, typeof(NumericUpDown)))
            {
                num.ValueChanged += ReloadExample;
            }
        }

        private void RemoveMethod()
        {
            foreach (NumericUpDown num in GetAll(this, typeof(NumericUpDown)))
            {
                num.ValueChanged -= ReloadExample;
            }
        }

        private void ReloadExample(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                float[,] matrix = numericFloat();
                adjPicture.Image = Matrices.Transform((Bitmap)basePicture.Image, matrix);
            }
        }

        private float[,] numericFloat()
        {
            return new float[,] { 
                { (float)rr.Value, (float)rg.Value, (float)rb.Value, (float)ra.Value, (float)rw.Value,},
                { (float)gr.Value, (float)gg.Value, (float)gb.Value, (float)ga.Value, (float)gw.Value,},
                { (float)br.Value, (float)bg.Value, (float)bb.Value, (float)ba.Value, (float)bw.Value,},
                { (float)ar.Value, (float)ag.Value, (float)ab.Value, (float)aa.Value, (float)aw.Value,},
                { (float)wr.Value, (float)wg.Value, (float)wb.Value, (float)wa.Value, (float)ww.Value,},
            };
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Application.StartupPath + "\\ColorMatricies";      
            saveFileDialog1.DefaultExt = "ini";
            saveFileDialog1.Filter = "Matrix files (*.ini)|*.ini";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog1.FileName, Matrices.MatrixToString(numericFloat()));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (NumericUpDown num in GetAll(this, typeof(NumericUpDown)))
            {
                num.ValueChanged -= ReloadExample;
                num.Value = 0;
                num.ValueChanged += ReloadExample;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.Filter = "Matrix files (*.ini)|*.ini";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RemoveMethod();
                    LoadValues(Matrices.StringToMatrix(File.ReadAllText(openFileDialog.FileName)));
                    AddMethod();
                    ReloadExample(sender, e);
                }
            }

        }

        private void LoadValues(float[,] matrix)
        {
            rr.Value = (decimal)matrix[0, 0];
            rg.Value = (decimal)matrix[0, 1];
            rb.Value = (decimal)matrix[0, 2];
            ra.Value = (decimal)matrix[0, 3];
            rw.Value = (decimal)matrix[0, 4];

            gr.Value = (decimal)matrix[1, 0];
            gg.Value = (decimal)matrix[1, 1];
            gb.Value = (decimal)matrix[1, 2];
            ga.Value = (decimal)matrix[1, 3];
            gw.Value = (decimal)matrix[1, 4];

            br.Value = (decimal)matrix[2, 0];
            bg.Value = (decimal)matrix[2, 1];
            bb.Value = (decimal)matrix[2, 2];
            ba.Value = (decimal)matrix[2, 3];
            bw.Value = (decimal)matrix[2, 4];

            ar.Value = (decimal)matrix[3, 0];
            ag.Value = (decimal)matrix[3, 1];
            ab.Value = (decimal)matrix[3, 2];
            aa.Value = (decimal)matrix[3, 3];
            aw.Value = (decimal)matrix[3, 4];

            wr.Value = (decimal)matrix[4, 0];
            wg.Value = (decimal)matrix[4, 1];
            wb.Value = (decimal)matrix[4, 2];
            wa.Value = (decimal)matrix[4, 3];
            ww.Value = (decimal)matrix[4, 4];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Matrices.MatrixToString(Matrices.Negative));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button5.Visible = !checkBox1.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float[,] matrix = numericFloat();
            adjPicture.Image = Matrices.Transform((Bitmap)basePicture.Image, matrix);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath + "\\ColorMatricies\\Example Images\\";
                openFileDialog.Filter = "Image files (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    basePicture.Image = Image.FromFile(openFileDialog.FileName);

                ReloadExample(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("https://docs.rainmeter.net/tips/colormatrix-guide/");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RemoveMethod();
            LoadValues(Matrix[comboBox1.Text]);
            AddMethod();
            ReloadExample(sender, e);
        }

        Color Clicked = Color.FromArgb(230, 237, 183);
        Color Default = Color.FromArgb(10, 150, 170);

        Color Selected = Color.FromArgb(6, 84, 96);
        Color Button = Color.FromArgb(10, 150, 170);
        Color TextColor = Color.Black;
        Color AltTextColor = Color.White;
        Color ClickedColor = Color.FromArgb(119, 119, 119);
        Color BackgroundColor = Color.White;
        Color GroupBoxColor = Color.White;
        Color TextBoxColor = Color.White;
        Color BorderColor = Color.Black;
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

            checkBox1.ForeColor = TextColor;
            this.BackColor = BackgroundColor;
        }
    }
}
