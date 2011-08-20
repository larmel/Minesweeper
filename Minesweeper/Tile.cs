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

        private bool mine;
        public bool Mine
        {
            get
            {
                return mine;
            }
            set
            {
                mine = value;
            }
        }

        private TileStatus status;
        public TileStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
    }
}
