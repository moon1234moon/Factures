using Caliburn.Micro;
using Factures.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.Models
{
    public class ConversionModel : Model
    {
        #region
        private int _currency_id_first;
        private int _currency_id_second;
        private decimal _ratio_one_to_two;
        private CurrencyModel _first_currency;
        private CurrencyModel _second_currency;
        private string _first_symbol;
        private string _second_symbol;
        #endregion

        public ConversionModel()
        {
            this.Table = "conversions";
            this.Fillable = new string[3];
            this.Fillable[0] = "currency_id_first";
            this.Fillable[1] = "currency_id_second";
            this.Fillable[2] = "ratio_one_to_two";
        }

        #region
        public string FirstSymbol
        {
            get { return _first_symbol; }
            set { _first_symbol = value; }
        }

        public string SecondSymbol
        {
            get { return _second_symbol; }
            set { _second_symbol = value; }
        }

        public CurrencyModel FirstCurrency
        {
            get { return _first_currency; }
            set { _first_currency = value; }
        }

        public CurrencyModel SecondCurrency
        {
            get { return _second_currency; }
            set { _second_currency = value; }
        }

        public decimal Ratio
        {
            get { return _ratio_one_to_two; }
            set { _ratio_one_to_two = value; }
        }

        public int Second
        {
            get { return _currency_id_second; }
            set { _currency_id_second = value; }
        }

        public int First
        {
            get { return _currency_id_first; }
            set { _currency_id_first = value; }
        }
        #endregion

        #region
        public BindableCollection<ConversionModel> GiveCollection(DataTable cs)
        {
            List<ConversionModel> output = new List<ConversionModel>();
            foreach (DataRow row in cs.Rows)
            {
                CurrencyModel currency = new CurrencyModel();
                CurrencyModel currency2 = new CurrencyModel();
                ConversionModel cm = new ConversionModel
                {
                    First = System.Convert.ToInt32(row[0].ToString()),
                    Second = System.Convert.ToInt32(row[1].ToString()),
                    Ratio = System.Convert.ToDecimal(row[2].ToString())
                };
                cm.FirstCurrency = currency.Get(cm.First);
                cm.SecondCurrency = currency2.Get(cm.Second);
                cm.FirstSymbol = cm.FirstCurrency.Symbol;
                cm.SecondSymbol = cm.SecondCurrency.Symbol;
                output.Add(cm);
            }
            return new BindableCollection<ConversionModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", First.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("currency_id_first", value));
            value = new string[2] { "number", Second.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("currency_id_second", value));
            value = new string[2] { "number", Ratio.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("ratio_one_to_two", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> ReverseFill()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", Second.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("currency_id_first", value));
            value = new string[2] { "number", First.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("currency_id_second", value));
            decimal NewRatio = 1 / Ratio;
            value = new string[2] { "number", NewRatio.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("ratio_one_to_two", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> FillMeForUpdate()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", Ratio.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("ratio_one_to_two", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> Primaries()
        {
            string[] value = new string[2] { "number", First.ToString() };
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("currency_id_first", value)
            };
            value = new string[2] { "number", Second.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("currency_id_second", value));
            return Data;
        }

        public ConversionModel ReturnMeFromDataTable(DataTable dt)
        {
            CurrencyModel cm = new CurrencyModel();
            foreach (DataRow row in dt.Rows)
            {
                this.First = System.Convert.ToInt32(row[0].ToString());
                this.Second = System.Convert.ToInt32(row[1].ToString());
                this.Ratio = System.Convert.ToDecimal(row[2].ToString());
                this.FirstCurrency = cm.Get(System.Convert.ToInt32(row[0].ToString()));
                this.SecondCurrency = cm.Get(System.Convert.ToInt32(row[1].ToString()));
            }
            return this;
        }
        #endregion

        #region
        public ConversionModel SaveThis()
        {
            //Save
            DataTable dt = this.Save(this.FillMe(), false);
            return this;
        }

        public ConversionModel UpdateThis()
        {
            //Save
            this.Update(this.FillMeForUpdate(), this.Primaries());
            return this;
        }

        public bool Delete()
        {
            this.DeleteThis(this.Primaries());
            return true;
        }

        public ConversionModel GetMeByFromTo(int from, int to)
        {
            ConversionModel cm = new ConversionModel();
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", from.ToString() };
            conditions.Add(new KeyValuePair<string, string[]>("currency_id_first", value));
            value = new string[2] { "number", to.ToString() };
            conditions.Add(new KeyValuePair<string, string[]>("currency_id_second", value));
            cm = ReturnMeFromDataTable(cm.FindByParameters(conditions));
            if (cm != null && cm.First.ToString() != "0" && cm.Second.ToString() != "0" && cm.Ratio.ToString() != "0")
            {
                return cm;
            }
            else
            {
                conditions = new List<KeyValuePair<string, string[]>>();
                value = new string[2] { "number", to.ToString() };
                conditions.Add(new KeyValuePair<string, string[]>("currency_id_first", value));
                value = new string[2] { "number", from.ToString() };
                conditions.Add(new KeyValuePair<string, string[]>("currency_id_second", value));
                cm = ReturnMeFromDataTable(cm.FindByParameters(conditions));
                if (cm != null && cm.First.ToString() != "0" && cm.Second.ToString() != "0" && cm.Ratio.ToString() != "0")
                {
                    return cm;
                }
                else
                {
                    CurrencyModel first = new CurrencyModel();
                    CurrencyModel second = new CurrencyModel();
                    first = first.Get(from);
                    second = second.Get(to);
                    string msg = "Please add a currency conversion from " + first.Name + " to " + second.Name;
                    throw new Exception(msg);
                }
            }
        }
        #endregion

        public decimal Convert(decimal number, int from, int to)
        {
            if (from != to)
            {
                ConversionModel cm = GetMeByFromTo(from, to);
                if (cm.First != from && cm.Second != to)
                    Ratio = 1 / cm.Ratio;
                else
                    Ratio = cm.Ratio;
                number *= (decimal)Ratio;
                return number;
            }
            return number;
        }
    }
}
