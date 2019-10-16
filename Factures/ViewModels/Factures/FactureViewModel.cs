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
    public class FactureViewModel : Conductor<object>
    {
        private BindableCollection<FactureModel> _factures;
        private int _search_number;

        public FactureViewModel()
        {
            // Fill Factures
            FactureModel facture = new FactureModel();
            Factures = facture.GiveCollection(facture.All());
        }

        #region
        public int SearchNumber
        {
            get { return _search_number; }
            set { _search_number = value; }
        }

        public BindableCollection<FactureModel> Factures
        {
            get { return _factures; }
            set
            {
                _factures = value;
                NotifyOfPropertyChange(() => Factures);
            }
        }
        #endregion

        public void CreateNew()
        {
            ActivateItem(new CreateFactureViewModel());
        }

        public void View(FactureModel facture)
        {
            ActivateItem(new ViewFactureViewModel(facture));
        }

        public void Search()
        {
            FactureModel fm = new FactureModel();
            fm = fm.GetMeByNumber(SearchNumber);
            if (fm != null)
                View(fm);
            else
                MessageBox.Show("Number not found.", "Wrong Number", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
