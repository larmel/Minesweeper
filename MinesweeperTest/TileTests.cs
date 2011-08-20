using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper;

namespace MinesweeperTests
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void TestTiled()
        {
            // No mine
            Tile t = new Tile();
            Assert.AreEqual(TileStatus.CLOSED, t.Status);
            Assert.IsTrue(t.Open());
            Assert.AreEqual(TileStatus.OPEN, t.Status);

            // Should hit a mine
            Tile t2 = new Tile(true);
            Assert.AreEqual(TileStatus.CLOSED, t2.Status);
            Assert.IsFalse(t2.Open());
            Assert.AreEqual(TileStatus.OPEN, t2.Status);
        }
    }
}
