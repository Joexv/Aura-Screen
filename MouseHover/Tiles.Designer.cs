
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
            this.SaveButton.Location = new System.Drawing.Point(12, 98);
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
            // Tiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 586);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.PreviewButton);
            this.Name = "Tiles";
            this.Text = "Tile Overlay";
            this.Load += new System.EventHandler(this.Tiles_Load);
            this.Shown += new System.EventHandler(this.Tiles_Shown);
            this.LocationChanged += new System.EventHandler(this.Tiles_LocationChanged);
            this.VisibleChanged += new System.EventHandler(this.Tiles_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer previewTimer;
        public System.Windows.Forms.Button PreviewButton;
        public System.Windows.Forms.Button SaveButton;
    }
}