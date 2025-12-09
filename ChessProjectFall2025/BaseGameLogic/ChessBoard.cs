using ChessProjectFall2025.BaseGameLogic;
using ChessProjectFall2025.ChessPieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProjectFall2025
{
    public class ChessBoard
    {
        private ChessPiece[,] squares = new ChessPiece[8, 8];
        //list of all moves to be added later
        private Point topLeftOfBoard = new Point(100, 100);//staring point of chess board
        private int squareSize = 110;
        public BaseGameLogic.PieceColor CurrentPlayer { get; private set; } = BaseGameLogic.PieceColor.White;

        public ChessBoard()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            //Clear board
            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++)
                    squares[x, y] = null;

            for (int x = 0; x < 8; x++)
            {
                squares[x, 1] = new Pawn(PieceColor.White, new BoardPosition(x, 1));
                squares[x, 6] = new Pawn(PieceColor.Black, new BoardPosition(x, 6));
            }

            //finish rest of pieces later
            squares[0, 0] = new Rook(PieceColor.White, new BoardPosition(0, 0));
            squares[1, 0] = new Knight(PieceColor.White, new BoardPosition(1, 0));

            UpdateAllPiecePositions();
        }

        private void DrawBoard(PaintEventArgs e)
        {
            var g = e.Graphics;

            for(int x =0; x < 8; x++)
            {
                for(int y =0; y < 8; y++)
                {
                    int screenX = topLeftOfBoard.X + (x * squareSize);
                    int screenY = topLeftOfBoard.Y + (y * squareSize);

                    var rect = new Rectangle(screenX, screenY, squareSize, squareSize);

                    //changing colours for the board
                    bool isAlternateBoardColour = (x + y) % 2 == 0;
                    Brush boardBrush = isAlternateBoardColour ? Brushes.Beige : Brushes.SaddleBrown;

                    g.FillRectangle(boardBrush, rect);//filled squares
                    g.DrawRectangle(Pens.Black, rect);//the outline of each square
                }
            }
        }

        private void UpdateAllPiecePositions()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = squares[x, y];
                    if (piece != null)
                    {
                        piece.UpdateScreenPosition(topLeftOfBoard, squareSize);
                    }
                }
            }
        }

        private void DrawPieces(PaintEventArgs e)//checks where pieces are and draws them (position tracker)
        {
            for(int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessPiece piece = squares[x, y];

                    if(piece != null)
                    {
                        int screenX = topLeftOfBoard.X + (x * squareSize) + (squareSize / 2);
                        int screenY = topLeftOfBoard.Y + (y * squareSize) + (squareSize / 2);

                        piece.UpdateScreenPosition(topLeftOfBoard, squareSize);

                        piece.Draw(e);
                    }
                }
            }
        } 

    }
}
