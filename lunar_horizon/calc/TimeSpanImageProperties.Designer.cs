namespace lunar_horizon.calc
{
    partial class TimeSpanImageProperties
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.btnHistogram = new System.Windows.Forms.Button();
            this.tbMinHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMaxHours = new System.Windows.Forms.TextBox();
            this.btnGenerateImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.LightSkyBlue;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Location = new System.Drawing.Point(0, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(134, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "label1";
            // 
            // btnHistogram
            // 
            this.btnHistogram.Location = new System.Drawing.Point(0, 105);
            this.btnHistogram.Name = "btnHistogram";
            this.btnHistogram.Size = new System.Drawing.Size(128, 23);
            this.btnHistogram.TabIndex = 1;
            this.btnHistogram.Text = "Histogram";
            this.btnHistogram.UseVisualStyleBackColor = true;
            this.btnHistogram.Click += new System.EventHandler(this.btnHistogram_Click);
            // 
            // tbMinHours
            // 
            this.tbMinHours.Location = new System.Drawing.Point(64, 22);
            this.tbMinHours.Name = "tbMinHours";
            this.tbMinHours.Size = new System.Drawing.Size(64, 20);
            this.tbMinHours.TabIndex = 3;
            this.tbMinHours.Text = "10";
            this.tbMinHours.TextChanged += new System.EventHandler(this.tbMinHours_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Min Hours";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max Hours";
            // 
            // tbMaxHours
            // 
            this.tbMaxHours.Location = new System.Drawing.Point(64, 50);
            this.tbMaxHours.Name = "tbMaxHours";
            this.tbMaxHours.Size = new System.Drawing.Size(64, 20);
            this.tbMaxHours.TabIndex = 3;
            this.tbMaxHours.Text = "500";
            this.tbMaxHours.TextChanged += new System.EventHandler(this.tbMaxHours_TextChanged);
            // 
            // btnGenerateImage
            // 
            this.btnGenerateImage.Location = new System.Drawing.Point(0, 76);
            this.btnGenerateImage.Name = "btnGenerateImage";
            this.btnGenerateImage.Size = new System.Drawing.Size(128, 23);
            this.btnGenerateImage.TabIndex = 1;
            this.btnGenerateImage.Text = "Generate Image";
            this.btnGenerateImage.UseVisualStyleBackColor = true;
            this.btnGenerateImage.Click += new System.EventHandler(this.btnGenerateImage_Click);
            // 
            // TimeSpanImageProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbMaxHours);
            this.Controls.Add(this.tbMinHours);
            this.Controls.Add(this.btnGenerateImage);
            this.Controls.Add(this.btnHistogram);
            this.Controls.Add(this.lblName);
            this.Name = "TimeSpanImageProperties";
            this.Size = new System.Drawing.Size(134, 608);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnHistogram;
        private System.Windows.Forms.TextBox tbMinHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMaxHours;
        private System.Windows.Forms.Button btnGenerateImage;
    }
}
