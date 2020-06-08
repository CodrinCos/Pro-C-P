using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace FireEscapeSimulator
{
    [Serializable]
    class Map
    {
        // ---------------------- Fields -----------------------
        private Tile[,] tilesArray;
        private int fireSpreadRate;
        private Random rand;
        private List<List<Tile>> rooms;
        private int heightOfMap;
        private int lengthOfMap;
        private int floorLevel;

        private int totalFloorTiles; // Not counting walls
        private int tilesOnFire;

        // -------------------- Properties ---------------------
        public Tile[,] TilesArray { get { return tilesArray; } set { tilesArray = value; } }
        public List<List<Tile>> Rooms { get { return rooms; } }
        public int FireSpreadRate { get { return fireSpreadRate; } set { fireSpreadRate = value; } }
        public int HeightOfMap { get { return heightOfMap; } set { heightOfMap = value; } }
        public int LengthOfMap { get { return lengthOfMap; } set { lengthOfMap = value; } }
        public int FloorLevel { get { return floorLevel; } }
        public AirFlow Airflow { get; set; }

        // -------------------- Constructors --------------------
        public Map()
        {
            fireSpreadRate = 4;
            rand = new Random();
            tilesOnFire = 0;
        }

        // ---------------------- Methods ----------------------
        #region Fire controls
        /// <summary>
        /// Loops through all the tiles. If a tile is not already on fire, is not an exit and is not a wall, the method determines if the Tile should catch on fire or not.
        /// </summary>
        public void SpreadFire()
        {
            for (int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for (int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    Tile t = tilesArray[i, j];

                    if (t is TileFloor)
                    {
                        if (!((TileFloor)t).OnFire)
                        {
                            DetermineFire(t);
                        }
                        if (!((TileFloor)t).OnFire && !((TileFloor)t).HasSmoke)
                        {
                            DetermineSmoke(t);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This method calculates the chance of a tile catching on fire based on the amount of adjacent tiles that are on fire, the fire spread rate, and randomness.
        /// This method also adds fire when a new tile burns. If the percentage of tiles that are in fire is more than 40% than the smoke will have the dimention of 2 tiles.
        /// </summary>
        /// <param name="tile">A Tile object</param>
        /// <returns>Returns a boolean indicating if the tile has caught on fire (true) or not (false).</returns>
        private bool DetermineFire(Tile tile)
        {
            int count = FireCount(tile.Location);
            int chance = rand.Next(0, 101);

            if (count != 0)
            {
                if(Airflow.airPathways[tile.Location.X, tile.Location.Y] == 1)
                {
                    if(chance <= 15)
                    {
                        SetTileOnFire(tilesArray[tile.Location.X, tile.Location.Y]);
                        return true;
                    }
                }
                if (chance <= 3 * count + fireSpreadRate + (WindowCount(tile.Location) * 2))
                {
                    SetTileOnFire(tilesArray[tile.Location.X, tile.Location.Y]);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method determines wether a tile should have smoke based on the surrounding tiles.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private bool DetermineSmoke(Tile tile)
        {
            double aux = GetPercentageOnFire();
            int count = FireCount(tile.Location);
            Point t;

            int chance = rand.Next(0, 101);
            if(count > 0)
            {
                ((TileFloor)tile).HasSmoke = true;
                return true;
            }

            if(aux >= 40)
            {
                for(int i = -1; i <= 1; i+=2)
                {
                    for(int j = -1; i<= 1; i+=2)
                    {
                        t = tile.Location;
                        t.X += i;
                        t.Y += j;
                        if(t.X + i > 0 && t.X + i < tilesArray.GetLength(0)-1 && t.Y + j > 0 && t.Y + j < tilesArray.GetLength(1) - 1)
                            if(FireCount(t) > 0)
                                ((TileFloor)tile).HasSmoke = true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Counts window tiles around a specified Tile.
        /// </summary>
        /// <param name="position">The location of the Tile around which this method will search for windows.</param>
        /// <returns>Number of windows found around the Tile.</returns>
        private int WindowCount(Point position)
        {
            int windows = 0;
            List<Point> walls = new List<Point>();
            int px = position.X;
            int py = position.Y;

            for (int d = 0; d < 2; d++) // d for distance from tile
            {
                for (int x = px - d; x < px + d + 1; x++)
                {
                    if (tilesArray[x, py - d] is TileWindow)
                        windows++;
                    else if (tilesArray[x, py - d] is TileWall)
                        walls.Add(new Point(x, py - d));

                    if (tilesArray[x, py + d] is TileWindow)
                        windows++;
                }

                for (int y = py - d + 1; y < py + d; y++)
                {
                    if (tilesArray[px - d, y] is TileWindow)
                        windows++;

                    if (tilesArray[px + d, y] is TileWindow)
                        windows++;
                }
            }

            return windows;
        }

        /// <summary>
        /// Counts the number of adjacent tiles that are on fire and returns it.
        /// </summary>
        /// <param name="position">A Point object representing the position of a tile in the grid</param>
        /// <returns>An integer representing the number of tiles that are on fire around the prodived location</returns>
        private int FireCount(Point position)
        {
            int counter = 0;
            int walls = 0;
            int x = position.X;
            int y = position.Y;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(x + i < tilesArray.GetLength(0) - 2 && y + j < tilesArray.GetLength(1) - 2)
                    if (tilesArray[x + i, y + j] is TileFloor)
                    {
                        if (((TileFloor)tilesArray[x + i, y + j]).OnFire)
                            counter++;
                        if (tilesArray[x + i, y + j] is TileWall)
                            walls++;
                    }
                }
            }

            if ((walls == 1 || walls == 2) && counter != 0)
                counter = 10;

            return counter;
        }

        /// <summary>
        /// Sets a Tile's OnFire property to true.
        /// </summary>
        /// <param name="t"></param>
        public void SetTileOnFire(Tile t)
        {
            if (t is TileFloor)
            {
                ((TileFloor)t).OnFire = true;
                tilesOnFire++;
            }
        }
        #endregion

        #region Map creation
        /// <summary>
        /// Fills the tilesArray with values obtained from a .txt file.
        /// </summary>
        public void CreateMap(string map)
        {
            string[] lines = null;
            int length, height;
            rooms = new List<List<Tile>>();

            switch (map)
            {
                case "fontys":
                    lines = ReadAllResourceLines(Properties.Resources.FontysFloor);
                    break;
                case "test":
                    lines = ReadAllResourceLines(Properties.Resources.TestMap);
                    break;
                default:
                    if (map != null)
                        if (map.Contains("path:"))
                        {
                            lines = File.ReadAllLines(map.Substring(5));
                        }
                        else
                            lines = ReadAllResourceLines(Properties.Resources.FontysFloor);
                    break;
            }

            string[] parts = lines[0].Split(' ');
            length = Convert.ToInt32(parts[0]);
            height = Convert.ToInt32(parts[1]);
            floorLevel = Convert.ToInt32(parts[2]);
            LengthOfMap = length;
            HeightOfMap = height;
            tilesArray = new Tile[length, height];
            for (int i = 0; i < height; i++)
            {
                parts = lines[i + 1].Split(' ');
                for (int j = 0; j < length; j++)
                {
                    switch (parts[j])
                    {
                        default:
                            break;
                        case "w": // Wall
                            tilesArray[j, i] = new TileWall(new Point(j, i));
                            break;
                        case "e": // Exit
                            tilesArray[j, i] = new TileExit(new Point(j, i), false);
                            totalFloorTiles++;
                            break;
                        case "n": // Floor (None)
                            tilesArray[j, i] = new TileFloor(new Point(j, i), 0);
                            totalFloorTiles++;
                            break;
                        case "r": // Window (Raam. What? The W was taken already...)
                            if (rand.Next(100) > 75)
                                tilesArray[j, i] = new TileWindow(new Point(j, i), true);
                            else
                                tilesArray[j, i] = new TileWindow(new Point(j, i), false);
                            break;
                        case "d": // Door
                            if (rand.Next(100) > 75)
                                tilesArray[j, i] = new TileDoor(new Point(j, i), true);
                            else
                                tilesArray[j, i] = new TileDoor(new Point(j, i), false);                            
                            break;
                        case "x": // Fire Extinguisher
                            tilesArray[j, i] = new TileFloor(new Point(j, i), 0);
                            totalFloorTiles++;
                            ((TileFloor)tilesArray[j, i]).PresentFireExtinguisher = new FireExtinguisher();
                            break;
                        case "c": // chair
                            tilesArray[j, i] = new TileFloor(new Point(j, i), 1);
                            break;
                        case "t": // table 
                            tilesArray[j, i] = new TileFloor(new Point(j, i), 2);
                            break;
                    }
                }
            }

            if(tilesArray[2,4] is TileFloor)
                ((TileFloor)tilesArray[2, 4]).OnFire = true;
            DetermineRooms();
        }

        /// <summary>
        /// Populates the list rooms with a list for each room in the map.
        /// </summary>
        private void DetermineRooms()
        {
            int roomNr = 1;
            int[,] m = PopulateIntArray();
            List<int> room = new List<int>();

            for (int i = 1; i < tilesArray.GetLength(0)-1; i++)
            {
                for (int j = 1; j < tilesArray.GetLength(1)-1; j++)
                {
                    if (m[i,j] == -1)
                    {
                        if (m[i - 1, j] == 0)
                        {
                            FloodFill(m, new Point(i - 1, j), roomNr);
                            roomNr++;
                        }      
                        else if(m[i, j - 1] == 0)
                        {
                            FloodFill(m, new Point(i, j - 1), roomNr);
                            roomNr++;
                        }

                        if (m[i + 1, j] == 0)
                        {
                            FloodFill(m, new Point(i + 1, j), roomNr);
                            roomNr++;
                        }
                        else if (m[i, j + 1] == 0)
                        {
                            FloodFill(m, new Point(i, j + 1), roomNr);
                            roomNr++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Floodfills an area until there are no more available floor tiles.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <param name="n"></param>
        private void FloodFill(int[,] m, Point p, int n)
        {
            int i, j;
            rooms.Add(new List<Tile>());
            Queue<Point> room = new Queue<Point>();
            room.Enqueue(p);
            m[p.X, p.Y] = n;

            while (room.Count != 0)
            {
                for (i = -1; i <= 1; i ++)
                    for (j = -1; j <= 1; j ++)
                    {
                        if (m[room.First().X + i, room.First().Y + j] == 0)
                        {
                            room.Enqueue(new Point(room.First().X + i, room.First().Y + j));
                            m[room.First().X + i, room.First().Y + j] = n;
                        }
                    }

                rooms[n - 1].Add(tilesArray[room.First().X, room.First().Y]);
                room.Dequeue();
            }
        }

        /// <summary>
        /// Removes the hallway from the rooms list. Might not be needed.
        /// </summary>
        private void RemoveHallway()
        {
            int hallway = 0;
            int x = 0;

            foreach (List<Tile> room in rooms)
            {
                foreach (Tile t in room)
                {
                    for (int i = -1; i <= 1; i += 2)
                        for (int j = -1; j <= 1; j += 2)
                            if (tilesArray[t.Location.X + i, t.Location.Y + j] is TileExit)
                                hallway = x;
                }

                x++;
            }

            rooms.RemoveAt(hallway);
        }

        /// <summary>
        /// Helper class for determining rooms
        /// </summary>
        /// <returns></returns>
        private int[,] PopulateIntArray()
        {
            int[,] m = new int[tilesArray.GetLength(0), tilesArray.GetLength(1)];

            for (int i = 0; i < tilesArray.GetLength(0); i++)
            {
                for (int j = 0; j < tilesArray.GetLength(1); j++)
                {
                    if (tilesArray[i, j] is TileDoor)
                        m[i, j] = -1;
                    else if (tilesArray[i, j] is TileExit)
                        m[i, j] = -1;
                    else if (tilesArray[i, j] is TileWall || tilesArray[i, j] is TileWindow)
                        m[i, j] = -2;
                    else
                        m[i, j] = 0;
                }
            }

            return m;
        }

        /// <summary>
        /// Helper method for reading a resource text file.
        /// </summary>
        /// <param name="resourceText"></param>
        /// <returns></returns>
        private string[] ReadAllResourceLines(string resourceText)
        {
            using (StringReader reader = new StringReader(resourceText))
            {
                return EnumerateLines(reader).ToArray();
            }
        }

        /// <summary>
        /// Helper method for reading a resource text file.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private IEnumerable<string> EnumerateLines(TextReader reader)
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }
        #endregion

        /// <summary>
        /// Calculates the percentage of tiles (not counting walls) that are on fire.
        /// </summary>
        /// <returns></returns>
        public double GetPercentageOnFire()
        {
            return Math.Round((double)tilesOnFire / totalFloorTiles * 100, 1);
        }

        /// <summary>
        /// Gets a tile from a specified position.
        /// </summary>
        /// <param name="location">The location of the desired tile.</param>
        /// <returns>Will return null if no Tile could be found at the specified location, otherwise it will return the found Tile.</returns>
        public Tile GetTile(Point location)
        {
            for (int i = 0; i < tilesArray.GetLength(0); i++)
                for (int j = 0; j < tilesArray.GetLength(1); j++)
                    if (tilesArray[i, j].Location == location)
                        return tilesArray[i, j];

            return null;
        }
    }
}
