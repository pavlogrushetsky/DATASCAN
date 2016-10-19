using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Model.Scanning;

namespace DATASCAN.View.Forms
{
    public partial class EditFloutecScanMemberForm : Form
    {
        public FloutecScanMember Member { get; set; }

        public List<FloutecScanMember> ExistentMembers { private get; set; } 

        public int ScanBaseId { private get; set; }

        public int EstimatorId { private get; set; }

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
                    Member = new FloutecScanMember
                    {
                        ScanBaseId = ScanBaseId,
                        EstimatorId = EstimatorId
                    };

                    cbScanIdentData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanIdentData);
                    cbScanAlarmData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanAlarmData);
                    cbScanInterData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanInterData);
                    cbScanInstantData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanInstantData);
                    cbScanHourlyData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanHourlyData);                    
                }
                else
                {
                    cbScanIdentData.Checked = Member.ScanIdentData;
                    cbScanAlarmData.Checked = Member.ScanAlarmData;
                    cbScanInterData.Checked = Member.ScanInterData;
                    cbScanInstantData.Checked = Member.ScanInstantData;
                    cbScanHourlyData.Checked = Member.ScanHourlyData;

                    cbScanIdentData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanIdentData);
                    cbScanAlarmData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanAlarmData);
                    cbScanInterData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanInterData);
                    cbScanInstantData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanInstantData);
                    cbScanHourlyData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanHourlyData);
                }

                bool showInfo = !cbScanIdentData.Enabled || !cbScanAlarmData.Enabled || !cbScanInterData.Enabled ||
                              !cbScanInstantData.Enabled || !cbScanHourlyData.Enabled;

                info.SetError(cbScanIdentData, !showInfo ? "" : "Деякі дані обчислювача вже опитуються");

                btnSave.Enabled = cbScanIdentData.Enabled || cbScanAlarmData.Enabled || cbScanInterData.Enabled ||
                              cbScanInstantData.Enabled || cbScanHourlyData.Enabled;
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
