using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Factures.DataBase
{
    class Connection
    {
        private readonly string connectionString = "Data Source=DESKTOP-MBA30FR;Initial Catalog=factures;Integrated Security=True";
        private readonly SqlConnection connection;

        public Connection()
        {
            this.connection = new SqlConnection(this.connectionString);
        }

        public string ConnectionString
        {
            get;
        }

        public DataTable ExecuteSelectQuery(string query)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = this.connection;

            if (!this.TestConnection())
                return null;
            this.connection.Open();

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            try
            {
                adp.Fill(ds, "table1");
                dt = ds.Tables["table1"];
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return null;
            }

            this.connection.Close();

            return dt;
        }

        public DataTable ExecuteInsertQuery(string query, string tableName, bool return_data = true)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, this.connection);
            if (!this.TestConnection())
                return null;
            this.connection.Open();
            cmd.ExecuteNonQuery();
            this.connection.Close();
            if (return_data == true && tableName != string.Empty)
            {
                query = "select * from " + tableName + " where id = (select max(id) from " + tableName + ")";
                dt = this.ExecuteSelectQuery(query);
            }
            return dt;
        }

        public bool ExecuteAQuery(string query)
        {
            SqlCommand cmd = new SqlCommand(query, this.connection);
            if (!this.TestConnection())
                return false;
            this.connection.Open();
            cmd.ExecuteNonQuery();
            this.connection.Close();
            return true;
        }

        public bool TestConnection()
        {
            try
            {
                this.connection.Open();
                this.connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}

