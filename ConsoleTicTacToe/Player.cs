using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTicTacToe
{
    class Player
    {
        private string name;
        private bool winner;
        private char character;
        private bool[,] takenSpots;


        public Player(string name, char character)
        {
            this.Name = name;
            this.Character = character;
            this.Winner = false;
            // Nine posible positions with the coordenates
            this.TakenSpots = new bool[3, 3];
        }

        public bool[,] TakenSpots { get => takenSpots; set => takenSpots = value; }
        public string Name { get => name; set => name = value; }
        public bool Winner { get => winner; set => winner = value; }
        public char Character { get => character; set => character = value; }
    }
}
