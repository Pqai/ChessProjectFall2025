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
    public class Rook : ChessPiece
    {
        public Rook(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.Rook;
            Size = new Size(70, 70);
            MovesStraight = true;
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = Math.Abs(target.X - Position.X);
            int dy = Math.Abs(target.Y - Position.Y);

            if(!((dx == 0 && dy > 0) || (dy == 0 && dx > 0)))
            {
                return false;
            }

            if (!IsPathClear(Position, target, board))
            {
                return false;
            }

            return !board.IsSquareOccupiedByFriend(target, Color);
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            return GetAllStraightMoves(board);
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //rook main body
            Point topLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y - 25);
            Point topRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y - 25);
            Point bottomRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 20);
            Point bottomLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 20);

            //rook head
            Point bottomSlantLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y - 29);
            Point bottomSlantRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y - 29);
            Point VerticalWallLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y - 40);
            Point VerticalWallRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y - 40);
            Point crownLeft = new Point(ScreenPosition.X - 16, ScreenPosition.Y - 40);
            Point crownDipLeft = new Point(ScreenPosition.X - 16, ScreenPosition.Y - 35);
            Point crownRight = new Point(ScreenPosition.X + 16, ScreenPosition.Y - 40);
            Point crownDipRight = new Point(ScreenPosition.X + 16, ScreenPosition.Y - 35);
            Point CrownDipMiddleR = new Point(ScreenPosition.X + 9, ScreenPosition.Y - 35);
            Point CrownDipMiddleL = new Point(ScreenPosition.X - 9, ScreenPosition.Y - 35);
            Point CrownRiseMiddleR = new Point(ScreenPosition.X + 9, ScreenPosition.Y - 40);
            Point CrownRiseMiddleL = new Point(ScreenPosition.X - 9, ScreenPosition.Y - 40);

            //rook base
            Point BaseSlantLeft = new Point(ScreenPosition.X - 30, ScreenPosition.Y + 25);
            Point BaseSlantRight = new Point(ScreenPosition.X + 30, ScreenPosition.Y + 25);
            Point baseWallLeft = new Point(ScreenPosition.X - 30, ScreenPosition.Y + 30);
            Point baseWallRight = new Point(ScreenPosition.X + 30, ScreenPosition.Y + 30);

            Point[] rook = new Point[]
            {
                bottomLeft,
                topLeft,
                bottomSlantLeft,
                VerticalWallLeft,
                crownLeft,
                crownDipLeft,
                CrownDipMiddleL,
                CrownRiseMiddleL,
                CrownRiseMiddleR,
                CrownDipMiddleR,
                crownDipRight,
                crownRight,
                VerticalWallRight,
                bottomSlantRight,
                topRight,
                bottomRight,
                BaseSlantRight,
                baseWallRight,
                baseWallLeft,
                BaseSlantLeft,
            };

            //body colour
            using (var brush = new SolidBrush(PieceColorValue))
            {
                e.Graphics.FillPolygon(brush, rook);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, rook);
            }
        }
    }
}
