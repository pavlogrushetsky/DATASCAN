using System;
using System.Windows.Forms;
using DATASCAN.Model;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditEstimatorsGroupForm : Form
    {
        public EstimatorsGroup Group { get; set; }

        public bool IsEdit { get; set; }

        public EditEstimatorsGroupForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Group = new EstimatorsGroup();
                    Text = "Додати групу обчислювачів";
                    Icon = Resources.Add;
                }
                else
                {
                    Text = "Інформація про групу обчислювачів";
                    txtName.Text = Group.Name;
                    Icon = Resources.Information1;
                }
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateFields();

            if (valid)
            {
                Group.Name = txtName.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool ValidateFields()
        {
            bool nameIsValid = !string.IsNullOrEmpty(txtName.Text);

            lblNameError.Visible = !nameIsValid;

            return nameIsValid;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            lblNameError.Visible = false;
        }
    }
}
