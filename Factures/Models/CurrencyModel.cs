using Caliburn.Micro;
using Factures.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.Models
{
    public class CurrencyModel : Model
    {
        private int _id;
        private string _name;
        private string _symbol;

        public CurrencyModel()
        {
            this.Table = "currencies";
            this.Fillable = new string[2];
            this.Fillable[0] = "name";
            this.Fillable[1] = "symbol";
        }

        #region
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        public BindableCollection<CurrencyModel> GiveCollection(DataTable dt)
        {
            List<CurrencyModel> output = new List<CurrencyModel>();
            foreach (DataRow row in dt.Rows)
            {
                CurrencyModel cr = new CurrencyModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Name = row[1].ToString(),
                    Symbol = row[2].ToString(),
                };

                output.Add(cr);
            }
            return new BindableCollection<CurrencyModel>(output);
        }

        public CurrencyModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Name = row[1].ToString();
                this.Symbol = row[2].ToString();
            }
            return this;
        }

    }
}
