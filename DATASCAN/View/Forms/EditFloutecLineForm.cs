using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DATASCAN.Core.Model.Floutecs;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditFloutecLineForm : Form
    {
        public FloutecMeasureLine Line { get; set; }

        public bool IsEdit { get; set; }

        public List<int> Numbers { get; set; }

        private bool _nameChanged;

        private bool _descriptionChanged;

        private bool _numberChanged;

        private bool _sensorTypeChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати вимірювальну нитку";

        private const string TITLE_EDIT = "Налаштування вимірювальної нитки";

        public EditFloutecLineForm()
        {
            InitializeComponent();

            cmbSensorType.Items.AddRange(new object[]{ "Діафрагма", "Лічильник", "Витратомір" });

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Line = new FloutecMeasureLine();
                    Text = TITLE_CREATE;
                    cmbSensorType.SelectedIndex = 0;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtName.Text = Line.Name;
                    txtDescription.Text = Line.Description;
                    numNumber.Value = Line.Number;
                    cmbSensorType.SelectedIndex = Line.SensorType;
                    Icon = Resources.Sensor1;
                }
            };

            btnSave.Select();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _nameChanged = !txtName.Text.Equals(Line.Name);
            SetChanged();
            err.SetError(txtName, "");
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _descriptionChanged = !txtDescription.Text.Equals(Line.Description);
            SetChanged();
        }

        private void numNumber_ValueChanged(object sender, EventArgs e)
        {
            _numberChanged = !numNumber.Value.Equals(Line.Number);
            SetChanged();
            err.SetError(numNumber, "");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateName() & ValidateNumber();

            if (valid)
            {
                Line.Name = txtName.Text;
                Line.Description = txtDescription.Text;
                Line.Number = (int)numNumber.Value;
                Line.SensorType = cmbSensorType.SelectedIndex;

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

        private void cmbSensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _sensorTypeChanged = !cmbSensorType.SelectedIndex.Equals(Line.SensorType);
            SetChanged();
        }

        private void SetChanged()
        {
            _changed = _nameChanged || _descriptionChanged || _numberChanged || _sensorTypeChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private bool ValidateName()
        {
            err.SetError(txtName, string.IsNullOrEmpty(txtName.Text) ? "Вкажіть назву нитки" : "");
            return string.IsNullOrEmpty(err.GetError(txtName));
        }

        private bool ValidateNumber()
        {
            err.SetError(numNumber, Numbers.Contains((int)numNumber.Value) ? "Номер нитки має бути унікальним" : "");
            return string.IsNullOrEmpty(err.GetError(numNumber));
        }
    }
}
