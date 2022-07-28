using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using static Tạo_Bàn_Cờ.SocketData;
using System.Data.SqlClient;

namespace Tạo_Bàn_Cờ
{
    public partial class Pvp : Form
    {
        Thread th;
        ChessBoardManager ChessBoard;
        string user = Dang_nhap.title;
        public static int check = 0;
        public static bool checktimeout, quitcheck=false;
        SocketManager socket;
 
        public Pvp(int k)
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            ChessBoard = new ChessBoardManager();
            ChessBoard.DrawChessBoard(panel1, textBox2, pictureBox1);
            ChessBoard.Currentplayer = k;

            ChessBoard.Endgame += ChessBoard_Endgame;
            ChessBoard.PlayerMark += ChessBoard_PlayerMark;
            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            timerCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            panel1.BackColor = Color.FromArgb(140, Color.White);
            panel2.BackColor = Color.FromArgb(130, Color.White);
           

            socket = new SocketManager();
        }
        void NewGame()
        {
           
            prcbCoolDown.Value = 0;
            timerCoolDown.Stop();
            ChessBoard.Currentplayer = Main.k;
            ChessBoard.DrawChessBoard(panel1, textBox2, pictureBox1);
            panel1.Enabled = true;
            //ChessBoard_PlayerMark(sender, e);
            pictureBox2.Enabled = true;
            socket.Send(new SocketData((int)SocketCommand.SENDNAME, Dang_nhap.title, new Point()));
        }    
        void EndGame()
        {
            timerCoolDown.Stop();
            panel1.Enabled = false;
            pictureBox2.Enabled = false;
            quitcheck = false;
        
        }
        private void ChessBoard_PlayerMark(object sender,ButtonclickEvent e)
        {
       
            timerCoolDown.Start();
            panel1.Enabled = false;
            prcbCoolDown.Value = 0;
            checktimeout = false;
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint1));

            pictureBox2.Enabled = false;
            Listen();
        }

        void reopenCSC(object obj)
        {
            Application.Run(new Main(user));
        }

        #region menu
        private void ChessBoard_Endgame(object sender, EventArgs e)
        {
            Win();
           
            socket.Send(new SocketData((int)SocketCommand.ENDGAME, "", new Point()));
            MessageBox.Show("Bạn đã thắng!");
            EndGame();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEWGAME, "", new Point()));
            panel1.Enabled = true;
        }
        private void pictureBox2_Click(object sender, EventArgs e)  ///Undo
        {
            
            socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));

        }
        void undo()
        {
            if (ChessBoard.Timeline.Count >= 2)
            {
                ChessBoard.Undo();
                ChessBoard.Undo();
                prcbCoolDown.Value = 0;
            }
            if(ChessBoard.Timeline.Count<2)
            {
                ChessBoard.Undo();
                ChessBoard.Undo();
                prcbCoolDown.Value = 0;
            }    
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) 
        {
            if (check == 0)
            {

                DialogResult Q = MessageBox.Show("Bạn có chắc muốn thoát Game?", "Warning!", MessageBoxButtons.OKCancel);
                if (Q == DialogResult.Cancel)
                    e.Cancel = true;
                else
                {
                    if(quitcheck)
                    {
                        Lose();
                    }    
                    try
                    {
                        socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                        socket.close();
                    }
                    catch
                    { }

                }    
            }

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            check = 1;
            DialogResult Q = MessageBox.Show("Bạn có chắc muốn thoát trận đấu?", "Warning!", MessageBoxButtons.OKCancel);
            if (Q == DialogResult.OK)
            {
                if (quitcheck)
                { 
                    Lose();
                }
                
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                    socket.close();
                }
                catch 
                { }
                 this.Close();
                 th = new Thread(reopenCSC);
                 th.SetApartmentState(ApartmentState.STA);
                 th.Start();
                this.Hide();
                var cuaSoChinh= new Main(user);
                cuaSoChinh.Closed += (s, args) => this.Close();
                cuaSoChinh.Show();
            }
            check = 0;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.MouseClick += new MouseEventHandler(pictureBox2_Click);

        }
        private void timerCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();
            if (checktimeout)
            {
                
                if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
                {
                    EndGame();
                    MessageBox.Show("Bạn đã thua!");
                    Lose();
                    socket.Send(new SocketData((int)SocketCommand.TiME_OUT, "", new Point()));
                }
            }
        }
        #endregion
        private void btnLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txtLAN.Text;

            if (!socket.ConnectServer())
            {
                checktimeout = true;
                socket.isServer = true;
                panel1.Enabled = true;
                quitcheck = true;
                socket.CreateServer();
               
            }
            else
            {
                checktimeout = false;
                socket.isServer = false;
                panel1.Enabled = false;
                quitcheck = true;
                Listen();
                socket.Send(new SocketData((int)SocketCommand.SENDNAME, Dang_nhap.title, new Point()));
              
            }

        }
    
        void request()
        {
                DialogResult Q = MessageBox.Show("Đối thủ muốn Undo?", "Request", MessageBoxButtons.YesNo);
                if(Q==DialogResult.Yes)
                {
                socket.Send(new SocketData((int)SocketCommand.YES, "", new Point()));
                undo();
                }    
                
        }
        private void Play_Shown(object sender, EventArgs e)
        {
            txtLAN.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtLAN.Text))
            {
                txtLAN.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            }
        }
        void Listen()
        {
           
                Thread listenThread = new Thread(() =>
                {
                    try
                    {
                        SocketData data = (SocketData)socket.Receive();
                        ProcessData(data);
                    }
                    catch { }
                   
                });     
                listenThread.IsBackground = true;
                listenThread.Start();
        }
        private void ProcessData(SocketData data)
        {
            switch(data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEWGAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        panel1.Enabled = false;
                    }));                 
                    break;
                case (int)SocketCommand.QUIT:
                    //quitcheck = false;
                    if (quitcheck == true)
                    {
                        this.Invoke((MethodInvoker)(() =>
                      {
                          timerCoolDown.Stop();
                          EndGame();
                          MessageBox.Show("Đối thủ đã chạy mất dép", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                          socket.close();
                          Win();

                      }));
                    }
                    socket.close();
                    break;
                case (int)SocketCommand.SEND_POINT:
                    checktimeout = true;
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcbCoolDown.Value = 0;
                        panel1.Enabled = true;
                        timerCoolDown.Start();
                        pictureBox2.Enabled = true;
                    })); 
                    ChessBoard.OtherplayerMark(data.Point);
                   
                    break;
                case (int)SocketCommand.UNDO:
                    request();           
                    break;
                case (int)SocketCommand.YES:
                    MessageBox.Show("Đối thủ đồng ý.");
                    this.Invoke((MethodInvoker)(() =>
                    {
                        undo();
                    }));
                    break;
                case (int)SocketCommand.NO:
                    MessageBox.Show("Đối thủ không đồng ý.");
                    break;
                case (int)SocketCommand.ENDGAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        EndGame();
                    }));
                    MessageBox.Show("Bạn đã thua!");
                    Lose();

                    break;
                case (int)SocketCommand.TiME_OUT:
                    MessageBox.Show("Bạn đã thắng!");
                    Win();
                    timerCoolDown.Stop();
                    break;
                case (int)SocketCommand.SEND_MESS:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        listView1.Items.Add(new ListViewItem(data.Message));
                        
                    }));
                    break;
                case (int)SocketCommand.SENDNAME:
                       if(socket.isServer == true)
                       {
                            ChessBoard.Players[1].Name = data.Message;
                            ChessBoard.Players[3].Name = data.Message;
                            socket.Send(new SocketData((int)SocketCommand.SENDNAME, Dang_nhap.title, new Point()));
                        }
                       else
                       {
                            ChessBoard.Players[0].Name = data.Message;
                            ChessBoard.Players[1].Name = Dang_nhap.title;
                            ChessBoard.Players[2].Name = data.Message;
                            ChessBoard.Players[3].Name = Dang_nhap.title;
                        this.Invoke((MethodInvoker)(() =>
                       {
                           ChessBoard.PlayerName.Text = Dang_nhap.title;
                        }));
                       }
                   
                    break;
                default:
                    break;
            }
            Listen();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {   
            socket.Send(new SocketData((int)SocketCommand.SEND_MESS,Dang_nhap.title+" : "+ txtmess.Text, new Point()));
            listView1.Items.Add(new ListViewItem(user+" : " + txtmess.Text));
            txtmess.Clear();
        }


        void Win()
        {
            quitcheck = false;
            string query = "update INFO set TONG+=1,WIN +=1,SCORE+=12 where TENTK = '"+Dang_nhap.title+"' ";
            using (SqlConnection conn = new SqlConnection(Connection.SeverConnection.stringConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query, conn);
                using (SqlDataReader dataReader = cmd2.ExecuteReader())
                {                   
                }
                conn.Close();
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtLAN_TextChanged(object sender, EventArgs e)
        {

        }

        void Lose()
        {
            quitcheck = false;
            string query = "update INFO set TONG+=1,LOSE +=1,SCORE-=12 where TENTK = '" + Dang_nhap.title + "' ";
            using (SqlConnection conn = new SqlConnection(Connection.SeverConnection.stringConnection))
            {
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query, conn);
                using (SqlDataReader dataReader = cmd2.ExecuteReader())
                {
                }
                conn.Close();
            }
        }
    }
}


