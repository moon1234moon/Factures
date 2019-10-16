using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.ViewModels
{
    public class CreateSizeViewModel : Conductor<object>
    {
        private string _size;

        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }

        public void Save()
        {
            if (Size != null && Size != string.Empty)
            {
                SizeModel sm = new SizeModel()
                {
                    Size = this.Size,
                };
                sm = sm.SaveThis();
                MessageBox.Show("Size Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Empty Size", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
