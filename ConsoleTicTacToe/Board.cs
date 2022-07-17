using System;

namespace ConsoleTicTacToe
{
    class Board
    {
        private char[,] board;
        private readonly string boardRepresentation = Resource.board;
        private Player player1;
        private Player player2;
        private bool isFull;
        private int takenFields = 0;

        internal Player Player1 { get => player1; set => player1 = value; }
        internal Player Player2 { get => player2; set => player2 = value; }
        public bool IsFull { get => isFull; set => isFull = value; }

        public Board(Player player1, Player player2)
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }
        private void InitBoard()
        {
            board = new char[3, 3];
            int counter = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    // Agrega 48 para obtener el número en valor ascii o unicode (no recuerdo) e.e
                    board[i, j] = (char)(++counter + 48);
                }
            }
        }
        public string GetRenderBoard()
        {
            if (board == null) InitBoard();
            if (boardRepresentation == null) throw new ApplicationException("No se pudo imprimir el tablero");
            else {
                return String.Format(
                    boardRepresentation,
                    board[0,0],
                    board[0,1],
                    board[0,2],
                    board[1,0],
                    board[1,1],
                    board[1,2],
                    board[2,0],
                    board[2,1],
                    board[2,2]
                    );
            }
        }

        public void UpdateBoard()
        {
            UpdatePlayerSpots(Player1);
            UpdatePlayerSpots(Player2);
        }

        /// <summary>
        /// Moves the player 1 to into the given coordenates
        /// </summary>
        /// <param name="x">x coordenates (-1 &lt; x &lt; 2)</param>
        /// <param name="y">x coordenates (-1 &lt; y &lt; 2)</param>
        public void MovePlayer1(int x, int y)
        {
            this.MovePlayer(player1, x, y);
        }

        /// <summary>
        /// Moves the player 2 to into the given coordenates
        /// </summary>
        /// <param name="x">x coordenates (-1 &lt; x &lt; 2)</param>
        /// <param name="y">x coordenates (-1 &lt; y &lt; 2)</param>
        public void MovePlayer2(int x, int y)
        {
            this.MovePlayer(player2, x, y);
        }

        /// <summary>
        /// Moves the selected player into the given coordenates.
        /// </summary>
        /// <param name="player">The player to move</param>
        /// <param name="x">x coordenates (-1 &lt; x &lt; 2)</param>
        /// <param name="y">x coordenates (-1 &lt; y &lt; 2)</param>
        private void MovePlayer(Player player, int x, int y)
        {
            if (x < 0 || x > 2 || y < 0 || y > 2)
                throw new OverflowException("Player cannot move outside of the board");

            if (board[x, y] == player1.Character || board[x, y] == player2.Character)
                throw new FieldAccessException("Spot is already taken");

            player.TakenSpots[x, y] = true;
            board[x, y] = player.Character;
            takenFields++;
            if (takenFields == 9) this.IsFull = true;
        }

        private void UpdatePlayerSpots(Player player)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (player.TakenSpots[i, j]) board[i, j] = player.Character;
        }
    }
}
