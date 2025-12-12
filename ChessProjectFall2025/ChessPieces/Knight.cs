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
            bool isKnightMove = (dx == 2 && dy == 1) || (dx == 1 && dy == 2);

            if (!isKnightMove)
            {
                return false;
            }
            return !board.IsSquareOccupiedByFriend(target, Color);
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            var moves = new List<BoardPosition>();

            int[] dxArray = { 2, 2, -2, -2, 1, 1, -1, -1 };
            int[] dyArray = { 1, -1, 1, -1, 2, -2, 2, -2 };

            for (int i = 0; i < 8; i++)
            {
                var target = new BoardPosition(Position.X + dxArray[i], Position.Y + dyArray[i]);
                if (target.IsValid() && CanMoveTo(target, board))
                {
                    moves.Add(target);
                }
            }
            return moves;
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //knight Base
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 5);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 5);
            Point baseTopAnchorL = new Point(ScreenPosition.X - 10, ScreenPosition.Y + 5);
            Point baseTopAnchorR = new Point(ScreenPosition.X + 10, ScreenPosition.Y + 5);

            //knight body
            Point leftChestPeak = new Point(ScreenPosition.X - 22, ScreenPosition.Y - 10);//
            Point leftChestCollar = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 15);//
            Point leftNeckValley = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 20);//
            Point leftMandibleConnector = new Point(ScreenPosition.X - 14, ScreenPosition.Y - 23);//
            Point leftMandibleCorner = new Point(ScreenPosition.X - 17, ScreenPosition.Y - 20);//
            Point leftMandibleChin = new Point(ScreenPosition.X - 19, ScreenPosition.Y - 18);//
            Point leftSnoutTop = new Point(ScreenPosition.X - 21, ScreenPosition.Y - 27);//
            Point leftForehead = new Point(ScreenPosition.X - 8, ScreenPosition.Y - 30);//
            Point leftEar = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 37);//
            Point leftEarStartOfHair = new Point(ScreenPosition.X + 3, ScreenPosition.Y - 30);//
            Point rightHairPointOne= new Point(ScreenPosition.X + 12, ScreenPosition.Y - 25);//
            Point rightHairPointTwo = new Point(ScreenPosition.X + 18, ScreenPosition.Y - 10);//
            Point rightHairPointThree = new Point(ScreenPosition.X + 16, ScreenPosition.Y - 0);//

            Point[] knight = new Point[]
            {
                bottomLeft,
                baseTopL,
                baseTopAnchorL,
                leftChestPeak,
                leftChestCollar,
                leftNeckValley,
                leftMandibleConnector,
                leftMandibleCorner,
                leftMandibleChin,
                leftSnoutTop,
                leftForehead,
                leftEar,
                leftEarStartOfHair,
                rightHairPointOne,
                rightHairPointTwo,
                rightHairPointThree,
                baseTopAnchorR,
                baseTopR,
                bottomRight
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
