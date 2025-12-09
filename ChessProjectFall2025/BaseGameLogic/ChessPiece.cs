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
        public abstract void Draw(PaintEventArgs e);
        public abstract bool CanMoveTo(BoardPosition target, ChessBoard board);

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


    }
}
