using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTicTacToe
{
    class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        private int turn = 1;
        private int status = 0; // 0 idle, 1 playing, 2 tie, 3 winner
        public Game()
        {
            this.player1 = new Player("1", 'X');
            this.player2 = new Player("2", 'O');
            this.board = new Board(player1, player2);
        }

        private bool CheckRow(Player player, int row)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!player.TakenSpots[row, i]) return false;
            }
            player.Winner = true;
            return true;
        }

        private bool CheckColumn(Player player, int column)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!player.TakenSpots[i, column]) return false;
            }
            player.Winner = true;
            return true;
        }

        private bool CheckDiag(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!player.TakenSpots[i, i]) return false;
            }
            player.Winner = true;
            return true;
        }

        private bool CheckAntiDiag(Player player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!player.TakenSpots[i, 2 - i]) return false;
            }
            player.Winner = true;
            return true;
        }

        public void Start()
        {
            int selectedField;
            this.status = 1;
            while (this.status == 1)
            {
                selectedField = 0;
                string stringBoard = board.GetRenderBoard();
                Console.Clear();
                Console.WriteLine(stringBoard);

                while (selectedField == 0)
                {
                    Console.WriteLine("Turn of player {0}. Choose your field: ", turn);
                    string field = Console.ReadLine();
                    if (Int32.TryParse(field, out selectedField))
                    {
                        if (selectedField < 0 || selectedField > 9)
                        {
                            Console.WriteLine("You must select a field between 0 and 9");
                            selectedField = 0;
                        }
                        else
                        {
                            int row = (selectedField - 1) / 3;
                            int col = (selectedField - 1) % 3;
                            if (this.turn == 1)
                            {
                                try
                                {
                                    board.MovePlayer1(row, col);
                                    this.CheckRow(player1, row);
                                    this.CheckColumn(player1, col);
                                    if (row == col) this.CheckDiag(player1);
                                    if (row + col == 2) this.CheckAntiDiag(player1);
                                    if (player1.Winner) Console.WriteLine("Player 1 wins");
                                    this.turn = 2;
                                } catch (Exception e)
                                {
                                    Console.WriteLine(e.Message + " Try again");
                                    selectedField = 0;
                                }
                            }
                            else if (this.turn == 2)
                            {
                                try
                                {
                                    board.MovePlayer2(row, col);
                                    this.CheckRow(player2, row);
                                    this.CheckColumn(player2, col);
                                    if (row == col) this.CheckDiag(player2);
                                    if (row + col == 2) this.CheckAntiDiag(player2);
                                    if (player2.Winner) Console.WriteLine("Player 2 wins");
                                    this.turn = 1;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message + " Try again");
                                    selectedField = 0;
                                }
                            }
                            if (player1.Winner || player2.Winner)
                            {
                                Console.Clear();
                                stringBoard = board.GetRenderBoard();
                                Console.WriteLine(stringBoard);
                                Console.WriteLine("Game over");
                                this.status = 3;
                            }
                            if (board.IsFull)
                            {
                                Console.Clear();
                                stringBoard = board.GetRenderBoard();
                                Console.WriteLine(stringBoard);
                                Console.WriteLine("Tie");
                                this.status = 2;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The input field is invalid");
                    }
                }
            }
        }
    }
}
