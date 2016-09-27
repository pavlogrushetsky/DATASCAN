namespace DATASCAN.View.Forms
{
    partial class EditRocForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRocForm));
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.gbDeviceAddress = new System.Windows.Forms.GroupBox();
            this.numRocGroup = new System.Windows.Forms.NumericUpDown();
            this.numRocUnit = new System.Windows.Forms.NumericUpDown();
            this.lblRocGroup = new System.Windows.Forms.Label();
            this.lblRocUnit = new System.Windows.Forms.Label();
            this.gbHostAddress = new System.Windows.Forms.GroupBox();
            this.numHostGroup = new System.Windows.Forms.NumericUpDown();
            this.numHostUnit = new System.Windows.Forms.NumericUpDown();
            this.lblHostGroup = new System.Windows.Forms.Label();
            this.lblHostUnit = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.MaskedTextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.MaskedTextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.gpTcpIp = new System.Windows.Forms.GroupBox();
            this.gbGPRS = new System.Windows.Forms.GroupBox();
            this.rbGPRS = new System.Windows.Forms.RadioButton();
            this.rbTCPIP = new System.Windows.Forms.RadioButton();
            this.lblScanType = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbDeviceAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRocGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRocUnit)).BeginInit();
            this.gbHostAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHostGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.gpTcpIp.SuspendLayout();
            this.gbGPRS.SuspendLayout();
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
            // gbDeviceAddress
            // 
            this.gbDeviceAddress.Controls.Add(this.numRocGroup);
            this.gbDeviceAddress.Controls.Add(this.numRocUnit);
            this.gbDeviceAddress.Controls.Add(this.lblRocGroup);
            this.gbDeviceAddress.Controls.Add(this.lblRocUnit);
            this.gbDeviceAddress.Location = new System.Drawing.Point(15, 100);
            this.gbDeviceAddress.Name = "gbDeviceAddress";
            this.gbDeviceAddress.Size = new System.Drawing.Size(196, 75);
            this.gbDeviceAddress.TabIndex = 2;
            this.gbDeviceAddress.TabStop = false;
            this.gbDeviceAddress.Text = "Адреса обчислювача";
            // 
            // numRocGroup
            // 
            this.numRocGroup.Location = new System.Drawing.Point(68, 45);
            this.numRocGroup.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.numRocGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRocGroup.Name = "numRocGroup";
            this.numRocGroup.Size = new System.Drawing.Size(105, 20);
            this.numRocGroup.TabIndex = 3;
            this.numRocGroup.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRocGroup.ValueChanged += new System.EventHandler(this.numRocGroup_ValueChanged);
            // 
            // numRocUnit
            // 
            this.numRocUnit.Location = new System.Drawing.Point(68, 19);
            this.numRocUnit.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.numRocUnit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRocUnit.Name = "numRocUnit";
            this.numRocUnit.Size = new System.Drawing.Size(105, 20);
            this.numRocUnit.TabIndex = 2;
            this.numRocUnit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRocUnit.ValueChanged += new System.EventHandler(this.numRocUnit_ValueChanged);
            // 
            // lblRocGroup
            // 
            this.lblRocGroup.AutoSize = true;
            this.lblRocGroup.Location = new System.Drawing.Point(6, 47);
            this.lblRocGroup.Name = "lblRocGroup";
            this.lblRocGroup.Size = new System.Drawing.Size(36, 13);
            this.lblRocGroup.TabIndex = 1;
            this.lblRocGroup.Text = "Group";
            // 
            // lblRocUnit
            // 
            this.lblRocUnit.AutoSize = true;
            this.lblRocUnit.Location = new System.Drawing.Point(6, 21);
            this.lblRocUnit.Name = "lblRocUnit";
            this.lblRocUnit.Size = new System.Drawing.Size(26, 13);
            this.lblRocUnit.TabIndex = 0;
            this.lblRocUnit.Text = "Unit";
            // 
            // gbHostAddress
            // 
            this.gbHostAddress.Controls.Add(this.numHostGroup);
            this.gbHostAddress.Controls.Add(this.numHostUnit);
            this.gbHostAddress.Controls.Add(this.lblHostGroup);
            this.gbHostAddress.Controls.Add(this.lblHostUnit);
            this.gbHostAddress.Location = new System.Drawing.Point(217, 100);
            this.gbHostAddress.Name = "gbHostAddress";
            this.gbHostAddress.Size = new System.Drawing.Size(195, 75);
            this.gbHostAddress.TabIndex = 3;
            this.gbHostAddress.TabStop = false;
            this.gbHostAddress.Text = "Адреса хоста";
            // 
            // numHostGroup
            // 
            this.numHostGroup.Location = new System.Drawing.Point(64, 45);
            this.numHostGroup.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.numHostGroup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHostGroup.Name = "numHostGroup";
            this.numHostGroup.Size = new System.Drawing.Size(111, 20);
            this.numHostGroup.TabIndex = 3;
            this.numHostGroup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHostGroup.ValueChanged += new System.EventHandler(this.numHostGroup_ValueChanged);
            // 
            // numHostUnit
            // 
            this.numHostUnit.Location = new System.Drawing.Point(64, 19);
            this.numHostUnit.Maximum = new decimal(new int[] {
            239,
            0,
            0,
            0});
            this.numHostUnit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHostUnit.Name = "numHostUnit";
            this.numHostUnit.Size = new System.Drawing.Size(111, 20);
            this.numHostUnit.TabIndex = 2;
            this.numHostUnit.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numHostUnit.ValueChanged += new System.EventHandler(this.numHostUnit_ValueChanged);
            // 
            // lblHostGroup
            // 
            this.lblHostGroup.AutoSize = true;
            this.lblHostGroup.Location = new System.Drawing.Point(6, 47);
            this.lblHostGroup.Name = "lblHostGroup";
            this.lblHostGroup.Size = new System.Drawing.Size(36, 13);
            this.lblHostGroup.TabIndex = 1;
            this.lblHostGroup.Text = "Group";
            // 
            // lblHostUnit
            // 
            this.lblHostUnit.AutoSize = true;
            this.lblHostUnit.Location = new System.Drawing.Point(6, 21);
            this.lblHostUnit.Name = "lblHostUnit";
            this.lblHostUnit.Size = new System.Drawing.Size(26, 13);
            this.lblHostUnit.TabIndex = 0;
            this.lblHostUnit.Text = "Unit";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(83, 20);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(309, 20);
            this.txtName.TabIndex = 4;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(83, 59);
            this.txtDescription.MaxLength = 200;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(309, 20);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(6, 22);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(56, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "IP-адреса";
            // 
            // txtAddress
            // 
            this.txtAddress.Culture = new System.Globalization.CultureInfo("en-US");
            this.txtAddress.Location = new System.Drawing.Point(68, 19);
            this.txtAddress.Mask = "099.099.099.099";
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.RejectInputOnFirstFailure = true;
            this.txtAddress.Size = new System.Drawing.Size(105, 20);
            this.txtAddress.TabIndex = 7;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 47);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 13);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "Порт";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(64, 19);
            this.txtPhone.Mask = "+38 (000) 000-0000";
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.RejectInputOnFirstFailure = true;
            this.txtPhone.Size = new System.Drawing.Size(111, 20);
            this.txtPhone.TabIndex = 9;
            this.txtPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(6, 22);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(52, 13);
            this.lblPhone.TabIndex = 10;
            this.lblPhone.Text = "Телефон";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(68, 45);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(105, 20);
            this.numPort.TabIndex = 11;
            this.numPort.Value = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numPort.ValueChanged += new System.EventHandler(this.numPort_ValueChanged);
            // 
            // gpTcpIp
            // 
            this.gpTcpIp.Controls.Add(this.txtAddress);
            this.gpTcpIp.Controls.Add(this.numPort);
            this.gpTcpIp.Controls.Add(this.lblAddress);
            this.gpTcpIp.Controls.Add(this.lblPort);
            this.gpTcpIp.Location = new System.Drawing.Point(15, 231);
            this.gpTcpIp.Name = "gpTcpIp";
            this.gpTcpIp.Size = new System.Drawing.Size(196, 75);
            this.gpTcpIp.TabIndex = 12;
            this.gpTcpIp.TabStop = false;
            this.gpTcpIp.Text = "TCP/IP";
            // 
            // gbGPRS
            // 
            this.gbGPRS.Controls.Add(this.txtPhone);
            this.gbGPRS.Controls.Add(this.lblPhone);
            this.gbGPRS.Location = new System.Drawing.Point(217, 231);
            this.gbGPRS.Name = "gbGPRS";
            this.gbGPRS.Size = new System.Drawing.Size(195, 75);
            this.gbGPRS.TabIndex = 13;
            this.gbGPRS.TabStop = false;
            this.gbGPRS.Text = "GPRS";
            // 
            // rbGPRS
            // 
            this.rbGPRS.AutoSize = true;
            this.rbGPRS.Location = new System.Drawing.Point(281, 197);
            this.rbGPRS.Name = "rbGPRS";
            this.rbGPRS.Size = new System.Drawing.Size(72, 17);
            this.rbGPRS.TabIndex = 18;
            this.rbGPRS.Text = "По GPRS";
            this.rbGPRS.UseVisualStyleBackColor = true;
            this.rbGPRS.CheckedChanged += new System.EventHandler(this.rbGPRS_CheckedChanged);
            // 
            // rbTCPIP
            // 
            this.rbTCPIP.AutoSize = true;
            this.rbTCPIP.Checked = true;
            this.rbTCPIP.Location = new System.Drawing.Point(83, 197);
            this.rbTCPIP.Name = "rbTCPIP";
            this.rbTCPIP.Size = new System.Drawing.Size(78, 17);
            this.rbTCPIP.TabIndex = 19;
            this.rbTCPIP.TabStop = true;
            this.rbTCPIP.Text = "По TCP/IP";
            this.rbTCPIP.UseVisualStyleBackColor = true;
            this.rbTCPIP.CheckedChanged += new System.EventHandler(this.rbTCPIP_CheckedChanged);
            // 
            // lblScanType
            // 
            this.lblScanType.AutoSize = true;
            this.lblScanType.Location = new System.Drawing.Point(12, 199);
            this.lblScanType.Name = "lblScanType";
            this.lblScanType.Size = new System.Drawing.Size(67, 13);
            this.lblScanType.TabIndex = 20;
            this.lblScanType.Text = "Опитування";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(317, 326);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(236, 326);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 22;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            this.err.Icon = ((System.Drawing.Icon)(resources.GetObject("err.Icon")));
            // 
            // EditRocForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 361);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblScanType);
            this.Controls.Add(this.rbTCPIP);
            this.Controls.Add(this.rbGPRS);
            this.Controls.Add(this.gbGPRS);
            this.Controls.Add(this.gpTcpIp);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.gbHostAddress);
            this.Controls.Add(this.gbDeviceAddress);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 400);
            this.Name = "EditRocForm";
            this.Text = "Додати обчислювач ROC809";
            this.gbDeviceAddress.ResumeLayout(false);
            this.gbDeviceAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRocGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRocUnit)).EndInit();
            this.gbHostAddress.ResumeLayout(false);
            this.gbHostAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHostGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.gpTcpIp.ResumeLayout(false);
            this.gpTcpIp.PerformLayout();
            this.gbGPRS.ResumeLayout(false);
            this.gbGPRS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox gbDeviceAddress;
        private System.Windows.Forms.NumericUpDown numRocUnit;
        private System.Windows.Forms.Label lblRocGroup;
        private System.Windows.Forms.Label lblRocUnit;
        private System.Windows.Forms.GroupBox gbHostAddress;
        private System.Windows.Forms.Label lblHostGroup;
        private System.Windows.Forms.Label lblHostUnit;
        private System.Windows.Forms.NumericUpDown numRocGroup;
        private System.Windows.Forms.NumericUpDown numHostGroup;
        private System.Windows.Forms.NumericUpDown numHostUnit;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.MaskedTextBox txtAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.MaskedTextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.GroupBox gpTcpIp;
        private System.Windows.Forms.GroupBox gbGPRS;
        private System.Windows.Forms.RadioButton rbGPRS;
        private System.Windows.Forms.RadioButton rbTCPIP;
        private System.Windows.Forms.Label lblScanType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider err;
    }
}