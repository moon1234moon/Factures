using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.ViewModels
{
    class ProductsViewModel : Conductor<object>
    {
        private BindableCollection<ProductModel> _products;

        public ProductsViewModel()
        {
            ProductModel pm = new ProductModel();
            DataTable dt = pm.All();
            Products = pm.GiveCollection(dt);
        }

        public BindableCollection<ProductModel> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyOfPropertyChange(() => Products);
            }
        }

        public void BtnView(ProductModel pr)
        {
            ActivateItem(new EditProductViewModel(pr));
        }

        public void CreateNew()
        {
            ActivateItem(new CreateProductViewModel());
        }
    }
}
