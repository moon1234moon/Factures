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
    public class FactureDetailsModel : Model
    {
        #region
        private int? _id;
        private int _facture_id;
        private int _product_id;
        private int _size_id;
        private int _currency_id;
        private decimal _unit_price;
        private int _quantity;
        private string _full_price;
        private string _size_symbol;
        private string _product_name;
        private string _total;
        #endregion

        public FactureDetailsModel()
        {
            this.Table = "facture_details";
            this.Fillable = new string[6];
            this.Fillable[0] = "facture_id";
            this.Fillable[1] = "product_id";
            this.Fillable[2] = "size_id";
            this.Fillable[3] = "quantity";
            this.Fillable[4] = "unit_price";
            this.Fillable[5] = "currency_id";
        }

        #region
        public int? Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public string ProductName
        {
            get { return _product_name; }
            set { _product_name = value; }
        }

        public string SizeSymbol
        {
            get { return _size_symbol; }
            set { _size_symbol = value; }
        }

        public string FullPrice
        {
            get { return _full_price; }
            set { _full_price = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }


        public decimal Price
        {
            get { return _unit_price; }
            set { _unit_price = value; }
        }


        public int Currency
        {
            get { return _currency_id; }
            set { _currency_id = value; }
        }


        public int Size
        {
            get { return _size_id; }
            set { _size_id = value; }
        }


        public int Product
        {
            get { return _product_id; }
            set { _product_id = value; }
        }


        public int  Facture
        {
            get { return _facture_id; }
            set { _facture_id = value; }
        }

        public object MesssageBox { get; private set; }
        #endregion

        #region
        public BindableCollection<FactureDetailsModel> GiveCollection(DataTable cs)
        {
            List<FactureDetailsModel> output = new List<FactureDetailsModel>();
            foreach (DataRow row in cs.Rows)
            {
                FactureDetailsModel ctm = new FactureDetailsModel
                {
                    Facture = System.Convert.ToInt32(row[0].ToString()),
                    Product = System.Convert.ToInt32(row[1].ToString()),
                };
                if (row[2].ToString() != null && row[2].ToString() != string.Empty)
                    ctm.Size = System.Convert.ToInt32(row[2].ToString());
                if (row[3].ToString() != null && row[3].ToString() != string.Empty)
                    ctm.Quantity = System.Convert.ToInt32(row[3].ToString());
                if (row[4].ToString() != null && row[4].ToString() != string.Empty)
                    ctm.Price = System.Convert.ToDecimal(row[4].ToString());
                if (row[5].ToString() != null && row[5].ToString() != string.Empty)
                    ctm.Currency = System.Convert.ToInt32(row[5].ToString());
                output.Add(ctm);
            }
            return new BindableCollection<FactureDetailsModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", Facture.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("facture_id", value));
            value = new string[2] { "number", Product.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("product_id", value));
            if (Size != 0)
            {
                value = new string[2] { "number", Size.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("size_id", value));
            }
            value = new string[2] { "number", Quantity.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("quantity", value));
            if (Price != null && Currency != null && Price != 0 && Currency != 0)
            {
                value = new string[2] { "number", Price.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("unit_price", value));
                value = new string[2] { "", Currency.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("currency_id", value));
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

        public FactureDetailsModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Facture = System.Convert.ToInt32(row[1].ToString());
                this.Product = System.Convert.ToInt32(row[2].ToString());
                this.Size = System.Convert.ToInt32(row[3].ToString());
                this.Quantity = System.Convert.ToInt32(row[4].ToString());
                this.Price = System.Convert.ToDecimal(row[5].ToString());
                this.Currency = System.Convert.ToInt32(row[6].ToString());
            }
            return this;
        }

        public List<FactureDetailsModel> GetFactureDetailsList(int facture_id)
        {
            List<FactureDetailsModel> FactureDetailsList = new List<FactureDetailsModel>();
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", facture_id.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("facture_id", value));
            DataTable dt = this.FindByParameters(Data);
            foreach (DataRow row in dt.Rows)
            {
                FactureDetailsModel fm = new FactureDetailsModel()
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Facture = System.Convert.ToInt32(row[1].ToString()),
                    Product = System.Convert.ToInt32(row[2].ToString()),
                };
                ProductModel p = new ProductModel();
                p = p.Get(fm.Product);
                fm.ProductName = p.Name;
                if (row[3].ToString() != string.Empty && System.Convert.ToInt32(row[3].ToString()) != 0)
                {
                    fm.Size = System.Convert.ToInt32(row[3].ToString());
                    SizeModel s = new SizeModel();
                    fm.SizeSymbol = s.Get(fm.Size).Size;
                }
                if (row[4].ToString() != string.Empty && System.Convert.ToInt32(row[4].ToString()) != 0)
                    fm.Quantity = System.Convert.ToInt32(row[4].ToString());
                if (row[5].ToString() != string.Empty && System.Convert.ToDecimal(row[5].ToString()) != 0)
                {
                    fm.Price = System.Convert.ToDecimal(row[5].ToString());
                    if (row[6].ToString() != string.Empty && System.Convert.ToInt32(row[6].ToString()) != 0)
                    {
                        fm.Currency = System.Convert.ToInt32(row[6].ToString());
                        CurrencyModel c = new CurrencyModel();
                        c = c.Get(fm.Currency);
                        fm.FullPrice = fm.Price.ToString() + " " + c.Symbol;
                        fm.Total = (fm.Price * fm.Quantity).ToString() + " " + c.Symbol;
                    }
                }
                FactureDetailsList.Add(fm);
            }
            return FactureDetailsList;
        }
        #endregion

        #region
        public FactureDetailsModel SaveThis()
        {
            //Save
            this.Save(this.FillMe());
            return this;
        }

        public FactureDetailsModel UpdateThis()
        {
            //Validation

            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public void Delete()
        {
            this.DeleteThis(this.Primaries());
        }
        #endregion

        public float? GetTotalFloat(int currency)
        {
            decimal total = Price * Quantity;
            if(currency != Currency)
            {
                try
                {
                    ConversionModel cm = new ConversionModel();
                    total = cm.Convert(total, Currency, currency);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            }
            return (float)total;
        }
    }
}
