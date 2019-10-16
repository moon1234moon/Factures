using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factures.ViewModels
{
    public class ConversionViewModel : Conductor<object>
    {
        private BindableCollection<ConversionModel> _conversions;

        public ConversionViewModel()
        {
            // Fill Conversions
            ConversionModel conversion = new ConversionModel();
            Conversions = conversion.GiveCollection(conversion.All());
        }

        #region
        public BindableCollection<ConversionModel> Conversions
        {
            get { return _conversions; }
            set
            {
                _conversions = value;
                NotifyOfPropertyChange(() => Conversions);
            }
        }
        #endregion

        public void CreateNew()
        {
            ActivateItem(new CreateConversionViewModel());
        }

        public void View(ConversionModel conversion)
        {
            ActivateItem(new ViewConversionViewModel(conversion));
        }

        public void Delete(ConversionModel conversion)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this conversion?", "Delete Conversion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool result = conversion.Delete();
                if (result)
                    MessageBox.Show("Conversion Deleted");
            }
        }
    }
}
