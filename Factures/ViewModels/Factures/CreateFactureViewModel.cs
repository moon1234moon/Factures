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
    public class CreateFactureViewModel : Conductor<object>
    {
        #region
        private string _name;
        private SeasonModel _season;
        private int _customer_id;
        private bool _cleared;
        private int _number;
        private int _quantity;
        private float _price;
        private string _total = "0";
        private DateTime _delivery_date;
        private CustomerModel _customer;
        private ProductModel _product;
        private SizeModel _size;
        private CurrencyModel _currency;
        private int _selected_currency;
        private BindableCollection<CustomerModel> _customers;
        private BindableCollection<SeasonModel> _seasons;
        private BindableCollection<ProductModel> _products;
        private BindableCollection<SizeModel> _sizes;
        private BindableCollection<CurrencyModel> _currencies;
        private BindableCollection<FactureDetailsModel> _facture_details;
        private List<FactureDetailsModel> _facture_details_list;
        private bool _is_enabled;
        #endregion

        public CreateFactureViewModel(FactureModel facture = null)
        {
            // Fill Customers
            CustomerModel customers = new CustomerModel();
            Customers = customers.GiveCollection(customers.All());
            // Fill Seasons
            SeasonModel sm = new SeasonModel();
            Seasons = sm.GiveCollection(sm.All());
            //Fill Sizes
            SizeModel size = new SizeModel();
            Sizes = size.GiveCollection(size.All());
            //Fill Currencies
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
            Delivery = DateTime.UtcNow.Date;
            if (facture != null)
            {
                for (int i = 0; i < Customers.Count; i++)
                {
                    int id1 = Customers[i].Id;
                    int id2 = (int)facture.Customer;
                    if (id1 == id2)
                    {
                        Customer = Customers[i];
                        break;
                    }
                }
                IsEnabled = false;
            }
            else
                IsEnabled = true;
        }

        #region
        public bool IsEnabled
        {
            get { return _is_enabled; }
            set
            {
                _is_enabled = value;
                NotifyOfPropertyChange(() => IsEnabled);
            }
        }

        public List<FactureDetailsModel> FactureDetailsList
        {
            get { return _facture_details_list; }
            set
            {
                _facture_details_list = value;
                NotifyOfPropertyChange(() => FactureDetailsList);
            }
        }

        public BindableCollection<FactureDetailsModel> FactureDetails
        {
            get { return _facture_details; }
            set
            {
                _facture_details = value;
                NotifyOfPropertyChange(() => FactureDetails);
            }
        }

        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyOfPropertyChange(() => Total);
            }
        }

        public float Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                SetTotal();
            }
        }

        public CurrencyModel Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                NotifyOfPropertyChange(() => Currency);
                SetTotal();
            }
        }

        public BindableCollection<CurrencyModel> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                NotifyOfPropertyChange(() => Currencies);
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                NotifyOfPropertyChange(() => Quantity);
                SetTotal();
            }
        }

        public SizeModel Size
        {
            get { return _size; }
            set
            {
                _size = value;
                NotifyOfPropertyChange(() => Size);
            }
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

        public ProductModel Product
        {
            get { return _product; }
            set
            {
                _product = value;
                NotifyOfPropertyChange(() => Product);
                if (Product != null && Product.Price != null && Product.Price != 0)
                {
                    Price = (float)Product.Price;
                    int currency_id = (int)Product.Currency;
                    for(int i = 0; i < Currencies.Count; i++)
                    {
                        if (Currencies[i].Id == currency_id)
                        {
                            SelectedCurrency = i;
                            break;
                        }
                    }
                }
                else
                {
                    Price = 0;
                }
            }
        }

        public int SelectedCurrency
        {
            get { return _selected_currency; }
            set
            {
                _selected_currency = value;
                NotifyOfPropertyChange(() => SelectedCurrency);
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

        public BindableCollection<SeasonModel> Seasons
        {
            get { return _seasons; }
            set
            {
                _seasons = value;
                NotifyOfPropertyChange(() => Seasons);
            }
        }

        public CustomerModel Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                NotifyOfPropertyChange(() => Customer);
                // Fill Products
                ProductModel prod = new ProductModel();
                Products = prod.GiveCustomerProducts(_customer.Id);
                ResetForm();
            }
        }

        public BindableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }

        public SeasonModel Season
        {
            get { return _season; }
            set
            {
                _season = value;
                NotifyOfPropertyChange(() => Season);
            }
        }

        public DateTime Delivery
        {
            get { return _delivery_date; }
            set
            {
                _delivery_date = value;
                NotifyOfPropertyChange(() => Delivery);
            }
        }

        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                NotifyOfPropertyChange(() => Number);
            }
        }

        public bool Cleared
        {
            get { return _cleared; }
            set
            {
                _cleared = value;
                NotifyOfPropertyChange(() => Cleared);
            }
        }

        public int Customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
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

        #region
        private void SetTotal()
        {
            if(Price != null && Quantity != null)
            {
                float total = Price * Quantity;
                Total = total + " ";
                if (Currency != null && Currency.Symbol != null && Currency.Symbol != string.Empty)
                    Total += Currency.Symbol;
            }
        }

        private FactureDetailsModel FactureDetailsBuild()
        {
            FactureDetailsModel facture_detail = new FactureDetailsModel();
            try
            {
                facture_detail.Product = Product.Id;
                facture_detail.ProductName = Product.Name;
                if ((Price != null))
                {
                    facture_detail.Price = (decimal)Price;
                    if (Currency != null)
                    {
                        facture_detail.Currency = Currency.Id;
                        facture_detail.FullPrice = Price.ToString() + " " + Currency.Symbol;
                    }
                    else
                    {
                        MessageBox.Show("Currency is required when you add a price", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    SetTotal();
                    facture_detail.Total = Total;
                }
                if (Quantity != null)
                    facture_detail.Quantity = Quantity;
                if (Size != null)
                {
                    facture_detail.Size = Size.Id;
                    facture_detail.SizeSymbol = Size.Size;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Data is Empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            return facture_detail;
        }

        private void ResetForm()
        {
            Price = 0;
            FactureDetails = new BindableCollection<FactureDetailsModel>();
            FactureDetailsList = new List<FactureDetailsModel>();
            //Fill Sizes
            SizeModel size = new SizeModel();
            Sizes = size.GiveCollection(size.All());
            Cleared = false;
            Delivery = DateTime.UtcNow.Date;

        }
        #endregion

        #region
        public void AddProduct()
        {
            // Create Entry for BindableCollection and Insert It
            FactureDetailsModel fd = FactureDetailsBuild();
            if (fd != null)
            {
                if (FactureDetailsList == null)
                {
                    FactureDetailsList = new List<FactureDetailsModel>
                    {
                        fd
                    };
                    FactureDetails = new BindableCollection<FactureDetailsModel>(FactureDetailsList);
                }
                else
                {
                    FactureDetailsList.Add(fd);
                    FactureDetails = new BindableCollection<FactureDetailsModel>(FactureDetailsList);
                }
            }
        }

        public void DeleteDetail(FactureDetailsModel fd)
        {
            FactureDetailsList.Remove(fd);
            FactureDetails = new BindableCollection<FactureDetailsModel>(FactureDetailsList);
        }
        #endregion

        public void Save()
        {
            // Create Facture and Save it
            FactureModel NewFacture = CreateFacture();
            if (NewFacture != null)
            {
                NewFacture = NewFacture.SaveThis();
                // Get Facture Id
                int NewFactureId = NewFacture.Id;
                // Loop over Details List and Save FactureDetails
                foreach(FactureDetailsModel facture_detail in FactureDetailsList)
                {
                    facture_detail.Facture = NewFactureId;
                    facture_detail.SaveThis();
                }
                MessageBox.Show("Facture Created Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private FactureModel CreateFacture()
        {
            if (CheckData())
            {
                FactureModel facture = new FactureModel()
                {
                    Number = this.Number,
                    Name = this.Name,
                    Customer = this.Customer.Id,
                    Cleared = this.Cleared,
                    Delivery = this.Delivery,
                };
                if (Season != null)
                    facture.Season = Season.Id;
                return facture;
            }
            return null;
        }

        private bool CheckData()
        {
            if (Name == string.Empty || Name == null)
                Name = "";
            if (Number == 0)
            {
                MessageBox.Show("Number is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Customer == null)
            {
                MessageBox.Show("Customer is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
