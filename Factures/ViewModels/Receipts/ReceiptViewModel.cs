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
    public class ReceiptViewModel : Conductor<object>
    {
        private BindableCollection<ReceiptModel> _receipts;
        private int _search_number;

        public ReceiptViewModel()
        {
            // Fill Receipts
            ReceiptModel receipt = new ReceiptModel();
            Receipts = receipt.GiveCollection(receipt.All());
        }

        #region
        public int SearchNumber
        {
            get { return _search_number; }
            set { _search_number = value; }
        }

        public BindableCollection<ReceiptModel> Receipts
        {
            get { return _receipts; }
            set
            {
                _receipts = value;
                NotifyOfPropertyChange(() => Receipts);
            }
        }
        #endregion

        public void CreateNew()
        {
            ActivateItem(new CreateReceiptViewModel());
        }

        public void View(ReceiptModel receipt)
        {
            ActivateItem(new ViewReceiptViewModel(receipt));
        }

        public void Search()
        {
            ReceiptModel rm = new ReceiptModel();
            rm = rm.GetMeByNumber(SearchNumber);
            if (rm.Id != null)
                View(rm);
            else
                MessageBox.Show("Number not found.", "Wrong Number", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
