using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public enum TileStatus {
        CLOSED, OPEN, FLAGGED
    }

    public class Tile
    {
        public Tile(bool mine = false)
        {
            Status = TileStatus.CLOSED;
            Mine = mine;
        }

        public bool Open()
        {
            Status = TileStatus.OPEN;
            return !Mine;
        }

        public bool Mine
        {
            get;
            set;
        }

        public TileStatus Status
        {
            get;
            set;
        }
    }
}
