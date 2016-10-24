using System;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Core.Model.Scanning;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditScheduledScanForm : Form
    {
        public ScheduledScan Scan { get; set; }

        public bool IsEdit { get; set; }

        private bool _titleChanged;

        private bool _periodsChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати опитування за графіком";

        private const string TITLE_EDIT = "Налаштування опитування за графіком";

        public EditScheduledScanForm()
        {
            InitializeComponent();

            ToolTip addPeriodTooltip = new ToolTip();
            addPeriodTooltip.SetToolTip(btnAddPeriod, "Додати період");

            ToolTip deletePeriodTooltip = new ToolTip();
            deletePeriodTooltip.SetToolTip(btnDeletePeriod, "Видалити період");

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Scan = new ScheduledScan();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtTitle.Text = Scan.Title;
                    UpdatePeriods();
                }
            };
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            _titleChanged = !txtTitle.Text.Equals(Scan.Title);
            SetChanged();
            err.SetError(txtTitle, "");
        }

        private void lstPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDeletePeriod.Enabled = lstPeriods.SelectedItems.Count > 0 && lstPeriods.SelectedItems[0] != null;
        }

        private void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            err.SetError(txtPeriod, "");
        }

        private void btnAddPeriod_Click(object sender, EventArgs e)
        {
            if (ValidatePeriod())
            {
                TimeSpan period;
                bool valid = TimeSpan.TryParse(txtPeriod.Text, out period);

                if (valid)
                {
                    Scan.Periods.Add(new ScanPeriod
                    {
                        Period = period
                    });

                    UpdatePeriods();
                    _periodsChanged = true;
                    SetChanged();
                    txtPeriod.Text = string.Empty;
                }                
            }
        }

        private void btnDeletePeriod_Click(object sender, EventArgs e)
        {
            ScanPeriod period = lstPeriods.SelectedItems[0].Tag as ScanPeriod;

            Scan.Periods.Remove(period);
            UpdatePeriods();

            _periodsChanged = true;
            SetChanged();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateTitle())
            {
                Scan.Title = txtTitle.Text;

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

        private bool ValidatePeriod()
        {
            string errMessage;

            if (!txtPeriod.MaskCompleted)
            {
                errMessage = "Період вказано невірно";
            }
            else if (Scan.Periods.Select(p => p.Period.ToString(@"hh\:mm")).Contains(txtPeriod.Text))
            {
                errMessage = "Період в графіку вже присутній";
            }
            else
            {
                errMessage = "";
            }

            err.SetError(txtPeriod, errMessage);
            return string.IsNullOrEmpty(err.GetError(txtPeriod));
        }

        private bool ValidateTitle()
        {
            err.SetError(txtTitle, string.IsNullOrEmpty(txtTitle.Text) ? "Вкажіть назву опитування" : "");
            return string.IsNullOrEmpty(err.GetError(txtTitle));
        }

        private void UpdatePeriods()
        {
            lstPeriods.Items.Clear();

            Scan.Periods.OrderBy(o => o.Period).ToList().ForEach(p =>
            {
                ListViewItem item = new ListViewItem
                {
                    Text = p.Period.ToString(@"hh\:mm"),
                    Tag = p
                };
                lstPeriods.Items.Add(item);
            });

            btnDeletePeriod.Enabled = lstPeriods.SelectedItems.Count > 0 && lstPeriods.SelectedItems[0] != null;
        }

        private void SetChanged()
        {
            _changed = _titleChanged || _periodsChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }    
    }
}
