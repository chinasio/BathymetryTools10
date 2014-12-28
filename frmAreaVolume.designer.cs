namespace BathymetryTools10
{
    partial class frmAreaVolume
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRasterLayers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartingElevation = new System.Windows.Forms.TextBox();
            this.txtEndingElevation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGradInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkVertUnitCorrect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elevation Layer:";
            // 
            // cboRasterLayers
            // 
            this.cboRasterLayers.BackColor = System.Drawing.Color.Wheat;
            this.cboRasterLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRasterLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRasterLayers.FormattingEnabled = true;
            this.cboRasterLayers.Location = new System.Drawing.Point(161, 8);
            this.cboRasterLayers.Name = "cboRasterLayers";
            this.cboRasterLayers.Size = new System.Drawing.Size(193, 21);
            this.cboRasterLayers.TabIndex = 1;
            this.cboRasterLayers.SelectedIndexChanged += new System.EventHandler(this.cboRasterLayers_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lowest Elevation:";
            // 
            // txtStartingElevation
            // 
            this.txtStartingElevation.BackColor = System.Drawing.Color.Wheat;
            this.txtStartingElevation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartingElevation.Location = new System.Drawing.Point(161, 35);
            this.txtStartingElevation.Name = "txtStartingElevation";
            this.txtStartingElevation.Size = new System.Drawing.Size(100, 20);
            this.txtStartingElevation.TabIndex = 3;
            this.txtStartingElevation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStartingElevation_KeyPress);
            // 
            // txtEndingElevation
            // 
            this.txtEndingElevation.BackColor = System.Drawing.Color.Wheat;
            this.txtEndingElevation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndingElevation.Location = new System.Drawing.Point(161, 61);
            this.txtEndingElevation.Name = "txtEndingElevation";
            this.txtEndingElevation.Size = new System.Drawing.Size(100, 20);
            this.txtEndingElevation.TabIndex = 5;
            this.txtEndingElevation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndingElevation_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Highest Elevation:";
            // 
            // txtGradInterval
            // 
            this.txtGradInterval.BackColor = System.Drawing.Color.Wheat;
            this.txtGradInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGradInterval.Location = new System.Drawing.Point(161, 87);
            this.txtGradInterval.Name = "txtGradInterval";
            this.txtGradInterval.Size = new System.Drawing.Size(100, 20);
            this.txtGradInterval.TabIndex = 7;
            this.txtGradInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGradInterval_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Graduation Interval:";
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Wheat;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(272, 35);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(82, 32);
            this.btnProcess.TabIndex = 9;
            this.btnProcess.Text = "Process";
            this.toolTip1.SetToolTip(this.btnProcess, "This tool will measure Rasters or TINs for volume and area\r\nin interations, then " +
                    "report the results in a new CSV table.\r\nThis table will be added to and opened i" +
                    "n the current ArcMap project.");
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Wheat;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gold;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(272, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 32);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnCancel, "This tool will measure Rasters or TINs for volume and area\r\nin interations, then " +
                    "report the results in a new CSV table.\r\nThis table will be added to and opened i" +
                    "n the current ArcMap project.");
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(323, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "*Note, Area is returned in acres.  All units are assumed to be meters by default";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Silver;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "A valid 3D Analyst license is required.";
            // 
            // chkVertUnitCorrect
            // 
            this.chkVertUnitCorrect.AutoSize = true;
            this.chkVertUnitCorrect.BackColor = System.Drawing.Color.Transparent;
            this.chkVertUnitCorrect.Checked = true;
            this.chkVertUnitCorrect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVertUnitCorrect.Location = new System.Drawing.Point(161, 113);
            this.chkVertUnitCorrect.Name = "chkVertUnitCorrect";
            this.chkVertUnitCorrect.Size = new System.Drawing.Size(156, 17);
            this.chkVertUnitCorrect.TabIndex = 12;
            this.chkVertUnitCorrect.Text = "Vertical Volume units in feet";
            this.chkVertUnitCorrect.UseVisualStyleBackColor = false;
            // 
            // frmAreaVolume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 179);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkVertUnitCorrect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtGradInterval);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEndingElevation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStartingElevation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboRasterLayers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAreaVolume";
            this.Text = "Bathymetry Area and Volume";
            this.Load += new System.EventHandler(this.frmAreaVolume_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboRasterLayers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartingElevation;
        private System.Windows.Forms.TextBox txtEndingElevation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGradInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkVertUnitCorrect;
        private System.Windows.Forms.Button btnCancel;
    }
}