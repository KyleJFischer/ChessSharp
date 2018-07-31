using System;
using System.Collections.Generic;
using System.Text;
using Chess.Pieces;

namespace Chess
{
    class Game
    {
        Board gameBoard;
        List<Piece> whiteGraveyard;
        List<Piece> blackGraveyard;

        public Game()
        {
            SetupOverallBoard();
            SetupPawns();
            SetupBackrow();

            gameBoard.printBoard();
            
        }

        public Game(Piece centerPiece)
        {
            //mainly for testing moves
            
            SetupOverallBoard();

            gameBoard.PlacePiece(centerPiece, 4, 4);

            gameBoard.printBoard();

        }

        internal void SetupPawns()
        {
            SetupRowOfPawns(1, true);
            SetupRowOfPawns(6, false);
        }

        internal void SetupBackrow()
        {
            SetupBackRowPieces(0, true);
            SetupBackRowPieces(7, false);

        }

        internal void SetupOverallBoard()
        {
            gameBoard = new Board();
            whiteGraveyard = new List<Piece>();
            blackGraveyard = new List<Piece>();

        }

        internal void SetupRowOfPawns(int row, bool whitePawns)
        {
            if (gameBoard == null)
            {
                return;

            }


            for (int i = 0; i < Math.Sqrt(gameBoard.grid.Length); i++)
            {
                var pawn = new Pawn(whitePawns);
                gameBoard.PlacePiece(pawn, i, row);
            }
        }

        internal void SetupBackRowPieces(int row, bool white)
        {
            if (gameBoard == null)
            {
                return;
            }
            var queen = new Queen(white);
            var king = new King(white);
            var rook1 = new Rook(white);
            var rook2 = new Rook(white);
            var bishop1 = new Bishop(white);
            var bishop2 = new Bishop(white);
            var knight1 = new Knight(white);
            var knight2 = new Knight(white);

            gameBoard.PlacePiece(king, 4, row);
            gameBoard.PlacePiece(queen, 3, row);

            gameBoard.PlacePiece(rook1, 0, row);
            gameBoard.PlacePiece(rook2, 7, row);
            gameBoard.PlacePiece(bishop1, 2, row);
            gameBoard.PlacePiece(bishop2, 5, row);
            gameBoard.PlacePiece(knight1, 1, row);
            gameBoard.PlacePiece(knight2, 6, row);
        }

        public bool MovePiece(Piece attackingPiece, int x, int y, out Piece defendingPiece)
        {
            defendingPiece = null;
            var listOfMoves = attackingPiece.GetListOfMoves();
            var validMove = false;
            foreach (var move in listOfMoves)
            {
                if (move.xMove == x && move.yMove == y)
                {
                    validMove = true;
                    break;
                }
            }
            if (validMove)
            {
                defendingPiece = this.gameBoard.GetPiece(x, y);
                if (defendingPiece != null)
                {
                    PutPieceInGraveyard(defendingPiece);
                }

                this.gameBoard.PlacePiece(attackingPiece, x, y);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PutPieceInGraveyard(Piece piece)
        {
            if (piece.isWhite)
            {
                whiteGraveyard.Add(piece);
            }
            else
            {
                blackGraveyard.Add(piece);
            }
        }

        public int GetGraveyardTotal(bool white)
        {
            List<Piece> graveyardInQuestion = null;
            var sum = 0;
            if (white)
            {
                graveyardInQuestion = whiteGraveyard;
            }
            else
            {
                graveyardInQuestion = blackGraveyard;
            }

            foreach (var item in graveyardInQuestion)
            {
                sum += item.pointValue;
            }
            return sum;
        }

        public List<Move> GetPiecesValidMoves(Piece piece)
        {
            var moves = new List<Move>();
            moves.AddRange(GetHorizontalMoves(piece));
            moves.AddRange(GetVerticalMoves(piece));
            moves.AddRange(GetDiagonalMoves(piece));
            return moves;
        }

        internal List<Move> GetHorizontalMoves(Piece piece)
        {
            var moves = new List<Move>();
            var piecesX = piece.xPostion;
            var piecesY = piece.yPostion;
            int currentDistance = 0;
            //Horizontal to The Right
            while (currentDistance <= piece.maxHorizontalMovement)
            {
                var newSpot = piecesX + currentDistance;

                if (newSpot >= 8)
                {
                    break;
                }

                var move = GetMoveIfPossible(piece, newSpot, piece.yPostion, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }
            currentDistance = 0;

            while (currentDistance <= piece.maxHorizontalMovement)
            {
                var newSpot = piecesX - currentDistance;

                if (newSpot < 0)
                {
                    break;
                }

                var move = GetMoveIfPossible(piece, newSpot, piece.yPostion, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }
            return moves;
        }

        public bool arePiecesSameColor(Piece piece1, Piece piece2)
        {
            return (piece1.isWhite == piece2.isWhite);
        }

        internal List<Move> GetVerticalMoves(Piece piece)
        {
            var moves = new List<Move>();
            var piecesX = piece.xPostion;
            var piecesY = piece.yPostion;
            int currentDistance = 0;
            //Horizontal to The Right
            while (currentDistance <= piece.maxVerticalMovement)
            {
                var newSpot = piecesY + currentDistance;

                if (newSpot >= 8)
                {
                    break;
                }

                var move = GetMoveIfPossible(piece, piece.xPostion, newSpot, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }
            currentDistance = 0;

            while (currentDistance <= piece.maxVerticalMovement)
            {
                var newSpot = piecesY - currentDistance;

                if (newSpot < 0)
                {
                    break;
                }

                var move = GetMoveIfPossible(piece, piece.xPostion, newSpot, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }
            return moves;
        }

        internal List<Move> GetDiagonalMoves(Piece piece)
        {
            var moves = new List<Move>();
            var piecesX = piece.xPostion;
            var piecesY = piece.yPostion;
            int currentDistance = 0;
            
            while (currentDistance <= piece.maxDiagonalMovement)
            {
                var placeX = currentDistance + piece.xPostion;
                var placeY = currentDistance + piece.yPostion;

                if (placeX > 7 || placeY > 7)
                {
                    break;
                }
                var move = GetMoveIfPossible(piece, placeX, placeY, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }

            currentDistance = 0;

            while (currentDistance <= piece.maxDiagonalMovement)
            {
                var placeX = currentDistance + piece.xPostion;
                var placeY = piece.yPostion - currentDistance;

                if (placeX > 7 || placeY < 0)
                {
                    break;
                }
                var move = GetMoveIfPossible(piece, placeX, placeY, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }

            currentDistance = 0;

            while (currentDistance <= piece.maxDiagonalMovement)
            {
                var placeX = piece.xPostion - currentDistance;
                var placeY = piece.yPostion - currentDistance;

                if (placeX < 0 || placeY < 0)
                {
                    break;
                }
                var move = GetMoveIfPossible(piece, placeX, placeY, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }

            currentDistance = 0;

            while (currentDistance <= piece.maxDiagonalMovement)
            {
                var placeX = piece.xPostion - currentDistance;
                var placeY = piece.yPostion + currentDistance;

                if (placeX < 0 || placeY >= 8)
                {
                    break;
                }
                var move = GetMoveIfPossible(piece, placeX, placeY, out Piece capturedPiece);

                if (move != null)
                {
                    moves.Add(move);
                }

                if (capturedPiece != null)
                {
                    break;
                }

                currentDistance++;
            }


            return moves;
        }

        internal Move GetMoveIfPossible(Piece piece, int x, int y, out Piece capturedPiece)
        {
            Move move = null; 

            if (piece.yPostion == y && piece.xPostion == x)
            {
                capturedPiece = null;
                return null;
            }

            capturedPiece = gameBoard.GetPiece(x, y);

            if (capturedPiece == null)
            {
                move = new Move(piece, x, y);
                
            }
            else
            {
                if (!arePiecesSameColor(piece, capturedPiece))
                {
                    move = new Move(piece, x, y);
                }
            }
            return move;
        }

        public void printMoves(List<Move> list)
        {
            foreach(var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public List<Move> GetKnightMovement(Piece piece)
        {
            var moves = new List<Move>();

            

        }

    }
}
