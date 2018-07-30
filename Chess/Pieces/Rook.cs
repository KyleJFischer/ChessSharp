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
        }

        public override List<Move> GetListOfMoves()
        {
            var moves = new List<Move>();

            moves.AddRange(GetHorizontalMoves(int.MaxValue));
            moves.AddRange(GetVerticalMoves(int.MaxValue));

            return moves;
        }

    }
}
