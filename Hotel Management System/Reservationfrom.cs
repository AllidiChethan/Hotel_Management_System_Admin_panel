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
using System.Data.SqlTypes;

namespace Hotel_Management_System
{
    public partial class Reservationfrom : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True"); 

        CRUDops ops =new CRUDops();
        public void cleardata()
        { 
            reservationidtbl.Clear();
            resclientnametbl.Text = string.Empty;
            resroomidtbl.Text = string.Empty;
            
        }
        public void details()
        {
            
            string sql = "select * from Reservation_tbl";
            var d = ops.dataset(sql);
            Reservationgridview.DataSource = d.Tables[0];
            
        }
        public void fillroomcombo()
        {
            string q = "select RoomId from Room_tbl where RoomFree='free'";
            var dt = ops.filldata(q);
            resroomidtbl.ValueMember = "RoomId";
            resroomidtbl.DataSource = dt;
        }
        public void fillclientcombo()
        {
            string q = "select ClientName from Client_tbl";
            var dt = ops.filldata1(q);
            resclientnametbl.ValueMember = "ClientName";
            resclientnametbl.DataSource = dt;
        }
        public Reservationfrom()
        {
            InitializeComponent();
        }
       

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm mf=new MainForm();
            mf.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl4.Text = DateTime.Now.ToLongTimeString();
        }
        DateTime today;

        private void Reservationfrom_Load(object sender, EventArgs e)
        {
            today = DateTime.Today;
            fillroomcombo();
            fillclientcombo();
            Datelbl4.Text = DateTime.Today.Day.ToString()+"-"+DateTime.Today.Month.ToString()+"-"+DateTime.Today.Year.ToString();
            
            details();

        }

        private void datein_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(datein.Value, today);
            if (res < 0)
                MessageBox.Show("Wrong Date for Reservation");
                datein.Value = DateTime.Today;
        }

        private void dateout_ValueChanged(object sender, EventArgs e)
        {
            int res = DateTime.Compare(dateout.Value, datein.Value);
            if (res < 0)
                MessageBox.Show("Choose Valid Out Date");
                dateout.Value = DateTime.Today;
        }
        public void updateroomstate()
        {
            string newstate = "busy";
            string query = "update Room_tbl set RoomFree='" + newstate + "' where RoomId='" + Convert.ToInt32(resroomidtbl.SelectedValue.ToString()) + "'";
            ops.AddDetails(query);
            fillroomcombo();
        }
        public void updateroomondelet()
        {

            string newstate = "free";
            int inp = Convert.ToInt32(Reservationgridview.SelectedRows[0].Cells[2].Value.ToString());
            string query = "update Room_tbl set RoomFree='" + newstate + "' where RoomId='" + inp + "'";
            ops.AddDetails(query);
            fillroomcombo();
        }

        private void Add_Click(object sender, EventArgs e)
        {

            try
            {
                if (reservationidtbl.Text!="" && resclientnametbl.Text!="" && resroomidtbl.Text != "")
                {
                    string q = "insert into Reservation_tbl values('" + reservationidtbl.Text + "','" + resclientnametbl.SelectedValue.ToString() + "','" + resroomidtbl.SelectedValue.ToString() + "','" + datein.Value.ToString() + "','" + dateout.Value.ToString() + "')";
                    connection.Open();
                    ops.AddDetails(q);
                    MessageBox.Show("Your Room Reservation is Completed");
                    updateroomstate();
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
                if (reservationidtbl.Text!=""&&resclientnametbl.Text!=""&&resroomidtbl.Text!="")
                {
                    string q = "update Reservation_tbl set Client='" + resclientnametbl.SelectedValue.ToString() + "',Room='" + resroomidtbl.SelectedValue.ToString() + "',DateIn='" + datein.Value.ToString() + "',DateOut='" + dateout.Value.ToString() + "' where ResId='" + reservationidtbl.Text + "'";
                    connection.Open();
                    ops.AddDetails(q);
                    MessageBox.Show("Your Preference of Reservation is Change");
                    updateroomondelet();
                    updateroomstate();
                    details();
                    cleardata();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details");
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

        private void Delete_Click(object sender, EventArgs e)
        {
            if(reservationidtbl.Text=="")
            {
                MessageBox.Show("enter a Reservation to be Deleted");
            }
            else
            {
                string q = "delete from Reservation_tbl where ResId='" + reservationidtbl.Text + "'";
                ops.AddDetails(q);
                connection.Close();
                MessageBox.Show("The Reservation Cancled Successfully and Client Details also Deleted");
                updateroomondelet();
                details();
                cleardata();
            }

        }

        private void roomsearch_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            details();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            
            string q = "select * from Reservation_tbl where ResId='" + roomsearch.Text + "'";
            var d = ops.dataset(q);
            Reservationgridview.DataSource = d.Tables[0];
            
        }

        private void Datelbl4_Click(object sender, EventArgs e)
        {

        }

        private void resclientnametbl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Reservationgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            reservationidtbl.Text = Reservationgridview.SelectedRows[0].Cells[0].Value.ToString();
            resclientnametbl.Text = Reservationgridview.SelectedRows[0].Cells[1].Value.ToString();
            resroomidtbl.Text = Reservationgridview.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void reservationidtbl_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
