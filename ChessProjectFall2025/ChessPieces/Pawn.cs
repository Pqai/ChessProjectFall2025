using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public class Pawn : ChessPiece
    {
        public override int MaxSteps => 2;
        public int Direction => Color == PieceColor.White ? 1 : -1;
        private int StartingRank => Color == PieceColor.White ? 1 : 6;

        public Pawn(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.Pawn;
            Size = new Size(40, 40);
            MaxSteps = 2;//only for its first move
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = target.X - BoardPosition.X;
            int dy = target.Y - BoardPosition.Y;
        }

        public override void Draw(PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            var moves = new List<BoardPosition>();

            var oneForward = new BoardPosition();
        }
    }
}
