namespace lunar_horizon.viz
{
    partial class MainView
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
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLatLon = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.cbTimeSpan = new System.Windows.Forms.ComboBox();
            this.cbUpdateContinuously = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSelectTime = new System.Windows.Forms.TrackBar();
            this.pnlPlot = new System.Windows.Forms.Panel();
            this.tabLeft = new System.Windows.Forms.TabControl();
            this.tabLayers = new System.Windows.Forms.TabPage();
            this.lvLayers = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTransparency = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbTransparency = new System.Windows.Forms.TrackBar();
            this.tabGenerator = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbObserverHeight = new System.Windows.Forms.TextBox();
            this.btnShowShadowcasters = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btnCasterRenderPoint = new System.Windows.Forms.Button();
            this.cbProcessor = new System.Windows.Forms.ComboBox();
            this.cbSurroundings = new System.Windows.Forms.ComboBox();
            this.btnProcessShadowQueue = new System.Windows.Forms.Button();
            this.cbCenter = new System.Windows.Forms.CheckBox();
            this.btnGPU4 = new System.Windows.Forms.Button();
            this.btnGPU1 = new System.Windows.Forms.Button();
            this.btnGPU3 = new System.Windows.Forms.Button();
            this.btnGPU2 = new System.Windows.Forms.Button();
            this.tabRight = new System.Windows.Forms.TabControl();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.pnlPropertiesHolder = new System.Windows.Forms.Panel();
            this.tabImageLibrary = new System.Windows.Forms.TabPage();
            this.lvImageLibrary = new System.Windows.Forms.ListView();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.tabCenter = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).BeginInit();
            this.tabLeft.SuspendLayout();
            this.tabLayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).BeginInit();
            this.tabGenerator.SuspendLayout();
            this.tabRight.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.tabImageLibrary.SuspendLayout();
            this.tabCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.tilesToolStripMenuItem,
            this.testingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1302, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "&Map";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // tilesToolStripMenuItem
            // 
            this.tilesToolStripMenuItem.Name = "tilesToolStripMenuItem";
            this.tilesToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tilesToolStripMenuItem.Text = "&Tiles";
            // 
            // testingToolStripMenuItem
            // 
            this.testingToolStripMenuItem.Name = "testingToolStripMenuItem";
            this.testingToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.testingToolStripMenuItem.Text = "&Testing";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1302, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLatLon});
            this.statusStrip1.Location = new System.Drawing.Point(0, 689);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1302, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLatLon
            // 
            this.lblLatLon.AutoSize = false;
            this.lblLatLon.Name = "lblLatLon";
            this.lblLatLon.Size = new System.Drawing.Size(600, 17);
            this.lblLatLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTime
            // 
            this.pnlTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTime.Controls.Add(this.lbCurrentTime);
            this.pnlTime.Controls.Add(this.cbTimeSpan);
            this.pnlTime.Controls.Add(this.cbUpdateContinuously);
            this.pnlTime.Controls.Add(this.label7);
            this.pnlTime.Controls.Add(this.label6);
            this.pnlTime.Controls.Add(this.dtStartTime);
            this.pnlTime.Controls.Add(this.label5);
            this.pnlTime.Controls.Add(this.tbSelectTime);
            this.pnlTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTime.Location = new System.Drawing.Point(0, 635);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(1302, 54);
            this.pnlTime.TabIndex = 17;
            // 
            // lbCurrentTime
            // 
            this.lbCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentTime.Location = new System.Drawing.Point(47, 24);
            this.lbCurrentTime.Name = "lbCurrentTime";
            this.lbCurrentTime.Size = new System.Drawing.Size(203, 17);
            this.lbCurrentTime.TabIndex = 1;
            this.lbCurrentTime.Text = "<current time>\r\n";
            this.lbCurrentTime.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cbTimeSpan
            // 
            this.cbTimeSpan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTimeSpan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTimeSpan.FormattingEnabled = true;
            this.cbTimeSpan.Items.AddRange(new object[] {
            "3 Months",
            "2 Months",
            "1 Month",
            "2 weeks",
            "1 week",
            "1 day",
            "1 hour"});
            this.cbTimeSpan.Location = new System.Drawing.Point(612, 25);
            this.cbTimeSpan.Name = "cbTimeSpan";
            this.cbTimeSpan.Size = new System.Drawing.Size(117, 21);
            this.cbTimeSpan.TabIndex = 3;
            // 
            // cbUpdateContinuously
            // 
            this.cbUpdateContinuously.AutoSize = true;
            this.cbUpdateContinuously.Checked = true;
            this.cbUpdateContinuously.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdateContinuously.Location = new System.Drawing.Point(735, 27);
            this.cbUpdateContinuously.Name = "cbUpdateContinuously";
            this.cbUpdateContinuously.Size = new System.Drawing.Size(124, 17);
            this.cbUpdateContinuously.TabIndex = 3;
            this.cbUpdateContinuously.Text = "Update Continuously";
            this.cbUpdateContinuously.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(556, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Duration:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Interval Start:";
            // 
            // dtStartTime
            // 
            this.dtStartTime.Location = new System.Drawing.Point(350, 25);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Size = new System.Drawing.Size(200, 20);
            this.dtStartTime.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Time:";
            // 
            // tbSelectTime
            // 
            this.tbSelectTime.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSelectTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbSelectTime.LargeChange = 50;
            this.tbSelectTime.Location = new System.Drawing.Point(0, 0);
            this.tbSelectTime.Maximum = 10000;
            this.tbSelectTime.Name = "tbSelectTime";
            this.tbSelectTime.Size = new System.Drawing.Size(1302, 45);
            this.tbSelectTime.TabIndex = 1;
            this.tbSelectTime.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // pnlPlot
            // 
            this.pnlPlot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlot.Location = new System.Drawing.Point(0, 525);
            this.pnlPlot.Name = "pnlPlot";
            this.pnlPlot.Size = new System.Drawing.Size(1302, 110);
            this.pnlPlot.TabIndex = 20;
            // 
            // tabLeft
            // 
            this.tabLeft.Controls.Add(this.tabLayers);
            this.tabLeft.Controls.Add(this.tabGenerator);
            this.tabLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabLeft.Location = new System.Drawing.Point(0, 49);
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.SelectedIndex = 0;
            this.tabLeft.Size = new System.Drawing.Size(219, 473);
            this.tabLeft.TabIndex = 21;
            // 
            // tabLayers
            // 
            this.tabLayers.Controls.Add(this.lvLayers);
            this.tabLayers.Controls.Add(this.tbTransparency);
            this.tabLayers.Location = new System.Drawing.Point(4, 22);
            this.tabLayers.Name = "tabLayers";
            this.tabLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayers.Size = new System.Drawing.Size(211, 447);
            this.tabLayers.TabIndex = 0;
            this.tabLayers.Text = "Layers";
            this.tabLayers.UseVisualStyleBackColor = true;
            // 
            // lvLayers
            // 
            this.lvLayers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvLayers.CheckBoxes = true;
            this.lvLayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colTransparency});
            this.lvLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvLayers.FullRowSelect = true;
            this.lvLayers.GridLines = true;
            this.lvLayers.HideSelection = false;
            this.lvLayers.Location = new System.Drawing.Point(3, 3);
            this.lvLayers.MultiSelect = false;
            this.lvLayers.Name = "lvLayers";
            this.lvLayers.ShowGroups = false;
            this.lvLayers.Size = new System.Drawing.Size(205, 396);
            this.lvLayers.TabIndex = 3;
            this.lvLayers.UseCompatibleStateImageBehavior = false;
            this.lvLayers.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 160;
            // 
            // colTransparency
            // 
            this.colTransparency.Text = " % ";
            this.colTransparency.Width = 40;
            // 
            // tbTransparency
            // 
            this.tbTransparency.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbTransparency.Location = new System.Drawing.Point(3, 399);
            this.tbTransparency.Maximum = 100;
            this.tbTransparency.Name = "tbTransparency";
            this.tbTransparency.Size = new System.Drawing.Size(205, 45);
            this.tbTransparency.TabIndex = 2;
            this.tbTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // tabGenerator
            // 
            this.tabGenerator.Controls.Add(this.label8);
            this.tabGenerator.Controls.Add(this.label4);
            this.tabGenerator.Controls.Add(this.tbObserverHeight);
            this.tabGenerator.Controls.Add(this.btnShowShadowcasters);
            this.tabGenerator.Controls.Add(this.lbStatus);
            this.tabGenerator.Controls.Add(this.btnCasterRenderPoint);
            this.tabGenerator.Controls.Add(this.cbProcessor);
            this.tabGenerator.Controls.Add(this.cbSurroundings);
            this.tabGenerator.Controls.Add(this.btnProcessShadowQueue);
            this.tabGenerator.Controls.Add(this.cbCenter);
            this.tabGenerator.Controls.Add(this.btnGPU4);
            this.tabGenerator.Controls.Add(this.btnGPU1);
            this.tabGenerator.Controls.Add(this.btnGPU3);
            this.tabGenerator.Controls.Add(this.btnGPU2);
            this.tabGenerator.Location = new System.Drawing.Point(4, 22);
            this.tabGenerator.Name = "tabGenerator";
            this.tabGenerator.Padding = new System.Windows.Forms.Padding(3);
            this.tabGenerator.Size = new System.Drawing.Size(211, 525);
            this.tabGenerator.TabIndex = 1;
            this.tabGenerator.Text = "Generator";
            this.tabGenerator.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Observer Height";
            // 
            // tbObserverHeight
            // 
            this.tbObserverHeight.Location = new System.Drawing.Point(10, 119);
            this.tbObserverHeight.Name = "tbObserverHeight";
            this.tbObserverHeight.Size = new System.Drawing.Size(51, 20);
            this.tbObserverHeight.TabIndex = 17;
            this.tbObserverHeight.Text = "0";
            // 
            // btnShowShadowcasters
            // 
            this.btnShowShadowcasters.Location = new System.Drawing.Point(8, 456);
            this.btnShowShadowcasters.Name = "btnShowShadowcasters";
            this.btnShowShadowcasters.Size = new System.Drawing.Size(75, 23);
            this.btnShowShadowcasters.TabIndex = 12;
            this.btnShowShadowcasters.Text = "Shadowcasters";
            this.btnShowShadowcasters.UseVisualStyleBackColor = true;
            // 
            // lbStatus
            // 
            this.lbStatus.Location = new System.Drawing.Point(10, 195);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(69, 18);
            this.lbStatus.TabIndex = 15;
            this.lbStatus.Text = "Idle";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCasterRenderPoint
            // 
            this.btnCasterRenderPoint.Location = new System.Drawing.Point(8, 427);
            this.btnCasterRenderPoint.Name = "btnCasterRenderPoint";
            this.btnCasterRenderPoint.Size = new System.Drawing.Size(75, 23);
            this.btnCasterRenderPoint.TabIndex = 12;
            this.btnCasterRenderPoint.Text = "Render Pt";
            this.btnCasterRenderPoint.UseVisualStyleBackColor = true;
            // 
            // cbProcessor
            // 
            this.cbProcessor.FormattingEnabled = true;
            this.cbProcessor.Items.AddRange(new object[] {
            "CPU",
            "GPU",
            "Combined"});
            this.cbProcessor.Location = new System.Drawing.Point(8, 6);
            this.cbProcessor.Name = "cbProcessor";
            this.cbProcessor.Size = new System.Drawing.Size(75, 21);
            this.cbProcessor.TabIndex = 16;
            // 
            // cbSurroundings
            // 
            this.cbSurroundings.FormattingEnabled = true;
            this.cbSurroundings.Items.AddRange(new object[] {
            "No Surroundings",
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20",
            "30",
            "40",
            "50",
            "All Surroundings"});
            this.cbSurroundings.Location = new System.Drawing.Point(10, 168);
            this.cbSurroundings.Name = "cbSurroundings";
            this.cbSurroundings.Size = new System.Drawing.Size(69, 21);
            this.cbSurroundings.TabIndex = 14;
            // 
            // btnProcessShadowQueue
            // 
            this.btnProcessShadowQueue.Location = new System.Drawing.Point(8, 28);
            this.btnProcessShadowQueue.Name = "btnProcessShadowQueue";
            this.btnProcessShadowQueue.Size = new System.Drawing.Size(75, 65);
            this.btnProcessShadowQueue.TabIndex = 8;
            this.btnProcessShadowQueue.Text = "Process Shadow Queue";
            this.btnProcessShadowQueue.UseVisualStyleBackColor = true;
            // 
            // cbCenter
            // 
            this.cbCenter.AutoSize = true;
            this.cbCenter.Checked = true;
            this.cbCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCenter.Location = new System.Drawing.Point(10, 145);
            this.cbCenter.Name = "cbCenter";
            this.cbCenter.Size = new System.Drawing.Size(63, 17);
            this.cbCenter.TabIndex = 13;
            this.cbCenter.Text = "Center?";
            this.cbCenter.UseVisualStyleBackColor = true;
            // 
            // btnGPU4
            // 
            this.btnGPU4.Location = new System.Drawing.Point(10, 303);
            this.btnGPU4.Name = "btnGPU4";
            this.btnGPU4.Size = new System.Drawing.Size(75, 23);
            this.btnGPU4.TabIndex = 11;
            this.btnGPU4.Text = "GPU 4";
            this.btnGPU4.UseVisualStyleBackColor = true;
            // 
            // btnGPU1
            // 
            this.btnGPU1.Location = new System.Drawing.Point(10, 216);
            this.btnGPU1.Name = "btnGPU1";
            this.btnGPU1.Size = new System.Drawing.Size(75, 23);
            this.btnGPU1.TabIndex = 9;
            this.btnGPU1.Text = "GPU 1";
            this.btnGPU1.UseVisualStyleBackColor = true;
            // 
            // btnGPU3
            // 
            this.btnGPU3.Location = new System.Drawing.Point(10, 274);
            this.btnGPU3.Name = "btnGPU3";
            this.btnGPU3.Size = new System.Drawing.Size(75, 23);
            this.btnGPU3.TabIndex = 11;
            this.btnGPU3.Text = "GPU 3";
            this.btnGPU3.UseVisualStyleBackColor = true;
            // 
            // btnGPU2
            // 
            this.btnGPU2.Location = new System.Drawing.Point(10, 245);
            this.btnGPU2.Name = "btnGPU2";
            this.btnGPU2.Size = new System.Drawing.Size(75, 23);
            this.btnGPU2.TabIndex = 10;
            this.btnGPU2.Text = "GPU 2";
            this.btnGPU2.UseVisualStyleBackColor = true;
            // 
            // tabRight
            // 
            this.tabRight.Controls.Add(this.tabProperties);
            this.tabRight.Controls.Add(this.tabImageLibrary);
            this.tabRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabRight.Location = new System.Drawing.Point(1102, 49);
            this.tabRight.Name = "tabRight";
            this.tabRight.SelectedIndex = 0;
            this.tabRight.Size = new System.Drawing.Size(200, 473);
            this.tabRight.TabIndex = 22;
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.pnlPropertiesHolder);
            this.tabProperties.Location = new System.Drawing.Point(4, 22);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabProperties.Size = new System.Drawing.Size(192, 447);
            this.tabProperties.TabIndex = 0;
            this.tabProperties.Text = "Properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            // 
            // pnlPropertiesHolder
            // 
            this.pnlPropertiesHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPropertiesHolder.Location = new System.Drawing.Point(3, 3);
            this.pnlPropertiesHolder.Name = "pnlPropertiesHolder";
            this.pnlPropertiesHolder.Size = new System.Drawing.Size(186, 441);
            this.pnlPropertiesHolder.TabIndex = 0;
            // 
            // tabImageLibrary
            // 
            this.tabImageLibrary.Controls.Add(this.lvImageLibrary);
            this.tabImageLibrary.Location = new System.Drawing.Point(4, 22);
            this.tabImageLibrary.Name = "tabImageLibrary";
            this.tabImageLibrary.Padding = new System.Windows.Forms.Padding(3);
            this.tabImageLibrary.Size = new System.Drawing.Size(192, 525);
            this.tabImageLibrary.TabIndex = 1;
            this.tabImageLibrary.Text = "Image Library";
            this.tabImageLibrary.UseVisualStyleBackColor = true;
            // 
            // lvImageLibrary
            // 
            this.lvImageLibrary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvImageLibrary.Location = new System.Drawing.Point(3, 3);
            this.lvImageLibrary.Name = "lvImageLibrary";
            this.lvImageLibrary.Size = new System.Drawing.Size(186, 519);
            this.lvImageLibrary.TabIndex = 0;
            this.lvImageLibrary.UseCompatibleStateImageBehavior = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 522);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1302, 3);
            this.splitter3.TabIndex = 23;
            this.splitter3.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(219, 49);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 473);
            this.splitter1.TabIndex = 24;
            this.splitter1.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(1099, 49);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 473);
            this.splitter4.TabIndex = 25;
            this.splitter4.TabStop = false;
            // 
            // tabCenter
            // 
            this.tabCenter.Controls.Add(this.tabMap);
            this.tabCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCenter.Location = new System.Drawing.Point(222, 49);
            this.tabCenter.Name = "tabCenter";
            this.tabCenter.SelectedIndex = 0;
            this.tabCenter.Size = new System.Drawing.Size(877, 473);
            this.tabCenter.TabIndex = 26;
            // 
            // tabMap
            // 
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(869, 447);
            this.tabMap.TabIndex = 0;
            this.tabMap.Text = "Map";
            this.tabMap.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 711);
            this.Controls.Add(this.tabCenter);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabRight);
            this.Controls.Add(this.tabLeft);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.pnlPlot);
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainView";
            this.Text = "MainView";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).EndInit();
            this.tabLeft.ResumeLayout(false);
            this.tabLayers.ResumeLayout(false);
            this.tabLayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).EndInit();
            this.tabGenerator.ResumeLayout(false);
            this.tabGenerator.PerformLayout();
            this.tabRight.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.tabImageLibrary.ResumeLayout(false);
            this.tabCenter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testingToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLatLon;
        private System.Windows.Forms.Panel pnlTime;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.ComboBox cbTimeSpan;
        private System.Windows.Forms.CheckBox cbUpdateContinuously;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbSelectTime;
        private System.Windows.Forms.Panel pnlPlot;
        private System.Windows.Forms.TabControl tabLeft;
        private System.Windows.Forms.TabPage tabLayers;
        internal System.Windows.Forms.ListView lvLayers;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colTransparency;
        private System.Windows.Forms.TrackBar tbTransparency;
        private System.Windows.Forms.TabPage tabGenerator;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbObserverHeight;
        private System.Windows.Forms.Button btnShowShadowcasters;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Button btnCasterRenderPoint;
        private System.Windows.Forms.ComboBox cbProcessor;
        private System.Windows.Forms.ComboBox cbSurroundings;
        private System.Windows.Forms.Button btnProcessShadowQueue;
        private System.Windows.Forms.CheckBox cbCenter;
        private System.Windows.Forms.Button btnGPU4;
        private System.Windows.Forms.Button btnGPU1;
        private System.Windows.Forms.Button btnGPU3;
        private System.Windows.Forms.Button btnGPU2;
        private System.Windows.Forms.TabControl tabRight;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.Panel pnlPropertiesHolder;
        private System.Windows.Forms.TabPage tabImageLibrary;
        private System.Windows.Forms.ListView lvImageLibrary;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.TabControl tabCenter;
        private System.Windows.Forms.TabPage tabMap;
    }
}