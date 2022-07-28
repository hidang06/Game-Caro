using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Tạo_Bàn_Cờ
{
    public partial class Đangnhap : Form
    {
        Thread th;
        public Đangnhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        #region opencachchoi
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //this.Close();
            /* th = new Thread(opencachchoi);
             th.SetApartmentState(ApartmentState.STA);
             th.Start();*/
            //this.Hide();
            var cachchoi = new Cach_choi();
            //cachchoi.Closed += (s, args) => this.Close();
            cachchoi.Show();
        }
        private void opencachchoi(object obj)
        {
            Application.Run(new Cach_choi());
        }
        #endregion

        #region openLogin
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            /*this.Close();
            th = new Thread(opennewlogin);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
            this.Hide();
            var login = new Dang_nhap();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }
        private void opennewlogin(object obj)
        {
            Application.Run(new Dang_nhap());
        }
        #endregion

        #region openRegister
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           /* this.Close();
               th = new Thread(openDK);
               th.SetApartmentState(ApartmentState.STA);
               th.Start();*/
            this.Hide();
            var dangky = new Dang_ky();
            dangky.Closed += (s, args) => this.Close();
            dangky.Show();
        }
        private void openDK(object obj)
        {
            Application.Run(new Dang_ky());
        }
        #endregion

        #region aboutgame
         private void button3_Click(object sender, EventArgs e)
        {
           
            
            
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Main.Ptbox = 2;
            //this.Hide();
            var aboutgame = new Gioi_thieu();
            //aboutgame.Closed += (s, args) => this.Close();
            aboutgame.Show();
        }
        private void openAG(object obj)
        {
            Main.Ptbox = 2;
            Application.Run(new Gioi_thieu());
        }
        #endregion
        private void Đangnhap_Load(object sender, EventArgs e)
        {
           
           
        }

        
    }
}
