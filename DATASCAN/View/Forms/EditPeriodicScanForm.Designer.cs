namespace DATASCAN.View.Forms
{
    partial class EditPeriodicScanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPeriodicScanForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblPeriod = new System.Windows.Forms.Label();
            this.numPeriod = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbMinutes = new System.Windows.Forms.RadioButton();
            this.rbHours = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(39, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Назва";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(59, 20);
            this.txtTitle.MaxLength = 200;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(312, 20);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            this.err.Icon = ((System.Drawing.Icon)(resources.GetObject("err.Icon")));
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 62);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 13);
            this.lblPeriod.TabIndex = 1;
            this.lblPeriod.Text = "Період";
            // 
            // numPeriod
            // 
            this.numPeriod.Location = new System.Drawing.Point(59, 60);
            this.numPeriod.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPeriod.Name = "numPeriod";
            this.numPeriod.Size = new System.Drawing.Size(119, 20);
            this.numPeriod.TabIndex = 3;
            this.numPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPeriod.ValueChanged += new System.EventHandler(this.numPeriod_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 106);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbMinutes
            // 
            this.rbMinutes.AutoSize = true;
            this.rbMinutes.Checked = true;
            this.rbMinutes.Location = new System.Drawing.Point(215, 60);
            this.rbMinutes.Name = "rbMinutes";
            this.rbMinutes.Size = new System.Drawing.Size(60, 17);
            this.rbMinutes.TabIndex = 4;
            this.rbMinutes.TabStop = true;
            this.rbMinutes.Text = "хвилин";
            this.rbMinutes.UseVisualStyleBackColor = true;
            this.rbMinutes.CheckedChanged += new System.EventHandler(this.rbMinutes_CheckedChanged);
            // 
            // rbHours
            // 
            this.rbHours.AutoSize = true;
            this.rbHours.Location = new System.Drawing.Point(296, 60);
            this.rbHours.Name = "rbHours";
            this.rbHours.Size = new System.Drawing.Size(54, 17);
            this.rbHours.TabIndex = 5;
            this.rbHours.Text = "годин";
            this.rbHours.UseVisualStyleBackColor = true;
            // 
            // EditPeriodicScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 141);
            this.Controls.Add(this.rbHours);
            this.Controls.Add(this.rbMinutes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numPeriod);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 180);
            this.Name = "EditPeriodicScanForm";
            this.Text = "Додати періодичне опитування";
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numPeriod;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.RadioButton rbHours;
        private System.Windows.Forms.RadioButton rbMinutes;
    }
}