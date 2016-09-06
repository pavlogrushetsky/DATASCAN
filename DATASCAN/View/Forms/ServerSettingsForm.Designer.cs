namespace DATASCAN.View.Forms
{
    partial class ServerSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerSettingsForm));
            this.lblServerName = new System.Windows.Forms.Label();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserPassword = new System.Windows.Forms.Label();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.lblServerNameError = new System.Windows.Forms.Label();
            this.lblDatabaseNameError = new System.Windows.Forms.Label();
            this.lblUserNameError = new System.Windows.Forms.Label();
            this.lblUserPasswordError = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Location = new System.Drawing.Point(12, 33);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(84, 13);
            this.lblServerName.TabIndex = 0;
            this.lblServerName.Text = "Назва сервера";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.AutoSize = true;
            this.lblDatabaseName.Location = new System.Drawing.Point(12, 82);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(98, 13);
            this.lblDatabaseName.TabIndex = 1;
            this.lblDatabaseName.Text = "Назва бази даних";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(12, 131);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(92, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Ім\'я користувача";
            // 
            // lblUserPassword
            // 
            this.lblUserPassword.AutoSize = true;
            this.lblUserPassword.Location = new System.Drawing.Point(12, 180);
            this.lblUserPassword.Name = "lblUserPassword";
            this.lblUserPassword.Size = new System.Drawing.Size(111, 13);
            this.lblUserPassword.TabIndex = 3;
            this.lblUserPassword.Text = "Пароль користувача";
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(129, 30);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(262, 20);
            this.txtServerName.TabIndex = 4;
            this.txtServerName.TextChanged += new System.EventHandler(this.txtServerName_TextChanged);
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(129, 79);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(262, 20);
            this.txtDatabaseName.TabIndex = 5;
            this.txtDatabaseName.TextChanged += new System.EventHandler(this.txtDatabaseName_TextChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(129, 128);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(262, 20);
            this.txtUserName.TabIndex = 6;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(129, 177);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(262, 20);
            this.txtUserPassword.TabIndex = 7;
            this.txtUserPassword.TextChanged += new System.EventHandler(this.txtUserPassword_TextChanged);
            // 
            // lblServerNameError
            // 
            this.lblServerNameError.AutoSize = true;
            this.lblServerNameError.ForeColor = System.Drawing.Color.Red;
            this.lblServerNameError.Location = new System.Drawing.Point(126, 14);
            this.lblServerNameError.Name = "lblServerNameError";
            this.lblServerNameError.Size = new System.Drawing.Size(124, 13);
            this.lblServerNameError.TabIndex = 8;
            this.lblServerNameError.Text = "Вкажіть назву сервера";
            this.lblServerNameError.Visible = false;
            // 
            // lblDatabaseNameError
            // 
            this.lblDatabaseNameError.AutoSize = true;
            this.lblDatabaseNameError.ForeColor = System.Drawing.Color.Red;
            this.lblDatabaseNameError.Location = new System.Drawing.Point(126, 63);
            this.lblDatabaseNameError.Name = "lblDatabaseNameError";
            this.lblDatabaseNameError.Size = new System.Drawing.Size(138, 13);
            this.lblDatabaseNameError.TabIndex = 9;
            this.lblDatabaseNameError.Text = "Вкажіть назву бази даних";
            this.lblDatabaseNameError.Visible = false;
            // 
            // lblUserNameError
            // 
            this.lblUserNameError.AutoSize = true;
            this.lblUserNameError.ForeColor = System.Drawing.Color.Red;
            this.lblUserNameError.Location = new System.Drawing.Point(126, 112);
            this.lblUserNameError.Name = "lblUserNameError";
            this.lblUserNameError.Size = new System.Drawing.Size(134, 13);
            this.lblUserNameError.TabIndex = 10;
            this.lblUserNameError.Text = "Вкажіть ім\'я користувача";
            this.lblUserNameError.Visible = false;
            // 
            // lblUserPasswordError
            // 
            this.lblUserPasswordError.AutoSize = true;
            this.lblUserPasswordError.ForeColor = System.Drawing.Color.Red;
            this.lblUserPasswordError.Location = new System.Drawing.Point(126, 161);
            this.lblUserPasswordError.Name = "lblUserPasswordError";
            this.lblUserPasswordError.Size = new System.Drawing.Size(152, 13);
            this.lblUserPasswordError.TabIndex = 11;
            this.lblUserPasswordError.Text = "Вкажіть пароль користувача";
            this.lblUserPasswordError.Visible = false;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(129, 223);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(262, 23);
            this.btnTestConnection.TabIndex = 12;
            this.btnTestConnection.Text = "Тестувати з\'єднання";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(316, 286);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Скасувати";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(235, 286);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Зберегти";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ServerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 321);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.lblUserPasswordError);
            this.Controls.Add(this.lblUserNameError);
            this.Controls.Add(this.lblDatabaseNameError);
            this.Controls.Add(this.lblServerNameError);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.lblUserPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.lblServerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(420, 360);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 360);
            this.Name = "ServerSettingsForm";
            this.Text = "Налаштування сервера баз даних";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserPassword;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label lblServerNameError;
        private System.Windows.Forms.Label lblDatabaseNameError;
        private System.Windows.Forms.Label lblUserNameError;
        private System.Windows.Forms.Label lblUserPasswordError;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}