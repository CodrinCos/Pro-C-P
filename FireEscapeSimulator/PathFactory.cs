using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FireEscapeSimulator
{

    public class PathFactory
    {
        // ---------------------- Fields -----------------------
        public Tile[,] map;

        // -------------------- Constructor --------------------
        public PathFactory(Tile[,] TilesArray)
        {
            this.map = TilesArray;
        }

        // ---------------------- Methods ----------------------

        /// <summary>
        /// This method is used for finding paths to all the exits on the map.
        /// </summary>
        /// <param name="position">The position of the Person.</param>
        /// <returns>A list of paths to the exits.</returns>
        public List<List<Tile>> FindPathsToExits(Point position)
        {
            var paths = new List<List<Tile>>();
            var exitLocations = new List<Point>();

            //get exits
            foreach (Tile t in map)
            {
                if (t is TileExit)
                    exitLocations.Add(t.Location);
            }
            foreach (Point p in exitLocations)
            {
                paths.Add(FindShortestPath(position, p));
            }

            return paths;
        }

        /// <summary>
        /// This method is used for finding paths to all the windows on the map.
        /// </summary>
        /// <param name="position">The position of the Person.</param>
        /// <returns>A list of paths to the windows.</returns>
        public List<List<Tile>> FindPathsToWindows(Point position)
        {
            var paths = new List<List<Tile>>();
            var windowLocations = new List<Point>();

            // Find windows
            foreach (Tile t in map)
            {
                if (t is TileWindow)
                    windowLocations.Add(t.Location);
            }

            // Find paths to windows
            foreach (Point p in windowLocations)
            {
                paths.Add(FindShortestPath(position, p));
            }

            return paths;
        }

        /// <summary>
        /// This method is used for finding paths to all the fire extinguishers on the map.
        /// </summary>
        /// <param name="position">The position of the Person.</param>
        /// <returns>A list of paths to the fire extinguishers.</returns>
        public List<List<Tile>> FindPathsToFireExtinguishers(Point position)
        {
            var paths = new List<List<Tile>>();
            var fireExtinguisherLocations = new List<Point>();

            // Get fire extinguishers
            foreach (Tile t in map)
            {
                if (t is TileFloor)
                    if (((TileFloor)t).PresentFireExtinguisher != null)
                        fireExtinguisherLocations.Add(t.Location);
            }

            foreach (Point p in fireExtinguisherLocations)
            {
                paths.Add(FindShortestPath(position, p));
            }

            return paths;
        }

        /// <summary>
        /// This method is used for finding paths to all the fires on the map.
        /// </summary>
        /// <param name="position">The position of the Person.</param>
        /// <returns>A list of paths to the fires.</returns>
        public List<List<Tile>> FindPathsToFire(Point position)
        {
            var paths = new List<List<Tile>>();
            var fireLocations = new List<Point>();

            // Get fires
            foreach (Tile t in map)
            {
                if (t is TileFloor)
                    if (((TileFloor)t).OnFire)
                        fireLocations.Add(t.Location);
            }

            foreach (Point p in fireLocations)
            {
                paths.Add(FindShortestPath(position, p, true));
            }

            return paths;
        }

        /// <summary>
        /// This method is used for finding the shortest path between two points. If no path can be found, it returns null.
        /// </summary>
        /// <param name="start"> The starting Point. </param>
        /// <param name="target"> The target Point. </param>
        /// <returns> A path to the target or null if no path can be found. </returns>
        public List<Tile> FindShortestPath(Point start, Point target, bool findFire = false)
        {
            NodeMap nMap = new NodeMap(map, target);

            var openNodes = new HashSet<Node>();
            var closedNodes = new HashSet<Node>();

            openNodes.Add(nMap.nodes[start.X, start.Y]);
            openNodes.First().G = 0;

            while (openNodes.Any())
            {
                Node currentNode = nMap.GetNodeWithSmallestF(openNodes);

                if (currentNode.Location.X == target.X && currentNode.Location.Y == target.Y)
                {
                    return ReconstructPath(currentNode);
                }

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                foreach (Node n in nMap.GetNeighbors(currentNode, findFire))
                {
                    if (closedNodes.Contains(n))
                    {
                        continue;
                    }

                    var tGscore = currentNode.G + 1;

                    if (!openNodes.Contains(n))
                    {
                        openNodes.Add(n);
                    }
                    else if (tGscore >= n.G)
                    {
                        continue;
                    }

                    n.Parent = currentNode;
                    n.G = tGscore;
                }
            }

            return null;
        }

        /// <summary>
        /// When a path has been found, this method traces back through the nodes on that path, stores them in a list and returns that list in reveresed order.
        /// </summary>
        /// <param name="lastNode"></param>
        /// <returns> A path. </returns>
        private List<Tile> ReconstructPath(Node lastNode)
        {
            List<Tile> path = new List<Tile>();

            while (lastNode.Parent != null)
            {
                path.Add(map[lastNode.Location.X, lastNode.Location.Y]);
                lastNode = lastNode.Parent;
            }

            path.Reverse();
            return path;
        }

        /// <summary>
        /// This class is a temporary representation of the current state of the map. A new node map is created each time FindShortestPath is called. 
        /// This allows the pathfinding to run independently of the simulation without interfering. 
        /// It also makes it possible to potentially run the pathfinding on multiple threads at the same time if that should be necessary.
        /// </summary>
        class NodeMap
        {
            public Node[,] nodes;
            public Tile[,] map;

            public NodeMap(Tile[,] map, Point target)
            {
                this.map = map;
                nodes = new Node[this.map.GetLength(0), this.map.GetLength(1)];

                for (int i = 0; i < this.map.GetLength(0); i++)
                {
                    for (int j = 0; j < this.map.GetLength(1); j++)
                    {
                        nodes[i, j] = new Node(this.map[i, j].Location, target);
                    }
                }
            }

            public Node GetNodeWithSmallestF(HashSet<Node> collection)
            {
                Node currentSmallest = collection.First();

                foreach (Node n in collection)
                {
                    if (currentSmallest.F > n.F)
                    {
                        currentSmallest = n;
                    }
                }

                return currentSmallest;
            }

            public HashSet<Node> GetNeighbors(Node currentNode, bool findFire = false)
            {
                var neighbors = new HashSet<Node>();

                if (currentNode.Location.X < nodes.GetLength(0) - 1)
                {
                    if (TestNodeViability(nodes[currentNode.Location.X + 1, currentNode.Location.Y], findFire))
                    {
                        neighbors.Add(nodes[currentNode.Location.X + 1, currentNode.Location.Y]);
                    }
                }

                if (currentNode.Location.X > 0)
                {
                    if (TestNodeViability(nodes[currentNode.Location.X - 1, currentNode.Location.Y], findFire))
                    {
                        neighbors.Add(nodes[currentNode.Location.X - 1, currentNode.Location.Y]);
                    }
                }

                if (currentNode.Location.Y < nodes.GetLength(1) - 1)
                {
                    if (TestNodeViability(nodes[currentNode.Location.X, currentNode.Location.Y + 1], findFire))
                    {
                        neighbors.Add(nodes[currentNode.Location.X, currentNode.Location.Y + 1]);
                    }
                }

                if (currentNode.Location.Y > 0)
                {
                    if (TestNodeViability(nodes[currentNode.Location.X, currentNode.Location.Y - 1], findFire))
                    {
                        neighbors.Add(nodes[currentNode.Location.X, currentNode.Location.Y - 1]);
                    }
                }

                return neighbors;
            }

            private bool TestNodeViability(Node n, bool findFire = false)
            {
                Tile t = map[n.Location.X, n.Location.Y];
                if (t is TileFloor || t is TileExit || t is TileDoor || t is TileWindow)
                {
                    if (t is TileFloor)
                    {
                        if (findFire)
                        {
                            if (((TileFloor)t).HasFurniture != TileFloor.furniture.None)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (((TileFloor)t).OnFire || ((TileFloor)t).HasFurniture != TileFloor.furniture.None)
                            {
                                return false;
                            }
                        }
                    }

                    if (t is TileWindow)
                    {
                        if (!((TileWindow)t).IsOpen)
                            return false;
                    }

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// This class represents a node in a NodeMap (a Tile in the map).
        /// </summary>
        class Node
        {
            public int G { get; set; }
            public int H { get; }
            public int F { get { return H + G; } }
            public Node Parent { get; set; }
            public Point Location { get; }

            public Node(Point location, Point target)
            {
                G = int.MaxValue;
                H = Math.Abs(location.X - target.X) + Math.Abs(location.Y - target.Y);
                Parent = null;
                this.Location = location;
            }
        }
    }
}
