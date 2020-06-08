using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    public class TileDoor : TileFloor
    {
        // ---------------------- Fields -----------------------
        private bool isOpen;

        // -------------------- Properties ---------------------
        public bool IsOpen { get { return isOpen; } set { isOpen = value; } }

        // -------------------- Constructor --------------------
        public TileDoor(Point location, bool isOpen, bool fire = false, bool smoke = false, bool occupied = false) : base ( fire,  smoke)
        {
            this.location = location;
            this.IsOpen = isOpen;
        }
    }
}
