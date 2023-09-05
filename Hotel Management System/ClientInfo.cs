using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.SqlClient;

namespace Hotel_Management_System
{
    public partial class ClientInfo : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        CRUDops ops = new CRUDops();
       
        public void cleardata()
        { 
            clientidtbl.Clear();
            clientnametbl.Clear();
            clientnumtbl.Clear();
            clientcrttbl.Text = string.Empty;
            
        }
        public void Collect_Details()
        { 
            
            string Myquery = "select * from Client_tbl";
            var ds = ops.dataset(Myquery);
            Clientgridview.DataSource = ds.Tables[0];
            
        }
        public ClientInfo()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Datelbl.Text = DateTime.Now.ToLongTimeString();
        }

        private void ClientInfo_Load(object sender, EventArgs e)
        {
            Datelbl.Text= DateTime.Now.ToLongTimeString();
            timer1.Start();
            Collect_Details();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientidtbl.Text != "" && clientnametbl.Text != "" && clientnumtbl.Text != "" && clientcrttbl.Text != "")
                {

                    string q = "insert into Client_tbl values('" + clientidtbl.Text + "','" + clientnametbl.Text + "','" + clientnumtbl.Text + "','" + clientcrttbl.SelectedItem.ToString() + "')";
                    connection.Open();
                    ops.AddDetails(q);
                    MessageBox.Show("Client details added successfully");
                    Collect_Details();
                    cleardata();
                }
                else
                {
                    
                    MessageBox.Show("Please Enter all the details..");
                    
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

        private void Clientgridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            clientidtbl.Text = Clientgridview.SelectedRows[0].Cells[0].Value.ToString();
            clientnametbl.Text = Clientgridview.SelectedRows[0].Cells[1].Value.ToString();
            clientnumtbl.Text = Clientgridview.SelectedRows[0].Cells[2].Value.ToString();
            clientcrttbl.Text = Clientgridview.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (clientidtbl.Text != "" && clientnametbl.Text != "" && clientnumtbl.Text != "" && clientcrttbl.Text != "")
                {
                    
                    string mq = "update client_tbl set ClientName='" + clientnametbl.Text + "',ClientPhone='" + clientnumtbl.Text + "',ClientCountry='" + clientcrttbl.SelectedItem.ToString() + "' where ClientId='" + clientidtbl.Text + "'";
                    connection.Open();
                    ops.AddDetails(mq);
                    MessageBox.Show("The Client Details are Successfully Updated");
                    Collect_Details();
                    cleardata();
                }
                else
                {
                    MessageBox.Show("Please Enter all The Details...");
                    connection.Close();
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
           
            string query = "delete from Client_tbl where ClientId= '" + clientidtbl.Text+ "'";
            ops.AddDetails(query);
            MessageBox.Show("The Client Info Sucessfully Deleted");
           
            Collect_Details();
            cleardata();

        }

        private void Search_Click(object sender, EventArgs e)
        {
            
            string mq = "select * from Client_tbl where ClientName ='" + Clientsearch.Text + "'";
            var d = ops.dataset(mq);
            Clientgridview.DataSource = d.Tables[0];
            
            cleardata();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Collect_Details();
            Clientsearch.Clear(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MainForm mf=new MainForm();
            mf.Show();
            this.Hide();
        }
    }
}
