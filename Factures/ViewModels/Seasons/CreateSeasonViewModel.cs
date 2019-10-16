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
    public class CreateSeasonViewModel : Conductor<object>
    {
        private string _year;

        public CreateSeasonViewModel()
        {

        }

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                NotifyOfPropertyChange(() => Year);
            }
        }

        public void Save()
        {
            if(Year != string.Empty && Year != " ")
            {
                SeasonModel NewSeasonModel = new SeasonModel()
                {
                    Year = this.Year,
                };
                NewSeasonModel = NewSeasonModel.SaveThis();
                MessageBox.Show("Season Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Season Empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
