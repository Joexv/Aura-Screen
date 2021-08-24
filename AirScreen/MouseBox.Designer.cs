
namespace AuraScreen
{
    partial class MouseBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LocationTimer = new System.Windows.Forms.Timer(this.components);
            this.InvertTimer = new System.Windows.Forms.Timer(this.components);
            this.MagTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // LocationTimer
            // 
            this.LocationTimer.Enabled = true;
            this.LocationTimer.Interval = 1;
            this.LocationTimer.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // InvertTimer
            // 
            this.InvertTimer.Enabled = true;
            this.InvertTimer.Interval = 1;
            this.InvertTimer.Tick += new System.EventHandler(this.InvertTimer_Tick);
            // 
            // MagTimer
            // 
            this.MagTimer.Interval = 1;
            this.MagTimer.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // MouseBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(484, 417);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MouseBox";
            this.Opacity = 0.15D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form2";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MouseBox_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.MouseBox_Shown);
            this.LocationChanged += new System.EventHandler(this.Form2_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.MouseBox_VisibleChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
            this.MouseLeave += new System.EventHandler(this.Form2_MouseLeave);
            this.Resize += new System.EventHandler(this.MouseBox_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer LocationTimer;
        private System.Windows.Forms.Timer InvertTimer;
        public System.Windows.Forms.Timer MagTimer;
    }
}