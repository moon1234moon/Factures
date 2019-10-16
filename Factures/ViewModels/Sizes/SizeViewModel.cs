using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.ViewModels
{
    public class SizeViewModel : Conductor<object>
    {
        private BindableCollection<SizeModel> _sizes;

        public SizeViewModel()
        {
            SizeModel size = new SizeModel();
            Sizes = size.GiveCollection(size.All());
        }

        public BindableCollection<SizeModel> Sizes
        {
            get { return _sizes; }
            set
            {
                _sizes = value;
                NotifyOfPropertyChange(() => Sizes);
            }
        }

        public void Edit(SizeModel sm)
        {
            ActivateItem(new EditSizeViewModel(sm));
        }

        public void CreateNew()
        {
            ActivateItem(new CreateSizeViewModel());
        }
    }
}
