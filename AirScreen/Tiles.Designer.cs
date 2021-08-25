
namespace AuraScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tiles));
            this.PreviewButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.previewTimer = new System.Windows.Forms.Timer(this.components);
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.PreviewButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.PreviewButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.PreviewButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreviewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewButton.ForeColor = System.Drawing.Color.White;
            this.PreviewButton.Location = new System.Drawing.Point(395, 106);
            this.PreviewButton.Margin = new System.Windows.Forms.Padding(4);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(264, 80);
            this.PreviewButton.TabIndex = 0;
            this.PreviewButton.Text = "Preview";
            this.PreviewButton.UseVisualStyleBackColor = false;
            this.PreviewButton.Visible = false;
            this.PreviewButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(209)))), ((int)(((byte)(20)))));
            this.SaveButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.SaveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.ForeColor = System.Drawing.Color.White;
            this.SaveButton.Location = new System.Drawing.Point(395, 18);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(264, 80);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
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
            this.numericUpDown1.Location = new System.Drawing.Point(222, 65);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(4);
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
            this.label1.Location = new System.Drawing.Point(217, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Opacity";
            this.label1.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(30, 104);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(312, 51);
            this.button1.TabIndex = 4;
            this.button1.Text = "Change Color";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
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
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(374, 555);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            this.groupBox1.Visible = false;
            this.groupBox1.VisibleChanged += new System.EventHandler(this.groupBox1_VisibleChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(33, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(312, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "Snap To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(189, 451);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(153, 88);
            this.button7.TabIndex = 15;
            this.button7.Text = "Vertically";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(30, 451);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(153, 88);
            this.button6.TabIndex = 14;
            this.button6.Text = "Horizontally";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(30, 423);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Fill";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(111, 310);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(153, 88);
            this.button5.TabIndex = 11;
            this.button5.Text = "Bottom";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(111, 216);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(153, 88);
            this.button4.TabIndex = 10;
            this.button4.Text = "Top";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(270, 216);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 182);
            this.button3.TabIndex = 9;
            this.button3.Text = "Right";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(30, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 182);
            this.button2.TabIndex = 8;
            this.button2.Text = "Left";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // squwiggly
            // 
            this.squwiggly.AutoSize = true;
            this.squwiggly.Location = new System.Drawing.Point(630, 363);
            this.squwiggly.Margin = new System.Windows.Forms.Padding(4);
            this.squwiggly.Name = "squwiggly";
            this.squwiggly.Size = new System.Drawing.Size(55, 29);
            this.squwiggly.TabIndex = 7;
            this.squwiggly.Text = "~";
            this.squwiggly.UseVisualStyleBackColor = true;
            this.squwiggly.Visible = false;
            this.squwiggly.CheckedChanged += new System.EventHandler(this.squwiggly_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(512, 297);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Manually Refresh On";
            this.label3.Visible = false;
            // 
            // f1
            // 
            this.f1.AutoSize = true;
            this.f1.Location = new System.Drawing.Point(512, 363);
            this.f1.Margin = new System.Windows.Forms.Padding(4);
            this.f1.Name = "f1";
            this.f1.Size = new System.Drawing.Size(68, 29);
            this.f1.TabIndex = 5;
            this.f1.Text = "F1";
            this.f1.UseVisualStyleBackColor = true;
            this.f1.Visible = false;
            this.f1.CheckedChanged += new System.EventHandler(this.f1_CheckedChanged);
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Location = new System.Drawing.Point(630, 329);
            this.r.Margin = new System.Windows.Forms.Padding(4);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(58, 29);
            this.r.TabIndex = 4;
            this.r.Text = "R";
            this.r.UseVisualStyleBackColor = true;
            this.r.Visible = false;
            this.r.CheckedChanged += new System.EventHandler(this.r_CheckedChanged);
            // 
            // shift
            // 
            this.shift.AutoSize = true;
            this.shift.Checked = true;
            this.shift.Location = new System.Drawing.Point(512, 329);
            this.shift.Margin = new System.Windows.Forms.Padding(4);
            this.shift.Name = "shift";
            this.shift.Size = new System.Drawing.Size(86, 29);
            this.shift.TabIndex = 3;
            this.shift.TabStop = true;
            this.shift.Text = "Shift";
            this.shift.UseVisualStyleBackColor = true;
            this.shift.Visible = false;
            this.shift.CheckedChanged += new System.EventHandler(this.shift_CheckedChanged);
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(900, 297);
            this.time.Margin = new System.Windows.Forms.Padding(4);
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
            this.time.Visible = false;
            this.time.ValueChanged += new System.EventHandler(this.time_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(746, 297);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Refresh Every";
            this.label2.Visible = false;
            // 
            // invert
            // 
            this.invert.AutoSize = true;
            this.invert.Location = new System.Drawing.Point(30, 56);
            this.invert.Margin = new System.Windows.Forms.Padding(4);
            this.invert.Name = "invert";
            this.invert.Size = new System.Drawing.Size(97, 29);
            this.invert.TabIndex = 0;
            this.invert.Text = "Invert";
            this.invert.UseVisualStyleBackColor = true;
            this.invert.CheckedChanged += new System.EventHandler(this.invert_CheckedChanged);
            // 
            // InvertTimer
            // 
            this.InvertTimer.Enabled = true;
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
            this.ScrollingTimer.Interval = 500;
            this.ScrollingTimer.Tick += new System.EventHandler(this.ScrollingTimer_Tick);
            // 
            // Tiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(968, 823);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PreviewButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(968, 823);
            this.Name = "Tiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Block Filter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Tiles_FormClosing);
            this.Load += new System.EventHandler(this.Tiles_Load);
            this.Shown += new System.EventHandler(this.Tiles_Shown);
            this.LocationChanged += new System.EventHandler(this.Tiles_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.Tiles_VisibleChanged);
            this.Resize += new System.EventHandler(this.Tiles_Resize);
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}