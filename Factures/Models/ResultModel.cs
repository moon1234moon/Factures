using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.Models
{
    public class ResultModel
    {
        #region
        private string _name;
        private string _result;
        private string _note;
        #endregion

        public ResultModel(string name = null, string result = null, string note = null)
        {
            if (name != null)
                Name = name;
            if (result != null)
                Result = result;
            if (note != null)
                Note = note;
        }

        #region
        public string Note
        {
            get { return _note; }
            set { _note = value; }
        }

        public string Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

    }
}
