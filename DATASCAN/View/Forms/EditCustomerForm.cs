using System;
using System.Net.Mail;
using System.Windows.Forms;
using DATASCAN.Model;
using DATASCAN.Properties;

namespace DATASCAN.View.Forms
{
    public partial class EditCustomerForm : Form
    {
        public Customer Customer { get; set; }

        public bool IsEdit { get; set; }

        private bool _titleChanged;

        private bool _personChanged;

        private bool _phoneChanged;

        private bool _emailChanged;

        private bool _changed;

        private const string TITLE_CREATE = "Додати замовника";

        private const string TITLE_EDIT = "Інформація про замовника";

        public EditCustomerForm()
        {            
            InitializeComponent();            

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Customer = new Customer();
                    Text = TITLE_CREATE;
                    Icon = Resources.Add;
                }
                else
                {
                    Text = TITLE_EDIT;
                    txtTitle.Text = Customer.Title;
                    txtPerson.Text = Customer.Person;
                    txtPhone.Text = Customer.Phone;
                    txtEmail.Text = Customer.Email;
                    Icon = Resources.Customer1;
                }
            };

            btnCancel.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool valid = ValidateFields();

            if (valid)
            {
                Customer.Title = txtTitle.Text;
                Customer.Person = txtPerson.Text;
                Customer.Phone = txtPhone.Text;
                Customer.Email = txtEmail.Text;

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

        private bool ValidateFields()
        {
            bool titleIsValid = !string.IsNullOrEmpty(txtTitle.Text);
            bool emailIsValid = true;
            bool phoneIsValid = string.IsNullOrEmpty(txtPhone.Text) || txtPhone.MaskCompleted;

            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                try
                {
                    // ReSharper disable once UnusedVariable
                    MailAddress email = new MailAddress(txtEmail.Text);
                }
                catch
                {
                    emailIsValid = false;
                }
            }

            lblTitleError.Visible = !titleIsValid;
            lblEmailError.Visible = !emailIsValid;
            lblPhoneError.Visible = !phoneIsValid;

            return titleIsValid && emailIsValid && phoneIsValid;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            _titleChanged = !txtTitle.Text.Equals(Customer.Title);
            SetChanged();
            lblTitleError.Visible = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            _emailChanged = !txtTitle.Text.Equals(Customer.Email);
            SetChanged();
            lblEmailError.Visible = false;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            _phoneChanged = !txtTitle.Text.Equals(Customer.Phone);
            SetChanged();
            lblPhoneError.Visible = false;
        }

        private void txtPerson_TextChanged(object sender, EventArgs e)
        {
            _personChanged = !txtTitle.Text.Equals(Customer.Person);
            SetChanged();
        }

        private void SetChanged()
        {
            _changed = _titleChanged || _personChanged || _phoneChanged || _emailChanged;

            if (IsEdit)
            {
                Text = _changed ? TITLE_EDIT + " *" : TITLE_EDIT;
            }
        }
    }
}
