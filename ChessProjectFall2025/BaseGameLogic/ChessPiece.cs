using ChessProjectFall2025.BaseGameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrawingColor = System.Drawing.Color;

namespace ChessProjectFall2025
{
    public abstract class ChessPiece
    {
        public BoardPosition Position { get; protected set; }
        public PieceColor Color { get; private set; }
        public PieceType Type { get; protected set; }
        public bool HasMoved { get; protected set; }

        public Point Center { get; protected set; }
        public Size Size { get; protected set; } = new Size(100, 100);
        public Point ScreenPosition { get; protected set; }
        public Color PieceColorValue { get; protected set; }
        public Color OutlineColor { get; protected set; }

        public virtual bool CanJumpOverPieces => false;
        public virtual bool MovesDiagonally => false;
        public virtual bool MovesStraight => false;
        public virtual int MaxSteps { get; set; } = 8; //change depending on piece 1 for king 8 for rook

        protected virtual Color WhitePieceColor => DrawingColor.Ivory;
        protected virtual Color WhiteOutlineColor => DrawingColor.DarkGray;
        protected virtual Color BlackPieceColor => DrawingColor.DarkSlateGray;
        protected virtual Color BlackOutlineColor => DrawingColor.Black;

        protected ChessPiece(PieceColor color, BoardPosition position)
        {
            Color = color;
            Position = position;
            HasMoved = false;
            PieceColorValue = color == PieceColor.White ? WhitePieceColor : BlackPieceColor;
            OutlineColor = color == PieceColor.White ? WhiteOutlineColor : BlackOutlineColor;
            //UpdateScreenPosition(new Point(0, 0), 100); // Default values
        }

        public abstract void Draw(PaintEventArgs e);
        public abstract bool CanMoveTo(BoardPosition target, ChessBoard board);
        public abstract List<BoardPosition> GetValidMoves(ChessBoard board);


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
            //call the special capture from item and ability.cs
            /*if () finish soon once i start on abilities and items
            {

            }*/
        }

        protected bool IsPathClear(BoardPosition from, BoardPosition to, ChessBoard board)
        {
            if (CanJumpOverPieces) return true;

            int dx = Math.Sign(to.X - from.X);
            int dy = Math.Sign(to.Y - from.Y);

            int currentX = from.X + dx;
            int currentY = from.Y + dy;

            while (currentX != to.X || currentY != to.Y)
            {
                var currentPos = new BoardPosition(currentX, currentY);
                if (board.IsSquareOccupied(currentPos))
                    return false;

                currentX += dx;
                currentY += dy;
            }

            return true;
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

        protected List<BoardPosition> GetAllDiagonalMoves(ChessBoard board, int maxSteps = 8)
        {
            var moves = new List<BoardPosition>();

            // All 4 diagonal directions
            int[,] directions = { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

            for (int i = 0; i < 4; i++)
            {
                moves.AddRange(GetMovesInDirection(directions[i, 0], directions[i, 1], board, maxSteps));
            }

            return moves;
        }

        protected List<BoardPosition> GetAllStraightMoves(ChessBoard board, int maxSteps = 8)
        {
            var moves = new List<BoardPosition>();

            // All 4 straight directions
            int[,] directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

            for (int i = 0; i < 4; i++)
            {
                moves.AddRange(GetMovesInDirection(directions[i, 0], directions[i, 1], board, maxSteps));
            }

            return moves;
        }
    }
}
