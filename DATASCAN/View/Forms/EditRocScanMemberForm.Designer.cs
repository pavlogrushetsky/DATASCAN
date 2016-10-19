namespace DATASCAN.View.Forms
{
    partial class EditRocScanMemberForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditRocScanMemberForm));
            this.cbScanEventData = new System.Windows.Forms.CheckBox();
            this.cbScanAlarmData = new System.Windows.Forms.CheckBox();
            this.cbScanMinuteData = new System.Windows.Forms.CheckBox();
            this.cbScanPeriodicData = new System.Windows.Forms.CheckBox();
            this.cbScanDailyData = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.info)).BeginInit();
            this.SuspendLayout();
            // 
            // cbScanEventData
            // 
            this.cbScanEventData.AutoSize = true;
            this.cbScanEventData.Location = new System.Drawing.Point(12, 24);
            this.cbScanEventData.Name = "cbScanEventData";
            this.cbScanEventData.Size = new System.Drawing.Size(78, 17);
            this.cbScanEventData.TabIndex = 1;
            this.cbScanEventData.Text = "Дані подій";
            this.cbScanEventData.UseVisualStyleBackColor = true;
            this.cbScanEventData.CheckedChanged += new System.EventHandler(this.cbScanEventData_CheckedChanged);
            // 
            // cbScanAlarmData
            // 
            this.cbScanAlarmData.AutoSize = true;
            this.cbScanAlarmData.Location = new System.Drawing.Point(12, 47);
            this.cbScanAlarmData.Name = "cbScanAlarmData";
            this.cbScanAlarmData.Size = new System.Drawing.Size(84, 17);
            this.cbScanAlarmData.TabIndex = 2;
            this.cbScanAlarmData.Text = "Дані аварій";
            this.cbScanAlarmData.UseVisualStyleBackColor = true;
            this.cbScanAlarmData.CheckedChanged += new System.EventHandler(this.cbScanAlarmData_CheckedChanged);
            // 
            // cbScanMinuteData
            // 
            this.cbScanMinuteData.AutoSize = true;
            this.cbScanMinuteData.Location = new System.Drawing.Point(12, 70);
            this.cbScanMinuteData.Name = "cbScanMinuteData";
            this.cbScanMinuteData.Size = new System.Drawing.Size(94, 17);
            this.cbScanMinuteData.TabIndex = 3;
            this.cbScanMinuteData.Text = "Хвилинні дані";
            this.cbScanMinuteData.UseVisualStyleBackColor = true;
            this.cbScanMinuteData.CheckedChanged += new System.EventHandler(this.cbScanMinuteData_CheckedChanged);
            // 
            // cbScanPeriodicData
            // 
            this.cbScanPeriodicData.AutoSize = true;
            this.cbScanPeriodicData.Location = new System.Drawing.Point(12, 93);
            this.cbScanPeriodicData.Name = "cbScanPeriodicData";
            this.cbScanPeriodicData.Size = new System.Drawing.Size(102, 17);
            this.cbScanPeriodicData.TabIndex = 4;
            this.cbScanPeriodicData.Text = "Періодичні дані";
            this.cbScanPeriodicData.UseVisualStyleBackColor = true;
            this.cbScanPeriodicData.CheckedChanged += new System.EventHandler(this.cbScanPeriodicData_CheckedChanged);
            // 
            // cbScanDailyData
            // 
            this.cbScanDailyData.AutoSize = true;
            this.cbScanDailyData.Location = new System.Drawing.Point(12, 116);
            this.cbScanDailyData.Name = "cbScanDailyData";
            this.cbScanDailyData.Size = new System.Drawing.Size(84, 17);
            this.cbScanDailyData.TabIndex = 5;
            this.cbScanDailyData.Text = "Добові дані";
            this.cbScanDailyData.UseVisualStyleBackColor = true;
            this.cbScanDailyData.CheckedChanged += new System.EventHandler(this.cbScanDailyData_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 156);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(97, 156);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // info
            // 
            this.info.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.info.ContainerControl = this;
            this.info.Icon = ((System.Drawing.Icon)(resources.GetObject("info.Icon")));
            // 
            // EditRocScanMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 191);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbScanDailyData);
            this.Controls.Add(this.cbScanPeriodicData);
            this.Controls.Add(this.cbScanMinuteData);
            this.Controls.Add(this.cbScanAlarmData);
            this.Controls.Add(this.cbScanEventData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 230);
            this.Name = "EditRocScanMemberForm";
            this.Text = "Опитувати дані";
            ((System.ComponentModel.ISupportInitialize)(this.info)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbScanEventData;
        private System.Windows.Forms.CheckBox cbScanAlarmData;
        private System.Windows.Forms.CheckBox cbScanMinuteData;
        private System.Windows.Forms.CheckBox cbScanPeriodicData;
        private System.Windows.Forms.CheckBox cbScanDailyData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider info;
    }
}