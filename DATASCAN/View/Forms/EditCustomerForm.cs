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

        public EditCustomerForm()
        {            
            InitializeComponent();            

            Load += (sender, args) =>
            {
                if (!IsEdit)
                {
                    Customer = new Customer();
                    Text = "Додати замовника";
                    Icon = Resources.Add;
                }
                else
                {
                    Text = "Деталі замовника";
                    txtTitle.Text = Customer.Title;
                    txtPerson.Text = Customer.Person;
                    txtPhone.Text = Customer.Phone;
                    txtEmail.Text = Customer.Email;
                    Icon = Resources.Information1;
                }
            };

            btnCancel.Select();
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
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
            lblTitleError.Visible = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lblEmailError.Visible = false;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            lblPhoneError.Visible = false;
        }
    }
}
