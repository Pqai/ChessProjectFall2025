using ChessProjectFall2025.BaseGameLogic;
using ChessProjectFall2025.ChessPieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public class Pawn : ChessPiece
    {
        public override int MaxSteps => 2;
        public int Direction => Color == PieceColor.White ? 1 : -1;

        public Pawn(PieceColor color, BoardPosition position) : base(color, position)
        {
            Type = PieceType.Pawn;
            Size = new Size(40, 40);
            MaxSteps = 2;//only for its first move
        }

        public override bool CanMoveTo(BoardPosition target, ChessBoard board)
        {
            int dx = target.X - Position.X;
            int dy = target.Y - Position.Y;

            if (dx == 0)
            {
                //moving a square forward
                if (dy == Direction && !board.IsSquareOccupied(target))
                {
                    return !board.IsSquareOccupied(target);
                }

                //moving two forward
                if (!HasMoved && dy == (2 * Direction) && !board.IsSquareOccupied(target))
                {
                    var intermediate = new BoardPosition(Position.X, Position.Y + Direction);
                    
                    return !board.IsSquareOccupied(intermediate);
                }

                return false;
            }

            //moving diagonally to capture
            else if (Math.Abs(dx) == 1 && dy == Direction)
            {
                if (board.IsSquareOccupiedByOpponent(target, Color))
                    return true;
            }
            return false;
        }

        public override List<BoardPosition> GetValidMoves(ChessBoard board)
        {
            var moves = new List<BoardPosition>();

            var oneForward = new BoardPosition();

            if(oneForward.IsValid() && !board.IsSquareOccupied(oneForward))
            {
                moves.Add(oneForward);

                if(!HasMoved)
                {
                    var twoForward = new BoardPosition(Position.X, Position.Y + (2 * Direction));
                    if (!board.IsSquareOccupied(twoForward))
                    {
                        moves.Add(twoForward);
                    }
                }
            }

            int[] captureX = { Position.X - 1, Position.X + 1};
            foreach(int x in captureX)
            {
                var capturePos = new BoardPosition(x, Position.Y + Direction);
                if (capturePos.IsValid() && board.IsSquareOccupiedByOpponent(capturePos, Color))
                {
                    moves.Add(capturePos);
                }
            }
            return moves;        
        }

        public override void Draw(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //pawn Base
            Point bottomLeft = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 30);
            Point bottomRight = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 30);
            Point baseTopL = new Point(ScreenPosition.X - 20, ScreenPosition.Y + 25);
            Point baseTopR = new Point(ScreenPosition.X + 20, ScreenPosition.Y + 25);

            //pawn body
            Point bottomBodyL = new Point(ScreenPosition.X - 15, ScreenPosition.Y + 25);
            Point bottomBodyR = new Point(ScreenPosition.X + 15, ScreenPosition.Y + 25);
            Point topBodyL = new Point(ScreenPosition.X - 12, ScreenPosition.Y + 10);
            Point topBodyR = new Point(ScreenPosition.X + 12, ScreenPosition.Y + 10);

            //pawn head
            Point headLeft = new Point(ScreenPosition.X - 15, ScreenPosition.Y - 5);
            Point headRight = new Point(ScreenPosition.X + 15, ScreenPosition.Y - 5 );
            Point headTop = new Point(ScreenPosition.X + 0, ScreenPosition.Y - 10);

            Point[] pawn = new Point[]
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
                e.Graphics.FillPolygon(brush, pawn);
            }

            //outline
            using (var pen = new Pen(OutlineColor, 2))
            {
                e.Graphics.DrawPolygon(pen, pawn);
            }
        }
    }
}
