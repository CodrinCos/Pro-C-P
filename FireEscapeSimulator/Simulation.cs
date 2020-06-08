using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FireEscapeSimulator
{
    internal delegate void DropFireExtinguisherEventHandler(Staff sender);
    internal delegate void SprayFireExtinguisherEventHandler(Staff sender, List<Tile> spray);

    [Serializable]
    class Simulation : IMapInfo
    {
        // ---------------------- Events -----------------------
        [field: NonSerialized]
        public event RedrawPanelEventHandler RedrawPanel;

        // ---------------------- Fields -----------------------
        private List<Person> personList;
        private Map map;
        [NonSerialized]
        private Timer simulationTimer;
        [NonSerialized]
        private int simulationInterval;
        private Person selectedPerson;
        private bool running;
        [NonSerialized]
        private List<SimulationState> previousStates = new List<SimulationState>();
        private static PathFactory pathFactory;

        // ----------------- Statistic Fields ------------------
        private int dead;
        private int escaped;
        private double timeElapsed;

        // -------------------- Properties ---------------------
        public List<Person> PersonList { get { return personList; } set { personList = value; } }
        public int NumberOfSavedSimulationStates { get { return previousStates.Count; } }
        public Map Map { get { return map; } }
        public Person SelectedPerson { get { return selectedPerson; } }
        public static PathFactory PathFactory { get { return pathFactory; } }
        public bool Running { get { return running; } }
        public int SimulationInterval { get { return simulationInterval; } }

        // --------------- Statistic Properties ----------------
        public int Dead { get { return dead; } }
        public int Escaped { get { return escaped; } }
        public double TimeElapsed { get { return timeElapsed; } }

        // -------------------- Constructor --------------------
        public Simulation(string mapToLoad)
        {
            InitializeNewSimulation(mapToLoad);
            simulationInterval = 500;

            simulationTimer = new Timer();
            simulationTimer.Interval = simulationInterval; // set interval
            simulationTimer.Elapsed += AdvanceSimulation;  // what event is raised every second
            simulationTimer.AutoReset = true;              // refreshes itself automatically after every interval

            escaped = 0;
            dead = 0;
            timeElapsed = 0;
        }

        // ---------------------- Methods ----------------------

        /// <summary>
        /// New default simulation initialization.
        /// </summary>
        /// <param name="mapToLoad"></param>
        private void InitializeNewSimulation(string mapToLoad)
        {
            personList = new List<Person>();
            map = new Map();
            Map.CreateMap(mapToLoad);
            pathFactory = new PathFactory(map.TilesArray);
            Map.Airflow = new AirFlow(pathFactory);

            // Create test persons for drawing
            AddPersonsToSimulation();
        }

        /// <summary>
        /// Create various persons of all types to the simulation for testing or demonstration purposes.
        /// </summary>
        private void AddPersonsToSimulation()
        {
            Person testPerson1 = new Person(new Point(4, 3), this);
            Person testPerson2 = new Person(new Point(4, 7), this);
            Person testPerson3 = new Person(new Point(12, 5), this, 1, 80);
            Person testTeacher1 = new Teacher(new Point(15, 8), this);
            Person testStaff1 = new Staff(new Point(20, 25), this);
            ((Staff)testStaff1).DropFireExtinguisher += new DropFireExtinguisherEventHandler(OnDropFireExtinguisher);
            ((Staff)testStaff1).SprayFireExtinguisher += new SprayFireExtinguisherEventHandler(OnSprayFireExtinguisher);

            personList.Add(testPerson1);
            personList.Add(testPerson2);
            personList.Add(testPerson3);
            personList.Add(testTeacher1);
            personList.Add(testStaff1);
        }

        /// <summary>
        /// Drop an equipped fire extinguisher  at the Staff's current location.
        /// </summary>
        /// <param name="sender">Staff</param>
        private void OnDropFireExtinguisher(Staff sender)
        {
            Tile t = map.GetTile(sender.Location);

            if (t is TileFloor)
            {
                ((TileFloor)t).PresentFireExtinguisher = sender.EquipedFireExtinguisher;
                sender.EquipedFireExtinguisher = null;
            }
        }

        /// <summary>
        /// Sprays an equipped fire extinguisher ahead of the Staff.
        /// </summary>
        /// <param name="sender">Staff</param>
        /// <param name="spray">The tiles to be sprayed.</param>
        private void OnSprayFireExtinguisher(Staff sender, List<Tile> spray)
        {
            foreach (Tile t in spray)
            {
                if (t is TileFloor)
                {
                    if (((TileFloor)t).OnFire)
                        ((TileFloor)t).OnFire = false;
                }
            }
        }

        /// <summary>
        /// Event which fires on each tick of the simulationTimer, advancing the simulation by allowing people to move and determining if any new tiles need to catch on fire.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void AdvanceSimulation(Object source, System.Timers.ElapsedEventArgs e)
        {
            previousStates.Add(new SimulationState(this));
            Map.SpreadFire();
            dead = 0;

            for (int i = PersonList.Count - 1; i >= 0; i--)
            {
                Person person = PersonList.ElementAt(i);

                if (person.Health != 0)
                {
                    if (map.TilesArray[person.Location.X, person.Location.Y] is TileFloor)
                    {
                        if (((TileFloor)map.TilesArray[person.Location.X, person.Location.Y]).HasSmoke)
                        {
                            person.DecreaseHealth(Person.DamageType.Smoke);
                        }

                        if (((TileFloor)map.TilesArray[person.Location.X, person.Location.Y]).OnFire)
                        {
                            person.DecreaseHealth(Person.DamageType.Fire);
                        }
                        person.IncreasePanicLevel(map.TilesArray[person.Location.X, person.Location.Y]);
                    }
                    if (map.TilesArray[person.Location.X, person.Location.Y] is TileExit)
                    {
                        PersonList.RemoveAt(i);
                        escaped++;
                    }
                    else if (map.TilesArray[person.Location.X, person.Location.Y] is TileWindow)
                    {
                        Random rng = new Random();

                        if (rng.Next(1, 21) <= 5 * map.FloorLevel) // Survival chance of jumping out of the window. Increased by floor level.
                            person.DecreaseHealth(Person.DamageType.Falling);
                        else
                        {
                            escaped++;
                            PersonList.RemoveAt(i);
                        }

                        person.Jumped = true;
                    }
                    else
                    {
                        if (person.Health > 0)
                            person.Move();
                    }
                }
                else
                {
                    dead++;
                }
            }

            timeElapsed += (double)simulationInterval / 1000;
            RedrawPanel();
        }

        #region Timer controls
        /// <summary>
        /// Starts the simulation and invalidates the panel
        /// </summary>
        public void Start()
        {
            running = true;
            simulationTimer.Enabled = true;
        }

        /// <summary>
        /// Stops the simulation and indalidates the panel, after which is starts a fresh simulation
        /// </summary>
        public void Stop()
        {
            running = false;
            simulationTimer.Enabled = false;
            previousStates.RemoveRange(0, previousStates.Count);

            RedrawPanel();
        }

        /// <summary>
        /// This method will pause or unpause the simulation by stopping the timer or resuming it.
        /// </summary>
        /// <returns>Will return a boolean value indicating if the simulation is currently paused (true) or not (false)</returns>
        public bool Pause()
        {
            if (simulationTimer.Enabled)
            {
                simulationTimer.Enabled = false;
                running = false;
                return true;
            } else
            {
                simulationTimer.Enabled = true;
                RedrawPanel();
                running = true;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// Sets a tile at a specified location in the gridmap on fire
        /// </summary>
        /// <param name="location">The location in the grid map.</param>
        /// <returns>Will return true if a tile could be found and successfully set on fire (wasn't on fire already). Otherwise, returns false.</returns>
        public bool AddFire(Point location)
        {
            Tile t = map.GetTile(location);

            if (t != null)
            {
                if (t is TileFloor)
                {
                    map.SetTileOnFire(t);
                    return true;
                }
            }

            return false;
        }

        #region Person controls
        /// <summary>
        /// Finds a Person object at a specified location in the grid map. The selectedPerson field is then set to equal the found Person object.
        /// </summary>
        /// <param name="location">The location in the grid ma.p</param>
        /// <returns>Will return true if a Person could be found at the provided location. Otherwise, returns false.</returns>
        public bool SelectPerson(Point location)
        {
            Person p = GetPerson(location);

            if (p != null)
            {
                selectedPerson = p;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deselects Person by resetting the selectedPerson field to null.
        /// </summary>
        public void DeselectPerson()
        {
            selectedPerson = null;
        }

        /// <summary>
        /// Finds and returns a Person object at the specified location in the grid map.
        /// </summary>
        /// <param name="location">The location in the grid map.</param>
        /// <returns>Will return the Person at the specified location. If no Person could be found, returns null.</returns>
        public Person GetPerson(Point location)
        {
            for (int i = 0; i < personList.Count; i++)
            {
                if (personList[i].Location == location)
                    return personList[i];
            }

            return null;
        }

        /// <summary>
        /// Adds a Person object to the simulation at the specified location.
        /// </summary>
        /// <param name="location">The location in the grid map.</param>
        /// <param name="type">The type of person to add.</param>
        /// <returns>Will return true if the clicked location was not outside the map and not occupied, therefore successfully adding the Person. Otherwise, returns false.</returns>
        public bool AddPerson(Point location, string type)
        {
            Tile t = map.GetTile(location);

            if (t != null && t is TileFloor)
            {
                if (((TileFloor)t).HasFurniture == TileFloor.furniture.None && !((TileFloor)t).OnFire)
                {
                    if (type == "Student")
                    {
                        personList.Add(new Person(location, this));
                    }
                    else if (type == "Teacher")
                    {
                        personList.Add(new Teacher(location, this));
                    }
                    else if (type == "Staff")
                    {
                        var staff = new Staff(location, this);
                        ((Staff)staff).DropFireExtinguisher += new DropFireExtinguisherEventHandler(OnDropFireExtinguisher);
                        ((Staff)staff).SprayFireExtinguisher += new SprayFireExtinguisherEventHandler(OnSprayFireExtinguisher);
                        personList.Add(staff);
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Sets a custom destination for the selected Person to go, instead of the exit.
        /// </summary>
        /// <param name="location">The location of the custom destination in the grid map.</param>
        /// <returns>Will return true if the selected location was inside the map and accessible. Otherwise, returns false.</returns>
        public bool ManuallyMovePerson(Point location)
        {
            if (selectedPerson != null)
            {
                Tile t = map.GetTile(location);

                if (t != null && t is TileFloor)
                {
                    selectedPerson.SetCustomTarget(t.Location);
                    return true;
                }
            }

            return false;
        }
        #endregion

        /// <summary>
        /// Sets the new simulation timer value.
        /// </summary>
        /// <param name="modifier"></param>
        public void SetSpeed(decimal modifier)
        {
            simulationTimer.Interval = Convert.ToInt32(500 / modifier);
        }

        /// <summary>
        /// Resets the simulation timer to the default value (=500).
        /// </summary>
        public void ResetSpeed()
        {
            simulationTimer.Interval = 500;
        }

        /// <summary>
        /// Change the fire spread rate.
        /// </summary>
        /// <param name="modifier">The new value for the fire spread rate.</param>
        public void SetFireSpreadRate(int modifier)
        {
            map.FireSpreadRate = modifier;
        }

        /// <summary>
        /// Resets the state of the simultion to the state that is saved in the previousStates list at the selected index. All following states after the index will be deleted.
        /// </summary>
        /// <param name="index">The index within the previousStates list.</param>
        public void RestoreState(int index)
        {
            using (var stream = new MemoryStream())
            {
                if (previousStates.Any() && index != NumberOfSavedSimulationStates)
                {
                    var bytes = previousStates[index].State;
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Position = 0;
                    BinaryFormatter bin = new BinaryFormatter();
                    Simulation deserializedSimulation = (Simulation)bin.Deserialize(stream);
                    this.personList = deserializedSimulation.personList;
                    this.map = deserializedSimulation.map;
                    this.selectedPerson = deserializedSimulation.SelectedPerson;
                    this.running = deserializedSimulation.Running;
                    this.dead = deserializedSimulation.Dead;
                    this.escaped = deserializedSimulation.Escaped;
                    this.timeElapsed = deserializedSimulation.TimeElapsed;
                    pathFactory = new PathFactory(map.TilesArray);
                    Map.Airflow = new AirFlow(pathFactory);
                }
            }
            if (previousStates.Any())
            {
                previousStates.RemoveRange(index, previousStates.Count - index);
            }
        }

        /// <summary>
        /// Returns the room that the provided location is a part of.
        /// </summary>
        /// <param name="location">The location for which the associated room is needed.</param>
        /// <returns>The room that the location is in. If no room could be found, returns null.</returns>
        public List<Tile> GetRoom(Point location)
        {
            foreach (List<Tile> room in map.Rooms)
            {
                foreach (Tile t in room)
                {
                    if (t.Location == location)
                        return room;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks a location for an associated room and returns a list of all students in that room.
        /// </summary>
        /// <param name="location">The location to check.</param>
        /// <returns>Returns a list of all students in the room.</returns>
        public List<Person> GetStudentsInRoom(Point location)
        {
            List<Person> studentsInRoom = new List<Person>();
            List<Tile> currentRoom = GetRoom(location);

            foreach (Person p in personList)
            {
                if (!(p is Teacher) && !(p is Staff))
                    foreach (Tile t in currentRoom)
                        if (t.Location == p.Location)
                            studentsInRoom.Add(p);
            }

            return studentsInRoom;
        }

        /// <summary>
        /// Opens or closes a window.
        /// </summary>
        /// <param name="location">Location of the window</param>
        /// <returns>Returns true if window was successfully opened or closed, otherwise, returns false.</returns>
        public bool OpenOrCloseWindow(Point location)
        {
            Tile t = map.GetTile(location);

            if (t == null)
                return false;

            if (t is TileWindow)
            {
                ((TileWindow)t).IsOpen = !((TileWindow)t).IsOpen;
                map.Airflow = new AirFlow(pathFactory);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Opens or closes a door.
        /// </summary>
        /// <param name="location">Location of the door</param>
        /// <returns>Returns true if door was successfully opened or closed, otherwise, returns false.</returns>
        public bool OpenOrCloseDoor(Point location)
        {
            Tile t = map.GetTile(location);

            if (t == null)
                return false;

            if (t is TileDoor)
            {
                ((TileDoor)t).IsOpen = !((TileDoor)t).IsOpen;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Gets a Tile at a specific location. Returns null if no Tile could be found.
        /// </summary>
        /// <param name="location">The location to search for a Tile.</param>
        /// <returns>Returns the found Tile. If no Tile could be found, returns null.</returns>
        public Tile GetTile(Point location)
        {
            return map.GetTile(location);
        }

        /// <summary>
        /// Gets the floor number of the map.
        /// </summary>
        /// <returns>Floor number</returns>
        public int GetFloorNumber()
        {
            return map.FloorLevel;
        }
    }
}
