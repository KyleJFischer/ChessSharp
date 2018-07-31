using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    class Knight : Piece
    {

        public Knight(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 3;
            this.letterRepresentation = 'N';
            this.hasCustomMovement = true;
        }

    }

}
