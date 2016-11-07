namespace DATASCAN.View.Forms
{
    partial class ConnectionSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionSettingsForm));
            this.gbDbf = new System.Windows.Forms.GroupBox();
            this.btnDbfPath = new System.Windows.Forms.Button();
            this.txtDbfPath = new System.Windows.Forms.TextBox();
            this.lblDbfPath = new System.Windows.Forms.Label();
            this.gbGPRS = new System.Windows.Forms.GroupBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblReadDelay = new System.Windows.Forms.Label();
            this.numReadDelay = new System.Windows.Forms.NumericUpDown();
            this.lblWriteDelay = new System.Windows.Forms.Label();
            this.numWriteDelay = new System.Windows.Forms.NumericUpDown();
            this.lblRetries = new System.Windows.Forms.Label();
            this.numRetries = new System.Windows.Forms.NumericUpDown();
            this.lblSettings = new System.Windows.Forms.Label();
            this.lblStopBits = new System.Windows.Forms.Label();
            this.lblDataBits = new System.Windows.Forms.Label();
            this.lblParity = new System.Windows.Forms.Label();
            this.lblBaudrate = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.lblPorts = new System.Windows.Forms.Label();
            this.cbPort3 = new System.Windows.Forms.ComboBox();
            this.cbPort2 = new System.Windows.Forms.ComboBox();
            this.cbPort1 = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblWaitingTime = new System.Windows.Forms.Label();
            this.numWaitingTime = new System.Windows.Forms.NumericUpDown();
            this.gbDbf.SuspendLayout();
            this.gbGPRS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingTime)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDbf
            // 
            this.gbDbf.Controls.Add(this.btnDbfPath);
            this.gbDbf.Controls.Add(this.txtDbfPath);
            this.gbDbf.Controls.Add(this.lblDbfPath);
            this.gbDbf.Location = new System.Drawing.Point(13, 13);
            this.gbDbf.Name = "gbDbf";
            this.gbDbf.Size = new System.Drawing.Size(379, 62);
            this.gbDbf.TabIndex = 0;
            this.gbDbf.TabStop = false;
            this.gbDbf.Text = "Таблиці DBF";
            // 
            // btnDbfPath
            // 
            this.btnDbfPath.Location = new System.Drawing.Point(348, 25);
            this.btnDbfPath.Name = "btnDbfPath";
            this.btnDbfPath.Size = new System.Drawing.Size(25, 23);
            this.btnDbfPath.TabIndex = 2;
            this.btnDbfPath.Text = "...";
            this.btnDbfPath.UseVisualStyleBackColor = true;
            this.btnDbfPath.Click += new System.EventHandler(this.btnDbfPath_Click);
            // 
            // txtDbfPath
            // 
            this.txtDbfPath.Location = new System.Drawing.Point(129, 27);
            this.txtDbfPath.Name = "txtDbfPath";
            this.txtDbfPath.ReadOnly = true;
            this.txtDbfPath.Size = new System.Drawing.Size(213, 20);
            this.txtDbfPath.TabIndex = 1;
            this.txtDbfPath.TextChanged += new System.EventHandler(this.txtDbfPath_TextChanged);
            // 
            // lblDbfPath
            // 
            this.lblDbfPath.AutoSize = true;
            this.lblDbfPath.Location = new System.Drawing.Point(7, 30);
            this.lblDbfPath.Name = "lblDbfPath";
            this.lblDbfPath.Size = new System.Drawing.Size(116, 13);
            this.lblDbfPath.TabIndex = 0;
            this.lblDbfPath.Text = "Шлях до таблиць DBF";
            // 
            // gbGPRS
            // 
            this.gbGPRS.Controls.Add(this.lblWaitingTime);
            this.gbGPRS.Controls.Add(this.numWaitingTime);
            this.gbGPRS.Controls.Add(this.lblTimeout);
            this.gbGPRS.Controls.Add(this.numTimeout);
            this.gbGPRS.Controls.Add(this.lblReadDelay);
            this.gbGPRS.Controls.Add(this.numReadDelay);
            this.gbGPRS.Controls.Add(this.lblWriteDelay);
            this.gbGPRS.Controls.Add(this.numWriteDelay);
            this.gbGPRS.Controls.Add(this.lblRetries);
            this.gbGPRS.Controls.Add(this.numRetries);
            this.gbGPRS.Controls.Add(this.lblSettings);
            this.gbGPRS.Controls.Add(this.lblStopBits);
            this.gbGPRS.Controls.Add(this.lblDataBits);
            this.gbGPRS.Controls.Add(this.lblParity);
            this.gbGPRS.Controls.Add(this.lblBaudrate);
            this.gbGPRS.Controls.Add(this.cbStopBits);
            this.gbGPRS.Controls.Add(this.cbDataBits);
            this.gbGPRS.Controls.Add(this.cbParity);
            this.gbGPRS.Controls.Add(this.cbBaudrate);
            this.gbGPRS.Controls.Add(this.lblPorts);
            this.gbGPRS.Controls.Add(this.cbPort3);
            this.gbGPRS.Controls.Add(this.cbPort2);
            this.gbGPRS.Controls.Add(this.cbPort1);
            this.gbGPRS.Location = new System.Drawing.Point(13, 81);
            this.gbGPRS.Name = "gbGPRS";
            this.gbGPRS.Size = new System.Drawing.Size(379, 400);
            this.gbGPRS.TabIndex = 1;
            this.gbGPRS.TabStop = false;
            this.gbGPRS.Text = "GPRS";
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Location = new System.Drawing.Point(176, 256);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(130, 13);
            this.lblTimeout.TabIndex = 23;
            this.lblTimeout.Text = "Таймаут з\'єднання, сек.";
            // 
            // numTimeout
            // 
            this.numTimeout.Location = new System.Drawing.Point(325, 254);
            this.numTimeout.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.Size = new System.Drawing.Size(48, 20);
            this.numTimeout.TabIndex = 22;
            this.numTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTimeout.ValueChanged += new System.EventHandler(this.numTimeout_ValueChanged);
            // 
            // lblReadDelay
            // 
            this.lblReadDelay.AutoSize = true;
            this.lblReadDelay.Location = new System.Drawing.Point(176, 373);
            this.lblReadDelay.Name = "lblReadDelay";
            this.lblReadDelay.Size = new System.Drawing.Size(127, 13);
            this.lblReadDelay.TabIndex = 21;
            this.lblReadDelay.Text = "Затримка читання, сек.";
            // 
            // numReadDelay
            // 
            this.numReadDelay.Location = new System.Drawing.Point(325, 371);
            this.numReadDelay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numReadDelay.Name = "numReadDelay";
            this.numReadDelay.Size = new System.Drawing.Size(48, 20);
            this.numReadDelay.TabIndex = 20;
            this.numReadDelay.ValueChanged += new System.EventHandler(this.numReadDelay_ValueChanged);
            // 
            // lblWriteDelay
            // 
            this.lblWriteDelay.AutoSize = true;
            this.lblWriteDelay.Location = new System.Drawing.Point(176, 334);
            this.lblWriteDelay.Name = "lblWriteDelay";
            this.lblWriteDelay.Size = new System.Drawing.Size(121, 13);
            this.lblWriteDelay.TabIndex = 19;
            this.lblWriteDelay.Text = "Затримка запиту, сек.";
            // 
            // numWriteDelay
            // 
            this.numWriteDelay.Location = new System.Drawing.Point(325, 332);
            this.numWriteDelay.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWriteDelay.Name = "numWriteDelay";
            this.numWriteDelay.Size = new System.Drawing.Size(48, 20);
            this.numWriteDelay.TabIndex = 18;
            this.numWriteDelay.ValueChanged += new System.EventHandler(this.numWriteDelay_ValueChanged);
            // 
            // lblRetries
            // 
            this.lblRetries.AutoSize = true;
            this.lblRetries.Location = new System.Drawing.Point(176, 217);
            this.lblRetries.Name = "lblRetries";
            this.lblRetries.Size = new System.Drawing.Size(86, 13);
            this.lblRetries.TabIndex = 17;
            this.lblRetries.Text = "Кількість спроб";
            // 
            // numRetries
            // 
            this.numRetries.Location = new System.Drawing.Point(325, 215);
            this.numRetries.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numRetries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRetries.Name = "numRetries";
            this.numRetries.Size = new System.Drawing.Size(48, 20);
            this.numRetries.TabIndex = 16;
            this.numRetries.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRetries.ValueChanged += new System.EventHandler(this.numRetries_ValueChanged);
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.Location = new System.Drawing.Point(176, 26);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(81, 13);
            this.lblSettings.TabIndex = 14;
            this.lblSettings.Text = "Налаштування";
            // 
            // lblStopBits
            // 
            this.lblStopBits.AutoSize = true;
            this.lblStopBits.Location = new System.Drawing.Point(176, 178);
            this.lblStopBits.Name = "lblStopBits";
            this.lblStopBits.Size = new System.Drawing.Size(48, 13);
            this.lblStopBits.TabIndex = 12;
            this.lblStopBits.Text = "Stop bits";
            // 
            // lblDataBits
            // 
            this.lblDataBits.AutoSize = true;
            this.lblDataBits.Location = new System.Drawing.Point(176, 137);
            this.lblDataBits.Name = "lblDataBits";
            this.lblDataBits.Size = new System.Drawing.Size(49, 13);
            this.lblDataBits.TabIndex = 11;
            this.lblDataBits.Text = "Data bits";
            // 
            // lblParity
            // 
            this.lblParity.AutoSize = true;
            this.lblParity.Location = new System.Drawing.Point(176, 96);
            this.lblParity.Name = "lblParity";
            this.lblParity.Size = new System.Drawing.Size(33, 13);
            this.lblParity.TabIndex = 10;
            this.lblParity.Text = "Parity";
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.AutoSize = true;
            this.lblBaudrate.Location = new System.Drawing.Point(176, 55);
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(50, 13);
            this.lblBaudrate.TabIndex = 9;
            this.lblBaudrate.Text = "Baudrate";
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(232, 175);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(141, 21);
            this.cbStopBits.TabIndex = 7;
            this.cbStopBits.SelectedIndexChanged += new System.EventHandler(this.cbStopBits_SelectedIndexChanged);
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(232, 134);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(141, 21);
            this.cbDataBits.TabIndex = 6;
            this.cbDataBits.SelectedIndexChanged += new System.EventHandler(this.cbDataBits_SelectedIndexChanged);
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(232, 93);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(141, 21);
            this.cbParity.TabIndex = 5;
            this.cbParity.SelectedIndexChanged += new System.EventHandler(this.cbParity_SelectedIndexChanged);
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Location = new System.Drawing.Point(232, 52);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(141, 21);
            this.cbBaudrate.TabIndex = 4;
            this.cbBaudrate.SelectedIndexChanged += new System.EventHandler(this.cbBaudrate_SelectedIndexChanged);
            // 
            // lblPorts
            // 
            this.lblPorts.AutoSize = true;
            this.lblPorts.Location = new System.Drawing.Point(6, 26);
            this.lblPorts.Name = "lblPorts";
            this.lblPorts.Size = new System.Drawing.Size(63, 13);
            this.lblPorts.TabIndex = 3;
            this.lblPorts.Text = "COM-порти";
            // 
            // cbPort3
            // 
            this.cbPort3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort3.FormattingEnabled = true;
            this.cbPort3.Location = new System.Drawing.Point(9, 134);
            this.cbPort3.Name = "cbPort3";
            this.cbPort3.Size = new System.Drawing.Size(123, 21);
            this.cbPort3.TabIndex = 2;
            this.cbPort3.DropDown += new System.EventHandler(this.cbPort3_DropDown);
            this.cbPort3.SelectedIndexChanged += new System.EventHandler(this.cbPort3_SelectedIndexChanged);
            // 
            // cbPort2
            // 
            this.cbPort2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort2.FormattingEnabled = true;
            this.cbPort2.Location = new System.Drawing.Point(9, 93);
            this.cbPort2.Name = "cbPort2";
            this.cbPort2.Size = new System.Drawing.Size(123, 21);
            this.cbPort2.TabIndex = 1;
            this.cbPort2.DropDown += new System.EventHandler(this.cbPort2_DropDown);
            this.cbPort2.SelectedIndexChanged += new System.EventHandler(this.cbPort2_SelectedIndexChanged);
            // 
            // cbPort1
            // 
            this.cbPort1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort1.FormattingEnabled = true;
            this.cbPort1.Location = new System.Drawing.Point(9, 52);
            this.cbPort1.Name = "cbPort1";
            this.cbPort1.Size = new System.Drawing.Size(123, 21);
            this.cbPort1.TabIndex = 0;
            this.cbPort1.DropDown += new System.EventHandler(this.cbPort1_DropDown);
            this.cbPort1.SelectedIndexChanged += new System.EventHandler(this.cbPort1_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(317, 506);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(236, 506);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblWaitingTime
            // 
            this.lblWaitingTime.AutoSize = true;
            this.lblWaitingTime.Location = new System.Drawing.Point(176, 295);
            this.lblWaitingTime.Name = "lblWaitingTime";
            this.lblWaitingTime.Size = new System.Drawing.Size(143, 13);
            this.lblWaitingTime.TabIndex = 25;
            this.lblWaitingTime.Text = "Очікування з\'єднання, сек.";
            // 
            // numWaitingTime
            // 
            this.numWaitingTime.Location = new System.Drawing.Point(325, 293);
            this.numWaitingTime.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numWaitingTime.Name = "numWaitingTime";
            this.numWaitingTime.Size = new System.Drawing.Size(48, 20);
            this.numWaitingTime.TabIndex = 24;
            this.numWaitingTime.ValueChanged += new System.EventHandler(this.numWaitingTime_ValueChanged);
            // 
            // ConnectionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 541);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbGPRS);
            this.Controls.Add(this.gbDbf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 580);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 580);
            this.Name = "ConnectionSettingsForm";
            this.Text = "Налаштування підключення";
            this.gbDbf.ResumeLayout(false);
            this.gbDbf.PerformLayout();
            this.gbGPRS.ResumeLayout(false);
            this.gbGPRS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRetries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDbf;
        private System.Windows.Forms.Button btnDbfPath;
        private System.Windows.Forms.TextBox txtDbfPath;
        private System.Windows.Forms.Label lblDbfPath;
        private System.Windows.Forms.GroupBox gbGPRS;
        private System.Windows.Forms.ComboBox cbPort1;
        private System.Windows.Forms.Label lblPorts;
        private System.Windows.Forms.ComboBox cbPort3;
        private System.Windows.Forms.ComboBox cbPort2;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Label lblStopBits;
        private System.Windows.Forms.Label lblDataBits;
        private System.Windows.Forms.Label lblParity;
        private System.Windows.Forms.Label lblBaudrate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblWriteDelay;
        private System.Windows.Forms.NumericUpDown numWriteDelay;
        private System.Windows.Forms.Label lblRetries;
        private System.Windows.Forms.NumericUpDown numRetries;
        private System.Windows.Forms.Label lblReadDelay;
        private System.Windows.Forms.NumericUpDown numReadDelay;
        private System.Windows.Forms.Label lblTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.Label lblWaitingTime;
        private System.Windows.Forms.NumericUpDown numWaitingTime;
    }
}