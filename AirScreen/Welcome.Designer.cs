
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
            this.label3 = new System.Windows.Forms.Label();
            this.changelog = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1374, 1329);
            this.tabControl1.TabIndex = 0;
            // 
            // WelcomeTab
            // 
            this.WelcomeTab.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.WelcomeTab.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.WelcomeTab.Controls.Add(this.label3);
            this.WelcomeTab.Controls.Add(this.changelog);
            this.WelcomeTab.Controls.Add(this.button2);
            this.WelcomeTab.Controls.Add(this.button1);
            this.WelcomeTab.Controls.Add(this.pictureBox1);
            this.WelcomeTab.Controls.Add(this.label2);
            this.WelcomeTab.Controls.Add(this.label1);
            this.WelcomeTab.Location = new System.Drawing.Point(8, 9);
            this.WelcomeTab.Name = "WelcomeTab";
            this.WelcomeTab.Padding = new System.Windows.Forms.Padding(3);
            this.WelcomeTab.Size = new System.Drawing.Size(1358, 1312);
            this.WelcomeTab.TabIndex = 0;
            this.WelcomeTab.Text = "tabPage1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(544, 713);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 55);
            this.label3.TabIndex = 6;
            this.label3.Text = "What\'s New";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // changelog
            // 
            this.changelog.Location = new System.Drawing.Point(22, 792);
            this.changelog.Multiline = true;
            this.changelog.Name = "changelog";
            this.changelog.Size = new System.Drawing.Size(1314, 514);
            this.changelog.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(681, 504);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(673, 126);
            this.button2.TabIndex = 4;
            this.button2.Text = "Tutorial";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(673, 126);
            this.button1.TabIndex = 3;
            this.button1.Text = "Get Started";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::AuraScreen.Properties.Resources.Aura_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(554, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 384);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1358, 119);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bringing zen to your computer screen\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1358, 125);
            this.label1.TabIndex = 0;
            this.label1.Text = "Welcome To Aura Screen";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GettingStartedTab
            // 
            this.GettingStartedTab.Location = new System.Drawing.Point(8, 9);
            this.GettingStartedTab.Name = "GettingStartedTab";
            this.GettingStartedTab.Padding = new System.Windows.Forms.Padding(3);
            this.GettingStartedTab.Size = new System.Drawing.Size(1358, 1312);
            this.GettingStartedTab.TabIndex = 1;
            this.GettingStartedTab.Text = "tabPage2";
            this.GettingStartedTab.UseVisualStyleBackColor = true;
            // 
            // Welcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 1329);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Welcome";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aura Welcome Screen";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Pink;
            this.Load += new System.EventHandler(this.Welcome_Load);
            this.tabControl1.ResumeLayout(false);
            this.WelcomeTab.ResumeLayout(false);
            this.WelcomeTab.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox changelog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}