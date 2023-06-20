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
    public partial class ManageScore : Form
    {
        CourseClass courseClass = new CourseClass();
        BDconnect connect = new BDconnect();
       ScoreForm scoreForm = new ScoreForm();

        public ManageScore()
        {
            InitializeComponent();
            formload();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string searchkey = txt_seach.Text;
            SqlCommand command = new SqlCommand("SELECT * FROM [score] WHERE Stdid LIKE '%" + searchkey + "%' OR cname LIKE '%" + searchkey + "%' OR details LIKE '%" + searchkey + "%'", connect.getconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgv_mscore.DataSource = table;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                try
                {
                    double score = double.Parse(txt_score.Text);
                    int id = int.Parse(txt_sid.Text);
                    string details = txt_sdetails.Text;
                    string cname = comboBox1.SelectedText;

                    connect.openConnect();

                    string sql = "UPDATE score SET cname = '" + cname + "' , score ='" + score + "'  WHERE Stdid='" + id + "' ";
                    SqlCommand command = new SqlCommand(sql, connect.getconnection);


                    command.ExecuteNonQuery();
                    connect.closeConnect();
                    MessageBox.Show("Score updated successfully");
                    btn_clear_Click(sender, e);
                    dgv_mscore.DataSource = getRecords(new SqlCommand("SELECT Stdid, cname, score,Details FROM score", connect.getconnection));

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void formload()
        {
            // Populate the ComboBox with course names
            comboBox1.DataSource = courseClass.getRecords();
            comboBox1.DisplayMember = "cname";
            comboBox1.ValueMember = "cid";

            // Display student list in the DataGridView
            dgv_mscore.DataSource = getRecords(new SqlCommand("SELECT Stdid, cname, score,Details FROM score", connect.getconnection));

        }

        public DataTable getRecords(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_score.Clear();
            txt_sdetails.Clear();
            txt_seach.Clear();
            txt_sid.Clear();
            comboBox1.SelectedIndex = -1;
        }

        public bool validation()
        {
            if (string.IsNullOrWhiteSpace(txt_sid.Text) ||
                string.IsNullOrWhiteSpace(txt_sdetails.Text) ||
                string.IsNullOrWhiteSpace(txt_score.Text) ||
                string.IsNullOrWhiteSpace(comboBox1.Text))
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
            if (string.IsNullOrWhiteSpace(txt_sid.Text))
            {
                MessageBox.Show("Student ID is required");
            }
            else
            {
                try
                {
                    int id = int.Parse(txt_sid.Text);
                    string sql = "DELETE FROM score WHERE Stdid ='" + id + "'";
                    SqlCommand sqlCommand = new SqlCommand(sql, connect.getconnection);

                    connect.openConnect();
                    sqlCommand.ExecuteNonQuery();
                    connect.closeConnect();

                    MessageBox.Show("Record deleted");
                    dgv_mscore.DataSource = getRecords(new SqlCommand("SELECT Stdid, cname, score, Details FROM score", connect.getconnection));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgv_mscore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //there is a error please correct it
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgv_mscore.Rows[e.RowIndex];
                    txt_sid.Text = row.Cells[0].Value.ToString();
                    comboBox1.SelectedValue = row.Cells[1].Value;
                    txt_score.Text = row.Cells[2].Value.ToString();
                    txt_sdetails.Text = row.Cells[3].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
