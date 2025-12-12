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
            Type = PieceType.Knight;
            Size = new Size(70, 70);
            CanJumpOverPieces = true;
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = Math.Abs(target.X - Position.X);
            int dy = Math.Abs(target.Y - Position.Y);

            //two spots in x, one spot in y OR two spots in y, one spot in x
            if (!((dx == 2 && dy == 1) || (dx == 1 && dy == 2)))
            {
                return false;
            }
            return !board.IsSquareOccupiedByFriend(target, Color);
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            throw new NotImplementedException();
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //knight Base
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 30);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 30);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);

            //knight body
            Point bottomBodyL = new Point(ScreenPosition.X - 15, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 15, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 12, ScreenPosition.Y + 10);
            Point topBodyR = new Point(ScreenPosition.X + 12, ScreenPosition.Y + 10);

            //knight head
            Point headLeft = new Point(ScreenPosition.X - 15, ScreenPosition.Y - 5);
            Point headRight = new Point(ScreenPosition.X + 15, ScreenPosition.Y - 5);
            Point headTop = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 10);

            Point[] knight = new Point[]
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
                e.Graphics.FillPolygon(brush, knight);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, knight);
            }
        }
    }
}
