namespace lunar_horizon.view
{
    partial class CompareToAndy
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
            this.hermiteASunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.tbImageIndex = new System.Windows.Forms.TrackBar();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andyOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comparisonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbImageIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(846, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hermiteASunToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.fileToolStripMenuItem.Text = "&Load";
            // 
            // hermiteASunToolStripMenuItem
            // 
            this.hermiteASunToolStripMenuItem.Name = "hermiteASunToolStripMenuItem";
            this.hermiteASunToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.hermiteASunToolStripMenuItem.Text = "Hermite A Sun";
            this.hermiteASunToolStripMenuItem.Click += new System.EventHandler(this.hermiteASunToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 513);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(846, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.pbImage);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 24);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(846, 444);
            this.pnlContainer.TabIndex = 2;
            // 
            // tbImageIndex
            // 
            this.tbImageIndex.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbImageIndex.Location = new System.Drawing.Point(0, 468);
            this.tbImageIndex.Name = "tbImageIndex";
            this.tbImageIndex.Size = new System.Drawing.Size(846, 45);
            this.tbImageIndex.TabIndex = 3;
            this.tbImageIndex.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbImageIndex.ValueChanged += new System.EventHandler(this.tbImageIndex_ValueChanged);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(261, 237);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.andyOnlyToolStripMenuItem,
            this.markOnlyToolStripMenuItem,
            this.comparisonToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // andyOnlyToolStripMenuItem
            // 
            this.andyOnlyToolStripMenuItem.Name = "andyOnlyToolStripMenuItem";
            this.andyOnlyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.andyOnlyToolStripMenuItem.Text = "Andy only";
            this.andyOnlyToolStripMenuItem.Click += new System.EventHandler(this.andyOnlyToolStripMenuItem_Click);
            // 
            // markOnlyToolStripMenuItem
            // 
            this.markOnlyToolStripMenuItem.Name = "markOnlyToolStripMenuItem";
            this.markOnlyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.markOnlyToolStripMenuItem.Text = "Mark only";
            this.markOnlyToolStripMenuItem.Click += new System.EventHandler(this.markOnlyToolStripMenuItem_Click);
            // 
            // comparisonToolStripMenuItem
            // 
            this.comparisonToolStripMenuItem.Checked = true;
            this.comparisonToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.comparisonToolStripMenuItem.Name = "comparisonToolStripMenuItem";
            this.comparisonToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.comparisonToolStripMenuItem.Text = "Comparison";
            this.comparisonToolStripMenuItem.Click += new System.EventHandler(this.comparisonToolStripMenuItem_Click);
            // 
            // CompareToAndy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 535);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.tbImageIndex);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CompareToAndy";
            this.Text = "CompareToAndy";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbImageIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hermiteASunToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.TrackBar tbImageIndex;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem andyOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comparisonToolStripMenuItem;
    }
}