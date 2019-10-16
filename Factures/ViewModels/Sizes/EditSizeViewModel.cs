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
    public class EditSizeViewModel : Conductor<object>
    {
        private string _size;
        private SizeModel _size_model;

        public EditSizeViewModel(SizeModel sm)
        {
            this._size_model = sm;
            Size = sm.Size;
        }

        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                NotifyOfPropertyChange(() => Size);
            }
        }

        public void Update()
        {
            if (Size != null && Size != string.Empty)
            {
                this._size_model.Size = this.Size;
                this._size_model = this._size_model.UpdateThis();
                MessageBox.Show("Size Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Empty Size", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
