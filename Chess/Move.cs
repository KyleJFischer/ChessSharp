using System;
using System.Collections.Generic;
using System.Text;
using Chess.Pieces;

namespace Chess
{
    public class Move
    {
        public Piece movingPiece;
        public int xMove;
        public int yMove;

        public Move(Piece piece, int x, int y)
        {
            this.movingPiece = piece;
            this.xMove = x;
            this.yMove = y;
        }

    }
}
