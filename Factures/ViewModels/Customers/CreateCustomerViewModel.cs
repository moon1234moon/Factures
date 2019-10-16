using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.ViewModels
{
    class CreateCustomerViewModel : Conductor<object>
    {
        private string _name;
        private string _phone;
        private BindableCollection<CustomertypeModel> _customer_types;
        private CustomertypeModel _customertype;
        private string _email;

        public CreateCustomerViewModel()
        {
            CustomertypeModel ctm = new CustomertypeModel();
            CustomerTypes = ctm.GetCollection(ctm.All());
        }

        #region
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


        public string  Phone
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

        public void Create()
        {
            //Check Input
            KeyValuePair<bool,string> message = this.Check();
            if(message.Key)
            {
                //Save
                CustomerModel cm = new CustomerModel() {
                    Name = this.Name,
                    Type = this.CustomerType.Id,
                };
                if (Phone != string.Empty && Phone != null)
                    cm.Phone = this.Phone;
                if (Email != string.Empty && Email != null)
                    cm.Email = this.Email;
                cm = cm.SaveThis();
                MessageBox.Show("Customer Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private KeyValuePair<bool, string> Check()
        {
            KeyValuePair<bool, string> chk = new KeyValuePair<bool, string>(true, "OK");
            if(Name == string.Empty || Name == null)
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
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
