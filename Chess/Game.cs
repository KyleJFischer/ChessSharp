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


        }

        public Game(Piece piece, int x, int y)
        {

            SetupOverallBoard();

            gameBoard.PlacePiece(piece, x, y);

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
            var listOfMoves = GetPiecesValidMoves(attackingPiece);
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
                if (attackingPiece.letterRepresentation == 'P')
                {
                    ((Pawn)attackingPiece).hasMoved = true;
                }

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
            switch (piece.letterRepresentation)
            {
                case 'N':
                    moves.AddRange(GetKnightMovement((Knight)piece));
                    break;
                case 'P':
                    moves.AddRange(GetPawnMovement((Pawn)piece));
                    break;
                default:
                    moves.AddRange(GetHorizontalMoves(piece));
                    moves.AddRange(GetVerticalMoves(piece));
                    moves.AddRange(GetDiagonalMoves(piece));
                    break;
            }

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

            if (x < 0 || x > 7)
            {
                capturedPiece = null;
                return null;
            }

            if (y < 0 || y > 7)
            {
                capturedPiece = null;
                return null;
            }


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

        public List<Move> GetKnightMovement(Knight piece)
        {
            var moves = new List<Move>();

            
            var xPosition1 = piece.xPostion + 2;
            var yPosition1 = piece.yPostion + 1;
            var move = GetMoveIfPossible(piece, xPosition1, yPosition1, out Piece capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }

            var xPosition2 = piece.xPostion + 2;
            var yPosition2 = piece.yPostion - 1;

            move = GetMoveIfPossible(piece, xPosition2, yPosition2, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }

            var xPosition3 = piece.xPostion - 2;
            var yPosition3 = piece.yPostion + 1;

            move = GetMoveIfPossible(piece, xPosition3, yPosition3, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }

            var xPosition4 = piece.xPostion - 2;
            var yPosition4 = piece.yPostion - 1;


            move = GetMoveIfPossible(piece, xPosition4, yPosition4, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }


            var xPosition5 = piece.xPostion + 1;
            var yPosition5 = piece.yPostion + 2;


            move = GetMoveIfPossible(piece, xPosition5, yPosition5, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }


            var xPosition6 = piece.xPostion + 1;
            var yPosition6 = piece.yPostion - 2;


            move = GetMoveIfPossible(piece, xPosition6, yPosition6, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }


            var xPosition7 = piece.xPostion - 1;
            var yPosition7 = piece.yPostion + 2;

            move = GetMoveIfPossible(piece, xPosition7, yPosition7, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }

            var xPosition8 = piece.xPostion - 1;
            var yPosition8 = piece.yPostion - 2;


            move = GetMoveIfPossible(piece, xPosition8, yPosition8, out capturedPiece);

            if (move != null)
            {
                moves.Add(move);
            }


            return moves;
        }

        public List<Move> GetPawnMovement(Pawn piece)
        {
            var moves = new List<Move>();
            var newyPosition = piece.yPostion + 1;

            var move = GetMoveIfPossible(piece, piece.xPostion, newyPosition, out Piece captured);

            if (move != null)
            {
                moves.Add(move);
            }

            var rightAttack = piece.xPostion + 1;
            move = GetMoveIfPossible(piece, rightAttack, newyPosition, out captured);

            if (move != null && captured != null)
            {
                moves.Add(move);
            }

            var leftAttack = piece.yPostion - 1;
            move = GetMoveIfPossible(piece, leftAttack, newyPosition, out captured);

            if (move != null && captured != null)
            {
                moves.Add(move);
            }


            if (!piece.hasMoved)
            {
                newyPosition = piece.xPostion + 2;

                move = GetMoveIfPossible(piece, piece.xPostion, newyPosition, out captured);

                if (move != null)
                {
                    moves.Add(move);
                }
            }



            return moves;
        }

        public Board CreateMoveBoard(Piece piece, List<Move> moves)
        {
            Board board = new Board();
            Piece moveBoardPiece = piece.Copy();
            board.PlacePiece(moveBoardPiece, piece.xPostion, piece.yPostion);

            foreach(var item in moves)
            {
                var movePiece = new MovePiece(piece.isWhite);
                board.PlacePiece(movePiece, item.xMove, item.yMove);
            }
            return board;
        }

        public void AddPieceToBoard(Piece piece, int x, int y)
        {
            gameBoard.PlacePiece(piece, x, y);
        }

        public void PrintBoard()
        {
            gameBoard.printBoard();
        }
    }
}
