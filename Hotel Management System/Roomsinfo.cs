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
    public partial class Roomsinfo : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        CRUDops ops =new CRUDops();
        
        public void cleardata()
        {
            roomnotbl.Clear();
            roomphonetbl.Clear();
            yesradio.Checked = false;
            noradio.Checked = false;

        }
        public void details()
        {
            
            string query = "select * from Room_tbl";
            var d = ops.dataset(query);
            Roomgridview.DataSource = d.Tables[0];
            

        }
        public Roomsinfo()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl3.Text = DateTime.Now.ToLongTimeString();
        }

        private void Datelbl_Click(object sender, EventArgs e)
        {

        }

        private void Roomsinfo_Load(object sender, EventArgs e)
        {
            Datelbl3.Text = DateTime.Now.ToLongTimeString();
            timer3.Start();
            details();
        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            
            try
            {
                string isfree;
                if (yesradio.Checked == true)
                {
                    isfree = "free";
                }
                else
                {
                    isfree = "busy";
                }
                if (roomnotbl.Text!="" && roomphonetbl.Text!="" && (yesradio.Checked==true || noradio.Checked==true))
                {
                    string q = "insert into Room_tbl values('" + roomnotbl.Text + "','" + roomphonetbl.Text + "','" + isfree + "')";
                    
                    ops.AddDetails(q);
                    MessageBox.Show("Room successfully Added");
                    details();
                    cleardata();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details..");
                }
            }
            catch (Exception ex)
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
                if (roomnotbl.Text!="" && roomphonetbl.Text!="" && (yesradio.Checked==true || noradio.Checked == true))
                {
                    string isfree;
                    if (yesradio.Checked == true)
                    {
                        isfree = "free";
                    }
                    else
                    {
                        isfree = "busy";
                    }
                    string query = "update Room_tbl set RoomPhone='" + roomphonetbl.Text + "',RoomFree='" + isfree + "' where RoomId='" + roomnotbl.Text + "' ";
                    
                    ops.AddDetails(query);
                    MessageBox.Show("The Room Details are Updated");
                    details();
                    cleardata();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details");
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
       

        private void Roomgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            roomnotbl.Text = Roomgridview.SelectedRows[0].Cells[0].Value.ToString();
            roomphonetbl.Text = Roomgridview.SelectedRows[0].Cells[1].Value.ToString();
            if (Roomgridview.SelectedRows[0].Cells[2].Value.ToString() == "free")
            {
                yesradio.Checked = true;
            }
            else
            {
                noradio.Checked = true;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
           
            string query = "delete from Room_tbl where RoomId='" + roomnotbl.Text + "'";
            ops.AddDetails(query);
            MessageBox.Show("The Room Details are Deleted");
            details();
            cleardata();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm mf=new MainForm();
            mf.Show();
            this.Hide();
        }

        private void Search_Click_1(object sender, EventArgs e)
        {
            
            string query = "select * from Room_tbl where RoomId='"+roomsearch.Text+"'";
            var d =ops.dataset(query);
            Roomgridview.DataSource = d.Tables[0];

        }

        private void Staffsearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            details();
        }
    }
}
