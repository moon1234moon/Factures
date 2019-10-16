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
    public class EditSeasonViewModel : Conductor<object>
    {
        private string _year;
        private SeasonModel _season;

        public EditSeasonViewModel(SeasonModel sm)
        {
            Year = sm.Year;
            Season = sm;
        }

        #region
        public SeasonModel Season
        {
            get { return _season; }
            set
            {
                _season = value;
                NotifyOfPropertyChange(() => Season);
            }
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
        #endregion

        public void Update()
        {
            if (Year != string.Empty && Year != " ")
            {
                Season.Year = Year;
                Season = Season.UpdateThis();
                MessageBox.Show("Season Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Season Empty", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
