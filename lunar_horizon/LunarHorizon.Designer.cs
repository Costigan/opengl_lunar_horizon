namespace lunar_horizon
{
    partial class LunarHorizon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LunarHorizon));
            this.pnlZedgraphContainer = new System.Windows.Forms.Panel();
            this.pnlSelectPixel = new System.Windows.Forms.Panel();
            this.cbProcessor = new System.Windows.Forms.ComboBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.cbSurroundings = new System.Windows.Forms.ComboBox();
            this.cbCenter = new System.Windows.Forms.CheckBox();
            this.btnShowShadowcasters = new System.Windows.Forms.Button();
            this.btnCasterRenderPoint = new System.Windows.Forms.Button();
            this.btnGPU4 = new System.Windows.Forms.Button();
            this.btnGPU3 = new System.Windows.Forms.Button();
            this.btnGPU2 = new System.Windows.Forms.Button();
            this.btnGPU1 = new System.Windows.Forms.Button();
            this.btnProcessShadowQueue = new System.Windows.Forms.Button();
            this.lblPixel = new System.Windows.Forms.Label();
            this.tabCenter = new System.Windows.Forms.TabControl();
            this.tabMap = new System.Windows.Forms.TabPage();
            this.tabTesting = new System.Windows.Forms.TabPage();
            this.tabRenderTest = new System.Windows.Forms.TabPage();
            this.lbRenderTestElevation = new System.Windows.Forms.Label();
            this.lbRenderTestAzimuth = new System.Windows.Forms.Label();
            this.lbRenderTestTimeDelta = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRenderTestElevation = new System.Windows.Forms.TrackBar();
            this.tbRenderTestAzimuth = new System.Windows.Forms.TrackBar();
            this.tbRenderTestDateDelta = new System.Windows.Forms.TrackBar();
            this.dpRenderTestDate = new System.Windows.Forms.DateTimePicker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblLatLon = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTraverseLatLonSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.northPoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.southPoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadQueuetoSelectedPatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeQueuefromSelectedPatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timeControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.horizonsAlreadyCalculatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showShadowCalculationQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowcasterRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andyComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meshTesterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meshCreationWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightCurveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eraseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightShadowcastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightSurroundingtestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightCurveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calculationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendExistingPatchesTo177ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processQueueNearHorizonOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMeshCenteredAtSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterTentpolesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useGPUProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useCPUProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadHorizonsForPaintedTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopDrawingPaintedTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.unloadHorizonsForPaintedTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.paintSelectedRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findMinmaxInDEMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRenderTestPatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderIceStabilityDepthTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comparePatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runVerboseComparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTestShadowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firstTestTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundayTest1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundayTest2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cPUVsGPUTest1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPUTestSundayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPUTestMondayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPUTestSaturdayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateAverageSunSeriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeLongestNightDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeLongestNightHistogramCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeLongestNightImageFromDatasetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sundayTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mondayTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowBugCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fridayTestnobileLongestnightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlTime = new System.Windows.Forms.Panel();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.cbTimeSpan = new System.Windows.Forms.ComboBox();
            this.cbUpdateContinuously = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtStartTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSelectTime = new System.Windows.Forms.TrackBar();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.pnlPlot = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.tabRight = new System.Windows.Forms.TabControl();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.pnlPropertiesHolder = new System.Windows.Forms.Panel();
            this.tabImageLibrary = new System.Windows.Forms.TabPage();
            this.lvImageLibrary = new System.Windows.Forms.ListView();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.pbTest1 = new System.Windows.Forms.PictureBox();
            this.btnMouseIdle = new System.Windows.Forms.ToolStripButton();
            this.btnMousePaint = new System.Windows.Forms.ToolStripButton();
            this.btnMouseErase = new System.Windows.Forms.ToolStripButton();
            this.btnDeletePaint = new System.Windows.Forms.ToolStripButton();
            this.btnDrawRectangle1 = new System.Windows.Forms.ToolStripButton();
            this.btnDrawRectangle128 = new System.Windows.Forms.ToolStripButton();
            this.btnDrawRectangle1024 = new System.Windows.Forms.ToolStripButton();
            this.btnToggleControlsPane = new System.Windows.Forms.ToolStripButton();
            this.btnTogglePropertiesPane = new System.Windows.Forms.ToolStripButton();
            this.btnMouseRulerMeasure = new System.Windows.Forms.ToolStripButton();
            this.btnMouseCrossSection = new System.Windows.Forms.ToolStripButton();
            this.tabCenter.SuspendLayout();
            this.tabTesting.SuspendLayout();
            this.tabRenderTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestElevation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestAzimuth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestDateDelta)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabLeft.SuspendLayout();
            this.tabLayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).BeginInit();
            this.tabGenerator.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).BeginInit();
            this.tabRight.SuspendLayout();
            this.tabProperties.SuspendLayout();
            this.tabImageLibrary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTest1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlZedgraphContainer
            // 
            this.pnlZedgraphContainer.BackColor = System.Drawing.Color.White;
            this.pnlZedgraphContainer.Location = new System.Drawing.Point(307, 21);
            this.pnlZedgraphContainer.Name = "pnlZedgraphContainer";
            this.pnlZedgraphContainer.Size = new System.Drawing.Size(741, 458);
            this.pnlZedgraphContainer.TabIndex = 4;
            // 
            // pnlSelectPixel
            // 
            this.pnlSelectPixel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlSelectPixel.Location = new System.Drawing.Point(25, 50);
            this.pnlSelectPixel.Name = "pnlSelectPixel";
            this.pnlSelectPixel.Size = new System.Drawing.Size(256, 256);
            this.pnlSelectPixel.TabIndex = 7;
            this.pnlSelectPixel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlSelectPixel_MouseDown);
            this.pnlSelectPixel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlSelectPixel_MouseMove);
            this.pnlSelectPixel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlSelectPixel_MouseUp);
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
            this.cbProcessor.SelectedIndexChanged += new System.EventHandler(this.cbProcessor_SelectedIndexChanged);
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
            // btnShowShadowcasters
            // 
            this.btnShowShadowcasters.Location = new System.Drawing.Point(8, 456);
            this.btnShowShadowcasters.Name = "btnShowShadowcasters";
            this.btnShowShadowcasters.Size = new System.Drawing.Size(75, 23);
            this.btnShowShadowcasters.TabIndex = 12;
            this.btnShowShadowcasters.Text = "Shadowcasters";
            this.btnShowShadowcasters.UseVisualStyleBackColor = true;
            this.btnShowShadowcasters.Click += new System.EventHandler(this.btnShowShadowcasters_Click);
            // 
            // btnCasterRenderPoint
            // 
            this.btnCasterRenderPoint.Location = new System.Drawing.Point(8, 427);
            this.btnCasterRenderPoint.Name = "btnCasterRenderPoint";
            this.btnCasterRenderPoint.Size = new System.Drawing.Size(75, 23);
            this.btnCasterRenderPoint.TabIndex = 12;
            this.btnCasterRenderPoint.Text = "Render Pt";
            this.btnCasterRenderPoint.UseVisualStyleBackColor = true;
            this.btnCasterRenderPoint.Click += new System.EventHandler(this.btnCasterRenderPoint_Click);
            // 
            // btnGPU4
            // 
            this.btnGPU4.Location = new System.Drawing.Point(10, 303);
            this.btnGPU4.Name = "btnGPU4";
            this.btnGPU4.Size = new System.Drawing.Size(75, 23);
            this.btnGPU4.TabIndex = 11;
            this.btnGPU4.Text = "GPU 4";
            this.btnGPU4.UseVisualStyleBackColor = true;
            this.btnGPU4.Click += new System.EventHandler(this.btnGPU4_Click);
            // 
            // btnGPU3
            // 
            this.btnGPU3.Location = new System.Drawing.Point(10, 274);
            this.btnGPU3.Name = "btnGPU3";
            this.btnGPU3.Size = new System.Drawing.Size(75, 23);
            this.btnGPU3.TabIndex = 11;
            this.btnGPU3.Text = "GPU 3";
            this.btnGPU3.UseVisualStyleBackColor = true;
            this.btnGPU3.Click += new System.EventHandler(this.btnGPU3_Click);
            // 
            // btnGPU2
            // 
            this.btnGPU2.Location = new System.Drawing.Point(10, 245);
            this.btnGPU2.Name = "btnGPU2";
            this.btnGPU2.Size = new System.Drawing.Size(75, 23);
            this.btnGPU2.TabIndex = 10;
            this.btnGPU2.Text = "GPU 2";
            this.btnGPU2.UseVisualStyleBackColor = true;
            this.btnGPU2.Click += new System.EventHandler(this.btnGPU2_Click);
            // 
            // btnGPU1
            // 
            this.btnGPU1.Location = new System.Drawing.Point(10, 216);
            this.btnGPU1.Name = "btnGPU1";
            this.btnGPU1.Size = new System.Drawing.Size(75, 23);
            this.btnGPU1.TabIndex = 9;
            this.btnGPU1.Text = "GPU 1";
            this.btnGPU1.UseVisualStyleBackColor = true;
            this.btnGPU1.Click += new System.EventHandler(this.btnGPU1_Click);
            // 
            // btnProcessShadowQueue
            // 
            this.btnProcessShadowQueue.Location = new System.Drawing.Point(8, 28);
            this.btnProcessShadowQueue.Name = "btnProcessShadowQueue";
            this.btnProcessShadowQueue.Size = new System.Drawing.Size(75, 65);
            this.btnProcessShadowQueue.TabIndex = 8;
            this.btnProcessShadowQueue.Text = "Process Shadow Queue";
            this.btnProcessShadowQueue.UseVisualStyleBackColor = true;
            this.btnProcessShadowQueue.Click += new System.EventHandler(this.btnProcessShadowQueue_Click);
            // 
            // lblPixel
            // 
            this.lblPixel.Location = new System.Drawing.Point(22, 31);
            this.lblPixel.Name = "lblPixel";
            this.lblPixel.Size = new System.Drawing.Size(156, 16);
            this.lblPixel.TabIndex = 9;
            this.lblPixel.Text = "<pixel>";
            // 
            // tabCenter
            // 
            this.tabCenter.Controls.Add(this.tabMap);
            this.tabCenter.Controls.Add(this.tabTesting);
            this.tabCenter.Controls.Add(this.tabRenderTest);
            this.tabCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCenter.Location = new System.Drawing.Point(222, 49);
            this.tabCenter.Name = "tabCenter";
            this.tabCenter.SelectedIndex = 0;
            this.tabCenter.Size = new System.Drawing.Size(764, 551);
            this.tabCenter.TabIndex = 10;
            // 
            // tabMap
            // 
            this.tabMap.Location = new System.Drawing.Point(4, 22);
            this.tabMap.Name = "tabMap";
            this.tabMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabMap.Size = new System.Drawing.Size(756, 525);
            this.tabMap.TabIndex = 0;
            this.tabMap.Text = "Map";
            this.tabMap.UseVisualStyleBackColor = true;
            // 
            // tabTesting
            // 
            this.tabTesting.Controls.Add(this.pnlZedgraphContainer);
            this.tabTesting.Controls.Add(this.lblPixel);
            this.tabTesting.Controls.Add(this.pnlSelectPixel);
            this.tabTesting.Location = new System.Drawing.Point(4, 22);
            this.tabTesting.Name = "tabTesting";
            this.tabTesting.Padding = new System.Windows.Forms.Padding(3);
            this.tabTesting.Size = new System.Drawing.Size(756, 525);
            this.tabTesting.TabIndex = 1;
            this.tabTesting.Text = "Testing";
            this.tabTesting.UseVisualStyleBackColor = true;
            // 
            // tabRenderTest
            // 
            this.tabRenderTest.Controls.Add(this.lbRenderTestElevation);
            this.tabRenderTest.Controls.Add(this.lbRenderTestAzimuth);
            this.tabRenderTest.Controls.Add(this.lbRenderTestTimeDelta);
            this.tabRenderTest.Controls.Add(this.label2);
            this.tabRenderTest.Controls.Add(this.label3);
            this.tabRenderTest.Controls.Add(this.label1);
            this.tabRenderTest.Controls.Add(this.tbRenderTestElevation);
            this.tabRenderTest.Controls.Add(this.tbRenderTestAzimuth);
            this.tabRenderTest.Controls.Add(this.tbRenderTestDateDelta);
            this.tabRenderTest.Controls.Add(this.dpRenderTestDate);
            this.tabRenderTest.Controls.Add(this.pbTest1);
            this.tabRenderTest.Location = new System.Drawing.Point(4, 22);
            this.tabRenderTest.Name = "tabRenderTest";
            this.tabRenderTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabRenderTest.Size = new System.Drawing.Size(756, 525);
            this.tabRenderTest.TabIndex = 2;
            this.tabRenderTest.Text = "Render Test";
            this.tabRenderTest.UseVisualStyleBackColor = true;
            // 
            // lbRenderTestElevation
            // 
            this.lbRenderTestElevation.Location = new System.Drawing.Point(356, 462);
            this.lbRenderTestElevation.Name = "lbRenderTestElevation";
            this.lbRenderTestElevation.Size = new System.Drawing.Size(100, 13);
            this.lbRenderTestElevation.TabIndex = 4;
            // 
            // lbRenderTestAzimuth
            // 
            this.lbRenderTestAzimuth.Location = new System.Drawing.Point(356, 443);
            this.lbRenderTestAzimuth.Name = "lbRenderTestAzimuth";
            this.lbRenderTestAzimuth.Size = new System.Drawing.Size(100, 13);
            this.lbRenderTestAzimuth.TabIndex = 4;
            // 
            // lbRenderTestTimeDelta
            // 
            this.lbRenderTestTimeDelta.Location = new System.Drawing.Point(356, 419);
            this.lbRenderTestTimeDelta.Name = "lbRenderTestTimeDelta";
            this.lbRenderTestTimeDelta.Size = new System.Drawing.Size(100, 13);
            this.lbRenderTestTimeDelta.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 467);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Elevation";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 419);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time Delta";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 443);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Azimuth";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbRenderTestElevation
            // 
            this.tbRenderTestElevation.BackColor = System.Drawing.Color.White;
            this.tbRenderTestElevation.Location = new System.Drawing.Point(94, 462);
            this.tbRenderTestElevation.Maximum = 900;
            this.tbRenderTestElevation.Name = "tbRenderTestElevation";
            this.tbRenderTestElevation.Size = new System.Drawing.Size(256, 45);
            this.tbRenderTestElevation.TabIndex = 2;
            this.tbRenderTestElevation.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRenderTestElevation.ValueChanged += new System.EventHandler(this.tbRenderTestElevation_ValueChanged);
            // 
            // tbRenderTestAzimuth
            // 
            this.tbRenderTestAzimuth.BackColor = System.Drawing.Color.White;
            this.tbRenderTestAzimuth.Location = new System.Drawing.Point(94, 435);
            this.tbRenderTestAzimuth.Maximum = 3600;
            this.tbRenderTestAzimuth.Name = "tbRenderTestAzimuth";
            this.tbRenderTestAzimuth.Size = new System.Drawing.Size(256, 45);
            this.tbRenderTestAzimuth.TabIndex = 2;
            this.tbRenderTestAzimuth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRenderTestAzimuth.ValueChanged += new System.EventHandler(this.tbRenderTestAzimuth_ValueChanged);
            // 
            // tbRenderTestDateDelta
            // 
            this.tbRenderTestDateDelta.BackColor = System.Drawing.Color.White;
            this.tbRenderTestDateDelta.Location = new System.Drawing.Point(94, 411);
            this.tbRenderTestDateDelta.Maximum = 1000;
            this.tbRenderTestDateDelta.Name = "tbRenderTestDateDelta";
            this.tbRenderTestDateDelta.Size = new System.Drawing.Size(256, 45);
            this.tbRenderTestDateDelta.TabIndex = 2;
            this.tbRenderTestDateDelta.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRenderTestDateDelta.ValueChanged += new System.EventHandler(this.tbRenderTestDateDelta_ValueChanged);
            // 
            // dpRenderTestDate
            // 
            this.dpRenderTestDate.Location = new System.Drawing.Point(94, 385);
            this.dpRenderTestDate.Name = "dpRenderTestDate";
            this.dpRenderTestDate.Size = new System.Drawing.Size(256, 20);
            this.dpRenderTestDate.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLatLon});
            this.statusStrip1.Location = new System.Drawing.Point(0, 767);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblLatLon
            // 
            this.lblLatLon.AutoSize = false;
            this.lblLatLon.Name = "lblLatLon";
            this.lblLatLon.Size = new System.Drawing.Size(600, 17);
            this.lblLatLon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mapToolStripMenuItem,
            this.queueToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.mouseToolStripMenuItem,
            this.calculationsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.tilesToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1192, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTraverseLatLonSummaryToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // loadTraverseLatLonSummaryToolStripMenuItem
            // 
            this.loadTraverseLatLonSummaryToolStripMenuItem.Name = "loadTraverseLatLonSummaryToolStripMenuItem";
            this.loadTraverseLatLonSummaryToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.loadTraverseLatLonSummaryToolStripMenuItem.Text = "Load Traverse LatLon Summary";
            this.loadTraverseLatLonSummaryToolStripMenuItem.Click += new System.EventHandler(this.loadTraverseLatLonSummaryToolStripMenuItem_Click);
            // 
            // mapToolStripMenuItem
            // 
            this.mapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.northPoleToolStripMenuItem,
            this.southPoleToolStripMenuItem});
            this.mapToolStripMenuItem.Name = "mapToolStripMenuItem";
            this.mapToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.mapToolStripMenuItem.Text = "&Map";
            // 
            // northPoleToolStripMenuItem
            // 
            this.northPoleToolStripMenuItem.Name = "northPoleToolStripMenuItem";
            this.northPoleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.northPoleToolStripMenuItem.Text = "North Pole 20m";
            this.northPoleToolStripMenuItem.Click += new System.EventHandler(this.northPoleToolStripMenuItem_Click);
            // 
            // southPoleToolStripMenuItem
            // 
            this.southPoleToolStripMenuItem.Checked = true;
            this.southPoleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.southPoleToolStripMenuItem.Name = "southPoleToolStripMenuItem";
            this.southPoleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.southPoleToolStripMenuItem.Text = "South Pole 20m";
            this.southPoleToolStripMenuItem.Click += new System.EventHandler(this.southPoleToolStripMenuItem_Click);
            // 
            // queueToolStripMenuItem
            // 
            this.queueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadQueuetoSelectedPatchesToolStripMenuItem,
            this.writeQueuefromSelectedPatchesToolStripMenuItem});
            this.queueToolStripMenuItem.Name = "queueToolStripMenuItem";
            this.queueToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.queueToolStripMenuItem.Text = "&Queue";
            // 
            // loadQueuetoSelectedPatchesToolStripMenuItem
            // 
            this.loadQueuetoSelectedPatchesToolStripMenuItem.Name = "loadQueuetoSelectedPatchesToolStripMenuItem";
            this.loadQueuetoSelectedPatchesToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.loadQueuetoSelectedPatchesToolStripMenuItem.Text = "Load queue (to selected patches)";
            this.loadQueuetoSelectedPatchesToolStripMenuItem.Click += new System.EventHandler(this.loadQueuetoSelectedPatchesToolStripMenuItem_Click);
            // 
            // writeQueuefromSelectedPatchesToolStripMenuItem
            // 
            this.writeQueuefromSelectedPatchesToolStripMenuItem.Name = "writeQueuefromSelectedPatchesToolStripMenuItem";
            this.writeQueuefromSelectedPatchesToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
            this.writeQueuefromSelectedPatchesToolStripMenuItem.Text = "Write queue (from selected patches)";
            this.writeQueuefromSelectedPatchesToolStripMenuItem.Click += new System.EventHandler(this.writeQueuefromSelectedPatchesToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.timeControlToolStripMenuItem,
            this.plotPanelToolStripMenuItem,
            this.toolStripSeparator4,
            this.horizonsAlreadyCalculatedToolStripMenuItem,
            this.showShadowCalculationQueueToolStripMenuItem,
            this.toolStripSeparator2,
            this.dWindowToolStripMenuItem,
            this.tileTesterToolStripMenuItem,
            this.shadowcasterRenderToolStripMenuItem,
            this.andyComparisonToolStripMenuItem,
            this.meshTesterToolStripMenuItem,
            this.meshCreationWindowToolStripMenuItem,
            this.aToolStripMenuItem,
            this.lightCurveToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.Checked = true;
            this.controlsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.controlsToolStripMenuItem.Text = "Controls Panel";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.propertiesToolStripMenuItem.Text = "Properties Panel";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // timeControlToolStripMenuItem
            // 
            this.timeControlToolStripMenuItem.Checked = true;
            this.timeControlToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.timeControlToolStripMenuItem.Name = "timeControlToolStripMenuItem";
            this.timeControlToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.timeControlToolStripMenuItem.Text = "Time Panel";
            this.timeControlToolStripMenuItem.Click += new System.EventHandler(this.timeControlToolStripMenuItem_Click);
            // 
            // plotPanelToolStripMenuItem
            // 
            this.plotPanelToolStripMenuItem.Name = "plotPanelToolStripMenuItem";
            this.plotPanelToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.plotPanelToolStripMenuItem.Text = "Plot Panel";
            this.plotPanelToolStripMenuItem.Click += new System.EventHandler(this.plotPanelToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(216, 6);
            // 
            // horizonsAlreadyCalculatedToolStripMenuItem
            // 
            this.horizonsAlreadyCalculatedToolStripMenuItem.Name = "horizonsAlreadyCalculatedToolStripMenuItem";
            this.horizonsAlreadyCalculatedToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.horizonsAlreadyCalculatedToolStripMenuItem.Text = "Horizons already calculated";
            this.horizonsAlreadyCalculatedToolStripMenuItem.Click += new System.EventHandler(this.horizonsAlreadyCalculatedToolStripMenuItem_Click);
            // 
            // showShadowCalculationQueueToolStripMenuItem
            // 
            this.showShadowCalculationQueueToolStripMenuItem.Name = "showShadowCalculationQueueToolStripMenuItem";
            this.showShadowCalculationQueueToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.showShadowCalculationQueueToolStripMenuItem.Text = "Horizon calculation queue";
            this.showShadowCalculationQueueToolStripMenuItem.Click += new System.EventHandler(this.showShadowCalculationQueueToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
            // 
            // dWindowToolStripMenuItem
            // 
            this.dWindowToolStripMenuItem.Name = "dWindowToolStripMenuItem";
            this.dWindowToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.dWindowToolStripMenuItem.Text = "3D window";
            this.dWindowToolStripMenuItem.Click += new System.EventHandler(this.dWindowToolStripMenuItem_Click);
            // 
            // tileTesterToolStripMenuItem
            // 
            this.tileTesterToolStripMenuItem.Name = "tileTesterToolStripMenuItem";
            this.tileTesterToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.tileTesterToolStripMenuItem.Text = "Tile Tester";
            this.tileTesterToolStripMenuItem.Click += new System.EventHandler(this.tileTesterToolStripMenuItem_Click);
            // 
            // shadowcasterRenderToolStripMenuItem
            // 
            this.shadowcasterRenderToolStripMenuItem.Name = "shadowcasterRenderToolStripMenuItem";
            this.shadowcasterRenderToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.shadowcasterRenderToolStripMenuItem.Text = "Shadowcaster Render";
            this.shadowcasterRenderToolStripMenuItem.Click += new System.EventHandler(this.shadowcasterRenderToolStripMenuItem_Click);
            // 
            // andyComparisonToolStripMenuItem
            // 
            this.andyComparisonToolStripMenuItem.Name = "andyComparisonToolStripMenuItem";
            this.andyComparisonToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.andyComparisonToolStripMenuItem.Text = "Andy Comparison";
            this.andyComparisonToolStripMenuItem.Click += new System.EventHandler(this.andyComparisonToolStripMenuItem_Click);
            // 
            // meshTesterToolStripMenuItem
            // 
            this.meshTesterToolStripMenuItem.Name = "meshTesterToolStripMenuItem";
            this.meshTesterToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.meshTesterToolStripMenuItem.Text = "Mesh Tester";
            this.meshTesterToolStripMenuItem.Click += new System.EventHandler(this.meshTesterToolStripMenuItem_Click);
            // 
            // meshCreationWindowToolStripMenuItem
            // 
            this.meshCreationWindowToolStripMenuItem.Name = "meshCreationWindowToolStripMenuItem";
            this.meshCreationWindowToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.meshCreationWindowToolStripMenuItem.Text = "Mesh Creation Window";
            this.meshCreationWindowToolStripMenuItem.Click += new System.EventHandler(this.meshCreationWindowToolStripMenuItem_Click);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.aToolStripMenuItem.Text = "Patch Compare";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // lightCurveToolStripMenuItem
            // 
            this.lightCurveToolStripMenuItem.Name = "lightCurveToolStripMenuItem";
            this.lightCurveToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.lightCurveToolStripMenuItem.Text = "Light Curve";
            this.lightCurveToolStripMenuItem.Click += new System.EventHandler(this.lightCurveToolStripMenuItem_Click);
            // 
            // mouseToolStripMenuItem
            // 
            this.mouseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.idleToolStripMenuItem,
            this.paintToolStripMenuItem,
            this.eraseToolStripMenuItem,
            this.renderPointToolStripMenuItem,
            this.highlightShadowcastersToolStripMenuItem,
            this.highlightSurroundingtestToolStripMenuItem,
            this.lightCurveToolStripMenuItem1});
            this.mouseToolStripMenuItem.Name = "mouseToolStripMenuItem";
            this.mouseToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.mouseToolStripMenuItem.Text = "&Mouse";
            // 
            // idleToolStripMenuItem
            // 
            this.idleToolStripMenuItem.Name = "idleToolStripMenuItem";
            this.idleToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.idleToolStripMenuItem.Text = "Idle";
            this.idleToolStripMenuItem.Click += new System.EventHandler(this.idleToolStripMenuItem_Click);
            // 
            // paintToolStripMenuItem
            // 
            this.paintToolStripMenuItem.Name = "paintToolStripMenuItem";
            this.paintToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.paintToolStripMenuItem.Text = "Paint";
            this.paintToolStripMenuItem.Click += new System.EventHandler(this.paintToolStripMenuItem_Click);
            // 
            // eraseToolStripMenuItem
            // 
            this.eraseToolStripMenuItem.Name = "eraseToolStripMenuItem";
            this.eraseToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.eraseToolStripMenuItem.Text = "Erase";
            this.eraseToolStripMenuItem.Click += new System.EventHandler(this.eraseToolStripMenuItem_Click);
            // 
            // renderPointToolStripMenuItem
            // 
            this.renderPointToolStripMenuItem.Name = "renderPointToolStripMenuItem";
            this.renderPointToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.renderPointToolStripMenuItem.Text = "Render Horizon";
            this.renderPointToolStripMenuItem.Click += new System.EventHandler(this.renderPointToolStripMenuItem_Click);
            // 
            // highlightShadowcastersToolStripMenuItem
            // 
            this.highlightShadowcastersToolStripMenuItem.Name = "highlightShadowcastersToolStripMenuItem";
            this.highlightShadowcastersToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.highlightShadowcastersToolStripMenuItem.Text = "Highlight Shadowcasters";
            this.highlightShadowcastersToolStripMenuItem.Click += new System.EventHandler(this.highlightShadowcastersToolStripMenuItem_Click);
            // 
            // highlightSurroundingtestToolStripMenuItem
            // 
            this.highlightSurroundingtestToolStripMenuItem.Name = "highlightSurroundingtestToolStripMenuItem";
            this.highlightSurroundingtestToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.highlightSurroundingtestToolStripMenuItem.Text = "Highlight Surrounding (test)";
            this.highlightSurroundingtestToolStripMenuItem.Click += new System.EventHandler(this.highlightSurroundingtestToolStripMenuItem_Click);
            // 
            // lightCurveToolStripMenuItem1
            // 
            this.lightCurveToolStripMenuItem1.Name = "lightCurveToolStripMenuItem1";
            this.lightCurveToolStripMenuItem1.Size = new System.Drawing.Size(223, 22);
            this.lightCurveToolStripMenuItem1.Text = "Light Curve";
            this.lightCurveToolStripMenuItem1.Click += new System.EventHandler(this.lightCurveToolStripMenuItem_Click);
            // 
            // calculationsToolStripMenuItem
            // 
            this.calculationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extendExistingPatchesTo177ToolStripMenuItem,
            this.processQueueNearHorizonOnlyToolStripMenuItem,
            this.exportMeshCenteredAtSelectionToolStripMenuItem});
            this.calculationsToolStripMenuItem.Name = "calculationsToolStripMenuItem";
            this.calculationsToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.calculationsToolStripMenuItem.Text = "Calculations";
            // 
            // extendExistingPatchesTo177ToolStripMenuItem
            // 
            this.extendExistingPatchesTo177ToolStripMenuItem.Name = "extendExistingPatchesTo177ToolStripMenuItem";
            this.extendExistingPatchesTo177ToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.extendExistingPatchesTo177ToolStripMenuItem.Text = "Extend existing patches to 177";
            this.extendExistingPatchesTo177ToolStripMenuItem.Click += new System.EventHandler(this.extendExistingPatchesTo177ToolStripMenuItem_Click);
            // 
            // processQueueNearHorizonOnlyToolStripMenuItem
            // 
            this.processQueueNearHorizonOnlyToolStripMenuItem.Name = "processQueueNearHorizonOnlyToolStripMenuItem";
            this.processQueueNearHorizonOnlyToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.processQueueNearHorizonOnlyToolStripMenuItem.Text = "Process Queue Near Horizon Only";
            this.processQueueNearHorizonOnlyToolStripMenuItem.Click += new System.EventHandler(this.processQueueNearHorizonOnlyToolStripMenuItem_Click);
            // 
            // exportMeshCenteredAtSelectionToolStripMenuItem
            // 
            this.exportMeshCenteredAtSelectionToolStripMenuItem.Name = "exportMeshCenteredAtSelectionToolStripMenuItem";
            this.exportMeshCenteredAtSelectionToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterTentpolesToolStripMenuItem,
            this.useGPUProcessorToolStripMenuItem,
            this.useCPUProcessorToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // filterTentpolesToolStripMenuItem
            // 
            this.filterTentpolesToolStripMenuItem.Checked = true;
            this.filterTentpolesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterTentpolesToolStripMenuItem.Name = "filterTentpolesToolStripMenuItem";
            this.filterTentpolesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.filterTentpolesToolStripMenuItem.Text = "Filter Tentpoles";
            this.filterTentpolesToolStripMenuItem.Click += new System.EventHandler(this.filterTentpolesToolStripMenuItem_Click);
            // 
            // useGPUProcessorToolStripMenuItem
            // 
            this.useGPUProcessorToolStripMenuItem.Checked = true;
            this.useGPUProcessorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useGPUProcessorToolStripMenuItem.Name = "useGPUProcessorToolStripMenuItem";
            this.useGPUProcessorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.useGPUProcessorToolStripMenuItem.Text = "Use GPU Processor";
            this.useGPUProcessorToolStripMenuItem.Click += new System.EventHandler(this.useGPUProcessorToolStripMenuItem_Click);
            // 
            // useCPUProcessorToolStripMenuItem
            // 
            this.useCPUProcessorToolStripMenuItem.Name = "useCPUProcessorToolStripMenuItem";
            this.useCPUProcessorToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.useCPUProcessorToolStripMenuItem.Text = "Use CPU Processor";
            this.useCPUProcessorToolStripMenuItem.Click += new System.EventHandler(this.useCPUProcessorToolStripMenuItem_Click);
            // 
            // tilesToolStripMenuItem
            // 
            this.tilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadHorizonsForPaintedTilesToolStripMenuItem,
            this.stopDrawingPaintedTilesToolStripMenuItem,
            this.toolStripSeparator5,
            this.unloadHorizonsForPaintedTilesToolStripMenuItem,
            this.toolStripSeparator6,
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1,
            this.paintSelectedRectangleToolStripMenuItem});
            this.tilesToolStripMenuItem.Name = "tilesToolStripMenuItem";
            this.tilesToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tilesToolStripMenuItem.Text = "Tiles";
            // 
            // loadHorizonsForPaintedTilesToolStripMenuItem
            // 
            this.loadHorizonsForPaintedTilesToolStripMenuItem.Name = "loadHorizonsForPaintedTilesToolStripMenuItem";
            this.loadHorizonsForPaintedTilesToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.loadHorizonsForPaintedTilesToolStripMenuItem.Text = "Load horizons for painted tiles";
            this.loadHorizonsForPaintedTilesToolStripMenuItem.Click += new System.EventHandler(this.loadHorizonsForPaintedTilesToolStripMenuItem_Click);
            // 
            // stopDrawingPaintedTilesToolStripMenuItem
            // 
            this.stopDrawingPaintedTilesToolStripMenuItem.Name = "stopDrawingPaintedTilesToolStripMenuItem";
            this.stopDrawingPaintedTilesToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.stopDrawingPaintedTilesToolStripMenuItem.Text = "Stop drawing painted tiles";
            this.stopDrawingPaintedTilesToolStripMenuItem.Click += new System.EventHandler(this.stopDrawingPaintedTilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(242, 6);
            // 
            // unloadHorizonsForPaintedTilesToolStripMenuItem
            // 
            this.unloadHorizonsForPaintedTilesToolStripMenuItem.Name = "unloadHorizonsForPaintedTilesToolStripMenuItem";
            this.unloadHorizonsForPaintedTilesToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.unloadHorizonsForPaintedTilesToolStripMenuItem.Text = "Unload horizons for painted tiles";
            this.unloadHorizonsForPaintedTilesToolStripMenuItem.Click += new System.EventHandler(this.unloadHorizonsForPaintedTilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(242, 6);
            // 
            // deleteHorizonsForPaintedTilesToolStripMenuItem1
            // 
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1.Name = "deleteHorizonsForPaintedTilesToolStripMenuItem1";
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1.Size = new System.Drawing.Size(245, 22);
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1.Text = "Delete horizons for painted tiles";
            this.deleteHorizonsForPaintedTilesToolStripMenuItem1.Click += new System.EventHandler(this.deleteHorizonsForPaintedTilesToolStripMenuItem1_Click);
            // 
            // paintSelectedRectangleToolStripMenuItem
            // 
            this.paintSelectedRectangleToolStripMenuItem.Name = "paintSelectedRectangleToolStripMenuItem";
            this.paintSelectedRectangleToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.paintSelectedRectangleToolStripMenuItem.Text = "Paint selected rectangle";
            this.paintSelectedRectangleToolStripMenuItem.Click += new System.EventHandler(this.paintSelectedRectangleToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findMinmaxInDEMToolStripMenuItem,
            this.loadRenderTestPatchToolStripMenuItem,
            this.renderIceStabilityDepthTestToolStripMenuItem,
            this.comparePatchesToolStripMenuItem,
            this.runVerboseComparisonToolStripMenuItem,
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem,
            this.generateTestShadowsToolStripMenuItem,
            this.firstTestTileToolStripMenuItem,
            this.sundayTest1ToolStripMenuItem,
            this.sundayTest2ToolStripMenuItem,
            this.cPUVsGPUTest1ToolStripMenuItem,
            this.gPUTestSundayToolStripMenuItem,
            this.gPUTestMondayToolStripMenuItem,
            this.gPUTestSaturdayToolStripMenuItem,
            this.generateAverageSunSeriesToolStripMenuItem,
            this.writeLongestNightDatasetToolStripMenuItem,
            this.writeLongestNightHistogramCSVToolStripMenuItem,
            this.writeLongestNightImageFromDatasetToolStripMenuItem,
            this.sundayTestToolStripMenuItem,
            this.mondayTestToolStripMenuItem,
            this.shadowBugCaseToolStripMenuItem,
            this.fridayTestnobileLongestnightToolStripMenuItem,
            this.loadAToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.testToolStripMenuItem.Text = "&Test";
            // 
            // findMinmaxInDEMToolStripMenuItem
            // 
            this.findMinmaxInDEMToolStripMenuItem.Name = "findMinmaxInDEMToolStripMenuItem";
            this.findMinmaxInDEMToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.findMinmaxInDEMToolStripMenuItem.Text = "Find min/max in DEM";
            this.findMinmaxInDEMToolStripMenuItem.Click += new System.EventHandler(this.findMinmaxInDEMToolStripMenuItem_Click);
            // 
            // loadRenderTestPatchToolStripMenuItem
            // 
            this.loadRenderTestPatchToolStripMenuItem.Name = "loadRenderTestPatchToolStripMenuItem";
            this.loadRenderTestPatchToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.loadRenderTestPatchToolStripMenuItem.Text = "Load Render Test Patch";
            this.loadRenderTestPatchToolStripMenuItem.Click += new System.EventHandler(this.loadRenderTestPatchToolStripMenuItem_Click);
            // 
            // renderIceStabilityDepthTestToolStripMenuItem
            // 
            this.renderIceStabilityDepthTestToolStripMenuItem.Name = "renderIceStabilityDepthTestToolStripMenuItem";
            this.renderIceStabilityDepthTestToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.renderIceStabilityDepthTestToolStripMenuItem.Text = "Render ice stability depth test";
            this.renderIceStabilityDepthTestToolStripMenuItem.Click += new System.EventHandler(this.renderIceStabilityDepthTestToolStripMenuItem_Click);
            // 
            // comparePatchesToolStripMenuItem
            // 
            this.comparePatchesToolStripMenuItem.Name = "comparePatchesToolStripMenuItem";
            this.comparePatchesToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.comparePatchesToolStripMenuItem.Text = "Compare Patches";
            this.comparePatchesToolStripMenuItem.Click += new System.EventHandler(this.comparePatchesToolStripMenuItem_Click);
            // 
            // runVerboseComparisonToolStripMenuItem
            // 
            this.runVerboseComparisonToolStripMenuItem.Name = "runVerboseComparisonToolStripMenuItem";
            this.runVerboseComparisonToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.runVerboseComparisonToolStripMenuItem.Text = "Run verbose comparison";
            this.runVerboseComparisonToolStripMenuItem.Click += new System.EventHandler(this.runVerboseComparisonToolStripMenuItem_Click);
            // 
            // fillMatricesOfCalculatedPatchesToolStripMenuItem
            // 
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem.Name = "fillMatricesOfCalculatedPatchesToolStripMenuItem";
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem.Text = "Fill Matrices of calculated patches";
            this.fillMatricesOfCalculatedPatchesToolStripMenuItem.Click += new System.EventHandler(this.fillMatricesOfCalculatedPatchesToolStripMenuItem_Click);
            // 
            // generateTestShadowsToolStripMenuItem
            // 
            this.generateTestShadowsToolStripMenuItem.Name = "generateTestShadowsToolStripMenuItem";
            this.generateTestShadowsToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.generateTestShadowsToolStripMenuItem.Text = "Generate Test Shadows";
            this.generateTestShadowsToolStripMenuItem.Click += new System.EventHandler(this.generateTestShadowsToolStripMenuItem_Click);
            // 
            // firstTestTileToolStripMenuItem
            // 
            this.firstTestTileToolStripMenuItem.Name = "firstTestTileToolStripMenuItem";
            this.firstTestTileToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.firstTestTileToolStripMenuItem.Text = "First Test Tile";
            this.firstTestTileToolStripMenuItem.Click += new System.EventHandler(this.firstTestTileToolStripMenuItem_Click);
            // 
            // sundayTest1ToolStripMenuItem
            // 
            this.sundayTest1ToolStripMenuItem.Name = "sundayTest1ToolStripMenuItem";
            this.sundayTest1ToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.sundayTest1ToolStripMenuItem.Text = "Sunday Test 1";
            this.sundayTest1ToolStripMenuItem.Click += new System.EventHandler(this.sundayTest1ToolStripMenuItem_Click);
            // 
            // sundayTest2ToolStripMenuItem
            // 
            this.sundayTest2ToolStripMenuItem.Name = "sundayTest2ToolStripMenuItem";
            this.sundayTest2ToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.sundayTest2ToolStripMenuItem.Text = "Sunday Test 2";
            this.sundayTest2ToolStripMenuItem.Click += new System.EventHandler(this.sundayTest2ToolStripMenuItem_Click);
            // 
            // cPUVsGPUTest1ToolStripMenuItem
            // 
            this.cPUVsGPUTest1ToolStripMenuItem.Name = "cPUVsGPUTest1ToolStripMenuItem";
            this.cPUVsGPUTest1ToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.cPUVsGPUTest1ToolStripMenuItem.Text = "CPU vs GPU Test 1";
            this.cPUVsGPUTest1ToolStripMenuItem.Click += new System.EventHandler(this.cPUVsGPUTest1ToolStripMenuItem_Click);
            // 
            // gPUTestSundayToolStripMenuItem
            // 
            this.gPUTestSundayToolStripMenuItem.Name = "gPUTestSundayToolStripMenuItem";
            this.gPUTestSundayToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.gPUTestSundayToolStripMenuItem.Text = "GPU test Sunday";
            this.gPUTestSundayToolStripMenuItem.Click += new System.EventHandler(this.gPUTestSundayToolStripMenuItem_Click);
            // 
            // gPUTestMondayToolStripMenuItem
            // 
            this.gPUTestMondayToolStripMenuItem.Name = "gPUTestMondayToolStripMenuItem";
            this.gPUTestMondayToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.gPUTestMondayToolStripMenuItem.Text = "GPU test Monday";
            this.gPUTestMondayToolStripMenuItem.Click += new System.EventHandler(this.gPUTestMondayToolStripMenuItem_Click);
            // 
            // gPUTestSaturdayToolStripMenuItem
            // 
            this.gPUTestSaturdayToolStripMenuItem.Name = "gPUTestSaturdayToolStripMenuItem";
            this.gPUTestSaturdayToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.gPUTestSaturdayToolStripMenuItem.Text = "GPU test Saturday";
            this.gPUTestSaturdayToolStripMenuItem.Click += new System.EventHandler(this.gPUTestSaturdayToolStripMenuItem_Click);
            // 
            // generateAverageSunSeriesToolStripMenuItem
            // 
            this.generateAverageSunSeriesToolStripMenuItem.Name = "generateAverageSunSeriesToolStripMenuItem";
            this.generateAverageSunSeriesToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.generateAverageSunSeriesToolStripMenuItem.Text = "Generate Average Sun Series";
            this.generateAverageSunSeriesToolStripMenuItem.Click += new System.EventHandler(this.generateAverageSunSeriesToolStripMenuItem_Click);
            // 
            // writeLongestNightDatasetToolStripMenuItem
            // 
            this.writeLongestNightDatasetToolStripMenuItem.Name = "writeLongestNightDatasetToolStripMenuItem";
            this.writeLongestNightDatasetToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.writeLongestNightDatasetToolStripMenuItem.Text = "Write Longest Night Dataset";
            this.writeLongestNightDatasetToolStripMenuItem.Click += new System.EventHandler(this.writeLongestNightDatasetToolStripMenuItem_Click);
            // 
            // writeLongestNightHistogramCSVToolStripMenuItem
            // 
            this.writeLongestNightHistogramCSVToolStripMenuItem.Name = "writeLongestNightHistogramCSVToolStripMenuItem";
            this.writeLongestNightHistogramCSVToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.writeLongestNightHistogramCSVToolStripMenuItem.Text = "Write Longest Night Histogram CSV";
            this.writeLongestNightHistogramCSVToolStripMenuItem.Click += new System.EventHandler(this.writeLongestNightHistogramCSVToolStripMenuItem_Click);
            // 
            // writeLongestNightImageFromDatasetToolStripMenuItem
            // 
            this.writeLongestNightImageFromDatasetToolStripMenuItem.Name = "writeLongestNightImageFromDatasetToolStripMenuItem";
            this.writeLongestNightImageFromDatasetToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.writeLongestNightImageFromDatasetToolStripMenuItem.Text = "Write Longest Night Image from Dataset";
            this.writeLongestNightImageFromDatasetToolStripMenuItem.Click += new System.EventHandler(this.writeLongestNightImageFromDatasetToolStripMenuItem_Click);
            // 
            // sundayTestToolStripMenuItem
            // 
            this.sundayTestToolStripMenuItem.Name = "sundayTestToolStripMenuItem";
            this.sundayTestToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.sundayTestToolStripMenuItem.Text = "Load longest-night-2028.tds";
            this.sundayTestToolStripMenuItem.Click += new System.EventHandler(this.sundayTestToolStripMenuItem_Click);
            // 
            // mondayTestToolStripMenuItem
            // 
            this.mondayTestToolStripMenuItem.Name = "mondayTestToolStripMenuItem";
            this.mondayTestToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.mondayTestToolStripMenuItem.Text = "Load shackelton-longest-night-2028.tds";
            this.mondayTestToolStripMenuItem.Click += new System.EventHandler(this.mondayTestToolStripMenuItem_Click);
            // 
            // shadowBugCaseToolStripMenuItem
            // 
            this.shadowBugCaseToolStripMenuItem.Name = "shadowBugCaseToolStripMenuItem";
            this.shadowBugCaseToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.shadowBugCaseToolStripMenuItem.Text = "Shadow bug case";
            this.shadowBugCaseToolStripMenuItem.Click += new System.EventHandler(this.shadowBugCaseToolStripMenuItem_Click);
            // 
            // fridayTestnobileLongestnightToolStripMenuItem
            // 
            this.fridayTestnobileLongestnightToolStripMenuItem.Name = "fridayTestnobileLongestnightToolStripMenuItem";
            this.fridayTestnobileLongestnightToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.fridayTestnobileLongestnightToolStripMenuItem.Text = "Load nobile-2m-longest-night-2028.tds";
            this.fridayTestnobileLongestnightToolStripMenuItem.Click += new System.EventHandler(this.fridayTestnobileLongestnightToolStripMenuItem_Click);
            // 
            // loadAToolStripMenuItem
            // 
            this.loadAToolStripMenuItem.Name = "loadAToolStripMenuItem";
            this.loadAToolStripMenuItem.Size = new System.Drawing.Size(307, 22);
            this.loadAToolStripMenuItem.Text = "Load shackleton-longest-night-2028-4m.tds";
            this.loadAToolStripMenuItem.Click += new System.EventHandler(this.loadShackletonLongestNight20284mToolStripMenuItem_Click);
            // 
            // tabLeft
            // 
            this.tabLeft.Controls.Add(this.tabLayers);
            this.tabLeft.Controls.Add(this.tabGenerator);
            this.tabLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabLeft.Location = new System.Drawing.Point(0, 49);
            this.tabLeft.Name = "tabLeft";
            this.tabLeft.SelectedIndex = 0;
            this.tabLeft.Size = new System.Drawing.Size(219, 551);
            this.tabLeft.TabIndex = 13;
            // 
            // tabLayers
            // 
            this.tabLayers.Controls.Add(this.lvLayers);
            this.tabLayers.Controls.Add(this.tbTransparency);
            this.tabLayers.Location = new System.Drawing.Point(4, 22);
            this.tabLayers.Name = "tabLayers";
            this.tabLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabLayers.Size = new System.Drawing.Size(211, 525);
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
            this.lvLayers.Size = new System.Drawing.Size(205, 474);
            this.lvLayers.TabIndex = 3;
            this.lvLayers.UseCompatibleStateImageBehavior = false;
            this.lvLayers.View = System.Windows.Forms.View.Details;
            this.lvLayers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvLayers_ItemChecked);
            this.lvLayers.SelectedIndexChanged += new System.EventHandler(this.lvLayers_SelectedIndexChanged);
            this.lvLayers.SizeChanged += new System.EventHandler(this.lvLayers_SizeChanged);
            this.lvLayers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvLayers_KeyDown);
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
            this.tbTransparency.Location = new System.Drawing.Point(3, 477);
            this.tbTransparency.Maximum = 100;
            this.tbTransparency.Name = "tbTransparency";
            this.tbTransparency.Size = new System.Drawing.Size(205, 45);
            this.tbTransparency.TabIndex = 2;
            this.tbTransparency.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbTransparency.ValueChanged += new System.EventHandler(this.tbTransparency_ValueChanged);
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
            this.tbObserverHeight.TextChanged += new System.EventHandler(this.tbObserverHeight_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMouseIdle,
            this.btnMousePaint,
            this.btnMouseErase,
            this.btnDeletePaint,
            this.btnDrawRectangle1,
            this.btnDrawRectangle128,
            this.btnDrawRectangle1024,
            this.toolStripSeparator1,
            this.btnToggleControlsPane,
            this.btnTogglePropertiesPane,
            this.toolStripSeparator3,
            this.btnMouseRulerMeasure,
            this.btnMouseCrossSection});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1192, 25);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            this.pnlTime.Location = new System.Drawing.Point(0, 713);
            this.pnlTime.Name = "pnlTime";
            this.pnlTime.Size = new System.Drawing.Size(1192, 54);
            this.pnlTime.TabIndex = 16;
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
            this.cbTimeSpan.SelectedIndexChanged += new System.EventHandler(this.cbTimeSpan_SelectedIndexChanged);
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
            this.dtStartTime.ValueChanged += new System.EventHandler(this.dtStartTime_ValueChanged);
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
            this.tbSelectTime.Size = new System.Drawing.Size(1192, 45);
            this.tbSelectTime.TabIndex = 1;
            this.tbSelectTime.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbSelectTime.Scroll += new System.EventHandler(this.tbSelectTime_Scroll);
            this.tbSelectTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbSelectTime_MouseUp);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(219, 49);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 551);
            this.splitter1.TabIndex = 17;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(1189, 49);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 664);
            this.splitter2.TabIndex = 18;
            this.splitter2.TabStop = false;
            // 
            // pnlPlot
            // 
            this.pnlPlot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlot.Location = new System.Drawing.Point(0, 603);
            this.pnlPlot.Name = "pnlPlot";
            this.pnlPlot.Size = new System.Drawing.Size(1189, 110);
            this.pnlPlot.TabIndex = 19;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 600);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(1189, 3);
            this.splitter3.TabIndex = 20;
            this.splitter3.TabStop = false;
            // 
            // tabRight
            // 
            this.tabRight.Controls.Add(this.tabProperties);
            this.tabRight.Controls.Add(this.tabImageLibrary);
            this.tabRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabRight.Location = new System.Drawing.Point(989, 49);
            this.tabRight.Name = "tabRight";
            this.tabRight.SelectedIndex = 0;
            this.tabRight.Size = new System.Drawing.Size(200, 551);
            this.tabRight.TabIndex = 21;
            // 
            // tabProperties
            // 
            this.tabProperties.Controls.Add(this.pnlPropertiesHolder);
            this.tabProperties.Location = new System.Drawing.Point(4, 22);
            this.tabProperties.Name = "tabProperties";
            this.tabProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabProperties.Size = new System.Drawing.Size(192, 525);
            this.tabProperties.TabIndex = 0;
            this.tabProperties.Text = "Properties";
            this.tabProperties.UseVisualStyleBackColor = true;
            // 
            // pnlPropertiesHolder
            // 
            this.pnlPropertiesHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPropertiesHolder.Location = new System.Drawing.Point(3, 3);
            this.pnlPropertiesHolder.Name = "pnlPropertiesHolder";
            this.pnlPropertiesHolder.Size = new System.Drawing.Size(186, 519);
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
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(986, 49);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 551);
            this.splitter4.TabIndex = 22;
            this.splitter4.TabStop = false;
            // 
            // pbTest1
            // 
            this.pbTest1.Location = new System.Drawing.Point(503, 15);
            this.pbTest1.Name = "pbTest1";
            this.pbTest1.Size = new System.Drawing.Size(512, 512);
            this.pbTest1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbTest1.TabIndex = 0;
            this.pbTest1.TabStop = false;
            // 
            // btnMouseIdle
            // 
            this.btnMouseIdle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMouseIdle.Image = ((System.Drawing.Image)(resources.GetObject("btnMouseIdle.Image")));
            this.btnMouseIdle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMouseIdle.Name = "btnMouseIdle";
            this.btnMouseIdle.Size = new System.Drawing.Size(23, 22);
            this.btnMouseIdle.Text = "toolStripButton1";
            this.btnMouseIdle.ToolTipText = "Drag Map";
            this.btnMouseIdle.Click += new System.EventHandler(this.btnMouseIdle_Click);
            // 
            // btnMousePaint
            // 
            this.btnMousePaint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMousePaint.Image = ((System.Drawing.Image)(resources.GetObject("btnMousePaint.Image")));
            this.btnMousePaint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMousePaint.Name = "btnMousePaint";
            this.btnMousePaint.Size = new System.Drawing.Size(23, 22);
            this.btnMousePaint.Text = "toolStripButton3";
            this.btnMousePaint.ToolTipText = "Paint Tiles";
            this.btnMousePaint.Click += new System.EventHandler(this.btnMousePaint_Click);
            // 
            // btnMouseErase
            // 
            this.btnMouseErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMouseErase.Image = ((System.Drawing.Image)(resources.GetObject("btnMouseErase.Image")));
            this.btnMouseErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMouseErase.Name = "btnMouseErase";
            this.btnMouseErase.Size = new System.Drawing.Size(23, 22);
            this.btnMouseErase.Text = "toolStripButton4";
            this.btnMouseErase.ToolTipText = "Erase Tiles";
            this.btnMouseErase.Click += new System.EventHandler(this.btnMouseErase_Click);
            // 
            // btnDeletePaint
            // 
            this.btnDeletePaint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeletePaint.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePaint.Image")));
            this.btnDeletePaint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeletePaint.Name = "btnDeletePaint";
            this.btnDeletePaint.Size = new System.Drawing.Size(23, 22);
            this.btnDeletePaint.Text = "toolStripButton2";
            this.btnDeletePaint.ToolTipText = "Delete Paint";
            this.btnDeletePaint.Click += new System.EventHandler(this.btnDeletePaint_Click);
            // 
            // btnDrawRectangle1
            // 
            this.btnDrawRectangle1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawRectangle1.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRectangle1.Image")));
            this.btnDrawRectangle1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawRectangle1.Name = "btnDrawRectangle1";
            this.btnDrawRectangle1.Size = new System.Drawing.Size(23, 22);
            this.btnDrawRectangle1.Text = "toolStripButton2";
            this.btnDrawRectangle1.ToolTipText = "Select Rectangle (128)";
            this.btnDrawRectangle1.Click += new System.EventHandler(this.btnDrawRectangle1_Click);
            // 
            // btnDrawRectangle128
            // 
            this.btnDrawRectangle128.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawRectangle128.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRectangle128.Image")));
            this.btnDrawRectangle128.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawRectangle128.Name = "btnDrawRectangle128";
            this.btnDrawRectangle128.Size = new System.Drawing.Size(23, 22);
            this.btnDrawRectangle128.Text = "toolStripButton2";
            this.btnDrawRectangle128.ToolTipText = "Select Rectangle (128)";
            this.btnDrawRectangle128.Click += new System.EventHandler(this.btnDrawRectangle128_Click);
            // 
            // btnDrawRectangle1024
            // 
            this.btnDrawRectangle1024.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDrawRectangle1024.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawRectangle1024.Image")));
            this.btnDrawRectangle1024.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawRectangle1024.Name = "btnDrawRectangle1024";
            this.btnDrawRectangle1024.Size = new System.Drawing.Size(23, 22);
            this.btnDrawRectangle1024.Text = "toolStripButton2";
            this.btnDrawRectangle1024.ToolTipText = "Select Rectangle (1024)";
            this.btnDrawRectangle1024.Click += new System.EventHandler(this.btnDrawRectangle1024_Click);
            // 
            // btnToggleControlsPane
            // 
            this.btnToggleControlsPane.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnToggleControlsPane.Image = ((System.Drawing.Image)(resources.GetObject("btnToggleControlsPane.Image")));
            this.btnToggleControlsPane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnToggleControlsPane.Name = "btnToggleControlsPane";
            this.btnToggleControlsPane.Size = new System.Drawing.Size(23, 22);
            this.btnToggleControlsPane.Text = "toolStripButton1";
            this.btnToggleControlsPane.ToolTipText = "Toggle Controls Pane";
            this.btnToggleControlsPane.Click += new System.EventHandler(this.btnToggleControlsPane_Click);
            // 
            // btnTogglePropertiesPane
            // 
            this.btnTogglePropertiesPane.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTogglePropertiesPane.Image = ((System.Drawing.Image)(resources.GetObject("btnTogglePropertiesPane.Image")));
            this.btnTogglePropertiesPane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTogglePropertiesPane.Name = "btnTogglePropertiesPane";
            this.btnTogglePropertiesPane.Size = new System.Drawing.Size(23, 22);
            this.btnTogglePropertiesPane.Text = "toolStripButton2";
            this.btnTogglePropertiesPane.ToolTipText = "Toggle Properties Pane";
            this.btnTogglePropertiesPane.Click += new System.EventHandler(this.btnTogglePropertiesPane_Click);
            // 
            // btnMouseRulerMeasure
            // 
            this.btnMouseRulerMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMouseRulerMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnMouseRulerMeasure.Image")));
            this.btnMouseRulerMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMouseRulerMeasure.Name = "btnMouseRulerMeasure";
            this.btnMouseRulerMeasure.Size = new System.Drawing.Size(23, 22);
            this.btnMouseRulerMeasure.Text = "Measure Distance";
            this.btnMouseRulerMeasure.Click += new System.EventHandler(this.btnMouseRulerMeasure_Click);
            // 
            // btnMouseCrossSection
            // 
            this.btnMouseCrossSection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMouseCrossSection.Image = global::lunar_horizon.Properties.Resources.CrossSection;
            this.btnMouseCrossSection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMouseCrossSection.Name = "btnMouseCrossSection";
            this.btnMouseCrossSection.Size = new System.Drawing.Size(23, 22);
            this.btnMouseCrossSection.Text = "toolStripButton2";
            this.btnMouseCrossSection.Click += new System.EventHandler(this.btnMouseCrossSection_Click);
            // 
            // LunarHorizon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 789);
            this.Controls.Add(this.tabCenter);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.splitter4);
            this.Controls.Add(this.tabRight);
            this.Controls.Add(this.tabLeft);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.pnlPlot);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.pnlTime);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LunarHorizon";
            this.Text = "Lunar Horizon";
            this.tabCenter.ResumeLayout(false);
            this.tabTesting.ResumeLayout(false);
            this.tabRenderTest.ResumeLayout(false);
            this.tabRenderTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestElevation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestAzimuth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbRenderTestDateDelta)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabLeft.ResumeLayout(false);
            this.tabLayers.ResumeLayout(false);
            this.tabLayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTransparency)).EndInit();
            this.tabGenerator.ResumeLayout(false);
            this.tabGenerator.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlTime.ResumeLayout(false);
            this.pnlTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSelectTime)).EndInit();
            this.tabRight.ResumeLayout(false);
            this.tabProperties.ResumeLayout(false);
            this.tabImageLibrary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTest1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlZedgraphContainer;
        private System.Windows.Forms.Panel pnlSelectPixel;
        private System.Windows.Forms.Label lblPixel;
        private System.Windows.Forms.TabControl tabCenter;
        private System.Windows.Forms.TabPage tabMap;
        private System.Windows.Forms.TabPage tabTesting;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblLatLon;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem northPoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem southPoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showShadowCalculationQueueToolStripMenuItem;
        private System.Windows.Forms.Button btnProcessShadowQueue;
        private System.Windows.Forms.TabPage tabRenderTest;
        private System.Windows.Forms.TrackBar tbRenderTestElevation;
        private System.Windows.Forms.TrackBar tbRenderTestAzimuth;
        private System.Windows.Forms.TrackBar tbRenderTestDateDelta;
        private System.Windows.Forms.DateTimePicker dpRenderTestDate;
        private System.Windows.Forms.PictureBox pbTest1;
        private System.Windows.Forms.Label lbRenderTestElevation;
        private System.Windows.Forms.Label lbRenderTestAzimuth;
        private System.Windows.Forms.Label lbRenderTestTimeDelta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRenderTestPatchToolStripMenuItem;
        private System.Windows.Forms.Button btnGPU1;
        private System.Windows.Forms.Button btnGPU2;
        private System.Windows.Forms.Button btnGPU3;
        private System.Windows.Forms.ToolStripMenuItem horizonsAlreadyCalculatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderIceStabilityDepthTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comparePatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runVerboseComparisonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillMatricesOfCalculatedPatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateTestShadowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileTesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowcasterRenderToolStripMenuItem;
        private System.Windows.Forms.Button btnCasterRenderPoint;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterTentpolesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstTestTileToolStripMenuItem;
        private System.Windows.Forms.Button btnShowShadowcasters;
        private System.Windows.Forms.ToolStripMenuItem sundayTest1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sundayTest2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findMinmaxInDEMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extendExistingPatchesTo177ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eraseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightShadowcastersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightSurroundingtestToolStripMenuItem;
        private System.Windows.Forms.Button btnGPU4;
        private System.Windows.Forms.ToolStripMenuItem cPUVsGPUTest1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem processQueueNearHorizonOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem andyComparisonToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbSurroundings;
        private System.Windows.Forms.CheckBox cbCenter;
        private System.Windows.Forms.ToolStripMenuItem gPUTestSundayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useGPUProcessorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useCPUProcessorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPUTestMondayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMeshCenteredAtSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meshTesterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meshCreationWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTraverseLatLonSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadQueuetoSelectedPatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeQueuefromSelectedPatchesToolStripMenuItem;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ComboBox cbProcessor;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPUTestSaturdayToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem lightCurveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightCurveToolStripMenuItem1;
        private System.Windows.Forms.TabControl tabLeft;
        private System.Windows.Forms.TabPage tabLayers;
        internal System.Windows.Forms.ListView lvLayers;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colTransparency;
        private System.Windows.Forms.TrackBar tbTransparency;
        private System.Windows.Forms.TabPage tabGenerator;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timeControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel pnlTime;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.ComboBox cbTimeSpan;
        private System.Windows.Forms.CheckBox cbUpdateContinuously;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtStartTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbSelectTime;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ToolStripMenuItem generateAverageSunSeriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnMouseIdle;
        private System.Windows.Forms.ToolStripButton btnMousePaint;
        private System.Windows.Forms.ToolStripButton btnMouseErase;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle1024;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle128;
        private System.Windows.Forms.ToolStripButton btnDeletePaint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnToggleControlsPane;
        private System.Windows.Forms.ToolStripButton btnTogglePropertiesPane;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.ToolStripButton btnMouseRulerMeasure;
        private System.Windows.Forms.ToolStripButton btnMouseCrossSection;
        private System.Windows.Forms.Panel pnlPlot;
        private System.Windows.Forms.ToolStripMenuItem plotPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeLongestNightDatasetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeLongestNightHistogramCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem writeLongestNightImageFromDatasetToolStripMenuItem;
        private System.Windows.Forms.TabControl tabRight;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TabPage tabImageLibrary;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.ListView lvImageLibrary;
        private System.Windows.Forms.ToolStripButton btnDrawRectangle1;
        private System.Windows.Forms.ToolStripMenuItem sundayTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mondayTestToolStripMenuItem;
        private System.Windows.Forms.Panel pnlPropertiesHolder;
        private System.Windows.Forms.ToolStripMenuItem shadowBugCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadHorizonsForPaintedTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deleteHorizonsForPaintedTilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fridayTestnobileLongestnightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unloadHorizonsForPaintedTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem stopDrawingPaintedTilesToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbObserverHeight;
        private System.Windows.Forms.ToolStripMenuItem paintSelectedRectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAToolStripMenuItem;
    }
}

