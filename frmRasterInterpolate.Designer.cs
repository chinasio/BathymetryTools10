namespace BathymetryTools10
{
    partial class frmRasterInterpolate
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
            this.cboRasterLayers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLineLayer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIntervalDistance = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboRasterLayers
            // 
            this.cboRasterLayers.BackColor = System.Drawing.Color.Wheat;
            this.cboRasterLayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRasterLayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRasterLayers.FormattingEnabled = true;
            this.cboRasterLayers.Location = new System.Drawing.Point(137, 8);
            this.cboRasterLayers.Name = "cboRasterLayers";
            this.cboRasterLayers.Size = new System.Drawing.Size(193, 21);
            this.cboRasterLayers.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Raster Layer:";
            // 
            // cboLineLayer
            // 
            this.cboLineLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboLineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLineLayer.FormattingEnabled = true;
            this.cboLineLayer.Location = new System.Drawing.Point(137, 35);
            this.cboLineLayer.Name = "cboLineLayer";
            this.cboLineLayer.Size = new System.Drawing.Size(193, 21);
            this.cboLineLayer.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Line Layer:";
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(242, 62);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(75, 23);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Interval Distance:";
            // 
            // txtIntervalDistance
            // 
            this.txtIntervalDistance.Location = new System.Drawing.Point(136, 62);
            this.txtIntervalDistance.Name = "txtIntervalDistance";
            this.txtIntervalDistance.Size = new System.Drawing.Size(100, 20);
            this.txtIntervalDistance.TabIndex = 8;
            // 
            // frmRasterInterpolate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 99);
            this.Controls.Add(this.txtIntervalDistance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.cboLineLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboRasterLayers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRasterInterpolate";
            this.Text = "frmRasterInterpolate";
            this.Load += new System.EventHandler(this.frmRasterInterpolate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRasterLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLineLayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIntervalDistance;
    }
}