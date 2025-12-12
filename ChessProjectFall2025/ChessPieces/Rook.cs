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

        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            throw new NotImplementedException();
        }

        public override void Draw(PaintEventArgs e)
        {
            //Pen pen = new Pen(Color.Yellow, 2);
            //Color ChessPiece = Color.Yellow;
            Graphics g = e.Graphics;

            //to fix later
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

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            throw new NotImplementedException();
        }
    }
}
