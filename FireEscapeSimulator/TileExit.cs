using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    class TileExit : Tile
    {
        // ---------------------- Fields -----------------------
        private bool isObstructed;

        // -------------------- Properties ---------------------
        public bool IsObstructed { get { return isObstructed; } set { isObstructed = value; } }

        // -------------------- Constructor --------------------
        public TileExit(Point location, bool isObs)
        {
            this.location = location;
            this.isObstructed = isObs;
        }
    }
}
