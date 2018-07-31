using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 5;
            this.letterRepresentation = 'R';
            this.maxHorizontalMovement = int.MaxValue;
            this.maxVerticalMovement = int.MaxValue;
        }

    }
}
