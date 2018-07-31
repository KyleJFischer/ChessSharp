using Chess.Pieces;
using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {

            var testPiece = new Pawn(true);
            testPiece.hasMoved = true;
            var opponent1 = new Pawn(false);
            var opp2 = new Pawn(false);

            var game = new Game(testPiece, 4, 4);
            game.AddPieceToBoard(opponent1, 5, 5);
            game.AddPieceToBoard(opp2, 3, 5);

            game.PrintBoard();
           

            var listOfMoves = game.GetPiecesValidMoves(testPiece);
            Console.WriteLine("__________________________________________");
            var moveBoard = game.CreateMoveBoard(testPiece, listOfMoves);
            moveBoard.printBoard();

            game.printMoves(listOfMoves);

            Console.ReadLine();
        }
    }
}
