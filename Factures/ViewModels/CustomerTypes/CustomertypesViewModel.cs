using Caliburn.Micro;
using Factures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Factures.ViewModels
{
    public class CustomertypesViewModel : Conductor<object>
    {
        private BindableCollection<CustomertypeModel> _customerTypes;

        public CustomertypesViewModel()
        {
            CustomertypeModel ct = new CustomertypeModel();
            DataTable CustomerTypesTable = ct.All();
            this.FillAllCustomerTypes(CustomerTypesTable);
        }

        public BindableCollection<CustomertypeModel> CustomerTypes
        {
            get { return _customerTypes;  }
            set
            {
                _customerTypes = value;
                NotifyOfPropertyChange(() => CustomerTypes);
            }
        }

        public void CreateNew()
        {
            ActivateItem(new CreateCustomerTypeViewModel());
        }

        public void BtnEdit(CustomertypeModel cm)
        {
            ActivateItem(new EditCustomerTypeViewModel(cm));
        }

        public void BtnDelete(CustomertypeModel cm)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete customer type?", "Delete Customer Type", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool result = cm.Delete();
                if (result)
                    MessageBox.Show("Customer Type Deleted");
            }
        }

        private void FillAllCustomerTypes(DataTable cts)
        {
            List<CustomertypeModel> output = new List<CustomertypeModel>();
            foreach (DataRow row in cts.Rows)
            {
                CustomertypeModel ctm = new CustomertypeModel
                {
                    Id = System.Convert.ToInt32(row[0].ToString()),
                    Name = row[1].ToString(),
                };

                output.Add(ctm);
            }
            this.CustomerTypes = new BindableCollection<CustomertypeModel>(output);
        }
    }
}
