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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            this.Hide();
            registerForm.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ManageStudent manageStudent = new ManageStudent();
            this.Hide();
            manageStudent.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            this.Hide();
            courseForm.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ManageCourseForm manageCourse = new ManageCourseForm();
            this.Hide();
            manageCourse.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            ScoreForm scoreForm = new ScoreForm();
            this.Hide();
            scoreForm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ManageScore manageScore = new ManageScore();
            this.Hide();
            manageScore.Show();
        }
    }
}
