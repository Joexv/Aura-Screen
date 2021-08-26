
namespace AuraScreen
{
    partial class Welcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Welcome));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.WelcomeTab = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.GettingStartedTab = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.WelcomeTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.WelcomeTab);
            this.tabControl1.Controls.Add(this.GettingStartedTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 661);
            this.tabControl1.TabIndex = 0;
            // 
            // WelcomeTab
            // 
            this.WelcomeTab.BackColor = System.Drawing.Color.Black;
            this.WelcomeTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WelcomeTab.Controls.Add(this.button1);
            this.WelcomeTab.Controls.Add(this.label2);
            this.WelcomeTab.Controls.Add(this.label1);
            this.WelcomeTab.Controls.Add(this.pictureBox1);
            this.WelcomeTab.Location = new System.Drawing.Point(4, 5);
            this.WelcomeTab.Margin = new System.Windows.Forms.Padding(2);
            this.WelcomeTab.Name = "WelcomeTab";
            this.WelcomeTab.Padding = new System.Windows.Forms.Padding(2);
            this.WelcomeTab.Size = new System.Drawing.Size(676, 652);
            this.WelcomeTab.TabIndex = 0;
            this.WelcomeTab.Text = "tabPage1";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(172, 577);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(332, 71);
            this.button1.TabIndex = 3;
            this.button1.Text = "Click Here To Get Started";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(-3, 300);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(688, 62);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bringing some zen to your computer screen\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(0, 235);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(685, 65);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome To Aura Screen";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::AuraScreen.Properties.Resources.Aura_Logo_2;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(672, 648);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // GettingStartedTab
            // 
            this.GettingStartedTab.Location = new System.Drawing.Point(4, 5);
            this.GettingStartedTab.Margin = new System.Windows.Forms.Padding(2);
            this.GettingStartedTab.Name = "GettingStartedTab";
            this.GettingStartedTab.Padding = new System.Windows.Forms.Padding(2);
            this.GettingStartedTab.Size = new System.Drawing.Size(676, 652);
            this.GettingStartedTab.TabIndex = 1;
            this.GettingStartedTab.Text = "tabPage2";
            this.GettingStartedTab.UseVisualStyleBackColor = true;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Welcome";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aura Welcome Screen";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Tomato;
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.tabControl1.ResumeLayout(false);
            this.WelcomeTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage WelcomeTab;
        private System.Windows.Forms.TabPage GettingStartedTab;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}