using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        bool hasMoved;

        public Pawn(bool isWhite)
        {
            this.isWhite = isWhite;
            this.pointValue = 1;
            this.letterRepresentation = 'P';
            this.hasMoved = true;
        }

        public override bool isSemiValidMove(int x, int y)
        {


            var result = false;
            if (hasMoved)
            {
                result = CheckVertical(x, 1);
            } else
            {
                result = CheckVertical(x, 2);
            }
            return result;
        }
    }
}
