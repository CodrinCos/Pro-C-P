using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    public abstract class Tile
    {
        // ---------------------- Fields -----------------------
        protected Point location;

        // -------------------- Properties ---------------------
        public Point Location { get { return location; } }

        // -------------------- Constructor --------------------

        public Tile(Point location)
        {
            this.location = location;
        }

        public Tile() // Required in sub-classes
        {

        }
    }
}

