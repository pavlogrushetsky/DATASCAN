using DATASCAN.View.Controls;

namespace DATASCAN.View
{
    sealed partial class DATASCANForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DATASCANForm));
            this.mnuDATASCAN = new System.Windows.Forms.MenuStrip();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splHorizontal = new System.Windows.Forms.SplitContainer();
            this.splVertical = new System.Windows.Forms.SplitContainer();
            this.grpEstimators = new System.Windows.Forms.GroupBox();
            this.trvEstimators = new System.Windows.Forms.TreeView();
            this.imgEstimators = new System.Windows.Forms.ImageList(this.components);
            this.grpScans = new System.Windows.Forms.GroupBox();
            this.trvScans = new System.Windows.Forms.TreeView();
            this.grpMessages = new System.Windows.Forms.GroupBox();
            this.lstMessages = new DATASCAN.View.Controls.LogListView();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgMessages = new System.Windows.Forms.ImageList(this.components);
            this.imageScans = new System.Windows.Forms.ImageList(this.components);
            this.mnuDATASCAN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splHorizontal)).BeginInit();
            this.splHorizontal.Panel1.SuspendLayout();
            this.splHorizontal.Panel2.SuspendLayout();
            this.splHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splVertical)).BeginInit();
            this.splVertical.Panel1.SuspendLayout();
            this.splVertical.Panel2.SuspendLayout();
            this.splVertical.SuspendLayout();
            this.grpEstimators.SuspendLayout();
            this.grpScans.SuspendLayout();
            this.grpMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuDATASCAN
            // 
            this.mnuDATASCAN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.mnuAbout});
            this.mnuDATASCAN.Location = new System.Drawing.Point(0, 0);
            this.mnuDATASCAN.Name = "mnuDATASCAN";
            this.mnuDATASCAN.Size = new System.Drawing.Size(884, 24);
            this.mnuDATASCAN.TabIndex = 0;
            this.mnuDATASCAN.Text = "menuStrip1";
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDatabase,
            this.mnuConnection});
            this.mnuSettings.Image = global::DATASCAN.Properties.Resources.Settings;
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(117, 20);
            this.mnuSettings.Text = "Налаштування";
            // 
            // mnuDatabase
            // 
            this.mnuDatabase.Image = global::DATASCAN.Properties.Resources.DatabaseSettings;
            this.mnuDatabase.Name = "mnuDatabase";
            this.mnuDatabase.Size = new System.Drawing.Size(169, 22);
            this.mnuDatabase.Text = "Сервер баз даних";
            this.mnuDatabase.Click += new System.EventHandler(this.mnuDatabase_Click);
            // 
            // mnuConnection
            // 
            this.mnuConnection.Image = global::DATASCAN.Properties.Resources.ConnectionSettings;
            this.mnuConnection.Name = "mnuConnection";
            this.mnuConnection.Size = new System.Drawing.Size(169, 22);
            this.mnuConnection.Text = "Підключення";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::DATASCAN.Properties.Resources.Info;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(115, 20);
            this.mnuAbout.Text = "Про програму";
            // 
            // splHorizontal
            // 
            this.splHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splHorizontal.Location = new System.Drawing.Point(0, 24);
            this.splHorizontal.Name = "splHorizontal";
            this.splHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splHorizontal.Panel1
            // 
            this.splHorizontal.Panel1.Controls.Add(this.splVertical);
            // 
            // splHorizontal.Panel2
            // 
            this.splHorizontal.Panel2.Controls.Add(this.grpMessages);
            this.splHorizontal.Size = new System.Drawing.Size(884, 537);
            this.splHorizontal.SplitterDistance = 294;
            this.splHorizontal.TabIndex = 1;
            // 
            // splVertical
            // 
            this.splVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splVertical.Location = new System.Drawing.Point(0, 0);
            this.splVertical.Name = "splVertical";
            // 
            // splVertical.Panel1
            // 
            this.splVertical.Panel1.Controls.Add(this.grpEstimators);
            // 
            // splVertical.Panel2
            // 
            this.splVertical.Panel2.Controls.Add(this.grpScans);
            this.splVertical.Size = new System.Drawing.Size(884, 294);
            this.splVertical.SplitterDistance = 294;
            this.splVertical.TabIndex = 0;
            // 
            // grpEstimators
            // 
            this.grpEstimators.Controls.Add(this.trvEstimators);
            this.grpEstimators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEstimators.Location = new System.Drawing.Point(0, 0);
            this.grpEstimators.Name = "grpEstimators";
            this.grpEstimators.Size = new System.Drawing.Size(294, 294);
            this.grpEstimators.TabIndex = 0;
            this.grpEstimators.TabStop = false;
            this.grpEstimators.Text = "Обчислювачі";
            // 
            // trvEstimators
            // 
            this.trvEstimators.BackColor = System.Drawing.SystemColors.Control;
            this.trvEstimators.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvEstimators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEstimators.FullRowSelect = true;
            this.trvEstimators.ImageIndex = 0;
            this.trvEstimators.ImageList = this.imgEstimators;
            this.trvEstimators.Location = new System.Drawing.Point(3, 16);
            this.trvEstimators.Name = "trvEstimators";
            this.trvEstimators.SelectedImageIndex = 0;
            this.trvEstimators.Size = new System.Drawing.Size(288, 275);
            this.trvEstimators.TabIndex = 0;
            // 
            // imgEstimators
            // 
            this.imgEstimators.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgEstimators.ImageStream")));
            this.imgEstimators.TransparentColor = System.Drawing.Color.Transparent;
            this.imgEstimators.Images.SetKeyName(0, "Customer.png");
            this.imgEstimators.Images.SetKeyName(1, "Business.png");
            this.imgEstimators.Images.SetKeyName(2, "Group.png");
            this.imgEstimators.Images.SetKeyName(3, "Estimator.png");
            this.imgEstimators.Images.SetKeyName(4, "Sensor.png");
            // 
            // grpScans
            // 
            this.grpScans.Controls.Add(this.trvScans);
            this.grpScans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpScans.Location = new System.Drawing.Point(0, 0);
            this.grpScans.Name = "grpScans";
            this.grpScans.Size = new System.Drawing.Size(586, 294);
            this.grpScans.TabIndex = 0;
            this.grpScans.TabStop = false;
            this.grpScans.Text = "Групи опитування ";
            // 
            // trvScans
            // 
            this.trvScans.BackColor = System.Drawing.SystemColors.Control;
            this.trvScans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvScans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvScans.ImageIndex = 0;
            this.trvScans.ImageList = this.imageScans;
            this.trvScans.Location = new System.Drawing.Point(3, 16);
            this.trvScans.Name = "trvScans";
            this.trvScans.SelectedImageIndex = 0;
            this.trvScans.Size = new System.Drawing.Size(580, 275);
            this.trvScans.TabIndex = 0;
            // 
            // grpMessages
            // 
            this.grpMessages.Controls.Add(this.lstMessages);
            this.grpMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMessages.Location = new System.Drawing.Point(0, 0);
            this.grpMessages.Name = "grpMessages";
            this.grpMessages.Size = new System.Drawing.Size(884, 239);
            this.grpMessages.TabIndex = 0;
            this.grpMessages.TabStop = false;
            this.grpMessages.Text = "Повідомлення";
            // 
            // lstMessages
            // 
            this.lstMessages.BackColor = System.Drawing.SystemColors.Control;
            this.lstMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus,
            this.colType,
            this.colTimestamp,
            this.colMessage});
            this.lstMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessages.FullRowSelect = true;
            this.lstMessages.Location = new System.Drawing.Point(3, 16);
            this.lstMessages.MultiSelect = false;
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(878, 220);
            this.lstMessages.SmallImageList = this.imgMessages;
            this.lstMessages.TabIndex = 0;
            this.lstMessages.UseCompatibleStateImageBehavior = false;
            this.lstMessages.View = System.Windows.Forms.View.Details;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Статус";
            // 
            // colType
            // 
            this.colType.Text = "Тип";
            // 
            // colTimestamp
            // 
            this.colTimestamp.Text = "Дата та час";
            // 
            // colMessage
            // 
            this.colMessage.Text = "Повідомлення";
            // 
            // imgMessages
            // 
            this.imgMessages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMessages.ImageStream")));
            this.imgMessages.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMessages.Images.SetKeyName(0, "Info.png");
            this.imgMessages.Images.SetKeyName(1, "Ok.png");
            this.imgMessages.Images.SetKeyName(2, "Attention.png");
            this.imgMessages.Images.SetKeyName(3, "HighPriority.png");
            // 
            // imageScans
            // 
            this.imageScans.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageScans.ImageStream")));
            this.imageScans.TransparentColor = System.Drawing.Color.Transparent;
            this.imageScans.Images.SetKeyName(0, "PeriodicScan.ico");
            this.imageScans.Images.SetKeyName(1, "ScheduledScan.ico");
            // 
            // DATASCANForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.splHorizontal);
            this.Controls.Add(this.mnuDATASCAN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuDATASCAN;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "DATASCANForm";
            this.Text = "DATASCAN - Програма опитування обчислювачів";
            this.mnuDATASCAN.ResumeLayout(false);
            this.mnuDATASCAN.PerformLayout();
            this.splHorizontal.Panel1.ResumeLayout(false);
            this.splHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splHorizontal)).EndInit();
            this.splHorizontal.ResumeLayout(false);
            this.splVertical.Panel1.ResumeLayout(false);
            this.splVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splVertical)).EndInit();
            this.splVertical.ResumeLayout(false);
            this.grpEstimators.ResumeLayout(false);
            this.grpScans.ResumeLayout(false);
            this.grpMessages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuDATASCAN;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuDatabase;
        private System.Windows.Forms.ToolStripMenuItem mnuConnection;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.SplitContainer splHorizontal;
        private System.Windows.Forms.SplitContainer splVertical;
        private System.Windows.Forms.GroupBox grpEstimators;
        private System.Windows.Forms.GroupBox grpScans;
        private System.Windows.Forms.GroupBox grpMessages;
        private System.Windows.Forms.TreeView trvEstimators;
        private System.Windows.Forms.TreeView trvScans;
        private LogListView lstMessages;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colTimestamp;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.ImageList imgMessages;
        private System.Windows.Forms.ImageList imgEstimators;
        private System.Windows.Forms.ImageList imageScans;
    }
}

