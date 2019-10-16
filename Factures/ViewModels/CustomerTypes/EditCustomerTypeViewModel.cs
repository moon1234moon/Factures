using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.ViewModels
{
    public class EditCustomerTypeViewModel : Conductor<object>
    {
        private string _type;
        private CustomertypeModel _customer;

        public EditCustomerTypeViewModel(CustomertypeModel cm)
        {
            Customer = cm;
            Type = cm.Name;
        }

        #region
        public CustomertypeModel Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyOfPropertyChange(() => Customer);
            }
        }


        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }
        #endregion

        public void Update()
        {
            Customer.Name = Type;
            Customer = Customer.UpdateThis();
            MessageBox.Show("Customer Type Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
