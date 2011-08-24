using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Minesweeper;

namespace MinesweeperTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void Board_has_specified_properties()
        {
            Board b = new Board(10, 20);
            Assert.AreEqual(b.Rows, 10);
            Assert.AreEqual(b.Cols, 20);
            Assert.AreEqual(b.Mines, 0);

            b.SetNumberOfMines(10);
            Assert.AreEqual(b.Mines, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Only_valid_sizes_are_accepted()
        {
            Board b = new Board(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Only_valid_number_of_mines_are_accepted()
        {
            Board b = new Board(10, 10);
            b.SetNumberOfMines(10);
            Assert.AreEqual(10, b.Mines);

            b.SetNumberOfMines(101);
            Assert.Fail();
        }

        [TestMethod]
        public void Adjacent_tiles_are_opened()
        {
            Board board = new Board(3, 3);
            board.Open(1, 1);
            Assert.AreEqual(TileStatus.OPEN, board.Grid[0, 0].Status);
        }

        [TestMethod]
        public void Border_cases_are_handled()
        {
            Board board = new Board(3, 3);
            board.Open(0, 0);
            Assert.AreEqual(TileStatus.OPEN, board.Grid[0, 1].Status);
        }

        [TestMethod]
        public void Opening_a_free_tile_should_return_true()
        {
            Board board = Board.FromString(
                "[ ]"
            );

            bool open = board.Open(0, 0);

            Assert.IsTrue(open);
        }

        [TestMethod]
        public void Opening_a_mine_should_return_false()
        {
            Board board = Board.FromString(
                "[x]"
            );

            bool open = board.Open(0, 0);

            Assert.IsFalse(open);
        }
    }
}
