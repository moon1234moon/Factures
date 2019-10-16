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
    public class ViewReceiptViewModel : Conductor<object>
    {
        #region
        private SeasonModel _season;
        private int _customer_id;
        private int _number;
        private string _details;
        private float? _amount;
        private ReceiptModel _receipt;
        private DateTime _delivery_date;
        private CustomerModel _customer;
        private CurrencyModel _currency;
        private int _selected_currency;
        private int _selected_customer;
        private int _selected_season;
        private int _selected_facture;
        private FactureModel _facture;
        private string _cheque;
        private string _main_button;
        private bool _enabled;
        private bool _facture_changed;
        private int? _old_facture = null;
        private BindableCollection<CustomerModel> _customers;
        private BindableCollection<SeasonModel> _seasons;
        private BindableCollection<CurrencyModel> _currencies;
        private BindableCollection<FactureModel> _factures;
        private bool _is_customer_enabled;
        private bool _is_customer_fixed;
        #endregion

        public ViewReceiptViewModel(ReceiptModel receipt, bool customer_fixed = false)
        {
            Receipt = receipt;
            // Fill Customers
            CustomerModel customers = new CustomerModel();
            DataTable cs = customers.All();
            Customers = customers.GiveCollection(cs);
            // Fill Seasons
            SeasonModel sm = new SeasonModel();
            DataTable ss = sm.All();
            Seasons = sm.GiveCollection(ss);
            //Fill Currencies
            CurrencyModel currency = new CurrencyModel();
            DataTable cc = currency.All();
            Currencies = currency.GiveCollection(cc);
            FillForm(cs, ss, cc);
            Enabled = false;
            MainButtonText = "Enable Editing";
            FactureChanged = false;
            IsCustomerFixed = customer_fixed;
            if (customer_fixed == false)
                IsCustomerEnabled = Enabled;
            else
                IsCustomerEnabled = false;
        }

        #region
        public bool IsCustomerFixed
        {
            get { return _is_customer_fixed; }
            set
            {
                _is_customer_fixed = value;
                NotifyOfPropertyChange(() => IsCustomerFixed);
            }
        }

        public bool IsCustomerEnabled
        {
            get { return _is_customer_enabled; }
            set
            {
                _is_customer_enabled = value;
                NotifyOfPropertyChange(() => IsCustomerEnabled);
            }
        }

        public int? OldFacture
        {
            get { return _old_facture; }
            set { _old_facture = value; }
        }

        public bool FactureChanged
        {
            get { return _facture_changed; }
            set { _facture_changed = value; }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                NotifyOfPropertyChange(() => Enabled);
                if (!IsCustomerFixed)
                    IsCustomerEnabled = _enabled;
            }
        }

        public string MainButtonText
        {
            get { return _main_button; }
            set
            {
                _main_button = value;
                NotifyOfPropertyChange(() => MainButtonText);
            }
        }

        public int SelectedFacture
        {
            get { return _selected_facture; }
            set
            {
                _selected_facture = value;
                NotifyOfPropertyChange(() => SelectedFacture);
            }
        }

        public int SelectedSeason
        {
            get { return _selected_season; }
            set
            {
                _selected_season = value;
                NotifyOfPropertyChange(() => SelectedSeason);
            }
        }

        public int SelectedCustomer
        {
            get { return _selected_customer; }
            set
            {
                _selected_customer = value;
                NotifyOfPropertyChange(() => SelectedCustomer);
            }
        }

        public ReceiptModel Receipt
        {
            get { return _receipt; }
            set
            {
                _receipt = value;
                NotifyOfPropertyChange(() => Receipt);
            }
        }

        public string Cheque
        {
            get { return _cheque; }
            set
            {
                _cheque = value;
                NotifyOfPropertyChange(() => Cheque);
            }
        }

        public FactureModel Facture
        {
            get { return _facture; }
            set
            {
                _facture = value;
                FactureChanged = true;
                NotifyOfPropertyChange(() => Facture);
                if (Currency != null && Facture != null)
                    Amount = Facture.TotalAmount(Currency.Id);
                else
                {
                    if (Facture != null)
                        MessageBox.Show("Please set the currency you want to convert the total amount to", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                NotifyOfPropertyChange(() => Details);
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

        public float? Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }

        public CurrencyModel Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                NotifyOfPropertyChange(() => Currency);
                if (Facture != null)
                {
                    Amount = Facture.TotalAmount(value.Id);
                }
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

        public int SelectedCurrency
        {
            get { return _selected_currency; }
            set
            {
                _selected_currency = value;
                NotifyOfPropertyChange(() => SelectedCurrency);
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
                Factures = Customer.GetMyUnClearedFactures();
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

        public int Customer_id
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        #endregion

        public void Save()
        {
            if (Enabled == false)
            {
                MainButtonText = "Update Receipt";
                Enabled = true;
            }
            else
            if (CheckForm())
            {
                ReceiptModel rm = Receipt;
                rm.Number = Number;
                rm.Customer = Customer.Id;
                rm.Amount = (decimal)Amount;
                rm.Currency = Currency.Id;
                rm.Delivery = Delivery;
                if (Details != null && Details.Trim() != string.Empty)
                    rm.Details = Details.Trim();
                if (Season != null)
                    rm.Season = Season.Id;
                if (Facture != null)
                    rm.Facture = Facture.Id;
                if (Cheque != null && Cheque.Trim() != string.Empty)
                    rm.Cheque = Cheque.Trim();
                rm = rm.UpdateThis();
                if (rm.Facture != null && rm.Facture.ToString() != "0")
                {
                    if(OldFacture != null)
                    {
                        FactureModel fm = new FactureModel();
                        fm = fm.Get((int)OldFacture);
                        fm.ClearUnclear(false);
                    }
                    rm.ClearFacture();
                }
                System.Windows.MessageBox.Show("Receipt created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void Delete()
        {
            var dialogResult = MessageBox.Show("Are you sure you want to delete this receipt?", "Delete Receipt", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                bool result = Receipt.Delete();
                if (result)
                    System.Windows.MessageBox.Show("Receipt Deleted");
            }
        }

        public void ClearFacture()
        {
            Facture = null;
        }

        public void ClearSeason()
        {
            Season = null;
        }

        public void FillForm(DataTable customers, DataTable seasons, DataTable currencies)
        {
            Delivery = (DateTime)Receipt.Delivery;
            Number = (int)Receipt.Number;
            Details = Receipt.Details;
            Cheque = Receipt.Cheque;
            for(int i = 0; i < customers.Rows.Count; i++)
            {
                string id = Receipt.Customer.ToString();
                string RowId = customers.Rows[i][0].ToString();
                if (id == RowId)
                {
                    SelectedCustomer = i;
                    Customer = Customers[i];
                    break;
                }
            }
            if (Receipt.Season != null)
                for (int i = 0; i < seasons.Rows.Count; i++)
                {
                    if (Receipt.Season.ToString() == seasons.Rows[i][0].ToString())
                    {
                        SelectedSeason = i;
                        Season = Seasons[i];
                        break;
                    }
                }
            if (Receipt.Currency != null)
                for (int i = 0; i < currencies.Rows.Count; i++)
                {
                    if (Receipt.Currency.ToString() == currencies.Rows[i][0].ToString())
                    {
                        SelectedCurrency = i;
                        Currency = Currencies[i];
                        break;
                    }
                }
            if (Receipt.Facture != null)
            {
                CustomerModel cm = new CustomerModel();
                cm = cm.Get((int)Receipt.Customer);
                Factures = cm.GetMyUnClearedFacturesWithOldCleared((int)Receipt.Facture);
                OldFacture = Receipt.Facture;
                for (int i = 0; i < Factures.Count; i++)
                {
                    if (Receipt.Facture.ToString() == Factures[i].Id.ToString())
                    {
                        SelectedFacture = i;
                        Facture = Factures[i];
                        break;
                    }
                }
            }
            Amount = (float)Receipt.Amount;
        }

        #region
        public bool CheckForm()
        {
            if (Number.ToString().Trim() == string.Empty || Number.ToString().Trim() == "0")
            {
                MessageViewed("Number");
                return false;
            }
            if (Customer == null)
            {
                MessageViewed("Customer");
                return false;
            }
            if (Amount.ToString().Trim() == string.Empty || Amount.ToString().Trim() == "0")
            {
                MessageViewed("Amount");
                return false;
            }
            if (Currency == null)
            {
                MessageViewed("Currency");
                return false;
            }
            if (Facture != null)
            {
                if (Amount != Facture.TotalAmount(Currency.Id))
                {
                    MessageViewed("AmountToTotal");
                    return false;
                }
            }
            return true;
        }

        public void MessageViewed(string msg)
        {
            switch (msg)
            {
                case "Number":
                    System.Windows.MessageBox.Show("Number can't be empty", "Number Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Customer":
                    MessageBox.Show("You must choose a customer", "Customer Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Amount":
                    MessageBox.Show("You must add an amount", "Amount Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Currency":
                    MessageBox.Show("You must choose a currency", "Currency Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "AmountToTotal":
                    MessageBox.Show("Amount must be equal to the total amount of the facture", "Amount - Total Inequality", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
