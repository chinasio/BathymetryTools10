namespace BathymetryTools10
{
    partial class frmPingSampler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPingSampler));
            this.label1 = new System.Windows.Forms.Label();
            this.cboEchosounderPoints = new System.Windows.Forms.ComboBox();
            this.cboSedimentCores = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.cbounits = new System.Windows.Forms.ComboBox();
            this.txtNumPoints = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpRangeSelect = new System.Windows.Forms.GroupBox();
            this.btnRangeSelect = new System.Windows.Forms.Button();
            this.grpQuantitySelect = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIteration = new System.Windows.Forms.TextBox();
            this.btnQuantitySelect = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cboDepthField = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpRangeSelect.SuspendLayout();
            this.grpQuantitySelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Echosounder Points:";
            // 
            // cboEchosounderPoints
            // 
            this.cboEchosounderPoints.BackColor = System.Drawing.Color.Wheat;
            this.cboEchosounderPoints.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEchosounderPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEchosounderPoints.FormattingEnabled = true;
            this.cboEchosounderPoints.Location = new System.Drawing.Point(148, 36);
            this.cboEchosounderPoints.Name = "cboEchosounderPoints";
            this.cboEchosounderPoints.Size = new System.Drawing.Size(167, 21);
            this.cboEchosounderPoints.TabIndex = 2;
            this.cboEchosounderPoints.SelectedIndexChanged += new System.EventHandler(this.cboEchosounderPoints_SelectedIndexChanged);
            // 
            // cboSedimentCores
            // 
            this.cboSedimentCores.BackColor = System.Drawing.Color.Wheat;
            this.cboSedimentCores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSedimentCores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSedimentCores.FormattingEnabled = true;
            this.cboSedimentCores.Location = new System.Drawing.Point(148, 9);
            this.cboSedimentCores.Name = "cboSedimentCores";
            this.cboSedimentCores.Size = new System.Drawing.Size(167, 21);
            this.cboSedimentCores.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sample Points:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Range:";
            // 
            // txtRange
            // 
            this.txtRange.BackColor = System.Drawing.Color.Wheat;
            this.txtRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRange.Location = new System.Drawing.Point(59, 19);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(58, 20);
            this.txtRange.TabIndex = 6;
            this.txtRange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRange_KeyPress);
            // 
            // cbounits
            // 
            this.cbounits.BackColor = System.Drawing.Color.Wheat;
            this.cbounits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbounits.FormattingEnabled = true;
            this.cbounits.Items.AddRange(new object[] {
            "Feet",
            "Meters",
            "Kilometers"});
            this.cbounits.Location = new System.Drawing.Point(123, 19);
            this.cbounits.Name = "cbounits";
            this.cbounits.Size = new System.Drawing.Size(103, 21);
            this.cbounits.TabIndex = 7;
            // 
            // txtNumPoints
            // 
            this.txtNumPoints.BackColor = System.Drawing.Color.Wheat;
            this.txtNumPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumPoints.Location = new System.Drawing.Point(82, 19);
            this.txtNumPoints.Name = "txtNumPoints";
            this.txtNumPoints.Size = new System.Drawing.Size(58, 20);
            this.txtNumPoints.TabIndex = 4;
            this.txtNumPoints.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumPoints_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Point Total:";
            // 
            // grpRangeSelect
            // 
            this.grpRangeSelect.BackColor = System.Drawing.Color.Transparent;
            this.grpRangeSelect.Controls.Add(this.btnRangeSelect);
            this.grpRangeSelect.Controls.Add(this.cbounits);
            this.grpRangeSelect.Controls.Add(this.label3);
            this.grpRangeSelect.Controls.Add(this.txtRange);
            this.grpRangeSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRangeSelect.Location = new System.Drawing.Point(77, 186);
            this.grpRangeSelect.Name = "grpRangeSelect";
            this.grpRangeSelect.Size = new System.Drawing.Size(238, 87);
            this.grpRangeSelect.TabIndex = 10;
            this.grpRangeSelect.TabStop = false;
            this.grpRangeSelect.Text = "Select by Range";
            // 
            // btnRangeSelect
            // 
            this.btnRangeSelect.BackColor = System.Drawing.Color.Wheat;
            this.btnRangeSelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnRangeSelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnRangeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRangeSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRangeSelect.Location = new System.Drawing.Point(141, 46);
            this.btnRangeSelect.Name = "btnRangeSelect";
            this.btnRangeSelect.Size = new System.Drawing.Size(85, 33);
            this.btnRangeSelect.TabIndex = 8;
            this.btnRangeSelect.Text = "Select";
            this.toolTip1.SetToolTip(this.btnRangeSelect, resources.GetString("btnRangeSelect.ToolTip"));
            this.btnRangeSelect.UseVisualStyleBackColor = false;
            this.btnRangeSelect.Click += new System.EventHandler(this.btnRangeSelect_Click);
            // 
            // grpQuantitySelect
            // 
            this.grpQuantitySelect.BackColor = System.Drawing.Color.Transparent;
            this.grpQuantitySelect.Controls.Add(this.label5);
            this.grpQuantitySelect.Controls.Add(this.txtIteration);
            this.grpQuantitySelect.Controls.Add(this.btnQuantitySelect);
            this.grpQuantitySelect.Controls.Add(this.label4);
            this.grpQuantitySelect.Controls.Add(this.txtNumPoints);
            this.grpQuantitySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpQuantitySelect.Location = new System.Drawing.Point(77, 92);
            this.grpQuantitySelect.Name = "grpQuantitySelect";
            this.grpQuantitySelect.Size = new System.Drawing.Size(238, 88);
            this.grpQuantitySelect.TabIndex = 11;
            this.grpQuantitySelect.TabStop = false;
            this.grpQuantitySelect.Text = "Select by Quantity";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Iteration Range (map units):";
            // 
            // txtIteration
            // 
            this.txtIteration.BackColor = System.Drawing.Color.Wheat;
            this.txtIteration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIteration.Location = new System.Drawing.Point(177, 55);
            this.txtIteration.Name = "txtIteration";
            this.txtIteration.Size = new System.Drawing.Size(32, 20);
            this.txtIteration.TabIndex = 9;
            this.txtIteration.Text = "1";
            this.toolTip1.SetToolTip(this.txtIteration, "Tool will search in interations of this distance\r\nuntil point total is found.  Sm" +
                    "aller numbers here\r\nwill be slower, but more accurate.");
            this.txtIteration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIteration_KeyPress);
            // 
            // btnQuantitySelect
            // 
            this.btnQuantitySelect.BackColor = System.Drawing.Color.Wheat;
            this.btnQuantitySelect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnQuantitySelect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnQuantitySelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuantitySelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuantitySelect.Location = new System.Drawing.Point(146, 12);
            this.btnQuantitySelect.Name = "btnQuantitySelect";
            this.btnQuantitySelect.Size = new System.Drawing.Size(85, 33);
            this.btnQuantitySelect.TabIndex = 5;
            this.btnQuantitySelect.Text = "Select";
            this.toolTip1.SetToolTip(this.btnQuantitySelect, resources.GetString("btnQuantitySelect.ToolTip"));
            this.btnQuantitySelect.UseVisualStyleBackColor = false;
            this.btnQuantitySelect.Click += new System.EventHandler(this.btnQuantitySelect_Click);
            // 
            // cboDepthField
            // 
            this.cboDepthField.BackColor = System.Drawing.Color.Wheat;
            this.cboDepthField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepthField.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepthField.FormattingEnabled = true;
            this.cboDepthField.Location = new System.Drawing.Point(148, 63);
            this.cboDepthField.Name = "cboDepthField";
            this.cboDepthField.Size = new System.Drawing.Size(167, 21);
            this.cboDepthField.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(78, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Value Field:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Silver;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(295, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "*Note, results will be added in multiple fields to the Sample Points layer.";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Wheat;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(218, 279);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 30);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Cancel";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmPingSampler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 342);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboDepthField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grpQuantitySelect);
            this.Controls.Add(this.grpRangeSelect);
            this.Controls.Add(this.cboSedimentCores);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboEchosounderPoints);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPingSampler";
            this.Text = "Ping Sampler";
            this.Load += new System.EventHandler(this.frmPingSampler_Load);
            this.grpRangeSelect.ResumeLayout(false);
            this.grpRangeSelect.PerformLayout();
            this.grpQuantitySelect.ResumeLayout(false);
            this.grpQuantitySelect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboEchosounderPoints;
        private System.Windows.Forms.ComboBox cboSedimentCores;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.ComboBox cbounits;
        private System.Windows.Forms.TextBox txtNumPoints;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpRangeSelect;
        private System.Windows.Forms.Button btnRangeSelect;
        private System.Windows.Forms.GroupBox grpQuantitySelect;
        private System.Windows.Forms.Button btnQuantitySelect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIteration;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cboDepthField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
    }
}