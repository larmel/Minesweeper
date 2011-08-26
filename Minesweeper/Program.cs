using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Minesweeper ***");
            do
            {
                Console.WriteLine("Input board <rows>, <cols>, <mines>");
                var input = Console.ReadLine().Split(',');
                try
                {
                    var rows = int.Parse(input[0]);
                    var cols = int.Parse(input[1]);
                    var mine = int.Parse(input[2]);

                    StartNewGame(rows, cols, mine);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }
            while (true);
        }

        private static void StartNewGame(int rows, int cols, int mines)
        {
            var board = new Board(rows, cols, mines);

            do
            {
                Console.WriteLine(board.Print());

                var input = Console.ReadLine().Split(' ');

                var command = input[0].Trim();
                var row = int.Parse(input[1].Trim());
                var col = int.Parse(input[2].Trim());

                switch (command)
                {
                    case "o":
                        board.Open(row, col);
                        break;
                    case "f":
                        board.Flag(row, col);
                        break;
                }
            }
            while (true);
        }
    }
}
