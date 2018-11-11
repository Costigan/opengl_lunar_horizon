namespace lunar_horizon.view
{
    partial class PatchCompare
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
            this.lbPatches = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbHorizon = new System.Windows.Forms.RadioButton();
            this.rbDelta = new System.Windows.Forms.RadioButton();
            this.rbSecond = new System.Windows.Forms.RadioButton();
            this.rbFirst = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbMaxDelta = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSelectTime = new System.Windows.Forms.TrackBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbTimeSpan = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.btnUpdateStart = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPatches
            // 
            this.lbPatches.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbPatches.FormattingEnabled = true;
            this.lbPatches.Location = new System.Drawing.Point(0, 155);
            this.lbPatches.Name = "lbPatches";
            this.lbPatches.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbPatches.Size = new System.Drawing.Size(172, 173);
            this.lbPatches.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(172, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 624);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.lbPatches);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(172, 624);
            this.panel2.TabIndex = 3;
            // 
            // btnLoad
            // 
            this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLoad.Location = new System.Drawing.Point(0, 328);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(172, 23);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Available Patches";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbHorizon);
            this.panel3.Controls.Add(this.rbDelta);
            this.panel3.Controls.Add(this.rbSecond);
            this.panel3.Controls.Add(this.rbFirst);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(172, 123);
            this.panel3.TabIndex = 2;
            // 
            // rbHorizon
            // 
            this.rbHorizon.AutoSize = true;
            this.rbHorizon.Location = new System.Drawing.Point(12, 72);
            this.rbHorizon.Name = "rbHorizon";
            this.rbHorizon.Size = new System.Drawing.Size(89, 17);
            this.rbHorizon.TabIndex = 0;
            this.rbHorizon.Text = "Horizon Delta";
            this.rbHorizon.UseVisualStyleBackColor = true;
            this.rbHorizon.Click += new System.EventHandler(this.rbHorizon_Click);
            // 
            // rbDelta
            // 
            this.rbDelta.AutoSize = true;
            this.rbDelta.Checked = true;
            this.rbDelta.Location = new System.Drawing.Point(12, 49);
            this.rbDelta.Name = "rbDelta";
            this.rbDelta.Size = new System.Drawing.Size(82, 17);
            this.rbDelta.TabIndex = 0;
            this.rbDelta.TabStop = true;
            this.rbDelta.Text = "Image Delta";
            this.rbDelta.UseVisualStyleBackColor = true;
            this.rbDelta.Click += new System.EventHandler(this.rbDelta_Click);
            // 
            // rbSecond
            // 
            this.rbSecond.AutoSize = true;
            this.rbSecond.Location = new System.Drawing.Point(12, 26);
            this.rbSecond.Name = "rbSecond";
            this.rbSecond.Size = new System.Drawing.Size(92, 17);
            this.rbSecond.TabIndex = 0;
            this.rbSecond.Text = "Second patch";
            this.rbSecond.UseVisualStyleBackColor = true;
            this.rbSecond.Click += new System.EventHandler(this.rbSecond_Click);
            // 
            // rbFirst
            // 
            this.rbFirst.AutoSize = true;
            this.rbFirst.Location = new System.Drawing.Point(12, 3);
            this.rbFirst.Name = "rbFirst";
            this.rbFirst.Size = new System.Drawing.Size(74, 17);
            this.rbFirst.TabIndex = 0;
            this.rbFirst.Text = "First patch";
            this.rbFirst.UseVisualStyleBackColor = true;
            this.rbFirst.Click += new System.EventHandler(this.rbFirst_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "What to show";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbMaxDelta);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.tbSelectTime);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(175, 538);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(551, 86);
            this.panel4.TabIndex = 4;
            // 
            // lbMaxDelta
            // 
            this.lbMaxDelta.Location = new System.Drawing.Point(355, 54);
            this.lbMaxDelta.Name = "lbMaxDelta";
            this.lbMaxDelta.Size = new System.Drawing.Size(47, 19);
            this.lbMaxDelta.TabIndex = 4;
            this.lbMaxDelta.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "max delta:";
            // 
            // tbSelectTime
            // 
            this.tbSelectTime.BackColor = System.Drawing.Color.White;
            this.tbSelectTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSelectTime.LargeChange = 50;
            this.tbSelectTime.Location = new System.Drawing.Point(288, 0);
            this.tbSelectTime.Maximum = 10000;
            this.tbSelectTime.Name = "tbSelectTime";
            this.tbSelectTime.Size = new System.Drawing.Size(263, 45);
            this.tbSelectTime.TabIndex = 1;
            this.tbSelectTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSelectTime.Scroll += new System.EventHandler(this.tbSelectTime_Scroll);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnUpdateStart);
            this.panel5.Controls.Add(this.cbTimeSpan);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.dtStartTime);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.lbCurrentTime);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(288, 86);
            this.panel5.TabIndex = 2;
            // 
            // cbTimeSpan
            // 
            this.cbTimeSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.cbTimeSpan.Size = new System.Drawing.Size(111, 21);
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
            // btnUpdateStart
            // 
            this.btnUpdateStart.Location = new System.Drawing.Point(198, 54);
            this.btnUpdateStart.Name = "btnUpdateStart";
            this.btnUpdateStart.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateStart.TabIndex = 4;
            this.btnUpdateStart.Text = "Update";
            this.btnUpdateStart.UseVisualStyleBackColor = true;
            this.btnUpdateStart.Click += new System.EventHandler(this.btnUpdateStart_Click);
            // 
            // PatchCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 624);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Name = "PatchCompare";
            this.Text = "PatchCompare";
            this.Load += new System.EventHandler(this.PatchCompare_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbPatches;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbDelta;
        private System.Windows.Forms.RadioButton rbSecond;
        private System.Windows.Forms.RadioButton rbFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TrackBar tbSelectTime;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ComboBox cbTimeSpan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.RadioButton rbHorizon;
        private System.Windows.Forms.Label lbMaxDelta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdateStart;
    }
}