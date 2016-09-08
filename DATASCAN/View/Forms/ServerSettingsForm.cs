using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DATASCAN.Infrastructure.Settings;
using DATASCAN.Services;

namespace DATASCAN.View.Forms
{
    public partial class ServerSettingsForm : Form
    {
        private bool _serverNameChanged;

        private bool _databaseNameChanged;

        private bool _userNameChanged;

        private bool _userPasswordChanged;

        private bool _settingsChanged;

        private bool _timeoutChanged;

        private bool _serverNameValid = true;

        private bool _databaseNameValid = true;

        private bool _userNameValid = true;

        private bool _userPasswordValid = true;

        private bool _settingsValid = true;

        private bool _timeoutValid = true;

        public ServerSettingsForm()
        {
            InitializeComponent();

            txtServerName.Text = ServerSettings.ServerName;
            txtDatabaseName.Text = ServerSettings.DatabaseName;
            txtUserName.Text = ServerSettings.UserName;
            txtUserPassword.Text = ServerSettings.UserPassword;
            numConnectionTimeout.Text = ServerSettings.ConnectionTimeout;

            btnCancel.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_settingsChanged)
            {
                if (_settingsValid)
                {
                    ServerSettings.ServerName = txtServerName.Text;
                    ServerSettings.DatabaseName = txtDatabaseName.Text;
                    ServerSettings.UserName = txtUserName.Text;
                    ServerSettings.UserPassword = txtUserPassword.Text;
                    ServerSettings.ConnectionTimeout = numConnectionTimeout.Text;

                    ServerSettings.Save();

                    DialogResult = DialogResult.OK;
                    Close();
                }               
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
            {
                DataSource = txtServerName.Text,
                InitialCatalog = txtDatabaseName.Text,
                MultipleActiveResultSets = true,
                ConnectTimeout = int.Parse(numConnectionTimeout.Text)
            };

            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtUserPassword.Text))
            {
                connectionString.IntegratedSecurity = true;
            }
            else
            {
                connectionString.IntegratedSecurity = false;
                connectionString.UserID = txtUserName.Text;
                connectionString.Password = txtUserPassword.Text;
            }

            btnTestConnection.Text = "Встановлення з'єднання ...";
            btnTestConnection.ForeColor = Color.Black;

            AllowEditing(false);

            DataContextService service = new DataContextService();

            bool result = await service.TestConnection(connectionString.ToString());

            if (result)
            {
                btnTestConnection.Text = "З'єднання встановлено";
                btnTestConnection.ForeColor = Color.Green;
            }
            else
            {
                btnTestConnection.Text = "З'єднання не встановлено";
                btnTestConnection.ForeColor = Color.Red;
            }
                                                             
            AllowEditing(true);
        }

        private void txtServerName_TextChanged(object sender, EventArgs e)
        {
            _serverNameChanged = !txtServerName.Text.Equals(ServerSettings.ServerName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            _databaseNameChanged = !txtDatabaseName.Text.Equals(ServerSettings.DatabaseName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            _userNameChanged = !txtUserName.Text.Equals(ServerSettings.UserName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtUserPassword_TextChanged(object sender, EventArgs e)
        {
            _userPasswordChanged = !txtUserPassword.Text.Equals(ServerSettings.UserPassword);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void numConnectionTimeout_ValueChanged(object sender, EventArgs e)
        {
            _timeoutChanged = !numConnectionTimeout.Text.Equals(ServerSettings.ConnectionTimeout);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void SetSettingsChanged()
        {
            _settingsChanged = _serverNameChanged || _databaseNameChanged || _userNameChanged || _userPasswordChanged || _timeoutChanged;

            string title = "Налаштування сервера баз даних";

            Text = _settingsChanged ? title + " *" : title;

            btnTestConnection.Text = "Тестувати з'єднання";
            btnTestConnection.ForeColor = Color.Black;
        }

        private void ValidateSettings()
        {
            _serverNameValid = !string.IsNullOrEmpty(txtServerName.Text);
            _databaseNameValid = !string.IsNullOrEmpty(txtDatabaseName.Text);
            _userNameValid = !(string.IsNullOrEmpty(txtUserName.Text) & !string.IsNullOrEmpty(txtUserPassword.Text));
            _userPasswordValid = !(string.IsNullOrEmpty(txtUserPassword.Text) & !string.IsNullOrEmpty(txtUserName.Text));
            _timeoutValid = true;

            lblServerNameError.Visible = !_serverNameValid;
            lblDatabaseNameError.Visible = !_databaseNameValid;
            lblUserNameError.Visible = !_userNameValid;
            lblUserPasswordError.Visible = !_userPasswordValid;

            _settingsValid = _serverNameValid && _databaseNameValid && _userNameValid && _userPasswordValid && _timeoutValid;

            btnTestConnection.Enabled = _settingsValid;
            btnSave.Enabled = _settingsValid;
        }

        private void AllowEditing(bool allow)
        {
            txtServerName.Enabled = allow;
            txtDatabaseName.Enabled = allow;
            txtUserName.Enabled = allow;
            txtUserPassword.Enabled = allow;
            numConnectionTimeout.Enabled = allow;
            btnSave.Enabled = allow;
            btnCancel.Enabled = allow;
            btnTestConnection.Enabled = allow;
        }

        private void numConnectionTimeout_KeyDown(object sender, KeyEventArgs e)
        {
            _timeoutChanged = !numConnectionTimeout.Text.Equals(ServerSettings.ConnectionTimeout);
            SetSettingsChanged();
            ValidateSettings();
        }
    }
}
