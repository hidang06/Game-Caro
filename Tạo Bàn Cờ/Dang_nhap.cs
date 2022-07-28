using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using Tạo_Bàn_Cờ.Connection;

namespace Tạo_Bàn_Cờ
{
    public partial class Dang_nhap : Form
    {

       
        public static string title;
        public Dang_nhap()
        {
            InitializeComponent();
        }
        private void opencuasochinh(object obj)
        {
            Main csc = new Main(title);
            Application.Run(csc);
        }

        void close()
        {
            Thread th = new Thread(opencuasochinh);
            this.Close();
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
           /* this.Hide();
            var cuaSoChinh = new CuaSoChinh(title);
            cuaSoChinh.Closed += (s, args) => this.Close();
            cuaSoChinh.Show();*/
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
           if(!string.IsNullOrEmpty(textBox1.Text)&&!string.IsNullOrEmpty(textBox2.Text))
            {
                string mySQL = string.Empty;

                mySQL += "SELECT *FROM [GAME_CARO].[dbo].[INFO]";
                mySQL += "WHERE TENTK = '" + textBox1.Text + "' ";
                mySQL += "AND PW ='" + textBox2.Text + "'";
                DataTable userData = SeverConnection.executeSQL(mySQL);
                if(userData.Rows.Count>0)
                {
                    title = textBox1.Text;
                    MyInfo.username = title;
                    textBox1.Clear();
                    textBox2.Clear();
                    close();
                    this.textBox1.Select();
                }
                else
                {
                    MessageBox.Show("The username or password is incorrect. Try again");
                    textBox2.Focus();
                    textBox2.SelectAll();
                }
            }
           else
            {
                MessageBox.Show("Please enter username and password.");
                textBox2.Select();
            }    
            
        }*/
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string mySQL = string.Empty;

                mySQL += "SELECT *FROM [GAME_CARO].[dbo].[INFO]";
                mySQL += "WHERE TENTK = '" + textBox1.Text + "' ";
                mySQL += "AND PW ='" + textBox2.Text + "'";
                DataTable userData = SeverConnection.executeSQL(mySQL);
                if (userData.Rows.Count >= 0)
                {
                    title = textBox1.Text;
                    Thong_tin.username = title;
                    textBox1.Clear();
                    textBox2.Clear();
                    close();
                    this.textBox1.Select();
                }
                else
                {
                    MessageBox.Show("The username or password is incorrect. Try again");
                    textBox2.Focus();
                    textBox2.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Please enter username and password.");
                textBox2.Select();
            }
        }
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            if (!pictureBox1.Focused)
            {
                Thread th = new Thread(reopendn);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            */
        }
        private void reopendn(object obj)
        {
            Application.Run(new Đangnhap());
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Select();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
