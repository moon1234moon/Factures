using Factures.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.Models
{
    public class ProductImageModel : Model
    {
        private int _id;
        private int? _image_id;
        private int? _product_id;

        public ProductImageModel()
        {
            this.Table = "product_image";
            this.Fillable = new string[2];
            this.Fillable[0] = "image_id";
            this.Fillable[1] = "product_id";
        }

        #region
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? Product
        {
            get { return _product_id; }
            set { _product_id = value; }
        }


        public int? Image
        {
            get { return _image_id; }
            set { _image_id = value; }
        }
        #endregion

        #region
        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            // Important
            if (Product == null || Image == null)
                return null;
            string[] value = new string[2] { "number", Image.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("image_id", value));
            value = new string[2] { "number", Product.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("product_id", value));
            return Data;
        }

        private List<KeyValuePair<string, string[]>> Primaries()
        {
            string[] value = new string[2];
            value[0] = "number";
            value[1] = Image.ToString();
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>
            {
                new KeyValuePair<string, string[]>("image_id", value),
            };
            value[0] = "number";
            value[1] = Product.ToString();
            Data.Add(new KeyValuePair<string, string[]>("product_id", value));
            return Data;
        }
        #endregion

        #region
        public ProductImageModel FillMeByImageId(int id)
        {
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", id.ToString() };
            conditions.Add(new KeyValuePair<string, string[]>("image_id", value));
            DataTable dt = this.FindByParameters(conditions);
            foreach(DataRow row in dt.Rows)
            {
                Id = System.Convert.ToInt32(row[0].ToString().Trim());
                Image = System.Convert.ToInt32(row[1].ToString().Trim());
                Product = System.Convert.ToInt32(row[2].ToString().Trim());
            }
            return this;
        }

        public List<ImageModel> ImagesOfProduct(int ProductId)
        {
            List<ImageModel> ImagesGot = new List<ImageModel>();
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", ProductId.ToString() };
            conditions.Add(new KeyValuePair<string, string[]>("product_id", value));
            DataTable result = this.FindByParameters(conditions);
            foreach(DataRow row in result.Rows)
            {
                ImageModel AnImage = new ImageModel()
                {
                    Id = System.Convert.ToInt32(row[1].ToString())
                };
                AnImage.FillMeById();
                ImagesGot.Add(AnImage);
            }
            return ImagesGot;
        }
        #endregion

        #region
        public ProductImageModel SaveThis()
        {
            if (Product == null || Image == null)
                return null;
            DataTable dt = this.Save(this.FillMe());
            return this;
        }

        public bool Delete()
        {
            return this.DeleteThis(this.Primaries());
        }
        #endregion
    }
}
