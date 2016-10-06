namespace DATASCAN.View.Forms
{
    partial class EditFloutecScanMemberForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFloutecScanMemberForm));
            this.cbScanIdentData = new System.Windows.Forms.CheckBox();
            this.cbScanAlarmData = new System.Windows.Forms.CheckBox();
            this.cbScanInterData = new System.Windows.Forms.CheckBox();
            this.cbScanInstantData = new System.Windows.Forms.CheckBox();
            this.cbScanHourlyData = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbScanIdentData
            // 
            this.cbScanIdentData.AutoSize = true;
            this.cbScanIdentData.Location = new System.Drawing.Point(12, 24);
            this.cbScanIdentData.Name = "cbScanIdentData";
            this.cbScanIdentData.Size = new System.Drawing.Size(116, 17);
            this.cbScanIdentData.TabIndex = 1;
            this.cbScanIdentData.Text = "Дані ідентифікації";
            this.cbScanIdentData.UseVisualStyleBackColor = true;
            this.cbScanIdentData.CheckedChanged += new System.EventHandler(this.cbScanIdentData_CheckedChanged);
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
            // cbScanInterData
            // 
            this.cbScanInterData.AutoSize = true;
            this.cbScanInterData.Location = new System.Drawing.Point(12, 70);
            this.cbScanInterData.Name = "cbScanInterData";
            this.cbScanInterData.Size = new System.Drawing.Size(97, 17);
            this.cbScanInterData.TabIndex = 3;
            this.cbScanInterData.Text = "Дані втручань";
            this.cbScanInterData.UseVisualStyleBackColor = true;
            this.cbScanInterData.CheckedChanged += new System.EventHandler(this.cbScanInterData_CheckedChanged);
            // 
            // cbScanInstantData
            // 
            this.cbScanInstantData.AutoSize = true;
            this.cbScanInstantData.Location = new System.Drawing.Point(12, 93);
            this.cbScanInstantData.Name = "cbScanInstantData";
            this.cbScanInstantData.Size = new System.Drawing.Size(88, 17);
            this.cbScanInstantData.TabIndex = 4;
            this.cbScanInstantData.Text = "Миттєві дані";
            this.cbScanInstantData.UseVisualStyleBackColor = true;
            this.cbScanInstantData.CheckedChanged += new System.EventHandler(this.cbScanInstantData_CheckedChanged);
            // 
            // cbScanHourlyData
            // 
            this.cbScanHourlyData.AutoSize = true;
            this.cbScanHourlyData.Location = new System.Drawing.Point(12, 116);
            this.cbScanHourlyData.Name = "cbScanHourlyData";
            this.cbScanHourlyData.Size = new System.Drawing.Size(87, 17);
            this.cbScanHourlyData.TabIndex = 5;
            this.cbScanHourlyData.Text = "Годинні дані";
            this.cbScanHourlyData.UseVisualStyleBackColor = true;
            this.cbScanHourlyData.CheckedChanged += new System.EventHandler(this.cbScanHourlyData_CheckedChanged);
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
            // EditFloutecScanMemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 191);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbScanHourlyData);
            this.Controls.Add(this.cbScanInstantData);
            this.Controls.Add(this.cbScanInterData);
            this.Controls.Add(this.cbScanAlarmData);
            this.Controls.Add(this.cbScanIdentData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(200, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(200, 230);
            this.Name = "EditFloutecScanMemberForm";
            this.Text = "Опитувати дані";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbScanIdentData;
        private System.Windows.Forms.CheckBox cbScanAlarmData;
        private System.Windows.Forms.CheckBox cbScanInterData;
        private System.Windows.Forms.CheckBox cbScanInstantData;
        private System.Windows.Forms.CheckBox cbScanHourlyData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}