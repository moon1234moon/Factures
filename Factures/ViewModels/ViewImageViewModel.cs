using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Factures.ViewModels
{
    public class ViewImageViewModel : Conductor<object>
    {
        #region
        private const string P = "Product";
        private object[] _data = new object[2];
        private BitmapImage _product_image;
        private string _title;
        #endregion

        public ViewImageViewModel(object[] data)
        {
            Data = data;
            ViewImage();
        }

        #region
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public object[] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public BitmapImage Image
        {
            get { return _product_image; }
            set
            {
                _product_image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }
        #endregion

        #region
        public void ViewImage()
        {
            switch(Data[0])
            {
                case P:
                    ProductModel product = (ProductModel)Data[1];
                    ViewProduct(product);
                    break;
                default:
                    break;
            }
        }

        public void ViewProduct(ProductModel product)
        {
            BitmapImage image = product.GetImageFromDb();
            if (image != null)
            {
                Image = image;
                Title = "Product " + product.Id + ": " + product.Name;
            }
            else
            {
                Image = new BitmapImage();
                MessageBox.Show("There is no image set for product " + product.Id, "No Image", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion
    }
}
