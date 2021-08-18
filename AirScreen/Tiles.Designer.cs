
namespace AirScreen
{
    partial class Tiles
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
            this.PreviewButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.previewTimer = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.squwiggly = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.f1 = new System.Windows.Forms.RadioButton();
            this.r = new System.Windows.Forms.RadioButton();
            this.shift = new System.Windows.Forms.RadioButton();
            this.time = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.invert = new System.Windows.Forms.CheckBox();
            this.InvertTimer = new System.Windows.Forms.Timer(this.components);
            this.InvertKeyChecker = new System.Windows.Forms.Timer(this.components);
            this.ScrollingTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.time)).BeginInit();
            this.SuspendLayout();
            // 
            // PreviewButton
            // 
            this.PreviewButton.Location = new System.Drawing.Point(12, 12);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(264, 80);
            this.PreviewButton.TabIndex = 0;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = true;
            this.PreviewButton.Visible = false;
            this.PreviewButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(282, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(264, 80);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Visible = false;
            this.SaveButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // previewTimer
            // 
            this.previewTimer.Interval = 2000;
            this.previewTimer.Tick += new System.EventHandler(this.previewTimer_Tick);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 2;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(405, 77);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            131072});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 31);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.numericUpDown1.Visible = false;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(399, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Opacity";
            this.label1.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(404, 140);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 80);
            this.button1.TabIndex = 4;
            this.button1.Text = "Change Color";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.squwiggly);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.f1);
            this.groupBox1.Controls.Add(this.r);
            this.groupBox1.Controls.Add(this.shift);
            this.groupBox1.Controls.Add(this.time);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.invert);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(689, 261);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            this.groupBox1.Visible = false;
            this.groupBox1.VisibleChanged += new System.EventHandler(this.groupBox1_VisibleChanged);
            // 
            // squwiggly
            // 
            this.squwiggly.AutoSize = true;
            this.squwiggly.Location = new System.Drawing.Point(154, 206);
            this.squwiggly.Name = "squwiggly";
            this.squwiggly.Size = new System.Drawing.Size(55, 29);
            this.squwiggly.TabIndex = 7;
            this.squwiggly.Text = "~";
            this.squwiggly.UseVisualStyleBackColor = true;
            this.squwiggly.CheckedChanged += new System.EventHandler(this.squwiggly_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Manually Refresh On";
            // 
            // f1
            // 
            this.f1.AutoSize = true;
            this.f1.Location = new System.Drawing.Point(35, 206);
            this.f1.Name = "f1";
            this.f1.Size = new System.Drawing.Size(68, 29);
            this.f1.TabIndex = 5;
            this.f1.Text = "F1";
            this.f1.UseVisualStyleBackColor = true;
            this.f1.CheckedChanged += new System.EventHandler(this.f1_CheckedChanged);
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Location = new System.Drawing.Point(154, 171);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(58, 29);
            this.r.TabIndex = 4;
            this.r.Text = "R";
            this.r.UseVisualStyleBackColor = true;
            this.r.CheckedChanged += new System.EventHandler(this.r_CheckedChanged);
            // 
            // shift
            // 
            this.shift.AutoSize = true;
            this.shift.Checked = true;
            this.shift.Location = new System.Drawing.Point(35, 171);
            this.shift.Name = "shift";
            this.shift.Size = new System.Drawing.Size(86, 29);
            this.shift.TabIndex = 3;
            this.shift.TabStop = true;
            this.shift.Text = "Shift";
            this.shift.UseVisualStyleBackColor = true;
            this.shift.CheckedChanged += new System.EventHandler(this.shift_CheckedChanged);
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(184, 79);
            this.time.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.time.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(120, 31);
            this.time.TabIndex = 2;
            this.time.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.time.ValueChanged += new System.EventHandler(this.time_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Refresh Every";
            // 
            // invert
            // 
            this.invert.AutoSize = true;
            this.invert.Location = new System.Drawing.Point(30, 45);
            this.invert.Name = "invert";
            this.invert.Size = new System.Drawing.Size(97, 29);
            this.invert.TabIndex = 0;
            this.invert.Text = "Invert";
            this.invert.UseVisualStyleBackColor = true;
            this.invert.CheckedChanged += new System.EventHandler(this.invert_CheckedChanged);
            // 
            // InvertTimer
            // 
            this.InvertTimer.Interval = 5000;
            this.InvertTimer.Tick += new System.EventHandler(this.InvertTimer_Tick);
            // 
            // InvertKeyChecker
            // 
            this.InvertKeyChecker.Interval = 1;
            this.InvertKeyChecker.Tick += new System.EventHandler(this.InvertKeyChecker_Tick);
            // 
            // ScrollingTimer
            // 
            this.ScrollingTimer.Enabled = true;
            this.ScrollingTimer.Interval = 500;
            this.ScrollingTimer.Tick += new System.EventHandler(this.ScrollingTimer_Tick);
            // 
            // Tiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(815, 689);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PreviewButton);
            this.DoubleBuffered = true;
            this.Name = "Tiles";
            this.Text = "Tile Overlay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tiles_FormClosing);
            this.Load += new System.EventHandler(this.Tiles_Load);
            this.Shown += new System.EventHandler(this.Tiles_Shown);
            this.LocationChanged += new System.EventHandler(this.Tiles_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.Tiles_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.time)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer previewTimer;
        public System.Windows.Forms.Button PreviewButton;
        public System.Windows.Forms.Button SaveButton;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton f1;
        private System.Windows.Forms.RadioButton r;
        private System.Windows.Forms.RadioButton shift;
        private System.Windows.Forms.NumericUpDown time;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox invert;
        private System.Windows.Forms.Timer InvertTimer;
        private System.Windows.Forms.RadioButton squwiggly;
        private System.Windows.Forms.Timer InvertKeyChecker;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer ScrollingTimer;
    }
}