using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireEscapeSimulator
{
    [Serializable]
    class Teacher : Person
    {
        // ---------------------- Fields -----------------------
        private bool goingForWindow;
        private Tile foundWindow;

        // -------------------- Constructor --------------------
        public Teacher(Point location, IMapInfo room, int speed = 1, int panic = 0) : base(location, room, speed, panic)
        {
            goingForWindow = true;
            foundWindow = null;
        }

        // ---------------------- Methods ----------------------

        /// <summary>
        /// Checks a room for students.
        /// </summary>
        /// <returns>Returns true if no students are found in the room, otherwise, returns false.</returns>
        private bool CheckForStudents()
        {
            if (mapInfo.GetStudentsInRoom(location).Count == 0)
                return true;

            return false;
        }

        /// <summary>
        /// This method will move the Teacher to the next Tile on the shortestPath list if possible (no obstacles detected). 
        /// During doing this will take care of the teacher’s behavior: close windows of there are open (inside the current room) 
        /// and wait for all the students to get out of the classroom before leaving it. If a manual target location is set, 
        /// the Teacher will instead follow a path towards this location. True if the target has been reached or a move was successful 
        /// (not blocked). False if all possible paths are blocked and no paths can be found.
        /// </returns>
        public override bool Move()
        {
            for (int i = 0; i < speed; i++)
            {
                #region Move to window behaviour
                if (goingForWindow)
                {
                    if (shouldFindNewPath)
                    {
                        shortestPath = ChoosePath(DestinationType.Window);
                        if (shortestPath == null) // No open window was found inside the room or no path could be found to one
                        {
                            goingForWindow = false;
                            return false;
                        }
                        else if (shortestPath.Count > 1) // If the window is not right next to the teacher 
                        {
                            foundWindow = shortestPath.Last();
                            shortestPath.Remove(shortestPath.Last());
                            List<Tile> currentTeachersRoom = mapInfo.GetRoom(location);
                            if (!currentTeachersRoom.Contains(shortestPath.Last())) // The window found is not in the room 
                            {
                                // In this case, go for an exit instead
                                goingForWindow = false;
                                shouldFindNewPath = true;
                                return false;
                            }
                            else
                            {
                                shouldFindNewPath = false;
                            }
                        }
                        else
                        {
                            // The window is next to the teacher so the teacher closes it
                            foundWindow = shortestPath.Last();
                            shortestPath.Remove(shortestPath.Last());
                            shouldFindNewPath = false;
                        }
                    }
                    else 
                    {
                        if (shortestPath.Count == 0)
                        {
                            // The teacher found the window and needs to close it
                            mapInfo.OpenOrCloseWindow(foundWindow.Location);
                            shouldFindNewPath = true;
                            return true;
                        }
                        else
                        {
                            // Otherwise, make move and change orientation
                            MoveTile();
                            return true;
                        }
                    }
                }
                #endregion
                #region Move to exit behaviour
                else
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
                                if (panic >= 0)
                                {
                                    if (rng.Next(1, 51) > 100 - panic) // The higher the panic level, the bigger the chance a Person decides to jump.
                                    {
                                        shortestPath = windowPath;
                                        shouldFindNewPath = false;
                                        targetReached = false;
                                        Move();
                                    }
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
                                // Search up to two tiles of the remaining path for a door. If a door is found
                                // the teacher must first wait for all students to exit the room.
                                for (int j = 0; j < Math.Min(shortestPath.Count, 2); j++)
                                {
                                    if (shortestPath[j] is TileDoor)
                                    {
                                        if (!CheckForStudents())
                                            return false;
                                    }
                                }

                                if (CheckForFire()) // If fire or obstacles are in the way, find new path
                                    shouldFindNewPath = true;
                                else
                                { // Otherwise, make move and change orientation
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
                #endregion
            }

            return true;
        }

        /// <summary>
        /// Sets the Person object's customTarget field to the provided Point; also resets the firstPathSearched field to make sure the Person object will recalculate a new path to the custom destination.
        /// A Teacher will also have its goingForWindow field set to false so that it moves to an exit after reaching the custom target.
        /// </summary>
        /// <param name="p"> The Point representing the location in the grid map. </param>
        public override void SetCustomTarget(Point p)
        {
            customTarget = p;
            goingForWindow = false;
            shouldFindNewPath = true;
            targetReached = false;
        }
    }
}

