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
        private BindableCollection<CurrencyModel> _currencies;
        private SeasonModel _receipt_season;
        private int _receipt_number;
        private SeasonModel _balance_season;
        private CurrencyModel _balance_currency;
        #endregion

        public ViewCustomerViewModel(CustomerModel customer)
        {
            Customer = customer;
            FillData();
        }

        #region
        public CurrencyModel BalanceCurrency
        {
            get { return _balance_currency; }
            set
            {
                _balance_currency = value;
                NotifyOfPropertyChange(() => BalanceCurrency);
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

        public SeasonModel BalanceSeason
        {
            get { return _balance_season; }
            set
            {
                _balance_season = value;
                NotifyOfPropertyChange(() => BalanceSeason);
            }
        }

        public int ReceiptNumber
        {
            get { return _receipt_number; }
            set
            {
                _receipt_number = value;
                NotifyOfPropertyChange(() => ReceiptNumber);
                FillReceipts();
            }
        }

        public SeasonModel ReceiptSeason
        {
            get { return _receipt_season; }
            set
            {
                _receipt_season = value;
                NotifyOfPropertyChange(() => ReceiptSeason);
                FillReceipts();
            }
        }

        public int FactureNumber
        {
            get { return _facture_number; }
            set
            {
                _facture_number = value;
                NotifyOfPropertyChange(() => FactureNumber);
                FillFactures();
            }
        }

        public SeasonModel FacturesSeason
        {
            get { return _facture_season; }
            set
            {
                _facture_season = value;
                NotifyOfPropertyChange(() => FacturesSeason);
                FillFactures();
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
                FillFactures();
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
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
            if (Currencies.Count > 0)
                BalanceCurrency = Currencies[0];
            FillSeasons();
            FillFactures();
            SetSearches();
            FillReceipts();
        }

        private void FillSeasons()
        {
            SeasonModel season = new SeasonModel();
            BindableCollection<SeasonModel> ss = season.GiveCollection(season.All());
            Seasons = new BindableCollection<SeasonModel>();
            season.Year = "All";
            Seasons.Add(season);
            season = new SeasonModel();
            season.Year = "None";
            Seasons.Add(season);
            foreach (SeasonModel s in ss)
            {
                Seasons.Add(s);
            }
        }

        private void SetSearches()
        {
            SetFactureSearchValues();
            ReceiptSeason = Seasons[0];
        }

        public void FillFactures()
        {
            if (FactureNumber.ToString().Trim() != "0" && (FactureNumber % 1) == 0)
            {
                try
                {
                    FactureModel fm = new FactureModel();
                    List<FactureModel> list = new List<FactureModel>() { fm.GetMeByNumber(FactureNumber, Customer.Id) };
                    if (list[0] == null)
                        throw new Exception("No facture for " + Customer.Name + " with number " + FactureNumber);
                    Factures = new BindableCollection<FactureModel>(list);
                }
                catch(Exception e)
                {
                    Factures = new BindableCollection<FactureModel>();
                }
            }
            else
            {
                switch (FactureType)
                {
                    case "All":
                        switch (FacturesSeason.Id.ToString())
                        {
                            case "0":
                                if (FacturesSeason.Year == "None")
                                {
                                    Factures = Customer.GetMyFactures(0);
                                }
                                else
                                    Factures = Customer.GetMyFactures();
                                break;
                            default:
                                Factures = Customer.GetMyFactures(FacturesSeason.Id);
                                break;
                        }
                        break;
                    case "Cleared":
                        switch (FacturesSeason.Id.ToString())
                        {
                            case "0":
                                if (FacturesSeason.Year == "None")
                                {
                                    Factures = Customer.GetMyClearedFactures(0);
                                }
                                else
                                    Factures = Customer.GetMyClearedFactures(FacturesSeason.Id);
                                break;
                            default:
                                Factures = Customer.GetMyClearedFactures(FacturesSeason.Id);
                                break;
                        }
                        break;
                    case "Uncleared":
                        switch (FacturesSeason.Id.ToString())
                        {
                            case "0":
                                if (FacturesSeason.Year == "None")
                                {
                                    Factures = Customer.GetMyUnClearedFactures(0);
                                }
                                else
                                    Factures = Customer.GetMyUnClearedFactures(FacturesSeason.Id);
                                break;
                            default:
                                Factures = Customer.GetMyUnClearedFactures(FacturesSeason.Id);
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void FillReceipts()
        {
            if (ReceiptNumber.ToString().Trim() != "0" && (ReceiptNumber % 1) == 0)
            {
                try
                {
                    ReceiptModel rm = new ReceiptModel();
                    List<ReceiptModel> list = new List<ReceiptModel>() { rm.GetMeByNumber(ReceiptNumber, Customer.Id) };
                    if (list[0] == null || list.Count == 0)
                        throw new Exception("No receipt for " + Customer.Name + " with number " + ReceiptNumber);
                    Receipts = new BindableCollection<ReceiptModel>(list);
                }
                catch (Exception e)
                {
                    Receipts = new BindableCollection<ReceiptModel>();
                }
            }
            else
            {
                switch (ReceiptSeason.Id.ToString())
                {
                    case "0":
                        if (ReceiptSeason.Year == "None")
                        {
                            Receipts = Customer.GetMyReceipts(0);
                        }
                        else
                            Receipts = Customer.GetMyReceipts();
                        break;
                    default:
                        Receipts = Customer.GetMyReceipts(FacturesSeason.Id);
                        break;
                }
            }
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

        #region
        public void ComputeBalance()
        {
            if (BalanceCurrency != null && BalanceCurrency.Id.ToString() != "0")
                ActivateItem(new BalanceViewModel(Customer, BalanceSeason, BalanceCurrency));
            else
                MessageBox.Show("Select a currency to view the balance in", "Select a Currency", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
