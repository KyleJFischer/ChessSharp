using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class MovePiece : Piece
    {
        public MovePiece(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 0;
            this.letterRepresentation = 'M';
        }
}
}
