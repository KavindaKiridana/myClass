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
    public partial class ManageStudent : Form
    {
        StudentClass student = new StudentClass();
        RegisterForm registerForm = new RegisterForm();
        public ManageStudent()
        {
            InitializeComponent();
            showdata();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            dgv_stdmanage.DataSource = student.searchRecords(txt_seach.Text);
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn = (DataGridViewImageColumn)dgv_stdmanage.Columns[7];
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (validation())
            {

                try
                {
                    //variable declaretion
                    string fname = txt_fname.Text;
                    string lname = txt_lname.Text;
                    DateTime bdate = dateTimePicker1.Value;
                    string phone = txt_phone.Text;
                    string address = txt_address.Text;
                    string gender = rbtn_male.Checked ? "Male" : "Female";
                    int id = int.Parse(txt_id.Text);
                    //image
                    //MemoryStream ms = new MemoryStream();
                    //imgbox.Image.Save(ms, imgbox.Image.RawFormat);
                    //byte[] img = ms.ToArray();

                    //creating the sql query
                    string sql = "UPDATE Tabledata SET StdFirstName = '" + fname + "', StdLastName = '" + lname + "' ,Birthdate='" + bdate + "' ,Phone='" + phone + "',Gender='" + gender + "' ,Address='" + address + "' WHERE Stdid='" + id + "' ";
                    //creating the command
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                    //handling the exceptions

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Successfully updated");
                    btn_clear.PerformClick();
                    showdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //creating the connection
        SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-64B0UVI\\SQLEXPRESS;Initial Catalog=studentdb;Integrated Security=True");


        bool validation()
        {
            if (string.IsNullOrWhiteSpace(txt_fname.Text) ||
                string.IsNullOrWhiteSpace(txt_lname.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text) ||
                string.IsNullOrWhiteSpace(txt_address.Text))
            //imgbox.Image == null || imgbox.Image == imgbox.InitialImage
            {
                MessageBox.Show("All fields are required");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            // Upload image from local server
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "select photo(*.jpg;*.png;*.gif)|*.jpg;*png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                imgbox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_seach.Clear();
            txt_fname.Text = string.Empty;
            txt_lname.Text = string.Empty;
            txt_phone.Text = string.Empty;
            txt_address.Text = string.Empty;
            txt_id.Clear();
            imgbox.Image = null;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                MessageBox.Show("Id number required");
            }
            else
            {
                try
                {
                    int id = int.Parse(txt_id.Text);
                    string sql = "DELETE FROM Tabledata WHERE Stdid='" + id + "'";
                    SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    MessageBox.Show("Successfully deleted");
                    btn_clear.PerformClick();
                    showdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void showdata()
        {
            dgv_stdmanage.DataSource = student.getRecords();
            //dgv_student.RowTemplate.Height = 70;
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn = (DataGridViewImageColumn)dgv_stdmanage.Columns[7];
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void dgv_stdmanage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row index
            {
                DataGridViewRow row = dgv_stdmanage.Rows[e.RowIndex];
                txt_id.Text = row.Cells[0].Value.ToString();
                txt_fname.Text = row.Cells[1].Value.ToString();
                txt_lname.Text = row.Cells[2].Value.ToString();
                dateTimePicker1.Value = (DateTime)row.Cells[3].Value;
                txt_phone.Text = row.Cells[4].Value.ToString();
                txt_address.Text = row.Cells[6].Value.ToString();

                if (row.Cells[5].Value.ToString() == "Male")
                    rbtn_male.Checked = true;
                else
                    rbtn_female.Checked = true;

                // Uncomment the following lines if you want to display an image
                //byte[] img = (byte[])row.Cells[7].Value;
                //MemoryStream memoryStream = new MemoryStream(img);
                //imgbox.Image = Image.FromStream(memoryStream);
            }
        }
    }
}
