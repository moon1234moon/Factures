using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.ViewModels
{
    public class SeasonViewModel : Conductor<object>
    {
        private BindableCollection<SeasonModel> _seasons;

        public SeasonViewModel()
        {
            SeasonModel sm = new SeasonModel();
            DataTable seasons = sm.All();
            Seasons = sm.GiveCollection(seasons);
        }

        public void CreateNew()
        {
            ActivateItem(new CreateSeasonViewModel());
        }

        public void Edit(SeasonModel sm)
        {
            ActivateItem(new EditSeasonViewModel(sm));
        }

        public BindableCollection<SeasonModel> Seasons
        {
            get { return _seasons; }
            set
            {
                _seasons = value;
                NotifyOfPropertyChange(() => Seasons);
            }
        }
    }
}
