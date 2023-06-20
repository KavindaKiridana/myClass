using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace myClass
{
    public partial class RegisterForm : Form
    {
        StudentClass student = new StudentClass();
        public RegisterForm()
        {
            InitializeComponent();
            showdata();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                try
                {
                    //add record
                    string fname = txt_fname.Text;
                    string lname = txt_lname.Text;
                    DateTime bdate = dateTimePicker1.Value;
                    string phone = txt_phone.Text;
                    string address = txt_address.Text;
                    string gender = rbtn_male.Checked ? "Male" : "Female";


                    //to get image form imgbox
                    MemoryStream ms = new MemoryStream();
                    imgbox.Image.Save(ms, imgbox.Image.RawFormat);
                    byte[] img = ms.ToArray();
                    bool success = student.addStudent(fname, lname, bdate, gender, phone, address, img);
                    if (success)
                    {
                        MessageBox.Show("Data inserted successfully.");
                    }
                    btn_clear.PerformClick();
                    showdata();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                /*
                bool success = student.addStudent(fname, lname, bdate, gender, phone, address,img);
                if (success)
                {
                    MessageBox.Show("Data inserted successfully.");
                }
                */
            }
        }

        bool validation()
        {
            if (string.IsNullOrWhiteSpace(txt_fname.Text) ||
                string.IsNullOrWhiteSpace(txt_lname.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text) ||
                string.IsNullOrWhiteSpace(txt_address.Text) ||
                imgbox.Image == null || imgbox.Image == imgbox.InitialImage)
            {
                MessageBox.Show("All fields are required");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            showdata();
        }
        //to show student data in dgv_student
        public void showdata()
        {
            dgv_student.DataSource = student.getRecords();
            //dgv_student.RowTemplate.Height = 70;
            DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
            imgColumn = (DataGridViewImageColumn)dgv_student.Columns[7];
            imgColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            // Clear data
            txt_fname.Text = string.Empty;
            txt_lname.Text = string.Empty;
            txt_phone.Text = string.Empty;
            txt_address.Text = string.Empty;
            imgbox.Image = null;
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
    }
}
