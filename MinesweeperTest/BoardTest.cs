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
        public void BoardHasSpecifiedProperties()
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
        public void OnlyValidSizesAreAccepted()
        {
            Board b = new Board(0, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OnlyValidNumberOfMinesAreAccepted()
        {
            Board b = new Board(10, 10);
            b.SetNumberOfMines(10);
            Assert.AreEqual(10, b.Mines);

            b.SetNumberOfMines(101);
            Assert.Fail();
        }

        [TestMethod]
        public void AdjacentTilesAreOpened()
        {
            Board board = new Board(3, 3);
            board.Open(1, 1);
            Assert.AreEqual(TileStatus.OPEN, board.Grid[0, 0].Status);
        }

        [TestMethod]
        public void BorderCasesAreHandled()
        {
            Board board = new Board(3, 3);
            board.Open(0, 0);
            Assert.AreEqual(TileStatus.OPEN, board.Grid[0, 1].Status);
        }

        [TestMethod]
        public void Opening_a_free_tile_should_return_true()
        {
            Board board = Board.FromString(
                "[ ] [ ] [ ]" +
                "[ ] [x] [x]" +
                "[x] [ ] [ ]"
            );

            bool open = board.Open(0, 2);

            Assert.IsTrue(open);
        }
    }
}
