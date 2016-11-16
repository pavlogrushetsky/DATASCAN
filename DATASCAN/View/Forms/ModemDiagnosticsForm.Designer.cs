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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModemDiagnosticsForm));
            this.imgModemMessages = new System.Windows.Forms.ImageList(this.components);
            this.mnuModemMessages = new System.Windows.Forms.MenuStrip();
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.lstModemMessages = new DATASCAN.View.Controls.LogListView();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnuModemMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgModemMessages
            // 
            this.imgModemMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgModemMessages.ImageStream")));
            this.imgModemMessages.TransparentColor = System.Drawing.Color.Transparent;
            this.imgModemMessages.Images.SetKeyName(0, "CallTransfer.png");
            this.imgModemMessages.Images.SetKeyName(1, "Phone.png");
            this.imgModemMessages.Images.SetKeyName(2, "PhoneDisconnected.png");
            this.imgModemMessages.Images.SetKeyName(3, "EndCall.png");
            this.imgModemMessages.Images.SetKeyName(4, "HighPriority.png");
            this.imgModemMessages.Images.SetKeyName(5, "IncomingData.png");
            this.imgModemMessages.Images.SetKeyName(6, "OutgoingData.png");
            this.imgModemMessages.Images.SetKeyName(7, "Wait.png");
            // 
            // mnuModemMessages
            // 
            this.mnuModemMessages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClear});
            this.mnuModemMessages.Location = new System.Drawing.Point(0, 0);
            this.mnuModemMessages.Name = "mnuModemMessages";
            this.mnuModemMessages.Size = new System.Drawing.Size(384, 24);
            this.mnuModemMessages.TabIndex = 1;
            this.mnuModemMessages.Text = "menuStrip1";
            // 
            // mnuClear
            // 
            this.mnuClear.Image = global::DATASCAN.Properties.Resources.Trash;
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(88, 20);
            this.mnuClear.Text = "Очистити";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // lstModemMessages
            // 
            this.lstModemMessages.BackColor = System.Drawing.SystemColors.Control;
            this.lstModemMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstModemMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus,
            this.colTimestamp,
            this.colPort,
            this.colMessage});
            this.lstModemMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstModemMessages.FullRowSelect = true;
            this.lstModemMessages.Location = new System.Drawing.Point(0, 24);
            this.lstModemMessages.MultiSelect = false;
            this.lstModemMessages.Name = "lstModemMessages";
            this.lstModemMessages.Size = new System.Drawing.Size(384, 337);
            this.lstModemMessages.SmallImageList = this.imgModemMessages;
            this.lstModemMessages.TabIndex = 0;
            this.lstModemMessages.UseCompatibleStateImageBehavior = false;
            this.lstModemMessages.View = System.Windows.Forms.View.Details;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Статус";
            // 
            // colTimestamp
            // 
            this.colTimestamp.Text = "Дата та час";
            // 
            // colPort
            // 
            this.colPort.Text = "Порт";
            // 
            // colMessage
            // 
            this.colMessage.Text = "Повідомлення";
            // 
            // ModemDiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.lstModemMessages);
            this.Controls.Add(this.mnuModemMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuModemMessages;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "ModemDiagnosticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Статус зв\'язку по модему";
            this.mnuModemMessages.ResumeLayout(false);
            this.mnuModemMessages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DATASCAN.View.Controls.LogListView lstModemMessages;
        private System.Windows.Forms.ImageList imgModemMessages;
        private System.Windows.Forms.MenuStrip mnuModemMessages;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colTimestamp;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ColumnHeader colPort;
    }
}