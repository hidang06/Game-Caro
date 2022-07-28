using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Tạo_Bàn_Cờ
{
    public class PlayerInfo
    {
        private Point point;
        public Point Point { get => point; set => point = value; }

        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value;}
        public PlayerInfo(Point point, int currentPlayer)
        {
            this.point = point;
            this.currentPlayer = currentPlayer;
        }    
    }
}
