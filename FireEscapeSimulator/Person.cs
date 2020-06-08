using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{
    public enum Orientation { North, South, West, East }

    [Serializable]
    class Person
    {
        // ------------------- Enumerations --------------------

        public enum DamageType { Fire, Smoke, Falling }

        protected enum DestinationType { Exit, Window, FireExtinguisher, Fire }

        // ---------------------- Fields -----------------------

        // Status variables
        protected Point location;
        protected int speed;
        protected int panic;
        protected int health;
        protected bool jumped;
        protected Orientation orientation;

        // Pathfinding variables
        protected List<Tile> shortestPath;
        protected bool shouldFindNewPath;
        protected Point? customTarget;
        protected bool targetReached;

        // Other variables
        protected bool escaped = false;
        protected Random rng;
        protected IMapInfo mapInfo;

        // -------------------- Properties ---------------------
        public Point Location { get { return location; } set { location = value; } }
        public int Speed { get { return speed; } set { speed = value; } }
        public int Panic { get { return panic; } set { panic = value; } }
        public int Health { get { return health; } set { health = value; } }
        public bool Jumped { get { return jumped; } set { jumped = value; } }
        public bool Escaped { get { return escaped; } }
        public Orientation Orientation { get { return orientation; } }

        // -------------------- Constructor --------------------
        public Person(Point location, IMapInfo mapInfo, int speed = 1, int panic = 0)
        {
            rng = new Random();

            this.location = location;
            this.mapInfo = mapInfo;
            this.orientation = Orientation.South;
            this.speed = speed;
            this.health = 100; // Full health by default
            jumped = false;

            if (panic != 0)
                this.panic = panic;
            else
                panic = rng.Next(1, 41);

            shouldFindNewPath = true;
            targetReached = false;
            escaped = false;
        }


        // ---------------------- Methods ----------------------

        /// <summary>
        /// Decreases the health of a person
        /// </summary>
        /// <param name="amount">The amount to be substracted from the person's current health</param>
        public void DecreaseHealth(DamageType damageType)
        {
            if (damageType == DamageType.Smoke)
            {
                if (panic < 50)
                    health -= 10;
                else // Take more damage from smoke when panicked and breathing hard
                    health -= 15;
            }  
            else if (damageType == DamageType.Fire)
                health -= 34;
            else if (damageType == DamageType.Falling)
                health -= 100;

            if (health < 0)
                health = 0;
        }

        /// <summary>
        /// Increase the panic level of a person
        /// </summary>
        /// <param name="damageType"></param>
        public void IncreasePanicLevel(Tile tile)
        {
            bool nearSmoke = false;
            bool nearFire = false;
            for(int i=-1; i<=1; i++)
            {
                for(int j=-1; j<=1; j++)
                {
                    if(i!=0 && j!=0 && ((TileFloor)tile).HasSmoke)
                    {
                        nearSmoke = true;
                    }
                    else if (i != 0 && j != 0 && ((TileFloor)tile).OnFire)
                    {
                        nearFire = true;
                    }
                    else if (i==0 && j ==0 && ((TileFloor)tile).HasSmoke)
                    {
                        panic += 10;
                    }
                    else if (i == 0 && j == 0 && ((TileFloor)tile).OnFire)
                    {
                        panic += 35;
                    }
                }
            }
            if (nearSmoke)
            {
                panic += 5;
            }
            if (nearFire)
            {
                panic += 10;
            }
            if (panic > 100)
            {
                panic = 100;
            }
        }

        /// <summary>
        /// Chooses the shortest path to a particular destination, indicated by the DestinationType enum.
        /// </summary>
        /// <param name="destination">Indicates the type of destination that a path needs to be found to.</param>
        /// <returns>Returns the shortest path to the desired destination type, null if no path is found/walkable.</returns>
        protected List<Tile> ChoosePath(DestinationType destination)
        {
            List<List<Tile>> paths = null;
            if (destination == DestinationType.Exit)
                paths = Simulation.PathFactory.FindPathsToExits(location);
            else if (destination == DestinationType.Window)
                paths = Simulation.PathFactory.FindPathsToWindows(location);
            else if (destination == DestinationType.FireExtinguisher)
                paths = Simulation.PathFactory.FindPathsToFireExtinguishers(location);
            else if (destination == DestinationType.Fire)
                paths = Simulation.PathFactory.FindPathsToFire(location);

            List<Tile> bestPath = null;

            foreach (List<Tile> path in paths)
            {
                if (path != null)
                {
                    if (bestPath == null)
                        bestPath = path;
                    else if (path.Count < bestPath.Count)
                        bestPath = path;
                }
            }

            return bestPath;
        }

        /// <summary>
        /// This method will move the person to the next Tile on the shortestPath list if possible (no obstacles detected). Otherwise it will attempt to find a new path towards any destination. If a manual target location is set, the Person will instead follow a path towards this location.
        /// </summary>
        /// <returns> 
        /// True if the target has been reached or a move was successfull (not blocked).
        /// False if all possible paths are blocked and no paths can be found.
        /// </returns>
        public virtual bool Move()
        {
            for (int i = 0; i < speed; i++)
            {
                if (!targetReached) // Has not yet reached custom target, if one is set. False by default.
                {
                    if (shouldFindNewPath)
                    {
                        if (customTarget != null) // If a custom target was set, go there instead.
                            shortestPath = Simulation.PathFactory.FindShortestPath(location, (Point)customTarget);
                        else
                        { // Otherwise, go to the exit
                            shortestPath = ChoosePath(DestinationType.Exit);
                            shouldFindNewPath = false;
                        }
                    }

                    if (shortestPath == null) // Check if a valid path to the exit could be found. If not, random chance to jump out of window instead.
                    {
                        List<Tile> windowPath = ChoosePath(DestinationType.Window);

                        if (windowPath == null)
                            return false; // No possible path to window found.
                        else
                        {
                            if (rng.Next(1, 101) > (100 - panic) * mapInfo.GetFloorNumber()) // The higher the panic level, the bigger the chance a Person decides to jump.
                            {
                                shortestPath = windowPath;
                                shouldFindNewPath = false;
                                targetReached = false;
                                Move();
                            }
                        }
                    }
                    else // A valid path exists to the destination
                    {
                        if (shortestPath.Count == 0) // Shortest path has no more elements, destination has been reached.
                        {
                            targetReached = true;

                            if (customTarget == null) // If no custom target was set, the destination was an exit and the person has escaped.
                                escaped = true;

                            return true;
                        }
                        else // Shortest path still has elements, destination has not yet been reached.
                        {
                            if (CheckForFire()) // If fire or obstacles are in the way, find new path.
                                shouldFindNewPath = true;
                            else // Otherwise, make move and change orientation
                            { 
                                MoveTile();
                                return true;
                            }
                        }
                    }
                }
                else // Custom target has been reached. Reset custom target and to null and indicate a new path must be found.
                {
                    customTarget = null;
                    shouldFindNewPath = true;
                    targetReached = false;

                    return false; // No new path was calculated
                }
            }

            return true;
        }

        /// <summary>
        /// Searches up to 5 tiles ahead of the remaining path for obstacles or fires.
        /// </summary>
        /// <returns>Returns true if something was in the way, otherwise returns false.</returns>
        protected bool CheckForFire()
        {
            for (int j = 0; j < Math.Min(shortestPath.Count, 5); j++)
            {
                if (shortestPath[j] is TileFloor)
                {
                    if ((((TileFloor)shortestPath[j]).HasFurniture != TileFloor.furniture.None) || ((TileFloor)shortestPath[j]).OnFire)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Changes a persons orientation based on their last tile.
        /// </summary>
        protected void SetOrientation()
        {
            Point next = shortestPath.First().Location;

            if (next.X > location.X)
            {
                orientation = Orientation.East;
            }
            else if (next.X < location.X)
            {
                orientation = Orientation.West;
            }
            else if (next.Y < location.Y)
            {
                orientation = Orientation.North;
            }
            else if (next.Y > location.Y)
            {
                orientation = Orientation.South;
            }
        }

        /// <summary>
        /// Sets the Person object's customTarget field to the provided Point; also resets the firstPathSearched field to make sure the Person object will recalculate a new path to the custom destination.
        /// </summary>
        /// <param name="p"> The Point representing the location in the grid map. </param>
        public virtual void SetCustomTarget(Point p)
        {
            customTarget = p;
            shouldFindNewPath = true;
            targetReached = false;
        }

        /// <summary>
        ///  Moves forward along the Person's path and sets the orientation.
        /// </summary>
        protected void MoveTile()
        {
            if (shortestPath.First() is TileDoor)
                if (!((TileDoor)shortestPath.First()).IsOpen)
                    ((TileDoor)shortestPath.First()).IsOpen = true;

            location = shortestPath.First().Location;
            shortestPath.Remove(shortestPath.First());

            if (shortestPath.Count > 1)
                SetOrientation();
        }
    }
}
