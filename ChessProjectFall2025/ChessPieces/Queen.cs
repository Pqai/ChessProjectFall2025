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
    public class Queen : ChessPiece
    {
        public Queen(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.Pawn;
            Size = new Size(80, 80);
            MovesDiagonally = true;
            MovesStraight = true;
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = Math.Abs(target.X - Position.X);
            int dy = Math.Abs(target.Y - Position.Y);

            bool isStraightMove = (dx == 0 && dy > 0) || (dy == 0 && dx > 0);
            bool isDiagonalMove = dx == dy && dx > 0;

            if (!isStraightMove && !isDiagonalMove)
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
            var moves = new List<BoardPosition>();

            //this is basically combining rook and bishop
            moves.AddRange(GetAllDiagonalMoves(board));
            moves.AddRange(GetAllStraightMoves(board));

            return moves;
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Queen Base
            Point BaseSlantLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 25);
            Point BaseSlantRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 25);
            Point baseWallLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 35);
            Point baseWallRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 35);

            //Queen body
            Point bottomBodyL = new Point(ScreenPosition.X - 16, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 16, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 8, ScreenPosition.Y + 5);
            Point topBodyR = new Point(ScreenPosition.X + 8, ScreenPosition.Y + 5);
            //weird bump before crown
            Point bumpLeft = new Point(ScreenPosition.X - 10, ScreenPosition.Y + 0);
            Point bumpRight = new Point(ScreenPosition.X + 10, ScreenPosition.Y + 0);
            Point bumpLeftBackToCenter = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 5);
            Point bumpRightBackToCenter = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 5);

            //Queen head
            Point headLeft = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 15);
            Point headRight = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 15);
            Point crownBaseL = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 15);//do
            Point crownBaseR = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 15);
            //Crown Drawing
            Point crossLI = new Point(ScreenPosition.X - 12, ScreenPosition.Y - 23);//
            Point crossLO = new Point(ScreenPosition.X - 14, ScreenPosition.Y - 28);
            Point crossLOU = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 30);
            Point crossLOR = new Point(ScreenPosition.X - 4, ScreenPosition.Y - 28);
            Point crossLIU = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 20);
            Point crossMR = new Point(ScreenPosition.X + 4, ScreenPosition.Y - 28);
            Point crossRD = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 30);
            Point crossROR = new Point(ScreenPosition.X + 14, ScreenPosition.Y - 28);
            Point crossROD = new Point(ScreenPosition.X + 12, ScreenPosition.Y - 23);

            Point[] queen = new Point[]
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
                e.Graphics.FillPolygon(brush, queen);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, queen);
            }
        }
    }
}
