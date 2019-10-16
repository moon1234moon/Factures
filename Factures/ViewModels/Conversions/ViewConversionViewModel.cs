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
    public class ViewConversionViewModel : Conductor<object>
    {
        #region
        private decimal _ratio;
        private int _selected_first;
        private int _selected_second;
        private ConversionModel _conversion;
        private CurrencyModel _first;
        private CurrencyModel _second;
        private string _neat_conversion;
        private BindableCollection<CurrencyModel> _currencies;
        #endregion

        public ViewConversionViewModel(ConversionModel conversion)
        {
            // Fill Currencies
            DataTable CurrenciesTable = new DataTable();
            CurrencyModel currency = new CurrencyModel();
            CurrenciesTable = currency.All();
            Currencies = currency.GiveCollection(CurrenciesTable);
            // Fill Data
            Conversion = conversion;
            Ratio = Conversion.Ratio;
            First = new CurrencyModel();
            Second = new CurrencyModel();
            First = First.Get(Conversion.First);
            Second = Second.Get(Conversion.Second);
            for (int i = 0; i < CurrenciesTable.Rows.Count; i++)
            {
                string index = CurrenciesTable.Rows[i][0].ToString();
                string first = Conversion.First.ToString();
                string second = Conversion.Second.ToString();
                if (index == first)
                    SelectedFirst = i;
                if (index == second)
                    SelectedSecond = i;
            }
        }

        #region
        public int SelectedFirst
        {
            get { return _selected_first; }
            set
            {
                _selected_first = value;
                NotifyOfPropertyChange(() => SelectedFirst);
            }
        }

        public int SelectedSecond
        {
            get { return _selected_second; }
            set
            {
                _selected_second = value;
                NotifyOfPropertyChange(() => SelectedSecond);
            }
        }

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

        public ConversionModel Conversion
        {
            get { return _conversion; }
            set
            {
                _conversion = value;
                NotifyOfPropertyChange(() => Conversion);
            }
        }
        #endregion

        private void SetNeatConversion()
        {
            NeatConversion = "1 ";
            if (First != null)
            {
                NeatConversion += First.Symbol + " = " + Ratio + " ";
            }
            if (Second != null)
            {
                NeatConversion += Second.Symbol;
            }
        }

        public void Save()
        {
            if (CheckAll())
            {
                Conversion.First = this.First.Id;
                Conversion.Second = this.Second.Id;
                Conversion.Ratio = this.Ratio;
                try
                {
                    Conversion.UpdateThis();
                    MessageBox.Show("Conversion Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CheckAll()
        {
            if (First == null)
            {
                MessageBox.Show("Choose First Currency", "First Currency Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Second == null)
            {
                MessageBox.Show("Choose Second Currency", "Second Currency Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Ratio.ToString().Trim() == "0")
            {
                MessageBox.Show("Choose a Ratio", "Ratio Empty", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }
}
