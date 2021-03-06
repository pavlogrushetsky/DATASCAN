﻿using DATASCAN.View.Controls;

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
            this.mnuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPause = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuModemStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splHorizontal = new System.Windows.Forms.SplitContainer();
            this.splVertical = new System.Windows.Forms.SplitContainer();
            this.grpEstimators = new System.Windows.Forms.GroupBox();
            this.trvEstimators = new System.Windows.Forms.TreeView();
            this.imgEstimators = new System.Windows.Forms.ImageList(this.components);
            this.grpScans = new System.Windows.Forms.GroupBox();
            this.trvScans = new System.Windows.Forms.TreeView();
            this.imageScans = new System.Windows.Forms.ImageList(this.components);
            this.grpMessages = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstMessages = new DATASCAN.View.Controls.LogListView();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgMessages = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.StatusStrip();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuExpand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.mnuNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuDATASCAN
            // 
            this.mnuDATASCAN.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRun,
            this.mnuPause,
            this.mnuSettings,
            this.mnuModemStatus,
            this.mnuHelp,
            this.mnuAbout,
            this.toolStripMenuItem1});
            this.mnuDATASCAN.Location = new System.Drawing.Point(0, 0);
            this.mnuDATASCAN.Name = "mnuDATASCAN";
            this.mnuDATASCAN.Size = new System.Drawing.Size(884, 24);
            this.mnuDATASCAN.TabIndex = 0;
            this.mnuDATASCAN.Text = "menuStrip1";
            // 
            // mnuRun
            // 
            this.mnuRun.Image = global::DATASCAN.Properties.Resources.Activate;
            this.mnuRun.Name = "mnuRun";
            this.mnuRun.Size = new System.Drawing.Size(73, 20);
            this.mnuRun.Text = "Запуск";
            this.mnuRun.Click += new System.EventHandler(this.mnuRun_Click);
            // 
            // mnuPause
            // 
            this.mnuPause.Image = global::DATASCAN.Properties.Resources.Deactivate;
            this.mnuPause.Name = "mnuPause";
            this.mnuPause.Size = new System.Drawing.Size(62, 20);
            this.mnuPause.Text = "Стоп";
            this.mnuPause.Visible = false;
            this.mnuPause.Click += new System.EventHandler(this.mnuPause_Click);
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
            this.mnuConnection.Click += new System.EventHandler(this.mnuConnection_Click);
            // 
            // mnuModemStatus
            // 
            this.mnuModemStatus.Image = global::DATASCAN.Properties.Resources.Phone;
            this.mnuModemStatus.Name = "mnuModemStatus";
            this.mnuModemStatus.Size = new System.Drawing.Size(174, 20);
            this.mnuModemStatus.Text = "Статус зв\'язку по модему";
            this.mnuModemStatus.Click += new System.EventHandler(this.mnuModemStatus_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Checked = true;
            this.mnuHelp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuHelp.Image = global::DATASCAN.Properties.Resources.Help;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(77, 20);
            this.mnuHelp.Text = "Довідка";
            this.mnuHelp.Click += new System.EventHandler(this.mnuHelp_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::DATASCAN.Properties.Resources.Info;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(115, 20);
            this.mnuAbout.Text = "Про програму";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
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
            this.trvScans.FullRowSelect = true;
            this.trvScans.ImageIndex = 0;
            this.trvScans.ImageList = this.imageScans;
            this.trvScans.Location = new System.Drawing.Point(3, 16);
            this.trvScans.Name = "trvScans";
            this.trvScans.SelectedImageIndex = 0;
            this.trvScans.Size = new System.Drawing.Size(580, 275);
            this.trvScans.TabIndex = 0;
            // 
            // imageScans
            // 
            this.imageScans.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageScans.ImageStream")));
            this.imageScans.TransparentColor = System.Drawing.Color.Transparent;
            this.imageScans.Images.SetKeyName(0, "PeriodicScan.ico");
            this.imageScans.Images.SetKeyName(1, "ScheduledScan.ico");
            this.imageScans.Images.SetKeyName(2, "Scan.ico");
            this.imageScans.Images.SetKeyName(3, "Estimator.ico");
            this.imageScans.Images.SetKeyName(4, "Sensor.ico");
            // 
            // grpMessages
            // 
            this.grpMessages.Controls.Add(this.tableLayoutPanel1);
            this.grpMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMessages.Location = new System.Drawing.Point(0, 0);
            this.grpMessages.Name = "grpMessages";
            this.grpMessages.Size = new System.Drawing.Size(884, 239);
            this.grpMessages.TabIndex = 0;
            this.grpMessages.TabStop = false;
            this.grpMessages.Text = "Повідомлення";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lstMessages, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(878, 220);
            this.tableLayoutPanel1.TabIndex = 0;
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
            this.lstMessages.Location = new System.Drawing.Point(3, 3);
            this.lstMessages.MultiSelect = false;
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(872, 192);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // status
            // 
            this.status.ImageScalingSize = new System.Drawing.Size(30, 10);
            this.status.Location = new System.Drawing.Point(0, 539);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(884, 22);
            this.status.TabIndex = 2;
            this.status.Text = "statusStrip1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.mnuNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "DATASCAN";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // mnuNotifyIcon
            // 
            this.mnuNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExpand,
            this.mnuExit});
            this.mnuNotifyIcon.Name = "mnuNotifyIcon";
            this.mnuNotifyIcon.Size = new System.Drawing.Size(138, 48);
            // 
            // mnuExpand
            // 
            this.mnuExpand.Name = "mnuExpand";
            this.mnuExpand.Size = new System.Drawing.Size(137, 22);
            this.mnuExpand.Text = "Розгорнути";
            this.mnuExpand.Click += new System.EventHandler(this.mnuExpand_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(137, 22);
            this.mnuExit.Text = "Вийти";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // helpProvider
            // 
            this.helpProvider.HelpNamespace = "C:\\Users\\pavlo\\Source\\Repos\\DATASCAN\\DATASCAN\\DATASCAN.chm";
            // 
            // DATASCANForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.status);
            this.Controls.Add(this.splHorizontal);
            this.Controls.Add(this.mnuDATASCAN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuDATASCAN;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "DATASCANForm";
            this.Text = "DATASCAN - Програма опитування обчислювачів";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DATASCANForm_FormClosing);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mnuNotifyIcon.ResumeLayout(false);
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
        private System.Windows.Forms.ImageList imgMessages;
        private System.Windows.Forms.ImageList imgEstimators;
        private System.Windows.Forms.ImageList imageScans;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip mnuNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem mnuExpand;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private LogListView lstMessages;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colTimestamp;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem mnuRun;
        private System.Windows.Forms.ToolStripMenuItem mnuPause;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuModemStatus;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    }
}

