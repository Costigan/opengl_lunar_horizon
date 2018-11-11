namespace lunar_horizon.view
{
    partial class MeshTester
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
            this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateSampleMeshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateFullSizedMeshV2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printBinaryFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawMeshGenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.right1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.right2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.right3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.down1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.down2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.down3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.drawMeshGenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testsToolStripMenuItem
            // 
            this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateSampleMeshToolStripMenuItem,
            this.generateFullSizedMeshV2ToolStripMenuItem});
            this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
            this.testsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.testsToolStripMenuItem.Text = "&Tests";
            // 
            // generateSampleMeshToolStripMenuItem
            // 
            this.generateSampleMeshToolStripMenuItem.Name = "generateSampleMeshToolStripMenuItem";
            this.generateSampleMeshToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.generateSampleMeshToolStripMenuItem.Text = "Generate Sample Mesh";
            this.generateSampleMeshToolStripMenuItem.Click += new System.EventHandler(this.generateSampleMeshToolStripMenuItem_Click);
            // 
            // generateFullSizedMeshV2ToolStripMenuItem
            // 
            this.generateFullSizedMeshV2ToolStripMenuItem.Name = "generateFullSizedMeshV2ToolStripMenuItem";
            this.generateFullSizedMeshV2ToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.generateFullSizedMeshV2ToolStripMenuItem.Text = "Generate Full-Sized variable res Mesh";
            this.generateFullSizedMeshV2ToolStripMenuItem.Click += new System.EventHandler(this.generateVariableSizedMeshToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.edgesToolStripMenuItem,
            this.facesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // edgesToolStripMenuItem
            // 
            this.edgesToolStripMenuItem.Checked = true;
            this.edgesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.edgesToolStripMenuItem.Name = "edgesToolStripMenuItem";
            this.edgesToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.edgesToolStripMenuItem.Text = "Edges";
            this.edgesToolStripMenuItem.Click += new System.EventHandler(this.edgesToolStripMenuItem_Click);
            // 
            // facesToolStripMenuItem
            // 
            this.facesToolStripMenuItem.Checked = true;
            this.facesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.facesToolStripMenuItem.Name = "facesToolStripMenuItem";
            this.facesToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.facesToolStripMenuItem.Text = "Faces";
            this.facesToolStripMenuItem.Click += new System.EventHandler(this.facesToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printBinaryFileToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "&Debug";
            // 
            // printBinaryFileToolStripMenuItem
            // 
            this.printBinaryFileToolStripMenuItem.Name = "printBinaryFileToolStripMenuItem";
            this.printBinaryFileToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.printBinaryFileToolStripMenuItem.Text = "Print Binary File";
            this.printBinaryFileToolStripMenuItem.Click += new System.EventHandler(this.printBinaryFileToolStripMenuItem_Click);
            // 
            // drawMeshGenToolStripMenuItem
            // 
            this.drawMeshGenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.centerToolStripMenuItem,
            this.diagonalToolStripMenuItem,
            this.right1ToolStripMenuItem,
            this.right2ToolStripMenuItem,
            this.right3ToolStripMenuItem,
            this.down1ToolStripMenuItem,
            this.down2ToolStripMenuItem,
            this.down3ToolStripMenuItem});
            this.drawMeshGenToolStripMenuItem.Name = "drawMeshGenToolStripMenuItem";
            this.drawMeshGenToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.drawMeshGenToolStripMenuItem.Text = "Draw Mesh Gen";
            // 
            // centerToolStripMenuItem
            // 
            this.centerToolStripMenuItem.Checked = true;
            this.centerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
            this.centerToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.centerToolStripMenuItem.Text = "Center";
            this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
            // 
            // diagonalToolStripMenuItem
            // 
            this.diagonalToolStripMenuItem.Checked = true;
            this.diagonalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.diagonalToolStripMenuItem.Name = "diagonalToolStripMenuItem";
            this.diagonalToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.diagonalToolStripMenuItem.Text = "Diagonal";
            this.diagonalToolStripMenuItem.Click += new System.EventHandler(this.diagonalToolStripMenuItem_Click);
            // 
            // right1ToolStripMenuItem
            // 
            this.right1ToolStripMenuItem.Checked = true;
            this.right1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.right1ToolStripMenuItem.Name = "right1ToolStripMenuItem";
            this.right1ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.right1ToolStripMenuItem.Text = "Right1";
            this.right1ToolStripMenuItem.Click += new System.EventHandler(this.right1ToolStripMenuItem_Click);
            // 
            // right2ToolStripMenuItem
            // 
            this.right2ToolStripMenuItem.Checked = true;
            this.right2ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.right2ToolStripMenuItem.Name = "right2ToolStripMenuItem";
            this.right2ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.right2ToolStripMenuItem.Text = "Right2";
            this.right2ToolStripMenuItem.Click += new System.EventHandler(this.right2ToolStripMenuItem_Click);
            // 
            // right3ToolStripMenuItem
            // 
            this.right3ToolStripMenuItem.Checked = true;
            this.right3ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.right3ToolStripMenuItem.Name = "right3ToolStripMenuItem";
            this.right3ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.right3ToolStripMenuItem.Text = "Right3";
            this.right3ToolStripMenuItem.Click += new System.EventHandler(this.right3ToolStripMenuItem_Click);
            // 
            // down1ToolStripMenuItem
            // 
            this.down1ToolStripMenuItem.Checked = true;
            this.down1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.down1ToolStripMenuItem.Name = "down1ToolStripMenuItem";
            this.down1ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.down1ToolStripMenuItem.Text = "Down1";
            this.down1ToolStripMenuItem.Click += new System.EventHandler(this.down1ToolStripMenuItem_Click);
            // 
            // down2ToolStripMenuItem
            // 
            this.down2ToolStripMenuItem.Checked = true;
            this.down2ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.down2ToolStripMenuItem.Name = "down2ToolStripMenuItem";
            this.down2ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.down2ToolStripMenuItem.Text = "Down2";
            this.down2ToolStripMenuItem.Click += new System.EventHandler(this.down2ToolStripMenuItem_Click);
            // 
            // down3ToolStripMenuItem
            // 
            this.down3ToolStripMenuItem.Checked = true;
            this.down3ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.down3ToolStripMenuItem.Name = "down3ToolStripMenuItem";
            this.down3ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.down3ToolStripMenuItem.Text = "Down3";
            this.down3ToolStripMenuItem.Click += new System.EventHandler(this.down3ToolStripMenuItem_Click);
            // 
            // pnlScroll
            // 
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 24);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Size = new System.Drawing.Size(884, 600);
            this.pnlScroll.TabIndex = 1;
            // 
            // MeshTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 624);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MeshTester";
            this.Text = "MeshTester";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
        private System.Windows.Forms.Panel pnlScroll;
        private System.Windows.Forms.ToolStripMenuItem generateSampleMeshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateFullSizedMeshV2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printBinaryFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawMeshGenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diagonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem right1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem right2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem right3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem down1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem down2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem down3ToolStripMenuItem;
    }
}