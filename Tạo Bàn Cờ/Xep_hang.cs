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
    public partial class Xep_hang : Form
    {
        Thread th;
        string user = Dang_nhap.title;
        string query1 = "select  TENTK,SCORE,ROW_NUMBER() over(order by SCORE DESC) as RANK_ from INFO";
      
        string constr = Connection.SeverConnection.stringConnection;
        public Xep_hang()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(110, Color.White);
            pictureBox1.BackColor = Color.FromArgb(60, Color.White);
            getRank();
        }
        void getRank()
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                DataSet data = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(query1, conn);
                adapter.Fill(data);
                SqlCommand comm = new SqlCommand("SELECT COUNT(*) FROM INFO", conn);
                Int32 count = (Int32)comm.ExecuteScalar();
                for (int i = 0; i < count; i++)
                { listBox1.Items.Add("  "+(i+1) + ". " + data.Tables[0].Rows[i]["TENTK"].ToString() + ": " + data.Tables[0].Rows[i]["SCORE"].ToString() + "\n"); }
               
    

                conn.Close();
            }
        }
        void reopenCSC(object obj)
        {
            Application.Run(new Main(user));
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Ranking_FormClosing(object sender, FormClosingEventArgs e)
        {
          /*  th = new Thread(reopenCSC);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();*/
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
