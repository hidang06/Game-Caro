using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tạo_Bàn_Cờ
{
    [Serializable]
    class SocketData
    {
        private int command;
        private Point point;
        private String message;

        public int Command { get => command; set => command = value; }
        public Point Point { get => point; set => point = value; }
        public string Message { get => message; set => message = value; }

        public SocketData(int command,string message,Point point)
        {
            this.Command = command;
            this.Message = message;
            this.Point = point;
        }
        public enum SocketCommand
        {
            SEND_POINT,
            NOTIFY,
            NEWGAME,
            ENDGAME,
            TiME_OUT,
            SEND_MESS,
            UNDO,
            SENDNAME,
            QUIT,
            YES,
            NO
        }
    }
}
