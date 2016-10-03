namespace DATASCAN.View.Forms
{
    partial class EditScheduledScanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditScheduledScanForm));
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.err = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAddPeriod = new System.Windows.Forms.Button();
            this.btnDeletePeriod = new System.Windows.Forms.Button();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.txtPeriod = new System.Windows.Forms.MaskedTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblPeriods = new System.Windows.Forms.Label();
            this.lstPeriods = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.err)).BeginInit();
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
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // err
            // 
            this.err.ContainerControl = this;
            this.err.Icon = ((System.Drawing.Icon)(resources.GetObject("err.Icon")));
            // 
            // btnAddPeriod
            // 
            this.btnAddPeriod.BackgroundImage = global::DATASCAN.Properties.Resources.AddPeriod;
            this.btnAddPeriod.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddPeriod.Location = new System.Drawing.Point(215, 97);
            this.btnAddPeriod.Name = "btnAddPeriod";
            this.btnAddPeriod.Size = new System.Drawing.Size(30, 30);
            this.btnAddPeriod.TabIndex = 3;
            this.btnAddPeriod.UseVisualStyleBackColor = true;
            this.btnAddPeriod.Click += new System.EventHandler(this.btnAddPeriod_Click);
            // 
            // btnDeletePeriod
            // 
            this.btnDeletePeriod.BackgroundImage = global::DATASCAN.Properties.Resources.RemovePeriod;
            this.btnDeletePeriod.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDeletePeriod.Enabled = false;
            this.btnDeletePeriod.Location = new System.Drawing.Point(215, 136);
            this.btnDeletePeriod.Name = "btnDeletePeriod";
            this.btnDeletePeriod.Size = new System.Drawing.Size(30, 30);
            this.btnDeletePeriod.TabIndex = 4;
            this.btnDeletePeriod.UseVisualStyleBackColor = true;
            this.btnDeletePeriod.Click += new System.EventHandler(this.btnDeletePeriod_Click);
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 60);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 13);
            this.lblPeriod.TabIndex = 5;
            this.lblPeriod.Text = "Період";
            // 
            // txtPeriod
            // 
            this.txtPeriod.Location = new System.Drawing.Point(59, 57);
            this.txtPeriod.Mask = "00:00";
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.RejectInputOnFirstFailure = true;
            this.txtPeriod.Size = new System.Drawing.Size(150, 20);
            this.txtPeriod.TabIndex = 6;
            this.txtPeriod.ValidatingType = typeof(System.DateTime);
            this.txtPeriod.TextChanged += new System.EventHandler(this.txtPeriod_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(215, 196);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblPeriods
            // 
            this.lblPeriods.AutoSize = true;
            this.lblPeriods.Location = new System.Drawing.Point(12, 97);
            this.lblPeriods.Name = "lblPeriods";
            this.lblPeriods.Size = new System.Drawing.Size(41, 13);
            this.lblPeriods.TabIndex = 9;
            this.lblPeriods.Text = "Графік";
            // 
            // lstPeriods
            // 
            this.lstPeriods.Location = new System.Drawing.Point(59, 97);
            this.lstPeriods.MultiSelect = false;
            this.lstPeriods.Name = "lstPeriods";
            this.lstPeriods.Size = new System.Drawing.Size(150, 69);
            this.lstPeriods.TabIndex = 10;
            this.lstPeriods.UseCompatibleStateImageBehavior = false;
            this.lstPeriods.View = System.Windows.Forms.View.List;
            this.lstPeriods.SelectedIndexChanged += new System.EventHandler(this.lstPeriods_SelectedIndexChanged);
            // 
            // EditScheduledScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 231);
            this.Controls.Add(this.lstPeriods);
            this.Controls.Add(this.lblPeriods);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtPeriod);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.btnDeletePeriod);
            this.Controls.Add(this.btnAddPeriod);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 270);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 270);
            this.Name = "EditScheduledScanForm";
            this.Text = "Додати опитування за графіком";
            ((System.ComponentModel.ISupportInitialize)(this.err)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.ErrorProvider err;
        private System.Windows.Forms.Button btnAddPeriod;
        private System.Windows.Forms.Button btnDeletePeriod;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.Label lblPeriods;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.MaskedTextBox txtPeriod;
        private System.Windows.Forms.ListView lstPeriods;
    }
}