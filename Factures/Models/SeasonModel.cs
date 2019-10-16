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
    public class SeasonModel : Model
    {
        private int _id;
        private string _year;

        public SeasonModel()
        {
            this.Table = "seasons";
            this.Fillable = new string[1];
            this.Fillable[0] = "years";
        }

        #region
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region
        public BindableCollection<SeasonModel> GiveCollection(DataTable dt)
        {
            List<SeasonModel> output = new List<SeasonModel>();
            foreach (DataRow row in dt.Rows)
            {
                SeasonModel cr = new SeasonModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Year = row[1].ToString(),
                };

                output.Add(cr);
            }
            return new BindableCollection<SeasonModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "", Year };
            Data.Add(new KeyValuePair<string, string[]>("years", value));
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

        public SeasonModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Year = row[1].ToString();
            }
            return this;
        }
        #endregion

        public SeasonModel SaveThis()
        {
            //Validation
            if (Year == string.Empty)
                return null;
            //Save
            this.Save(this.FillMe());
            return this;
        }

        public SeasonModel UpdateThis()
        {
            if (Year == string.Empty)
                return null;
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }
    }
}
