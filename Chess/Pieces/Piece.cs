using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Pieces
{

    public abstract class Piece
    {
        public int pointValue;
        public char letterRepresentation;
        public bool isWhite;
        public int xPostion;
        public int yPostion;
        public bool hasCustomMovement = false;

        public int maxHorizontalMovement;
        public int maxVerticalMovement;
        public int maxDiagonalMovement;

        public override string ToString()
        {
            var firstLetter = (isWhite) ? "W" : "B";
            return (firstLetter) + letterRepresentation;
        }


        public Piece Copy()
        {
            Piece other = (Piece)this.MemberwiseClone();
            return other;
        }
    }
}
