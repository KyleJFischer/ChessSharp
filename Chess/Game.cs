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

        int totalInWhiteGraveYard = 0;
        int totalInBlackGraveYard = 0;

        public Game()
        {
            gameBoard = new Board();
            SetupRowOfPawns(1, true);
            SetupRowOfPawns(6, false);
            SetupBackRowPieces(0, true);
            SetupBackRowPieces(7, false);
            whiteGraveyard = new List<Piece>();
            blackGraveyard = new List<Piece>();

            gameBoard.printBoard();
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
            foreach(var move in listOfMoves)
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
            } else
            {
                return false;
            }
        }

        public void PutPieceInGraveyard(Piece piece)
        {
            if (piece.isWhite)
            {
                whiteGraveyard.Add(piece);
            } else
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
            } else
            {
                graveyardInQuestion = blackGraveyard;
            }

            foreach(var item in graveyardInQuestion)
            {
                sum += item.pointValue;
            }
            return sum;
        }

        public List<Move> GetPiecesValidMoves(Piece piece)
        {
            var moves = piece.GetListOfMoves();


            


            return moves;
        }
    }
}
