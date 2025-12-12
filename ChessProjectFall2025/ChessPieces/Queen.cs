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
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 30);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 30);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);

            //Queen body
            Point bottomBodyL = new Point(ScreenPosition.X - 15, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 15, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 12, ScreenPosition.Y + 10);
            Point topBodyR = new Point(ScreenPosition.X + 12, ScreenPosition.Y + 10);

            //Queen head
            Point headLeft = new Point(ScreenPosition.X - 15, ScreenPosition.Y - 5);
            Point headRight = new Point(ScreenPosition.X + 15, ScreenPosition.Y - 5);
            Point headTop = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 10);

            Point[] queen = new Point[]
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
