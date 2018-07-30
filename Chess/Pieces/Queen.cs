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
        }


        public override List<Move> GetListOfMoves()
        {
            var moves = new List<Move>();

            moves.AddRange(GetHorizontalMoves(int.MaxValue));
            moves.AddRange(GetVerticalMoves(int.MaxValue));
            moves.AddRange(GetDiagonalMoves(int.MaxValue));

            return moves;
        }


    }
}
