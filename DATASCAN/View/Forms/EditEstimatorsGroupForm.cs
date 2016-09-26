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

        private bool _changed;

        private const string TITLE_CREATE = "Додати групу обчислювачів";

        private const string TITLE_EDIT = "Інформація про групу обчислювачів";

        public EditEstimatorsGroupForm()
        {
            InitializeComponent();

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Group = new EstimatorsGroup();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtName.Text = Group.Name;
                    Icon = Resources.Group1;
                }
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateName();

            if (valid)
            {
                Group.Name = txtName.Text;

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
            _changed = !txtName.Text.Equals(Group.Name);
            err.SetError(txtName, "");

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }

        private bool ValidateName()
        {
            err.SetError(txtName, string.IsNullOrEmpty(txtName.Text) ? "Вкажіть назву групи" : "");
            return string.IsNullOrEmpty(err.GetError(txtName));
        }
    }
}
