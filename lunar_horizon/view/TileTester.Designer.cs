namespace lunar_horizon.view
{
    partial class TileTester
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
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hillshadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillTest1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchForBug1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.cbWhichHorizon = new System.Windows.Forms.ComboBox();
            this.btnCompare = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cbGpuSelfShadow = new System.Windows.Forms.CheckBox();
            this.btnGpuCalculate = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbUseCache = new System.Windows.Forms.CheckBox();
            this.btnWriteCache = new System.Windows.Forms.Button();
            this.btnClearCache = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cbCasterRender = new System.Windows.Forms.CheckBox();
            this.cbSelfShadow = new System.Windows.Forms.CheckBox();
            this.btnShadow2To1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPlotMode = new System.Windows.Forms.Button();
            this.btnSelect2 = new System.Windows.Forms.Button();
            this.btnSelect1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.tbAzimuth = new System.Windows.Forms.TrackBar();
            this.tbElevation = new System.Windows.Forms.TrackBar();
            this.panel6 = new System.Windows.Forms.Panel();
            this.tbSelectTime = new System.Windows.Forms.TrackBar();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbTimeSpan = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.pnlShadowCaster = new System.Windows.Forms.Panel();
            this.cbShadowCaster = new System.Windows.Forms.CheckBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAzimuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbElevation)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1285, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hillshadeToolStripMenuItem,
            this.shadowsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // hillshadeToolStripMenuItem
            // 
            this.hillshadeToolStripMenuItem.Name = "hillshadeToolStripMenuItem";
            this.hillshadeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.hillshadeToolStripMenuItem.Text = "Hillshade";
            // 
            // shadowsToolStripMenuItem
            // 
            this.shadowsToolStripMenuItem.Name = "shadowsToolStripMenuItem";
            this.shadowsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.shadowsToolStripMenuItem.Text = "Shadows";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillTest1ToolStripMenuItem,
            this.searchForBug1ToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // fillTest1ToolStripMenuItem
            // 
            this.fillTest1ToolStripMenuItem.Name = "fillTest1ToolStripMenuItem";
            this.fillTest1ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.fillTest1ToolStripMenuItem.Text = "&Fill Test 1";
            this.fillTest1ToolStripMenuItem.Click += new System.EventHandler(this.fillTest1ToolStripMenuItem_Click);
            // 
            // searchForBug1ToolStripMenuItem
            // 
            this.searchForBug1ToolStripMenuItem.Name = "searchForBug1ToolStripMenuItem";
            this.searchForBug1ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.searchForBug1ToolStripMenuItem.Text = "Search for bug 1";
            this.searchForBug1ToolStripMenuItem.Click += new System.EventHandler(this.searchForBug1ToolStripMenuItem_Click);
            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(110, 24);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(972, 406);
            this.pnlScroll.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel11);
            this.panel2.Controls.Add(this.panel10);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(107, 570);
            this.panel2.TabIndex = 3;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.LightBlue;
            this.panel11.Controls.Add(this.cbWhichHorizon);
            this.panel11.Controls.Add(this.btnCompare);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 389);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(107, 80);
            this.panel11.TabIndex = 10;
            // 
            // cbWhichHorizon
            // 
            this.cbWhichHorizon.FormattingEnabled = true;
            this.cbWhichHorizon.Items.AddRange(new object[] {
            "Target",
            "CPU",
            "GPU",
            "Delta"});
            this.cbWhichHorizon.Location = new System.Drawing.Point(12, 35);
            this.cbWhichHorizon.Name = "cbWhichHorizon";
            this.cbWhichHorizon.Size = new System.Drawing.Size(79, 21);
            this.cbWhichHorizon.TabIndex = 3;
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(12, 6);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 2;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.LightBlue;
            this.panel10.Controls.Add(this.cbGpuSelfShadow);
            this.panel10.Controls.Add(this.btnGpuCalculate);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 309);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(107, 80);
            this.panel10.TabIndex = 9;
            // 
            // cbGpuSelfShadow
            // 
            this.cbGpuSelfShadow.AutoSize = true;
            this.cbGpuSelfShadow.Checked = true;
            this.cbGpuSelfShadow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGpuSelfShadow.Location = new System.Drawing.Point(12, 57);
            this.cbGpuSelfShadow.Name = "cbGpuSelfShadow";
            this.cbGpuSelfShadow.Size = new System.Drawing.Size(86, 17);
            this.cbGpuSelfShadow.TabIndex = 1;
            this.cbGpuSelfShadow.Text = "Self Shadow";
            this.cbGpuSelfShadow.UseVisualStyleBackColor = true;
            // 
            // btnGpuCalculate
            // 
            this.btnGpuCalculate.Location = new System.Drawing.Point(12, 6);
            this.btnGpuCalculate.Name = "btnGpuCalculate";
            this.btnGpuCalculate.Size = new System.Drawing.Size(75, 45);
            this.btnGpuCalculate.TabIndex = 0;
            this.btnGpuCalculate.Text = "GPU Calculate";
            this.btnGpuCalculate.UseVisualStyleBackColor = true;
            this.btnGpuCalculate.Click += new System.EventHandler(this.btnGpuCalculate_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.Controls.Add(this.cbUseCache);
            this.panel5.Controls.Add(this.btnWriteCache);
            this.panel5.Controls.Add(this.btnClearCache);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 226);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(107, 83);
            this.panel5.TabIndex = 8;
            // 
            // cbUseCache
            // 
            this.cbUseCache.AutoSize = true;
            this.cbUseCache.Location = new System.Drawing.Point(12, 61);
            this.cbUseCache.Name = "cbUseCache";
            this.cbUseCache.Size = new System.Drawing.Size(79, 17);
            this.cbUseCache.TabIndex = 2;
            this.cbUseCache.Text = "Use Cache";
            this.cbUseCache.UseVisualStyleBackColor = true;
            // 
            // btnWriteCache
            // 
            this.btnWriteCache.Location = new System.Drawing.Point(12, 32);
            this.btnWriteCache.Name = "btnWriteCache";
            this.btnWriteCache.Size = new System.Drawing.Size(75, 23);
            this.btnWriteCache.TabIndex = 1;
            this.btnWriteCache.Text = "Write Cache";
            this.btnWriteCache.UseVisualStyleBackColor = true;
            this.btnWriteCache.Click += new System.EventHandler(this.btnWriteCache_Click);
            // 
            // btnClearCache
            // 
            this.btnClearCache.Location = new System.Drawing.Point(12, 3);
            this.btnClearCache.Name = "btnClearCache";
            this.btnClearCache.Size = new System.Drawing.Size(75, 23);
            this.btnClearCache.TabIndex = 0;
            this.btnClearCache.Text = "Clear Cache";
            this.btnClearCache.UseVisualStyleBackColor = true;
            this.btnClearCache.Click += new System.EventHandler(this.btnClearCache_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.PowderBlue;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Shadow Cache";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.Controls.Add(this.cbCasterRender);
            this.panel4.Controls.Add(this.cbSelfShadow);
            this.panel4.Controls.Add(this.btnShadow2To1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 132);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(107, 78);
            this.panel4.TabIndex = 6;
            // 
            // cbCasterRender
            // 
            this.cbCasterRender.AutoSize = true;
            this.cbCasterRender.Location = new System.Drawing.Point(12, 55);
            this.cbCasterRender.Name = "cbCasterRender";
            this.cbCasterRender.Size = new System.Drawing.Size(94, 17);
            this.cbCasterRender.TabIndex = 3;
            this.cbCasterRender.Text = "Caster Render";
            this.cbCasterRender.UseVisualStyleBackColor = true;
            // 
            // cbSelfShadow
            // 
            this.cbSelfShadow.AutoSize = true;
            this.cbSelfShadow.Location = new System.Drawing.Point(12, 32);
            this.cbSelfShadow.Name = "cbSelfShadow";
            this.cbSelfShadow.Size = new System.Drawing.Size(86, 17);
            this.cbSelfShadow.TabIndex = 1;
            this.cbSelfShadow.Text = "Self Shadow";
            this.cbSelfShadow.UseVisualStyleBackColor = true;
            // 
            // btnShadow2To1
            // 
            this.btnShadow2To1.Location = new System.Drawing.Point(12, 3);
            this.btnShadow2To1.Name = "btnShadow2To1";
            this.btnShadow2To1.Size = new System.Drawing.Size(75, 23);
            this.btnShadow2To1.TabIndex = 0;
            this.btnShadow2To1.Text = "Calculate";
            this.btnShadow2To1.UseVisualStyleBackColor = true;
            this.btnShadow2To1.Click += new System.EventHandler(this.btnShadow2To1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.PowderBlue;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Shadows";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.Controls.Add(this.btnPlotMode);
            this.panel3.Controls.Add(this.btnSelect2);
            this.panel3.Controls.Add(this.btnSelect1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(107, 100);
            this.panel3.TabIndex = 4;
            // 
            // btnPlotMode
            // 
            this.btnPlotMode.Location = new System.Drawing.Point(12, 61);
            this.btnPlotMode.Name = "btnPlotMode";
            this.btnPlotMode.Size = new System.Drawing.Size(75, 23);
            this.btnPlotMode.TabIndex = 0;
            this.btnPlotMode.Text = "Plot Horizon";
            this.btnPlotMode.UseVisualStyleBackColor = true;
            this.btnPlotMode.Click += new System.EventHandler(this.btnPlotMode_Click);
            // 
            // btnSelect2
            // 
            this.btnSelect2.Location = new System.Drawing.Point(12, 32);
            this.btnSelect2.Name = "btnSelect2";
            this.btnSelect2.Size = new System.Drawing.Size(75, 23);
            this.btnSelect2.TabIndex = 0;
            this.btnSelect2.Text = "Select From";
            this.btnSelect2.UseVisualStyleBackColor = true;
            this.btnSelect2.Click += new System.EventHandler(this.btnSelect2_Click);
            // 
            // btnSelect1
            // 
            this.btnSelect1.Location = new System.Drawing.Point(12, 3);
            this.btnSelect1.Name = "btnSelect1";
            this.btnSelect1.Size = new System.Drawing.Size(75, 23);
            this.btnSelect1.TabIndex = 0;
            this.btnSelect1.Text = "Select To";
            this.btnSelect1.UseVisualStyleBackColor = true;
            this.btnSelect1.Click += new System.EventHandler(this.btnSelect1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.PowderBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mouse";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(107, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 570);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(110, 430);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1175, 164);
            this.panel1.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.tbAzimuth);
            this.panel8.Controls.Add(this.tbElevation);
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(914, 71);
            this.panel8.TabIndex = 10;
            // 
            // tbAzimuth
            // 
            this.tbAzimuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbAzimuth.Location = new System.Drawing.Point(0, 0);
            this.tbAzimuth.Maximum = 359;
            this.tbAzimuth.Name = "tbAzimuth";
            this.tbAzimuth.Size = new System.Drawing.Size(869, 71);
            this.tbAzimuth.TabIndex = 8;
            this.tbAzimuth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbAzimuth.Value = 180;
            this.tbAzimuth.ValueChanged += new System.EventHandler(this.tbAzimuth_ValueChanged);
            // 
            // tbElevation
            // 
            this.tbElevation.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbElevation.Location = new System.Drawing.Point(869, 0);
            this.tbElevation.Maximum = 100;
            this.tbElevation.Name = "tbElevation";
            this.tbElevation.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbElevation.Size = new System.Drawing.Size(45, 71);
            this.tbElevation.TabIndex = 0;
            this.tbElevation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbElevation.ValueChanged += new System.EventHandler(this.tbElevation_ValueChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.tbSelectTime);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 78);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1175, 86);
            this.panel6.TabIndex = 9;
            // 
            // tbSelectTime
            // 
            this.tbSelectTime.BackColor = System.Drawing.Color.White;
            this.tbSelectTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSelectTime.LargeChange = 50;
            this.tbSelectTime.Location = new System.Drawing.Point(288, 0);
            this.tbSelectTime.Maximum = 10000;
            this.tbSelectTime.Name = "tbSelectTime";
            this.tbSelectTime.Size = new System.Drawing.Size(887, 45);
            this.tbSelectTime.TabIndex = 1;
            this.tbSelectTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSelectTime.Scroll += new System.EventHandler(this.tbSelectTime_Scroll);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.cbTimeSpan);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.dtStartTime);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.lbCurrentTime);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(288, 86);
            this.panel7.TabIndex = 2;
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
            // panel9
            // 
            this.panel9.Controls.Add(this.pnlShadowCaster);
            this.panel9.Controls.Add(this.cbShadowCaster);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(1085, 24);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(200, 406);
            this.panel9.TabIndex = 7;
            // 
            // pnlShadowCaster
            // 
            this.pnlShadowCaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlShadowCaster.Location = new System.Drawing.Point(0, 17);
            this.pnlShadowCaster.Name = "pnlShadowCaster";
            this.pnlShadowCaster.Size = new System.Drawing.Size(200, 389);
            this.pnlShadowCaster.TabIndex = 5;
            // 
            // cbShadowCaster
            // 
            this.cbShadowCaster.AutoSize = true;
            this.cbShadowCaster.BackColor = System.Drawing.Color.PowderBlue;
            this.cbShadowCaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbShadowCaster.Location = new System.Drawing.Point(0, 0);
            this.cbShadowCaster.Name = "cbShadowCaster";
            this.cbShadowCaster.Size = new System.Drawing.Size(200, 17);
            this.cbShadowCaster.TabIndex = 6;
            this.cbShadowCaster.Text = "Shadow Caster";
            this.cbShadowCaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbShadowCaster.UseVisualStyleBackColor = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(1082, 24);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 406);
            this.splitter2.TabIndex = 8;
            this.splitter2.TabStop = false;
            // 
            // TileTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 594);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TileTester";
            this.Text = "TileTester";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAzimuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbElevation)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hillshadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowsToolStripMenuItem;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btnSelect2;
        private System.Windows.Forms.Button btnSelect1;
        private System.Windows.Forms.Button btnShadow2To1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar tbAzimuth;
        private System.Windows.Forms.TrackBar tbElevation;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnClearCache;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnWriteCache;
        private System.Windows.Forms.CheckBox cbUseCache;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TrackBar tbSelectTime;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox cbTimeSpan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.CheckBox cbSelfShadow;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillTest1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForBug1ToolStripMenuItem;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel pnlShadowCaster;
        private System.Windows.Forms.CheckBox cbShadowCaster;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.CheckBox cbCasterRender;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox cbGpuSelfShadow;
        private System.Windows.Forms.Button btnGpuCalculate;
        private System.Windows.Forms.Button btnPlotMode;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.ComboBox cbWhichHorizon;
    }
}