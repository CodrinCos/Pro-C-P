using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEscapeSimulator
{
    [Serializable]
    class Staff : Person
    {
        // ---------------------- Events -----------------------
        [field: NonSerialized]
        public event DropFireExtinguisherEventHandler DropFireExtinguisher;
        [field: NonSerialized]
        public event SprayFireExtinguisherEventHandler SprayFireExtinguisher;

        // ---------------------- Fields -----------------------
        private bool goingForExtinguisher;
        private bool fightingFire;
        private FireExtinguisher equipedFireExtinguisher;

        // -------------------- Properties ---------------------
        public FireExtinguisher EquipedFireExtinguisher { get { return equipedFireExtinguisher; } set { equipedFireExtinguisher = value; } }

        // -------------------- Constructor --------------------
        public Staff(Point location, IMapInfo room, int speed = 1, int panic = 0) : base(location, room, speed, panic)
        {
            goingForExtinguisher = true;
            fightingFire = false;
            equipedFireExtinguisher = null;
        }

        // ---------------------- Methods ----------------------
        
        /// <summary>
        /// When a staff person is close to the fire this method will check the tile behind in order to know if can back off from the fire.
        /// </summary>
        /// <param name="checkedOrientations">Orientations that have already been checked. Applies to the recursive call.</param>
        private void BackOffFromFire(List<Orientation> checkedOrientations)
        {
            #region Get away from the fire behaviour
            Point p = new Point(0, 0);

            switch (orientation)
            {
                case Orientation.North:
                    p = new Point(location.X, location.Y + 1);
                    break;
                case Orientation.East:
                    p = new Point(location.X - 1, location.Y);
                    break;
                case Orientation.South:
                    p = new Point(location.X, location.Y - 1);
                    break;
                case Orientation.West:
                    p = new Point(location.X + 1, location.Y);
                    break;
                default:
                    break;
            }

            Tile tileBehind = mapInfo.GetTile(p);
            bool validTileFound = false;

            if (!(tileBehind is TileWall) && !(tileBehind is TileWindow)) // Tile behind was not a wall or a window, still need to check if it's not on fire or otherwise obstructed.
            {
                if (tileBehind is TileFloor)
                {
                    if (!(((TileFloor)tileBehind).OnFire) && (((TileFloor)tileBehind).HasFurniture != TileFloor.furniture.None)) // Tile behind was clear, we can move to it safely.
                    {
                        location = p; // Make move
                        validTileFound = true;
                    }
                }
            }

            if (!validTileFound) // Tile behind is a wall, window, or blocked, must find alternative.
            {
                checkedOrientations.Add(orientation); // Add current orientation to the list that have already been checked.

                if (orientation == Orientation.North || orientation == Orientation.South)
                {
                    if (!checkedOrientations.Contains(Orientation.West))
                    {
                        orientation = Orientation.West;
                    }
                    else if (!checkedOrientations.Contains(Orientation.East))
                    {
                        orientation = Orientation.East;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (orientation == Orientation.East || orientation == Orientation.West)
                {
                    if (!checkedOrientations.Contains(Orientation.North))
                    {
                        orientation = Orientation.North;
                    }
                    else if (!checkedOrientations.Contains(Orientation.South))
                    {
                        orientation = Orientation.South;
                    }
                    else
                    {
                        return;
                    }
                }

                BackOffFromFire(checkedOrientations);
            }
            #endregion
        }

        /// <summary>
        /// This method controls the movement of the Staff.
        /// By default, they will first attempt to move to a fire extinguisher. Once they obtain it, they move on to the fighting fire behaviour.
        /// The fighting fire behaviour will move to the nearest fire and use the fire extinguisher. It will check if the fire gets to within three tiles of the Staff.
        /// When it does, the Staff moves backwards, away from the fire.
        /// If the fire extinguisher runs out, or the Staff takes too much damage, or no fire extinguisher could not be found, the Staff heads for the exit.
        /// </returns>
        public override bool Move()
        {
            for (int i = 0; i < speed; i++)
            {
                #region Move to fire extinguisher behaviour
                if (goingForExtinguisher)
                {
                    if (shouldFindNewPath)
                    {
                        shortestPath = ChoosePath(DestinationType.FireExtinguisher);

                        if (shortestPath == null) // No fire extinguisher could be found or no path could be found to one.
                        {
                            goingForExtinguisher = false;
                            return false;
                        }
                        else if (shortestPath.Count > 30) // Do not attempt to go for a fire extinguisher that's too far away.
                        { // In this case, go for an exit instead.
                            goingForExtinguisher = false;
                            return false;
                        }
                        else
                            shouldFindNewPath = false;
                    }
                    else // Path to fire extinguisher already found.
                    {
                        if (shortestPath.Count  == 0) // End of the path reached, no fire extinguisher found. Some other staff must have gotten it first.
                        {
                            shouldFindNewPath = true;
                            return false;
                        }
                        else if (((TileFloor)shortestPath.First()).PresentFireExtinguisher != null) // Check if the next tile has a fire extinguiser.
                        { // If so, equip it
                            equipedFireExtinguisher = ((TileFloor)shortestPath.First()).PresentFireExtinguisher;
                            ((TileFloor)shortestPath.First()).PresentFireExtinguisher = null;
                            goingForExtinguisher = false;
                            fightingFire = true;
                            shouldFindNewPath = true; // New path to fire must be calculated.
                        }
                        else // Next tile does not yet hold a fire extinguisher, destination has therefore not yet been reached.
                        { // Make move and set orientation.
                            MoveTile();
                            return true;
                        }
                    }
                }
                #endregion
                #region Fight fire behaviour
                else if (fightingFire)
                {
                    if (health < 50 || panic > 60) // Taken too much damage, drop fire extinguisher and head for exit.
                    {
                        DropFireExtinguisher(this);
                        fightingFire = false;
                        return true;
                    }

                    // Find path to nearest fire every tick.
                    shortestPath = ChoosePath(DestinationType.Fire);

                    if (shortestPath == null)
                        return false;

                    if (shortestPath.Count > 4) // If fire is still far away, remove the last three tiles to make sure the
                    { // person keeps some distance from the flames.
                        for (int j = 0; j < 4; j++)
                            shortestPath.Remove(shortestPath.Last());

                        // Make move and set orientation
                        MoveTile();
                        return true;
                    }
                    else if (shortestPath.Count < 4) // Too close to fire, need to back off first!
                    {
                        if (shortestPath.Count > 1)
                            SetOrientation();

                        BackOffFromFire(new List<Orientation>());
                    }

                    // Distance to fire should be correct (3 tiles) at this point.

                    if (equipedFireExtinguisher != null)
                        equipedFireExtinguisher.Use();

                    SprayFireExtinguisher(this, shortestPath); // Send message to Simulation to spray tiles.

                    if (equipedFireExtinguisher.UsesLeft == 0) // Fire extinguisher is empty, drop it and head for exit.
                    {
                        DropFireExtinguisher(this);
                        fightingFire = false;
                    }

                    return true;
                }
                #endregion
                #region Move to exit behaviour
                else
                {
                    base.Move();
                }
                #endregion
            }

            return true;
        }

        /// <summary>
        /// Sets the Person object's customTarget field to the provided Point; also resets the firstPathSearched field to make sure the Person object will recalculate a new path to the custom destination.
        /// A Staff will also drop any fire extinguisher it may have on it and afterwards move to the exit.
        /// </summary>
        /// <param name="p"> The Point representing the location in the grid map. </param>
        public override void SetCustomTarget(Point p)
        {
            customTarget = p;
            shouldFindNewPath = true;
            targetReached = false;
            goingForExtinguisher = false;
            fightingFire = false;

            if (equipedFireExtinguisher != null)
                DropFireExtinguisher(this);
        }
    }
}

