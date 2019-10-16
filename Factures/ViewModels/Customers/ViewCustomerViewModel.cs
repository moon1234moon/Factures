using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Factures.ViewModels
{
    public class ViewCustomerViewModel : Conductor<IScreen>.Collection.OneActive
    {
        #region
        private int _facture_number;
        private string[] _facture_types;
        private string _facture_type;
        private string _customer_name;
        private BindableCollection<ProductModel> _products;
        private CustomerModel _customer;
        private BindableCollection<CustomerModel> _customer_bound;
        private BitmapImage _product_image;
        private SeasonModel _facture_season;
        private BindableCollection<FactureModel> _factures;
        private BindableCollection<ReceiptModel> _receipts;
        private BindableCollection<SeasonModel> _seasons;
        #endregion

        public ViewCustomerViewModel(CustomerModel customer)
        {
            Customer = customer;
            FillData();
        }

        #region
        public int FactureNumber
        {
            get { return _facture_number; }
            set
            {
                _facture_number = value;
                NotifyOfPropertyChange(() => FactureNumber);
            }
        }

        public SeasonModel FacturesSeason
        {
            get { return _facture_season; }
            set
            {
                _facture_season = value;
                NotifyOfPropertyChange(() => FacturesSeason);
            }
        }

        public BindableCollection<SeasonModel> Seasons
        {
            get { return _seasons; }
            set
            {
                _seasons = value;
                NotifyOfPropertyChange(() => Seasons);
            }
        }

        public string FactureType
        {
            get { return _facture_type; }
            set
            {
                _facture_type = value;
                NotifyOfPropertyChange(() => FactureType);
            }
        }

        public string[] FactureTypes
        {
            get { return _facture_types; }
            set
            {
                _facture_types = value;
                NotifyOfPropertyChange(() => FactureTypes);
            }
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

        public BindableCollection<FactureModel> Factures
        {
            get { return _factures; }
            set
            {
                _factures = value;
                NotifyOfPropertyChange(() => Factures);
            }
        }

        public BitmapImage Image
        {
            get { return _product_image; }
            set
            {
                _product_image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public BindableCollection<CustomerModel> CustomerBound
        {
            get { return _customer_bound; }
            set
            {
                _customer_bound = value;
                NotifyOfPropertyChange(() => CustomerBound);
            }
        }

        public string CustomerName
        {
            get { return _customer_name; }
            set
            {
                _customer_name = value;
                NotifyOfPropertyChange(() => CustomerName);
            }
        }

        public CustomerModel Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyOfPropertyChange(() => Customer);
            }
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
        #endregion

        #region
        private void FillData()
        {
            CustomerName = Customer.Id + " - " + Customer.Name;
            List<CustomerModel> customer_bound = new List<CustomerModel>();
            customer_bound.Add(Customer);
            CustomerBound = new BindableCollection<CustomerModel>(customer_bound);
            Products = Customer.GetMyProducts();
            Factures = Customer.GetMyFactures();
            Receipts = Customer.GetMyReceipts();
            FillSeasons();
            SetSearches();
        }

        private void FillSeasons()
        {
            SeasonModel season = new SeasonModel();
            BindableCollection<SeasonModel> ss = season.GiveCollection(season.All());
            Seasons = new BindableCollection<SeasonModel>();
            Seasons.Add(new SeasonModel());
            foreach (SeasonModel s in ss)
            {
                Seasons.Add(s);
            }
        }

        private void SetSearches()
        {
            SetFactureSearchValues();
        }
        #endregion

        #region
        public void ViewProduct(ProductModel product)
        {
            BitmapImage image = product.GetImageFromDb();
            if(image != null)
            {
                Image = image;
            }
            else
            {
                Image = new BitmapImage();
                MessageBox.Show("There is no image set for product " + product.Id, "No Image", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void EditProduct(ProductModel product)
        {
            ActivateItem(new EditProductViewModel(product, true));
        }

        public void CreateProduct()
        {
            ProductModel product = new ProductModel();
            product.Customer = Customer.Id;
            ActivateItem(new CreateProductViewModel(product));
        }
        #endregion

        #region
        public void ViewFacture(FactureModel facture)
        {
            ActivateItem(new ViewFactureViewModel(facture, true));
        }

        public void CreateFacture()
        {
            FactureModel facture = new FactureModel();
            facture.Customer = Customer.Id;
            ActivateItem(new CreateFactureViewModel(facture));
        }

        private void SetFactureSearchValues()
        {
            FacturesSeason = Seasons[0];
            FactureTypes = new string[]
            {
                "All",
                "Cleared",
                "Uncleared",
            };
            FactureType = FactureTypes[0];
        }
        #endregion

        #region
        public void CreateReceipt()
        {
            ReceiptModel receipt = new ReceiptModel();
            receipt.Customer = Customer.Id;
            ActivateItem(new CreateReceiptViewModel(receipt));
        }

        public void ViewReceipt(ReceiptModel receipt)
        {
            ActivateItem(new ViewReceiptViewModel(receipt, true));
        }
        #endregion
    }
}
