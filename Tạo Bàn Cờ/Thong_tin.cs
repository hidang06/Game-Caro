using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Tạo_Bàn_Cờ
{
    public partial class Thong_tin : Form
    {

        public static string username;
        string user = Dang_nhap.title;
        string query1 = "select * from INFO where TENTK = '" + username + "'";
        
        string constr= Connection.SeverConnection.stringConnection;
        public Thong_tin()
        {
            InitializeComponent();
            groupBox1.BackColor = Color.FromArgb(120, Color.White);
            groupBox2.BackColor = Color.FromArgb(120, Color.White);
            groupBox3.BackColor = Color.FromArgb(120, Color.White);
            groupBox4.BackColor = Color.FromArgb(120, Color.White);
            groupBox5.BackColor = Color.FromArgb(120, Color.White);
            groupBox6.BackColor = Color.FromArgb(120, Color.White);
            UserBox.Text = user;
            GetInfo();

        }
        void GetInfo()
        {

            using (SqlConnection conn = new SqlConnection(constr))
            {
                string query2 = "select * from (select TENTK,SCORE,ROW_NUMBER() over(order by SCORE DESC) as RANK_ from INFO) as foo where TENTK = '"+username+"'; ";
                SqlDataAdapter adapter2 = new SqlDataAdapter(query2, conn);
                DataSet data2 = new DataSet();
                adapter2.Fill(data2);
                Rank.Text = data2.Tables[0].Rows[0]["RANK_"].ToString();
                conn.Open();
                SqlCommand cmd = new SqlCommand(query1, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            NGDK.Text = reader[2].ToString();
                            SVDC.Text = reader[3].ToString();
                            Win.Text = reader[4].ToString();
                            Lose.Text = reader[5].ToString();
                            DIEM.Text = reader[6].ToString();
                            
                        }
                    }
                }
                conn.Close();
            }
                
        }

        void reopenCSC(object obj)
        {
            Application.Run(new Main(user));
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        private void MyInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* Thread th = new Thread(reopenCSC);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MyInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
           /* var reopencsc = new CuaSoChinh(user);
            reopencsc.Show();*/
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Rank_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter_1(object sender, EventArgs e)
        {

        }
    }
}
