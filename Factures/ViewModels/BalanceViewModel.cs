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
    public class BalanceViewModel : Conductor<object>
    {
        #region
        private float _sum_factures_uncleared;
        private float _sum_factures_cleared;
        private float _sum_factures;
        private float _sum_receipts;
        private float _remainder;
        private float _total_factures_no_season;
        private float _total_factures_cleared;
        private float _total_factures;
        private float _total_factures_uncleared;
        private float _total_receipt_season;
        private float _total_receipt_season_no_factures;
        private float _total_receipt_season_with_factures;
        private float _total_receipt_no_season;
        private float _total_receipt_no_season_no_factures;
        private float _total_receipt_no_season_with_factures;
        private CurrencyModel _currency;
        private SeasonModel _season;
        private CustomerModel _customer;
        private List<int?> _facture_ids_with_receipts;
        private BindableCollection<FactureModel> _facture;
        private BindableCollection<ReceiptModel> _receipts;
        private BindableCollection<FactureModel> _factures_list;
        private BindableCollection<FactureModel> _factures_list_cleared;
        private BindableCollection<FactureModel> _factures_list_un_cleared;
        private BindableCollection<FactureModel> _factures_standalone;
        private BindableCollection<ReceiptModel> _receipts_season = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_season_no_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_season_with_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season_no_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season_with_factures = new BindableCollection<ReceiptModel>();
        #endregion

        public BalanceViewModel(CustomerModel customer = null, SeasonModel season = null, CurrencyModel currency = null)
        {
            Customer = customer;
            Season = season;
            Currency = currency;
            SetBalance();
        }

        #region
        public float SumFacturesUncleared
        {
            get { return _sum_factures_uncleared; }
            set
            {
                _sum_factures_uncleared = value;
                NotifyOfPropertyChange(() => SumFacturesUncleared);
            }
        }

        public float SumFacturesCleared
        {
            get { return _sum_factures_cleared; }
            set
            {
                _sum_factures_cleared = value;
                NotifyOfPropertyChange(() => SumFacturesCleared);
            }
        }

        public float SumFactures
        {
            get { return _sum_factures; }
            set
            {
                _sum_factures = value;
                NotifyOfPropertyChange(() => SumFactures);
            }
        }

        public float SumReceipts
        {
            get { return _sum_receipts; }
            set
            {
                _sum_receipts = value;
                NotifyOfPropertyChange(() => SumReceipts);
            }
        }

        public float Remainder
        {
            get { return _remainder; }
            set
            {
                _remainder = value;
                NotifyOfPropertyChange(() => Remainder);
            }    
        }

        public float TotalReceiptSeason
        {
            get { return _total_receipt_season; }
            set
            {
                _total_receipt_season = value;
                NotifyOfPropertyChange(() => TotalReceiptSeason);
            }
        }

        public float TotalReceiptSeasonNoFacture
        {
            get { return _total_receipt_season_no_factures; }
            set
            {
                _total_receipt_season_no_factures = value;
                NotifyOfPropertyChange(() => TotalReceiptSeasonNoFacture);
            }
        }

        public float TotalReceiptSeasonWithFacture
        {
            get { return _total_receipt_season_with_factures; }
            set
            {
                _total_receipt_season_with_factures = value;
                NotifyOfPropertyChange(() => TotalReceiptSeasonWithFacture);
            }
        }

        public float TotalReceiptNoSeason
        {
            get { return _total_receipt_no_season; }
            set
            {
                _total_receipt_no_season = value;
                NotifyOfPropertyChange(() => TotalReceiptNoSeason);
            }
        }

        public float TotalReceiptNoSeasonNoFacture
        {
            get { return _total_receipt_no_season_no_factures; }
            set
            {
                _total_receipt_no_season_no_factures = value;
                NotifyOfPropertyChange(() => TotalReceiptNoSeasonNoFacture);
            }
        }

        public float TotalReceiptNoSeasonWithFacture
        {
            get { return _total_receipt_no_season_with_factures; }
            set
            {
                _total_receipt_no_season_with_factures = value;
                NotifyOfPropertyChange(() => TotalReceiptNoSeasonWithFacture);
            }
        }

        public float TotalFactures
        {
            get { return _total_factures; }
            set
            {
                _total_factures = value;
                NotifyOfPropertyChange(() => TotalFactures);
            }
        }
        
        public float TotalFacturesUnCleared
        {
            get { return _total_factures_uncleared; }
            set
            {
                _total_factures_uncleared = value;
                NotifyOfPropertyChange(() => TotalFacturesUnCleared);
            }
        }

        public float TotalFacturesCleared
        {
            get { return _total_factures_cleared; }
            set
            {
                _total_factures_cleared = value;
                NotifyOfPropertyChange(() => TotalFacturesCleared);
            }
        }

        public float TotalFacturesNoSeason
        {
            get { return _total_factures_no_season; }
            set
            {
                _total_factures_no_season = value;
                NotifyOfPropertyChange(() => TotalFacturesNoSeason);
            }
        }

        public List<int?> FactureIdsWithReceipts
        {
            get { return _facture_ids_with_receipts; }
            set
            {
                _facture_ids_with_receipts = value;
                NotifyOfPropertyChange(() => FactureIdsWithReceipts);
            }
        }

        public BindableCollection<ReceiptModel> ReceiptsNoSeasonsNoFactures
        {
            get { return _receipts_no_season_no_factures; }
            set
            {
                _receipts_no_season_no_factures = value;
                NotifyOfPropertyChange(() => ReceiptsNoSeasonsNoFactures);
            }
        }

        public BindableCollection<ReceiptModel> ReceiptsSeasonsNoFactures
        {
            get { return _receipts_season_no_factures; }
            set
            {
                _receipts_season_no_factures = value;
                NotifyOfPropertyChange(() => ReceiptsSeasonsNoFactures);
            }
        }

        public BindableCollection<ReceiptModel> ReceiptsSeasonsWithFactures
        {
            get { return _receipts_season_with_factures; }
            set
            {
                _receipts_season_with_factures = value;
                NotifyOfPropertyChange(() => ReceiptsSeasonsWithFactures);
            }
        }

        public BindableCollection<ReceiptModel> ReceiptsNoSeasons
        {
            get { return _receipts_no_season; }
            set
            {
                _receipts_no_season = value;
                NotifyOfPropertyChange(() => ReceiptsNoSeasons);
            }
        }

        public BindableCollection<ReceiptModel> ReceiptsSeasons
        {
            get { return _receipts_season; }
            set
            {
                _receipts_season = value;
                NotifyOfPropertyChange(() => ReceiptsSeasons);
            }
        }

        public BindableCollection<ReceiptModel> Receipts
        {
            get { return _receipts; }
            set
            {
                _receipts = value;
                NotifyOfPropertyChange(() => Receipts);
            }
        }

        public BindableCollection<FactureModel> FacturesListUncleared
        {
            get { return _factures_list_un_cleared; }
            set
            {
                _factures_list_un_cleared = value;
                NotifyOfPropertyChange(() => FacturesListUncleared);
            }
        }

        public BindableCollection<FactureModel> FacturesListCleared
        {
            get { return _factures_list_cleared; }
            set
            {
                _factures_list_cleared = value;
                NotifyOfPropertyChange(() => FacturesListCleared);
            }
        }

        public BindableCollection<FactureModel> FacturesSandalone
        {
            get { return _factures_standalone; }
            set
            {
                _factures_standalone = value;
                NotifyOfPropertyChange(() => FacturesSandalone);
            }
        }

        public BindableCollection<FactureModel> FacturesList
        {
            get { return _factures_list; }
            set
            {
                _factures_list = value;
                NotifyOfPropertyChange(() => FacturesList);
            }
        }

        public BindableCollection<FactureModel> Factures
        {
            get { return _facture; }
            set
            {
                _facture = value;
                NotifyOfPropertyChange(() => Factures);
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

        public SeasonModel Season
        {
            get { return _season; }
            set
            {
                _season = value;
                NotifyOfPropertyChange(() => Season);
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
        #endregion

        #region
        public void SetBalance()
        {
            try
            {
                if (Customer != null)
                {
                    SetFactures();
                    SetReceipts();
                    Compute();
                    ShowResult();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ShowResult()
        {
            string message = "The balance for customer " + Customer.Name;
            if(Season != null && Season.Id.ToString() != "0")
            {
                message += " in Season " + Season.Year;
            }
            message += " is complete";
            MessageBox.Show(message, "Balance Calculation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void Compute()
        {
            // Factures Summation
            SumFacturesUncleared = TotalFacturesUnCleared + TotalFacturesNoSeason;
            SumFacturesCleared = TotalFacturesCleared;
            SumFactures = SumFacturesUncleared + SumFacturesCleared;

            // Receipts Summation
            SumReceipts = TotalReceiptSeason + TotalReceiptNoSeason;

            // Calculation
            Remainder = SumFactures - SumReceipts;
        }

        public void SetReceipts()
        {
            BindableCollection<ReceiptModel> receipts_season = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_season_no_factures = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_season_with_factures = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_no_season = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_no_season_no_factures = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_no_season_with_factures = new BindableCollection<ReceiptModel>();
            if (Season != null)
                receipts_season = Customer.GetMyReceipts(Season.Id);
            receipts_no_season = Customer.GetMyReceipts(0);
            List<int?> receipt_facture_ids = new List<int?>();
            List<int?> facture_ids = new List<int?>();
            for (int i = 0; i < FacturesList.Count; i++)
            {
                int? Id = FacturesList[i].GetReceipt();
                if (Id != null)
                {
                    receipt_facture_ids.Add(Id);
                    facture_ids.Add(FacturesList[i].Id);
                }
            }
            for (int i = 0; i < FacturesSandalone.Count; i++)
            {
                int? Id = FacturesSandalone[i].GetReceipt();
                if (Id != null)
                {
                    receipt_facture_ids.Add(Id);
                    facture_ids.Add(FacturesSandalone[i].Id);
                }
            }

            bool added = false;
            foreach(ReceiptModel receipt in receipts_season)
            {
                added = false;
                foreach(int? receipt_id in receipt_facture_ids)
                {
                    if(receipt.Id == receipt_id)
                    {
                        receipts_season_with_factures.Add(receipt);
                        added = true;
                    }
                }
                if(!added)
                {
                    receipts_season_no_factures.Add(receipt);
                }
            }
            foreach (ReceiptModel receipt in receipts_no_season)
            {
                added = false;
                foreach (int? receipt_id in receipt_facture_ids)
                {
                    if (receipt.Id == receipt_id)
                    {
                        receipts_no_season_with_factures.Add(receipt);
                        added = true;
                    }
                }
                if (!added)
                {
                    receipts_no_season_no_factures.Add(receipt);
                }
            }

            ReceiptsSeasons = receipts_season;
            ReceiptsNoSeasons = receipts_no_season;
            ReceiptsSeasonsWithFactures = receipts_season_with_factures;
            ReceiptsSeasonsNoFactures = receipts_season_no_factures;
            ReceiptsNoSeasonsNoFactures = receipts_no_season_no_factures;
            FactureIdsWithReceipts = facture_ids;

            TotalReceiptSeason = AddReceiptsAmount(ReceiptsSeasons, Currency.Id);
            TotalReceiptSeasonNoFacture = AddReceiptsAmount(ReceiptsSeasonsNoFactures, Currency.Id);
            TotalReceiptSeasonWithFacture = AddReceiptsAmount(ReceiptsSeasonsWithFactures, Currency.Id);
            TotalReceiptNoSeason = AddReceiptsAmount(ReceiptsNoSeasons, Currency.Id);
            TotalReceiptNoSeasonNoFacture = AddReceiptsAmount(ReceiptsNoSeasonsNoFactures, Currency.Id);
            TotalReceiptNoSeasonWithFacture = AddReceiptsAmount(ReceiptsSeasonsWithFactures, Currency.Id);
        }

        public void SetFactures()
        {
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_cleared = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_un_cleared = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_standalone = new BindableCollection<FactureModel>();
            if (Season != null && Season.Id.ToString() != "0")
            {
                factures = Customer.GetMyFactures(Season.Id);
            }
            factures_standalone = Customer.GetMyUnClearedFactures(0);
            foreach(FactureModel facture in factures)
            {
                if (facture.Cleared == true)
                    factures_cleared.Add(facture);
                else
                    factures_un_cleared.Add(facture);
            }
            FacturesList = factures;
            FacturesSandalone = factures_standalone;
            FacturesListCleared = factures_cleared;
            FacturesListUncleared = factures_un_cleared;
            Factures = FacturesList;
            foreach(FactureModel fm in factures_standalone)
            {
                Factures.Add(fm);
            }

            TotalFacturesNoSeason = AddFacturesAmount(FacturesSandalone, Currency.Id);
            TotalFacturesUnCleared = AddFacturesAmount(FacturesListUncleared, Currency.Id);
            TotalFacturesCleared = AddFacturesAmount(FacturesListCleared, Currency.Id);
            TotalFactures = AddFacturesAmount(FacturesList, Currency.Id);
        }
        #endregion

        #region
        public float AddReceiptsAmount(BindableCollection<ReceiptModel> receipts, int currency)
        {
            float total = 0;
            foreach(ReceiptModel rm in receipts)
            {
                ConversionModel cm = new ConversionModel();
                total += (float)cm.Convert((decimal)rm.Amount, (int)rm.Currency, currency);
            }
            return total;
        }

        public float AddFacturesAmount(BindableCollection<FactureModel> factures, int currency)
        {
            float total = 0;
            foreach(FactureModel fm in factures)
            {
                total += (float)fm.TotalAmount(currency);
            }
            return total;
        }
        #endregion
    }
}
