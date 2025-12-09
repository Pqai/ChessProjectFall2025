using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025.ChessPieces
{
    public class Knight : ChessPiece
    {
        public Knight(PieceColor color, BoardPosition position) : base(color, position)
        {
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            throw new NotImplementedException();
        }

        public override void Draw(PaintEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
