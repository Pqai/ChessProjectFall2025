using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public abstract class ChessPiece
    {
        public BoardPosition Position { get; protected set; }
        public Point Center { get; set; }
        public virtual int MoveX { get; set; }
        public virtual int MoveY { get; set; }

        public abstract void Draw(PaintEventArgs e);

        public virtual void Move(int X1, int X2, int Y1, int Y2)
        {

        }
    }
}
