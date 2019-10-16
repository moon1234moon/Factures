using Caliburn.Micro;
using Factures.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Factures.Models
{
    public class ProductModel : Model
    {
        #region
        private int _id;
        private string _name;
        private string _details;
        private decimal? _price;
        private List<string[]> _images;
        private int? _customer;
        private int? _currency;
        private string _currency_symbol;
        private string _customer_name;
        private string _full_price;
        private List<string> _changed;
        #endregion

        public ProductModel()
        {
            this.Table = "products";
            this.Fillable = new string[5];
            this.Fillable[0] = "name";
            this.Fillable[1] = "details";
            this.Fillable[2] = "price";
            this.Fillable[3] = "currency_id";
            this.Fillable[4] = "customer_id";
        }

        #region
        public List<string> Changed
        {
            get { return _changed; }
            set { _changed = value; }
        }

        public string FullPrice
        {
            get { return _full_price; }
            set { _full_price = value; }
        }

        public string CurrencySymbol
        {
            get { return _currency_symbol; }
            set { _currency_symbol = value; }
        }

        public string CustomerName
        {
            get { return _customer_name; }
            set { _customer_name = value; }
        }

        public int? Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public int? Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }


        public List<string[]> Images
        {
            get { return _images; }
            set { _images = value; }
        }


        public decimal? Price
        {
            get { return _price; }
            set { _price = value; }
        }


        public string Details
        {
            get { return _details; }
            set { _details = value; }
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
        public BindableCollection<ProductModel> GiveCollection(DataTable cs)
        {
            List<ProductModel> output = new List<ProductModel>();
            foreach (DataRow row in cs.Rows)
            {
                ProductModel ctm = new ProductModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Name = row[1].ToString(),
                    Customer = System.Convert.ToInt32(row[5].ToString()),
                };
                CustomerModel cm = new CustomerModel();
                cm = cm.Get(System.Convert.ToInt32(ctm.Customer));
                ctm.CustomerName = cm.Name;
                if (row[2] != null)
                {
                    ctm.Details = row[2].ToString();
                }
                try
                {
                    ctm.Price = System.Convert.ToDecimal(row[3].ToString());
                    int currency = System.Convert.ToInt32(row[4].ToString());
                    ctm.Currency = currency;
                    CurrencyModel ct = new CurrencyModel();
                    ct = ct.Get(System.Convert.ToInt32(currency));
                    ctm.CurrencySymbol = ct.Symbol;
                    ctm.FullPrice = ctm.Price.ToString() + " " + ctm.CurrencySymbol;
                }
                catch
                {

                }
                output.Add(ctm);
            }
            return new BindableCollection<ProductModel>(output);
        }

        public BindableCollection<ProductModel> GiveCustomerProducts(int customer)
        {
            List<KeyValuePair<string, string[]>> conditions = new List<KeyValuePair<string, string[]>>();
            string[] value = new string[2] { "number", customer.ToString() };
            conditions.Add(new KeyValuePair<string, string[]>("customer_id", value));
            return GiveCollection(this.FindByParameters(conditions));
        }

        private List<KeyValuePair<string, string[]>> FillMe()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            // Important
            if (Name == null || Customer == null)
                return null;
            string[] value = new string[2] { "", Name };
            Data.Add(new KeyValuePair<string, string[]>("name", value));
            value = new string[2] { "number", Customer.ToString() };
            Data.Add(new KeyValuePair<string, string[]>("customer_id", value));

            // Not Important
            if (Details != string.Empty && Details != null)
            {
                value = new string[2] { "", Details };
                Data.Add(new KeyValuePair<string, string[]>("details", value));
            }
            if (Price != 0 && Price.HasValue)
            {
                value = new string[2] { "number", Price.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("price", value));
                value = new string[2] { "number", Currency.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("currency_id", value));
            }

            return Data;
        }

        private List<KeyValuePair<string, string[]>> FillMeForUpdate()
        {
            List<KeyValuePair<string, string[]>> Data = new List<KeyValuePair<string, string[]>>();
            // Important
            if (Name == null || Customer == null)
                return null;
            string[] value = new string[2] { "", Name };
            if (this.CheckAChange("Name"))
            {
                Data.Add(new KeyValuePair<string, string[]>("name", value));
            }
            if (this.CheckAChange("Customer"))
            {
                value = new string[2] { "number", Customer.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("customer_id", value));
            }

            // Not Important
            if (this.CheckAChange("Details") && Details != string.Empty && Details != null)
            {
                value = new string[2] { "", Details };
                Data.Add(new KeyValuePair<string, string[]>("details", value));
            }
            if (this.CheckAChange("Price") && Price != 0 && Price.HasValue)
            {
                value = new string[2] { "number", Price.ToString() };
                Data.Add(new KeyValuePair<string, string[]>("price", value));
            }
            if (this.CheckAChange("Currency") && Price != 0 && Price.HasValue && Currency != null)
            {
                value = new string[2] { "number", Currency.ToString() };
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

        public ProductModel Get(int id)
        {
            DataTable dt = this.Find(id);
            foreach (DataRow row in dt.Rows)
            {
                this.Id = System.Convert.ToInt32(row[0].ToString());
                this.Name = row[1].ToString();
                if(row[2].ToString() != null)
                    this.Details = row[2].ToString();
                try
                {
                    decimal p = System.Convert.ToDecimal(row[3].ToString());
                    if (p != 0)
                    {
                        this.Price = p;
                        this.Currency = System.Convert.ToInt32(row[4].ToString());
                    }
                }
                catch(Exception e)
                { }
                try
                {
                    int c = System.Convert.ToInt32(row[5].ToString());
                    if (c != 0)
                    {
                        this.Customer = c;
                    }
                }
                catch (Exception e)
                { }
            }
            return this;
        }

        public BitmapImage GetImageFromDb()
        {
            List<ImageModel> ImagesList = new List<ImageModel>();
            ProductImageModel pim = new ProductImageModel();
            ImagesList = pim.ImagesOfProduct(Id);
            if (ImagesList.Count > 0)
            {
                byte[] blob = ImagesList[0].Base64ToByteArr(ImagesList[0].Image);
                MemoryStream stream = new MemoryStream();
                stream.Write(blob, 0, blob.Length);
                stream.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                bi.StreamSource = ms;
                bi.EndInit();
                return bi;
            }
            return null;
        }

        private ImageModel GetImageModelFromDb()
        {
            List<ImageModel> ImagesList = new List<ImageModel>();
            ProductImageModel pim = new ProductImageModel();
            ImagesList = pim.ImagesOfProduct(Id);
            if(ImagesList.Count > 0)
                return ImagesList[0];
            return null;
        }

        public BitmapImage GetImageAsBitmapImage(string[] new_image)
        {
            ImageModel image_given = new ImageModel()
            {
               Name = new_image[0].ToString(),
               Type = new_image[1].ToString(),
               Image = new_image[2].ToString()
            };
            byte[] blob = image_given.Base64ToByteArr(image_given.Image);
            MemoryStream stream = new MemoryStream();
            stream.Write(blob, 0, blob.Length);
            stream.Position = 0;

            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);
            bi.StreamSource = ms;
            bi.EndInit();
            return bi;
        }
        #endregion

        public bool CheckAChange(string change)
        {
            if (Changed == null)
                return false;
            foreach(string s in Changed)
            {
                if (s == change)
                    return true;
            }
            return false;
        }

        #region
        public ProductModel SaveThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            DataTable dt = this.Save(this.FillMe());
            this.Id = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (Images != null || Images[0] == null)
            {
                foreach (string[] im in Images)
                {
                    ImageModel NewImage = new ImageModel()
                    {
                        Name = im[0].ToString(),
                        Type = im[1].ToString(),
                        Image = im[2].ToString()
                    };
                    NewImage = NewImage.SaveThis();
                    ProductImageModel ProductImage = new ProductImageModel()
                    {
                        Product = Id,
                        Image = NewImage.Id
                    };
                    ProductImage = ProductImage.SaveThis();
                }
            }
            return this;
        }

        public ProductModel UpdateThis()
        {
            //Validation
            if (Name == string.Empty)
                return null;
            //Save
            List<KeyValuePair<string, string[]>> filled = this.FillMeForUpdate();
            if (filled != null && filled.Count > 0)
            {
                this.Update(filled, this.Primaries());
            }
            this.UpdateImage();
            Changed = new List<string>();
            return this;
        }

        private void UpdateImage()
        {
            if (this.CheckAChange("Image"))
            {
                // Get Image If exists
                ImageModel MyImage = this.GetImageModelFromDb();
                if (MyImage == null)
                {
                    // We don't have an old Image => create new Image
                    foreach (string[] im in Images)
                    {
                        ImageModel NewImage = new ImageModel()
                        {
                            Name = im[0].ToString(),
                            Type = im[1].ToString(),
                            Image = im[2].ToString()
                        };
                        NewImage = NewImage.SaveThis();
                        ProductImageModel ProductImage = new ProductImageModel()
                        {
                            Product = Id,
                            Image = NewImage.Id
                        };
                        ProductImage = ProductImage.SaveThis();
                    }
                }
                else
                {
                    // We have an old Image
                    if(Images == null || Images.Count == 0)
                    {
                        // Delete old Image and ProductImage row
                        ProductImageModel prod_img = new ProductImageModel();
                        prod_img = prod_img.FillMeByImageId(MyImage.Id);
                        prod_img.Delete();
                        MyImage.Delete();
                    }
                    else
                    {
                        // Update new Image
                        foreach (string[] im in Images)
                        {
                            MyImage.Name = im[0].ToString();
                            MyImage.Type = im[1].ToString();
                            MyImage.Image = im[2].ToString();
                        }
                        MyImage = MyImage.UpdateThis();
                    }
                }
            }
        }
        #endregion

    }
}
