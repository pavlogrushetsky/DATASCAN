using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DATASCAN.Core.Entities.Floutecs;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditFloutecForm : Form
    {
        public Floutec Floutec { get; set; }

        public bool IsEdit { get; set; }

        public List<int> Addresses { private get; set; } 

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
                    rbGPRS.Checked = Floutec.IsScannedViaGPRS;
                    rbDbf.Checked = !rbGPRS.Checked;
                    Icon = Resources.Estimator1;
                }

                txtPhone.Enabled = rbGPRS.Checked;
            };

            btnSave.Select();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _nameChanged = !txtName.Text.Equals(Floutec.Name);
            SetChanged();
            err.SetError(txtName, "");
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
            err.SetError(txtPhone, "");
        }

        private void numAddress_ValueChanged(object sender, EventArgs e)
        {
            _addressChanged = !numAddress.Value.Equals(Floutec.Address);
            SetChanged();
            err.SetError(numAddress, "");
        }

        private void rbDbf_CheckedChanged(object sender, EventArgs e)
        {
            _scanTypeChanged = rbGPRS.Checked != Floutec.IsScannedViaGPRS;
            SetChanged();
            txtPhone.Enabled = rbGPRS.Checked;
        }

        private void rbGPRS_CheckedChanged(object sender, EventArgs e)
        {
            _scanTypeChanged = rbGPRS.Checked != Floutec.IsScannedViaGPRS;
            SetChanged();
            txtPhone.Enabled = rbGPRS.Checked;
            if (!rbGPRS.Checked)
            {
                err.SetError(txtPhone, "");
                txtPhone.Text = Floutec.Phone;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {           
            bool valid = ValidateName() & ValidatePhone() & ValidateAddress();

            if (valid)
            {
                Floutec.Name = txtName.Text;
                Floutec.Description = txtDescription.Text;
                Floutec.Phone = txtPhone.Text;
                Floutec.Address = (int)numAddress.Value;
                Floutec.IsScannedViaGPRS = rbGPRS.Checked;

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

        private void SetChanged()
        {
            _changed = _nameChanged || _descriptionChanged || _phoneChanged || _addressChanged || _scanTypeChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private bool ValidateName()
        {
            err.SetError(txtName, string.IsNullOrEmpty(txtName.Text) ? "Вкажіть назву обчислювача" : "");
            return string.IsNullOrEmpty(err.GetError(txtName));
        }

        private bool ValidatePhone()
        {
            err.SetError(txtPhone, !rbGPRS.Checked || (rbGPRS.Checked && !string.IsNullOrEmpty(txtPhone.Text) && txtPhone.MaskCompleted) ? "" : "Номер телефону вказано невірно");
            return string.IsNullOrEmpty(err.GetError(txtPhone));
        }

        private bool ValidateAddress()
        {
            err.SetError(numAddress, !Addresses.Contains((int)numAddress.Value) ? "" : "Обчислювач з такою адресою вже існує");
            return string.IsNullOrEmpty(err.GetError(numAddress));
        }
    }
}
