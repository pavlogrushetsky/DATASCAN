using System;
using System.Windows.Forms;
using DATASCAN.Model.Rocs;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditRocForm : Form
    {
        public Roc809 Roc { get; set; }

        public bool IsEdit { get; set; }

        private bool _nameChanged;

        private bool _descriptionChanged;

        private bool _rocUnitChanged;

        private bool _rocGroupChanged;

        private bool _hostUnitChanged;

        private bool _hostGroupChanged;

        private bool _scanTypeChanged;

        private bool _phoneChanged;

        private bool _addressChanged;

        private bool _portChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати обчислювач ROC809";

        private const string TITLE_EDIT = "Налаштування обчислювача ROC809";

        public EditRocForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Roc = new Roc809();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtName.Text = Roc.Name;
                    txtDescription.Text = Roc.Description;
                    numRocUnit.Value = Roc.RocUnit;
                    numRocGroup.Value = Roc.RocGroup;
                    numHostUnit.Value = Roc.HostUnit;
                    numHostGroup.Value = Roc.HostGroup;
                    rbGPRS.Checked = Roc.IsScannedViaGPRS;
                    rbTCPIP.Checked = !rbGPRS.Checked;
                    txtAddress.Text = ConvertIpToMask(Roc.Address);
                    txtPhone.Text = Roc.Phone;
                    numPort.Value = Roc.Port;
                    Icon = Resources.Estimator1;
                }

                txtAddress.Enabled = rbTCPIP.Checked;
                numPort.Enabled = rbTCPIP.Checked;
                txtPhone.Enabled = rbGPRS.Checked;
            };

            btnCancel.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateFields();

            if (valid)
            {
                Roc.Name = txtName.Text;
                Roc.Description = txtDescription.Text;
                Roc.RocUnit = (int)numRocUnit.Value;
                Roc.RocGroup = (int)numRocGroup.Value;
                Roc.HostUnit = (int)numHostUnit.Value;
                Roc.HostGroup = (int)numHostGroup.Value;
                Roc.Address = txtAddress.Text.Replace(" ", string.Empty);
                Roc.IsScannedViaGPRS = rbGPRS.Checked;
                Roc.Port = (int)numPort.Value;
                Roc.Phone = txtPhone.Text;

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _nameChanged = !txtName.Text.Equals(Roc.Name);
            SetChanged();
            lblNameError.Visible = false;
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _descriptionChanged = !txtDescription.Text.Equals(Roc.Description);
            SetChanged();
        }

        private void numRocUnit_ValueChanged(object sender, EventArgs e)
        {
            _rocUnitChanged = !numRocUnit.Value.Equals(Roc.RocUnit);
            SetChanged();
        }

        private void numRocGroup_ValueChanged(object sender, EventArgs e)
        {
            _rocGroupChanged = !numRocGroup.Value.Equals(Roc.RocGroup);
            SetChanged();
        }

        private void numHostUnit_ValueChanged(object sender, EventArgs e)
        {
            _hostUnitChanged = !numHostUnit.Value.Equals(Roc.HostUnit);
            SetChanged();
        }

        private void numHostGroup_ValueChanged(object sender, EventArgs e)
        {
            _hostGroupChanged = !numHostGroup.Value.Equals(Roc.HostGroup);
            SetChanged();
        }

        private void rbTCPIP_CheckedChanged(object sender, EventArgs e)
        {
            _scanTypeChanged = rbGPRS.Checked != Roc.IsScannedViaGPRS;
            SetChanged();
            txtAddress.Enabled = rbTCPIP.Checked;
            numPort.Enabled = rbTCPIP.Checked;
            txtPhone.Enabled = rbGPRS.Checked;
        }

        private void rbGPRS_CheckedChanged(object sender, EventArgs e)
        {
            _scanTypeChanged = rbGPRS.Checked != Roc.IsScannedViaGPRS;
            SetChanged();
            txtAddress.Enabled = rbTCPIP.Checked;
            numPort.Enabled = rbTCPIP.Checked;
            txtPhone.Enabled = rbGPRS.Checked;
            lblParameterError.Visible = false;
            if (!rbGPRS.Checked)
            {                
                txtPhone.Text = Roc.Phone;
            }
            else
            {
                txtAddress.Text = ConvertIpToMask(Roc.Address);
                numPort.Value = Roc.Port == 0 ? 4000 : Roc.Port;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            _addressChanged = !txtAddress.Text.Replace(" ", string.Empty).Equals(Roc.Address);
            SetChanged();
            lblParameterError.Visible = false;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            _phoneChanged = !txtPhone.Text.Equals(Roc.Phone);
            SetChanged();
        }

        private void numPort_ValueChanged(object sender, EventArgs e)
        {
            _portChanged = !numPort.Value.Equals(Roc.Port);
            SetChanged();
            lblParameterError.Visible = false;
        }

        private bool ValidateFields()
        {
            bool nameIsValid = !string.IsNullOrEmpty(txtName.Text);
            bool paramIsValid = (rbGPRS.Checked && !string.IsNullOrEmpty(txtPhone.Text) && txtPhone.MaskCompleted) ||
                (rbTCPIP.Checked && !string.IsNullOrEmpty(txtAddress.Text) && txtAddress.MaskCompleted);

            lblNameError.Visible = !nameIsValid;
            lblParameterError.Visible = !paramIsValid;

            return nameIsValid && paramIsValid;
        }

        private void SetChanged()
        {
            _changed = _nameChanged || _descriptionChanged || _rocUnitChanged || _rocGroupChanged || _hostUnitChanged ||
                       _hostGroupChanged || _scanTypeChanged || _addressChanged || _portChanged || _phoneChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private string ConvertIpToMask(string ip)
        {
            string[] ips = ip.Split('.');
            for (int i = 0; i < ips.Length; i++)
            {
                ips[i] = ips[i].PadRight(3, ' ');
            }
            return $"{ips[0]}.{ips[1]}.{ips[2]}.{ips[3]}";
        }
    }
}
