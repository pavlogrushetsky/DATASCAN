using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Model.Scanning;

namespace DATASCAN.View.Forms
{
    public partial class EditRocScanMemberForm : Form
    {
        public RocScanMember Member { get; set; }

        public bool IsEdit { get; set; }

        public List<RocScanMember> ExistentMembers { private get; set; }

        public int ScanBaseId { private get; set; }

        public int EstimatorId { private get; set; }

        public bool _scanEventDataChanged;

        public bool _scanAlarmDataChanged;

        public bool _scanMinuteDataChanged;

        public bool _scanPeriodicDataChanged;

        public bool _scanDailyDataChanged;

        public bool _changed;

        public EditRocScanMemberForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Member = new RocScanMember
                    {
                        ScanBaseId = ScanBaseId,
                        EstimatorId = EstimatorId
                    };

                    cbScanEventData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanEventData);
                    cbScanAlarmData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanAlarmData);
                    cbScanMinuteData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanMinuteData);
                    cbScanPeriodicData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanPeriodicData);
                    cbScanDailyData.Enabled = !ExistentMembers.Any(m => m.EstimatorId == EstimatorId && m.ScanDailyData);
                }
                else
                {
                    cbScanEventData.Checked = Member.ScanEventData;
                    cbScanAlarmData.Checked = Member.ScanAlarmData;
                    cbScanMinuteData.Checked = Member.ScanMinuteData;
                    cbScanPeriodicData.Checked = Member.ScanPeriodicData;
                    cbScanDailyData.Checked = Member.ScanDailyData;

                    cbScanEventData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanEventData);
                    cbScanAlarmData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanAlarmData);
                    cbScanMinuteData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanMinuteData);
                    cbScanPeriodicData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanPeriodicData);
                    cbScanDailyData.Enabled = !ExistentMembers.Except(new[] { Member }).Any(m => m.EstimatorId == Member.EstimatorId && m.ScanDailyData);
                }

                bool showInfo = !cbScanEventData.Enabled || !cbScanAlarmData.Enabled || !cbScanMinuteData.Enabled ||
                              !cbScanPeriodicData.Enabled || !cbScanDailyData.Enabled;

                info.SetError(cbScanEventData, !showInfo ? "" : "Деякі дані обчислювача вже опитуються");

                btnSave.Enabled = cbScanEventData.Enabled || cbScanAlarmData.Enabled || cbScanMinuteData.Enabled ||
                              cbScanPeriodicData.Enabled || cbScanDailyData.Enabled;
            };

            btnCancel.Select();
        }

        private void cbScanEventData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanEventDataChanged = cbScanEventData.Checked != Member.ScanEventData;
            SetChanged();
        }

        private void cbScanAlarmData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanAlarmDataChanged = cbScanAlarmData.Checked != Member.ScanAlarmData;
            SetChanged();
        }

        private void cbScanMinuteData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanMinuteDataChanged = cbScanMinuteData.Checked != Member.ScanMinuteData;
            SetChanged();
        }

        private void cbScanPeriodicData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanPeriodicDataChanged = cbScanPeriodicData.Checked != Member.ScanPeriodicData;
            SetChanged();
        }

        private void cbScanDailyData_CheckedChanged(object sender, System.EventArgs e)
        {
            _scanDailyDataChanged = cbScanDailyData.Checked != Member.ScanDailyData;
            SetChanged();
        }

        private void SetChanged()
        {
            _changed = _scanPeriodicDataChanged || _scanDailyDataChanged || _scanAlarmDataChanged ||
                       _scanEventDataChanged || _scanMinuteDataChanged;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Member.ScanAlarmData = cbScanAlarmData.Checked;
            Member.ScanDailyData = cbScanDailyData.Checked;
            Member.ScanEventData = cbScanEventData.Checked;
            Member.ScanMinuteData = cbScanMinuteData.Checked;
            Member.ScanPeriodicData = cbScanPeriodicData.Checked;

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
