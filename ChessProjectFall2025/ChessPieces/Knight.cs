using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Graphics g = e.Graphics;

            //pawn Base
            Point bottomLeft = new Point(Center.X - 20, Center.Y + 30);
            Point bottomRight = new Point(Center.X + 20, Center.Y + 30);
            Point baseTopL = new Point(Center.X - 20, Center.Y + 25);
            Point baseTopR = new Point(Center.X + 20, Center.Y + 25);

            //pawn body
            Point bottomBodyL = new Point(Center.X - 15, Center.Y + 25);
            Point bottomBodyR = new Point(Center.X + 15, Center.Y + 25);
            Point topBodyL = new Point(Center.X - 12, Center.Y + 10);
            Point topBodyR = new Point(Center.X + 12, Center.Y + 10);

            //pawn head
            Point headLeft = new Point(Center.X - 15, Center.Y - 5);
            Point headRight = new Point(Center.X + 15, Center.Y - 5);
            Point headTop = new Point(Center.X + 0, Center.Y - 10);

            Point[] pawn = new Point[]
            {
                bottomLeft,
                baseTopL,
                bottomBodyL,
                topBodyL,
                headLeft,
                headTop,
                headRight,
                topBodyR,
                bottomBodyR,
                baseTopR,
                bottomRight,
            };

            using (var brush = new SolidBrush(PieceColorValue))
            {
                e.Graphics.FillPolygon(brush, pawn);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, pawn);
            }
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
