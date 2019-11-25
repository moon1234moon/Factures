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
        /*
         * Variables
         */
        #region
        /*
         * Titles and results variables
         */
        #region
        private string _title;
        private string _total_facturs_un_cleared_text;
        private string _total_facturs_cleared_text;
        private BindableCollection<ResultModel> _result;
        #endregion

        /*
         * Sums and totals variables
         */
        #region
        private float _remainder;
        private float _sum_factures_uncleared;
        private float _sum_factures_cleared;
        private float _sum_receipts;
        private float _sum_receipts_on_credit;
        private float _sum_receipts_on_facture;
        private float _total_factures_no_season_cleared;
        private float _total_factures_no_season_un_cleared;
        private float _total_factures_cleared;
        private float _total_factures_uncleared;
        private float _total_receipt_season;
        private float _total_receipt_season_no_factures;
        private float _total_receipt_season_with_factures;
        private float _total_receipt_no_season;
        private float _total_receipt_no_season_no_factures;
        private float _total_receipt_no_season_with_factures;
        #endregion

        /*
         * Collections and main variables
         */
        #region
        private Boolean _seasoned_factures;
        private CurrencyModel _currency;
        private SeasonModel _season;
        private CustomerModel _customer;
        private BindableCollection<FactureModel> _facture;
        private BindableCollection<ReceiptModel> _receipts;
        private BindableCollection<FactureModel> _factures_list;
        private BindableCollection<FactureModel> _factures_list_cleared;
        private BindableCollection<FactureModel> _factures_list_un_cleared;
        private BindableCollection<FactureModel> _factures_standalone_cleared;
        private BindableCollection<FactureModel> _factures_standalone_un_cleared;
        private BindableCollection<FactureModel> _factures_standalone;
        private BindableCollection<ReceiptModel> _receipts_season = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_season_no_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_season_with_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season_no_factures = new BindableCollection<ReceiptModel>();
        private BindableCollection<ReceiptModel> _receipts_no_season_with_factures = new BindableCollection<ReceiptModel>();
        #endregion
        #endregion

        /*
         * Constructor
         */
        #region
        public BalanceViewModel(CustomerModel customer = null, SeasonModel season = null, CurrencyModel currency = null, Boolean seasoned_factures = true)
        {
            SeasondFactures = seasoned_factures;
            Customer = customer;
            Season = season;
            Currency = currency;
            SetBalance();
        }
        #endregion

        /*
         * Properties
         */
        #region
        /*
         * Titles and results properties
         */
        #region
        public string TotalFacturesUnClearedText
        {
            get { return _total_facturs_un_cleared_text; }
            set
            {
                _total_facturs_un_cleared_text = value;
                NotifyOfPropertyChange(() => TotalFacturesUnClearedText);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string TotalFacturesClearedText
        {
            get { return _total_facturs_cleared_text; }
            set
            {
                _total_facturs_cleared_text = value;
                NotifyOfPropertyChange(() => TotalFacturesClearedText);
            }
        }

        public BindableCollection<ResultModel> Result
        {
            get { return _result; }
            set
            {
                _result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }
        #endregion

        /*
         * Sums and Totals properties
         */
        #region
        public float Remainder
        {
            get { return _remainder; }
            set
            {
                _remainder = value;
                NotifyOfPropertyChange(() => Remainder);
            }
        }

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

        public float SumReceiptsOnCredit
        {
            get { return _sum_receipts_on_credit; }
            set
            {
                _sum_receipts_on_credit = value;
                NotifyOfPropertyChange(() => SumReceiptsOnCredit);
            }
        }

        public float SumReceiptsOnFacture
        {
            get { return _sum_receipts_on_facture; }
            set
            {
                _sum_receipts_on_facture = value;
                NotifyOfPropertyChange(() => SumReceiptsOnFacture);
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

        public float TotalFacturesNoSeasonCleared
        {
            get { return _total_factures_no_season_cleared; }
            set
            {
                _total_factures_no_season_cleared = value;
                NotifyOfPropertyChange(() => TotalFacturesNoSeasonCleared);
            }
        }

        public float TotalFacturesNoSeasonUnCleared
        {
            get { return _total_factures_no_season_un_cleared; }
            set
            {
                _total_factures_no_season_un_cleared = value;
                NotifyOfPropertyChange(() => TotalFacturesNoSeasonUnCleared);
            }
        }
        #endregion

        /*
         * Collections and main properites
         */
        #region
        public Boolean SeasondFactures
        {
            get { return _seasoned_factures; }
            set
            {
                _seasoned_factures = value;
                NotifyOfPropertyChange(() => SeasondFactures);
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

        public BindableCollection<ReceiptModel> ReceiptsNoSeasonsWithFactures
        {
            get { return _receipts_no_season_with_factures; }
            set
            {
                _receipts_no_season_with_factures = value;
                NotifyOfPropertyChange(() => ReceiptsNoSeasonsWithFactures);
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

        public BindableCollection<FactureModel> FacturesStandaloneCleared
        {
            get { return _factures_standalone_cleared; }
            set
            {
                _factures_standalone_cleared = value;
                NotifyOfPropertyChange(() => FacturesStandaloneCleared);
            }
        }
        
        public BindableCollection<FactureModel> FacturesStandaloneUnCleared
        {
            get { return _factures_standalone_un_cleared; }
            set
            {
                _factures_standalone_un_cleared = value;
                NotifyOfPropertyChange(() => FacturesStandaloneUnCleared);
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
        #endregion

        /*
         * Main funtions
         */
        #region
        public void SetBalance()
        {
            try
            {
                if (Customer != null)
                {
                    SetTitle();
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
        #endregion

        /*
         * Computing funtions
         */
        #region
        public void Compute()
        {
            /*
             * Add Factures totals to set main sums
             */
            ComputeFactures();

            /*
             * Add Recipt totals to set main sums
             */
            ComputeReceipts();

            /*
             * Compute Result
             */
            ComputeResult();

            /*
             * Print Result
             */
            PrintResult();
        }

        public void PrintResult()
        {
            /*
             * Print Facture Amounts
             */
            TotalFacturesClearedText = "Total amount = " + SumFacturesCleared + " " + Currency.Symbol;
            TotalFacturesUnClearedText = "Total amount = " + SumFacturesUncleared + " " + Currency.Symbol;

            List<ResultModel> results = new List<ResultModel> ();
            ResultModel result = new ResultModel("Sum of unpaid factures", SumFacturesUncleared + " " + Currency.Symbol);
            results.Add(result);
            result = new ResultModel("Sum of receipts paid on credit", SumReceiptsOnCredit + " " + Currency.Symbol);
            results.Add(result);
            result = new ResultModel("Remainder", Remainder + " " + Currency.Symbol);
            results.Add(result);
            Result = new BindableCollection<ResultModel>(results);
        }

        public void ComputeResult()
        {
            /*
             * Remainder = Amount Needed from Customer
             * Remainder = Sum-Of-Factures-Un-Paid - Sum-Of-Receipts-Paid-On-Credit;
             */
            Remainder = SumFacturesUncleared - SumReceiptsOnCredit;
        }

        public void ComputeReceipts()
        {

            /*
             * Add paid amount on credit
             * With Season and With no Season
             * 
             * Save
             */
            SumReceiptsOnCredit = TotalReceiptNoSeasonNoFacture + TotalReceiptSeasonNoFacture;


            /*
             * Add paid amount on facture
             * With Season and With no Season
             * 
             * Save
             */
            SumReceiptsOnFacture = TotalReceiptSeasonNoFacture + TotalReceiptSeasonWithFacture;

            /*
             * Add Whole paid amount and Save
             */
            SumReceipts = SumReceiptsOnCredit + SumReceiptsOnFacture;
        }

        public void ComputeFactures()
        {
            switch (Season.Id)
            {
                case 0:
                    switch (Season.Year)
                    {
                        case "None":
                            SumFacturesUncleared = TotalFacturesNoSeasonUnCleared;
                            SumFacturesCleared = TotalFacturesNoSeasonCleared;
                            break;
                        case "All":
                            SumFacturesUncleared = TotalFacturesUnCleared;
                            SumFacturesCleared = TotalFacturesCleared;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    SumFacturesUncleared = TotalFacturesUnCleared;
                    SumFacturesCleared = TotalFacturesCleared;
                    break;
            }
        }
        #endregion

        /*
         * Setting variables and computing totals funtions
         */
        #region
        public void SetTitle()
        {
            switch (Season.Id)
            {
                case 0:
                    switch (Season.Year)
                    {
                        case "None":
                            Title = "Balance for " + Customer.Name + " for factures and receipts that don't have a season";
                            break;
                        default:
                            Title = "Balance for " + Customer.Name + " for factures and receipts in all seasons";
                            break;
                    }
                    break;
                default:
                    Title = "Balance for " + Customer.Name + " for factures and receipts in season " + Season.Year;
                    break;
            }
        }

        public void SetReceipts()
        {
            /*
             * Variables
             */
            #region
            BindableCollection<ReceiptModel> receipts = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_season = new BindableCollection<ReceiptModel>();
            BindableCollection<ReceiptModel> receipts_no_season = new BindableCollection<ReceiptModel>();
            ReceiptsSeasons = new BindableCollection<ReceiptModel>();
            ReceiptsNoSeasons = new BindableCollection<ReceiptModel>();
            ReceiptsSeasonsNoFactures = new BindableCollection<ReceiptModel>();
            ReceiptsSeasonsWithFactures = new BindableCollection<ReceiptModel>();
            ReceiptsNoSeasonsNoFactures = new BindableCollection<ReceiptModel>();
            ReceiptsNoSeasonsWithFactures = new BindableCollection<ReceiptModel>();
            #endregion

            /*
             * Filling Receipts
             */
            if (Season != null)
            {
                switch (Season.Id)
                {
                    case 0:
                        switch (Season.Year)
                        {
                            case "None":
                                ReceiptsNoSeasons = Customer.GetMyReceipts(Currency.Id, 0);
                                receipts_no_season = Customer.GetMyReceipts(Currency.Id, 0);
                                break;
                            case "All":
                                ReceiptsSeasons = Customer.GetMyReceipts(Currency.Id, -1);
                                receipts_season = Customer.GetMyReceipts(Currency.Id, -1);
                                switch (SeasondFactures)
                                {
                                    case true:
                                        ReceiptsNoSeasons = Customer.GetMyReceipts(Currency.Id, 0);
                                        receipts_no_season = Customer.GetMyReceipts(Currency.Id, 0);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        ReceiptsSeasons = Customer.GetMyReceipts(Currency.Id, Season.Id);
                        receipts_season = Customer.GetMyReceipts(Currency.Id, Season.Id);
                        switch (SeasondFactures)
                        {
                            case true:
                                ReceiptsNoSeasons = Customer.GetMyReceipts(Currency.Id, 0);
                                receipts_no_season = Customer.GetMyReceipts(Currency.Id, 0);
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }

            /*
             * Fill receiprs vaious variables
             */
            foreach(ReceiptModel receipt in ReceiptsSeasons)
            {
                string facture = receipt.Facture.ToString();
                if (facture == "")
                    ReceiptsSeasonsNoFactures.Add(receipt);
                else
                    ReceiptsSeasonsWithFactures.Add(receipt);
            }
            foreach (ReceiptModel receipt in ReceiptsNoSeasons)
            {
                string facture = receipt.Facture.ToString();
                if (facture == "")
                    ReceiptsNoSeasonsNoFactures.Add(receipt);
                else
                    ReceiptsNoSeasonsWithFactures.Add(receipt);
            }

            receipts = ReceiptsSeasons;
            foreach (ReceiptModel receipt in ReceiptsNoSeasons)
            {
                receipts.Add(receipt);
            }
            Receipts = receipts;
            ReceiptsSeasons = receipts_season;
            ReceiptsNoSeasons = receipts_no_season;

            TotalReceiptSeason = AddReceiptsAmount(ReceiptsSeasons, Currency.Id);
            TotalReceiptSeasonNoFacture = AddReceiptsAmount(ReceiptsSeasonsNoFactures, Currency.Id);
            TotalReceiptSeasonWithFacture = AddReceiptsAmount(ReceiptsSeasonsWithFactures, Currency.Id);
            TotalReceiptNoSeason = AddReceiptsAmount(ReceiptsNoSeasons, Currency.Id);
            TotalReceiptNoSeasonNoFacture = AddReceiptsAmount(ReceiptsNoSeasonsNoFactures, Currency.Id);
            TotalReceiptNoSeasonWithFacture = AddReceiptsAmount(ReceiptsSeasonsWithFactures, Currency.Id);
        }

        public void SetFactures()
        {
            /*
             * Variables
             */
            #region
            BindableCollection<FactureModel> factures = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_cleared = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_un_cleared = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_standalone = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_standalone_cleared = new BindableCollection<FactureModel>();
            BindableCollection<FactureModel> factures_standalone_un_cleared = new BindableCollection<FactureModel>();
            #endregion

            switch (Season.Id)
            {
                case 0:
                    switch(Season.Year)
                    {
                        case "None":
                            factures = Customer.GetMyFactures(0);
                            factures_standalone = factures;
                            FacturesList = factures;
                            break;
                        case "All":
                            factures = Customer.GetMyFactures(-1);
                            switch(SeasondFactures)
                            {
                                case true:
                                    factures_standalone = Customer.GetMyFactures(0);
                                    break;
                                default:
                                    break;
                            }
                            FacturesList = factures;
                            foreach(FactureModel facture in factures_standalone)
                            {
                                FacturesList.Add(facture);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    factures = Customer.GetMyFactures(Season.Id);
                    switch (SeasondFactures)
                    {
                        case true:
                            factures_standalone = Customer.GetMyFactures(0);
                            break;
                        default:
                            break;
                    }
                    FacturesList = factures;
                    foreach (FactureModel facture in factures_standalone)
                    {
                        FacturesList.Add(facture);
                    }
                    break;
            }

            /*
             * Fill Cleared and Un-Cleared factures
             */
            foreach(FactureModel facture in factures)
            {
                if (facture.Cleared == true)
                    factures_cleared.Add(facture);
                else
                    factures_un_cleared.Add(facture);
            }
            foreach (FactureModel facture in factures_standalone)
            {
                if (facture.Cleared == true)
                    factures_standalone_cleared.Add(facture);
                else
                    factures_standalone_un_cleared.Add(facture);
            }

            /*
             * Save All Factures
             */
            FacturesSandalone = factures_standalone;
            FacturesListCleared = factures_cleared;
            FacturesListUncleared = factures_un_cleared;
            FacturesStandaloneCleared = factures_standalone_cleared;
            FacturesStandaloneUnCleared = factures_standalone_un_cleared;


            /*
             * Set The Total of All Factures
             */
            TotalFacturesNoSeasonCleared = AddFacturesAmount(FacturesStandaloneCleared, Currency.Id);
            TotalFacturesNoSeasonUnCleared = AddFacturesAmount(FacturesStandaloneUnCleared, Currency.Id);
            TotalFacturesUnCleared = AddFacturesAmount(FacturesListUncleared, Currency.Id);
            TotalFacturesCleared = AddFacturesAmount(FacturesListCleared, Currency.Id);
        }
        #endregion

        /*
         * Addition funtions
         */
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
