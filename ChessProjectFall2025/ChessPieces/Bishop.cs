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
    public class Bishop : ChessPiece
    {
        public Bishop(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.Bishop;
            Size = new Size(70, 70);
            MovesDiagonally = true;
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            throw new NotImplementedException();
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Bishop Base
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 30);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 30);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);

            //Bishop body
            Point bottomBodyL = new Point(ScreenPosition.X - 15, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 15, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 12, ScreenPosition.Y + 10);
            Point topBodyR = new Point(ScreenPosition.X + 12, ScreenPosition.Y + 10);

            //Bishop head
            Point headLeft = new Point(ScreenPosition.X - 15, ScreenPosition.Y - 5);
            Point headRight = new Point(ScreenPosition.X + 15, ScreenPosition.Y - 5);
            Point headTop = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 10);

            Point[] bishop = new Point[]
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
                e.Graphics.FillPolygon(brush, bishop);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, bishop);
            }
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
