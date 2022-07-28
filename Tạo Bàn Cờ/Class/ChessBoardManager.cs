using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tạo_Bàn_Cờ
{

    public class ChessBoardManager
    {

        private List<player> players;
        private int currentplayer;
        private TextBox playerName;
        private PictureBox playermark;
        private Panel Chessboard;
        private List<List<Button>>matrix;
        private Stack<PlayerInfo> timeline;
        public List<player> Players { get => players; set => players = value; }
        public int Currentplayer { get => currentplayer; set => currentplayer = value; }
        public TextBox PlayerName { get => playerName; set => playerName = value; }
        public Panel ChessBoard { get => Chessboard; set => Chessboard = value; }
        public PictureBox Playermark { get => playermark; set => playermark = value; }
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; } ///
        public Stack<PlayerInfo> Timeline { get => timeline; set => timeline = value; } ///
        private event EventHandler<ButtonclickEvent> playerMark;
        public event EventHandler<ButtonclickEvent> PlayerMark
        {
            add
            {
                playerMark += value;
            }
            remove
            {
                playerMark -= value;
            }
        }

        private event EventHandler endGame;
        public event EventHandler Endgame
        {
            add
            {
                endGame += value;
            }
            remove
            {
                endGame += value;
            }
        }

        public void DrawChessBoard(Panel chessboard, TextBox playerName, PictureBox mark)
        {
            this.playerName = playerName;
            this.playermark = mark;
            this.Chessboard = chessboard;

            chessboard.Controls.Clear();    ///clear bàn cờ -> làm mới
            
            timeline = new Stack<PlayerInfo>();
            this.players = new List<player>()
            {
                new player(Dang_nhap.title,Image.FromFile(Application.StartupPath+"\\Resources\\i.png")),
                new player("",Image.FromFile(Application.StartupPath+"\\Resources\\O.png")),
                new player(Dang_nhap.title,Image.FromFile(Application.StartupPath+"\\Resources\\x.png")),
                new player("",Image.FromFile(Application.StartupPath+"\\Resources\\blackcircle.png")),
            };
            Currentplayer = Currentplayer == 0 ? 0 : 2;
            changeplayer();

            Button oldbutton = new Button() { Width = 0, Location = new Point(0, 0) };
            matrix = new List<List<Button>>();     //tạo ma trận tọa độ bàn cờ
            for (int i = 0; i < Cons.DRAW_CHESS_HEIGHT; i++)
            {
                matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.DRAW_CHESS_WIDTH; j++)
                {
                    Button bnt = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HIEGHT,
                        Location = new Point(oldbutton.Location.X + Cons.CHESS_WIDTH, oldbutton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString(), //xác định vị trí button theo hàng 
                        
                    };
                    bnt.Click += Bnt_Click;
                    matrix[i].Add(bnt); ///
                    chessboard.Controls.Add(bnt);
                    oldbutton = bnt;

                }
                oldbutton = new Button() { Location = new Point(0, oldbutton.Location.Y + Cons.CHESS_HIEGHT) };
            }
        }
        private Point Getbuttonaxis(Button bnt)         ///lấy giá trị tọa độ nút
        {
            int doc = Convert.ToInt32(bnt.Tag);
            int ngang = matrix[doc].IndexOf(bnt);
            Point button = new Point(ngang, doc);
            return button;
        }
        private void Mark(Button bnt)
        {
            bnt.BackgroundImage = players[currentplayer].Mark;
            bkgrd_btn();
            //currentplayer = currentplayer == 1 ? 0 : 1;
        }
        void bkgrd_btn()
        {
            switch (currentplayer)
            {
                case 0: case 1:
                    currentplayer = currentplayer == 1 ? 0 : 1;
                    break;
                case 2: case 3:
                    currentplayer = currentplayer == 3 ? 2 : 3;
                    break;
            }    
        }
        void bkgrd_btn1(PlayerInfo oldpos)
        {
            oldpos = timeline.Peek();
            switch (currentplayer)
            {
                case 0:
                case 1:
                    currentplayer = oldpos.CurrentPlayer == 1 ? 0 : 1;
                    break;
                case 2:
                case 3:
                    currentplayer = oldpos.CurrentPlayer == 3 ? 2 : 3;
                    break;
            }
        }

        private void Bnt_Click(object sender, EventArgs e)
        {
            
            Button bnt = sender as Button;
           
            if (bnt.BackgroundImage != null)
                return;
            Chessboard.Enabled = true;
            Mark(bnt);
            Timeline.Push(new PlayerInfo(Getbuttonaxis(bnt), Currentplayer)); ///
            changeplayer();
            if (playerMark != null)
                playerMark(this, new ButtonclickEvent(Getbuttonaxis(bnt)));

            if (isEndGame(bnt))
            {
                EndGame();
            }
        }

        public void OtherplayerMark(Point point)
        {

            Button bnt = matrix[point.Y][point.X];

            if (bnt.BackgroundImage != null)
                return;
            Chessboard.Enabled = true;

            Mark(bnt);
            Timeline.Push(new PlayerInfo(Getbuttonaxis(bnt), Currentplayer)); ///
            changeplayer();
        }

        #region xulythangthua
        public void EndGame()
        {
            if (endGame != null)
                endGame(this, new EventArgs());
        }
        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimaryDiagonal(btn) || isEndSubDiagonal(btn);
        }
        private bool isEndHorizontal(Button btn)
        {
            Point point = Getbuttonaxis(btn);
            int countLeft = 0;
            int RowPos = point.Y;
            int ColPos = point.X;
            for (int i = point.X; i >= 0; i--)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;

                }
                else break;

            }
            int countRight = 0;
            for (int i = point.X + 1; i < Cons.DRAW_CHESS_WIDTH; i++)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;

                }
                else
                    break;

            }
            int counthorizon = countLeft + countRight;
            if (counthorizon >= 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (ColPos - j >= 0) // Kiểm tra các phần tử bên trái
                        if (matrix[RowPos][ColPos - j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos][ColPos - j].BackColor = Color.PeachPuff;

                    if (ColPos + j < Cons.DRAW_CHESS_WIDTH) // Kiểm tra các phần tử bên phải
                        if (matrix[RowPos][ColPos + j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos][ColPos + j].BackColor = Color.PeachPuff;
                }
            }
            return counthorizon >= 5;

        }
        private bool isEndVertical(Button btn)
        {
            Point point = Getbuttonaxis(btn);
            int countTop = 0;
            int RowPos = point.Y;
            int ColPos = point.X;
            for (int i = point.Y; i >= 0; i--)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;

                }
                else break;
            }
            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.DRAW_CHESS_HEIGHT; i++)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;

                }
                else
                    break;
            }
            int countvertical = countTop + countBottom;
            if (countvertical >= 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (RowPos - j >= 0)  // Kiểm tra các phần tử phía trên
                        if (matrix[RowPos - j][ColPos].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos - j][ColPos].BackColor = Color.PeachPuff;

                    if (RowPos + j < Cons.DRAW_CHESS_HEIGHT)  // Kiểm tra các phần tử phía dưới
                        if (matrix[RowPos + j][ColPos].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos + j][ColPos].BackColor = Color.PeachPuff;
                }
            }
            return countvertical >= 5;
        }
        private bool isEndPrimaryDiagonal(Button btn)
        {
            Point point = Getbuttonaxis(btn);
            int countTop = 0;
            int RowPos = point.Y;
            int ColPos = point.X;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;
                if (matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                  
                }
                else break;
            }
            int countBottom = 0;
            for (int i = 1; i <= Cons.DRAW_CHESS_WIDTH-point.X; i++)
            {
                if (point.Y + i >= Cons.DRAW_CHESS_HEIGHT || point.X + i >= Cons.DRAW_CHESS_WIDTH)
                    break;
                if (matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                  
                }
                else
                    break;
            }

            int countdiag = countTop + countBottom;
            if (countdiag >= 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (RowPos - j >= 0 && ColPos - j >= 0) // Kiểm tra các phần tử chéo trên
                        if (matrix[RowPos - j][ColPos - j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos - j][ColPos - j].BackColor = Color.PeachPuff;

                    if (RowPos + j < Cons.CHESS_HIEGHT && ColPos + j < Cons.CHESS_WIDTH) // Kiểm tra các phần tử chéo dưới
                        if (matrix[RowPos + j][ColPos + j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos + j][ColPos + j].BackColor = Color.PeachPuff;
                }

            }
            return countdiag >= 5;


        }
        private bool isEndSubDiagonal(Button btn)
        {
            Point point = Getbuttonaxis(btn);
            int countTop = 0;
            int RowPos = point.Y;
            int ColPos = point.X;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i >= Cons.DRAW_CHESS_WIDTH || point.Y - i < 0)
                    break;
                if (matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                
                }
                else break;
            }
            int countBottom = 0;
            for (int i = 1; i <= Cons.DRAW_CHESS_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.DRAW_CHESS_HEIGHT || point.X - i < 0)
                    break;
                if (matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
              
                }
                else
                    break;
            }
            int countdiag = countTop + countBottom;
            if (countdiag >= 5)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (RowPos - j >= 0 && ColPos + j <=Cons.CHESS_WIDTH) // Kiểm tra các phần tử chéo trên
                        if (matrix[RowPos - j][ColPos + j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos - j][ColPos + j].BackColor = Color.PeachPuff;

                    if (RowPos + j < Cons.CHESS_HIEGHT && ColPos - j >= 0) // Kiểm tra các phần tử chéo dưới
                        if (matrix[RowPos + j][ColPos - j].BackgroundImage == btn.BackgroundImage)
                            matrix[RowPos + j][ColPos - j].BackColor = Color.PeachPuff;
                }

            }
            return countdiag >= 5;
        }
        #endregion

        void changeplayer()
        {
            playerName.Text = players[currentplayer].Name;
            playermark.Image = players[currentplayer].Mark;
        }
        public bool Undo() ////
        {
            if (timeline.Count <= 2)
            {
                return false;
            }
            PlayerInfo oldpos = timeline.Pop();
            Button btn = matrix[oldpos.Point.Y][oldpos.Point.X];
            btn.BackgroundImage = null;
            if (timeline.Count > 1)
            {
                if (Main.k == 0)
                {
                    Currentplayer = Currentplayer == 0 ? 0 : 1;
                }
                else if (Main.k==2)
                {
                    Currentplayer = Currentplayer == 2 ? 2 : 3;
                }    
            }
            else
            {
                oldpos = timeline.Peek();
                bkgrd_btn();
                //currentplayer = currentplayer == 1 ? 0 : 1;
            }
            changeplayer();
            return true;
        }
    }

    public class ButtonclickEvent : EventArgs
    {
        private Point ClickedPoint;

        public Point ClickedPoint1 { get => ClickedPoint; set => ClickedPoint = value; }
        public ButtonclickEvent(Point point)
        {
            this.ClickedPoint = point;
        }

    }
}
