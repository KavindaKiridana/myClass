using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace myClass
{
    class StudentClass
    {
        BDconnect connect = new BDconnect();

        // create function to add new student 
        public bool addStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] photo)
        //public bool addStudent(string fname, string lname, DateTime bdate, string gender, string phone, string address, byte[] photo)
        {


            connect.openConnect();


            SqlCommand command = new SqlCommand("INSERT INTO [Tabledata] (StdFirstName, StdLastName, BirthDate, Gender, Phone, Address,Photo) VALUES (@fn, @ln, @birth, @gn, @num, @address,@img)");
            //SqlCommand command = new SqlCommand("INSERT INTO Table (StdFirstName, StdLastName, BirthDate, Gender, Phone, Address, Photo) VALUES (@fn, @ln, @birth, @gn, @num, @address, @img)");
            command.Connection = connect.getconnection;
            command.Parameters.Add("@fn", System.Data.SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", System.Data.SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@birth", System.Data.SqlDbType.DateTime).Value = bdate;
            command.Parameters.Add("@gn", System.Data.SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@num", System.Data.SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@address", System.Data.SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@img", System.Data.SqlDbType.VarBinary).Value = photo;

            command.ExecuteNonQuery();
            connect.closeConnect();
            return true;
        }

        //to get student table
        public DataTable getRecords()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Tabledata]", connect.getconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);//please fix the error at this line (please mention where did u modified within //comments
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //function that can execute count query (male ,female ,total)
        public string execute(string query)
        {
            SqlCommand command = new SqlCommand(query, connect.getconnection);
            connect.openConnect();
            string count = command.ExecuteScalar().ToString();
            connect.closeConnect();
            return count;
        }

        //get total number of students
        public string totalStudent()
        {
            return execute("SELECT count(*) FROM [Tabledata]");
        }

        //get male student count
        public string totMaleStudent()
        {
            return execute("SELECT count(*) FROM [Tabledata] WHERE 'Gender'='Male' ");
        }

        //get female student count
        public string totFemaleStudent()
        {
            try
            {
                return execute("SELECT count(*) FROM [Tabledata] WHERE 'Gender'='Female' ");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return execute("SELECT count(*) FROM [Tabledata] WHERE 'Gender'='Female' ");
        }

        //create a function to search student
        public DataTable searchRecords(string searchInformation)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM [Tabledata] WHERE Stdid LIKE '%" + searchInformation + "%' OR StdFirstName LIKE '%" + searchInformation + "%' OR StdLastName LIKE '%" + searchInformation + "%' OR Phone LIKE '%" + searchInformation + "%'", connect.getconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
