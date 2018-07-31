using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    class Bishop : Piece
    {
        public Bishop(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 3;
            this.letterRepresentation = 'B';
            this.maxDiagonalMovement = int.MaxValue;
        }
    }
}
