using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Tạo_Bàn_Cờ.Connection;
using System.Data.SqlClient;
namespace Tạo_Bàn_Cờ
{
    public partial class Dang_ky : Form
    {
        public static DateTime date;
        Thread th;
        public Dang_ky()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void openLogin()//object obj)
        {
            //Application.Run(new Login());
            this.Hide();
            var login = new Dang_nhap();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }


        private void Dangky_Load(object sender, EventArgs e)
        {       }

        private void Dangky_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void reopendn(object obj)
        {
            Application.Run(new Đangnhap());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Save Data";
            
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please enter UserName",caption,btn,ico);
                textBox1.Select();
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter Password", caption, btn, ico);
                textBox2.Select();
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please enter Confirm", caption, btn, ico);
                textBox3.Select();
                return;
            }

            if (textBox2.Text!=textBox3.Text)
            {
                MessageBox.Show("Your password and confirm password not match");
                textBox1.Select();
                return;
            }

            //Lưu dữ liệu
            string yourSQL = "SELECT TENTK FROM [GAME_CARO].[dbo].[INFO] WHERE TENTK = '" + textBox1.Text + "'";
            DataTable checkDuplicates = Tạo_Bàn_Cờ.Connection.SeverConnection.executeSQL(yourSQL);
           /* if(checkDuplicates.Rows.Count>0)
            {
                MessageBox.Show("The username already exist. Please try another");
                textBox1.SelectAll();
                return;
            }*/
            DialogResult result;
            result = MessageBox.Show("Do you want to save this Login.");
            if(result==DialogResult.OK)
            {
                date = DateTime.Now;
              
                string mySQL = string.Empty;
                mySQL += "INSERT INTO [GAME_CARO].[dbo].[INFO] (TENTK,PW,NGDK,TONG,WIN,LOSE,SCORE)";
                // mySQL += "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "'"+ + ",0,0,0,1600)";
                mySQL += "VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + date + "',0,0,0,1600)";
                Tạo_Bàn_Cờ.Connection.SeverConnection.executeSQL(mySQL);
                MessageBox.Show("Succeeded!");
       
                
            }
            /*  this.Close();
              th = new Thread(openLogin);
              th.SetApartmentState(ApartmentState.STA);
              th.Start();*/
            openLogin();
            pictureBox2.Enabled = false;
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

    }
}
