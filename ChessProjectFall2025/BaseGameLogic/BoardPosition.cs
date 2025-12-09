using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectFall2025
{
    public struct BoardPosition : IEquatable<BoardPosition>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public BoardPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public BoardPosition(string chessNotation)//constructer
        {
            if (string.IsNullOrEmpty(chessNotation) || chessNotation.Length != 2)
                throw new ArgumentException("Invalid chess notation. Must be like 'E4', 'A1', etc.");

            char fileChar = char.ToUpper(chessNotation[0]);  // A-H
            char rankChar = chessNotation[1];                // 1-8

            if (fileChar < 'A' || fileChar > 'H')
                throw new ArgumentException($"Invalid file: {fileChar}. Must be A-H.");

            if (rankChar < '1' || rankChar > '8')
                throw new ArgumentException($"Invalid rank: {rankChar}. Must be 1-8.");

            X = fileChar - 'A';            // A=0, B=1, ..., H=7
            Y = 8 - (rankChar - '0');      // Rank 1=Row7, Rank 8=Row0
        }

        //converts to chess notation
        //use for replay system
        public string ToChessNotation()
        {
            char file = (char)('A' + X);
            int rank = 8 - Y;
            return $"{file}{rank}";
        }

        // Check if position is within board bounds
        public static bool IsValid(int x, int y)
        {
            return x >= 0 && x < 8 && y >= 0 && y < 8;
        }

        public bool IsValid()
        {
            return IsValid(X, Y);
        }

        public BoardPosition Offset(int dx, int dy)
        {
            return new BoardPosition(X + dx, Y + dy);
        }

        public int DistanceTo(BoardPosition other)
        {
            int dx = Math.Abs(other.X - X);
            int dy = Math.Abs(other.Y - Y);
            return Math.Max(dx, dy); // Chess distance (kings move)
        }

        public static bool operator == (BoardPosition a, BoardPosition b)=> a.X == b.X && a.Y == b.Y;

        public static bool operator !=(BoardPosition a, BoardPosition b) => !(a == b);

        public override bool Equals(object obj)
        {
            return obj is BoardPosition position && Equals(position);
        }

        public bool Equals(BoardPosition other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        // For debugging - shows chess notation
        public override string ToString()
        {
            return ToChessNotation();
        }
    }
}
