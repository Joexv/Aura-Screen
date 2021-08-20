
namespace AirScreen
{
    partial class Toolbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbox));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cursorToggle = new System.Windows.Forms.Button();
            this.borderToggle = new System.Windows.Forms.Button();
            this.cursorInvert = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.enlargeCursor = new System.Windows.Forms.Button();
            this.shrinkCursor = new System.Windows.Forms.Button();
            this.raiseOpacity = new System.Windows.Forms.Button();
            this.lowerOpacity = new System.Windows.Forms.Button();
            this.AO_Toggle = new System.Windows.Forms.Button();
            this.AO_Active = new System.Windows.Forms.Button();
            this.SF_Toggle = new System.Windows.Forms.Button();
            this.SF_Program = new System.Windows.Forms.Button();
            this.SF_Cycle = new System.Windows.Forms.Button();
            this.BF_Toggle = new System.Windows.Forms.Button();
            this.BF_Manual = new System.Windows.Forms.Button();
            this.BF_Edit = new System.Windows.Forms.Button();
            this.BF_Left = new System.Windows.Forms.Button();
            this.BF_Bottom = new System.Windows.Forms.Button();
            this.BF_Top = new System.Windows.Forms.Button();
            this.BF_Right = new System.Windows.Forms.Button();
            this.BF_Cycle = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.cursorToggle);
            this.flowLayoutPanel1.Controls.Add(this.borderToggle);
            this.flowLayoutPanel1.Controls.Add(this.cursorInvert);
            this.flowLayoutPanel1.Controls.Add(this.button3);
            this.flowLayoutPanel1.Controls.Add(this.enlargeCursor);
            this.flowLayoutPanel1.Controls.Add(this.shrinkCursor);
            this.flowLayoutPanel1.Controls.Add(this.raiseOpacity);
            this.flowLayoutPanel1.Controls.Add(this.lowerOpacity);
            this.flowLayoutPanel1.Controls.Add(this.AO_Toggle);
            this.flowLayoutPanel1.Controls.Add(this.AO_Active);
            this.flowLayoutPanel1.Controls.Add(this.SF_Toggle);
            this.flowLayoutPanel1.Controls.Add(this.SF_Program);
            this.flowLayoutPanel1.Controls.Add(this.SF_Cycle);
            this.flowLayoutPanel1.Controls.Add(this.BF_Toggle);
            this.flowLayoutPanel1.Controls.Add(this.BF_Manual);
            this.flowLayoutPanel1.Controls.Add(this.BF_Edit);
            this.flowLayoutPanel1.Controls.Add(this.BF_Top);
            this.flowLayoutPanel1.Controls.Add(this.BF_Bottom);
            this.flowLayoutPanel1.Controls.Add(this.BF_Left);
            this.flowLayoutPanel1.Controls.Add(this.BF_Right);
            this.flowLayoutPanel1.Controls.Add(this.BF_Cycle);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(752, 1184);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.MouseEnter += new System.EventHandler(this.flowLayoutPanel1_MouseEnter);
            // 
            // cursorToggle
            // 
            this.cursorToggle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cursorToggle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cursorToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.cursorToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cursorToggle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.cursorToggle.FlatAppearance.BorderSize = 0;
            this.cursorToggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.cursorToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cursorToggle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorToggle.ForeColor = System.Drawing.Color.White;
            this.cursorToggle.Image = global::AirScreen.Properties.Resources.Mouse_Filter_1;
            this.cursorToggle.Location = new System.Drawing.Point(7, 7);
            this.cursorToggle.MaximumSize = new System.Drawing.Size(180, 190);
            this.cursorToggle.MinimumSize = new System.Drawing.Size(25, 25);
            this.cursorToggle.Name = "cursorToggle";
            this.cursorToggle.Padding = new System.Windows.Forms.Padding(5);
            this.cursorToggle.Size = new System.Drawing.Size(180, 190);
            this.cursorToggle.TabIndex = 48;
            this.cursorToggle.Text = "Cursor Toggle";
            this.cursorToggle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cursorToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cursorToggle.UseVisualStyleBackColor = false;
            this.cursorToggle.Click += new System.EventHandler(this.cursorToggle_Click);
            // 
            // borderToggle
            // 
            this.borderToggle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.borderToggle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.borderToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.borderToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.borderToggle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.borderToggle.FlatAppearance.BorderSize = 0;
            this.borderToggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.borderToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.borderToggle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.borderToggle.ForeColor = System.Drawing.Color.White;
            this.borderToggle.Image = global::AirScreen.Properties.Resources.Mouse_Border_1;
            this.borderToggle.Location = new System.Drawing.Point(193, 7);
            this.borderToggle.MaximumSize = new System.Drawing.Size(180, 190);
            this.borderToggle.MinimumSize = new System.Drawing.Size(25, 25);
            this.borderToggle.Name = "borderToggle";
            this.borderToggle.Padding = new System.Windows.Forms.Padding(5);
            this.borderToggle.Size = new System.Drawing.Size(180, 190);
            this.borderToggle.TabIndex = 50;
            this.borderToggle.Text = "Toggle Border";
            this.borderToggle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.borderToggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.borderToggle.UseVisualStyleBackColor = false;
            this.borderToggle.Click += new System.EventHandler(this.borderToggle_Click);
            // 
            // cursorInvert
            // 
            this.cursorInvert.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cursorInvert.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cursorInvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.cursorInvert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cursorInvert.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.cursorInvert.FlatAppearance.BorderSize = 0;
            this.cursorInvert.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.cursorInvert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cursorInvert.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cursorInvert.ForeColor = System.Drawing.Color.White;
            this.cursorInvert.Image = global::AirScreen.Properties.Resources.Mouse_Invert_1;
            this.cursorInvert.Location = new System.Drawing.Point(379, 7);
            this.cursorInvert.MaximumSize = new System.Drawing.Size(180, 190);
            this.cursorInvert.MinimumSize = new System.Drawing.Size(25, 25);
            this.cursorInvert.Name = "cursorInvert";
            this.cursorInvert.Padding = new System.Windows.Forms.Padding(5);
            this.cursorInvert.Size = new System.Drawing.Size(180, 190);
            this.cursorInvert.TabIndex = 49;
            this.cursorInvert.Text = "Invert";
            this.cursorInvert.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cursorInvert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cursorInvert.UseVisualStyleBackColor = false;
            this.cursorInvert.Click += new System.EventHandler(this.cursorInvert_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::AirScreen.Properties.Resources.Mouse_Shape_1;
            this.button3.Location = new System.Drawing.Point(565, 7);
            this.button3.MaximumSize = new System.Drawing.Size(180, 190);
            this.button3.MinimumSize = new System.Drawing.Size(25, 25);
            this.button3.Name = "button3";
            this.button3.Padding = new System.Windows.Forms.Padding(5);
            this.button3.Size = new System.Drawing.Size(180, 190);
            this.button3.TabIndex = 78;
            this.button3.Text = "Cycle Cursor Shapes";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.Cursor_Cycle_Click);
            // 
            // enlargeCursor
            // 
            this.enlargeCursor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.enlargeCursor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.enlargeCursor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.enlargeCursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.enlargeCursor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.enlargeCursor.FlatAppearance.BorderSize = 0;
            this.enlargeCursor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.enlargeCursor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enlargeCursor.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enlargeCursor.ForeColor = System.Drawing.Color.White;
            this.enlargeCursor.Image = global::AirScreen.Properties.Resources.Mouse_Enlarge_1;
            this.enlargeCursor.Location = new System.Drawing.Point(7, 203);
            this.enlargeCursor.MaximumSize = new System.Drawing.Size(180, 190);
            this.enlargeCursor.MinimumSize = new System.Drawing.Size(25, 25);
            this.enlargeCursor.Name = "enlargeCursor";
            this.enlargeCursor.Padding = new System.Windows.Forms.Padding(5);
            this.enlargeCursor.Size = new System.Drawing.Size(180, 190);
            this.enlargeCursor.TabIndex = 52;
            this.enlargeCursor.Text = "Enlarge Cursor";
            this.enlargeCursor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.enlargeCursor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.enlargeCursor.UseVisualStyleBackColor = false;
            this.enlargeCursor.Click += new System.EventHandler(this.enlargeCursor_Click);
            // 
            // shrinkCursor
            // 
            this.shrinkCursor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.shrinkCursor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shrinkCursor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.shrinkCursor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shrinkCursor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.shrinkCursor.FlatAppearance.BorderSize = 0;
            this.shrinkCursor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.shrinkCursor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.shrinkCursor.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shrinkCursor.ForeColor = System.Drawing.Color.White;
            this.shrinkCursor.Image = ((System.Drawing.Image)(resources.GetObject("shrinkCursor.Image")));
            this.shrinkCursor.Location = new System.Drawing.Point(193, 203);
            this.shrinkCursor.MaximumSize = new System.Drawing.Size(180, 190);
            this.shrinkCursor.MinimumSize = new System.Drawing.Size(25, 25);
            this.shrinkCursor.Name = "shrinkCursor";
            this.shrinkCursor.Padding = new System.Windows.Forms.Padding(5);
            this.shrinkCursor.Size = new System.Drawing.Size(180, 190);
            this.shrinkCursor.TabIndex = 51;
            this.shrinkCursor.Text = "Shrink Cursor";
            this.shrinkCursor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.shrinkCursor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.shrinkCursor.UseVisualStyleBackColor = false;
            this.shrinkCursor.Click += new System.EventHandler(this.shrinkCursor_Click);
            // 
            // raiseOpacity
            // 
            this.raiseOpacity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.raiseOpacity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.raiseOpacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.raiseOpacity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.raiseOpacity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.raiseOpacity.FlatAppearance.BorderSize = 0;
            this.raiseOpacity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.raiseOpacity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.raiseOpacity.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.raiseOpacity.ForeColor = System.Drawing.Color.White;
            this.raiseOpacity.Image = global::AirScreen.Properties.Resources.Mouse_Enlarge_1;
            this.raiseOpacity.Location = new System.Drawing.Point(379, 203);
            this.raiseOpacity.MaximumSize = new System.Drawing.Size(180, 190);
            this.raiseOpacity.MinimumSize = new System.Drawing.Size(25, 25);
            this.raiseOpacity.Name = "raiseOpacity";
            this.raiseOpacity.Padding = new System.Windows.Forms.Padding(5);
            this.raiseOpacity.Size = new System.Drawing.Size(180, 190);
            this.raiseOpacity.TabIndex = 54;
            this.raiseOpacity.Text = "Raise Opacaity";
            this.raiseOpacity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.raiseOpacity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.raiseOpacity.UseVisualStyleBackColor = false;
            this.raiseOpacity.Click += new System.EventHandler(this.raiseOpacity_Click);
            // 
            // lowerOpacity
            // 
            this.lowerOpacity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lowerOpacity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.lowerOpacity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.lowerOpacity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lowerOpacity.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.lowerOpacity.FlatAppearance.BorderSize = 0;
            this.lowerOpacity.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.lowerOpacity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lowerOpacity.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowerOpacity.ForeColor = System.Drawing.Color.White;
            this.lowerOpacity.Image = global::AirScreen.Properties.Resources.Mouse_DOpacity_1;
            this.lowerOpacity.Location = new System.Drawing.Point(565, 203);
            this.lowerOpacity.MaximumSize = new System.Drawing.Size(180, 190);
            this.lowerOpacity.MinimumSize = new System.Drawing.Size(25, 25);
            this.lowerOpacity.Name = "lowerOpacity";
            this.lowerOpacity.Padding = new System.Windows.Forms.Padding(5);
            this.lowerOpacity.Size = new System.Drawing.Size(180, 190);
            this.lowerOpacity.TabIndex = 53;
            this.lowerOpacity.Text = "Lower Opacity";
            this.lowerOpacity.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lowerOpacity.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.lowerOpacity.UseVisualStyleBackColor = false;
            this.lowerOpacity.Click += new System.EventHandler(this.lowerOpacity_Click);
            // 
            // AO_Toggle
            // 
            this.AO_Toggle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AO_Toggle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AO_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.AO_Toggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AO_Toggle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.AO_Toggle.FlatAppearance.BorderSize = 0;
            this.AO_Toggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.AO_Toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AO_Toggle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AO_Toggle.ForeColor = System.Drawing.Color.White;
            this.AO_Toggle.Image = global::AirScreen.Properties.Resources.AppOverlay_Toggle_1;
            this.AO_Toggle.Location = new System.Drawing.Point(7, 399);
            this.AO_Toggle.MaximumSize = new System.Drawing.Size(180, 190);
            this.AO_Toggle.MinimumSize = new System.Drawing.Size(25, 25);
            this.AO_Toggle.Name = "AO_Toggle";
            this.AO_Toggle.Padding = new System.Windows.Forms.Padding(5);
            this.AO_Toggle.Size = new System.Drawing.Size(180, 190);
            this.AO_Toggle.TabIndex = 55;
            this.AO_Toggle.Text = "Toggle App Overlay";
            this.AO_Toggle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AO_Toggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AO_Toggle.UseVisualStyleBackColor = false;
            this.AO_Toggle.Click += new System.EventHandler(this.AO_Toggle_Click);
            // 
            // AO_Active
            // 
            this.AO_Active.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AO_Active.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AO_Active.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.AO_Active.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AO_Active.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.AO_Active.FlatAppearance.BorderSize = 0;
            this.AO_Active.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.AO_Active.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AO_Active.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AO_Active.ForeColor = System.Drawing.Color.White;
            this.AO_Active.Image = global::AirScreen.Properties.Resources.AppOverlay_Active_1;
            this.AO_Active.Location = new System.Drawing.Point(193, 399);
            this.AO_Active.MaximumSize = new System.Drawing.Size(180, 190);
            this.AO_Active.MinimumSize = new System.Drawing.Size(25, 25);
            this.AO_Active.Name = "AO_Active";
            this.AO_Active.Padding = new System.Windows.Forms.Padding(5);
            this.AO_Active.Size = new System.Drawing.Size(180, 190);
            this.AO_Active.TabIndex = 56;
            this.AO_Active.Text = "Program AO Mode";
            this.AO_Active.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AO_Active.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AO_Active.UseVisualStyleBackColor = false;
            this.AO_Active.Click += new System.EventHandler(this.AO_Active_Click);
            // 
            // SF_Toggle
            // 
            this.SF_Toggle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SF_Toggle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SF_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.SF_Toggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SF_Toggle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.SF_Toggle.FlatAppearance.BorderSize = 0;
            this.SF_Toggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.SF_Toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SF_Toggle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SF_Toggle.ForeColor = System.Drawing.Color.White;
            this.SF_Toggle.Image = global::AirScreen.Properties.Resources.ScreenFilter_Toggle_1;
            this.SF_Toggle.Location = new System.Drawing.Point(379, 399);
            this.SF_Toggle.MaximumSize = new System.Drawing.Size(180, 190);
            this.SF_Toggle.MinimumSize = new System.Drawing.Size(25, 25);
            this.SF_Toggle.Name = "SF_Toggle";
            this.SF_Toggle.Padding = new System.Windows.Forms.Padding(5);
            this.SF_Toggle.Size = new System.Drawing.Size(180, 190);
            this.SF_Toggle.TabIndex = 67;
            this.SF_Toggle.Text = "Toggle Screen Filter";
            this.SF_Toggle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SF_Toggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SF_Toggle.UseVisualStyleBackColor = false;
            this.SF_Toggle.Click += new System.EventHandler(this.SF_Toggle_Click);
            // 
            // SF_Program
            // 
            this.SF_Program.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SF_Program.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SF_Program.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.SF_Program.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SF_Program.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.SF_Program.FlatAppearance.BorderSize = 0;
            this.SF_Program.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.SF_Program.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SF_Program.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SF_Program.ForeColor = System.Drawing.Color.White;
            this.SF_Program.Image = global::AirScreen.Properties.Resources.ScreenFilter_Program_1;
            this.SF_Program.Location = new System.Drawing.Point(565, 399);
            this.SF_Program.MaximumSize = new System.Drawing.Size(180, 190);
            this.SF_Program.MinimumSize = new System.Drawing.Size(25, 25);
            this.SF_Program.Name = "SF_Program";
            this.SF_Program.Padding = new System.Windows.Forms.Padding(5);
            this.SF_Program.Size = new System.Drawing.Size(180, 190);
            this.SF_Program.TabIndex = 69;
            this.SF_Program.Text = "Toggle SF Program Mode";
            this.SF_Program.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SF_Program.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SF_Program.UseVisualStyleBackColor = false;
            this.SF_Program.Click += new System.EventHandler(this.SF_Program_Click);
            // 
            // SF_Cycle
            // 
            this.SF_Cycle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SF_Cycle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SF_Cycle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.SF_Cycle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SF_Cycle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.SF_Cycle.FlatAppearance.BorderSize = 0;
            this.SF_Cycle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.SF_Cycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SF_Cycle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SF_Cycle.ForeColor = System.Drawing.Color.White;
            this.SF_Cycle.Image = global::AirScreen.Properties.Resources.ScreenFilter_Cycle_1;
            this.SF_Cycle.Location = new System.Drawing.Point(7, 595);
            this.SF_Cycle.MaximumSize = new System.Drawing.Size(180, 190);
            this.SF_Cycle.MinimumSize = new System.Drawing.Size(25, 25);
            this.SF_Cycle.Name = "SF_Cycle";
            this.SF_Cycle.Padding = new System.Windows.Forms.Padding(5);
            this.SF_Cycle.Size = new System.Drawing.Size(180, 190);
            this.SF_Cycle.TabIndex = 68;
            this.SF_Cycle.Text = "Cycle Screen Filters";
            this.SF_Cycle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.SF_Cycle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SF_Cycle.UseVisualStyleBackColor = false;
            this.SF_Cycle.Click += new System.EventHandler(this.SF_Cycle_Click);
            // 
            // BF_Toggle
            // 
            this.BF_Toggle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Toggle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Toggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Toggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Toggle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Toggle.FlatAppearance.BorderSize = 0;
            this.BF_Toggle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Toggle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Toggle.ForeColor = System.Drawing.Color.White;
            this.BF_Toggle.Image = global::AirScreen.Properties.Resources.BoxFilter_Toggle_1;
            this.BF_Toggle.Location = new System.Drawing.Point(193, 595);
            this.BF_Toggle.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Toggle.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Toggle.Name = "BF_Toggle";
            this.BF_Toggle.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Toggle.Size = new System.Drawing.Size(180, 190);
            this.BF_Toggle.TabIndex = 70;
            this.BF_Toggle.Text = "Toggle Block Filter";
            this.BF_Toggle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Toggle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Toggle.UseVisualStyleBackColor = false;
            this.BF_Toggle.Click += new System.EventHandler(this.BF_Toggle_Click);
            // 
            // BF_Manual
            // 
            this.BF_Manual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Manual.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Manual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Manual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Manual.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Manual.FlatAppearance.BorderSize = 0;
            this.BF_Manual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Manual.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Manual.ForeColor = System.Drawing.Color.White;
            this.BF_Manual.Image = global::AirScreen.Properties.Resources.BoxFilter_Manual_1;
            this.BF_Manual.Location = new System.Drawing.Point(379, 595);
            this.BF_Manual.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Manual.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Manual.Name = "BF_Manual";
            this.BF_Manual.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Manual.Size = new System.Drawing.Size(180, 190);
            this.BF_Manual.TabIndex = 72;
            this.BF_Manual.Text = "Block Filter Manual Mode";
            this.BF_Manual.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Manual.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Manual.UseVisualStyleBackColor = false;
            this.BF_Manual.Click += new System.EventHandler(this.BF_Manual_Click);
            // 
            // BF_Edit
            // 
            this.BF_Edit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Edit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Edit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Edit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Edit.FlatAppearance.BorderSize = 0;
            this.BF_Edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Edit.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Edit.ForeColor = System.Drawing.Color.White;
            this.BF_Edit.Image = global::AirScreen.Properties.Resources.BoxFilter_Edit_1;
            this.BF_Edit.Location = new System.Drawing.Point(565, 595);
            this.BF_Edit.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Edit.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Edit.Name = "BF_Edit";
            this.BF_Edit.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Edit.Size = new System.Drawing.Size(180, 190);
            this.BF_Edit.TabIndex = 73;
            this.BF_Edit.Text = "Block Filter Edit Mode";
            this.BF_Edit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Edit.UseVisualStyleBackColor = false;
            this.BF_Edit.Click += new System.EventHandler(this.BF_Edit_Click);
            // 
            // BF_Left
            // 
            this.BF_Left.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Left.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Left.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Left.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Left.FlatAppearance.BorderSize = 0;
            this.BF_Left.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Left.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Left.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Left.ForeColor = System.Drawing.Color.White;
            this.BF_Left.Image = global::AirScreen.Properties.Resources.BoxFilter_Toggle_1;
            this.BF_Left.Location = new System.Drawing.Point(379, 791);
            this.BF_Left.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Left.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Left.Name = "BF_Left";
            this.BF_Left.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Left.Size = new System.Drawing.Size(180, 190);
            this.BF_Left.TabIndex = 76;
            this.BF_Left.Text = "Block Filter Left";
            this.BF_Left.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Left.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Left.UseVisualStyleBackColor = false;
            this.BF_Left.Click += new System.EventHandler(this.BF_Left_Click);
            // 
            // BF_Bottom
            // 
            this.BF_Bottom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Bottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Bottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Bottom.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Bottom.FlatAppearance.BorderSize = 0;
            this.BF_Bottom.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Bottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Bottom.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Bottom.ForeColor = System.Drawing.Color.White;
            this.BF_Bottom.Image = global::AirScreen.Properties.Resources.BF_Bottom_1;
            this.BF_Bottom.Location = new System.Drawing.Point(193, 791);
            this.BF_Bottom.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Bottom.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Bottom.Name = "BF_Bottom";
            this.BF_Bottom.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Bottom.Size = new System.Drawing.Size(180, 190);
            this.BF_Bottom.TabIndex = 75;
            this.BF_Bottom.Text = "Block Filter Bottom";
            this.BF_Bottom.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Bottom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Bottom.UseVisualStyleBackColor = false;
            this.BF_Bottom.Click += new System.EventHandler(this.BF_Bottom_Click);
            // 
            // BF_Top
            // 
            this.BF_Top.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Top.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Top.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Top.FlatAppearance.BorderSize = 0;
            this.BF_Top.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Top.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Top.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Top.ForeColor = System.Drawing.Color.White;
            this.BF_Top.Image = global::AirScreen.Properties.Resources.BF_Top_1;
            this.BF_Top.Location = new System.Drawing.Point(7, 791);
            this.BF_Top.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Top.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Top.Name = "BF_Top";
            this.BF_Top.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Top.Size = new System.Drawing.Size(180, 190);
            this.BF_Top.TabIndex = 74;
            this.BF_Top.Text = "Block Filter Top";
            this.BF_Top.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Top.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Top.UseVisualStyleBackColor = false;
            this.BF_Top.Click += new System.EventHandler(this.BF_Top_Click);
            // 
            // BF_Right
            // 
            this.BF_Right.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Right.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Right.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Right.FlatAppearance.BorderSize = 0;
            this.BF_Right.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Right.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Right.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Right.ForeColor = System.Drawing.Color.White;
            this.BF_Right.Image = global::AirScreen.Properties.Resources.BF_Right_1;
            this.BF_Right.Location = new System.Drawing.Point(565, 791);
            this.BF_Right.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Right.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Right.Name = "BF_Right";
            this.BF_Right.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Right.Size = new System.Drawing.Size(180, 190);
            this.BF_Right.TabIndex = 77;
            this.BF_Right.Text = "Block Filter Right";
            this.BF_Right.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Right.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Right.UseVisualStyleBackColor = false;
            this.BF_Right.Click += new System.EventHandler(this.BF_Right_Click);
            // 
            // BF_Cycle
            // 
            this.BF_Cycle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BF_Cycle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BF_Cycle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(170)))));
            this.BF_Cycle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BF_Cycle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(53)))), ((int)(((byte)(65)))));
            this.BF_Cycle.FlatAppearance.BorderSize = 0;
            this.BF_Cycle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(237)))), ((int)(((byte)(183)))));
            this.BF_Cycle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BF_Cycle.Font = new System.Drawing.Font("Constantia", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BF_Cycle.ForeColor = System.Drawing.Color.White;
            this.BF_Cycle.Image = global::AirScreen.Properties.Resources.BlockFilter_Cycle_1;
            this.BF_Cycle.Location = new System.Drawing.Point(7, 987);
            this.BF_Cycle.MaximumSize = new System.Drawing.Size(180, 190);
            this.BF_Cycle.MinimumSize = new System.Drawing.Size(25, 25);
            this.BF_Cycle.Name = "BF_Cycle";
            this.BF_Cycle.Padding = new System.Windows.Forms.Padding(5);
            this.BF_Cycle.Size = new System.Drawing.Size(180, 190);
            this.BF_Cycle.TabIndex = 71;
            this.BF_Cycle.Text = "Cycle Block Locations";
            this.BF_Cycle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BF_Cycle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BF_Cycle.UseVisualStyleBackColor = false;
            this.BF_Cycle.Click += new System.EventHandler(this.BF_Cycle_Click);
            // 
            // Toolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(752, 1184);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Toolbox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Air Screen Toolbox";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Toolbox_Load);
            this.Shown += new System.EventHandler(this.Toolbox_Shown);
            this.Leave += new System.EventHandler(this.Toolbox_Leave);
            this.MouseEnter += new System.EventHandler(this.Toolbox_MouseEnter);
            this.Resize += new System.EventHandler(this.Toolbox_Resize);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button cursorToggle;
        private System.Windows.Forms.Button cursorInvert;
        private System.Windows.Forms.Button borderToggle;
        private System.Windows.Forms.Button shrinkCursor;
        private System.Windows.Forms.Button enlargeCursor;
        private System.Windows.Forms.Button lowerOpacity;
        private System.Windows.Forms.Button raiseOpacity;
        private System.Windows.Forms.Button AO_Toggle;
        private System.Windows.Forms.Button AO_Active;
        private System.Windows.Forms.Button SF_Toggle;
        private System.Windows.Forms.Button SF_Cycle;
        private System.Windows.Forms.Button SF_Program;
        private System.Windows.Forms.Button BF_Toggle;
        private System.Windows.Forms.Button BF_Cycle;
        private System.Windows.Forms.Button BF_Manual;
        private System.Windows.Forms.Button BF_Edit;
        private System.Windows.Forms.Button BF_Top;
        private System.Windows.Forms.Button BF_Bottom;
        private System.Windows.Forms.Button BF_Left;
        private System.Windows.Forms.Button BF_Right;
        private System.Windows.Forms.Button button3;
    }
}