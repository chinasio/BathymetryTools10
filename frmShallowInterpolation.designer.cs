namespace BathymetryTools10
{
    partial class frmShallowInterpolation
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboShoreline = new System.Windows.Forms.ComboBox();
            this.cboDepthLayer = new System.Windows.Forms.ComboBox();
            this.cboLineLayer = new System.Windows.Forms.ComboBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.txtDistInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.cboShoreDepthField = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboDepthlayerDepthfield = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shoreline Layer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Depth Point Layer:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Defined Line Layer:";
            // 
            // cboShoreline
            // 
            this.cboShoreline.BackColor = System.Drawing.Color.Wheat;
            this.cboShoreline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShoreline.FormattingEnabled = true;
            this.cboShoreline.Location = new System.Drawing.Point(131, 34);
            this.cboShoreline.Name = "cboShoreline";
            this.cboShoreline.Size = new System.Drawing.Size(144, 21);
            this.cboShoreline.TabIndex = 3;
            this.cboShoreline.SelectedIndexChanged += new System.EventHandler(this.cboShoreline_SelectedIndexChanged);
            // 
            // cboDepthLayer
            // 
            this.cboDepthLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthLayer.FormattingEnabled = true;
            this.cboDepthLayer.Location = new System.Drawing.Point(130, 100);
            this.cboDepthLayer.Name = "cboDepthLayer";
            this.cboDepthLayer.Size = new System.Drawing.Size(144, 21);
            this.cboDepthLayer.TabIndex = 5;
            this.cboDepthLayer.SelectedIndexChanged += new System.EventHandler(this.cboDepthLayer_SelectedIndexChanged);
            // 
            // cboLineLayer
            // 
            this.cboLineLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboLineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLineLayer.FormattingEnabled = true;
            this.cboLineLayer.Location = new System.Drawing.Point(131, 5);
            this.cboLineLayer.Name = "cboLineLayer";
            this.cboLineLayer.Size = new System.Drawing.Size(144, 21);
            this.cboLineLayer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cboLineLayer, "User defined transect lines, connecting shoreline to depth point,\r\n to hold the n" +
        "ewly generated points");
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Wheat;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(177, 186);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(98, 32);
            this.btnProcess.TabIndex = 8;
            this.btnProcess.Text = "Process";
            this.toolTip1.SetToolTip(this.btnProcess, "This tool will generate points at a user defined distance\r\nalong the Line Layer");
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // txtDistInterval
            // 
            this.txtDistInterval.BackColor = System.Drawing.Color.Wheat;
            this.txtDistInterval.Location = new System.Drawing.Point(222, 160);
            this.txtDistInterval.Name = "txtDistInterval";
            this.txtDistInterval.Size = new System.Drawing.Size(54, 20);
            this.txtDistInterval.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtDistInterval, "Interval to place new points along the Line Layer");
            this.txtDistInterval.TextChanged += new System.EventHandler(this.txtDistInterval_TextChanged);
            this.txtDistInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistInterval_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(28, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Interval Distance (in map units):";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Wheat;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(188, 224);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnClose, "This tool will generate points at a user defined distance\r\nalong the Line Layer");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboShoreDepthField
            // 
            this.cboShoreDepthField.BackColor = System.Drawing.Color.Wheat;
            this.cboShoreDepthField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShoreDepthField.FormattingEnabled = true;
            this.cboShoreDepthField.Location = new System.Drawing.Point(131, 61);
            this.cboShoreDepthField.Name = "cboShoreDepthField";
            this.cboShoreDepthField.Size = new System.Drawing.Size(144, 21);
            this.cboShoreDepthField.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(14, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Shoreline Depth Field:";
            // 
            // cboDepthlayerDepthfield
            // 
            this.cboDepthlayerDepthfield.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthlayerDepthfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthlayerDepthfield.FormattingEnabled = true;
            this.cboDepthlayerDepthfield.Location = new System.Drawing.Point(130, 127);
            this.cboDepthlayerDepthfield.Name = "cboDepthlayerDepthfield";
            this.cboDepthlayerDepthfield.Size = new System.Drawing.Size(144, 21);
            this.cboDepthlayerDepthfield.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "Depth Point Depth Field:";
            // 
            // frmShallowInterpolation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 261);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboDepthlayerDepthfield);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboShoreDepthField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDistInterval);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.cboLineLayer);
            this.Controls.Add(this.cboDepthLayer);
            this.Controls.Add(this.cboShoreline);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShallowInterpolation";
            this.Text = "Shallow Interpolation";
            this.Load += new System.EventHandler(this.frmShallowInterpolation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboShoreline;
        private System.Windows.Forms.ComboBox cboDepthLayer;
        private System.Windows.Forms.ComboBox cboLineLayer;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.TextBox txtDistInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboShoreDepthField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboDepthlayerDepthfield;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
    }
}