using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Magnifier;

namespace AuraScreen
{
    public partial class MatrixCreator : Form
    {
        public MatrixCreator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddMethod();
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
            float[,] matrix = numericFloat();
            adjPicture.Image = Matrices.Transform((Bitmap)basePicture.Image, matrix);
        }

        private float[,] numericFloat()
        {
            return new float[,] { 
                { (float)numericUpDown1.Value, (float)numericUpDown2.Value, (float)numericUpDown3.Value, (float)numericUpDown4.Value, (float)numericUpDown5.Value,},
                { (float)numericUpDown6.Value, (float)numericUpDown7.Value, (float)numericUpDown8.Value, (float)numericUpDown9.Value, (float)numericUpDown10.Value,},
                { (float)numericUpDown11.Value, (float)numericUpDown12.Value, (float)numericUpDown13.Value, (float)numericUpDown14.Value, (float)numericUpDown15.Value,},
                { (float)numericUpDown16.Value, (float)numericUpDown17.Value, (float)numericUpDown18.Value, (float)numericUpDown19.Value, (float)numericUpDown20.Value,},
                { (float)numericUpDown21.Value, (float)numericUpDown22.Value, (float)numericUpDown23.Value, (float)numericUpDown24.Value, (float)numericUpDown25.Value,},
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
            DialogResult dialogResult = MessageBox.Show("This will overwrite all current values and cannot be undone. Continue?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = Application.StartupPath;
                    openFileDialog.Filter = "Matrix files (*.ini)|*.ini";
                    openFileDialog.FilterIndex = 2;
                    openFileDialog.RestoreDirectory = false;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        float[,] matrix = Matrices.StringToMatrix(File.ReadAllText(openFileDialog.FileName));
                        RemoveMethod();
                        numericUpDown1.Value = (decimal)matrix[0, 0];
                        numericUpDown2.Value = (decimal)matrix[0, 1];
                        numericUpDown3.Value = (decimal)matrix[0, 2];
                        numericUpDown4.Value = (decimal)matrix[0, 3];
                        numericUpDown5.Value = (decimal)matrix[0, 4];

                        numericUpDown6.Value = (decimal)matrix[1, 0];
                        numericUpDown7.Value = (decimal)matrix[1, 1];
                        numericUpDown8.Value = (decimal)matrix[1, 2];
                        numericUpDown9.Value = (decimal)matrix[1, 3];
                        numericUpDown10.Value = (decimal)matrix[1, 4];

                        numericUpDown11.Value = (decimal)matrix[2, 0];
                        numericUpDown12.Value = (decimal)matrix[2, 1];
                        numericUpDown13.Value = (decimal)matrix[2, 2];
                        numericUpDown14.Value = (decimal)matrix[2, 3];
                        numericUpDown15.Value = (decimal)matrix[2, 4];

                        numericUpDown16.Value = (decimal)matrix[3, 0];
                        numericUpDown17.Value = (decimal)matrix[3, 1];
                        numericUpDown18.Value = (decimal)matrix[3, 2];
                        numericUpDown19.Value = (decimal)matrix[3, 3];
                        numericUpDown20.Value = (decimal)matrix[3, 4];

                        numericUpDown21.Value = (decimal)matrix[4, 0];
                        numericUpDown22.Value = (decimal)matrix[4, 1];
                        numericUpDown23.Value = (decimal)matrix[4, 2];
                        numericUpDown24.Value = (decimal)matrix[4, 3];
                        numericUpDown25.Value = (decimal)matrix[4, 4];

                        AddMethod();
                        ReloadExample(sender, e);
                    }
                }
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Matrices.MatrixToString(Matrices.Negative));
        }
    }
}
