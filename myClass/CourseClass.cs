using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace myClass
{
    class CourseClass
    {
        BDconnect connect = new BDconnect();

        // create function to add new class 
        public bool addStudent(string cname, int hours, string details)
        {
            connect.openConnect();

            string sql = "INSERT INTO [course] (cname, chour,details) VALUES ('" + cname + "','" + hours + "','" + details + "')";
            SqlCommand command = new SqlCommand(sql, connect.getconnection);

            command.Connection = connect.getconnection;
            command.CommandType = CommandType.Text;

            command.ExecuteNonQuery();
            connect.closeConnect();
            return true;
        }

        //create a function to datagridview
        public DataTable getRecords()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [course]", connect.getconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);//please fix the error at this line (please mention where did u modified within //comments
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
