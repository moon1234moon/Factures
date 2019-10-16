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
    public class CreateConversionViewModel : Conductor<object>
    {
        private decimal _ratio;
        private CurrencyModel _first;
        private CurrencyModel _second;
        private string _neat_conversion;
        private BindableCollection<CurrencyModel> _currencies;

        public CreateConversionViewModel()
        {
            // Fill Currencies
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
        }

        #region
        public string NeatConversion
        {
            get { return _neat_conversion; }
            set
            {
                _neat_conversion = value;
                NotifyOfPropertyChange(() => NeatConversion);
            }
        }

        public decimal Ratio
        {
            get { return _ratio; }
            set
            {
                _ratio = value;
                NotifyOfPropertyChange(() => Ratio);
                SetNeatConversion();
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

        public CurrencyModel First
        {
            get { return _first; }
            set
            {
                _first = value;
                NotifyOfPropertyChange(() => First);
                SetNeatConversion();
            }
        }

        public CurrencyModel Second
        {
            get { return _second; }
            set
            {
                _second = value;
                NotifyOfPropertyChange(() => Second);
                SetNeatConversion();
            }
        }
        #endregion

        private void SetNeatConversion()
        {
            NeatConversion = "1 ";
            if(First != null)
            {
                NeatConversion += First.Symbol + " = "+ Ratio + " ";
            }
            if(Second != null)
            {
                NeatConversion += Second.Symbol;
            }
        }

        public void Save()
        {
            if(CheckAll())
            {
                ConversionModel cm = new ConversionModel()
                {
                    First = this.First.Id,
                    Second = this.Second.Id,
                    Ratio = this.Ratio,
                };
                try
                {
                    cm = cm.SaveThis();
                    MessageBox.Show("Conversion Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch(Exception e)
                {
                    MessageBox.Show("You already have this conversion", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CheckAll()
        {
            if(First == null)
            {
                MessageBox.Show("Choose First Currency", "First Currency Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Second == null)
            {
                MessageBox.Show("Choose Second Currency", "Second Currency Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Ratio.ToString().Trim() == "0")
            {
                MessageBox.Show("Choose a Ratio", "Ratio Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
