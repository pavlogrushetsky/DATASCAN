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

            txtServerName.Text = Settings.ServerName;
            txtDatabaseName.Text = Settings.DatabaseName;
            txtUserName.Text = Settings.UserName;
            txtUserPassword.Text = Settings.UserPassword;
            numConnectionTimeout.Text = Settings.ConnectionTimeout;

            btnCancel.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_settingsChanged)
            {
                if (_settingsValid)
                {
                    Settings.ServerName = txtServerName.Text;
                    Settings.DatabaseName = txtDatabaseName.Text;
                    Settings.UserName = txtUserName.Text;
                    Settings.UserPassword = txtUserPassword.Text;
                    Settings.ConnectionTimeout = numConnectionTimeout.Text;

                    Settings.Save();

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

            DataContextService service = new DataContextService(connectionString.ToString());

            bool result = await service.TestConnection(false);

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
            _serverNameChanged = !txtServerName.Text.Equals(Settings.ServerName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtDatabaseName_TextChanged(object sender, EventArgs e)
        {
            _databaseNameChanged = !txtDatabaseName.Text.Equals(Settings.DatabaseName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            _userNameChanged = !txtUserName.Text.Equals(Settings.UserName);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void txtUserPassword_TextChanged(object sender, EventArgs e)
        {
            _userPasswordChanged = !txtUserPassword.Text.Equals(Settings.UserPassword);
            SetSettingsChanged();
            ValidateSettings();
        }

        private void numConnectionTimeout_ValueChanged(object sender, EventArgs e)
        {
            _timeoutChanged = !numConnectionTimeout.Text.Equals(Settings.ConnectionTimeout);
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
            _serverNameValid = ValidateServerName();
            _databaseNameValid = ValidateDatabaseName();
            _userNameValid = ValidateUserName();
            _userPasswordValid = ValidateUserPassword();
            _timeoutValid = true;

            _settingsValid = _serverNameValid && _databaseNameValid && _userNameValid && _userPasswordValid && _timeoutValid;

            btnTestConnection.Enabled = _settingsValid;
            btnSave.Enabled = _settingsValid;
        }

        private bool ValidateServerName()
        {
            err.SetError(txtServerName, string.IsNullOrEmpty(txtServerName.Text) ? "Вкажіть назву сервера" : "");
            return string.IsNullOrEmpty(err.GetError(txtServerName));
        }

        private bool ValidateDatabaseName()
        {
            err.SetError(txtDatabaseName, string.IsNullOrEmpty(txtDatabaseName.Text) ? "Вкажіть назву бази даних" : "");
            return string.IsNullOrEmpty(err.GetError(txtDatabaseName));
        }

        private bool ValidateUserName()
        {
            err.SetError(txtUserName, !(string.IsNullOrEmpty(txtUserName.Text) & !string.IsNullOrEmpty(txtUserPassword.Text)) ? "" : "Вкажіть ім'я користувача");
            return string.IsNullOrEmpty(err.GetError(txtUserName));
        }

        private bool ValidateUserPassword()
        {
            err.SetError(txtUserPassword, !(string.IsNullOrEmpty(txtUserPassword.Text) & !string.IsNullOrEmpty(txtUserName.Text)) ? "" : "Вкажіть пароль користувача");
            return string.IsNullOrEmpty(err.GetError(txtUserPassword));
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
            _timeoutChanged = !numConnectionTimeout.Text.Equals(Settings.ConnectionTimeout);
            SetSettingsChanged();
            ValidateSettings();
        }
    }
}
