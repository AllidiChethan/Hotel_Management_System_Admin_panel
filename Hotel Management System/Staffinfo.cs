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
using System.Web;

namespace Hotel_Management_System
{
    
    public partial class Staffinfo : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");

        CRUDops ops = new CRUDops();
        public void cleardata()
        {
            staffidtbl.Clear();
            staffnametbl.Clear();
            staffpasstbl.Clear();
            staffgentbl.Text= string.Empty;
            staffnumtbl.Clear();
        }
        public void details()
        { 
            
            string myq = "select * from Staff_tbl";
            var d = ops.dataset(myq);
            Staffgridview.DataSource = d.Tables[0];
            
        }
        public Staffinfo()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl1.Text = DateTime.Now.ToLongTimeString();
        }

        private void Staffinfo_Load(object sender, EventArgs e)
        {
            Datelbl1.Text= DateTime.Now.ToLongTimeString();
            timer2.Start();
            details();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm m = new MainForm();
            m.Show();
            this.Hide();
        }

        private void Clientgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            staffidtbl.Text = Staffgridview.SelectedRows[0].Cells[0].Value.ToString();
            staffnametbl.Text = Staffgridview.SelectedRows[0].Cells[1].Value.ToString();
            staffnumtbl.Text = Staffgridview.SelectedRows[0].Cells[2].Value.ToString();
            staffgentbl.Text = Staffgridview.SelectedRows[0].Cells[3].Value.ToString();
            staffpasstbl.Text = Staffgridview.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (staffidtbl.Text!="" && staffnametbl.Text!="" && staffnumtbl.Text!="" && staffgentbl.Text!="" && staffpasstbl.Text != "")
                {
                    int res = staffpasstbl.Text.Length;
                    if (res > 3 & res < 7)
                    {
                        string q = "insert into Staff_tbl values('" + staffidtbl.Text + "','" + staffnametbl.Text + "','" + staffnumtbl.Text + "','" + staffgentbl.SelectedItem.ToString() + "','" + staffpasstbl.Text + "')";
                        ops.AddDetails(q);
                        MessageBox.Show("The Staff Information Is Successfully Added");
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct format Password should be greater than 3 characters and less than 6 characters");
                    }
                    details();
                    cleardata();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details..");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (staffidtbl.Text!="" && staffnametbl.Text!="" && staffnumtbl.Text!="" && staffgentbl.Text!="" && staffpasstbl.Text != "")
                {
                    int res = staffpasstbl.Text.Length;
                    if (res > 3 && res < 6)
                    {
                        string query = "update Staff_tbl set StaffName='" + staffnametbl.Text + "',StaffPhone='" + staffnumtbl.Text + "',Gender='" + staffgentbl.SelectedItem.ToString() + "',StaffPassword='" + staffpasstbl.Text + "' where StaffId='" + staffidtbl.Text + "'";
                        
                        ops.AddDetails(query);
                        MessageBox.Show("The Staff Details are Successfully Updated");
                        cleardata();
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct format Password should be greater than 3 characters and less than 6 characters");
                    }
                    details();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details..");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            
            string query = "delete from Staff_tbl where StaffId='" + staffidtbl.Text + "'";
            ops.AddDetails(query);
            MessageBox.Show("The Details Of The Selected Staff Member Is Deleted");
            details();
            cleardata();
            

        }

        private void Search_Click(object sender, EventArgs e)
        {
            
            string q = "select * from Staff_tbl where StaffName='"+Staffsearch.Text+"'";
            var d=ops.dataset(q);
            Staffgridview.DataSource = d.Tables[0];
         

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            details();
        }

        private void Datelbl1_Click(object sender, EventArgs e)
        {

        }

        private void staffpasstbl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
