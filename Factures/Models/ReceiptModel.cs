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
    public class ReceiptModel : Model
    {
        #region
        private int? _id;
        private int? _season;
        private int? _customer_id;
        private int? _number;
        private decimal? _amount;
        private int? _currency;
        private DateTime? _delivery_date;
        private string _customer_name;
        private string _season_year;
        private string _currency_symbol;
        private string _details;
        private int? _facture;
        private string _cheque;
        private string _full_amount_string;
        #endregion

        public ReceiptModel()
        {
            this.Table = "receipts";
            this.Fillable = new string[9];
            this.Fillable[0] = "number";
            this.Fillable[1] = "customer_id";
            this.Fillable[2] = "season_id";
            this.Fillable[3] = "details";
            this.Fillable[4] = "amount";
            this.Fillable[5] = "currency_id";
            this.Fillable[6] = "delivery_date";
            this.Fillable[7] = "facture_id";
            this.Fillable[8] = "cheque";
        }

        #region
        public string FullAmount
        {
            get { return _full_amount_string; }
            set { _full_amount_string = value; }
        }

        public string Cheque
        {
            get { return _cheque; }
            set { _cheque = value; }
        }

        public int? Facture
        {
            get { return _facture; }
            set { _facture = value; }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value; }
        }

        public string CurrencySymbol
        {
            get { return _currency_symbol; }
            set { _currency_symbol = value; }
        }

        public int? Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public decimal? Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public string SeasonYear
        {
            get { return _season_year; }
            set { _season_year = value; }
        }

        public string CustomerName
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        public int? Season
        {
            get { return _season; }
            set { _season = value; }
        }

        public DateTime? Delivery
        {
            get { return _delivery_date; }
            set { _delivery_date = value; }
        }

        public int? Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? Customer
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }
        #endregion

        #region
        public BindableCollection<ReceiptModel> GiveCollection(DataTable cs)
        {
            List<ReceiptModel> output = new List<ReceiptModel>();
            SeasonModel sm = new SeasonModel();
            CustomerModel cm = new CustomerModel();
            CurrencyModel cr = new CurrencyModel();
            FactureModel fm = new FactureModel();
            foreach (DataRow row in cs.Rows)
            {
                cm = cm.Get(System.Convert.ToInt32(row[2].ToString()));
                ReceiptModel ctm = new ReceiptModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Number = System.Convert.ToInt32(row[1].ToString()),
                    Customer = cm.Id,
                    CustomerName = cm.Name,
                };
                if (row[3].ToString() != null && row[3].ToString() != string.Empty)
                {
                    sm = sm.Get(System.Convert.ToInt32(row[3].ToString()));
                    ctm.Season = sm.Id;
                    ctm.SeasonYear = sm.Year;
                }
                if (row[4].ToString() != null && row[4].ToString() != string.Empty)
                {
                    ctm.Details = row[4].ToString();
                }
                if (row[5].ToString() != null && row[5].ToString() != string.Empty)
                {
                    ctm.Amount = System.Convert.ToDecimal(row[5].ToString());
                    if (row[6].ToString() != null && row[6].ToString() != string.Empty)
                    {
                        cr = cr.Get(System.Convert.ToInt32(row[6].ToString()));
                        ctm.Currency = cr.Id;
                        ctm.CurrencySymbol = cr.Symbol;
                    }
                }
                if (row[7].ToString() != null && row[7].ToString() != string.Empty)
                {
                    ctm.Delivery = System.Convert.ToDateTime(row[7].ToString());
                }
                if (row[8].ToString() != null && row[8].ToString() != string.Empty)
                {
                    fm = fm.Get(System.Convert.ToInt32(row[8].ToString()));
                    ctm.Facture = fm.Id;
                }
                if (row[9].ToString() != null && row[9].ToString() != string.Empty)
                {
                    ctm.Cheque = row[9].ToString();
                }
                output.Add(ctm);
            }
            return new BindableCollection<ReceiptModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", Number.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("number", value));
            value = new string[2] { "number", Customer.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("customer_id", value));
            if (Season != null && Season.ToString() != "0")
            {
                value = new string[2] { "number", Season.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("season_id", value));
            }
            if (Details != null && Details.ToString().Trim() != string.Empty)
            {
                value = new string[2] { "", Details.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("details", value));
            }
            if (Amount.ToString() != string.Empty && Amount.ToString() != "0")
            {
                value = new string[2] { "number", Amount.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("amount", value));
                if (Currency.ToString() != string.Empty && Currency.ToString() != "0")
                {
                    value = new string[2] { "number", Currency.ToString() };
                    Data.Add(new KeyValuePair<string, string[]>("currency_id", value));
                }
            }
            if (Delivery != null && Delivery.ToString().Trim() != string.Empty && Delivery.ToString() != "0")
            {
                value = new string[2] { "", Delivery.ToString().Trim() };
                Data.Add(new KeyValuePair<string, string[]>("delivery_date", value));
            }
            if (Facture != null && Facture.ToString() != "0")
            {
                value = new string[2] { "number", Facture.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("facture_id", value));
            }
            if (Cheque != null && Cheque.ToString().Trim() != "0")
            {
                value = new string[2] { "", Cheque.ToString().Trim() };
                Data.Add(new KeyValuePair<string, string[]>("cheque", value));
            }
            return Data;
        }

        private List<KeyValuePair<string, string[]>> Primaries()
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = Id.ToString();
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("id", value)
            };
            return Data;
        }

        public ReceiptModel ReturnMeFromDataTable(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Number = System.Convert.ToInt32(row[1].ToString());
                this.Customer = System.Convert.ToInt32(row[2].ToString());
                if (row[3].ToString() != "0" && row[3].ToString() != "")
                {
                    this.Season = System.Convert.ToInt32(row[3].ToString());
                }
                if (row[4].ToString() != "")
                {
                    this.Details = row[4].ToString();
                }
                if (row[5].ToString() != "0" && row[5].ToString() != "")
                {
                    this.Amount = System.Convert.ToDecimal(row[5].ToString());
                    if (row[6].ToString() != "0" && row[6].ToString() != "")
                    {
                        this.Currency = System.Convert.ToInt32(row[6].ToString());
                    }
                }
                if (row[7].ToString() != "")
                {
                    this.Delivery = System.Convert.ToDateTime(row[7].ToString());
                }
                if (row[8].ToString() != "0" && row[8].ToString() != "")
                {
                    this.Facture = System.Convert.ToInt32(row[8].ToString());
                }
                if (row[9].ToString() != "")
                {
                    this.Cheque = row[9].ToString();
                }
            }
            return this;
        }

        public ReceiptModel GetMeByNumber(int number, int? customer = null)
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = number.ToString();
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("number", value)
            };
            if (customer != null)
            {
                value = new string[2];
                value[0] = "number";
                value[1] = customer.ToString();
                conditions.Add(new KeyValuePair<string, string[]>("customer_id", value));
            }
            DataTable dt = this.FindByParameters(conditions);
            return this.ReturnMeFromDataTable(dt);
        }

        public ReceiptModel Get(int id)
        {
            DataTable dt = this.Find(id);
            return this.ReturnMeFromDataTable(dt);
        }
        #endregion

        #region
        public void ClearFacture()
        {
            FactureModel fm = new FactureModel();
            fm = fm.Get((int)Facture);
            fm.ClearUnclear(true);
        }

        public ReceiptModel SaveThis()
        {
            //Save
            DataTable dt = this.Save(this.FillMe());
            Id = System.Convert.ToInt32(dt.Rows[0][0].ToString());
            return this;
        }

        public ReceiptModel UpdateThis()
        {
            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public bool Delete()
        {
            this.DeleteThis(this.Primaries());
            return true;
        }
        #endregion

        #region
        public void SetFullAmount(int currency)
        {
            decimal amount = (decimal)Amount;
            if (currency != Currency)
            {
                try
                {
                    ConversionModel cm = new ConversionModel();
                    amount = cm.Convert((decimal)Amount, (int)Currency, currency);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            CurrencyModel c = new CurrencyModel();
            c = c.Get(currency);
            FullAmount = amount + " " + c.Symbol;
        }
        #endregion

        #region
        public ReceiptModel GetMeByFacture(int facture_id)
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = facture_id.ToString();
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("facture_id", value)
            };
            DataTable dt = this.FindByParameters(conditions);
            if (dt.Rows.Count > 0)
                return this.ReturnMeFromDataTable(dt);
            return null;
        }
        #endregion
    }
}
