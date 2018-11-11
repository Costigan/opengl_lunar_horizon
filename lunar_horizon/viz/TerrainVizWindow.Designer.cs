namespace lunar_horizon.viz
{
    partial class TerrainVizWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbSelectTime = new System.Windows.Forms.TrackBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbTimeSpan = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.loadTraversePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTraversePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1190, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTraversePathToolStripMenuItem,
            this.addTraversePathToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 631);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbSelectTime);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 655);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1190, 86);
            this.panel2.TabIndex = 2;
            // 
            // tbSelectTime
            // 
            this.tbSelectTime.BackColor = System.Drawing.Color.White;
            this.tbSelectTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSelectTime.LargeChange = 50;
            this.tbSelectTime.Location = new System.Drawing.Point(288, 0);
            this.tbSelectTime.Maximum = 10000;
            this.tbSelectTime.Name = "tbSelectTime";
            this.tbSelectTime.Size = new System.Drawing.Size(902, 45);
            this.tbSelectTime.TabIndex = 1;
            this.tbSelectTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSelectTime.ValueChanged += new System.EventHandler(this.tbSelectTime_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbTimeSpan);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.dtStartTime);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.lbCurrentTime);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 86);
            this.panel3.TabIndex = 2;
            // 
            // cbTimeSpan
            // 
            this.cbTimeSpan.FormattingEnabled = true;
            this.cbTimeSpan.Items.AddRange(new object[] {
            "3 Months",
            "2 Months",
            "1 Month",
            "2 weeks",
            "1 week",
            "1 day",
            "1 hour"});
            this.cbTimeSpan.Location = new System.Drawing.Point(73, 54);
            this.cbTimeSpan.Name = "cbTimeSpan";
            this.cbTimeSpan.Size = new System.Drawing.Size(200, 21);
            this.cbTimeSpan.TabIndex = 3;
            this.cbTimeSpan.SelectedIndexChanged += new System.EventHandler(this.cbTimeSpan_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Interval:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Start Time:";
            // 
            // dtStartTime
            // 
            this.dtStartTime.Location = new System.Drawing.Point(73, 28);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Size = new System.Drawing.Size(200, 20);
            this.dtStartTime.TabIndex = 0;
            this.dtStartTime.ValueChanged += new System.EventHandler(this.dtStartTime_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Current Time:";
            // 
            // lbCurrentTime
            // 
            this.lbCurrentTime.Location = new System.Drawing.Point(70, 8);
            this.lbCurrentTime.Name = "lbCurrentTime";
            this.lbCurrentTime.Size = new System.Drawing.Size(203, 17);
            this.lbCurrentTime.TabIndex = 1;
            this.lbCurrentTime.Text = "<current time>\r\n";
            this.lbCurrentTime.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // loadTraversePathToolStripMenuItem
            // 
            this.loadTraversePathToolStripMenuItem.Name = "loadTraversePathToolStripMenuItem";
            this.loadTraversePathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.loadTraversePathToolStripMenuItem.Text = "Load traverse path";
            this.loadTraversePathToolStripMenuItem.Click += new System.EventHandler(this.loadTraversePathToolStripMenuItem_Click);
            // 
            // addTraversePathToolStripMenuItem
            // 
            this.addTraversePathToolStripMenuItem.Name = "addTraversePathToolStripMenuItem";
            this.addTraversePathToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addTraversePathToolStripMenuItem.Text = "Add traverse path";
            // 
            // TerrainVizWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 741);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TerrainVizWindow";
            this.Text = "TerrainVizWindow";
            this.Load += new System.EventHandler(this.TerrainVizWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TrackBar tbSelectTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbTimeSpan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.ToolStripMenuItem loadTraversePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addTraversePathToolStripMenuItem;
    }
}