﻿using System;
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
            var b = new Board(10, 20, 30);
            Assert.AreEqual(b.Rows, 10);
            Assert.AreEqual(b.Cols, 20);
            Assert.AreEqual(b.Mines, 30);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Only_valid_sizes_are_accepted()
        {
            new Board(0, 1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Only_valid_number_of_mines_are_accepted()
        {
            new Board(10, 10, 10*10 + 1);
        }

        [TestMethod]
        public void Opening_a_free_tile_should_return_true()
        {
            var board = Board.FromString(
                "[ ]"
            );

            bool open = board.Open(0, 0);

            Assert.IsTrue(open);
        }

        [TestMethod]
        public void Opening_a_mine_should_return_false()
        {
            var board = Board.FromString(
                "[x]"
            );

            bool open = board.Open(0, 0);

            Assert.IsFalse(open);
        }

        [TestMethod]
        public void All_adjacent_tiles_are_opened_when_field_contains_no_nearby_mines()
        {
            var board = Board.FromString(
                "[ ] [ ] [ ]",
                "[ ] [ ] [ ]",
                "[ ] [ ] [ ]"
            );

            board.Open(1, 1);

            foreach (var t in board.Grid)
            {
                Assert.AreEqual(TileStatus.OPEN, t.Status);
            }
        }

        [TestMethod]
        public void All_adjacent_tiles_are_opened_when_opening_empty_tile_in_a_corner()
        {
            var board = Board.FromString(
                "[ ] [ ]",
                "[ ] [ ]"
            );

            board.Open(0, 0);

            foreach (var t in board.Grid)
            {
                Assert.AreEqual(TileStatus.OPEN, t.Status);
            }
        }

    }
}
