using ChessProjectFall2025.BaseGameLogic;
using ChessProjectFall2025.ChessPieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace ChessProjectFall2025
{
    public class ChessBoard
    {
        private ChessPiece[,] squares = new ChessPiece[8, 8];
        private List<Move> moveHistory = new List<Move>();//the move history list
        private Point topLeftOfBoard = new Point(100, 100);//staring point of chess board
        private int squareSize = 110;
        private Brush darkSquareBrush = Brushes.SaddleBrown;
        private Brush lightSquareBrush = Brushes.Beige;
        private Brush selectedSquareBrush = new SolidBrush(Color.FromArgb(200, Color.Yellow));
        public PieceColor CurrentPlayer { get; private set; } = PieceColor.White;
        public GameState State { get; private set; } = GameState.InProgress;


        public ChessBoard()
        {
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            ClearBoard();

            moveHistory.Clear();

            CurrentPlayer = PieceColor.White;
            State = GameState.InProgress;

            SetupPawns();

            SetupPieces();

            UpdateAllPiecePositions();

            OnGameStateChanged(new GameStateChangedEventArgs { NewState = State });
        }

        private void ClearBoard()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    squares[x, y] = null;
                }
            }
        }

        private void SetupPawns()
        {
            // White pawns (rank 2)
            for (int x = 0; x < 8; x++)
            {
                squares[x, 1] = new Pawn(PieceColor.White, new BoardPosition(x, 1));
            }

            // Black pawns (rank 7)
            for (int x = 0; x < 8; x++)
            {
                squares[x, 6] = new Pawn(PieceColor.Black, new BoardPosition(x, 6));
            }
        }

        private void SetupPieces()
        {
            // White pieces (rank 1)
            squares[0, 0] = new Rook(PieceColor.White, new BoardPosition(0, 0));
            squares[1, 0] = new Knight(PieceColor.White, new BoardPosition(1, 0));
            squares[2, 0] = new Bishop(PieceColor.White, new BoardPosition(2, 0));
            squares[3, 0] = new Queen(PieceColor.White, new BoardPosition(3, 0));
            squares[4, 0] = new King(PieceColor.White, new BoardPosition(4, 0));
            squares[5, 0] = new Bishop(PieceColor.White, new BoardPosition(5, 0));
            squares[6, 0] = new Knight(PieceColor.White, new BoardPosition(6, 0));
            squares[7, 0] = new Rook(PieceColor.White, new BoardPosition(7, 0));

            // Black pieces (rank 8)
            squares[0, 7] = new Rook(PieceColor.Black, new BoardPosition(0, 7));
            squares[1, 7] = new Knight(PieceColor.Black, new BoardPosition(1, 7));
            squares[2, 7] = new Bishop(PieceColor.Black, new BoardPosition(2, 7));
            squares[3, 7] = new Queen(PieceColor.Black, new BoardPosition(3, 7));
            squares[4, 7] = new King(PieceColor.Black, new BoardPosition(4, 7));
            squares[5, 7] = new Bishop(PieceColor.Black, new BoardPosition(5, 7));
            squares[6, 7] = new Knight(PieceColor.Black, new BoardPosition(6, 7));
            squares[7, 7] = new Rook(PieceColor.Black, new BoardPosition(7, 7));
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

        public ChessPiece GetPieceAt(BoardPosition position)
        {
            if (!position.IsValid())
                return null;

            return squares[position.X, position.Y];
        }

        private void SetPieceAt(BoardPosition position, ChessPiece piece)
        {
            if (position.IsValid())
            {
                squares[position.X, position.Y] = piece;
                if (piece != null)
                {
                    piece.UpdateScreenPosition(topLeftOfBoard, squareSize);
                }
            }
        }

        public bool IsSquareOccupied(BoardPosition position)
        {
            return GetPieceAt(position) != null;
        }

        public bool IsSquareOccupiedByOpponent(BoardPosition position, PieceColor myColor)
        {
            var piece = GetPieceAt(position);
            return piece != null && piece.Color != myColor;
        }

        public bool IsSquareOccupiedByFriend(BoardPosition position, PieceColor myColor)
        {
            var piece = GetPieceAt(position);
            return piece != null && piece.Color == myColor;
        }

        public bool MovePiece(BoardPosition from, BoardPosition to)
        {
            // Validate basic conditions
            if (State != GameState.InProgress)
                return false;

            var piece = GetPieceAt(from);
            if (piece == null || piece.Color != CurrentPlayer)
                return false;

            // Check if the move is valid for this piece
            if (!piece.CanMoveTo(to, this))
                return false;

            // Check if move would leave king in check
            if (WouldMoveCauseCheck(from, to, CurrentPlayer))
                return false;

            // Execute the move
            return ExecuteMove(piece, from, to);
        }

        private bool ExecuteMove(ChessPiece piece, BoardPosition from, BoardPosition to)
        {
            // Capture piece if any
            ChessPiece capturedPiece = GetPieceAt(to);

            // Remove piece from old position
            SetPieceAt(from, null);

            // Place piece at new position
            piece.MoveTo(to);
            SetPieceAt(to, piece);

            // Add to move history
            var move = new Move(from, to, piece, capturedPiece);
            moveHistory.Add(move);

            // Check for pawn promotion
            if (piece is Pawn pawn && (to.Y == 0 || to.Y == 7))
            {
                PromotePawn(pawn, to);
            }

            // Switch player
            var previousPlayer = CurrentPlayer;
            CurrentPlayer = CurrentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;

            // Check game state
            CheckGameState();

            // Raise events
            OnPieceMoved(new PieceMovedEventArgs
            {
                From = from,
                To = to,
                PieceMoved = piece,
                PieceCaptured = capturedPiece,
                Player = previousPlayer
            });

            OnPlayerChanged(new PlayerChangedEventArgs
            {
                PreviousPlayer = previousPlayer,
                CurrentPlayer = CurrentPlayer
            });

            return true;
        }

        private bool WouldMoveCauseCheck(BoardPosition from, BoardPosition to, PieceColor playerColor)
        {
            // Temporarily make the move
            ChessPiece originalPiece = GetPieceAt(from);
            ChessPiece targetPiece = GetPieceAt(to);

            // Remove piece from original position
            SetPieceAt(from, null);

            // Place piece at new position
            SetPieceAt(to, originalPiece);

            // Check if king is in check
            bool causesCheck = IsKingInCheck(playerColor);

            // Restore original state
            SetPieceAt(from, originalPiece);
            SetPieceAt(to, targetPiece);

            return causesCheck;
        }

        public BoardPosition FindKingPosition(PieceColor color)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = squares[x, y];
                    if (piece is King king && king.Color == color)
                    {
                        return new BoardPosition(x, y);
                    }
                }
            }
            throw new InvalidOperationException($"King for {color} not found on board!");
        }
        public bool IsKingInCheck(PieceColor kingColor)
        {
            var kingPos = FindKingPosition(kingColor);
            var opponentColor = kingColor == PieceColor.White ? PieceColor.Black : PieceColor.White;

            // Check if any opponent piece can attack the king
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = squares[x, y];
                    if (piece != null && piece.Color == opponentColor)
                    {
                        if (piece.CanMoveTo(kingPos, this))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool HasAnyValidMoves(PieceColor playerColor)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = squares[x, y];
                    if (piece != null && piece.Color == playerColor)
                    {
                        var validMoves = piece.GetValidMoves(this);
                        foreach (var move in validMoves)
                        {
                            if (!WouldMoveCauseCheck(new BoardPosition(x, y), move, playerColor))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void CheckGameState()
        {
            if (IsKingInCheck(CurrentPlayer))
            {
                if (!HasAnyValidMoves(CurrentPlayer))
                {
                    State = GameState.CheckMate;
                    OnGameStateChanged(new GameStateChangedEventArgs
                    {
                        NewState = State,
                        Winner = CurrentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White
                    });
                }
                else
                {
                    State = GameState.Check;
                    OnGameStateChanged(new GameStateChangedEventArgs { NewState = State });
                }
            }
            else if (!HasAnyValidMoves(CurrentPlayer))
            {
                State = GameState.StaleMate;
                OnGameStateChanged(new GameStateChangedEventArgs { NewState = State });
            }
            else
            {
                State = GameState.InProgress;
            }
        }

        private void PromotePawn(Pawn pawn, BoardPosition position)
        {
            var newQueen = new Queen(pawn.Color, position);
            SetPieceAt(position, newQueen);
        }

        public List<BoardPosition> GetValidMovesForPiece(BoardPosition position)
        {
            var piece = GetPieceAt(position);
            if (piece == null || piece.Color != CurrentPlayer)
                return new List<BoardPosition>();

            var validMoves = piece.GetValidMoves(this);
            var legalMoves = new List<BoardPosition>();

            // Filter out moves that would leave king in check
            foreach (var move in validMoves)
            {
                if (!WouldMoveCauseCheck(position, move, CurrentPlayer))
                {
                    legalMoves.Add(move);
                }
            }

            return legalMoves;
        }

        public BoardPosition? ScreenToBoardPosition(Point screenPoint)
        {
            int boardX = (screenPoint.X - topLeftOfBoard.X) / squareSize;
            int boardY = (screenPoint.Y - topLeftOfBoard.Y) / squareSize;

            if (boardX >= 0 && boardX < 8 && boardY >= 0 && boardY < 8)
            {
                return new BoardPosition(boardX, boardY);
            }

            return null;
        }

        public Rectangle GetSquareRectangle(BoardPosition position)
        {
            int screenX = topLeftOfBoard.X + (position.X * squareSize);
            int screenY = topLeftOfBoard.Y + (position.Y * squareSize);


            return new Rectangle(screenX, screenY, squareSize, squareSize);
        }

        public void Draw(PaintEventArgs e, BoardPosition? selectedPosition = null, List<BoardPosition> validMoves = null)
        {
            DrawBoard(e, selectedPosition, validMoves);
            DrawPieces(e);
        }

        private void DrawBoard(PaintEventArgs e, BoardPosition? selectedPosition, List<BoardPosition> validMoves)
        {
            var g = e.Graphics;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var position = new BoardPosition(x, y);
                    var rect = GetSquareRectangle(position);

                    // Determine square color
                    bool isLightSquare = (x + y) % 2 == 0;
                    Brush squareBrush = isLightSquare ? lightSquareBrush : darkSquareBrush;

                    // Draw square background
                    g.FillRectangle(squareBrush, rect);

                    // Highlight selected square
                    if (selectedPosition.HasValue && selectedPosition.Value == position)
                    {
                        g.FillRectangle(selectedSquareBrush, rect);
                    }

                    // Highlight valid moves
                    if (validMoves != null && validMoves.Contains(position))
                    {
                        using (var moveBrush = new SolidBrush(Color.FromArgb(100, Color.LightGreen)))
                        {
                            g.FillRectangle(moveBrush, rect);
                        }
                    }

                    // Draw square border
                    g.DrawRectangle(Pens.Black, rect);

                    // Draw coordinates (optional)
                    DrawCoordinates(g, position, rect);
                }
            }
        }

        private void DrawCoordinates(Graphics g, BoardPosition position, Rectangle rect)
        {
            // Draw file letters (A-H) at bottom
            if (position.Y == 7)
            {
                string file = ((char)('A' + position.X)).ToString();
                g.DrawString(file, new Font("Arial", 10), Brushes.Black,
                            rect.X + squareSize / 2 - 5, rect.Y + squareSize + 2);
            }

            // Draw rank numbers (1-8) on left side
            if (position.X == 0)
            {
                string rank = (8 - position.Y).ToString();
                g.DrawString(rank, new Font("Arial", 10), Brushes.Black,
                            rect.X - 15, rect.Y + squareSize / 2 - 8);
            }
        }

        private void DrawPieces(PaintEventArgs e)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = squares[x, y];
                    if (piece != null)
                    {
                        piece.Draw(e);
                    }
                }
            }
        }

        public event EventHandler<PieceMovedEventArgs> PieceMoved;
        public event EventHandler<PlayerChangedEventArgs> PlayerChanged; 
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        protected virtual void OnPieceMoved(PieceMovedEventArgs e)
        {
            PieceMoved?.Invoke(this, e);
        }

        protected virtual void OnPlayerChanged(PlayerChangedEventArgs e)
        {
            PlayerChanged?.Invoke(this, e);
        }

        protected virtual void OnGameStateChanged(GameStateChangedEventArgs e)
        {
            GameStateChanged?.Invoke(this, e);
        }

        // Properties for UI customization
        public Point BoardTopLeft
        {
            get => topLeftOfBoard;
            set { topLeftOfBoard = value; UpdateAllPiecePositions(); }
        }
        
        public int SquareSize
        {
            get => squareSize;
            set { squareSize = value; UpdateAllPiecePositions(); }
        }
        
        public Brush LightSquareBrush
        {
            get => lightSquareBrush;
            set => lightSquareBrush = value;
        }
        
        public Brush DarkSquareBrush
        {
            get => darkSquareBrush;
            set => darkSquareBrush = value;
        }
    }

    public class PieceMovedEventArgs : EventArgs
    {
        public BoardPosition From { get; set; }
        public BoardPosition To { get; set; }
        public ChessPiece PieceMoved { get; set; }
        public ChessPiece PieceCaptured { get; set; }
        public PieceColor Player { get; set; }
    }

    public class PlayerChangedEventArgs : EventArgs
    {
        public PieceColor PreviousPlayer { get; set; }
        public PieceColor CurrentPlayer { get; set; }
    }

    public class GameStateChangedEventArgs : EventArgs
    {
        public GameState NewState { get; set; }
        public PieceColor Winner { get; set; }
    }
}
