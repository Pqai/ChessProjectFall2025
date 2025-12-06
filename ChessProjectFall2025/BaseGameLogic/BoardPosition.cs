using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectFall2025
{
    public struct BoardPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator == (BoardPosition a, BoardPosition b)=> a.X == b.X && a.Y == b.Y;

        public static bool operator !=(BoardPosition a, BoardPosition b) => !(a == b);
    }
}
