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
    class CreateCustomerTypeViewModel : Conductor<object>
    {
        private string _type;

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyOfPropertyChange(() => Type);
            }
        }

        public void Create()
        {
            CustomertypeModel ct = new CustomertypeModel
            {
                Name = Type
            };
            ct = ct.SaveThis();
            MessageBox.Show("Customer Type Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
