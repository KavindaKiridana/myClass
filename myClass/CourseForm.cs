using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myClass
{
    public partial class CourseForm : Form
    {
        CourseClass objcourse = new CourseClass();
        public CourseForm()
        {
            InitializeComponent();
            showData();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_cName.Clear();
            txt_details.Clear();
            txt_hours.Clear();
        }

        bool validation()
        {
            if (string.IsNullOrWhiteSpace(txt_cName.Text) ||
                string.IsNullOrWhiteSpace(txt_details.Text) ||
                string.IsNullOrWhiteSpace(txt_hours.Text))
            {
                MessageBox.Show("All fields are required");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void showData()
        {
            //show data in dgv
            dgv_student.DataSource = objcourse.getRecords();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                try
                {
                    //add record
                    string cname = txt_cName.Text;
                    string dateils = txt_details.Text;
                    int hours = int.Parse(txt_hours.Text);

                    bool success = objcourse.addStudent(cname, hours, dateils);
                    MessageBox.Show("Data inserted successfully.");

                    btn_clear.PerformClick();
                    showData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
