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
        public bool canMoveBackwards = true;

        public int maxHorizontalMovement;
        public int maxVerticalMovement;
        public int maxDiagonalMovement;

        public override string ToString()
        {
            var firstLetter = (isWhite) ? "W" : "B";
            return (firstLetter) + letterRepresentation;
        }

        public abstract List<Move> GetListOfMoves();

        public List<Move> GetHorizontalMoves(int maxDistance)
        {
            var moves = new List<Move>();

            var positiveDone = false;
            var negativeDone = false;
            var currentDistance = 0;
            while (currentDistance < maxDistance)
            {
                var testLocationPositive = this.xPostion + currentDistance;
                var testLocationNegative = this.xPostion - currentDistance;

                if (!positiveDone && testLocationPositive > 7)
                {
                    positiveDone = true;

                } else
                {
                    var move = new Move(this, testLocationPositive, this.yPostion);
                    moves.Add(move);
                }

                if (!negativeDone && testLocationNegative < 0)
                {
                    negativeDone = true;
                }
                else
                {
                    var move = new Move(this, testLocationNegative, this.yPostion);
                    moves.Add(move);
                }

                if (positiveDone && negativeDone)
                {
                    break;
                }

                currentDistance++;
            }
            


            return moves;
        }

        public List<Move> GetVerticalMoves(int maxDistance)
        {
            var moves = new List<Move>();

            var positiveDone = false;
            var negativeDone = false;
            var currentDistance = 0;
            while (currentDistance < maxDistance)
            {
                var testLocationPositive = this.yPostion + currentDistance;
                var testLocationNegative = this.yPostion - currentDistance;

                if (!positiveDone && testLocationPositive > 7)
                {
                    positiveDone = true;

                }
                else
                {
                    var move = new Move(this, testLocationPositive, this.yPostion);
                    moves.Add(move);
                }

                if (!negativeDone && testLocationNegative < 0)
                {
                    negativeDone = true;
                }
                else
                {
                    var move = new Move(this, testLocationNegative, this.yPostion);
                    moves.Add(move);
                }

                if (positiveDone && negativeDone)
                {
                    break;
                }

                currentDistance++;
            }



            return moves;
        }

        public List<Move> GetDiagonalMoves(int maxDistance)
        {
            var moves = new List<Move>();

            //Get UpRight
            
            //Get UpLeft
            
            //Get DownLeft

            //Get DownRight


            return moves;
        }

        public bool CheckHorizontal(int requestedY, int maxDistance)
        {
            var temp = Math.Abs(this.yPostion - requestedY);
            if (temp > maxDistance)
            {
                return false;
            }
            return true;
        }

        public bool CheckVertical(int requestedX, int maxDistance)
        {
            var temp = Math.Abs(this.xPostion - requestedX);
            if (temp > maxDistance)
            {
                return false;
            }
            return true;
        }

        public bool CheckDiagonals(int maxDistance)
        {
            return false;
        }
    }
}
