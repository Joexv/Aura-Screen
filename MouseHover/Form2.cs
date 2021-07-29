using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MouseHover
{
    using ps = Properties.Settings;
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            //Adjust Settings
            this.Width = ps.Default.width;
            this.Height = ps.Default.height;
            this.BackColor = ps.Default.color;

            this.Opacity = .5;
            this.TopMost = true;
            this.BackColor = ps.Default.color;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            // Makes the form circular:
            System.Drawing.Drawing2D.GraphicsPath GP = new System.Drawing.Drawing2D.GraphicsPath();

            switch (ps.Default.style)
            {
                case "Rectangle":
                    GP.AddRectangle(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;
                case "Ellipse":
                    GP.AddEllipse(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;
                case "Circle":
                    //centerX - radius, centerY - radius,
                    //radius + radius, radius + radius
                    GP.AddEllipse(0, 0, this.Width, this.Width);
                    this.Region = new Region(GP);
                    return;
                default:
                    GP.AddRectangle(this.ClientRectangle);
                    this.Region = new Region(GP);
                    return;
            }
        }

        const int WS_EX_TRANSPARENT = 0x20;

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT;
                return cp;
            }
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Point pt = Cursor.Position;
            //Adjust Location
            if(ps.Default.style == "Circle")
                pt.Offset(-1 * this.Width / 2, -1 * this.Width / 2);
            else
                pt.Offset(-1 * this.Width / 2, -1 * this.Height / 2);
            this.Location = pt;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (ps.Default.border)
            {
                base.OnPaint(e);
                Pen pen = new Pen(ps.Default.borderColor, ps.Default.borderThicc);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                switch (ps.Default.style)
                {
                    case "Rectangle":
                        e.Graphics.DrawRectangle(pen, rect);
                        return;
                    case "Circle":
                        e.Graphics.DrawEllipse(pen, new Rectangle(0 ,0, this.Width, this.Width));
                        return;
                    case "Ellipse":
                        e.Graphics.DrawEllipse(pen, rect);
                        return;
                }
            }
        }

        Image CaptureScreen(int sourceX, int sourceY, int destX, int destY, Size regionSize)
        {
            Bitmap bmp = new Bitmap(regionSize.Width, regionSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(sourceX, sourceY, destX, destY, regionSize);
            return bmp;
        }

        public Bitmap Transform(Bitmap source)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(source.Width, source.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the negative color matrix
            ColorMatrix colorMatrix = new ColorMatrix(
            new float[][]
            {
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
             });

            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height),
                        0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            //g.Dispose();

            return newBitmap;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (ps.Default.invert)
            {
                //Image image = CaptureScreen(Cursor.Position.X, Cursor.Position.Y, 0, 0, new Size(this.Width, this.Height));
                //this.BackgroundImage = Transform((Bitmap)image);
                //this.TransparencyKey = Color.Turquoise;
                //this.BackColor = Color.Turquoise;
            }
        }

        public Bitmap CaptureScreen()
        {
            Point pt = Cursor.Position;
            pt.Offset(-1 * this.Width, -1 * this.Height);

            this.BackgroundImage = null;
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
            this.Opacity = 0.75;
            Bitmap b = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(b);
            
            g.CopyFromScreen(this.Location.X * 2, (int)(this.Location.Y * 2.5), 0, 0, b.Size);
            g.Dispose();
            return b;
        }

        private void Form2_LocationChanged(object sender, EventArgs e)
        {
            //this.BackgroundImage = CaptureScreen(); //Transform(CaptureScreen());
        }

        public static int GetWindowsScaling()
        {
            var currentDPI = (int)Registry.GetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "LogPixels", 96);
            return (int)(96 / (float)currentDPI); ;
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
