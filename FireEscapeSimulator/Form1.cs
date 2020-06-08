using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Reflection;
using System.IO;

namespace FireEscapeSimulator
{
    public delegate void RedrawPanelEventHandler();

    [Serializable]
    public partial class h : Form
    {
        // ----------------- Tools Enumeration -----------------
        private enum Tool
        {
            None, AddFireManually, AddStudent, AddTeacher, AddStaff
        }

        // ---------------------- Fields -----------------------
        private Simulation simulation;
        private Random random = new Random();
        private FileHelper fh;
        private Tool selectedTool;

        // Control variables
        private bool simulationRunning;
        private bool isRestarted;
        private bool isStopped;

        private int TilePixelSize = 15; // Default pixel size of tiles
        private int MapTopLeftXPos = 50; // Position of the top left corner (x-coordinate) of the map inside the panel. Change this to have the map be drawn further to the left or right inside the panel
        private int MapTopLeftYPos = 150; // Position of the top left corner (y-coordinate) of the map inside the panel. Change this to have the map be drawn further up or down inside the panel

        //Image assets
        private Bitmap mapImage;
        private Image floor = Properties.Resources.floor;
        private Image wall = Properties.Resources.wall;
        private Image personRight = Properties.Resources.personRight;
        private Image personUp = Properties.Resources.personUp;
        private Image personDown = Properties.Resources.personDown;
        private Image personLeft = Properties.Resources.personLeft;
        private Image exit = Properties.Resources.exit;
        private Image skull = Properties.Resources.skull;
        private Image window = Properties.Resources.window;
        private Image dyingRight = Properties.Resources.dyingRight;
        private Image dyingUp = Properties.Resources.dyingUp;
        private Image dyingDown = Properties.Resources.dyingDown;
        private Image dyingLeft = Properties.Resources.dyingLeft;
        private Image fireExtinguisher = Properties.Resources.fire_extinguisher;
        private Image chair = Properties.Resources.chair;
        private Image chair1 = Properties.Resources.chair1;
        private Image chair2 = Properties.Resources.chair2;
        private Image chair3 = Properties.Resources.chair3;
        private Image table = Properties.Resources.table;
        private Image window1 = Properties.Resources.window1;
        private Image window2 = Properties.Resources.window2;
        private Image window3 = Properties.Resources.window3;
        private Image windowClosed = Properties.Resources.window_closed;
        private Image doorOpen = Properties.Resources.door_open;
        private Image doorClosed = Properties.Resources.door_closed;
        private Image doorClosedVertical = Properties.Resources.door_closed_vertical;
        private Image panicMedium = Properties.Resources.panic_medium;
        private Image panicBig = Properties.Resources.panic_big;
        private Image teacher = Properties.Resources.teacher;
        private Image staff = Properties.Resources.staff;
        private Image desk = Properties.Resources.desk;

        public string loadedMap;

        // ---------------------- Methods ----------------------
        public h()
        {
            InitializeComponent();
            var personTypes = new List<PersonType>();
            personTypes.Add(new PersonType("Student"));
            personTypes.Add(new PersonType("Teacher"));
            personTypes.Add(new PersonType("Staff"));
            personSelector.DataSource = personTypes;
            personSelector.DisplayMember = "Value";
            //personSelector.ValueMember = "Value";
            //Activate double buffering to eliminate flicker
            typeof(Panel).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, panelMap, new object[] { true });

            loadedMap = "fontys";
            simulation = new Simulation(loadedMap);
            simulation.RedrawPanel += new RedrawPanelEventHandler(OnRedrawPanel);
            fh = new FileHelper();
            selectedTool = Tool.None;
            simulationRunning = false;
            isRestarted = false;
            isStopped = false;

            exit.RotateFlip(RotateFlipType.Rotate270FlipNone);
            MakeImage(TilePixelSize);

            panelMap.Invalidate();
            this.MouseWheel += Form1_MouseWheel;
            ChangeButtonColours();
        }

        /// <summary>
        /// Gets called when a RedrawPanel event is received and will redraw the panel to reflect changes in the simulation.
        /// </summary>
        private void OnRedrawPanel()
        {
            panelMap.Invalidate();
        }

        /// <summary>
        /// Creates a Bitmap image by stitching together tile images based on the TilesArray.
        /// </summary>
        private void MakeImage(int tilePixelSize)
        {
            #region Make image
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();

            if (mapImage != null) //the first time it will be null
            {
                mapImage.Dispose();
            }

            TilePixelSize = tilePixelSize;

            mapImage = new Bitmap(TilePixelSize * simulation.Map.LengthOfMap + MapTopLeftXPos, TilePixelSize * simulation.Map.HeightOfMap + MapTopLeftYPos);

            using (Graphics gr = Graphics.FromImage(mapImage))
            {
                for (int i = 0; i <= simulation.Map.LengthOfMap - 1; i++)
                    for (int j = 0; j <= simulation.Map.HeightOfMap - 1; j++)
                    {
                    Point drawLocation = new Point(TilePixelSize * simulation.Map.TilesArray[i, j].Location.X + MapTopLeftXPos, TilePixelSize * simulation.Map.TilesArray[i, j].Location.Y + MapTopLeftYPos);

                    if (simulation.Map.TilesArray[i, j] is TileWindow)
                    {
                        int rand = random.Next(100);
                        if (((TileWindow)simulation.Map.TilesArray[i, j]).IsOpen)
                        {
                            if (rand < 33)
                            {
                                gr.DrawImage(window1, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                            else if (rand < 66)
                            {
                                gr.DrawImage(window2, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                            else
                            {
                                gr.DrawImage(window3, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                        }
                        else
                        {
                            gr.DrawImage(windowClosed, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    }
                    else if (simulation.Map.TilesArray[i, j] is TileWall)
                        gr.DrawImage(wall, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    else if (simulation.Map.TilesArray[i, j] is TileExit)
                    {
                        gr.DrawImage(floor, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        gr.DrawImage(exit, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                    else if (simulation.Map.TilesArray[i, j] is TileFloor)
                    {
                        if (((TileFloor)simulation.Map.TilesArray[i, j]).OnFire == false && ((TileFloor)simulation.Map.TilesArray[i, j]).HasSmoke == false)
                        {
                            gr.DrawImage(floor, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);

                            if (simulation.Map.TilesArray[i, j] is TileDoor)
                            {
                                if (((TileDoor)simulation.Map.TilesArray[i, j]).IsOpen)
                                {
                                    gr.DrawImage(doorOpen, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                }
                                else if (i>0 && simulation.Map.TilesArray[i - 1, j] is TileWall)
                                {
                                    gr.DrawImage(doorClosed, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                }
                                else if (j>0 && simulation.Map.TilesArray[i, j - 1] is TileWall)
                                {
                                    gr.DrawImage(doorClosedVertical, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                }
                             }
                        }
                        if (((TileFloor)simulation.Map.TilesArray[i, j]).HasFurniture == TileFloor.furniture.Chair)
                        {
                            gr.DrawImage(chair, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                        else if (((TileFloor)simulation.Map.TilesArray[i, j]).HasFurniture == TileFloor.furniture.Table)
                        {
                            gr.DrawImage(desk, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    }                 
                }

                // Displays room numbers. For testing only
                bool displayRoomNumbers = false;
                int s = 0;

                if (displayRoomNumbers)
                {
                    foreach (List<Tile> room in simulation.Map.Rooms)
                    {
                        foreach (Tile t in room)
                        {
                            Point drawLocation = new Point(TilePixelSize * t.Location.X + MapTopLeftXPos, TilePixelSize * t.Location.Y + MapTopLeftYPos);
                            gr.DrawString(Convert.ToString(s), Font, new SolidBrush(Color.Black), drawLocation.X, drawLocation.Y);
                        }

                        s++;
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// Enables and disables UI controls as necessary for the current state of the simulation.
        /// </summary>
        private void ChangeButtonStatuses()
        {
            if (simulationRunning)
            {
                btnSetSpeed.Enabled = false;
                btnSetFireSpreadRate.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnPause.Enabled = true;
                btnRestart.Enabled = false;
            }
            else
            {
                btnStop.Enabled = false;
                btnStart.Enabled = false;
                btnPause.Enabled = false;
                btnPause.Text = "Pause";
                btnSetSpeed.Enabled = true;
                btnSetFireSpreadRate.Enabled = true;
                btnRestart.Enabled = true;
            }

            if (isRestarted)
            {
                btnStart.Enabled = true;
                btnRestart.Enabled = false;
                btnSetSpeed.Enabled = true;
                btnSetFireSpreadRate.Enabled = true;
            }

            if (isStopped)
            {
                btnAddPerson.Enabled = false;
                btnStartFire.Enabled = false;
                personSelector.Enabled = false;
                btnSetSpeed.Enabled = false;
                btnSetFireSpreadRate.Enabled = false;

                selectedTool = Tool.None;
                simulation.DeselectPerson();
                ChangeButtonColours();
            }
            else
            {
                btnAddPerson.Enabled = true;
                btnStartFire.Enabled = true;
                personSelector.Enabled = true;
            }
        }

        /// <summary>
        /// Changes the background colour of the left hand side buttons to green if they are selected.
        /// </summary>
        private void ChangeButtonColours()
        {
            if (selectedTool == Tool.AddStudent || selectedTool == Tool.AddTeacher || selectedTool == Tool.AddStaff)
            {
                btnAddPerson.BackColor = Color.Green;
                btnStartFire.BackColor = SystemColors.Control;
            }
            else if (selectedTool == Tool.AddFireManually)
            {
                btnStartFire.BackColor = Color.Green;
                btnAddPerson.BackColor = SystemColors.Control;
            }
            else
            {
                btnStartFire.BackColor = SystemColors.Control;
                btnAddPerson.BackColor = SystemColors.Control;
            }
        }

        /// <summary>
        /// Resets the map, the simulation, and all UI elements to their default values.
        /// </summary>
        private void ResetMap()
        {
            simulation.RedrawPanel += new RedrawPanelEventHandler(OnRedrawPanel);
            MakeImage(TilePixelSize);
            isRestarted = true;
            isStopped = false;
            ChangeButtonStatuses();
            isRestarted = false;
            panelMap.Invalidate();
            vScrollBar.Maximum = 16;
            hScrollBar.Maximum = 16;
            ResetStats();
        }

        /// <summary>
        /// Resets the UI to the default state where no simulation is loaded. Enables and disables the necessary UI controls and updates result labels where necessary.
        /// </summary>
        private void StopSimulationUI()
        {
            simulationRunning = false;
            isStopped = true;
            ChangeButtonStatuses();
            ResetStats();
            simulation.ResetSpeed();
            simulation.Stop();
        }

        /// <summary>
        /// Loads a new map from a string code.
        /// </summary>
        /// <param name="mapName">The string code defining the new map.</param>
        private void LoadMap(string mapName)
        {
            loadedMap = mapName;

            if (simulation != null)
            {
                var oldSimulation = simulation;
                simulation.Map.FireSpreadRate = oldSimulation.Map.FireSpreadRate;
            }

            simulation = new Simulation(loadedMap);
            MakeImage(TilePixelSize);
            simulation.RedrawPanel += new RedrawPanelEventHandler(OnRedrawPanel);

            ResetMap();
            simulation.Stop();
        }

        /// <summary>
        /// Change the value of the stats.
        /// </summary>
        private void ResetStats()
        {
            lblEscaped.Text = simulation.Escaped.ToString() + " escaped";
            lblDead.Text = simulation.Dead.ToString() + " dead";
            lblFireArea.Text = String.Format("The fire spread to {0}% of the map", simulation.Map.GetPercentageOnFire().ToString());
            lblTimeElapsed.Text = String.Format("Time elapsed: {0} seconds", simulation.TimeElapsed.ToString());
            selectStatesBar.Maximum = simulation.NumberOfSavedSimulationStates;
        }

        private void panelMap_Paint(object sender, PaintEventArgs e)
        {
            #region Drawing
            Graphics gr = e.Graphics;
            gr.DrawImage(mapImage, 0, 0, mapImage.Width, mapImage.Height);

            for (int i = 0; i <= simulation.Map.LengthOfMap - 1; i++)
                for (int j = 0; j <= simulation.Map.HeightOfMap - 1; j++)
                {
                Point drawLocation = new Point(TilePixelSize * simulation.Map.TilesArray[i, j].Location.X + MapTopLeftXPos, TilePixelSize * simulation.Map.TilesArray[i, j].Location.Y + MapTopLeftYPos);
                if (simulation.Map.TilesArray[i, j] is TileFloor)
                {
                    if (((TileFloor)simulation.Map.TilesArray[i, j]).OnFire == true)
                    {
                        int r = random.Next();

                        if (r <= int.MaxValue / 3)
                        {
                            gr.FillRectangle(new SolidBrush(Color.Red), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        } else if (r >= int.MaxValue / 3 && r <= int.MaxValue / 3 * 2)
                        {
                            gr.FillRectangle(new SolidBrush(Color.Crimson), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        } else
                        {
                            gr.FillRectangle(new SolidBrush(Color.Firebrick), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    } else if (((TileFloor)simulation.Map.TilesArray[i, j]).HasSmoke == true)
                    {
                        int r = random.Next();

                        if (r <= int.MaxValue / 2)
                        {
                            gr.FillRectangle(new SolidBrush(Color.LightGray), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        } else
                        {
                            gr.FillRectangle(new SolidBrush(Color.Silver), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    }
                        else if (simulation.Map.TilesArray[i, j] is TileDoor)
                        {
                            if (((TileDoor)simulation.Map.TilesArray[i, j]).IsOpen)
                            {
                                
                                gr.DrawImage(doorOpen, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                            else if (simulation.Map.TilesArray[i - 1, j] is TileWall)
                            {
                                gr.DrawImage(doorClosed, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                            else if (simulation.Map.TilesArray[i, j - 1] is TileWall)
                            {
                                gr.DrawImage(doorClosedVertical, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            }
                        }

                        if (((TileFloor)simulation.Map.TilesArray[i, j]).PresentFireExtinguisher != null)
                        gr.DrawImage(fireExtinguisher, drawLocation.X, drawLocation.Y, TilePixelSize / 2, TilePixelSize / 2);
                } else if (simulation.Map.TilesArray[i, j] is TileWindow)
                {
                    if (((TileWindow)simulation.Map.TilesArray[i, j]).IsOpen)
                    {
                        gr.DrawImage(window1, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    } else
                    {
                        gr.DrawImage(windowClosed, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                }
            }

            foreach (Person p in simulation.PersonList)
            {
                Point drawLocation = new Point(TilePixelSize * p.Location.X + MapTopLeftXPos, TilePixelSize * p.Location.Y + MapTopLeftYPos);
                gr.ResetTransform();

                if (p.Health <= 100 && p.Health > 50)
                {
                    switch (p.Orientation)
                    {
                        case Orientation.North:
                            gr.DrawImage(personUp, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.South:
                            gr.DrawImage(personDown, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.West:
                            gr.DrawImage(personLeft, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.East:
                            gr.DrawImage(personRight, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        default:
                            break;
                    }
                    if (p is Teacher)
                    {
                        gr.DrawImage(teacher, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                    if (p is Staff)
                    {
                        gr.DrawImage(staff, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                    if (p.Panic >= 33 && p.Panic < 66)
                    {
                        gr.DrawImage(panicMedium, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    } else if (p.Panic >= 66)
                    {
                        gr.DrawImage(panicBig, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                } else if (p.Health <= 50 && p.Health > 0)
                {
                    switch (p.Orientation)
                    {
                        case Orientation.North:
                            gr.DrawImage(dyingUp, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.South:
                            gr.DrawImage(dyingDown, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.West:
                            gr.DrawImage(dyingLeft, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.East:
                            gr.DrawImage(dyingRight, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        default:
                            break;
                    }
                    if (p is Teacher)
                    {
                        gr.DrawImage(teacher, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                    if (p is Staff)
                    {
                        gr.DrawImage(staff, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                    if (p.Panic >= 33 && p.Panic < 66)
                    {
                        gr.DrawImage(panicMedium, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    } else if (p.Panic >= 66)
                    {
                        gr.DrawImage(panicBig, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                    }
                } else if (p.Health == 0 && p.Jumped)
                {
                    switch (p.Orientation)
                    {
                        case Orientation.North:
                            gr.DrawImage(skull, drawLocation.X, drawLocation.Y - TilePixelSize, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.South:
                            gr.DrawImage(skull, drawLocation.X, drawLocation.Y + TilePixelSize, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.West:
                            gr.DrawImage(skull, drawLocation.X - TilePixelSize, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        case Orientation.East:
                            gr.DrawImage(skull, drawLocation.X + TilePixelSize, drawLocation.Y, TilePixelSize, TilePixelSize);
                            break;
                        default:
                            break;
                    }
                } else
                {
                    gr.DrawImage(skull, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                }

                if (p == simulation.SelectedPerson)
                {
                    gr.FillRectangle(new SolidBrush(Color.FromArgb(128, 0, 102, 204)), drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);

                    if (p.Health <= 100 && p.Health > 50)
                    {
                        switch (p.Orientation)
                        {
                            case Orientation.North:
                                gr.DrawImage(personUp, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.South:
                                gr.DrawImage(personDown, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.West:
                                gr.DrawImage(personLeft, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.East:
                                gr.DrawImage(personRight, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            default:
                                break;
                        }
                        if (p is Teacher)
                        {
                            gr.DrawImage(teacher, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                        if (p is Staff)
                        {
                            gr.DrawImage(staff, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                        if (p.Panic >= 33 && p.Panic < 66)
                        {
                            gr.DrawImage(panicMedium, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        } else if (p.Panic >= 66)
                        {
                            gr.DrawImage(panicBig, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    } else if (p.Health <= 50 && p.Health > 0)
                    {
                        switch (p.Orientation)
                        {
                            case Orientation.North:
                                gr.DrawImage(dyingUp, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.South:
                                gr.DrawImage(dyingDown, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.West:
                                gr.DrawImage(dyingLeft, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.East:
                                gr.DrawImage(dyingRight, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            default:
                                break;
                        }
                        if (p is Teacher)
                        {
                            gr.DrawImage(teacher, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                        if (p is Staff)
                        {
                            gr.DrawImage(staff, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                        if (p.Panic >= 33 && p.Panic < 66)
                        {
                            gr.DrawImage(panicMedium, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        } else if (p.Panic >= 66)
                        {
                            gr.DrawImage(panicBig, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                        }
                    } else if (p.Health == 0 && p.Jumped)
                    {
                        switch (p.Orientation)
                        {
                            case Orientation.North:
                                gr.DrawImage(skull, drawLocation.X, drawLocation.Y + TilePixelSize, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.South:
                                gr.DrawImage(skull, drawLocation.X, drawLocation.Y - TilePixelSize, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.West:
                                gr.DrawImage(skull, drawLocation.X - TilePixelSize, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            case Orientation.East:
                                gr.DrawImage(skull, drawLocation.X + TilePixelSize, drawLocation.Y, TilePixelSize, TilePixelSize);
                                break;
                            default:
                                break;
                        }
                    } else
                        gr.DrawImage(skull, drawLocation.X, drawLocation.Y, TilePixelSize, TilePixelSize);
                }

                if (p is Staff)
                {
                    if (((Staff)p).EquipedFireExtinguisher != null)
                    {
                        switch (p.Orientation)
                        {
                            case Orientation.North:
                                gr.DrawImage(fireExtinguisher, drawLocation.X + TilePixelSize / 2, drawLocation.Y, TilePixelSize / 2, TilePixelSize / 2);
                                break;
                            case Orientation.South:
                                gr.DrawImage(fireExtinguisher, drawLocation.X, drawLocation.Y + TilePixelSize / 2, TilePixelSize / 2, TilePixelSize / 2);
                                break;
                            case Orientation.West:
                                gr.DrawImage(fireExtinguisher, drawLocation.X, drawLocation.Y, TilePixelSize / 2, TilePixelSize / 2);
                                break;
                            case Orientation.East:
                                gr.DrawImage(fireExtinguisher, drawLocation.X + TilePixelSize / 2, drawLocation.Y + TilePixelSize / 2, TilePixelSize / 2, TilePixelSize / 2);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            #endregion
            // Update labels
            ResetStats();
            numSavedStateslbl.Text = (simulation.NumberOfSavedSimulationStates).ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (simulation.TimeElapsed != 0)
            { // If a previous simulation has run, a new simulation needs to be started
                var oldSimulation = simulation;
                simulation = new Simulation(loadedMap);
                simulation.Map.FireSpreadRate = oldSimulation.Map.FireSpreadRate;
                simulation.RedrawPanel += new RedrawPanelEventHandler(OnRedrawPanel);
            }

            MakeImage(TilePixelSize);
            simulation.Start();
            simulationRunning = true;
            ChangeButtonStatuses();
            ResetStats();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            simulation = new Simulation(loadedMap);
            ResetMap();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            ResetStats();
            if (simulation.Pause() == true) // Simulation is paused
                btnPause.Text = "Unpause";
            else                            // Simulation is unpaused
                btnPause.Text = "Pause";

            if (simulation.Running)
            {
                btnSetSpeed.Enabled = false;
                btnSetFireSpreadRate.Enabled = false;
            } else
            {
                btnSetSpeed.Enabled = true;
                btnSetFireSpreadRate.Enabled = true;
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopSimulationUI();
            simulation.Stop();
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            DialogResult dialogResult = MessageBox.Show("Would you like to save the simulation?", "Confirmation window", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SaveFileDialog sdf = new SaveFileDialog();
                if(sdf.ShowDialog() == DialogResult.OK)
                {
                    string x = Path.GetFullPath(sdf.FileName);
                    fh.SaveSimulation(simulation, x);
                }
                
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Would you like to load a new simulation?", "Confirmation window", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                Simulation deserializedSimulation = fh.LoadSimulation(this);
                if (deserializedSimulation != null)
                {
                    simulation.PersonList = deserializedSimulation.PersonList;
                    simulation.Map.TilesArray = deserializedSimulation.Map.TilesArray;
                    panelMap.Invalidate();
                } else
                {
                    MessageBox.Show("Invalid file path or Cancel button pressed.");
                }
            }
        }

        private void fontysFloorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMap("fontys");
            panelMap.Invalidate();
        }

        private void testMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMap("test");
            panelMap.Invalidate();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openMapFile = new OpenFileDialog();
            openMapFile.Filter = "Text file|*.txt";
            openMapFile.Title = "Open a map file";

            if (openMapFile.ShowDialog() == DialogResult.OK)
            {
                LoadMap("path:" + openMapFile.FileName);
            }

            panelMap.Invalidate(); ;
        }

        private void btnStartFire_Click(object sender, EventArgs e)
        {
            selectedTool = Tool.AddFireManually;
            ChangeButtonColours();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            if (personSelector.SelectedItem.ToString() == "Student")
            {
                selectedTool = Tool.AddStudent;
            }
            else if (personSelector.SelectedItem.ToString() == "Teacher")
            {
                selectedTool = Tool.AddTeacher;
            }
            else if (personSelector.SelectedItem.ToString() == "Staff")
            {
                selectedTool = Tool.AddStaff;
            }

            ChangeButtonColours();
        }

        private void panelMap_MouseClick(object sender, MouseEventArgs e)
        {
            Point clickedTile = new Point(((e.X - MapTopLeftXPos) / TilePixelSize), ((e.Y - MapTopLeftYPos) / TilePixelSize));

            #region Left Click
            if (e.Button == MouseButtons.Left)
            {
                if (selectedTool == Tool.None)
                {
                    if (simulation.SelectPerson(clickedTile))
                        panelMap.Invalidate();
                    else if (simulation.OpenOrCloseWindow(clickedTile))
                        panelMap.Invalidate();
                    else if (simulation.OpenOrCloseDoor(clickedTile))
                        panelMap.Invalidate();
                    else
                    {
                        selectedTool = Tool.None;
                        simulation.DeselectPerson();
                        panelMap.Invalidate();
                    }
                }
                else if (selectedTool == Tool.AddStudent || selectedTool == Tool.AddTeacher || selectedTool == Tool.AddStaff)
                {
                    if (selectedTool == Tool.AddStudent)
                    {
                        if (simulation.AddPerson(clickedTile, "Student")) panelMap.Invalidate();
                    } 
                    else if (selectedTool == Tool.AddTeacher)
                    {
                        if (simulation.AddPerson(clickedTile, "Teacher")) panelMap.Invalidate();
                    } 
                    else if (selectedTool == Tool.AddStaff)
                    {
                        if (simulation.AddPerson(clickedTile, "Staff")) panelMap.Invalidate();
                    }

                }
                else if (selectedTool == Tool.AddFireManually)
                {
                    if (simulation.AddFire(clickedTile))
                        panelMap.Invalidate();
                }
            }
            #endregion
            #region Right Click
            else if (e.Button == MouseButtons.Right)
            {
                if (selectedTool == Tool.None)
                {
                    if (simulation.SelectPerson(clickedTile))
                    {
                        if (!simulation.Running)
                        {
                            Person p = simulation.GetPerson(clickedTile);
                            var popupPersonParameters = new PersonParametersPopup(p.Health, p.Speed, p.Panic);
                            popupPersonParameters.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            popupPersonParameters.StartPosition = FormStartPosition.Manual;

                            if (popupPersonParameters.ShowDialog(this) == DialogResult.OK)
                            {
                                p.Health = popupPersonParameters.FormHealth;
                                p.Speed = popupPersonParameters.FormSpeed;
                                p.Panic = popupPersonParameters.FormPanic;

                                panelMap.Invalidate();
                            }
                        }
                    }
                    else if (simulation.ManuallyMovePerson(clickedTile))
                        panelMap.Invalidate();
                    else
                    {
                        selectedTool = Tool.None;
                        simulation.DeselectPerson();
                        panelMap.Invalidate();
                    }
                }
                else
                {
                    selectedTool = Tool.None;
                    ChangeButtonColours();
                }
            }
            #endregion
        }

        private void btnSetSpeed_Click(object sender, EventArgs e)
        {
            Console.WriteLine(simulation.SimulationInterval);
            simulation.Pause();
            simulation.SetSpeed(simulationSpeedUpDown.Value);
            simulation.Pause();
            Console.WriteLine(simulation.SimulationInterval);
        }

        private void btnSetFireSpreadRate_Click(object sender, EventArgs e)
        {
            simulation.SetFireSpreadRate((int)fireSpreadRateUpDown.Value);
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (TilePixelSize > 5)
                {
                    TilePixelSize -= 5;
                    hScrollBar.Maximum -= 2;
                    vScrollBar.Maximum -= 2;
                    int hOldValue = hScrollBar.Value;
                    int vOldValue = vScrollBar.Value;

                    if (vScrollBar.Value > 0 || hScrollBar.Value > 0)
                    {
                        vScrollBar.Value = 0;
                        hScrollBar.Value = 0;
                        MapTopLeftXPos = 50;
                        MapTopLeftYPos = 150;
                    }
                    MakeImage(TilePixelSize);
                    panelMap.Invalidate();
                }
            }
            else
            {
                if (TilePixelSize < 40)
                {
                    TilePixelSize += 5;
                    MakeImage(TilePixelSize);
                    panelMap.Invalidate();
                    hScrollBar.Maximum += 2;
                    vScrollBar.Maximum += 2;
                }
            }
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
            {
                MapTopLeftXPos -= (e.NewValue - e.OldValue) * simulation.Map.LengthOfMap * 3;
            } else
            {
                MapTopLeftXPos += (e.OldValue - e.NewValue) * simulation.Map.LengthOfMap * 3;
            }

            MakeImage(TilePixelSize);
            panelMap.Invalidate();
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
            {
                MapTopLeftYPos -= (e.NewValue - e.OldValue) * simulation.Map.HeightOfMap * 3;
            } else
            {
                MapTopLeftYPos += (e.OldValue - e.NewValue) * simulation.Map.HeightOfMap * 3;
            }

            MakeImage(TilePixelSize);
            panelMap.Invalidate();
        }

        private void btnRestoreState_Click(object sender, EventArgs e)
        {
            int j;
            if (Int32.TryParse(selectedStatetb.Text, out j))
            {
                simulation.RestoreState(j);
            }
            panelMap.Invalidate();
        }

        private void selectStatesBar_Scroll(object sender, EventArgs e)
        {
            selectedStatetb.Text = selectStatesBar.Value.ToString();
        }

        private void personSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (personSelector.SelectedItem.ToString() == "Student")
            {
                selectedTool = Tool.AddStudent;
            }
            else if (personSelector.SelectedItem.ToString() == "Teacher")
            {
                selectedTool = Tool.AddTeacher;
            }
            else if (personSelector.SelectedItem.ToString() == "Staff")
            {
                selectedTool = Tool.AddStaff;
            }

            ChangeButtonColours();
        }
    }

    // This class represents a modified ComboBox that allows us to draw the image of a person in it.
    public sealed class PersonSelector : ComboBox
    {
        public PersonSelector()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                PersonType item = (PersonType)Items[e.Index];

                e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top);

                e.Graphics.DrawString(item.Value, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }
    }

    //This class represents an element in the PersonSelector ComboBox.
    public sealed class PersonType
    {
        public string Value { get; set; }

        public Image Image { get; set; }

        public PersonType()
            : this("")
        { }

        public PersonType(string val)
        {
            Value = val;
            var resName = "";
            if (val == "Teacher")
            {
                resName = "teacher";

            } else if (val == "Staff")
            {
                resName = "staff";
            }

            Image = (Image)(new Bitmap(((Image)Properties.Resources.ResourceManager.GetObject("personDown")), new Size(16, 16)));
            using (Graphics g = Graphics.FromImage(Image))
            {
                if (resName != "")
                {
                    g.DrawImage((Image)(new Bitmap(((Image)Properties.Resources.ResourceManager.GetObject(resName)), new Size(16, 16))), 0, 0);
                }
            }
            //Image = (Image) (new Bitmap(((Image)Properties.Resources.ResourceManager.GetObject(resName)), new Size(16,16)));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
