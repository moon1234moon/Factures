using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factures.DataBase
{
    public class Model
    {
        private string _table;
        private List<string> _columns;
        private string[] _fillable;
        private Connection db;

        #region
        public string Table
        {
            get { return _table; }
            set { _table = value; }
        }

        public string[] Fillable
        {
            get { return _fillable; }
            set { _fillable = value; }
        }

        public List<string> Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        #endregion

        private bool IsFillable(string columnName)
        {
            for(int i = 0; i < Fillable.Length; i++)
            {
                if(Fillable[i] == columnName)
                    return true;
            }
            return false;
        }

        public DataTable Find(int id)
        {
            string query = "SELECT * FROM " + Table + " WHERE id = " + id;
            this.db = new Connection();
            DataTable dt = this.db.ExecuteSelectQuery(query);
            if(dt != null)
            {
                return dt;
            }
            return null;
        }

        public DataTable All()
        {
            string query = "SELECT * FROM " + Table;
            this.db = new Connection();
            DataTable dt = this.db.ExecuteSelectQuery(query);
            if (dt != null)
            {
                return dt;
            }
            return new DataTable();
        }

        public DataTable FindByLikeParameters(List<KeyValuePair<string, string[]>> conditions, List<KeyValuePair<string, string[]>> union = null)
        {
            string query = "SELECT * FROM " + Table + " WHERE ";
            var last = conditions[conditions.Count - 1];
            foreach (var element in conditions)
            {
                if (element.Equals(last))
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " LIKE " + element.Value[1] + " ";
                    else
                        query += element.Key + " LIKE '%" + element.Value[1] + "%' ";
                }
                else
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " LIKE " + element.Value[1] + " AND ";
                    else
                        query += element.Key + " LIKE '%" + element.Value[1] + "%' AND ";
                }
            }
            if (union != null)
            {
                query += "UNION ";
                query += "SELECT * FROM " + Table + " WHERE ";
                last = union[union.Count - 1];
                foreach (var element in union)
                {
                    if (element.Equals(last))
                    {
                        if (element.Value[0] == "number")
                            query += element.Key + " LIKE " + element.Value[1] + " ";
                        else
                            query += element.Key + " = '%" + element.Value[1] + "%' ";
                    }
                    else
                    {
                        if (element.Value[0] == "number")
                            query += element.Key + " LIKE " + element.Value[1] + " AND ";
                        else
                            query += element.Key + " LIKE '%" + element.Value[1] + "%' AND ";
                    }
                }
            }
            this.db = new Connection();
            DataTable result = this.db.ExecuteSelectQuery(query);
            return result;
        }

        public DataTable FindByParameters(List<KeyValuePair<string, string[]>> conditions, List<KeyValuePair<string, string[]>> union = null)
        {
            string query = "SELECT * FROM " + Table + " WHERE ";
            var last = conditions[conditions.Count - 1];
            foreach (var element in conditions)
            {
                if (element.Equals(last))
                {
                    if (element.Value[0] == "number")
                    {
                        switch(element.Value[1])
                        {
                            case "NULL":
                                query += element.Key + " IS " + element.Value[1] + " ";
                                break;
                            case "NOT-NULL":
                                query += element.Key + " IS NOT NULL ";
                                break;
                            default:
                                query += element.Key + " = " + element.Value[1] + " ";
                                break;
                        }
                    }
                    else
                        query += element.Key + " = '" + element.Value[1] + "' ";
                }
                else
                {
                    if (element.Value[0] == "number")
                    {
                        switch (element.Value[1])
                        {
                            case "NULL":
                                query += element.Key + " IS " + element.Value[1] + " AND ";
                                break;
                            case "NOT-NULL":
                                query += element.Key + " IS NOT NULL AND ";
                                break;
                            default:
                                query += element.Key + " = " + element.Value[1] + " AND ";
                                break;
                        }
                    }
                    else
                        query += element.Key + " = '" + element.Value[1] + "' AND ";
                }
            }
            if(union != null)
            {
                query += "UNION ";
                query += "SELECT * FROM " + Table + " WHERE ";
                last = union[union.Count - 1];
                foreach (var element in union)
                {
                    if (element.Equals(last))
                    {
                        if (element.Value[0] == "number")
                            if (element.Value[1] != "NULL")
                                query += element.Key + " = " + element.Value[1] + " ";
                            else
                                query += element.Key + " IS " + element.Value[1] + " ";
                        else
                            query += element.Key + " = '" + element.Value[1] + "' ";
                    }
                    else
                    {
                        if (element.Value[0] == "number")
                            if (element.Value[1] != "NULL")
                                query += element.Key + " = " + element.Value[1] + " AND ";
                            else
                                query += element.Key + " IS " + element.Value[1] + " AND ";
                        else
                            query += element.Key + " = '" + element.Value[1] + "' AND ";
                    }
                }
            }
            this.db = new Connection();
            DataTable result = this.db.ExecuteSelectQuery(query);
            return result;
        }

        #region
        public void Update(List<KeyValuePair<string, string[]>> Data, List<KeyValuePair<string, string[]>> Primaries)
        {
            string query = "UPDATE "+Table+" SET ";
            var last = Data[Data.Count - 1];
            foreach (var element in Data)
            {
                if (IsFillable(element.Key))
                {
                    if (element.Equals(last))
                    {
                        if (element.Value[0] == "number")
                            query += element.Key + " = " + element.Value[1] + " ";
                        else
                            query += element.Key + " = '" + element.Value[1] + "' ";
                    }
                    else
                    {
                        if (element.Value[0] == "number")
                            query += element.Key + " = " + element.Value[1] + ", ";
                        else
                            query += element.Key + " = '" + element.Value[1] + "', ";
                    }
                }
                else
                    throw new Exception($"Column {element.Key} is not Fillable");
            }
            query += "WHERE ";
            last = Primaries[Primaries.Count - 1];
            foreach (var element in Primaries)
            {
                if (element.Equals(last))
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " = " + element.Value[1] + ";";
                    else
                        query += element.Key + " = '" + element.Value[1] + "';";
                }
                else
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " = " + element.Value[1] + " AND ";
                    else
                        query += element.Key + " = '" + element.Value[1] + "' AND ";
                }
            }
            this.db = new Connection();
            var result = this.db.ExecuteAQuery(query);
        }

        public DataTable Save(List<KeyValuePair<string,string[]>> Data, bool return_data = true)
        {
            string query = "INSERT INTO " + Table + "(";
            var last = Data[Data.Count - 1];
            List<string[]> values = new List<string[]>();
            foreach (var element in Data)
            {
                if (IsFillable(element.Key))
                {
                    if (element.Equals(last))
                        query += element.Key + ")";
                    else
                        query += element.Key + ",";
                    values.Add(element.Value);
                }
                else
                {
                    throw new Exception($"Column {element.Key} is not Fillable");
                }
            }
            query += " VALUES(";
            string[] LastValue = values[values.Count - 1];
            foreach (var value in values)
            {
                if(value == LastValue)
                {
                    if(value[0] == "number")
                        query += value[1] + ");";
                    else
                        query += "'"+value[1] + "');";
                }
                else
                {
                    if (value[0] == "number")
                        query += value[1] + ",";
                    else
                        query += "'" + value[1] + "',";
                }
            }
            this.db = new Connection();
            DataTable dt = this.db.ExecuteInsertQuery(query, Table, return_data);
            return dt;
        }

        public bool DeleteThis(List<KeyValuePair<string, string[]>> Primaries)
        {
            string query = "DELETE FROM " + Table + " WHERE ";
            var last = Primaries[Primaries.Count - 1];
            foreach (var element in Primaries)
            {
                if (element.Equals(last))
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " = " + element.Value[1] + ";";
                    else
                        query += element.Key + " = '" + element.Value[1] + "';";
                }
                else
                {
                    if (element.Value[0] == "number")
                        query += element.Key + " = " + element.Value[1] + " AND ";
                    else
                        query += element.Key + " = '" + element.Value[1] + "' AND ";
                }
            }
            this.db = new Connection();
            bool result = this.db.ExecuteAQuery(query);
            return result;
        }
        #endregion
    }
}
