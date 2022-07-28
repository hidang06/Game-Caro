using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Tạo_Bàn_Cờ
{

    public class player
    {
        private string name;

        public string Name { get => name; set => name = value; }
       

        private Image mark;
        public Image Mark { get => mark; set => mark = value; }
        public player(string name, Image mark)
        {
            this.Name = name;
            this.Mark = mark;
        }
    
    }
}
