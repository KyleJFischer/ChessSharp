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

        public override List<Move> GetListOfMoves()
        {
            throw new NotImplementedException();
        }
    }
}
