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
    public class SizeModel : Model
    {
        private int _id;
        private string _size;

        public SizeModel()
        {
            this.Table = "sizes";
            this.Fillable = new string[1];
            this.Fillable[0] = "size";
        }

        #region
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        #endregion

        #region
        public BindableCollection<SizeModel> GiveCollection(DataTable cs)
        {
            List<SizeModel> output = new List<SizeModel>();
            foreach (DataRow row in cs.Rows)
            {
                SizeModel ctm = new SizeModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Size = row[1].ToString(),
                };
                output.Add(ctm);
            }
            return new BindableCollection<SizeModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "", Size.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("size", value));
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

        public SizeModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Size = row[1].ToString();
            }
            return this;
        }
        #endregion

        #region
        public SizeModel SaveThis()
        {
            //Validation

            //Save
            this.Save(this.FillMe());
            return this;
        }

        public SizeModel UpdateThis()
        {
            //Validation

            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }

        public bool Delete()
        {
            return this.DeleteThis(this.Primaries());
        }
        #endregion
    }
}
