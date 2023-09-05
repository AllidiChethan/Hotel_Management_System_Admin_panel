using System;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        { 
            SplashBar.Value = SplashBar.Value+1;
            if (SplashBar.Value == 100)
            {
                timer1.Stop();
                Form1 f = new Form1();
                f.Show();
                this.Hide();
            }

        }

        private void Loading_Load(object sender, EventArgs e)
        {
              this.timer1.Start();
        }

        private void SplashBar_Click(object sender, EventArgs e)
        {

        }
    }
}
