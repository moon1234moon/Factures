using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Factures.ViewModels
{
    public class CustomersViewModel : Conductor<object>
    {
        #region
        private string _search_text;
        private string _search_type;
        private BindableCollection<CustomerModel> _customers;
        private BindableCollection<string> _search_types;
        private int _selected_type_index;
        #endregion

        public CustomersViewModel()
        {
            CustomerModel cm = new CustomerModel();
            DataTable customers = cm.All();
            Customers = cm.GiveCollection(customers);
            SetSearchTypes();
        }

        #region
        public string SearchText
        {
            get { return _search_text; }
            set
            {
                _search_text = value;
                NotifyOfPropertyChange(() => SearchText);
            }
        }

        public string SearchType
        {
            get { return _search_type; }
            set
            {
                _search_type = value;
                NotifyOfPropertyChange(() => SearchType);
            }
        }


        public int SelectedTypeIndex
        {
            get { return _selected_type_index; }
            set
            {
                _selected_type_index = value;
                NotifyOfPropertyChange(() => SelectedTypeIndex);
            }
        }

        public BindableCollection<string> SearchTypes
        {
            get { return _search_types; }
            set
            {
                _search_types = value;
                NotifyOfPropertyChange(() => SearchTypes);
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
        #endregion

        #region
        public void CreateNew()
        {
            ActivateItem(new CreateCustomerViewModel());
        }

        public void BtnEdit(CustomerModel cm)
        {
            ActivateItem(new EditCustomerViewModel(cm));
        }

        public void BtnView(CustomerModel cm)
        {
            ActivateItem(new ViewCustomerViewModel(cm));
        }
        #endregion

        #region
        public void Search()
        {
            if (SearchText != null && SearchText.Trim() != "")
            {
                try
                {
                    CustomerModel customer = new CustomerModel();
                    switch (SearchType)
                    {
                        case "Id":
                            customer = customer.Get(System.Convert.ToInt32(SearchText));
                            if (customer == null || customer.Name == null)
                                throw new Exception("Can't find Customer with Id " + SearchText);
                            BtnView(customer);
                            break;
                        case "Name":
                            customer = new CustomerModel();
                            Customers = customer.SearchBy("Name", SearchText);
                            if (Customers.Count == 0)
                                throw new Exception("There is no name like " + SearchText);
                            if (Customers.Count == 1)
                                BtnView(Customers[0]);
                            if (Customers.Count > 1)
                                ActivateItem(new BlankViewModel());
                            break;
                        case "Customer Type Name":
                            customer = new CustomerModel();
                            Customers = customer.SearchBy("Customer Type Name", SearchText);
                            if (Customers.Count == 0)
                                throw new Exception("There is no customer type like " + SearchText);
                            if (Customers.Count == 1)
                                BtnView(Customers[0]);
                            if (Customers.Count > 1)
                                ActivateItem(new BlankViewModel());
                            break;
                        case "Customer Type Id":
                            customer = new CustomerModel();
                            Customers = customer.SearchBy("Customer Type Id", SearchText);
                            if (Customers.Count == 0)
                                throw new Exception("There is no customers of type Id " + SearchText);
                            if (Customers.Count == 1)
                                BtnView(Customers[0]);
                            if (Customers.Count > 1)
                                ActivateItem(new BlankViewModel());
                            break;
                        default:
                            MessageBox.Show("Set a search type please!", "Search Type", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
                catch(Exception e)
                {
                    ActivateItem(new BlankViewModel());
                    MessageBox.Show(e.Message, "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void SetSearchTypes()
        {
            List<string> types = new List<string>()
            {
                "Id",
                "Name",
                "Customer Type Name",
                "Customer Type Id",
            };
            SearchTypes = new BindableCollection<string>(types);
            SearchType = SearchTypes[0];
        }
        #endregion
    }
}
