using System;
using System.Windows.Forms;
using DATASCAN.Core.Model.Rocs;
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
            bool valid = ValidateName() & ValidatePhone() & ValidateAddress();

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
            err.SetError(txtName, "");
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
            err.SetError(txtAddress, "");
            err.SetError(txtPhone, "");
            if (!rbGPRS.Checked && !string.IsNullOrEmpty(Roc.Phone))
            {                
                txtPhone.Text = Roc.Phone;
            }
            else
            {
                txtAddress.Text = !string.IsNullOrEmpty(Roc.Address) ? ConvertIpToMask(Roc.Address) : string.Empty;
                numPort.Value = Roc.Port == 0 ? 4000 : Roc.Port;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            _addressChanged = !txtAddress.Text.Replace(" ", string.Empty).Equals(Roc.Address);
            SetChanged();
            err.SetError(txtAddress, "");
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            _phoneChanged = !txtPhone.Text.Equals(Roc.Phone);
            SetChanged();
            err.SetError(txtPhone, "");
        }

        private void numPort_ValueChanged(object sender, EventArgs e)
        {
            _portChanged = !numPort.Value.Equals(Roc.Port);
            SetChanged();
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

        private bool ValidateName()
        {
            err.SetError(txtName, string.IsNullOrEmpty(txtName.Text) ? "Вкажіть назву обчислювача" : "");
            return string.IsNullOrEmpty(err.GetError(txtName));
        }

        private bool ValidateAddress()
        {
            err.SetError(txtAddress, !rbTCPIP.Checked || rbTCPIP.Checked & !string.IsNullOrEmpty(txtAddress.Text) & txtAddress.MaskCompleted ? "" : "IP-адресу вказано невірно");
            return string.IsNullOrEmpty(err.GetError(txtAddress));
        }

        private bool ValidatePhone()
        {
            err.SetError(txtPhone, !rbGPRS.Checked || rbGPRS.Checked & !string.IsNullOrEmpty(txtPhone.Text) & txtPhone.MaskCompleted ? "" : "Номер телефону вказано невірно");
            return string.IsNullOrEmpty(err.GetError(txtPhone));
        }
    }
}
