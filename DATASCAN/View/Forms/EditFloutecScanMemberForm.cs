using System.Windows.Forms;
using DATASCAN.Model.Scanning;

namespace DATASCAN.View.Forms
{
    public partial class EditFloutecScanMemberForm : Form
    {
        public FloutecScanMember Member { get; set; }

        public bool IsEdit { get; set; }

        public bool _scanIdentDataChanged;

        public bool _scanAlarmDataChanged;

        public bool _scanInterDataChanged;

        public bool _scanInstantDataChanged;

        public bool _scanHourlyDataChanged;

        public bool _changed;

        public EditFloutecScanMemberForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Member = new FloutecScanMember();
                }
                else
                {
                    cbScanIdentData.Checked = Member.ScanIdentData;
                    cbScanAlarmData.Checked = Member.ScanAlarmData;
                    cbScanInterData.Checked = Member.ScanInterData;
                    cbScanInstantData.Checked = Member.ScanInstantData;
                    cbScanHourlyData.Checked = Member.ScanHourlyData;
                }
            };

            btnCancel.Select();
        }

        private void cbScanIdentData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanIdentDataChanged = cbScanIdentData.Checked != Member.ScanIdentData;
            SetChanged();
        }

        private void cbScanAlarmData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanAlarmDataChanged = cbScanAlarmData.Checked != Member.ScanAlarmData;
            SetChanged();
        }

        private void cbScanInterData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanInterDataChanged = cbScanInterData.Checked != Member.ScanInterData;
            SetChanged();
        }

        private void cbScanInstantData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanInstantDataChanged = cbScanInstantData.Checked != Member.ScanInstantData;
            SetChanged();
        }

        private void cbScanHourlyData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanHourlyDataChanged = cbScanHourlyData.Checked != Member.ScanHourlyData;
            SetChanged();
        }

        private void SetChanged()
        {
            _changed = _scanInstantDataChanged || _scanHourlyDataChanged || _scanAlarmDataChanged ||
                       _scanIdentDataChanged || _scanInterDataChanged;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Member.ScanAlarmData = cbScanAlarmData.Checked;
            Member.ScanHourlyData = cbScanHourlyData.Checked;
            Member.ScanIdentData = cbScanIdentData.Checked;
            Member.ScanInterData = cbScanInterData.Checked;
            Member.ScanInstantData = cbScanInstantData.Checked;

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

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
