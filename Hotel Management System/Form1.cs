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

namespace Hotel_Management_System
{
    public partial class Form1 : Form
    {
        SqlConnection con=new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True ");
        CRUDops ops = new CRUDops();
        public Form1()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string q = "select count(*) from Staff_tbl where StaffName='" + usernametb.Text + "'and StaffPassword='" + passwordtb.Text + "'";
            var dt = ops.login(q);
            if (dt.Rows[0][0].ToString() == "1")
            {
                MainForm mf=new MainForm();
                mf.Show();
                this.Hide();
            }
            else 
            {
                MessageBox.Show("Wrong Username or Password");
            }
            con.Close();
            
        }

        private void usernametb_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
