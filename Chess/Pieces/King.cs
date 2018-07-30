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
        }

        public override List<Move> GetListOfMoves()
        {
            var moves = new List<Move>();

            moves.AddRange(GetHorizontalMoves(1));
            moves.AddRange(GetVerticalMoves(1));
            moves.AddRange(GetDiagonalMoves(1));

            return moves;
        }
    }
}
