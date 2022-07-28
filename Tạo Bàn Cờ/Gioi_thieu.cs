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
    public partial class Gioi_thieu : Form
    {
        Thread th;
        int num;
        string user = Dang_nhap.title;
        public Gioi_thieu()
        {
            InitializeComponent();
            num = Main.Ptbox;
        }

        void reopenDN(object obj)
        {
            Application.Run(new Đangnhap());
        }
        void reopenCSC(object obj)
        {
            Application.Run(new Main(user));
        }
        private void aboutgame_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* if (num == 1)
            {
                th = new Thread(reopenCSC);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
               
            }
            else if (num==2)
            {
                th = new Thread(reopenDN);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }    */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
