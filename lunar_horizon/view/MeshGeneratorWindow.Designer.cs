namespace lunar_horizon.view
{
    partial class MeshGeneratorWindow
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
            this.label8 = new System.Windows.Forms.Label();
            this.btnShowCoverage = new System.Windows.Forms.Button();
            this.btnWriteMesh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbZoom = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbIncludeColors = new System.Windows.Forms.CheckBox();
            this.cbAscii = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbStats = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Bounds";
            // 
            // btnShowCoverage
            // 
            this.btnShowCoverage.Location = new System.Drawing.Point(3, 3);
            this.btnShowCoverage.Name = "btnShowCoverage";
            this.btnShowCoverage.Size = new System.Drawing.Size(75, 40);
            this.btnShowCoverage.TabIndex = 5;
            this.btnShowCoverage.Text = "Show Coverage";
            this.btnShowCoverage.UseVisualStyleBackColor = true;
            this.btnShowCoverage.Click += new System.EventHandler(this.btnShowCoverage_Click);
            // 
            // btnWriteMesh
            // 
            this.btnWriteMesh.Location = new System.Drawing.Point(3, 49);
            this.btnWriteMesh.Name = "btnWriteMesh";
            this.btnWriteMesh.Size = new System.Drawing.Size(75, 40);
            this.btnWriteMesh.TabIndex = 5;
            this.btnWriteMesh.Text = "Write Mesh";
            this.btnWriteMesh.UseVisualStyleBackColor = true;
            this.btnWriteMesh.Click += new System.EventHandler(this.btnWriteMesh_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Zoom Progression";
            // 
            // cbZoom
            // 
            this.cbZoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbZoom.FormattingEnabled = true;
            this.cbZoom.Items.AddRange(new object[] {
            "1,2,3,4,5,6,1000",
            "1,3,6,10,14,18,1000",
            "1.5,2,4,8,1000,1001,1002",
            "2,3,6,7,8,9,1000",
            "2,4,6, 8,10,14,1000",
            "2,4,8,12,16,20,1000",
            "3,6,12,16,20,24,1000"});
            this.cbZoom.Location = new System.Drawing.Point(0, 0);
            this.cbZoom.Name = "cbZoom";
            this.cbZoom.Size = new System.Drawing.Size(150, 21);
            this.cbZoom.Sorted = true;
            this.cbZoom.TabIndex = 7;
            this.cbZoom.TextChanged += new System.EventHandler(this.cbZoom_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbZoom);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 28);
            this.panel3.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(150, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "Generate";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cbIncludeColors);
            this.panel4.Controls.Add(this.cbAscii);
            this.panel4.Controls.Add(this.btnShowCoverage);
            this.panel4.Controls.Add(this.btnWriteMesh);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 67);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 95);
            this.panel4.TabIndex = 10;
            // 
            // cbIncludeColors
            // 
            this.cbIncludeColors.AutoSize = true;
            this.cbIncludeColors.Location = new System.Drawing.Point(84, 72);
            this.cbIncludeColors.Name = "cbIncludeColors";
            this.cbIncludeColors.Size = new System.Drawing.Size(64, 17);
            this.cbIncludeColors.TabIndex = 6;
            this.cbIncludeColors.Text = " Colors?";
            this.cbIncludeColors.UseVisualStyleBackColor = true;
            // 
            // cbAscii
            // 
            this.cbAscii.AutoSize = true;
            this.cbAscii.Location = new System.Drawing.Point(84, 49);
            this.cbAscii.Name = "cbAscii";
            this.cbAscii.Size = new System.Drawing.Size(53, 17);
            this.cbAscii.TabIndex = 6;
            this.cbAscii.Text = "ASCII";
            this.cbAscii.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label14.Dock = System.Windows.Forms.DockStyle.Top;
            this.label14.Location = new System.Drawing.Point(0, 162);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(150, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Stats";
            // 
            // tbStats
            // 
            this.tbStats.BackColor = System.Drawing.Color.White;
            this.tbStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbStats.Location = new System.Drawing.Point(0, 175);
            this.tbStats.Name = "tbStats";
            this.tbStats.ReadOnly = true;
            this.tbStats.Size = new System.Drawing.Size(150, 13);
            this.tbStats.TabIndex = 12;
            // 
            // MeshGeneratorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(150, 339);
            this.Controls.Add(this.tbStats);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Name = "MeshGeneratorWindow";
            this.Text = "Generate .ply mesh";
            this.TopMost = true;
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnShowCoverage;
        private System.Windows.Forms.Button btnWriteMesh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbZoom;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox cbIncludeColors;
        private System.Windows.Forms.CheckBox cbAscii;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbStats;
    }
}