using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace myClass
{
    public partial class LoginForm : Form
    {
        BDconnect bDconnect = new BDconnect();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string username = txt_name.Text;
            string pin = txt_pin.Text;

            string sql = "SELECT * FROM [user] WHERE pin = '" + pin + "' AND uname ='" + username + "'";
            SqlCommand sqlCommand = new SqlCommand(sql, bDconnect.getconnection);

            DataTable dataTable = new DataTable();

            try
            {
                bDconnect.getconnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                bDconnect.getconnection.Close();
            }

            if (dataTable.Rows.Count > 0)
            {
                Form1 form1 = new Form1();//change this line 
                this.Hide();
                form1.Show();
            }
        }
    }
}
