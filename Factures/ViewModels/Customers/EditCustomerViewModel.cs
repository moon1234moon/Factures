using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.ViewModels
{
    public class EditCustomerViewModel : Conductor<object>
    {
        private string _name;
        private string _phone;
        private BindableCollection<CustomertypeModel> _customer_types;
        private CustomertypeModel _customertype;
        private string _email;
        private int _selectedindex;
        private CustomerModel _customer;

        public EditCustomerViewModel(CustomerModel cm)
        {
            Customer = cm;
            CustomertypeModel ctm = new CustomertypeModel();
            DataTable cts = ctm.All();
            CustomerTypes = ctm.GetCollection(cts);
            Name = cm.Name;
            Phone = cm.Phone;
            Email = cm.Email;
            CustomerType = ctm.Get(cm.Type);
            for(int i = 0; i < cts.Rows.Count; i++)
            {
                string RowID = cts.Rows[i][0].ToString();
                string CustomerId = CustomerType.Id.ToString();
                if (RowID == CustomerId)
                {
                    SelectedIndex = i;
                    break;
                }
            }
        }

        #region
        public CustomerModel Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public int SelectedIndex
        {
            get { return _selectedindex; }
            set { _selectedindex = value; }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }


        public CustomertypeModel CustomerType
        {
            get { return _customertype; }
            set
            {
                _customertype = value;
                NotifyOfPropertyChange(() => CustomerType);
            }
        }


        public BindableCollection<CustomertypeModel> CustomerTypes
        {
            get { return _customer_types; }
            set { _customer_types = value; }
        }


        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                NotifyOfPropertyChange(() => Phone);
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }
        #endregion

        public void Update()
        {
            //Check Input
            KeyValuePair<bool, string> message = this.Check();
            if (message.Key)
            {
                //Save
                Customer.Name = this.Name;
                Customer.Type = this.CustomerType.Id;
                if (Phone != string.Empty && Phone != null)
                    Customer.Phone = this.Phone;
                if (Email != string.Empty && Email != null)
                    Customer.Email = this.Email;
                Customer = Customer.UpdateThis();
                MessageBox.Show("Customer Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #region
        private KeyValuePair<bool, string> Check()
        {
            KeyValuePair<bool, string> chk = new KeyValuePair<bool, string>(true, "OK");
            if (Name == string.Empty || Name == null)
            {
                chk = new KeyValuePair<bool, string>(false, "Name is required");
                MessageBox.Show(chk.Value, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (CustomerType == null)
            {
                chk = new KeyValuePair<bool, string>(false, "Customer Type is required");
                MessageBox.Show(chk.Value, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (Email != null && Email != string.Empty && !this.IsValidEmail(Email))
            {
                chk = new KeyValuePair<bool, string>(false, "Email is Invalid");
                MessageBox.Show(chk.Value, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return chk;
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        #endregion
    }
}
