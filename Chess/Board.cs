using System;
using System.Collections.Generic;
using System.Text;
using Chess.Pieces;

namespace Chess
{
    public class Board
    {
        public Piece[,] grid;

        public Board()
        {
            grid = new Piece[8, 8];
        }


        public void PlacePiece(Piece piece, int x, int y)
        {
            grid[x, y] = piece;
            piece.xPostion = x;
            piece.yPostion = y;
        }

        public Piece GetPiece(int x, int y)
        {
            return grid[x, y];
        }

        public void printBoard()
        {
            int counter = 0;
            string thingToPrint = "";
            foreach(var piece in grid)
            {
                counter++;
                if (piece == null)
                {
                    thingToPrint += "  |";
                } else
                {
                    thingToPrint += piece.ToString() + "|";

                }
                if (counter == 8)
                {
                    Console.WriteLine(thingToPrint);
                    thingToPrint = "";
                    counter = 0;
                }
            }
        }
    }
}
