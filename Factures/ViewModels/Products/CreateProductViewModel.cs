using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Factures.ViewModels
{
    public class CreateProductViewModel : Conductor<object>
    {
        #region
        private string _name;
        private string _details;
        private decimal? _price;
        private string _image;
        private List<string[]> _image_list;
        private CustomerModel _customer;
        private CurrencyModel _currency;
        private BindableCollection<CurrencyModel> _currencies;
        private BindableCollection<CustomerModel> _customers;
        private bool _is_enabled;
        #endregion

        public CreateProductViewModel(ProductModel product = null)
        {
            // Get Currencies
            CurrencyModel currency = new CurrencyModel();
            Currencies = currency.GiveCollection(currency.All());
            // Get Customers
            CustomerModel customers = new CustomerModel();
            DataTable CustomersTable = customers.All();
            Customers = customers.GiveCollection(CustomersTable);
            if (product != null)
            {
                for (int i = 0; i < Customers.Count; i++)
                {
                    int id1 = Customers[i].Id;
                    int id2 = (int)product.Customer;
                    if (id1 == id2)
                    {
                        Customer = Customers[i];
                        break;
                    }
                }
                IsEnabled = false;
            }
            else
                IsEnabled = true;
        }

        #region
        public bool IsEnabled
        {
            get { return _is_enabled; }
            set
            {
                _is_enabled = value;
                NotifyOfPropertyChange(() => IsEnabled);
            }
        }

        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        public CurrencyModel Currency
        {
            get { return _currency; }
            set
            {
                _currency = value;
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
                NotifyOfPropertyChange(() => Customer);
            }
        }

        public List<string[]> ImageList
        {
            get { return _image_list; }
            set
            {
                _image_list = value;
                NotifyOfPropertyChange(() => ImageList);
            }
        }


        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }


        public string Details
        {
            get { return _details; }
            set
            {
                _details = value;
                NotifyOfPropertyChange(() => Details);
            }
        }


        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
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
                            Image = file;
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

        public void PictureDelete()
        {
            Image = "";
            ImageList = new List<string[]>();
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

        public void Save()
        {
            if (this.CheckInput())
            {
                ProductModel product = new ProductModel()
                {
                    Name = Name,
                    Customer = Customer.Id
                };
                if (Details != string.Empty && Details != null)
                    product.Details = Details;
                if (Price != 0 && Price != null)
                {
                    product.Price = Price;
                    product.Currency = Currency.Id;
                }
                if (Image != string.Empty && Image != null)
                {
                    product.Images = ImageList;
                }
                product = product.SaveThis();
                MessageBox.Show("Product Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                if(Currency == null)
                {
                    string chk = "Can't set a price and not set a currency";
                    System.Windows.MessageBox.Show(chk, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

    }
}
