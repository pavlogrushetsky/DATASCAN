﻿namespace DATASCAN.View.Forms
{
    partial class EditFloutecLineForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFloutecLineForm));
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblSensorType = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.numNumber = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblNameError = new System.Windows.Forms.Label();
            this.lblNumberError = new System.Windows.Forms.Label();
            this.cmbSensorType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Назва";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 83);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(33, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Опис";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(12, 132);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(41, 13);
            this.lblNumber.TabIndex = 2;
            this.lblNumber.Text = "Номер";
            // 
            // lblSensorType
            // 
            this.lblSensorType.AutoSize = true;
            this.lblSensorType.Location = new System.Drawing.Point(185, 132);
            this.lblSensorType.Name = "lblSensorType";
            this.lblSensorType.Size = new System.Drawing.Size(71, 13);
            this.lblSensorType.TabIndex = 3;
            this.lblSensorType.Text = "Тип сенсора";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 30);
            this.txtName.MaxLength = 50;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(333, 20);
            this.txtName.TabIndex = 4;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(59, 80);
            this.txtDescription.MaxLength = 200;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(333, 20);
            this.txtDescription.TabIndex = 5;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // numNumber
            // 
            this.numNumber.Location = new System.Drawing.Point(59, 130);
            this.numNumber.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumber.Name = "numNumber";
            this.numNumber.Size = new System.Drawing.Size(120, 20);
            this.numNumber.TabIndex = 6;
            this.numNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumber.ValueChanged += new System.EventHandler(this.numNumber_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(236, 176);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(317, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblNameError
            // 
            this.lblNameError.AutoSize = true;
            this.lblNameError.ForeColor = System.Drawing.Color.Red;
            this.lblNameError.Location = new System.Drawing.Point(56, 14);
            this.lblNameError.Name = "lblNameError";
            this.lblNameError.Size = new System.Drawing.Size(111, 13);
            this.lblNameError.TabIndex = 10;
            this.lblNameError.Text = "Вкажіть назву нитки";
            this.lblNameError.Visible = false;
            // 
            // lblNumberError
            // 
            this.lblNumberError.AutoSize = true;
            this.lblNumberError.ForeColor = System.Drawing.Color.Red;
            this.lblNumberError.Location = new System.Drawing.Point(56, 114);
            this.lblNumberError.Name = "lblNumberError";
            this.lblNumberError.Size = new System.Drawing.Size(181, 13);
            this.lblNumberError.TabIndex = 11;
            this.lblNumberError.Text = "Номер нитки має бути унікальним";
            this.lblNumberError.Visible = false;
            // 
            // cmbSensorType
            // 
            this.cmbSensorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorType.Location = new System.Drawing.Point(262, 129);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new System.Drawing.Size(130, 21);
            this.cmbSensorType.TabIndex = 7;
            this.cmbSensorType.SelectedIndexChanged += new System.EventHandler(this.cmbSensorType_SelectedIndexChanged);
            // 
            // EditFloutecLineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 211);
            this.Controls.Add(this.cmbSensorType);
            this.Controls.Add(this.lblNumberError);
            this.Controls.Add(this.lblNameError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numNumber);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblSensorType);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 250);
            this.Name = "EditFloutecLineForm";
            this.Text = "Додати вимірювальну нитку";
            ((System.ComponentModel.ISupportInitialize)(this.numNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblSensorType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown numNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNameError;
        private System.Windows.Forms.Label lblNumberError;
        private System.Windows.Forms.ComboBox cmbSensorType;
    }
}