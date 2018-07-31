using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = int.MaxValue;
            this.letterRepresentation = 'K';
            this.maxHorizontalMovement = 1;
            this.maxVerticalMovement = 1;
            this.maxDiagonalMovement = 1;
        }

    }
}
