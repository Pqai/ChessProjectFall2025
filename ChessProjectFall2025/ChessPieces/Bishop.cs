using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
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
            int dx = Math.Abs(target.X - Position.X);
            int dy = Math.Abs(target.Y - Position.Y);

            if(dx != dy || dx == 0)
            {
                return false;
            }

            if(!IsPathClear(Position, target, board))
            {
                return false;
            }

            return !board.IsSquareOccupiedByFriend(target, Color);
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            return GetAllDiagonalMoves(board);
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //Bishop Base
            Point BaseSlantLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 25);
            Point BaseSlantRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 25);
            Point baseWallLeft = new Point(ScreenPosition.X - 25, ScreenPosition.Y + 35);
            Point baseWallRight = new Point(ScreenPosition.X + 25, ScreenPosition.Y + 35);

            //Bishop body
            Point bottomBodyL = new Point(ScreenPosition.X - 16, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 16, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 8, ScreenPosition.Y + 5);
            Point topBodyR = new Point(ScreenPosition.X + 8, ScreenPosition.Y + 5);
            //weird bump before crown
            Point bumpLeft = new Point(ScreenPosition.X - 10, ScreenPosition.Y + 0);
            Point bumpRight = new Point(ScreenPosition.X + 10, ScreenPosition.Y + 0);
            Point bumpLeftBackToCenter = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 5);
            Point bumpRightBackToCenter = new Point(ScreenPosition.X + 8, ScreenPosition.Y - 5);

            //Bishop head
            Point headLeft = new Point(ScreenPosition.X - 14, ScreenPosition.Y - 10);
            Point headLeftHip = new Point(ScreenPosition.X - 12, ScreenPosition.Y - 15);
            Point headCutLeft = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 16);
            Point headCutBL = new Point(ScreenPosition.X - 3, ScreenPosition.Y - 11);
            Point headCutBR = new Point(ScreenPosition.X - 1, ScreenPosition.Y - 13);
            Point headCutRight = new Point(ScreenPosition.X - 6, ScreenPosition.Y - 20);
            Point topHead = new Point(ScreenPosition.X - 0, ScreenPosition.Y - 28);
            Point headRightHip = new Point(ScreenPosition.X + 12, ScreenPosition.Y - 15);
            Point headRight = new Point(ScreenPosition.X + 14, ScreenPosition.Y - 10);

            Point[] bishop = new Point[]
            {
                baseWallLeft,
                BaseSlantLeft,
                bottomBodyL,
                topBodyL,
                bumpLeft,
                bumpLeftBackToCenter,
                headLeft,
                headLeftHip,
                headCutLeft,
                headCutBL,
                headCutBR,
                headCutRight,
                topHead,
                headRightHip,
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
                e.Graphics.FillPolygon(brush, bishop);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, bishop);
            }
        }
    }
}
