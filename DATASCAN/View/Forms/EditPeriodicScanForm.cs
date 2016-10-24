using System;
using System.Windows.Forms;
using DATASCAN.Core.Entities.Scanning;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditPeriodicScanForm : Form
    {
        public PeriodicScan Scan { get; set; }

        public bool IsEdit { get; set; }

        private bool _titleChanged;

        private bool _periodChanged;

        private bool _periodTypeChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати періодичне опитування";

        private const string TITLE_EDIT = "Налаштування періодичного опитування";

        public EditPeriodicScanForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Scan = new PeriodicScan();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtTitle.Text = Scan.Title;
                    numPeriod.Value = Scan.Period;
                    rbMinutes.Checked = !Scan.PeriodType;
                    rbHours.Checked = Scan.PeriodType;
                    Icon = Resources.PeriodicScan1;
                }
            };
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            _titleChanged = !txtTitle.Text.Equals(Scan.Title);
            SetChanged();
            err.SetError(txtTitle, "");
        }

        private void numPeriod_ValueChanged(object sender, EventArgs e)
        {
            _periodChanged = !numPeriod.Value.Equals(Scan.Period);
            SetChanged();
        }

        private void rbMinutes_CheckedChanged(object sender, EventArgs e)
        {
            _periodTypeChanged = rbMinutes.Checked == Scan.PeriodType;
            SetChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateTitle())
            {
                Scan.Title = txtTitle.Text;
                Scan.Period = (int)numPeriod.Value;
                Scan.PeriodType = rbHours.Checked;

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

        private void SetChanged()
        {
            _changed = _titleChanged || _periodChanged || _periodTypeChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private bool ValidateTitle()
        {
            err.SetError(txtTitle, string.IsNullOrEmpty(txtTitle.Text) ? "Вкажіть назву опитування" : "");
            return string.IsNullOrEmpty(err.GetError(txtTitle));
        }      
    }
}
