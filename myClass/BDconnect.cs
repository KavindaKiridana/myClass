using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace myClass
{
    class BDconnect
    {
        // Create connection
        SqlConnection connect = new SqlConnection("Data Source=DESKTOP-64B0UVI\\SQLEXPRESS;Initial Catalog=studentdb;Integrated Security=True");

        // to get connection
        public SqlConnection getconnection
        {
            get
            {
                return connect;
            }
        }

        //create a function to open connection
        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        //create a function to close connection
        public void closeConnect()
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }
    }
}
