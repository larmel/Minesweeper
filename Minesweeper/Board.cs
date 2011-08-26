using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Board
    {
        public int Mines { get; private set; }

        public Tile[,] Grid { get; private set; }

        public Board(int rows, int cols, int mines)
        {
            if (rows < 1 || cols < 1)
                throw new ArgumentException("Board size can not be less than 1x1");
            if (mines > rows * cols)
                throw new ArgumentException("Illegal number of mines");
            SetDimensions(rows, cols);
            SetNumberOfMines(mines);
        }

        private void SetDimensions(int rows, int cols)
        {
            Grid = new Tile[rows, cols];
            for (int r = 0; r < rows; ++r)
            {
                for (int c = 0; c < cols; ++c)
                {
                    Grid[r, c] = new Tile();
                }
            }
        }

        private void SetNumberOfMines(int count)
        {
            Mines = count;
            var random = new Random();

            var placed = 0;
            while (placed < count)
            {
                var row = random.Next() % Rows;
                var col = random.Next() % Cols;
                if (!Grid[row, col].Mine)
                {
                    Grid[row, col].Mine = true;
                    placed++;
                }
            }
        }

        public bool Open(int row, int col)
        {
            var open = Grid[row, col].Open();
            if (!open) return false;
            if (NumberOfSurroundingMines(row, col) == 0)
            {
                for (int r = row - 1; r <= row + 1; ++r)
                {
                    for (int c = col - 1; c <= col + 1; ++c)
                    {
                        if (IsInside(r, c))
                            Grid[r, c].Open();
                    }
                }
            }
            return true;
        }

        public void Flag(int row, int col)
        {
            if (Grid[row, col].Status != TileStatus.OPEN)
                Grid[row, col].Status = (Grid[row, col].Status == TileStatus.FLAGGED) ? TileStatus.CLOSED : TileStatus.FLAGGED;
        }

        private bool IsInside(int r, int c)
        {
            return r >= 0 && c >= 0 && r < Rows && c < Cols;
        }

        public int Rows
        {
            get { return Grid.GetLength(0); }
        }

        public int Cols
        {
            get { return Grid.GetLength(1); }
        }

        /**
         * Creates a board from string input like this
         * [ ] [x] [ ]
         * [x] [ ] [ ]
         * [ ] [x] [x]
         */
        public static Board FromString(params string[] s)
        {
            var rows = s.Length;
            var cols = s[0].Count((c) => c == '[');

            var board = new Board(rows, cols, 0);
            for (var r = 0; r < rows; ++r)
            {
                for (var c = 0; c < cols; ++c)
                {
                    var minechar = s[r].ElementAt(1 + "] [ ".Length * c);
                    if (minechar == 'x')
                        board.Grid[r, c].Mine = true;
                }
            }
            return board;
        }

        public string Print()
        {
            string board = "";
            for (int row = 0; row < Rows; ++row)
            {
                for (int col = 0; col < Cols; ++col)
                {
                    switch (Grid[row, col].Status)
                    {
                        case TileStatus.CLOSED:
                            board += "[ ]";
                            break;
                        case TileStatus.FLAGGED:
                            board += "[F]";
                            break;
                        case TileStatus.OPEN:
                            board += string.Format(" {0} ", NumberOfSurroundingMines(row, col));
                            break;
                    }
                    if (col != Cols - 1)
                        board += " ";
                }
                board += Environment.NewLine;
            }
            return board;
        }

        private int NumberOfSurroundingMines(int row, int col)
        {
            int count = 0;
            for (int r = row - 1; r <= row + 1; ++r)
            {
                for (int c = col - 1; c <= col + 1; ++c)
                {
                    if (IsInside(r, c) && Grid[r, c].Mine)
                        count++;
                }
            }
            return count;
        }
    }
}
