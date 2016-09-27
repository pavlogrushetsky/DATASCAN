namespace DATASCAN.View.Forms
{
    partial class EditFloutecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFloutecForm));
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblScanType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.MaskedTextBox();
            this.numAddress = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rbDbf = new System.Windows.Forms.RadioButton();
            this.rbGPRS = new System.Windows.Forms.RadioButton();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Назва";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 62);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(33, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Опис";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(12, 142);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(92, 13);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Номер телефону";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 101);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(44, 13);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Адреса";
            // 
            // lblScanType
            // 
            this.lblScanType.AutoSize = true;
            this.lblScanType.Location = new System.Drawing.Point(12, 180);
            this.lblScanType.Name = "lblScanType";
            this.lblScanType.Size = new System.Drawing.Size(67, 13);
            this.lblScanType.TabIndex = 4;
            this.lblScanType.Text = "Опитування";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 20);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(261, 20);
            this.txtName.TabIndex = 5;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(110, 59);
            this.txtDescription.MaxLength = 200;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(261, 20);
            this.txtDescription.TabIndex = 6;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(110, 139);
            this.txtPhone.Mask = "+38 (000) 000-0000";
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.RejectInputOnFirstFailure = true;
            this.txtPhone.Size = new System.Drawing.Size(261, 20);
            this.txtPhone.TabIndex = 8;
            this.txtPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // numAddress
            // 
            this.numAddress.Location = new System.Drawing.Point(110, 99);
            this.numAddress.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numAddress.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAddress.Name = "numAddress";
            this.numAddress.Size = new System.Drawing.Size(261, 20);
            this.numAddress.TabIndex = 7;
            this.numAddress.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAddress.ValueChanged += new System.EventHandler(this.numAddress_ValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 216);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbDbf
            // 
            this.rbDbf.AutoSize = true;
            this.rbDbf.Checked = true;
            this.rbDbf.Location = new System.Drawing.Point(110, 178);
            this.rbDbf.Name = "rbDbf";
            this.rbDbf.Size = new System.Drawing.Size(88, 17);
            this.rbDbf.TabIndex = 9;
            this.rbDbf.TabStop = true;
            this.rbDbf.Text = "Таблиці DBF";
            this.rbDbf.UseVisualStyleBackColor = true;
            this.rbDbf.CheckedChanged += new System.EventHandler(this.rbDbf_CheckedChanged);
            // 
            // rbGPRS
            // 
            this.rbGPRS.AutoSize = true;
            this.rbGPRS.Enabled = false;
            this.rbGPRS.Location = new System.Drawing.Point(204, 178);
            this.rbGPRS.Name = "rbGPRS";
            this.rbGPRS.Size = new System.Drawing.Size(72, 17);
            this.rbGPRS.TabIndex = 10;
            this.rbGPRS.Text = "По GPRS";
            this.rbGPRS.UseVisualStyleBackColor = true;
            this.rbGPRS.CheckedChanged += new System.EventHandler(this.rbGPRS_CheckedChanged);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            this.err.Icon = ((System.Drawing.Icon)(resources.GetObject("err.Icon")));
            // 
            // EditFloutecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 251);
            this.Controls.Add(this.rbGPRS);
            this.Controls.Add(this.rbDbf);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.numAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblScanType);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 290);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 290);
            this.Name = "EditFloutecForm";
            this.Text = "Додати обчислювач ФЛОУТЕК";
            ((System.ComponentModel.ISupportInitialize)(this.numAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblScanType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.MaskedTextBox txtPhone;
        private System.Windows.Forms.NumericUpDown numAddress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RadioButton rbDbf;
        private System.Windows.Forms.RadioButton rbGPRS;
        private System.Windows.Forms.ErrorProvider err;
    }
}