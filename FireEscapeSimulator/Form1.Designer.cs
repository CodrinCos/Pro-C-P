namespace FireEscapeSimulator
{
    partial class h
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(h));
            this.panelMap = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEscaped = new System.Windows.Forms.Label();
            this.lblDead = new System.Windows.Forms.Label();
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.lblFireArea = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonHelp = new System.Windows.Forms.ToolStripDropDownButton();
            this.tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forAddingAPersonChooseTheTypeOfPeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadMap = new System.Windows.Forms.ToolStripDropDownButton();
            this.fontysFloorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStartFire = new System.Windows.Forms.Button();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetSpeed = new System.Windows.Forms.Button();
            this.simulationSpeedUpDown = new System.Windows.Forms.NumericUpDown();
            this.btnRestart = new System.Windows.Forms.Button();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.statusLabel = new System.Windows.Forms.Label();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetFireSpreadRate = new System.Windows.Forms.Button();
            this.fireSpreadRateUpDown = new System.Windows.Forms.NumericUpDown();
            this.numSavedStateslbl = new System.Windows.Forms.TextBox();
            this.selectedStatetb = new System.Windows.Forms.TextBox();
            this.btnRestoreState = new System.Windows.Forms.Button();
            this.selectStatesBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.personSelector = new FireEscapeSimulator.PersonSelector();
            this.gbRestoreState = new System.Windows.Forms.GroupBox();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationSpeedUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fireSpreadRateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectStatesBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.gbRestoreState.SuspendLayout();
            this.gbTools.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMap
            // 
            this.panelMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMap.AutoScroll = true;
            this.panelMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMap.Location = new System.Drawing.Point(137, 69);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(770, 550);
            this.panelMap.TabIndex = 0;
            this.panelMap.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMap_Paint);
            this.panelMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelMap_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.BackColor = System.Drawing.Color.Lime;
            this.btnStart.Location = new System.Drawing.Point(10, 72);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 35);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stats:";
            // 
            // lblEscaped
            // 
            this.lblEscaped.AutoSize = true;
            this.lblEscaped.Location = new System.Drawing.Point(172, 44);
            this.lblEscaped.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEscaped.Name = "lblEscaped";
            this.lblEscaped.Size = new System.Drawing.Size(57, 13);
            this.lblEscaped.TabIndex = 3;
            this.lblEscaped.Text = "0 escaped";
            // 
            // lblDead
            // 
            this.lblDead.AutoSize = true;
            this.lblDead.Location = new System.Drawing.Point(265, 44);
            this.lblDead.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDead.Name = "lblDead";
            this.lblDead.Size = new System.Drawing.Size(40, 13);
            this.lblDead.TabIndex = 4;
            this.lblDead.Text = "0 dead";
            // 
            // lblTimeElapsed
            // 
            this.lblTimeElapsed.Location = new System.Drawing.Point(347, 44);
            this.lblTimeElapsed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(162, 19);
            this.lblTimeElapsed.TabIndex = 12;
            this.lblTimeElapsed.Text = "Time elapsed:";
            // 
            // lblFireArea
            // 
            this.lblFireArea.AutoSize = true;
            this.lblFireArea.Location = new System.Drawing.Point(532, 44);
            this.lblFireArea.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFireArea.Name = "lblFireArea";
            this.lblFireArea.Size = new System.Drawing.Size(160, 13);
            this.lblFireArea.TabIndex = 6;
            this.lblFireArea.Text = "The fire spread to 0% of the map";
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPause.Enabled = false;
            this.btnPause.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPause.Location = new System.Drawing.Point(126, 127);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(95, 35);
            this.btnPause.TabIndex = 7;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.Firebrick;
            this.btnStop.Enabled = false;
            this.btnStop.ForeColor = System.Drawing.SystemColors.Control;
            this.btnStop.Location = new System.Drawing.Point(125, 72);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 35);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(10, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 35);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLoad.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLoad.Location = new System.Drawing.Point(125, 19);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(95, 35);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonHelp,
            this.toolStripSeparator1,
            this.loadMap});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1172, 27);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonHelp
            // 
            this.toolStripDropDownButtonHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem,
            this.tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem,
            this.youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem,
            this.youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem,
            this.youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem,
            this.restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem,
            this.youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem,
            this.youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem,
            this.forAddingAPersonChooseTheTypeOfPeToolStripMenuItem});
            this.toolStripDropDownButtonHelp.Image = global::FireEscapeSimulator.Properties.Resources.question_mark;
            this.toolStripDropDownButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonHelp.Name = "toolStripDropDownButtonHelp";
            this.toolStripDropDownButtonHelp.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonHelp.Text = "Help";
            // 
            // tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem
            // 
            this.tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem.Name = "tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem";
            this.tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem.Text = "Use the scroll wheel to zoom in or zoom out";
            // 
            // tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem
            // 
            this.tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem.Name = "tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem";
            this.tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem.Text = "Right click on a peson to change their parameters";
            // 
            // youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem
            // 
            this.youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem.Name = "youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem";
            this.youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem.Text = "You can left click on an empty tile to unselect a person";
            // 
            // youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem
            // 
            this.youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem.Name = "youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem";
            this.youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem.Text = "You right click on an empty tile to unselect a tool";
            // 
            // youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem
            // 
            this.youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem.Name = "youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelec" +
    "tingTheLocationWithRightClickToolStripMenuItem";
            this.youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem.Text = "You can make a person move to a certain location by left clicking on the person a" +
    "nd after that by selecting the location with right click";
            // 
            // restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem
            // 
            this.restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem.Name = "restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMe" +
    "nuItem";
            this.restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem.Text = "Restart button will reset the map to the initial state (without new person(s) and" +
    " fire(s))";
            // 
            // youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem
            // 
            this.youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem.Name = "youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem";
            this.youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem.Text = "You can reload an old state (rewind) with the controls on the right. Use the slid" +
    "er for rough adjustments or put the number of the state you wish to restore dire" +
    "ctly into the selected state box.";
            // 
            // youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem
            // 
            this.youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem.Name = "youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem";
            this.youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem.Text = "You can left click on a window to open or close it";
            // 
            // forAddingAPersonChooseTheTypeOfPeToolStripMenuItem
            // 
            this.forAddingAPersonChooseTheTypeOfPeToolStripMenuItem.Name = "forAddingAPersonChooseTheTypeOfPeToolStripMenuItem";
            this.forAddingAPersonChooseTheTypeOfPeToolStripMenuItem.Size = new System.Drawing.Size(1067, 22);
            this.forAddingAPersonChooseTheTypeOfPeToolStripMenuItem.Text = "For adding a person: choose the type of person, after that press the \"Add\" button" +
    " and then left click on the map where you want to place the person";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // loadMap
            // 
            this.loadMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadMap.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontysFloorToolStripMenuItem,
            this.testMapToolStripMenuItem,
            this.loadFileToolStripMenuItem});
            this.loadMap.Image = ((System.Drawing.Image)(resources.GetObject("loadMap.Image")));
            this.loadMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadMap.Name = "loadMap";
            this.loadMap.Size = new System.Drawing.Size(73, 24);
            this.loadMap.Text = "Load map";
            this.loadMap.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.loadMap.ToolTipText = "Load map";
            // 
            // fontysFloorToolStripMenuItem
            // 
            this.fontysFloorToolStripMenuItem.Name = "fontysFloorToolStripMenuItem";
            this.fontysFloorToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.fontysFloorToolStripMenuItem.Text = "Fontys floor";
            this.fontysFloorToolStripMenuItem.Click += new System.EventHandler(this.fontysFloorToolStripMenuItem_Click);
            // 
            // testMapToolStripMenuItem
            // 
            this.testMapToolStripMenuItem.Name = "testMapToolStripMenuItem";
            this.testMapToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.testMapToolStripMenuItem.Text = "Test map";
            this.testMapToolStripMenuItem.Click += new System.EventHandler(this.testMapToolStripMenuItem_Click);
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.loadFileToolStripMenuItem.Text = "Load file";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // btnStartFire
            // 
            this.btnStartFire.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartFire.Location = new System.Drawing.Point(6, 96);
            this.btnStartFire.Name = "btnStartFire";
            this.btnStartFire.Size = new System.Drawing.Size(75, 23);
            this.btnStartFire.TabIndex = 14;
            this.btnStartFire.Text = "Start fire";
            this.btnStartFire.UseVisualStyleBackColor = true;
            this.btnStartFire.Click += new System.EventHandler(this.btnStartFire_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddPerson.Location = new System.Drawing.Point(6, 42);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(63, 23);
            this.btnAddPerson.TabIndex = 15;
            this.btnAddPerson.Text = "Add";
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSetSpeed);
            this.groupBox1.Controls.Add(this.simulationSpeedUpDown);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(912, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(249, 52);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulation Speed";
            // 
            // btnSetSpeed
            // 
            this.btnSetSpeed.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSetSpeed.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSetSpeed.Location = new System.Drawing.Point(174, 25);
            this.btnSetSpeed.Name = "btnSetSpeed";
            this.btnSetSpeed.Size = new System.Drawing.Size(70, 24);
            this.btnSetSpeed.TabIndex = 18;
            this.btnSetSpeed.Text = "Set";
            this.btnSetSpeed.UseVisualStyleBackColor = false;
            this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
            // 
            // simulationSpeedUpDown
            // 
            this.simulationSpeedUpDown.DecimalPlaces = 1;
            this.simulationSpeedUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.simulationSpeedUpDown.Location = new System.Drawing.Point(4, 29);
            this.simulationSpeedUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.simulationSpeedUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.simulationSpeedUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.simulationSpeedUpDown.Name = "simulationSpeedUpDown";
            this.simulationSpeedUpDown.Size = new System.Drawing.Size(165, 20);
            this.simulationSpeedUpDown.TabIndex = 0;
            this.simulationSpeedUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestart.BackColor = System.Drawing.Color.Lime;
            this.btnRestart.Enabled = false;
            this.btnRestart.Location = new System.Drawing.Point(10, 127);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(95, 35);
            this.btnRestart.TabIndex = 18;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar.Location = new System.Drawing.Point(117, 69);
            this.vScrollBar.Maximum = 12;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 550);
            this.vScrollBar.TabIndex = 20;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(1051, 453);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(10, 13);
            this.statusLabel.TabIndex = 11;
            this.statusLabel.Text = " ";
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Location = new System.Drawing.Point(137, 622);
            this.hScrollBar.Maximum = 12;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(770, 17);
            this.hScrollBar.TabIndex = 21;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSetFireSpreadRate);
            this.groupBox2.Controls.Add(this.fireSpreadRateUpDown);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(912, 134);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(249, 52);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fire spread rate";
            // 
            // btnSetFireSpreadRate
            // 
            this.btnSetFireSpreadRate.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSetFireSpreadRate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSetFireSpreadRate.Location = new System.Drawing.Point(174, 27);
            this.btnSetFireSpreadRate.Name = "btnSetFireSpreadRate";
            this.btnSetFireSpreadRate.Size = new System.Drawing.Size(70, 24);
            this.btnSetFireSpreadRate.TabIndex = 18;
            this.btnSetFireSpreadRate.Text = "Set";
            this.btnSetFireSpreadRate.UseVisualStyleBackColor = false;
            this.btnSetFireSpreadRate.Click += new System.EventHandler(this.btnSetFireSpreadRate_Click);
            // 
            // fireSpreadRateUpDown
            // 
            this.fireSpreadRateUpDown.Location = new System.Drawing.Point(4, 29);
            this.fireSpreadRateUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.fireSpreadRateUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.fireSpreadRateUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.fireSpreadRateUpDown.Name = "fireSpreadRateUpDown";
            this.fireSpreadRateUpDown.Size = new System.Drawing.Size(165, 20);
            this.fireSpreadRateUpDown.TabIndex = 0;
            this.fireSpreadRateUpDown.Value = new decimal(new int[] {
            40,
            0,
            0,
            65536});
            // 
            // numSavedStateslbl
            // 
            this.numSavedStateslbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numSavedStateslbl.Enabled = false;
            this.numSavedStateslbl.Location = new System.Drawing.Point(5, 32);
            this.numSavedStateslbl.Name = "numSavedStateslbl";
            this.numSavedStateslbl.ReadOnly = true;
            this.numSavedStateslbl.Size = new System.Drawing.Size(114, 20);
            this.numSavedStateslbl.TabIndex = 24;
            // 
            // selectedStatetb
            // 
            this.selectedStatetb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedStatetb.Location = new System.Drawing.Point(166, 32);
            this.selectedStatetb.Name = "selectedStatetb";
            this.selectedStatetb.Size = new System.Drawing.Size(75, 20);
            this.selectedStatetb.TabIndex = 25;
            this.selectedStatetb.Text = "0";
            // 
            // btnRestoreState
            // 
            this.btnRestoreState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreState.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRestoreState.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnRestoreState.Location = new System.Drawing.Point(176, 106);
            this.btnRestoreState.Name = "btnRestoreState";
            this.btnRestoreState.Size = new System.Drawing.Size(70, 24);
            this.btnRestoreState.TabIndex = 26;
            this.btnRestoreState.Text = "Restore State";
            this.btnRestoreState.UseVisualStyleBackColor = false;
            this.btnRestoreState.Click += new System.EventHandler(this.btnRestoreState_Click);
            // 
            // selectStatesBar
            // 
            this.selectStatesBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectStatesBar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.selectStatesBar.Location = new System.Drawing.Point(4, 58);
            this.selectStatesBar.Maximum = 999;
            this.selectStatesBar.Name = "selectStatesBar";
            this.selectStatesBar.Size = new System.Drawing.Size(237, 45);
            this.selectStatesBar.TabIndex = 27;
            this.selectStatesBar.Scroll += new System.EventHandler(this.selectStatesBar_Scroll);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Selected State";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Total number of states";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnAddPerson);
            this.groupBox3.Controls.Add(this.personSelector);
            this.groupBox3.Location = new System.Drawing.Point(6, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(75, 71);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Add person";
            // 
            // personSelector
            // 
            this.personSelector.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.personSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.personSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.personSelector.FormattingEnabled = true;
            this.personSelector.Location = new System.Drawing.Point(6, 19);
            this.personSelector.Name = "personSelector";
            this.personSelector.Size = new System.Drawing.Size(63, 21);
            this.personSelector.TabIndex = 30;
            this.personSelector.SelectedIndexChanged += new System.EventHandler(this.personSelector_SelectedIndexChanged);
            // 
            // gbRestoreState
            // 
            this.gbRestoreState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRestoreState.Controls.Add(this.label3);
            this.gbRestoreState.Controls.Add(this.numSavedStateslbl);
            this.gbRestoreState.Controls.Add(this.btnRestoreState);
            this.gbRestoreState.Controls.Add(this.selectStatesBar);
            this.gbRestoreState.Controls.Add(this.label2);
            this.gbRestoreState.Controls.Add(this.selectedStatetb);
            this.gbRestoreState.Location = new System.Drawing.Point(912, 191);
            this.gbRestoreState.Name = "gbRestoreState";
            this.gbRestoreState.Size = new System.Drawing.Size(247, 130);
            this.gbRestoreState.TabIndex = 32;
            this.gbRestoreState.TabStop = false;
            this.gbRestoreState.Text = "Restoring states";
            // 
            // gbTools
            // 
            this.gbTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbTools.Controls.Add(this.groupBox3);
            this.gbTools.Controls.Add(this.btnStartFire);
            this.gbTools.Location = new System.Drawing.Point(12, 513);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(93, 123);
            this.gbTools.TabIndex = 33;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Tools";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.btnStart);
            this.groupBox5.Controls.Add(this.btnRestart);
            this.groupBox5.Controls.Add(this.btnPause);
            this.groupBox5.Controls.Add(this.btnLoad);
            this.groupBox5.Controls.Add(this.btnStop);
            this.groupBox5.Location = new System.Drawing.Point(922, 442);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(231, 177);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Simulation controls";
            // 
            // h
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 648);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.gbTools);
            this.Controls.Add(this.gbRestoreState);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.lblFireArea);
            this.Controls.Add(this.lblTimeElapsed);
            this.Controls.Add(this.lblDead);
            this.Controls.Add(this.lblEscaped);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelMap);
            this.MinimumSize = new System.Drawing.Size(1086, 686);
            this.Name = "h";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Fire Escape Simulator";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.simulationSpeedUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fireSpreadRateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectStatesBar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.gbRestoreState.ResumeLayout(false);
            this.gbRestoreState.PerformLayout();
            this.gbTools.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMap;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEscaped;
        private System.Windows.Forms.Label lblDead;
        private System.Windows.Forms.Label lblTimeElapsed;
        private System.Windows.Forms.Label lblFireArea;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton loadMap;
        private System.Windows.Forms.ToolStripMenuItem fontysFloorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.Button btnStartFire;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetSpeed;
        private System.Windows.Forms.NumericUpDown simulationSpeedUpDown;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSetFireSpreadRate;
        private System.Windows.Forms.NumericUpDown fireSpreadRateUpDown;
        private System.Windows.Forms.TextBox numSavedStateslbl;
        private System.Windows.Forms.TextBox selectedStatetb;
        private System.Windows.Forms.Button btnRestoreState;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonHelp;
        private System.Windows.Forms.ToolStripMenuItem tipUseTheScrollWheelToZoomInOrZoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tipRightClickOnAPesonToChangeTheirParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem youCanMakeAPersonMoveToACertainLocationByLeftClickingOnAPersonAndAfterThatBySelectingTheLocationWithRightClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restartButtonWillResetTheMapToTheInitialStatewithoutNewPersonsAndFiresToolStripMenuItem;
        private System.Windows.Forms.TrackBar selectStatesBar;
        private System.Windows.Forms.ToolStripMenuItem youCanReloadAnOldStaterewindWithTheControlsOnTheRightToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private PersonSelector personSelector;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox gbRestoreState;
        private System.Windows.Forms.ToolStripMenuItem youCanLeftClickOnAWindowToOpenOrCloseItToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forAddingAPersonChooseTheTypeOfPeToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripMenuItem youCanLeftClickOnAnEmptyTileToUnselectAPersonOrAToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem youRightClickOnAnEmptyTileToUnselectAToolToolStripMenuItem;
    }
}

