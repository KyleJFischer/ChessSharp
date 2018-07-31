using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public bool hasMoved;

        public Pawn(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 1;
            this.letterRepresentation = 'P';
            this.hasMoved = false;
        }


    }
}
