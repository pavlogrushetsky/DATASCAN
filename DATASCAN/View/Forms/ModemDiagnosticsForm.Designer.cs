namespace DATASCAN.View.Forms
{
    partial class ModemDiagnosticsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModemDiagnosticsForm));
            this.lstModemMessages = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstModemMessages
            // 
            this.lstModemMessages.BackColor = System.Drawing.SystemColors.Control;
            this.lstModemMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstModemMessages.Location = new System.Drawing.Point(0, 0);
            this.lstModemMessages.Name = "lstModemMessages";
            this.lstModemMessages.Size = new System.Drawing.Size(384, 361);
            this.lstModemMessages.TabIndex = 0;
            this.lstModemMessages.UseCompatibleStateImageBehavior = false;
            this.lstModemMessages.View = System.Windows.Forms.View.List;
            // 
            // ModemDiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lstModemMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ModemDiagnosticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статус послідовного з\'єднання";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstModemMessages;
    }
}