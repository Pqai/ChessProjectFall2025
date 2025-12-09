using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectFall2025.BaseGameLogic
{
    public enum PieceColor { White, Black }
    public enum  PieceType { King, Queen, Rook, Bishop, Knight, Pawn}
    public enum GameState { InProgress, Check, CheckMate, StaleMate}
}
