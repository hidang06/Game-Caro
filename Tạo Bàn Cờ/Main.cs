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
    
    public partial class Main : Form
    {
        Thread th;
        public static int k;
        public static int Ptbox = 0;
        string username = Dang_nhap.title;
        public Main()
        {
            InitializeComponent();
        }
        public Main(string user) : this()
        {
            label3.Text = username;

        }
        void Info(object obj)
        {
            Application.Run(new Thong_tin());
        }
        void ReopenCSC(object obj)
        {
            Application.Run(new Đangnhap());
        }
        void Rank(object obj)
        {
            Application.Run(new Xep_hang());
        }
        void Play(object obj)
        {
            if (radioButton1.Checked)
            {
                k = 0;
                Application.Run(new Pvp(k));
            }
            else
            {
                k = 2;
                Application.Run(new Pvp(2));
            }
        }
        void Aboutgame(object obj)
        {
            Application.Run(new Gioi_thieu());
        }
        void AIChess(object obj)
        {
            Application.Run(new VS_May());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            /*this.Close();
            th = new Thread(Aboutgame);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
         
            var aboutgame= new Gioi_thieu();
          
            aboutgame.Show();
            Ptbox = 1;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            /*this.Close();
            th = new Thread(Info);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
           
            var myInfo = new Thong_tin();
       
            myInfo.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(Play);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
           /* int k;
            this.Hide();
            if (radioButton2.Checked)
                k = 2;
            else
                k = 0;
            var play = new Play(k);
            play.Closed += (s, args) => this.Close();
            play.Show();*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(ReopenCSC);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            /*this.Hide();
            var đangnhap= new Đangnhap();
            đangnhap.Closed += (s, args) => this.Close();
            đangnhap.Show();*/
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            /*this.Close();
            th = new Thread(Rank);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
          
            var ranking= new Xep_hang();
         
            ranking.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            /*this.Close();
            th = new Thread(AIChess);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
            this.Hide();
            var aIChess = new VS_May();
            aIChess.Closed += (s, args) => this.Close();
            aIChess.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
