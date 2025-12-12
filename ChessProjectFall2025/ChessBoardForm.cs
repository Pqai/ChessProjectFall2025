using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public partial class ChessBoardForm : Form
    {
        private ChessBoard board;
        private BoardPosition? selectedPosition = null;
        private List<BoardPosition> validMoves = new List<BoardPosition>();

        public ChessBoardForm()
        {
            InitializeComponent();
            board = new ChessBoard();
            this.DoubleBuffered = true;
            this.Paint += ChessBoardForm_Paint;
            this.MouseClick += ChessBoardForm_MouseClick;
            this.Resize += ChessBoardForm_Resize;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ClientSize = new Size(8 * 60 + 100, 8 * 60 + 100);
        }

        private void ChessBoardForm_Paint(object sender, PaintEventArgs e)
        {
            // Clear background
            e.Graphics.Clear(Color.White);

            // Draw the chess board and pieces
            board.Draw(e, selectedPosition, validMoves);

            // Draw current player indicator
            DrawPlayerIndicator(e);
        }

        private void DrawPlayerIndicator(PaintEventArgs e)
        {
            string playerText = $"Current Player: {board.CurrentPlayer}";
            e.Graphics.DrawString(playerText,
                new Font("Arial", 12, FontStyle.Bold),
                Brushes.DarkBlue,
                10, 10);
        }

        private void ChessBoardForm_MouseClick(object sender, MouseEventArgs e)
        {
            var clickedPos = GetBoardPositionFromMouse(e.Location);

            if (clickedPos.HasValue)
            {
                selectedPosition = null;
                validMoves.Clear();
                this.Invalidate();
                return;
            }
            // Select a piece
            if (!selectedPosition.HasValue)
            {
                var piece = board.GetPieceAt(clickedPos.Value);
                if (piece != null && piece.Color == board.CurrentPlayer)
                {
                    selectedPosition = clickedPos.Value;
                    validMoves = board.GetValidMovesForPiece(clickedPos.Value);
                    this.Invalidate();
                }
            }
            else
            {
                // Clicking again to deselect
                if (clickedPos.Value == selectedPosition.Value)
                {
                    selectedPosition = null;
                    validMoves.Clear();
                    this.Invalidate();
                    return;
                }

                // Try to move the piece
                bool moved = board.MovePiece(selectedPosition.Value, clickedPos.Value);
                selectedPosition = null;
                validMoves.Clear();
                this.Invalidate();

                if (!moved)
                {
                    MessageBox.Show("Invalid move!", "Chess",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private BoardPosition? GetBoardPositionFromMouse(Point mousePos)
        { 
            return board.ScreenToBoardPosition(mousePos);
        }

        private void ChessBoardForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
