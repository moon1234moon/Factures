using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        private string _title = "Factures Appication";

        public string Title
        {
            get { return _title = "Factures Appication"; }
        }

        #region
        public void CustomerTypes()
        {
            ActivateItem(new CustomertypesViewModel());
        }

        public void Customers()
        {
            ActivateItem(new CustomersViewModel());
        }

        public void Seasons()
        {
            ActivateItem(new SeasonViewModel());
        }

        public void Factures()
        {
            ActivateItem(new FactureViewModel());
        }

        public void Products()
        {
            ActivateItem(new ProductsViewModel());
        }

        public void Sizes()
        {
            ActivateItem(new SizeViewModel());
        }

        public void Receipts()
        {
            ActivateItem(new ReceiptViewModel());
        }

        public void Conversions()
        {
            ActivateItem(new ConversionViewModel());
        }
        #endregion

    }
}
