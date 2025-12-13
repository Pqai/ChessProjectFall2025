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
    public class King : ChessPiece
    {
        public King(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.King;
            Size = new Size(80, 80);
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = Math.Abs(target.X - Position.X);
            int dy = Math.Abs(target.Y - Position.Y);

            return dx <= 1 && dy <= 1 && (dx != 0 || dy != 0) && !board.IsSquareOccupiedByFriend(target, Color);
        }

 

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            var moves = new List<BoardPosition>();

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    var target = new BoardPosition(Position.X + dx, Position.Y + dy);
                    if (target.IsValid() && CanMoveTo(target, board))
                    {
                        moves.Add(target);
                    }
                }
            }
            return moves;
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //King Base
            Point BaseSlantLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 25);
            Point BaseSlantRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 25);
            Point baseWallLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 35);
            Point baseWallRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 35);

            //King body
            Point bottomBodyL = new Point(ScreenPosition.X - 16, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 16, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 8, ScreenPosition.Y + 5);
            Point topBodyR = new Point(ScreenPosition.X + 8, ScreenPosition.Y + 5);
            //weird bump before crown
            Point bumpLeft = new Point(ScreenPosition.X - 10, ScreenPosition.Y + 0);
            Point bumpRight = new Point(ScreenPosition.X + 10, ScreenPosition.Y + 0);
            Point bumpLeftBackToCenter = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 5);
            Point bumpRightBackToCenter = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 5);

            //King head
            Point headLeft = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 15);
            Point headRight = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 15);
            Point crownBaseL = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 15);//do
            Point crownBaseR = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 15);
            //Cross Drawing
            Point crossLI = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 23);//
            Point crossLO = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 23);
            Point crossLOU = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 28);
            Point crossLOR = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 28);
            Point crossLIU = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 31);
            Point crossMR = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 31);
            Point crossRD = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 28);
            Point crossROR = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 28);
            Point crossROD = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 23);
            Point crossRIL = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 23);

            Point[] king = new Point[]
            {
                baseWallLeft,
                BaseSlantLeft,
                bottomBodyL,
                topBodyL,
                bumpLeft,
                bumpLeftBackToCenter,
                headLeft,
                crownBaseL,

                crossLI,
                crossLO,

                crossLOU,
                crossLOR,

                crossLIU,
                crossMR,

                crossRD,
                crossROR,

                crossROD,
                crossRIL,

                crownBaseR,
                headRight,
                bumpRightBackToCenter,
                bumpRight,
                topBodyR,
                bottomBodyR,
                BaseSlantRight,
                baseWallRight
            };

            using (var brush = new SolidBrush(PieceColorValue))
            {
                e.Graphics.FillPolygon(brush, king);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, king);
            }
        }
    }
}
