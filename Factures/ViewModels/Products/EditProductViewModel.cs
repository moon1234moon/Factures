using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Factures.Models;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;

namespace Factures.ViewModels
{
    public class EditProductViewModel : Conductor<object>
    {
        #region
        private ProductModel _product;
        private int _id;
        private string _name;
        private string _details;
        private decimal? _price;
        private BitmapImage _img;
        private CustomerModel _customer;
        private CurrencyModel _currency;
        private BindableCollection<CurrencyModel> _currencies;
        private BindableCollection<CustomerModel> _customers;
        private int _selected_customer;
        private int _selected_currency;
        private string _image;
        private List<string[]> _image_list;
        private bool _is_customer_enabled;
        #endregion

        public EditProductViewModel(ProductModel product, bool contsant_customer = false)
        {
            // Get Currencies
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
            // Get Customers
            CustomerModel customers = new CustomerModel();
            DataTable CustomersTable = customers.All();
            Customers = customers.GiveCollection(CustomersTable);
            // Fill Data
            Product = product;
            Product.Changed = new List<string>();
            Id = product.Id;
            Name = product.Name;
            Customer = customers.Get((int)Product.Customer);
            Details = product.Details;
            if (product.Price != null && Product.Currency != null)
            {
                Price = product.Price;
                Currency = currency.Get((int)Product.Currency);
            }
            // Get Images
            Image = Product.GetImageFromDb();
            Product.Changed = new List<string>();
            FillSelected(product);
            if (contsant_customer)
                IsCustomerEnabled = false;
            else
                IsCustomerEnabled = true;
        }

        #region
        public bool IsCustomerEnabled
        {
            get { return _is_customer_enabled; }
            set
            {
                _is_customer_enabled = value;
                NotifyOfPropertyChange(() => IsCustomerEnabled);
            }
        }

        public ProductModel Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public string IImage
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => IImage);
            }
        }

        public List<string[]> ImageList
        {
            get { return _image_list; }
            set
            {
                _image_list = value;
                Product.Images = _image_list;
                string change = "Image";
                if(!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => ImageList);
            }
        }

        public int SelectedCustomer
        {
            get { return _selected_customer; }
            set
            {
                _selected_customer = value;
                NotifyOfPropertyChange(() => SelectedCustomer);
            }
        }

        public int SelectedCurrency
        {
            get { return _selected_currency; }
            set
            {
                _selected_currency = value;
                NotifyOfPropertyChange(() => SelectedCurrency);
            }
        }

        public BitmapImage Image
        {
            get { return _img; }
            set
            {
                _img = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public CurrencyModel Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
                Product.Currency = Currency.Id;
                string change = "Currency";
                if (!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => Currency);
            }
        }

        public BindableCollection<CustomerModel> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                NotifyOfPropertyChange(() => Customers);
            }
        }


        public BindableCollection<CurrencyModel> Currencies
        {
            get { return _currencies; }
            set
            {
                _currencies = value;
                NotifyOfPropertyChange(() => Currencies);
            }
        }

        public CustomerModel Customer
        {
            get { return _customer; }
            set
            {
                _customer = value;
                Product.Customer = _customer.Id;
                string change = "Customer";
                if (!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => Customer);
            }
        }

        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                string change = "Price";
                Product.Price = _price;
                if (!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => Price);
            }
        }


        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                Product.Details = _details;
                string change = "Details";
                if (!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => Details);
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                Product.Name = _name;
                string change = "Name";
                if (!Product.CheckAChange(change))
                    Product.Changed.Add(change);
                NotifyOfPropertyChange(() => Name);
            }
        }


        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => Id);
            }
        }

        #endregion

        #region
        public void Picture()
        {
            if (Customer != null)
            {
                System.Windows.Forms.OpenFileDialog op = new System.Windows.Forms.OpenFileDialog
                {
                    Title = "Select Image",
                    Filter = "2All supported graphics|*.jpg;*.jpeg;*.png|" +
                            "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                            "Portable Network Graphic (*.png)|*.png",
                    Multiselect = true
                };
                if (System.Convert.ToBoolean(op.ShowDialog()) == true)
                {
                    // Create a PictureBox.
                    try
                    {
                        string[] NewImage;
                        List<string[]> images = new List<string[]>();
                        foreach (string file in op.FileNames)
                        {
                            string contentType = "";
                            ImageFormat imgFormat = ImageFormat.Jpeg;
                            //Set the contenttype based on File Extension
                            switch (Path.GetExtension(file))
                            {
                                case ".jpg":
                                    contentType = "image/jpeg";
                                    break;
                                case ".png":
                                    contentType = "image/png";
                                    imgFormat = ImageFormat.Png;
                                    break;
                                case ".gif":
                                    contentType = "image/gif";
                                    imgFormat = ImageFormat.Gif;
                                    break;
                                case ".bmp":
                                    contentType = "image/bmp";
                                    imgFormat = ImageFormat.Bmp;
                                    break;
                            }

                            /**
                             * Adding Parameters to Image List
                             * [Name, Type, Image as Data]
                             * */
                            NewImage = new string[3];
                            NewImage[0] = Name;
                            NewImage[1] = contentType;
                            Image myImage = Bitmap.FromFile(op.FileName);
                            NewImage[2] = ImageToBase64(myImage, imgFormat);
                            Image = Product.GetImageAsBitmapImage(NewImage);
                            images.Add(NewImage);
                        }
                        ImageList = images;
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image. You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            }
            else
            {
                string chk = "Customer is required before setting and image";
               MessageBox.Show(chk, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        #endregion

        public void PictureDelete()
        {
            Image = new BitmapImage();
            ImageList = new List<string[]>();
        }

        public void Update()
        {
            if (this.CheckInput())
            {
                Product = Product.UpdateThis();
                MessageBox.Show("Product Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool CheckInput()
        {
            if (Name == string.Empty || Name == null)
            {
                string chk = "Name is required";
                System.Windows.MessageBox.Show(chk, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Customer == null)
            {
                string chk = "Customer is required";
                System.Windows.MessageBox.Show(chk, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Price != 0 && Price != null)
            {
                if (Currency == null)
                {
                    string chk = "Can't set a price and not set a currency";
                    System.Windows.MessageBox.Show(chk, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

        private void FillSelected(ProductModel product)
        {
            CustomerModel c = new CustomerModel();
            DataTable dt = Customer.All();
            c = c.Get(System.Convert.ToInt32(product.Customer));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string RowID = dt.Rows[i][0].ToString();
                string CustomerId = Customer.Id.ToString();
                if (RowID == CustomerId)
                {
                    SelectedCustomer = i;
                    break;
                }
            }
            if (Price != 0 && Price != null)
            {
                CurrencyModel cm = new CurrencyModel();
                DataTable cts = cm.All();
                cm = cm.Get(System.Convert.ToInt32(product.Currency));
                for (int i = 0; i < cts.Rows.Count; i++)
                {
                    string RowID = cts.Rows[i][0].ToString();
                    string CurrencyId = Currency.Id.ToString();
                    if (RowID == CurrencyId)
                    {
                        SelectedCurrency = i;
                        break;
                    }
                }
            }
        }
    }
}
