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
    public class FactureModel : Model
    {
        #region
        private int _id;
        private string _name;
        private int _season;
        private int _customer_id;
        private bool _cleared;
        private int _number;
        private DateTime _delivery_date;
        private string _customer_name;
        private string _season_year;
        private string _name_number;
        #endregion

        public FactureModel()
        {
            this.Table = "factures";
            this.Fillable = new string[6];
            this.Fillable[0] = "number";
            this.Fillable[1] = "name";
            this.Fillable[2] = "customer_id";
            this.Fillable[3] = "delivery_date";
            this.Fillable[4] = "cleared";
            this.Fillable[5] = "season_id";
        }

        #region
        public string NameNumber
        {
            get { return _name_number; }
            set { _name_number = value; }
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

        public int Season
        {
            get { return _season; }
            set { _season = value; }
        }

        public DateTime Delivery
        {
            get { return _delivery_date; }
            set { _delivery_date = value; }
        }


        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public bool Cleared
        {
            get { return _cleared; }
            set { _cleared = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public int Customer
        {
            get { return _customer_id; }
            set { _customer_id = value; }
        }


        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region
        public BindableCollection<FactureModel> GiveCollection(DataTable cs)
        {
            List<FactureModel> output = new List<FactureModel>();
            SeasonModel sm = new SeasonModel();
            CustomerModel cm = new CustomerModel();
            foreach (DataRow row in cs.Rows)
            {
                cm = cm.Get(System.Convert.ToInt32(row[4].ToString()));
                FactureModel ctm = new FactureModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Number = System.Convert.ToInt32(row[1].ToString()),
                    Customer = cm.Id,
                    CustomerName = cm.Name,
                    Cleared = System.Convert.ToBoolean(row[6].ToString()),
                };
                if (row[3] != null && row[3].ToString() != "")
                {
                    sm = sm.Get(System.Convert.ToInt32(row[3].ToString()));
                    ctm.Season = sm.Id;
                    ctm.SeasonYear = sm.Year;
                }
                if (row[2].ToString() != null && row[2].ToString() != string.Empty)
                    ctm.Name = row[2].ToString();
                ctm.NameNumber = ctm.Name + " - " + ctm.Number;
                if (row[5].ToString() != null && row[5].ToString() != string.Empty)
                    ctm.Delivery = System.Convert.ToDateTime(row[5].ToString());
                output.Add(ctm);
            }
            return new BindableCollection<FactureModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "", Name };
            Data.Add(new KeyValuePair<string, string[]>("name", value));
            value = new string[2] { "number", Number.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("number", value));
            value = new string[2] { "number", Customer.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("customer_id", value));
            if (Season.ToString() != "0")
            {
                value = new string[2] { "number", Season.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("season_id", value));
            }
            if (Cleared == true)
                value = new string[2] { "number", "1" };
            else
                value = new string[2] { "number", "0" };
            Data.Add(new KeyValuePair<string, string[]>("cleared", value));
            value = new string[2] { "", Delivery.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("delivery_date", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> FillCleared()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value;
            if (Cleared == true)
                value = new string[2] { "number", "1" };
            else
                value = new string[2] { "number", "0" };
            Data.Add(new KeyValuePair<string, string[]>("cleared", value));
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

        public FactureModel GetMeByNumber(int number, int? customer = null)
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = number.ToString();
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("number", value)
            };
            if(customer != null)
            {
                value = new string[2];
                value[0] = "number";
                value[1] = customer.ToString();
                conditions.Add(new KeyValuePair<string, string[]>("customer_id", value));
            }
            DataTable dt = this.FindByParameters(conditions);
            if (dt.Rows.Count > 0)
                return this.ReturnMeFromDataTable(dt);
            else
                return null;
        }

        public FactureModel Get(int id)
        {
            DataTable dt = this.Find(id);
            return this.ReturnMeFromDataTable(dt);
        }

        public FactureModel ReturnMeFromDataTable(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Number = System.Convert.ToInt32(row[1].ToString());
                this.Name = row[2].ToString();
                if (row[3] != null && row[3].ToString() != "")
                    this.Season = System.Convert.ToInt32(row[3].ToString());
                this.Customer = System.Convert.ToInt32(row[4].ToString());
                this.Delivery = System.Convert.ToDateTime(row[5].ToString());
                this.Cleared = System.Convert.ToBoolean(row[6].ToString());
            }
            return this;
        }

        public List<FactureDetailsModel> GetFactureDetailsList()
        {
            FactureDetailsModel facture_details = new FactureDetailsModel();
            return facture_details.GetFactureDetailsList(Id);
        }
        #endregion

        #region
        public void ClearUnclear(bool NewClear)
        {
            Cleared = NewClear;
            this.Update(this.FillCleared(), this.Primaries());
        }

        public FactureModel SaveThis()
        {
            //Save
            DataTable dt = this.Save(this.FillMe());
            Id = System.Convert.ToInt32(dt.Rows[0][0].ToString());
            return this;
        }

        public FactureModel UpdateThis()
        {
            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public void Delete(List<FactureDetailsModel> FactureDetails)
        {
            if(FactureDetails.Count > 0)
            {
                foreach(FactureDetailsModel fd in FactureDetails)
                {
                    fd.Delete();
                }
            }
            this.DeleteThis(this.Primaries());
;        }
        #endregion

        public float? TotalAmount(int currency)
        {
            float? total = 0;
            List<FactureDetailsModel> Details = GetFactureDetailsList();
            foreach(FactureDetailsModel fdm in Details)
            {
                total += fdm.GetTotalFloat(currency);
                if(total == null)
                {
                    return null;
                }
            }
            return total;
        }
    }
}
