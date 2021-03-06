﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DATASCAN.Core.Entities.Rocs;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditRocPointForm : Form
    {
        public Roc809MeasurePoint Point { get; set; }

        public bool IsEdit { get; set; }

        public Dictionary<int, List<int>> Numbers { get; set; }

        private bool _nameChanged;

        private bool _descriptionChanged;

        private bool _numberChanged;

        private bool _histSegmentChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати вимірювальну точку";

        private const string TITLE_EDIT = "Налаштування вимірювальної точки";

        public EditRocPointForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Point = new Roc809MeasurePoint();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtName.Text = Point.Name;
                    txtDescription.Text = Point.Description;
                    numNumber.Value = Point.Number;
                    numHistSegment.Value = Point.HistSegment;
                    Icon = Resources.Sensor1;
                }
            };

            btnSave.Select();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _nameChanged = !txtName.Text.Equals(Point.Name);
            SetChanged();
            err.SetError(txtName, "");
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            _descriptionChanged = !txtDescription.Text.Equals(Point.Description);
            SetChanged();
        }

        private void numNumber_ValueChanged(object sender, EventArgs e)
        {
            _numberChanged = !numNumber.Value.Equals(Point.Number);
            SetChanged();
            err.SetError(numNumber, "");
        }

        private void numHistSegment_ValueChanged(object sender, EventArgs e)
        {
            _histSegmentChanged = !numHistSegment.Value.Equals(Point.HistSegment);
            SetChanged();
            err.SetError(numNumber, "");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateName() & ValidateNumber();

            if (valid)
            {
                Point.Name = txtName.Text;
                Point.Description = txtDescription.Text;
                Point.Number = (int)numNumber.Value;
                Point.HistSegment = (int)numHistSegment.Value;

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
            _changed = _nameChanged || _descriptionChanged || _numberChanged || _histSegmentChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private bool ValidateName()
        {
            err.SetError(txtName, string.IsNullOrEmpty(txtName.Text) ? "Вкажіть назву точки" : "");
            return string.IsNullOrEmpty(err.GetError(txtName));
        }

        private bool ValidateNumber()
        {
            err.SetError(numNumber, Numbers.Any(n => n.Key.Equals((int)numHistSegment.Value) & n.Value.Contains((int)numNumber.Value)) ? "Номер точки має бути унікальним в межах історичного сегменту" : "");
            return string.IsNullOrEmpty(err.GetError(numNumber));
        }
    }
}
