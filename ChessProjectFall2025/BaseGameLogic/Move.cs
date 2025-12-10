using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectFall2025.BaseGameLogic
{
    public class Move
    {
        //this is where info about which move is done when is stored
        public BoardPosition From { get; }
        public BoardPosition To { get; }
        public ChessPiece PieceMoved { get; }
        public ChessPiece PieceCaptured { get; }
        public bool IsCheck { get; set; }
        public bool IsCheckMate { get; set; }
        public bool IsCastling { get; set; }
        public bool IsEnPassant { get; set; }
        // other special abilities and items here that we made
        public bool IsSwap { get; set; }
        public bool IsProtected { get; set; }
        public bool IsMoveAgain { get; set; }
        public bool IsYourFuturePiece { get; set; }
        public bool IsContinousMove { get; set; }
        public bool IsNoGoingBack { get; set; }
        
        public Move(BoardPosition from, BoardPosition to, ChessPiece pieceMoved, ChessPiece pieceCaptured = null)
        {
            From = from;
            To = to;
            PieceMoved = pieceMoved;
            PieceCaptured = pieceCaptured;
        }
    }
}
