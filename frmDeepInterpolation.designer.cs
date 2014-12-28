namespace BathymetryTools10
{
    partial class frmDeepInterpolation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeepInterpolation));
            this.txtDistInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboShorelineLayer = new System.Windows.Forms.ComboBox();
            this.cboDepthLayer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.cboDepthlayerDepthfield = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDistInterval
            // 
            this.txtDistInterval.BackColor = System.Drawing.Color.Wheat;
            this.txtDistInterval.Location = new System.Drawing.Point(215, 91);
            this.txtDistInterval.Name = "txtDistInterval";
            this.txtDistInterval.Size = new System.Drawing.Size(56, 20);
            this.txtDistInterval.TabIndex = 5;
            this.toolTip1.SetToolTip(this.txtDistInterval, "Interval to place new points along the Line Layer");
            this.txtDistInterval.TextChanged += new System.EventHandler(this.txtDistInterval_TextChanged);
            this.txtDistInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDistInterval_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Interval Distance (in map units):";
            // 
            // cboShorelineLayer
            // 
            this.cboShorelineLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboShorelineLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboShorelineLayer.FormattingEnabled = true;
            this.cboShorelineLayer.Location = new System.Drawing.Point(132, 6);
            this.cboShorelineLayer.Name = "cboShorelineLayer";
            this.cboShorelineLayer.Size = new System.Drawing.Size(144, 21);
            this.cboShorelineLayer.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cboShorelineLayer, "User defined transect lines.");
            // 
            // cboDepthLayer
            // 
            this.cboDepthLayer.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthLayer.FormattingEnabled = true;
            this.cboDepthLayer.Location = new System.Drawing.Point(132, 33);
            this.cboDepthLayer.Name = "cboDepthLayer";
            this.cboDepthLayer.Size = new System.Drawing.Size(144, 21);
            this.cboDepthLayer.TabIndex = 3;
            this.cboDepthLayer.SelectedIndexChanged += new System.EventHandler(this.cboDepthLayer_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Line Layer:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Depth Point Layer:";
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Wheat;
            this.btnProcess.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnProcess.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcess.Location = new System.Drawing.Point(178, 117);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(98, 32);
            this.btnProcess.TabIndex = 6;
            this.btnProcess.Text = "Process";
            this.toolTip1.SetToolTip(this.btnProcess, "This tool will generate points at a user defined distance\r\nalong the Line Layer");
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Wheat;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(186, 155);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 30);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboDepthlayerDepthfield
            // 
            this.cboDepthlayerDepthfield.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthlayerDepthfield.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthlayerDepthfield.FormattingEnabled = true;
            this.cboDepthlayerDepthfield.Location = new System.Drawing.Point(132, 60);
            this.cboDepthlayerDepthfield.Name = "cboDepthlayerDepthfield";
            this.cboDepthlayerDepthfield.Size = new System.Drawing.Size(144, 21);
            this.cboDepthlayerDepthfield.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "Depth Point Depth Field:";
            // 
            // frmDeepInterpolation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 194);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboDepthlayerDepthfield);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtDistInterval);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboShorelineLayer);
            this.Controls.Add(this.cboDepthLayer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDeepInterpolation";
            this.Text = "Deep Interpolation";
            this.Load += new System.EventHandler(this.frmDeepInterpolation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDistInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboShorelineLayer;
        private System.Windows.Forms.ComboBox cboDepthLayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboDepthlayerDepthfield;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
    }
}