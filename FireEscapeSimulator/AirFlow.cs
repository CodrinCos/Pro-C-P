using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    [Serializable]
    class AirFlow
    {
        // ---------------------- Fields -----------------------
        public int[,] airPathways;
        private List<Point> windows;
        private List<List<Tile>> paths;

        // -------------------- Constructors --------------------
        public AirFlow(PathFactory pf)
        {
            airPathways = new int[pf.map.GetLength(0), pf.map.GetLength(1)];
            paths = new List<List<Tile>>();
            windows = new List<Point>();
            Initialize(pf);
        }

        // ---------------------- Methods ----------------------
        /// <summary>
        /// Uses the PathFactory class to get the airflow from every open window to the rest, and sets the values in the airPathways accordingly.
        /// </summary>
        /// <param name="pf"></param>
        public void Initialize(PathFactory pf)
        {
            foreach(Tile t in pf.map)
            {
                airPathways[t.Location.X, t.Location.Y] = 0;
                if (t is TileWindow && ((TileWindow)t).IsOpen)
                    windows.Add(t.Location);
            }

            for (int i = 0; i < windows.Count(); i++)
                for (int j = windows.Count()-1; j > i; j--)
                    paths.Add(pf.FindShortestPath(windows[i], windows[j]));

            foreach(List<Tile> path in paths)
                if(path != null)
                    if(path.Count() > 0)
                        foreach(Tile t in path)
                            airPathways[t.Location.X, t.Location.Y] = 1;
        }
    }
}
