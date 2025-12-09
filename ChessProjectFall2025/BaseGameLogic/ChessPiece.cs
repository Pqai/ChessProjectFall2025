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
        public abstract void Draw(PaintEventArgs e);
        public abstract bool CanMoveTo(BoardPosition target, ChessBoard board);
        public Point ScreenPosition { get; protected set; }

        public enum PieceColor
        {
            White,
            Black
        }

        public enum PieceType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn
        }

        protected ChessPiece(PieceColor color, BoardPosition position)
        {
            Color = color;
            Position = position;
            HasMoved = false;
            UpdateScreenPosition(new Point(0, 0), 60); // Default values
        }

        public virtual void MoveTo(BoardPosition newPosition)
        {
            Position = newPosition;
            HasMoved = true;
        }

        public void UpdateScreenPosition()
        {

        }


    }
}
