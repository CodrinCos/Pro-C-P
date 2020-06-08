using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    public class TileFloor : Tile
    {
        // ------------------- Enumerations --------------------
        public enum furniture { None = 0, Chair = 1, Table = 2 }

        // ---------------------- Fields -----------------------
        protected bool onFire;
        protected bool hasSmoke;
        protected furniture hasFurniture;
        protected FireExtinguisher presentFireExtinguisher;

        // -------------------- Properties ---------------------
        public bool OnFire { get { return onFire; } set { onFire = value; } }
        public bool HasSmoke { get { return hasSmoke; } set { hasSmoke = value; } }
        public FireExtinguisher PresentFireExtinguisher { get { return presentFireExtinguisher; } set { presentFireExtinguisher = value; } }
        public furniture HasFurniture { get { return hasFurniture; } set { hasFurniture = value; } }

        // -------------------- Constructor --------------------
        public TileFloor(Point location, int occupied, bool fire = false, bool smoke = false, FireExtinguisher presentFireExtinguisher = null)
        {
            this.location = location;
            this.onFire = fire;
            this.HasSmoke = smoke;

            if (occupied == 0)
                hasFurniture = furniture.None;
            else if (occupied == 1)
                hasFurniture = furniture.Chair;
            else
                hasFurniture = furniture.Table;

            this.presentFireExtinguisher = presentFireExtinguisher;
        }

        public TileFloor(bool fire, bool smoke)
        {
            onFire = fire;
            hasSmoke = smoke;
        }
    }
}
