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
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 30);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 30);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);

            //King body
            Point bottomBodyL = new Point(ScreenPosition.X - 15, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 15, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 12, ScreenPosition.Y + 10);
            Point topBodyR = new Point(ScreenPosition.X + 12, ScreenPosition.Y + 10);

            //King head
            Point headLeft = new Point(ScreenPosition.X - 15, ScreenPosition.Y - 5);
            Point headRight = new Point(ScreenPosition.X + 15, ScreenPosition.Y - 5);
            Point headTop = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 10);

            Point[] king = new Point[]
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
