using System;
using System.Windows.Forms;
using DATASCAN.Model.Floutecs;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditFloutecForm : Form
    {
        public Floutec Floutec { get; set; }

        public bool IsEdit { get; set; }

        private bool _nameChanged;

        private bool _descriptionChanged;

        private bool _phoneChanged;

        private bool _addressChanged;

        private bool _scanTypeChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати обчислювач ФЛОУТЕК";

        private const string TITLE_EDIT = "Налаштування обчислювача ФЛОУТЕК";

        public EditFloutecForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Floutec = new Floutec();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtName.Text = Floutec.Name;
                    txtDescription.Text = Floutec.Description;
                    txtPhone.Text = Floutec.Phone;
                    numAddress.Value = Floutec.Address;
                    cbScanType.Checked = Floutec.IsScannedViaGPRS;
                    Icon = Resources.Estimator1;
                }

                txtPhone.Enabled = cbScanType.Checked;
            };

            btnCancel.Select();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _nameChanged = !txtName.Text.Equals(Floutec.Name);
            SetChanged();
            lblNameError.Visible = false;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _descriptionChanged = !txtDescription.Text.Equals(Floutec.Description);
            SetChanged();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            _phoneChanged = !txtPhone.Text.Equals(Floutec.Phone);
            SetChanged();
            lblPhoneError.Visible = false;
        }

        private void numAddress_ValueChanged(object sender, EventArgs e)
        {
            _addressChanged = !numAddress.Value.Equals(Floutec.Address);
            SetChanged();
        }

        private void cbScanType_CheckedChanged(object sender, EventArgs e)
        {
            _scanTypeChanged = cbScanType.Checked != Floutec.IsScannedViaGPRS;
            SetChanged();
            txtPhone.Enabled = cbScanType.Checked;
            cbScanType.Text = cbScanType.Checked ? "За номером телефону через GPRS" : "Таблиці DBF";
            if (!cbScanType.Checked)
            {
                lblPhoneError.Visible = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool valid = ValidateFields();

            if (valid)
            {
                Floutec.Name = txtName.Text;
                Floutec.Description = txtDescription.Text;
                Floutec.Phone = txtPhone.Text;
                Floutec.Address = (int)numAddress.Value;
                Floutec.IsScannedViaGPRS = cbScanType.Checked;

                if (IsEdit && !_changed)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateFields()
        {
            bool nameIsValid = !string.IsNullOrEmpty(txtName.Text);
            bool phoneIsValid = !cbScanType.Checked || (cbScanType.Checked && !string.IsNullOrEmpty(txtPhone.Text) && txtPhone.MaskCompleted);

            lblNameError.Visible = !nameIsValid;
            lblPhoneError.Visible = !phoneIsValid;

            return nameIsValid && phoneIsValid;
        }

        private void SetChanged()
        {
            _changed = _nameChanged || _descriptionChanged || _phoneChanged || _addressChanged || _scanTypeChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }
    }
}
