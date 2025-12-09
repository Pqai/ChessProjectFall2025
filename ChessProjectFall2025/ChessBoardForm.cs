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

        public ChessBoardForm()
        {
            InitializeComponent();
            board = new ChessBoard();
            this.DoubleBuffered = true;
            this.Paint += ChessBoardForm_Paint;
            this.MouseClick += ChessBoardForm_MouseClick;
        }

        private void ChessBoardForm_Paint(object sender, PaintEventArgs e)
        {
            // Clear background
            e.Graphics.Clear(Color.White);

            // Draw the chess board and pieces
            board.Draw(e);

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
            if (!clickedPos.HasValue) return;

            if (!selectedPosition.HasValue)
            {
                // Select a piece
                var piece = board.GetPieceAt(clickedPos.Value);
                if (piece != null && piece.Color == board.CurrentPlayer)
                {
                    selectedPosition = clickedPos.Value;
                    this.Invalidate(); // Trigger redraw
                }
            }
            else
            {
                // Try to move
                bool moved = board.MovePiece(selectedPosition.Value, clickedPos.Value);
                selectedPosition = null;
                this.Invalidate(); // Trigger redraw

                if (!moved)
                {
                    MessageBox.Show("Invalid move!", "Chess",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private BoardPosition? GetBoardPositionFromMouse(Point mousePos)
        {
            // Convert screen coordinates to board coordinates
            // You'll need to know your board's position and square size
            // This depends on how you implement ChessBoard.Draw()
            return null; // Implement based on your board layout
        }
    }
}
