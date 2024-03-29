﻿
namespace AuraScreen
{
    partial class AppOverlay
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
            this.AO_AttatchTimer = new System.Windows.Forms.Timer(this.components);
            this.AO_InvertTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // AO_AttatchTimer
            // 
            this.AO_AttatchTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AO_InvertTimer
            // 
            this.AO_InvertTimer.Enabled = true;
            this.AO_InvertTimer.Interval = 5000;
            this.AO_InvertTimer.Tick += new System.EventHandler(this.AO_InvertTimer_Tick);
            // 
            // AppOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "AppOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AppOverlay_Load);
            this.Shown += new System.EventHandler(this.AppOverlay_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer AO_AttatchTimer;
        private System.Windows.Forms.Timer AO_InvertTimer;
    }
}