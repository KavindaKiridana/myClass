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
    public partial class ManageCourseForm : Form
    {
        CourseClass objcourse = new CourseClass();
        SqlConnection sqlConnection = new SqlConnection(" Data Source=DESKTOP-64B0UVI\\SQLEXPRESS;Initial Catalog=studentdb;Integrated Security=True ");

        public ManageCourseForm()
        {
            InitializeComponent();
            showData();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                try
                {
                    int id = int.Parse(txt_score.Text);
                    string cname = txt_sid.Text;
                    int hours = int.Parse(txt_h.Text);
                    string details = txt_details.Text;

                    string sql = "UPDATE course SET cname = '" + cname + "',chour='" + hours + "',details='" + details + "'  WHERE cid='" + id + "' ";
                    SqlCommand command = new SqlCommand(sql, sqlConnection);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                    showData();

                    MessageBox.Show("Successfully updated");
                    btn_clear.PerformClick();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void showData()
        {
            //show data in dgv
            dgv_mscore.DataSource = objcourse.getRecords();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_sid.Clear();
            txt_score.Clear();
            txt_details.Clear();
            txt_h.Clear();
        }

        bool validation()
        {
            if (string.IsNullOrWhiteSpace(txt_sid.Text) ||
                string.IsNullOrWhiteSpace(txt_details.Text) ||
                string.IsNullOrWhiteSpace(txt_score.Text) ||
                string.IsNullOrWhiteSpace(txt_h.Text))
            {
                MessageBox.Show("All fields are required");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_score.Text))
            {
                MessageBox.Show("Course id must required");
            }
            else
            {
                try
                {
                    int id = int.Parse(txt_score.Text);
                    string sql = "DELETE FROM course WHERE cid ='" + id + "'";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Record deleted");
                    showData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgv_mscore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_score.Text = dgv_mscore.CurrentRow.Cells[0].Value.ToString();
            txt_sid.Text = dgv_mscore.CurrentRow.Cells[1].Value.ToString();
            txt_h.Text = dgv_mscore.CurrentRow.Cells[2].Value.ToString();
            txt_details.Text = dgv_mscore.CurrentRow.Cells[3].Value.ToString();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string searchkey = txt_seach.Text;
            SqlCommand command = new SqlCommand("SELECT * FROM [course] WHERE cid LIKE '%" + searchkey + "%' OR cname LIKE '%" + searchkey + "%' OR details LIKE '%" + searchkey + "%'", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgv_mscore.DataSource = table;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
