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
    public class CustomerModel : Model
    {
        private int _id;
        private string _name;
        private int _type;
        private string _phone;
        private string _email;
        private string _type_name;

        public CustomerModel()
        {
            this.Table = "customers";
            this.Fillable = new string[4];
            this.Fillable[0] = "name";
            this.Fillable[1] = "type_id";
            this.Fillable[2] = "phone_nb";
            this.Fillable[3] = "email";
        }

        #region
        public string TypeName
        {
            get { return _type_name; }
            set { _type_name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public int Type
        {
            get { return _type; }
            set { _type = value; }
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

        #region
        public BindableCollection<CustomerModel> GiveCollection(DataTable cs)
        {
            List<CustomerModel> output = new List<CustomerModel>();
            foreach (DataRow row in cs.Rows)
            {
                CustomerModel ctm = GetMeFromRow(row);
                output.Add(ctm);
            }
            return new BindableCollection<CustomerModel>(output);
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "", Name };
            Data.Add(new KeyValuePair<string, string[]>("name", value));
            value = new string[2] { "number", Type.ToString()};
            Data.Add(new KeyValuePair<string, string[]>("type_id", value));
            if(Phone != String.Empty && Phone != null)
            {
                value = new string[2] { "", Phone };
                Data.Add(new KeyValuePair<string, string[]>("phone_nb", value));
            }
            if (Email != String.Empty && Email != null)
            {
                value = new string[2] { "", Email };
                Data.Add(new KeyValuePair<string, string[]>("email", value));
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

        private CustomerModel GetMeFromRow(DataRow row)
        {
            CustomerModel ctm = new CustomerModel
            {
                Id = System.Convert.ToInt32(row[0].ToString()),
                Name = row[1].ToString(),
                Type = System.Convert.ToInt32(row[2].ToString()),
                TypeName = CustomertypeModel.GetTypeName(System.Convert.ToInt32(row[2])),
                Phone = row[3].ToString(),
                Email = row[4].ToString(),
            };
            return ctm;
        }
        #endregion

        #region
        public CustomerModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Name = row[1].ToString();
                this.Type = System.Convert.ToInt32(row[2].ToString());
                this.Phone = row[3].ToString();
                this.Email = row[4].ToString();
            }
            return this;
        }

        public CustomerModel SaveThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            this.Save(this.FillMe());
            return this;
        }

        public CustomerModel UpdateThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            this.Update(this.FillMe(), this.Primaries());
            return this;
        }
        #endregion

        #region
        public BindableCollection<FactureModel> GetMyFactures(int? season = null)
        {
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            if (this.Id == 0)
                return null;
            else
            {
                FactureModel fm = new FactureModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                if(season != null)
                {
                    if (season != 0)
                        value = new string[2] { "number", season.ToString() };
                    else
                        value = new string[2] { "number", "NULL" };
                    Data.Add(new KeyValuePair<string, string[]>("season_id", value));
                }
                factures = fm.GiveCollection(fm.FindByParameters(Data));
            }
            return factures;
        }

        public BindableCollection<FactureModel> GetMyClearedFactures(int? season = null)
        {
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            if (this.Id == 0)
                return null;
            else
            {
                FactureModel fm = new FactureModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                value = new string[2] { "number", "1" };
                Data.Add(new KeyValuePair<string, string[]>("cleared", value));
                if (season != null)
                {
                    if (season != 0)
                        value = new string[2] { "number", season.ToString() };
                    else
                        value = new string[2] { "number", "NULL" };
                    Data.Add(new KeyValuePair<string, string[]>("season_id", value));
                }
                factures = fm.GiveCollection(fm.FindByParameters(Data));
            }
            return factures;
        }

        public BindableCollection<FactureModel> GetMyUnClearedFactures(int? season = null)
        {
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            if (this.Id == 0)
                return null;
            else
            {
                FactureModel fm = new FactureModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                value = new string[2] { "number", "0" };
                Data.Add(new KeyValuePair<string, string[]>("cleared", value));
                if (season != null)
                {
                    if (season != 0)
                        value = new string[2] { "number", season.ToString() };
                    else
                        value = new string[2] { "number", "NULL" };
                    Data.Add(new KeyValuePair<string, string[]>("season_id", value));
                }
                factures = fm.GiveCollection(fm.FindByParameters(Data));
            }
            return factures;
        }

        public BindableCollection<FactureModel> GetMyUnClearedFacturesWithOldCleared(int id)
        {
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            if (this.Id == 0)
                return null;
            else
            {
                FactureModel fm = new FactureModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                value = new string[2] { "number", "0" };
                Data.Add(new KeyValuePair<string, string[]>("cleared", value));
                List<KeyValuePair<string, string[]>> Union = new List<KeyValuePair<string, string[]>>();
                value = new string[2] { "number", id.ToString() };
                Union.Add(new KeyValuePair<string, string[]>("id", value));
                DataTable full_table = fm.FindByParameters(Data, Union);
                factures = fm.GiveCollection(full_table);
            }
            return factures;
        }
        #endregion

        #region
        public BindableCollection<ProductModel> GetMyProducts()
        {
            BindableCollection<ProductModel> products = new BindableCollection<ProductModel>();
            if (this.Id == 0)
                return null;
            else
            {
                ProductModel product = new ProductModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                products = product.GiveCollection(product.FindByParameters(Data));
            }
            return products;
        }
        #endregion

        #region
        public BindableCollection<ReceiptModel> GetMyReceipts(int? season = null)
        {
            BindableCollection<ReceiptModel> receipts = new BindableCollection<ReceiptModel>();
            if (this.Id == 0)
                return null;
            else
            {
                ReceiptModel receipt = new ReceiptModel();
                string[] value = new string[2] { "number", Id.ToString() };
                List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
                {
                    new KeyValuePair<string, string[]>("customer_id", value)
                };
                if (season != null)
                {
                    if (season != 0)
                        value = new string[2] { "number", season.ToString() };
                    else
                        value = new string[2] { "number", "NULL" };
                    Data.Add(new KeyValuePair<string, string[]>("season_id", value));
                }
                receipts = receipt.GiveCollection(receipt.FindByParameters(Data));
            }
            return receipts;
        }
        #endregion

        #region
        public BindableCollection<CustomerModel> SearchBy(string search, string name)
        {
            BindableCollection<CustomerModel> customers = new BindableCollection<CustomerModel>();
            CustomerModel customer = new CustomerModel();
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value;
            switch (search)
            {
                case "Name":
                    value = new string[2] { "", name };
                    Data.Add(new KeyValuePair<string, string[]>("name", value));
                    break;
                case "Customer Type Name":
                    CustomertypeModel customer_types = new CustomertypeModel();
                    List<CustomertypeModel> types = new List<CustomertypeModel>();
                    types = customer_types.GetListOfNameSimilarTo(name);
                    List<CustomerModel> css = new List<CustomerModel>();
                    foreach(CustomertypeModel type in types)
                    {
                        List<CustomerModel> NewCustomers = new List<CustomerModel>();
                        NewCustomers = GetCustomersOfType(type.Id);
                        foreach (CustomerModel c in NewCustomers)
                            css.Add(c);
                    }
                    return new BindableCollection<CustomerModel>(css);
                case "Customer Type Id":
                    value = new string[2] { "number", name };
                    Data.Add(new KeyValuePair<string, string[]>("type_id", value));
                    break;
                default:
                    return null;
            }
            customers = customer.GiveCollection(customer.FindByLikeParameters(Data));
            return customers;
        }

        public List<CustomerModel> GetCustomersOfType(int type)
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            string[] value;
            value = new string[2] { "number", type.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("type_id", value));
            DataTable dt = this.FindByParameters(Data);
            List<CustomerModel> list = new List<CustomerModel>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(GetMeFromRow(row));
            }
            return list;
        }
        #endregion
    }
}
