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
    public partial class ScoreForm : Form
    {
        CourseClass courseClass = new CourseClass();
        BDconnect connect = new BDconnect();
        public ScoreForm()
        {
            InitializeComponent();
            
            showdata2();
        }

        private void txt_sid_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ScoreForm_Load(object sender, EventArgs e)
        {
            
        }

        public void showdata2()
        {
            comboBox1.DataSource = courseClass.getRecords();
            comboBox1.DisplayMember = "cname";
            comboBox1.ValueMember = "cid";

            // Display student list in the DataGridView
            dgv_student.DataSource = getRecords(new SqlCommand("SELECT Stdid, StdFirstName, StdLastName FROM Tabledata", connect.getconnection));
            //Display score list in DataGridView
            dgvShowData();
        }

        // Get student table
        public DataTable getRecords(SqlCommand command)
        {
            command.Connection = connect.getconnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        //create a function to show data on dgvScore
        public void dgvShowData()
        {
            string sql = "select * from score";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connect.getconnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "score");
            dgv_score.DataSource = dataSet.Tables["score"];
        }

        private void btn_add_Click(object sender, EventArgs e)
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

                    string sql = "INSERT INTO [score] (cname, Stdid, score, details) VALUES (@cname, @sid, @score, @details)";
                    SqlCommand command = new SqlCommand(sql, connect.getconnection);

                    command.Parameters.AddWithValue("@cname", cname);
                    command.Parameters.AddWithValue("@sid", id);
                    command.Parameters.AddWithValue("@score", score);
                    command.Parameters.AddWithValue("@details", details);

                    command.ExecuteNonQuery();
                    connect.closeConnect();
                    MessageBox.Show("Score added successfully");
                    btn_clear_Click(sender, e);
                    dgvShowData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_score.Text = string.Empty;
            txt_sdetails.Text = string.Empty;
            txt_sid.Text = string.Empty;
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

        private void dgv_student_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_sid.Text = dgv_student.CurrentRow.Cells[0].Value.ToString();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
    }
}
