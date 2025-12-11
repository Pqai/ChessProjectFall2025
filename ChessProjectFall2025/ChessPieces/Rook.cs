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
            Point topLeft = new Point(Center.X - 20, Center.Y - 25);
            Point topRight = new Point(Center.X + 20, Center.Y - 25);
            Point bottomRight = new Point(Center.X + 25, Center.Y + 20);
            Point bottomLeft = new Point(Center.X - 25, Center.Y + 20);

            //rook head
            Point bottomSlantLeft = new Point(Center.X - 25, Center.Y - 29);
            Point bottomSlantRight = new Point(Center.X + 25, Center.Y - 29);
            Point VerticalWallLeft = new Point(Center.X - 25, Center.Y - 40);
            Point VerticalWallRight = new Point(Center.X + 25, Center.Y - 40);
            Point crownLeft = new Point(Center.X - 16, Center.Y - 40);
            Point crownDipLeft = new Point(Center.X - 16, Center.Y - 35);
            Point crownRight = new Point(Center.X + 16, Center.Y - 40);
            Point crownDipRight = new Point(Center.X + 16, Center.Y - 35);
            Point CrownDipMiddleR = new Point(Center.X + 9, Center.Y - 35);
            Point CrownDipMiddleL = new Point(Center.X - 9, Center.Y - 35);
            Point CrownRiseMiddleR = new Point(Center.X + 9, Center.Y - 40);
            Point CrownRiseMiddleL = new Point(Center.X - 9, Center.Y - 40);

            //rook base
            Point BaseSlantLeft = new Point(Center.X - 30, Center.Y + 25);
            Point BaseSlantRight = new Point(Center.X + 30, Center.Y + 25);
            Point baseWallLeft = new Point(Center.X - 30, Center.Y + 30);
            Point baseWallRight = new Point(Center.X + 30, Center.Y + 30);

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
