namespace lunar_horizon.calc
{
    partial class LocationProbePropertySheet
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lvProbes = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tbElevation = new System.Windows.Forms.TextBox();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbHorizon = new System.Windows.Forms.RadioButton();
            this.rbLightcurve = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location Probes";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(41, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(105, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lvProbes
            // 
            this.lvProbes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProbes.Location = new System.Drawing.Point(0, 46);
            this.lvProbes.Name = "lvProbes";
            this.lvProbes.Size = new System.Drawing.Size(174, 309);
            this.lvProbes.TabIndex = 2;
            this.lvProbes.UseCompatibleStateImageBehavior = false;
            this.lvProbes.View = System.Windows.Forms.View.List;
            this.lvProbes.SelectedIndexChanged += new System.EventHandler(this.lvProbes_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 33);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.tbElevation);
            this.panel2.Controls.Add(this.tbLocation);
            this.panel2.Controls.Add(this.tbName);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 103);
            this.panel2.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox4.Location = new System.Drawing.Point(68, 60);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(106, 20);
            this.textBox4.TabIndex = 4;
            // 
            // tbElevation
            // 
            this.tbElevation.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbElevation.Location = new System.Drawing.Point(68, 40);
            this.tbElevation.Name = "tbElevation";
            this.tbElevation.Size = new System.Drawing.Size(106, 20);
            this.tbElevation.TabIndex = 2;
            // 
            // tbLocation
            // 
            this.tbLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbLocation.Location = new System.Drawing.Point(68, 20);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(106, 20);
            this.tbLocation.TabIndex = 1;
            // 
            // tbName
            // 
            this.tbName.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbName.Location = new System.Drawing.Point(68, 0);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(106, 20);
            this.tbName.TabIndex = 3;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(68, 103);
            this.panel3.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "TBD";
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Elevation";
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Location";
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Name";
            // 
            // rbHorizon
            // 
            this.rbHorizon.AutoSize = true;
            this.rbHorizon.Checked = true;
            this.rbHorizon.Location = new System.Drawing.Point(6, 19);
            this.rbHorizon.Name = "rbHorizon";
            this.rbHorizon.Size = new System.Drawing.Size(61, 17);
            this.rbHorizon.TabIndex = 0;
            this.rbHorizon.TabStop = true;
            this.rbHorizon.Text = "Horizon";
            this.rbHorizon.UseVisualStyleBackColor = true;
            // 
            // rbLightcurve
            // 
            this.rbLightcurve.AutoSize = true;
            this.rbLightcurve.Location = new System.Drawing.Point(6, 42);
            this.rbLightcurve.Name = "rbLightcurve";
            this.rbLightcurve.Size = new System.Drawing.Size(79, 17);
            this.rbLightcurve.TabIndex = 0;
            this.rbLightcurve.Text = "Light Curve";
            this.rbLightcurve.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbLightcurve);
            this.groupBox2.Controls.Add(this.rbHorizon);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 458);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 100);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display";
            // 
            // LocationProbePropertySheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvProbes);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "LocationProbePropertySheet";
            this.Size = new System.Drawing.Size(174, 558);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListView lvProbes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbElevation;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbHorizon;
        private System.Windows.Forms.RadioButton rbLightcurve;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
