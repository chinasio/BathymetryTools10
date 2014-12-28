namespace BathymetryTools10
{
    partial class frmAutoInterpolate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutoInterpolate));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtDistInterval = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboShorelineLayer = new System.Windows.Forms.ComboBox();
            this.txtShorelinePointInterval = new System.Windows.Forms.TextBox();
            this.cboDepthlayerDepthfield = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboShoreDepthField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboDepthLayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Wheat;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(177, 215);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(98, 32);
            this.btnProcess.TabIndex = 42;
            this.btnProcess.Text = "Process";
            this.toolTip1.SetToolTip(this.btnProcess, resources.GetString("btnProcess.ToolTip"));
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click_1);
            // 
            // txtDistInterval
            // 
            this.txtDistInterval.BackColor = System.Drawing.Color.Wheat;
            this.txtDistInterval.Location = new System.Drawing.Point(218, 147);
            this.txtDistInterval.Name = "txtDistInterval";
            this.txtDistInterval.Size = new System.Drawing.Size(56, 20);
            this.txtDistInterval.TabIndex = 40;
            this.toolTip1.SetToolTip(this.txtDistInterval, "Interval to place new points along the Line Layer");
            this.txtDistInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistInterval_KeyPress_1);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Wheat;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(189, 253);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 30);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboShorelineLayer
            // 
            this.cboShorelineLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboShorelineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShorelineLayer.FormattingEnabled = true;
            this.cboShorelineLayer.Location = new System.Drawing.Point(130, 6);
            this.cboShorelineLayer.Name = "cboShorelineLayer";
            this.cboShorelineLayer.Size = new System.Drawing.Size(144, 21);
            this.cboShorelineLayer.TabIndex = 35;
            this.toolTip1.SetToolTip(this.cboShorelineLayer, "The layer containing one or more selected shoreline features\r\n");
            this.cboShorelineLayer.SelectedIndexChanged += new System.EventHandler(this.cboShorelineLayer_SelectedIndexChanged_1);
            // 
            // txtShorelinePointInterval
            // 
            this.txtShorelinePointInterval.BackColor = System.Drawing.Color.Wheat;
            this.txtShorelinePointInterval.Location = new System.Drawing.Point(219, 184);
            this.txtShorelinePointInterval.Name = "txtShorelinePointInterval";
            this.txtShorelinePointInterval.Size = new System.Drawing.Size(56, 20);
            this.txtShorelinePointInterval.TabIndex = 41;
            this.toolTip1.SetToolTip(this.txtShorelinePointInterval, "Interval to place new points along the Line Layer");
            // 
            // cboDepthlayerDepthfield
            // 
            this.cboDepthlayerDepthfield.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthlayerDepthfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthlayerDepthfield.FormattingEnabled = true;
            this.cboDepthlayerDepthfield.Location = new System.Drawing.Point(131, 98);
            this.cboDepthlayerDepthfield.Name = "cboDepthlayerDepthfield";
            this.cboDepthlayerDepthfield.Size = new System.Drawing.Size(144, 21);
            this.cboDepthlayerDepthfield.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 12);
            this.label6.TabIndex = 47;
            this.label6.Text = "Depth Point Depth Field:";
            // 
            // cboShoreDepthField
            // 
            this.cboShoreDepthField.BackColor = System.Drawing.Color.Wheat;
            this.cboShoreDepthField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShoreDepthField.FormattingEnabled = true;
            this.cboShoreDepthField.Location = new System.Drawing.Point(129, 33);
            this.cboShoreDepthField.Name = "cboShoreDepthField";
            this.cboShoreDepthField.Size = new System.Drawing.Size(144, 21);
            this.cboShoreDepthField.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "Shoreline Depth Field:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(74, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "distance (in map units):";
            // 
            // cboDepthLayer
            // 
            this.cboDepthLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthLayer.FormattingEnabled = true;
            this.cboDepthLayer.Location = new System.Drawing.Point(131, 71);
            this.cboDepthLayer.Name = "cboDepthLayer";
            this.cboDepthLayer.Size = new System.Drawing.Size(144, 21);
            this.cboDepthLayer.TabIndex = 37;
            this.cboDepthLayer.SelectedIndexChanged += new System.EventHandler(this.cboDepthLayer_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Shoreline segment:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Depth Point Layer:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(74, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 49;
            this.label4.Text = "Outpoint interval";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(74, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "distance (in map units):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(74, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Shoreline point interval";
            // 
            // frmAutoInterpolate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 293);
            this.Controls.Add(this.txtShorelinePointInterval);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboDepthlayerDepthfield);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboShoreDepthField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtDistInterval);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboShorelineLayer);
            this.Controls.Add(this.cboDepthLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAutoInterpolate";
            this.Text = "Auto Interpolate";
            this.Load += new System.EventHandler(this.frmAutoInterpolate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtDistInterval;
        private System.Windows.Forms.ComboBox cboDepthlayerDepthfield;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboShoreDepthField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboShorelineLayer;
        private System.Windows.Forms.ComboBox cboDepthLayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtShorelinePointInterval;
    }
}