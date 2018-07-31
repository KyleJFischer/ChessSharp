using Chess.Pieces;
using System;

namespace Chess
{
    class Program
    {
        static Game game = new Game();
        static void Main(string[] args)
        {
            while (true)
            {
                game.PrintBoard();
                Console.Write("Input: ");
                var input = Console.ReadLine();
                Console.Clear();
                reactOnInput(input);
            }
        }

        static void reactOnInput(string input)
        {
            string lowerInput = input.ToLower();
            var tokens = lowerInput.Split(" ");
            switch (tokens[0])
            {
                case "move":
                    movePieces(tokens);
                    break;
                case "m":
                    movePieces(tokens);
                    break;
                case "l":
                    listMoves(tokens);
                    break;
                case "list":
                    listMoves(tokens);
                    break;
                case "newgame":
                    newGame();
                    break;
            }
        }

        static void newGame()
        {
            Console.Write("Are you Sure?: ");
            var questionResult = Console.ReadLine().ToLower();
            if (questionResult.Contains("y"))
            {
                game = new Game();
            }
            Console.Clear();
        }

        static void movePieces(string[] parts)
        {
            if (parts.Length != 5)
            {
                Console.WriteLine("You need 5 inputs (Move StartingX StartingY EndingX EndingY)");
                return;
            }

            int StartingX = int.Parse(parts[1]);
            int StartingY = int.Parse(parts[2]);

            var piece = game.GetPiece(StartingX, StartingY);

            if (piece == null)
            {
                Console.WriteLine("No Piece was found there!");
                return;
            }

            if (piece.isWhite != game.WhitesTurn)
            {
                Console.WriteLine("ITS NOT YOUR TURN");
                return;
            }

            int EndingX = int.Parse(parts[3]);
            int EndingY = int.Parse(parts[4]);
            var result = game.MovePiece(piece, EndingX, EndingY, out var deadPiece);

            if (!result)
            {
                Console.WriteLine("Invalid move");
            }
            var winner = game.DetermineWinner();
            if (winner != 0)
            {
                var winnerText = (winner == 1 )? "White" : "Black";
                Console.WriteLine("We have a winner!");
                Console.WriteLine("The winner is " + winnerText);
            }
        }

        static void listMoves(string[] parts)
        {
            var piece = getPiece(parts);
            if (piece == null)
            {
                return;
            }
            Console.WriteLine("Possible Moves for that Piece.");
            game.printMoves(game.GetPiecesValidMoves(piece));

        }

        static Piece getPiece(string[] parts)
        {
            int StartingX = int.Parse(parts[1]);
            int StartingY = int.Parse(parts[2]);

            var piece = game.GetPiece(StartingX, StartingY);

            if (piece == null)
            {
                Console.WriteLine("No Piece was found there!");
                return null;
            }
            return piece;
        }

        static void TestMethod()
        {
            var testPiece = new Pawn(false);
            game = new Game(testPiece, 0, 6);

           var list = game.GetPiecesValidMoves(testPiece);
            var moveBoard = game.CreateMoveBoard(testPiece, list);
            moveBoard.printBoard();

        }
    }
}
