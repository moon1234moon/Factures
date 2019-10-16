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
    public class CreateReceiptViewModel : Conductor<object>
    {
        #region
        private SeasonModel _season;
        private int _customer_id;
        private int _number;
        private string _details;
        private float? _amount;
        private DateTime _delivery_date;
        private CustomerModel _customer;
        private CurrencyModel _currency;
        private int _selected_currency;
        private FactureModel _facture;
        private string _cheque;
        private BindableCollection<CustomerModel> _customers;
        private BindableCollection<SeasonModel> _seasons;
        private BindableCollection<CurrencyModel> _currencies;
        private BindableCollection<FactureModel> _factures;
        private bool _is_enabled;
        #endregion

        public CreateReceiptViewModel(ReceiptModel receipt = null)
        {
            // Fill Customers
            CustomerModel customers = new CustomerModel();
            Customers = customers.GiveCollection(customers.All());
            // Fill Seasons
            SeasonModel sm = new SeasonModel();
            Seasons = sm.GiveCollection(sm.All());
            //Fill Currencies
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
            Delivery = DateTime.UtcNow.Date;
            if(receipt != null)
            {
                for (int i = 0; i < Customers.Count; i++)
                {
                    int id1 = Customers[i].Id;
                    int id2 = (int)receipt.Customer;
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
                NotifyOfPropertyChange(() => Facture);
                if (Currency != null)
                    Amount = Facture.TotalAmount(Currency.Id);
                else
                {
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
            if (CheckForm())
            {
                ReceiptModel rm = new ReceiptModel()
                {
                    Number = Number,
                    Customer = Customer.Id,
                    Amount = (decimal)Amount,
                    Currency = Currency.Id,
                    Delivery = Delivery
                };
                if (Details != null && Details.Trim() != string.Empty)
                    rm.Details = Details.Trim(); ;
                if (Season != null)
                    rm.Season = Season.Id;
                if (Facture != null)
                    rm.Facture = Facture.Id;
                if (Cheque != null && Cheque.Trim() != string.Empty)
                    rm.Cheque = Cheque.Trim();
                rm = rm.SaveThis();
                if(rm.Facture != null && rm.Facture.ToString() != "0")
                {
                    rm.ClearFacture();
                }
                MessageBox.Show("Receipt created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        #region
        public bool CheckForm()
        {
            if(Number.ToString().Trim() == string.Empty || Number.ToString().Trim() == "0")
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
            if(Facture != null)
            {
                if(Amount != Facture.TotalAmount(Currency.Id))
                {
                    MessageViewed("AmountToTotal");
                    return false;
                }
            }
            return true;
        }

        public void MessageViewed(string msg)
        {
            switch(msg)
            {
                case "Number":
                    MessageBox.Show("Number can't be empty", "Number Empty", MessageBoxButton.OK, MessageBoxImage.Error);
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
