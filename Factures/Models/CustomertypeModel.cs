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
    public class CustomertypeModel : Model
    {
        private int _id;
        private string _name;

        public CustomertypeModel()
        {
            this.Table = "customers_types";
            this.Fillable = new string[1];
            this.Fillable[0] = "name";
        }

        #region
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

        #region
        private List<KeyValuePair<string, string[]>> FillMe()
        {
            string[] value = new string[2];
            value[0] = "";
            value[1] = Name;
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("name", value)
            };
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

        public CustomertypeModel ReturnMeFromDataTable(DataTable dt)
        {
            CustomertypeModel cm = new CustomertypeModel();
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Name = row[1].ToString();
            }
            return this;
        }

        public CustomertypeModel ReturnMeFromDataRow(DataRow dr)
        {
            CustomertypeModel cm = new CustomertypeModel
            {
                Id = System.Convert.ToInt32(dr[0].ToString()),
                Name = dr[1].ToString()
            };
            return cm;
        }
        #endregion

        #region
        public CustomertypeModel SaveThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            this.Save(this.FillMe());
            return this;
        }

        public CustomertypeModel UpdateThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public bool Delete()
        {
            return this.DeleteThis(this.Primaries());
        }
        #endregion

        #region
        public CustomertypeModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach(DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Name = row[1].ToString();
            }
            return this;
        }

        public BindableCollection<CustomertypeModel> GetCollection(DataTable types)
        {
            List<CustomertypeModel> output = new List<CustomertypeModel>();
            foreach (DataRow row in types.Rows)
            {
                CustomertypeModel fm = new CustomertypeModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Name = row[1].ToString(),
                };

                output.Add(fm);
            }
            return new BindableCollection<CustomertypeModel>(output);
        }

        public static string GetTypeName(int id)
        {
            CustomertypeModel ct = new CustomertypeModel();
            ct = ct.Get(id);
            return ct.Name;
        }
        #endregion

        #region
        public List<CustomertypeModel> GetListOfNameSimilarTo(string name)
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value;
            value = new string[2] { "", name };
            Data.Add(new KeyValuePair<string, string[]>("name", value));
            List<CustomertypeModel> list = new List<CustomertypeModel>();
            DataTable dt = this.FindByLikeParameters(Data);
            foreach(DataRow row in dt.Rows)
            {
                list.Add(this.ReturnMeFromDataRow(row));
            }
            return list;
        }
        #endregion

    }
}
