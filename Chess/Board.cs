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

            for (int y = 0; y < 8; y++)
            {
                var thingToPrint = "|";
                for (int x = 0; x < 8; x++)
                {
                    var piece = GetPiece(x, y);

                    if (piece == null)
                    {
                        thingToPrint += "  |";
                    } else
                    {
                        var whiteOrBlack = piece.isWhite ? "W" : "B";
                        thingToPrint += $"{whiteOrBlack}{piece.letterRepresentation}|";
                    }
                }
                Console.WriteLine(thingToPrint);
                
            }
        }
    }
}
