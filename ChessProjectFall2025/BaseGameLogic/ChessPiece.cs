using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public abstract class ChessPiece
    {
        public BoardPosition Position { get; protected set; }
        public PieceColor Color { get; private set; }
        public Point Center { get; protected set; }
        public bool HasMoved { get; protected set; }
        public Size Size { get; protected set; } = new Size(100, 100);
        public Point ScreenPosition { get; protected set; }
        public virtual int MaxSteps => 8; //change depending on piece 1 for king 8 for rook

        public abstract void Draw(PaintEventArgs e);
        public abstract bool CanMoveTo(BoardPosition target, ChessBoard board);
        public abstract List<BoardPosition> GetValidMoves(ChessBoard board);

        protected ChessPiece(PieceColor color, BoardPosition position)
        {
            Color = color;
            Position = position;
            HasMoved = false;
            UpdateScreenPosition(new Point(0, 0), 100); // Default values
        }

        public virtual void MoveTo(BoardPosition newPosition)
        {
            Position = newPosition;
            HasMoved = true;
        }

        public void UpdateScreenPosition(Point topLeftOfBoard, int squareSize)
        {
            ScreenPosition = new Point(
            topLeftOfBoard.X + (Position.X * squareSize) + (squareSize / 2),
            topLeftOfBoard.Y + (Position.Y * squareSize) + (squareSize / 2)
            );
        }

        public virtual void OnCapture()//general function for when piece is caught
        {

        }

        protected List<BoardPosition> GetMovesInDirection(int dx, int dy, ChessBoard board, int maxSteps = 8)
        {
            var moves = new List<BoardPosition>();
            int steps = 0;

            while (steps < maxSteps)
            {
                steps++;
                int newX = Position.X + (dx * steps);
                int newY = Position.Y + (dy * steps);

                if (!BoardPosition.IsValid(newX, newY))
                    break;

                var targetPos = new BoardPosition(newX, newY);

                // Can't move onto friendly piece
                if (board.IsSquareOccupiedByFriend(targetPos, Color))
                    break;

                // Add the move
                moves.Add(targetPos);

                // If we capture a piece, stop in this direction
                if (board.IsSquareOccupiedByOpponent(targetPos, Color))
                    break;
            }

            return moves;
        }
    }
}
