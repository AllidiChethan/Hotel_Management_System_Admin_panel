using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    internal class CRUDops
    {
        SqlConnection connection =new SqlConnection(@"Data Source=INCHETHAN\SQLEXPRESS;Initial Catalog=HMS;Integrated Security=True");
        public void AddDetails(string query)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public DataSet dataset(string query)
        {
            connection.Open();
            SqlDataAdapter adp=new SqlDataAdapter(query,connection);
            DataSet ds=new DataSet();
            adp.Fill(ds);
            connection.Close();
            return ds;
            
        }
        public DataTable filldata(string query)
        {
            connection.Open();
            SqlCommand cmd=new SqlCommand(query,connection);
            SqlDataReader r=cmd.ExecuteReader();
            DataTable d=new DataTable();
            d.Columns.Add("Roomid", typeof(int));
            d.Load(r);
            connection.Close();
            return d;
            
        }
        public DataTable filldata1(string query)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader r = cmd.ExecuteReader();
            DataTable d = new DataTable();
            d.Columns.Add("ClientName", typeof(string));
            d.Load(r);
            connection.Close();
            return d;
        }
        public DataTable login(string Query)
        {
            connection.Open();
            SqlDataAdapter cmd=new SqlDataAdapter(Query,connection);
            DataTable dt = new DataTable();
            cmd.Fill(dt);
            connection.Close();
            return dt;
        }
    }
}
