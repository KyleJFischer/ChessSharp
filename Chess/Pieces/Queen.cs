using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 9;
            this.letterRepresentation = 'Q';
            this.maxDiagonalMovement = int.MaxValue;
            this.maxHorizontalMovement = int.MaxValue;
            this.maxDiagonalMovement = int.MaxValue;
        }

    }
}
