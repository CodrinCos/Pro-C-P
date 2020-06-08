using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    class TileWindow : Tile
    {
        // ---------------------- Fields -----------------------
        private bool isOpen;

        // -------------------- Properties ---------------------
        public bool IsOpen { get { return isOpen; } set { isOpen = value; } }

        // -------------------- Constructor --------------------
        public TileWindow(Point location, bool isOpen)
        {
            this.location = location;
            this.isOpen = isOpen;
        }
    }
}
