using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace TurtleSim2000_Linux
{

    public class Game1 : Game
    {

        //just for reference.  not really important
        String GameInfo = "TurtleSim 2000 (Build 77) v0.6 BETA";

        #region Public Defined Variables
        //fonts
        SpriteFont debugfont;
        SpriteFont debugfontsmall;
        SpriteFont speechfont;
        SpriteFont clockfont;
        int frames = 0;

        //gui textures
        Texture2D buttonup;
        Texture2D buttondown;

        Texture2D messagebox;
        Texture2D messagebox2;
        Texture2D messagebox3;
        Texture2D clock_tex;
        Texture2D buttonselector;
        Texture2D ButtonA;

        //Chara
        CharaManager charaManager;           // NEWEST WAY OF DEALING WITH EVERYTHING TO DO WITH CHARA!

        //music
        Song basic;                      //old way of storing music
        Song m_daylight;                 //old way of storing music
        bool songstart = false;          //Start playing music (set to false to switch song)

        // Sound FX
        SoundEffect soundEffect;         //Holds a sound.  plays if asked nicely

        ScriptCompiler MasterScript = new ScriptCompiler();    //Handles all scripts and puts them in one place.
        int totscripts = 0;              //Gets the total amount of compiled scripts

        int actionmenuscroller = -300;   //moves the menu back and forth when it is called or closed

        //engine bool
        bool bStart = true;               //Main game menu.  (only true on startup.)
        bool bSleep = false;              //Determines if the player is asleep.
        bool bActive = false;             //Determines if the player is active.  (walking; eating; ext)
        bool bGamerun = false;            //Continues to loop through; should be true always after start
        bool bLoner = false;              //Determines if the player is being too anti-social
        bool bDorm = false;               //Tells the game that player is in the dorm room
        bool bMenu = false;               //Calls the main action select menu.
        bool bError = false;              //calls the ERROR textbox.  halts game.
        bool bAddtime = true;            //tells the clock to add +1 time until it reached the set time by a variable
        bool bHud = false;                //will show the hud.  false will hide it
        bool bFirstrun = false;           //Sets the game up; only enable this to refresh all variables to default
        bool bShowtext = false;           //Tells the engine to show textbox and run through script
        bool bShowtextWindow = true;      // Tells wether to show or hide the text window during dialog
        bool bShowTransWindow = false;    // Draw the transparent text window?
        bool bRunevent = false;           //Tells the engine to run a specific event (actions)
        bool bGameover = false;           //If the player loses; opens a new scene
        bool bWin = false;                //If the player wins; opens a new scene
        bool fixfirstscripterror = true;  //helps patch up the "nothing to say" error at first event.
        bool bQuestion = false;           //If the player is being asked a question, this halts script reader from continueing.
        bool bDebugmode = false;          //tells the game to run a certain script set by the developer on startup.
        bool bRunTut = false;             //first time running the game will put it in tutorial mode.
        bool bWait = false;               //tells the game to hold while in a script
        bool reRunAfterWait = false;      //Tells the game to re-run script commands because a WAIT command shut them off too early.
        bool bReRunScriptInit = false;    // Tells the game to re-run script init.  Usually done after a script jump.  This makes everything clean.
        bool bPlayMusic = true;           //Determines if music should play.. or not.  (determined by user)
        bool bAuthorMode = false;          //tells the game to run a debug script on startup.

        //GAME STORY ENGINE VARIABLES
        string[] forkAnswers = new string[8];
        string[] forkScript = new string[8];

        string SetMusic;

        int dpadx = 0;
        int dpady = 0;
        int SelectorPosX = 0;      //Sets the Button selector along X coords; for Xbox360
        int SelectorPosY = 0;      //Sets Y axis.  (these keep it memorised too)

        string eventname = "";
        string charatalk = "No Name:\n\"";
        string ErrorReason = "Fuck, I don't know.";

        //Game Objects
        PlayerData Player = new PlayerData();
        Controls controller = new Controls();
        //VariableControl VC = new VariableControl();
        GameEvents gameEvents = new GameEvents();
        Stamps stamps = new Stamps();
        Save gameSaver = new Save();
        Background bgManager;                       // Handles all background bullshit.  Show/Import
        Scene_Start sceneStart;                     // SCENE: Handles the start scene and all of it's controls.
        ProgressBar pBar;
        ProgressBar pBar_Social;
        ProgressBar pBar_Fat;
        ProgressBar pBar_Energy;
        ProgressBar pBar_Charlsee;
        ProgressBar pBar_HeroHP;
        Clock clock;

        int FakeDayofWeek = 0;                       //Used to make sure an event doesn't run twice in one day.
        int WaitTime = 0;                            //amount of time (in seconds) to wait in a script.

        int Turns = 0;                               //How many actions the player has done in one game playthrough

        //string[,] script = new string[101,500];     //MasterScript string array; holds all scripts (old)
        string dialouge = "Nothing to say";         //This pulls the dialouge from script and displays it.
        int textWindowYLocation = 320;              // Tells where the dialog box should sit on the Y axis.
        string charaCode;                           // Tells who is speaking in speech
        Color charaColor;                           // Tells what color the chara's name should be in
        int scriptreaderx = 0;                      //ScriptReaderx tells what script to read from
        int scriptreadery = 0;                      //Scriptreadery tells what line to read from

        //animation related
        string TransitionType;
        Transitions transition = new Transitions();             // Class that deals with game transitions.

        // System related
        int screenSizeWidth;                                     // Stores the size of the screen int X and Y fasion.
        int screenSizeHeight;
        bool bFullScreen = false;                               // True: sets game for fullscreen.  False: sets game for normal windowed mode (480x800)

        string dialougetr;                                      //Used to add a TypeWritter effect (prints char by char.)
        int dialogCharPos = 0;                                  //tells what char was last typed.
        bool bTypewritting = false;                             //tell the typewritter to type.
        int bgScroller = 0;                                     //Scrolls the background to create parallaxing effect
        int bgParallax = 0;                                     //applys bgScroller to the background

        //animation frame ints
        int AbuttonFrame = 0;                                   //Moves the "A" button up/down

        //just for testing and messing
        Random Rando = new Random();         //Gives us a random generated number
        float vibrator = 0.1f;               //Controls Vibration function for controller 1
        float vibrator2 = 0.1f;              //For Controller 2.
        #endregion

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Interface GUI = new Interface();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;
            base.Initialize();

            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 800;

            //dbWindow.Show();

            for (int x = 0; x < 500; x++)
            {
                Player.GameSwitches[x] = false;
                Player.GameVariables[x] = 0;
            }

            // launch in fullscreen or in windowed.
            if (bFullScreen)
            {
                // Get fullscreen resolution.  (probably not a good idea to get it from DisplayMode.)
                screenSizeWidth = GraphicsDevice.DisplayMode.Width;
                screenSizeHeight = GraphicsDevice.DisplayMode.Height;

                graphics.ToggleFullScreen();
            }
            else
            {
                screenSizeHeight = 480;
                screenSizeWidth = 800;
            }

            //graphics.ToggleFullScreen();

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Loads all game assets on startup
            #region Content Loader
            //GUI specific loaders
            buttonup = Content.Load<Texture2D>("assets/gui/gui_button_up");
            buttondown = Content.Load<Texture2D>("assets/gui/gui_button_down");
            buttonselector = Content.Load<Texture2D>("assets/gui/gui_button_selector");

            
            debugfont = Content.Load<SpriteFont>("fonts/debugfont");
            debugfontsmall = Content.Load<SpriteFont>("fonts/debugfontsmall");
            speechfont = Content.Load<SpriteFont>("fonts/speechfont");
            clockfont = Content.Load<SpriteFont>("fonts/clockfont");
            messagebox = Content.Load<Texture2D>("assets/gui/messagebox");
            messagebox2 = Content.Load<Texture2D>("assets/gui/messagebox2");
            messagebox3 = Content.Load<Texture2D>("assets/gui/messagebox3");
            clock_tex = Content.Load<Texture2D>("assets/gui/clock");
            ButtonA = Content.Load<Texture2D>("assets/gui/gui_button_A");

            //music
            basic = Content.Load<Song>("assets/music/Ah_Eh_I_Oh_You");
            m_daylight = Content.Load<Song>("assets/music/Daylight");

            //SoundFX
            soundEffect = Content.Load<SoundEffect>("assets/soundfx/doorslam");

            //GUI OBJECTS
            //PASS ALL TEXTURE AND SPRITEFONTS AS REFERENCE!
            GUI.LoadContent(this.Content, spriteBatch);
            #endregion

            // Load Transitions
            transition.loadContent(spriteBatch, this.Content);

            //Game Object Inits
            totscripts = MasterScript.Compile();
            bAuthorMode = MasterScript.IsDebug();
            stamps.Content(messagebox, debugfont);

            // setup charamanager
            charaManager = new CharaManager(this.Content);
            bgManager = new Background(this.Content);

            // setup scenes
            sceneStart = new Scene_Start(this.Content, screenSizeWidth, screenSizeHeight);
            sceneStart.GameInfo = GameInfo;

            // Progress Bar testing
            pBar = new ProgressBar(this.Content, "HP", new Rectangle(10, 10, 180, 40));
            pBar_Energy = new ProgressBar(this.Content, "Energy", new Rectangle(10, 35, 180, 40));
            pBar_Fat = new ProgressBar(this.Content, "Fat", new Rectangle(10, 85, 1, 1));
            pBar_Social = new ProgressBar(this.Content, "Social", new Rectangle(10, 60, 1, 1));
            pBar_Charlsee = new ProgressBar(this.Content, "Charlsee HP", new Rectangle(20, 20, 1, 1));
            pBar_HeroHP = new ProgressBar(this.Content, "Hero HP", new Rectangle(600, 340, 1, 1));

            // Clock
            clock = new Clock(this.Content);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        // ==================================================================================
        // ============================ Game Update Function ================================
        // ==================================================================================
        protected override void Update(GameTime gameTime)
        {
            frames += 1;

            stamps.update();
            bgManager.Update();
            clock.Update();
            Player.Update();
            

            // update game variable 100 randomly every frame.
            Player.GameVariables[100] = Rando.Next(6);


            pBar.setValue(Player.State.HP);
            pBar_Fat.setValue(Player.State.Fat);
            pBar_Energy.setValue(Player.State.Energy);
            pBar_Social.setValue(Player.State.Social);
            pBar_HeroHP.setValue(Player.GameVariables[80]);
            pBar_Charlsee.setValue(Player.GameVariables[81]);

            pBar.Update();
            pBar_Fat.Update();
            pBar_Energy.Update();
            pBar_Social.Update();
            pBar_Charlsee.Update();
            pBar_HeroHP.Update();

            // Dick with transition update function
            transition.Update();

            #region GameLogic (win/lose)
            //lets do some updates from Variable Control.

            if (bWin == true)
            {
                eventname = "win";
                bShowtext = true;
            }
            if (bGameover == true)
            {
                eventname = "lose";
                bShowtext = true;
            }
            #endregion

            #region Music Controls
            //Music Controls
            MediaPlayer.IsRepeating = true;
            if (!songstart)
            {
                if (bDorm == true) MediaPlayer.Play(m_daylight);
                if (bStart == true) MediaPlayer.Play(basic);
                songstart = true;

            }
            if (!bPlayMusic)
            {
                MediaPlayer.Stop();
            }
            #endregion

            #region Parallax Effect (Background)
            // Background Scrolling (Parallax)
            if (bgScroller != 0)
            {
                if (bgScroller > 0)
                {
                    bgParallax++;
                    bgScroller--;
                }
                if (bgScroller < 0)
                {
                    bgParallax--;
                    bgScroller++;
                }
            }
            #endregion

            #region Transitions
            if (TransitionType != null)
            {
                if (TransitionType == "FadeBlack")
                {
                    // BlackSwipe Logic
                }
                else
                {
                    ErrorReason = "The Transition Type: " + TransitionType + ", does not exist.";
                    bError = true;
                }
            }
            #endregion

            #region "A" Button Animator
            if (frames >= 1)
            {
                AbuttonFrame -= 10;
                if (AbuttonFrame <= 50) AbuttonFrame = 0;
                frames = 0;
            }
            #endregion

            #region DebugMode Handler
            if (bDebugmode == true)
            {
                // bShowtext = true;
            }
            #endregion

            #region Wait Script Control Handler
            //Pause time during scripts
            if (bWait == true)
            {
                if (WaitTime >= 1)
                {
                    WaitTime--;
                }
                else
                    bWait = false;
            }
            #endregion

            //-------------------------- Controls (mouse and button actions) -----------------
            // Updates all controls (Mouse, Keyboard, GamePad)
            controller.Update();

            if (controller.bGamePad == true) ButtonSelector();

            #region StartScreen Logic
            //handles for start screen
            if (bStart == true)
            {
                // update scene_Start
                sceneStart.Update();

                //buttons to click
                #region Button Controller

                // Send Control updates to all scenes:
                sceneStart.UpdateControls(controller.MousePos, controller.bClicked);


                if (controller.bClicked == true || bAuthorMode == true)
                {
                    if (bAuthorMode == true)
                    {
                        eventname = "debug";
                        bDorm = true;
                        bRunevent = true;
                        bStart = false;
                        bDebugmode = true;
                        bShowtext = true;
                        sceneStart.Unload();

                    }
                    if (sceneStart.btnDemo.bPressed)
                    {
                        eventname = "demo";
                        bDorm = true;
                        bRunevent = true;
                        bStart = false;
                        bDebugmode = true;
                        bShowtext = true;
                        sceneStart.Unload();
                    }

                    if (sceneStart.sceneOptions.btnFullScreen.bPressed)
                    {
                        graphics.ToggleFullScreen();
                    }

                    if (sceneStart.btnQuit.bPressed)
                    {
                        this.Exit();
                    }
                    if (sceneStart.btnStart.bPressed)
                    {
                        bFirstrun = true;
                        //sceneStart.Unload();
                    }

                    // For continueing from savegame
                    if (sceneStart.btnContinue.bPressed)
                    {
                        bDorm = true;

                        // Load from file and store into game variables.
                        gameSaver.sD.gVariables = Player.GameVariables;
                        gameSaver.loadFromFile();
                        Player.loadFromSave();
                        //bStart = false;
                        
                        // setup background stuff
                        bgManager.setBackground("School_ProDorm_bedroom");
                        bgManager.setBackgroundDimensions(800, 480);
                        bgManager.bShowBackground = true;

                        eventname = "wut";
                        bRunevent = true;
                        bShowtext = true;
                        bStart = false;
                        bDorm = true;
                        bHud = true;
                        //bRunevent = true;
                    }
                    else
                    {
                        if (controller.bGamePad == true)
                        {
                           // bFirstrun = true;
                        }
                    }
                }
            }
            #endregion

            charaManager.Update();

            #endregion

            //----------------------------------- Scripts Controls-------------------------------

            // Events() SetupGame() DayEvents() PopEvents() are all called here.
            #region Game Event Handlers
            if (bFirstrun == true || bRunTut == true) setupgame();

            animateactionmenu();  //calls the animator to move the menu about.
                                  //Also handles button collision events

            //Events ran from player selection
            if (bRunevent == true)
            {
                // zero out scriptreadery for bug on startup
                scriptreadery = 0;

                //I just put them all into a function.
                Events();

                bRunevent = false;
            }

            //Waits for the Action Menu to be gone before drawing the text box
            if (bShowtext == true)
            {
                bMenu = false;
                if (actionmenuscroller == -300) textwindow(eventname);
            }

            //place events here.  it will only run once then de-activate itself.

            //events ran by either random, or incur onto the player.
            if (bMenu) PopEvents();

            if (bQuestion == true) ForkQuestion();

            #endregion

            #region Typewritter Effect
            //adds a typewritter effect to the dialog.
            if (bTypewritting)
            {
                int strlng = dialougetr.Length;
                //if player clicks during mid-typing; just print it all at once.
                if (controller.bClicked == true && dialogCharPos >= 2)
                {
                    dialouge = dialougetr;
                    dialogCharPos = strlng;

                }
                else
                {
                    //Else; print char by char.
                    dialouge = dialouge + dialougetr[dialogCharPos];
                    dialogCharPos++;
                }
                if (dialogCharPos >= strlng)
                {
                    bTypewritting = false;
                    dialogCharPos = 0;
                }
            }
            #endregion

            #region Change Background & Music Logic
            if (bDorm == true)
            {
            }

            // SafetoSwap checks if transitions are in a spot that we can change the bg without it looking janky.
            // Use two variables for holding backgrounds because it will revert back to DormRoom if there is no active BG.
            if (SetMusic != null) m_daylight = Content.Load<Song>("assets/music/" + SetMusic + "");

            if (transition.SafeToSwap()) bgManager.Swap();

            #endregion

            controller.bClicked = false;

            base.Update(gameTime);
        }

        // ===============================================================================================
        // ====================================== Game Draw Function =====================================
        // ===============================================================================================
        protected override void Draw(GameTime gameTime)
        {

            Color clr = Color.White;
            if (bQuestion == true) clr = Color.Gray;

            //set background color
            GraphicsDevice.Clear(Color.DarkRed);

            // If game is just starting, Handle the start screen
            if (bStart == true) sceneStart.Draw(spriteBatch);

            
            spriteBatch.Begin();

            //background manage here
            if (bDorm == true)  //CHANGE THIS TO GO OFF A DIFFERENT BOOL!!!!
            {
                //spriteBatch.Draw(bg_manage, new Rectangle(-50 + bgParallax, 0, 900, 500), clr);
                bgManager.Draw(spriteBatch);
            }

            if (bDebugmode) spriteBatch.DrawString(debugfontsmall, "AUTHOR DEBUG MODE", new Vector2(670, 11), Color.White);

            //Draw The Action Menu
            GUI.ActionMenuShow(actionmenuscroller, Player.GameVariables[490], Player);
            if (bMenu == true) GUI.ClassWindowShow(Player.Time.Day, Player.Time.weekDay, Player.Schedule.currentClass, Player.GameVariables[452]);
          
            //Draw Progress bars.
            if (bHud == true)
            {
                spriteBatch.Draw(messagebox, new Rectangle(0, -5, 300, 130), Color.White * 0.8f);
                pBar.Draw(spriteBatch);
                pBar_Energy.Draw(spriteBatch);
                pBar_Social.Draw(spriteBatch);
                pBar_Fat.Draw(spriteBatch);
            }


            if (bHud == true) clock.Draw(spriteBatch);

            if (bShowtext == true)
            {
                if (actionmenuscroller == -300)
                {

                    // Draw chara
                    charaManager.Draw(spriteBatch);

                    transition.Draw();

                    if (bShowtextWindow) spriteBatch.Draw(messagebox2, new Rectangle(0, textWindowYLocation, 797, 125), Color.White);
                    if (bShowTransWindow) spriteBatch.Draw(messagebox3, new Rectangle(0, textWindowYLocation, 800, 125), Color.White);
                    if (dialouge != null) spriteBatch.DrawString(speechfont, dialouge, new Vector2(30, textWindowYLocation + 10), Color.White);
                    if (charaCode != null) spriteBatch.Draw(messagebox2, new Rectangle(10, textWindowYLocation - 19, 200, 30), Color.White);
                    if (charaCode != null) spriteBatch.DrawString(speechfont, charaCode, new Vector2(25, textWindowYLocation - 22), charaColor);
                    spriteBatch.Draw(ButtonA, new Rectangle(750, textWindowYLocation + 89 + AbuttonFrame, 24, 24), Color.White);
                }

            }

            if (bQuestion == true) GUI.ForkQuestionShow(forkAnswers);

            if (Player.GameVariables[80] > 0)
            {
                pBar_HeroHP.Draw(spriteBatch);
                pBar_Charlsee.Draw(spriteBatch);
            }

            stamps.draw(spriteBatch);

            // If in author mode draw TSS version please
            if (bAuthorMode)
            {
                spriteBatch.DrawString(debugfontsmall, GameInfo, new Vector2(536, 0), Color.White);
            }
            
            spriteBatch.End();

            #region Debug/Error
            //If bError is set to true, it will display the error reason set by developer
            if (bError == true)
            {
                //This should stop most of the game.
                bMenu = false;
                bDorm = false;
                bActive = false;
                bShowtext = false;

                spriteBatch.Begin();
                spriteBatch.Draw(messagebox, new Rectangle(60, 260, 600, 120), Color.White);
                spriteBatch.DrawString(debugfont, "FUCK! A PROBLEM HAPPENED!", new Vector2(90, 280), Color.White);
                spriteBatch.DrawString(debugfontsmall, "ERROR: " + ErrorReason, new Vector2(90, 310), Color.White);
                spriteBatch.DrawString(debugfontsmall, "Info: " + GameInfo, new Vector2(90, 345), Color.White);
                spriteBatch.End();

            }

            #endregion

            base.Draw(gameTime);
        }

        // =========================================================================================
        // ==================================== My Custom Functions ================================
        // =========================================================================================

        //starts game logic by seting up the rules and variables
        protected void setupgame()
        {
            if (bFirstrun == true)
            {
                bDorm = true;
                bgManager.setBackground("School_ProDorm_bedroom");
                bgManager.setBackgroundDimensions(800, 480);
                bgManager.bShowBackground = true;
                eventname = "gamestart_short";
                bShowtext = true;
                bError = false;
                //This error should never happen...
                ErrorReason = "How did you fuck up in the tutorial?  fuck.";
                bFirstrun = false;
                songstart = false;
                bStart = false;
                
                // Randomly create class names
                Player.CreateClasses(Rando, 3);
            }

            if (bRunTut == true)
            {
                Player.addHp(60);
                Player.addEnergy(60);
                Player.addSocial(40);
                Player.addFat(20);
                bDorm = true;
                bHud = true;
                ErrorReason = "LOL, there is no script to run.  Sorry, bro.";
                //eventname = "gamestart";
                //bShowtext = true;
                bShowtext = false;
                bError = false;
                bMenu = true;
                bFirstrun = false;
                bStart = false;
                songstart = false;
                bRunTut = false;
            }

        }
 
        //Waits for player to answer, then sends answer to script
        protected void ForkQuestion()
        {

            // Set new bounds if in full screen.
            float screenModX = screenSizeWidth / 800;
            float screenModY = screenSizeHeight / 480;

            // Button Bounds width and height
            int sModW = 500;
            int sModH = 40;
            if (bFullScreen)
            {
                // if full screen; do the math
                screenModY += .3f;
                screenModX += .45f;
                sModW = Convert.ToInt32(screenModX * 500);
                sModH = Convert.ToInt32(screenModY * 40);
            }

            if (controller.bClicked == true && bWait == false)
            {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);

                Rectangle[] Q = new Rectangle[8];
                Q[0] = new Rectangle(Convert.ToInt32(200 * screenModX), Convert.ToInt32(140 * screenModY), sModW, sModH);  // increment y by 60
                Q[1] = new Rectangle(Convert.ToInt32(200 * screenModX), Convert.ToInt32(200 * screenModY), sModW, sModH);
                Q[2] = new Rectangle(Convert.ToInt32(200 * screenModX), Convert.ToInt32(260 * screenModY), sModW, sModH);
                Q[3] = new Rectangle(200, 320, 500, 40);
                Q[4] = new Rectangle(200, 380, 500, 40);
                Q[5] = new Rectangle(200, 440, 500, 40);  // when this is reached, we will need to change Y
                Q[6] = new Rectangle(200, 220, 500, 40);
                Q[7] = new Rectangle(200, 220, 500, 40);
                //Q[8] = new Rectangle(200, 220, 500, 40);


                if (controller.bGamePad == false)
                {
                    int i = 0;
                    while (forkAnswers[i] != null)
                    {
                        if (Q[i].Contains(mousePosition))
                        {
                            bQuestion = false;
                            eventname = forkScript[i];
                            scriptreaderx = 0;
                            scriptreadery = 0;
                            charaManager.setDarkenChara(false);
                        }
                        i++;
                    }
                }
                else
                {
                    int i = 0;
                    while (forkAnswers[i] != null)
                    {
                        if (Q[i].Intersects(new Rectangle(SelectorPosX, SelectorPosY, 500, 40)))
                        {
                            scriptreaderx = 0;
                            scriptreadery = 0;
                            eventname = forkScript[i];
                            bQuestion = false;
                        }
                    }
                }

                if (1 == 2)
                {
                    GUI.bGamePad = true;
                    if(dpady < 0)
                    {
                        GUI.hoveredAnswer++;
                        if (GUI.hoveredAnswer > forkAnswers.Length) GUI.hoveredAnswer = 0;
                    }
                    if (dpady > 0)
                    {
                        GUI.hoveredAnswer--;
                        if (GUI.hoveredAnswer < 0) GUI.hoveredAnswer = forkAnswers.Length;
                    }

                }

            }
        }

        //Action Menu Animator
        protected void animateactionmenu()
        {

            #region Animation
            if (bMenu == true)
            {
                if (actionmenuscroller < -20)
                {
                    actionmenuscroller += 10;
                }
            }

            if (bMenu == false)
            {
                if (bStart == false)
                {
                    if (actionmenuscroller > -300)
                    {
                        actionmenuscroller -= 7;
                    }
                }
            }
            #endregion  //animates the menu

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            Rectangle button3 = new Rectangle(30 + actionmenuscroller, 220, 130, 30);
            Rectangle button9 = new Rectangle(160 + actionmenuscroller, 220, 130, 30);
            Rectangle button4 = new Rectangle(30 + actionmenuscroller, 260, 130, 30);
            Rectangle button10 = new Rectangle(160 + actionmenuscroller, 260, 130, 30);
            Rectangle button5 = new Rectangle(30 + actionmenuscroller, 300, 130, 30);
            Rectangle button11 = new Rectangle(160 + actionmenuscroller, 300, 130, 30);
            Rectangle button6 = new Rectangle(30 + actionmenuscroller, 340, 130, 30);
            Rectangle button12 = new Rectangle(160 + actionmenuscroller, 340, 130, 30);
            Rectangle button7 = new Rectangle(30 + actionmenuscroller, 380, 130, 30);
            Rectangle button13 = new Rectangle(160 + actionmenuscroller, 380, 130, 30);
            Rectangle button8 = new Rectangle(30 + actionmenuscroller, 420, 130, 30);
            Rectangle button14 = new Rectangle(160 + actionmenuscroller, 420, 130, 30);

            if (controller.bClicked == true)
            {
                if (actionmenuscroller == -20)
                {
                    if (controller.bGamePad == false)
                    {
                        //  FOR NORMAL MOUSE OPERATION
                        if (button3.Contains(mousePosition))
                        {
                            bShowtext = true;
                            bRunevent = true;
                            eventname = "sleep";
                        }
                        if (button4.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "tv";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button5.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "xbox";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button6.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "write";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button7.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "music";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button8.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "walk";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button9.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "text";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button10.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "eat";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button11.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "homework";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button12.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "class";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button13.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "porn";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button14.Contains(mousePosition))
                        {
                            bRunevent = true;
                            eventname = "savegame";
                            bMenu = false;
                            bShowtext = true;

                        }
                    }
                    else
                    {
                        //FOR GAME PAD OPERATION
                        if (button3.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bShowtext = true;
                            bRunevent = true;
                            eventname = "sleep";
                        }
                        if (button4.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "tv";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button5.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "xbox";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button6.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "write";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button7.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "music";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button8.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "walk";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button9.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "text";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button10.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "eat";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button11.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "homework";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button12.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "study";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button13.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "porn";
                            bMenu = false;
                            bShowtext = true;
                        }
                        if (button14.Intersects(new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 130, 30)))
                        {
                            bRunevent = true;
                            eventname = "savegame";
                            bMenu = false;
                            bShowtext = true;
                        }

                        //Make sure the selector doesn't go out of bounds.
                        if (dpady >= 7)
                        {
                            if (dpadx == 1)
                            {
                                dpady = 1;
                                dpadx = 2;
                            }
                            else
                            {
                                dpady = 6;
                            }
                        }
                        if (dpady == 0)
                        {
                            if (dpadx == 2)
                            {
                                dpady = 6;
                                dpadx = 1;
                            }
                            else
                            {
                                dpadx = 6;
                            }
                        }
                        if (dpadx >= 3) dpadx = 2;
                        if (dpadx == 0) dpadx = 1;

                    }
                }

            }

        }

        //button selector mover
        protected void ButtonSelector()
        {

            if (bMenu == true)
            {

                if (dpady >= 7)
                {
                    if (dpadx == 1)
                    {
                        dpady = 1;
                        dpadx = 2;
                    }
                    else
                    {
                        dpady = 6;
                    }
                }
                if (dpady == 0)
                {
                    if (dpadx == 2)
                    {
                        dpady = 6;
                        dpadx = 1;
                    }
                    else
                    {
                        dpadx = 6;
                    }
                }
                if (dpadx >= 3) dpadx = 2;
                if (dpadx == 0) dpadx = 1;

                if (dpadx == 1) SelectorPosX = 30;
                if (dpadx == 2) SelectorPosX = 160;
                if (dpady == 1) SelectorPosY = 220;
                if (dpady == 2) SelectorPosY = 260;
                if (dpady == 3) SelectorPosY = 300;
                if (dpady == 4) SelectorPosY = 340;
                if (dpady == 5) SelectorPosY = 380;
                if (dpady == 6) SelectorPosY = 420;

            }
            if (bQuestion == true)
            {
                if (dpady >= 3)
                {
                    dpady = 2;
                }
                if (dpady == 0)
                {
                    dpadx = 1;
                }

                if (dpady == 1) SelectorPosY = 220;
                if (dpady == 2) SelectorPosY = 280;
            }
        }

        //Text window  (AND SCRIPT READER)
        protected void textwindow(string ename)
        {

            //scripts are stored like this:
            //  Array(x, y)  X: Scripts  Y: Lines in Scripts

            bool bReloop = true;
            ename = eventname;

            //=== set the script reader to the correct script ===
            //It does this by checking if Y is 0, then it
            //is, it will cycle through the array until it finds the
            //matching eventname in the script array, then
            //set the Y value to that script number.
            if (scriptreadery == 0 || bReRunScriptInit == true)
            {
                scriptreaderx = 0;
                bReRunScriptInit = false;
                while (MasterScript.Read(scriptreaderx, 0) != ename)
                {
                    scriptreaderx++;

                    //hard-coded to only support up to 100 scripts.  This can be changed to any 32-bit number.
                    //Highly recomend setting this to the exact number of scripts that are available.
                    if (scriptreaderx >= 100)
                    {
                        //if exceeds X, then there must not be a script for eventname, throw error.
                        ErrorReason = "There is no script found for '" + ename + "' Please check spelling or \nif the file is missing.";
                        bError = true;
                        bShowtext = false;
                        ename = MasterScript.Read(scriptreaderx, 0);

                    }
                }

                //set the script reader to the correct line.
                scriptreadery++;

                //RUNS  THROUGH ALL SCRIPT COMMANDS AND EXECUTES THEM!
                //THIS ONE FUNCTION IS VARY IMPORTANT TO EVERYTHING!!!
                int returnReason = ScriptCommandsHelper();

                // If ScriptCommandsHelper returns with a 2, then we need to refresh this function.  so return.
                if (returnReason == 2)
                {
                    // tell the engine to redo the start of a script as if the player clicked.
                    bReRunScriptInit = true;

                    return;
                }

                // If scriptCommandHelper returns 3, and GS[490] is true, then force return to old script
                if (returnReason == 3)
                {

                }
                else
                {
                    TypewritterEffect();
                }

                //if (MasterScript.Read(scriptreaderx, scriptreadery) != null && bQuestion == false) dialougetr = forDialog;

            }

            if (reRunAfterWait && bWait == false)
            {
                ScriptCommandsHelper();
            }


            if (controller.bClicked == true && bQuestion == false && bTypewritting == false && bWait == false)
            {

                // To return the script reader if GS[490]"Script Return" = true. 
                if (Player.GameSwitches[490] == true && MasterScript.Read(scriptreaderx, scriptreadery + 1) == null)
                {
                    // Set script reader x and y to where we left off from the old script
                    scriptreaderx = Player.GameVariables[491];
                    scriptreadery = Player.GameVariables[492] + 1;

                    // cleanup
                    Player.GameVariables[491] = 0;
                    Player.GameVariables[492] = 0;
                    Player.GameSwitches[490] = false;

                    //if (MasterScript.Read(scriptreaderx, scriptreadery) == null) bClicked = true;
                }

                //fixfirstscripterror = false;
                if (MasterScript.Read(scriptreaderx, scriptreadery + 1) != null)
                {
                    if (reRunAfterWait)
                    {
                        reRunAfterWait = false;
                    }
                    else scriptreadery++;

                    //run through the script commands and like.. run them.
                    ScriptCommandsHelper();

                    // Advances script when we jump around and we land on the same line.
                    if (MasterScript.Read(scriptreaderx, scriptreadery) == null) controller.bClicked = true;

                    if (bWait == false) TypewritterEffect();
                }
                else
                {
                        scriptreaderx = 0;
                        scriptreadery = 0;

                        bShowtext = false;
                        bMenu = true;
                        if (bAuthorMode == true) this.Exit();
                }
            }
            //if(script[scriptreaderx,scriptreadery] != null) dialouge = script[scriptreaderx, scriptreadery];
        }

        //============================ STORY SWITCHES FROM SCRIPT ==========================
        private void StorySwitches(string trigger)
        {
            bool triggered = false;

            if (trigger == "shake_screen")
            {
                triggered = true;
                charaManager.shakeCharaOnScreen(20);
                bgManager.shakeBackground(20);
            }

            if (trigger == "shake_chara")
            {
                triggered = true;
                charaManager.shakeCharaOnScreen(20);
            }

            if (trigger == "sMetEmi_badend")
            {
                Player.GameSwitches[5] = true;
                Player.addSocial(-2);
                triggered = true;
            }

            if (trigger == "gamestart")
            {
                bRunTut = true;
                bShowtext = false;
                scriptreaderx = 1;
                scriptreadery = 0;
                triggered = true;
                
                stamps.Popup("Waste of Time.\nYou Decided to play the worst\nGame ever!  Contraturation.");
            }

            if (trigger == "emi_calls_one_bad")
            {
                //will trigger Emi to be more aggressive!
                triggered = true;
                stamps.Popup("Tread Softly\nEmi is probably tired of your shit.");
            }

            if (trigger == "emi_calls_one_good")
            {
                //will trigger Emi to walk with you at certain times
                Player.GameSwitches[1] = true;  //Emi_walksnow
                triggered = true;
                stamps.Popup("The Yes Man\nYou can't say no to that pouty face\nof hers.");
            }

            if (trigger == "emi_addheart")
            {
                Player.GameVariables[0]++;
                triggered = true;
            }

            if (trigger == "charlsee_battle")
            {
                Player.GameVariables[80] = 100;
                Player.GameVariables[81] = 100;
                triggered = true;
            }

            if (!triggered)
            {
                ErrorReason = "A trigger was called, but no trigger exists for '" + trigger + "'\n please check spelling and if the trigger even exists.";
                bError = true;
            }

        }

        //all the events!
        private void Events()
        {
            int R = 0;
            R = Rando.Next(5);
            int Time = Player.Time.FullTime;

            // Keep add time in this variable then add it to both clock and Player.Time later
            int addTimeMinutes = 0;

            //do we need to add homework?
            if (Time >= 1200)
            {
                if (Player.Time.DayOfWeek == 1)
                {
                    if (FakeDayofWeek == 0) Player.GameVariables[490]++;
                    FakeDayofWeek = 1;
                }
                if (Player.Time.DayOfWeek == 3)
                {
                    if (FakeDayofWeek == 1) Player.GameVariables[490]++;
                    FakeDayofWeek = 2;
                }
                if (Player.Time.DayOfWeek == 5)
                {
                    if (FakeDayofWeek == 2) Player.GameVariables[490]++;
                    FakeDayofWeek = 0;
                }
            }

            if (eventname == "sleep")
            {
                addTimeMinutes = 800;
                Player.addEnergy(30);
                Player.addFat(2);
            }
            if (eventname == "tv")
            {
                addTimeMinutes = 200;
                Player.addEnergy(-5);
                Player.addFat(4);
                Player.addHp(-1);
                Player.addSocial(-2);
            }
            if (eventname == "xbox")
            {
                addTimeMinutes = 400;
                Player.addEnergy(-6);
                Player.addFat(2);
                Player.addHp(-7);
                Player.addSocial(-1);
            }
            if (eventname == "write")
            {
                addTimeMinutes = 200;
                Player.addEnergy(-5);
                Player.addFat(1);
                Player.addHp(-2);
                Player.addSocial(-1);
            }
            if (eventname == "music")
            {
                addTimeMinutes = 300;
                Player.addEnergy(-4);
                Player.addHp(-4);
                Player.addSocial(-1);
                Player.addFat(2);
            }
            if (eventname == "walk")
            {
                addTimeMinutes = 200;
                Player.addEnergy(-10);
                Player.addHp(-8);
                Player.addFat(-5);
                Player.addSocial(2);

                // Run through GameEvents and see if any trigger.
                string oEvent = gameEvents.WalkingEvents(ref Player);
                if (oEvent != "0") eventname = oEvent;
            }

            if (eventname == "text")
            {
                //this is a special case

            }
            if (eventname == "eat")
            {
                addTimeMinutes = 200;
                Player.addEnergy(-10);
                Player.addHp(30);
                Player.addFat(2);
                Player.addSocial(1);

                // Run through GameEvents and see if any trigger
                string oEvent = gameEvents.Eat(ref Player);
                if (oEvent != "0") eventname = oEvent;
            }
            if (eventname == "homework")
            {
                int timetoadd;

                if (Player.GameVariables[490] >= 1)
                {

                    timetoadd = Player.GameVariables[490] * 100;
                    if (timetoadd >= 500)
                    {
                        timetoadd = 500;
                        Player.addEnergy(-5);
                        Player.addHp(-3);
                        Player.addFat(3);
                    }

                    addTimeMinutes = 200 + timetoadd;

                    Player.addEnergy(-5);
                    Player.addHp(-2);
                    Player.addFat(1);
                    Player.addSocial(-1);

                    Player.GameVariables[490] = 0;
                }
                else eventname = "nohomework";
            }
            if (eventname == "class")
            {
                if (Time >= 800 & Time <= 1700)
                {
                    addTimeMinutes = 200;
                    Player.addEnergy(-6);
                    Player.addHp(-3);
                    Player.addFat(1);
                    Player.addSocial(1);
                    string oEvent = gameEvents.School(ref Player);
                    if (oEvent != "0") eventname = oEvent;
                    else
                        eventname = "hadclass";
                    Player.GameVariables[450] = Player.Time.Day;
                    Player.GameVariables[452] = 1;
                    //gameEvents.School(VC, ref Player.GameVariables, ref Player.GameSwitches);
                }
                else
                {
                    eventname = "noclass";
                }
            }
            if (eventname == "porn")
            {
                addTimeMinutes = 100;
                Player.addEnergy(-10);
                Player.addHp(-8);
                Player.addFat(6);
                Player.addSocial(-2);
            }

            if (eventname == "savegame")
            {
                IAsyncResult result;
                result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
                StorageDevice device = StorageDevice.EndShowSelector(result);

                Player.GameVariables[453] = Time;

                gameSaver.SyncData(device, Player.GameSwitches, Player.GameVariables);
                gameSaver.sD.gSwitches = Player.GameSwitches;
                gameSaver.sD.gVariables = Player.GameVariables;
                gameSaver.DumpToFile();
            }

            // Did the player skip class?
            if (Time >= 1700)
            {
                int dayOfWeek = Player.Time.DayOfWeek;
                if (dayOfWeek == 1 || dayOfWeek == 3 || dayOfWeek == 5)
                {
                    if (Player.GameVariables[450] != Player.Time.Day)
                    {
                        // Add a skip day
                        Player.GameVariables[451]++;       // v[451] days skipped total
                        Player.GameVariables[452] = 2;     // v[452] 0 = not happend yet 1 = went to class 2 = skipped
                    }
                }
            }
            int dayofWeek = Player.Time.DayOfWeek;
            if (dayofWeek == 2 || dayofWeek == 4 || dayofWeek == 6)
            {
                Player.GameVariables[452] = 0;
            }

            // Add time to both clock and Player.Time
            clock.addTime(addTimeMinutes);
            Player.addTime(addTimeMinutes);

        }

        //All random events and events that occur without player's choice.
        int PopEvents()
        {
            //Emi calls first time.
            if (Player.GameVariables[10] < Player.Time.Day - 3 && Player.GameSwitches[3] == true && Player.GameSwitches[0] == false)
            {
                eventname = "emi_calls_one";
                bMenu = false;
                bShowtext = true;
                Player.addSocial(1);
                Player.GameSwitches[0] = true;
            }

            return 3;
        }

        //Script Commands Get Defined HERE! 
        private int ScriptCommands()
        {

            // Check if the code is a command:
            // We check by seeing if it is a speech command, and ignore if it is.
            // This should increase speed as we don't have to check every line 100+ times
            string sliceCom = "?";
            if (MasterScript.Read(scriptreaderx, scriptreadery) != null) sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 1);
            if (sliceCom != "S[" && sliceCom != "?")
            {

                    #region Charaevent Family (TSS v2.1)
                    // set sliceCom to the params of "charaevent"
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 10)
                    {
                        sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 10);

                        //intercept script action things here, so actions happen and are not displayed as dialogue.
                        if (sliceCom == "charaevent")
                        {
                            // Is it Show, Move, or Exit?
                            sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(11, 4);
                            //Console.WriteLine(sliceCom);

                            // For Show
                            #region Charaevent Show
                            if (sliceCom == "show")
                            {
                                // Needed variables:
                                string charName;
                                string charPose;

                                // Check if Syntax is correct. (check for '=')
                                if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 18)
                                {
                                    int qPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('=');

                                    // if '=' is present, then read chara pose from same line.
                                    if (qPos >= 19)
                                    {
                                        // Get charName (reletive based on where the = is.
                                        charName = MasterScript.Read(scriptreaderx, scriptreadery).Substring(16, qPos - 17);

                                        // Now to get pose
                                        charPose = MasterScript.Read(scriptreaderx, scriptreadery).Substring(qPos + 2);

                                        // check if there is a with: in the next line as parameters
                                        string specialArg = "none";
                                        if (MasterScript.Read(scriptreaderx, scriptreadery + 1).Contains("with:"))
                                        {
                                            // move to that line
                                            scriptreadery++;

                                            // store that line
                                            string arg = MasterScript.Read(scriptreaderx, scriptreadery);

                                            // slice off with:
                                            arg = arg.Remove(0, 6);

                                            // find special command
                                            if (arg == "move in right")
                                            {
                                                specialArg = "right";
                                            }
                                            if (arg == "move in left")
                                            {
                                                specialArg = "left";
                                            }
                                        }

                                        // show the char
                                        charaManager.Show(charName, charPose, "alpha", specialArg);

                                        // disable the HUD
                                        bHud = false;

                                    }
                                    else
                                    {
                                        ErrorReason = "Incorrect syntax for charaevent show. Line: " + scriptreadery + " in script " + scriptreaderx;
                                        bError = true;
                                    return -2;
                                    }
                                }

                                scriptreadery++;
                                return 1;

                            }
                            #endregion

                            // For Move
                            #region Charaevent Move
                            if (sliceCom == "move")
                            {
                                string charName;
                                string charDir;
                                int charAmount = 0;
                                int charSpeed = 2;

                                // check to see if '=' is present
                                if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 18)
                                {
                                    // get location of '=', ',' and '-'
                                    int qPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('=');
                                    int cPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf(',');
                                    int dPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('-');

                                    // if '=' is present, then read chara pose from same line.
                                    if (qPos >= 17)
                                    {
                                        // get charname
                                        charName = MasterScript.Read(scriptreaderx, scriptreadery).Substring(16, qPos - 17);

                                        // get charDirection
                                        charDir = MasterScript.Read(scriptreaderx, scriptreadery).Substring(qPos + 2, cPos - qPos - 2);

                                        // get charAmount
                                        charAmount = Convert.ToInt32(MasterScript.Read(scriptreaderx, scriptreadery).Substring(cPos + 2, dPos - cPos - 2));

                                        // get speed
                                        charSpeed = Convert.ToInt32(MasterScript.Read(scriptreaderx, scriptreadery).Substring(dPos + 1));

                                        // apply to charamove
                                        charaManager.Move(charName, charDir, charSpeed, charAmount);

                                        // Tell to console
                                        Console.WriteLine("TSS: moving chara - " + charName + " " + charAmount + " pixels to the " + charDir);

                                           scriptreadery++;
                                        return 1;
                                        
                                    }
                                    else
                                    {
                                        ErrorReason = "Syntax Error: Missing a '=' in line: " + MasterScript.Read(scriptreaderx, scriptreadery);
                                        bError = true;
                                    return -2;
                                    }
                                }

                            }
                            #endregion

                            // For Exit
                            #region Charaevent Exit
                            if (sliceCom == "exit")
                            {
                                // get which chara slot to use:
                                if (MasterScript.Read(scriptreaderx, scriptreadery).Length > 15)
                                {
                                    // Get name of chara to exit
                                    sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(16);
                                    Console.WriteLine(sliceCom);
                                    string chara;
                                    chara = sliceCom;

                                    try
                                    {
                                        //charaSlot = Convert.ToInt32(sliceCom);
                                    }
                                    catch
                                    {
                                        // give conversion error
                                        string scriptHang = MasterScript.Read(scriptreaderx, scriptreadery);
                                        ErrorReason = "Tried to convert string to integer.  String to convert: " + sliceCom + "\n Whole string: " + scriptHang;
                                        bError = true;
                                    return -2;
                                    }

                                    // Apply actions
                                    charaManager.Exit(chara);

                                    scriptreadery++;

                                    // Check and see if there are any actors on screen:
                                    if (charaManager.drawnCharas == 0) bHud = true;
                                return 1;

                                }
                                else
                                {
                                    // If no selected char, remove ALL.
                                    charaManager.Exit();

                                    bHud = true;
                                    scriptreadery++;
                                return 1;
                                }
                            }
                        #endregion

                        }
                    }
                    #endregion
                    
                    #region bgChange (TSS v2)
                    // set sliceCom to the params of "charaevent"
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 8)
                    {
                        sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 8);

                        if (sliceCom == "bgchange")
                        {
                            if (MasterScript.Read(scriptreaderx, scriptreadery).Length > 10)
                            {
                                if (MasterScript.Read(scriptreaderx, scriptreadery).Contains("="))
                                {
                                    // Get pos of = sign
                                    int modPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('=');

                                    // Get new background set
                                    sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(modPos + 2);
                                    Console.WriteLine("Background change to: " + sliceCom);
                                    
                                    // New background handler 
                                    bgManager.setBackground(sliceCom);
                                    bgManager.setBackgroundDimensions(800, 480);
                                    bgManager.bShowBackground = true;

                                    scriptreadery++;

                                    // check if there are any 'with' params on next line.
                                    if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 4) == "with")
                                    {
                                        // extra params.  (with transition:
                                        // Transition param
                                        if (MasterScript.Read(scriptreaderx, scriptreadery).Contains("transition:"))
                                        {
                                            // Add transition shit here.
                                            // Get : index
                                            int colPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf(":");

                                            // Get Transition type:
                                            sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(colPos + 1);
                                            if (sliceCom.Substring(0, 1) == " ") sliceCom = sliceCom.Substring(1);
                                            TransitionType = sliceCom;

                                            if (TransitionType == "FadeBlack") transition.Show("FadeBlack", 60);

                                            scriptreadery++;
                                        return 1;
                                        }
                                    }
                                }
                                else
                                {
                                    // Error handler for no =
                                    string scriptname = MasterScript.Read(scriptreaderx, 0);
                                    ErrorReason = "Syntax error: Missing '=' sign on line: " + MasterScript.Read(scriptreaderx, scriptreadery) + "\nOn script: " + scriptname;
                                    bError = true;
                                return -2;
                                }
                            }
                            else
                            {
                                // Legacy support
                                scriptreadery++;

                                bgManager.setBackground(MasterScript.Read(scriptreaderx, scriptreadery));
                                bgManager.setBackgroundDimensions(800, 480);

                                scriptreadery++;
                            return 1;
                            }
                        }
                    }
                    #endregion

                    #region GameOver
                    if (MasterScript.Read(scriptreaderx, scriptreadery) == "gameover")
                    {
                         this.Exit();
                    return 1;
                    }
                    #endregion

                    #region music (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 6)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 5) == "music")
                        {
                            // Get line from masterScript
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);

                            // for playing music:
                            Console.WriteLine("Index out of range here: " + line);
                            if (line.Substring(6, 4) == "play")
                            {
                                // check and get = pos
                                int modPos = 12;
                                if (line.Contains("=")) modPos = line.IndexOf("=");
                                else
                                {
                                    // Error handler
                                    Console.WriteLine("Syntax Error: Missing '=' on line: " + line + " \nScript: " + MasterScript.Read(scriptreaderx, 0));
                                    ErrorReason = "Syntax Error: Missing '=' on line: " + line + " \nScript: " + MasterScript.Read(scriptreaderx, 0);
                                    bError = true;
                                }

                                // Set song, and play!
                                string song = line.Substring(modPos + 2);
                                SetMusic = song;
                                songstart = false;
                                bPlayMusic = true;
                                scriptreadery++;
                            return 1;
                            }
                            else
                            // for stoping music:
                            if (line.Substring(6, 4) == "stop")
                            {
                                bPlayMusic = false;
                                scriptreadery++;
                            return 1;
                            }
                            else
                            {
                                // Error Handler
                                Console.WriteLine("Syntax Error: Music commands are: Stop, Play. Script: " + MasterScript.Read(scriptreaderx, 0));
                                ErrorReason = "Syntax Error: Music commands are: Stop, Play. Not: " + line.Substring(6, 4) + "\nScript: " + MasterScript.Read(scriptreaderx, 0);
                                bError = true;
                            return -2;
                            }
                        }
                    }
                    #endregion

                    #region SoundFX (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 7)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 7) == "soundfx")
                        {
                            // Variables:
                            int modPos = 0;

                            // Grab line
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);

                            // Handler for Stop:
                            if (line.Contains("stop") && line.Contains("=") == false)
                            {
                                scriptreadery++;
                                soundEffect = null;
                            }
                            else
                                // Grab ='s pos. (Also Handler for play:)
                                if (line.Contains("="))
                            {
                                // Get modpos
                                modPos = line.IndexOf("=");

                                // grab sound effect:
                                string sfx = line.Substring(modPos + 2);

                                // Check if there are any mods and store then rip:
                                int vol = 5;
                                if (sfx.Contains("-"))
                                {
                                    // Grab '-' pos
                                    int minPos = sfx.IndexOf("-");

                                    // Try and grab and convert mod to Volume integer
                                    try
                                    {
                                        vol = Convert.ToInt32(sfx.Substring(minPos + 1, 1));
                                    }
                                    catch
                                    {
                                        // Error Handler
                                        ErrorReason = "Tried to convert string to int32 on line: " + line;
                                        Console.WriteLine(ErrorReason);
                                        bError = true;
                                    return -2;
                                    }

                                    // Rip out mod
                                    sfx = sfx.Substring(0, minPos - 1);

                                }

                                soundEffect = Content.Load<SoundEffect>("assets/soundfx/" + sfx);
                                Console.WriteLine("playing sound: " + sfx);
                                scriptreadery++;
                                soundEffect.Play();
                            return 1;
                            }
                            else
                            {
                                // Error Handler
                                ErrorReason = "Syntax Error: Missing '=' on line: " + line + "\nScript:" + MasterScript.Read(scriptreaderx, 0);
                                Console.WriteLine(ErrorReason);
                                bError = true;
                            return -2;
                            }
                        }
                    }
                    #endregion

                    #region bgScroll (Parallaxing) (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 8)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 8) == "bgscroll")
                        {
                            // Variables
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);
                            int modPos = 10;

                            // Check for '=':
                            if (line.Contains("="))
                            {
                                // Grab = pos
                                modPos = line.IndexOf("=");

                                // Get parallax int:
                                bgScroller = Convert.ToInt32(line.Substring(modPos + 2));
                            }
                            else
                            {
                                // error handler
                                ErrorReason = "Syntax Error: Missing '=' on line: " + line + "\nScript:" + MasterScript.Read(scriptreaderx, 0);
                                Console.WriteLine(ErrorReason);
                                bError = true;
                            return -2;
                            }

                            // Move on from here:
                            scriptreadery++;
                        return 1;
                        }
                    }
                    #endregion

                    #region Fork Question (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery) == "Fork Question")
                    {
                        int i = 0;
                        WaitTime = 30;    //sets the time to 1/2 second (30 frames.)
                        bWait = true;     //Makes it so the player doesn't quickly click an answer
                        scriptreadery++;

                        // Get Dialog
                        dialouge = MasterScript.Read(scriptreaderx, scriptreadery);

                        scriptreadery++;

                        // Start check for Fork End or page break.
                        // then loop and add answers until one of those two are found.
                        int leftPos = 0, rightPos = 0;
                        string line = "";                   // saves space
                        do
                        {
                            line = MasterScript.Read(scriptreaderx, scriptreadery);

                            // Get Parethenis
                            leftPos = line.IndexOf("(");
                            rightPos = line.IndexOf(")");

                            // Grab Answer:
                            forkAnswers[i] = line.Substring(leftPos + 1, rightPos - 1);

                            // Grab Script:
                            forkScript[i] = line.Substring(rightPos + 2);

                            scriptreadery++;
                            i++;
                        } while (MasterScript.Read(scriptreaderx, scriptreadery) != "Fork End");

                    //scriptreadery++;

                        bQuestion = true;
                        charaManager.setDarkenChara(true);  // Darkens chara so they don't have bad contrast with answer boxes.

                    return 2;
                }
                    #endregion

                    #region Jumpto Script (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 7)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 7) == "$jumpto")
                        {
                            // Variables:
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);
                            string script = "asdf";
                            int colPos = 10;

                            // check for colon:
                            if (line.Contains(":"))
                            {
                                // get : pos
                                colPos = line.IndexOf(":");

                                // see if there is a return argument and store '-' index
                                int hyIndex = 0;
                                if (line.Contains("-return"))
                                {
                                    hyIndex = line.IndexOf('-');
                                    line = line.Remove(hyIndex, 7);

                                    // Set game variables and switches up
                                    Player.GameVariables[491] = scriptreaderx;         // These will tell the script int. to return to this script on end
                                    Player.GameVariables[492] = scriptreadery++;
                                    Player.GameSwitches[490] = true;

                                    // check and make sure there is no space at the end.
                                    if (line[hyIndex - 1] == ' ') line = line.Remove(hyIndex - 1, 1);
                                }
                                


                                // Get script:
                                script = line.Substring(colPos + 2);

                                eventname = script;

                                // Clean up:
                                dialouge = null;
                                scriptreaderx = 0;
                                scriptreadery = 0;
                            return 2;
                            }
                            else
                            {
                                // Error handler
                                ErrorReason = "Syntax Error: Missing ':' on line: " + line + "\nScript:" + MasterScript.Read(scriptreaderx, 0);
                                Console.WriteLine(ErrorReason);
                                bError = true;
                            return -2;
                            }
                        }
                    }
                    #endregion

                    #region Wait (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 4)
                    {
                        // See if it is the wait command
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 4) == "wait")
                        {
                            // Check for legacy:
                            if (MasterScript.Read(scriptreaderx, scriptreadery).Length > 4)
                            {
                                // Check for '='
                                if (MasterScript.Read(scriptreaderx, scriptreadery).Contains("="))
                                {
                                    // get time from line
                                    sliceCom = MasterScript.Read(scriptreaderx, scriptreadery);
                                    int modPos = sliceCom.IndexOf("=");
                                    sliceCom = sliceCom.Substring(modPos + 2);

                                    bWait = true;
                                    WaitTime = Convert.ToInt32(sliceCom);
                                    scriptreadery++;
                                    reRunAfterWait = true;
                                return 2;
                                }
                                else
                                {
                                    // Error Logic
                                    string line = MasterScript.Read(scriptreaderx, scriptreadery);
                                    string script = MasterScript.Read(scriptreaderx, 0);
                                    ErrorReason = "Syntax Error: Missing '=' in line: " + line + ". \nScript: " + script;
                                    bError = true;
                                return -2;
                                }
                            }
                            else
                            {
                                // Legacy Logic
                                bWait = true;
                                scriptreadery++;
                                WaitTime = Convert.ToInt32(MasterScript.Read(scriptreaderx, scriptreadery));
                                scriptreadery++;
                                reRunAfterWait = true;
                            return 2;
                            }
                        }
                    }
                    #endregion

                    #region $Variable Commands (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length > 12)
                    {
                        sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 9);
                        if (sliceCom == "$variable")
                        {
                            // Create variables to store data:
                            string sV = "";     // string version of variable
                            int V = 0;          // Int version of variable
                            int A = 0;          // Argument for variable

                            string fuckinghell = MasterScript.Read(scriptreaderx, scriptreadery);
                            string fuckinghell2 = MasterScript.Read(scriptreaderx, 0);

                            // Get which variable is being modded
                            sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(10, 3);

                            char[] sliceChar = sliceCom.ToCharArray();

                            for (int c = 0; c < 3; c++)
                            {
                                if (char.IsDigit(sliceChar[c])) sV += sliceChar[c];
                            }

                            // Convert (sV) to integer (V)
                            V = Convert.ToInt32(sV);

                            // Find the modifier (+, -, =, *)
                            sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(12, 4);

                            // For Adding:
                            #region Adding
                            if (sliceCom.Contains('+'))
                            {
                                // get +'s index
                                int modPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('+');
                                sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(modPos);

                                // Check if argument is RANDOM
                                if (sliceCom.Contains("-r"))
                                {
                                    // Rip out -r
                                    sliceCom = sliceCom.Substring(2, sliceCom.Length - 4);

                                    // Store Argument
                                    A = Convert.ToInt32(sliceCom);

                                    A = Rando.Next(0, A);

                                    // Set finished result to Player.GameVariables
                                    Player.GameVariables[V] += A;
                                }
                                else
                                {
                                    sliceCom = sliceCom.Substring(2);
                                    A = Convert.ToInt32(sliceCom);      // Convert to int
                                    Player.GameVariables[V] += A;               // Store finished result in Player.GameVariables[]
                                }

                                scriptreadery++;
                            return 1;
                            }
                            #endregion

                            // For Subtracting:
                            #region Subtracting
                            if (sliceCom.Contains('-'))
                            {
                                // get -'s index
                                int modPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('-');
                                sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(modPos);

                                // Check if argument is RANDOM
                                if (sliceCom.Contains("-r"))
                                {
                                    // Rip out -r
                                    sliceCom = sliceCom.Substring(2, sliceCom.Length - 4);

                                    // Store Argument
                                    A = Convert.ToInt32(sliceCom);

                                    A = Rando.Next(A);

                                    // Set finished result to Player.GameVariables
                                    Player.GameVariables[V] -= A;
                                }

                                // See if it is getting a value from another variable
                                string varMod;
                                if (sliceCom.Contains("v"))
                                {
                                    // Get var value and get gamevariable int
                                    int vPos = sliceCom.IndexOf("v");
                                    varMod = sliceCom.Substring(vPos + 1);
                                    A = Player.GameVariables[Convert.ToInt32(varMod)];

                                    // subtract gamevariable from gamevariable
                                    Player.GameVariables[V] -= A;

                                }
                                else
                                {
                                    sliceCom = sliceCom.Substring(2);
                                    A = Convert.ToInt32(sliceCom);      // Convert to int
                                    Player.GameVariables[V] -= A;               // Store finished result in Player.GameVariables[]
                                }

                                scriptreadery++;
                            return 1;
                            }
                            #endregion

                            // For Setting:
                            #region Setting
                            if (sliceCom.Contains('='))
                            {
                                // get +'s index
                                int modPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('=');
                                sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(modPos);

                                // Check if argument is RANDOM
                                if (sliceCom.Contains("-r"))
                                {
                                    // Rip out -r
                                    sliceCom = sliceCom.Substring(2, sliceCom.Length - 4);

                                    // Store Argument
                                    A = Convert.ToInt32(sliceCom);

                                    A = Rando.Next(A);

                                    // Set finished result to Player.GameVariables
                                    Player.GameVariables[V] = A;
                                }
                                else
                                {
                                    sliceCom = sliceCom.Substring(2);
                                    A = Convert.ToInt32(sliceCom);      // Convert to int
                                    Player.GameVariables[V] = A;               // Store finished result in Player.GameVariables[]
                                }

                                scriptreadery++;
                            return 1;
                            }
                            #endregion

                            // For Multiplying
                            #region Multiplying
                            if (sliceCom.Contains('*'))
                            {
                                // get +'s index
                                int modPos = MasterScript.Read(scriptreaderx, scriptreadery).IndexOf('*');
                                sliceCom = MasterScript.Read(scriptreaderx, scriptreadery).Substring(modPos);

                                // Check if argument is RANDOM
                                if (sliceCom.Contains("-r"))
                                {
                                    // Rip out -r
                                    sliceCom = sliceCom.Substring(2, sliceCom.Length - 4);

                                    // Store Argument
                                    A = Convert.ToInt32(sliceCom);

                                    A = Rando.Next(0, A);
                                    
                                    // Set finished result to Player.GameVariables
                                    Player.GameVariables[V] *= A;
                                }
                                else
                                {
                                    sliceCom = sliceCom.Substring(2);
                                    A = Convert.ToInt32(sliceCom);      // Convert to int
                                    Player.GameVariables[V] *= A;               // Store finished result in Player.GameVariables[]
                                }

                                scriptreadery++;
                            return 1;
                            }
                            #endregion

                            // Say what we did to the console:
                            Console.WriteLine("TSS: Variable: " + V + ". Is being set to: " + A);
                        }
                        if (MasterScript.Read(scriptreaderx, scriptreadery) == "$variable_add")
                        {
                            // Used to directly alter game variables
                            int V;
                            string P;
                            int intP;
                            scriptreadery++;
                            V = Convert.ToInt32(MasterScript.Read(scriptreaderx, scriptreadery));
                            scriptreadery++;
                            P = MasterScript.Read(scriptreaderx, scriptreadery);
                            scriptreadery++;

                            if (P == "random")
                            {
                                intP = Convert.ToInt32(MasterScript.Read(scriptreaderx, scriptreadery));
                                intP = Rando.Next(intP);
                                scriptreadery++;
                            }
                            else intP = Convert.ToInt32(P);

                            Player.GameVariables[V] += intP;

                        }
                    }
                    #endregion

                    #region $switch Commands (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 7)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 7) == "$switch")
                        {
                            // Variables
                            int s = 0;
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);
                            sliceCom = line.Substring(8, 1);

                            // Grab and convert to int32 which switch to switch
                            try
                            {
                                s = Convert.ToInt32(sliceCom);
                            }
                            catch
                            {
                                Console.WriteLine("Failed to convert string to int32");
                                ErrorReason = "Tried to convert string to Int32: " + line + "\nScript: " + MasterScript.Read(scriptreaderx, 0);
                                bError = true;
                            return -2;
                            }

                            // get = pos
                            int opPos = 0;
                            if (line.Contains("=")) opPos = line.IndexOf("=");
                            else
                            {
                                Console.WriteLine("Syntax Error: missing '=' on line: " + line);
                                ErrorReason = "Syntax Error: missing '=' on line: " + line + "\nScript: " + MasterScript.Read(scriptreaderx, 0);
                                bError = true;
                            return -2;
                            }

                            // Get True or False
                            string tf;
                            tf = line.Substring(opPos + 2);

                            // Mod game Switches with collected data.
                            if (tf == "false") Player.GameSwitches[s] = false;
                            if (tf == "true") Player.GameSwitches[s] = true;
                            else
                            {
                                Console.WriteLine("Tried to change Switch[" + s + "] to a none boolean type: '" + tf + "' on Line: " + line);
                                ErrorReason = "Tried to change Switch[" + s + "] to a none boolean type: " + tf + "on Line: " + line + "\nScript: " + MasterScript.Read(scriptreaderx, 0);
                                bError = true;
                            return -2;
                            }

                            scriptreadery++;
                        return 1;
                        }
                    }
                    #endregion

                    #region $Trigger Commands (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length >= 8)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Substring(0, 8) == "$trigger")
                        {
                            // Variables:
                            string line = MasterScript.Read(scriptreaderx, scriptreadery);
                            int colPos = 10;
                            string trigger;

                            // Find : pos
                            if (line.Contains(":"))
                            {
                                // Get : pos
                                colPos = line.IndexOf(":");

                                // Get Trigger:
                                trigger = line.Substring(colPos + 2);

                                // Call trigger:
                                StorySwitches(trigger);

                            }
                            else
                            {
                                // Error Handler:
                                ErrorReason = "Syntax Error: Missing ':' on line: " + line + "\nScript:" + MasterScript.Read(scriptreaderx, 0);
                                Console.WriteLine(ErrorReason);
                                bError = true;
                            return -2;
                            }

                            // Get outa here!
                            scriptreadery++;
                        return 1;
                        }
                    }
                    #endregion

                    #region #if branch (TSS v2)
                    if (MasterScript.Read(scriptreaderx, scriptreadery).Length > 3)
                    {
                        if (MasterScript.Read(scriptreaderx, scriptreadery).Contains("#if"))
                        {
                            // Declare all needed variables:
                            int ifType = 0;     // gets what type of If statement this is
                            int ifPos = 0;      // Holds the position in where the >,<,= sign is
                            int aType = 0;      // gets what type Argument A is
                            int bType = 0;      // gets what type Argument B is
                            string arg1;        // Stores Argument A
                            string arg2;        // Stores Arguemnt B
                            int a;              // Converted Argument A to int
                            int b;              // Converted Argument B to int

                            // Grab the whole line to save time and space:
                                 string line = MasterScript.Read(scriptreaderx, scriptreadery);
                            scriptreadery++;

                            // Get the Opp type & pos
                            if (line.Contains("="))
                            {
                                ifPos = line.IndexOf("=");
                                ifType = 0;
                            }
                            else
                            if (line.Contains(">"))
                            {
                                ifPos = line.IndexOf(">");
                                ifType = 1;
                            }
                            else
                            if (line.Contains("<"))
                            {
                                ifPos = line.IndexOf("<");
                                ifType = 2;
                            }

                            // Get left and right () pos's
                            int leftPar = line.IndexOf("(");
                            int rightPar = line.IndexOf(")");

                            // Get Argument types:
                            string strAType = line.Substring(leftPar + 1, 1);
                            string strBType = line.Substring(ifPos + 2, 1);

                            // Get Argument's data:  (Crazy math)
                            arg1 = line.Substring(leftPar + 2, ifPos - leftPar - 3);
                            arg2 = line.Substring(ifPos + 3, rightPar - ifPos - 3);

                            // Get Argument type { 0 = Number, 1 = Variable, 2 = Switch }
                            if (strAType == "n") aType = 0;
                            if (strAType == "v") aType = 1;
                            if (strAType == "s") aType = 2;
                            if (strBType == "n") bType = 0;
                            if (strBType == "v") bType = 1;
                            if (strBType == "s") bType = 2;

                            // Gut then Convert to Int32
                            a = Convert.ToInt32(arg1);
                            b = Convert.ToInt32(arg2);

                            // Get Game Data
                            if (aType == 1) a = Player.GameVariables[a];
                            if (bType == 1) b = Player.GameVariables[b];
                            if (aType == 2)
                            {
                                if (Player.GameSwitches[a]) a = 1;
                                else a = 0;
                            }
                            if (bType == 2)
                            {
                                if (Player.GameSwitches[b]) b = 1;
                                else b = 0;
                            }

                            // Finally!  Actually do the comparison
                            if (ifType == 0)
                            {
                                if (a == b)
                                {
                                    return 1;
                                }
                                else
                                {
                                    // need to make it keep track how many Endif's there are, so it can have multiple if blocks.
                                    while (MasterScript.Read(scriptreaderx, scriptreadery) != "#endif")
                                    {
                                        scriptreadery++;
                                    }
                                return 1;
                                }
                            }
                            if (ifType == 1)
                            {
                                if (a >= b)
                                {
                                    return 1;
                                }
                                else
                                {
                                    while (MasterScript.Read(scriptreaderx, scriptreadery) != "#endif")
                                    {
                                        scriptreadery++;
                                    }
                                return 1;
                                }
                            }

                            // For LESS THAN
                            if (ifType == 2)
                            {
                                if (a <= b)
                                {
                                    return 1;
                                }
                                else
                                {
                                    while(MasterScript.Read(scriptreaderx, scriptreadery) != "#endif")
                                    {
                                        scriptreadery++;
                                    }
                                return 1;
                                }
                            }


                        }

                        if (MasterScript.Read(scriptreaderx, scriptreadery) == "#endif")
                        {
                            scriptreadery++;
                        return 1;
                        }
                    }
                #endregion

                // Check if the line is null and return with 3
                if (sliceCom == null) return 3;

                return 0;

                //Console.WriteLine("Reading from Script: " + scriptreaderx + " On line: " + scriptreadery);
            }
            return 0;
        }

        // Handles how many times we need to run through commands.
        private int ScriptCommandsHelper()
        {
            bool bLoop = true;
            int returnReason = 0;
            int commandsRan = 0;

            while (bLoop)
            {
                returnReason = ScriptCommands();

                if (returnReason == 0) bLoop = false;
                if (returnReason == 1) commandsRan++;
                if (returnReason == 2) bLoop = false;
                if (returnReason == 3) bLoop = false;
            }

            if (returnReason == 2) Console.WriteLine("Returning 2, reseting script reader and textwindow!");

            return returnReason;
        }

        // The Effect that makes the thought and speech type out char by char
        private void TypewritterEffect()
        {
            //line 583(emi) PF -> From Christian 'Turtle' Norris, himself. (^33) 
            string gaystring = MasterScript.Read(scriptreaderx, scriptreadery);
            if (gaystring != null)
            {
                int strlength = gaystring.Length;

                charaCode = null;

                // Escape Sequence for if there is a $[x] in dialog
                // This converts that to that variable's value
                #region Get Variable Value Escape seq
                if (gaystring.Contains("$["))
                {
                    char[] gaychar = gaystring.ToCharArray();
                    string varGetValue = "";
                    int varToDisplay = 0;
                    for (int i = 0; i < strlength; i++)
                    {
                        if (gaychar[i] == '$' && gaychar[i + 1] == '[')
                        {
                            string dialogWithValue = "";  // this will store the split chars and value when done

                            // Determin how many digits are in there and grab them accordingly
                            if (gaychar[i + 2] != ']') varGetValue = Convert.ToString(gaychar[i + 2]);
                            if (gaychar[i + 3] != ']') varGetValue += Convert.ToString(gaychar[i + 3]);
                            if (gaychar[i + 4] != ']') varGetValue += Convert.ToString(gaychar[i + 4]);
                            varToDisplay = Convert.ToInt32(varGetValue);

                            // Grab characters up to the '$' and put them in a seperate string
                            for (int j = 0; j < i; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j]);
                            }

                            // Add the variable's value to the new string
                            dialogWithValue += Convert.ToString(Player.GameVariables[varToDisplay]);

                            // Determin how many char's we need to skip to get the second half
                            int toSkip = 4;
                            if (varToDisplay > 9) toSkip = 5;
                            if (varToDisplay > 99) toSkip = 6;

                            // Add the remaining bit of the original string to the new one
                            for (int j = i; j < strlength - toSkip; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j + toSkip]);
                            }

                            // Give the new string to the old string and get it's new length
                            gaystring = dialogWithValue;
                            gaychar = gaystring.ToCharArray();
                            strlength = gaystring.Length;

                        }
                    }
                }

                #endregion

                // Escape Sequence for if there is a @pro in dialog
                // This converts that to the player's name
                #region Get Player Name Escape seq
                if (gaystring.Contains("@pro"))
                {
                    char[] gaychar = gaystring.ToCharArray();
                    for (int i = 0; i < strlength; i++)
                    {
                        if (gaychar[i] == '@' && gaychar[i + 1] == 'p')
                        {
                            string dialogWithValue = "";  // this will store the split chars and value when done

                            // Grab characters up to the '@' and put them in a seperate string
                            for (int j = 0; j < i; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j]);
                            }

                            // Add the variable's value to the new string
                            dialogWithValue += Player.Name;

                            // Determin how many char's we need to skip to get the second half
                            int toSkip = 4;

                            // Add the remaining bit of the original string to the new one
                            for (int j = i; j < strlength - toSkip; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j + toSkip]);
                            }

                            // Give the new string to the old string and get it's new length
                            gaystring = dialogWithValue;
                            gaychar = gaystring.ToCharArray();
                            strlength = gaystring.Length;

                        }
                    }
                }

                #endregion

                // Escape for setting speaker name and color
                #region Get Speaker's name and color escape S[xxx]
                if (gaystring.Contains("S["))
                {
                    char[] gaychar = gaystring.ToCharArray();
                    for (int i = 0; i < strlength; i++)
                    {
                        if (gaychar[i] == 'S' && gaychar[i + 1] == '[')
                        {
                            string speakerName;           // This stores the chara code for who is talking
                            string dialogWithValue = "";  // this will store the split chars and value when done

                            // get the chara code
                            speakerName = gaychar[i + 2].ToString();
                            speakerName += gaychar[i + 3].ToString();
                            speakerName += gaychar[i + 4].ToString();

                            // Grab characters up to the '@' and put them in a seperate string
                            for (int j = 0; j < i; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j]);
                            }

                            // set the speaker's color and name to their respected places
                            #region Chara Codes and Colors
                            if (speakerName == "emi")
                            {
                                charaColor = Color.OrangeRed;
                                charaCode = "Emi:";
                            }
                            if (speakerName == "pro")
                            {
                                charaColor = Color.Green;
                                charaCode = Player.Name + ":";
                            }
                            if (speakerName == "kay" || speakerName == "shz")
                            {
                                charaColor = Color.Blue;
                                charaCode = "Kayla:";
                            }
                            if (speakerName == "rin")
                            {
                                charaColor = Color.Red;
                                charaCode = "Rin:";
                            }
                            if (speakerName == "mis")
                            {
                                charaColor = Color.Pink;
                                charaCode = "Misha:";
                            }
                            if (speakerName == "ken")
                            {
                                charaColor = Color.Orange;
                                charaCode = "Kenji:";
                            }
                            if (speakerName == "lil")
                            {
                                charaColor = Color.Yellow;
                                charaCode = "Lilly:";
                            }
                            if (speakerName == "han" || speakerName == "ari")
                            {
                                charaColor = Color.Purple;
                                charaCode = "Aria:";
                            }
                            if (speakerName == "ell")
                            {
                                charaColor = Color.RosyBrown;
                                charaCode = "Ellie:";
                            }
                            if (speakerName == "stv")
                            {
                                charaColor = Color.PowderBlue;
                                charaCode = "Steve:";
                            }
                            if (speakerName == "cha")
                            {
                                charaColor = Color.Red;
                                charaCode = "Charlsee:";
                            }
                            if (speakerName == "est")
                            {
                                charaColor = Color.RosyBrown;
                                charaCode = "Estella:";
                            }
                            if (speakerName == "chr")
                            {
                                charaColor = Color.OrangeRed;
                                charaCode = "Chris:";
                            }
                            if (speakerName == "Suz")
                            {
                                charaColor = Color.DeepPink;
                                charaCode = "Suzy:";
                            }
                            if (speakerName == "???")
                            {
                                charaColor = Color.DimGray;
                                charaCode = "?????:";
                            }
                            #endregion

                            // Determin how many char's we need to skip to get the second half
                            int toSkip = 6;

                            // Add the remaining bit of the original string to the new one
                            for (int j = i; j < strlength - toSkip; j++)
                            {
                                dialogWithValue += Convert.ToString(gaychar[j + toSkip]);
                            }

                            // Give the new string to the old string and get it's new length
                            gaystring = dialogWithValue;
                            gaychar = gaystring.ToCharArray();
                            strlength = gaystring.Length;

                        }
                    }
                }

                #endregion

                // Escape for clear window (hides text box window)
                #region Escape to hide text window
                if (gaystring.Contains("$clear"))
                {

                    // find the right $
                    int loc = gaystring.IndexOf("$clear");

                    // just delete everything after this escape.
                    gaystring = gaystring.Remove(loc);
                    strlength = gaystring.Length;

                    // disable text window
                    bShowtextWindow = false;

                }
                else
                {
                    bShowtextWindow = true;
                }
                #endregion

                // This tells the engine to draw the text window either in top, center, or bottom.
                #region Escape for Text window Position
                if (gaystring.Contains("$center"))
                {
                    int loc = gaystring.IndexOf("$center");

                    gaystring = gaystring.Remove(loc);
                    strlength = gaystring.Length;

                    textWindowYLocation = 160;
                }
                else
                    if (gaystring.Contains("$top"))
                {
                    int loc = gaystring.IndexOf("$top");

                    gaystring = gaystring.Remove(loc);
                    strlength = gaystring.Length;

                    textWindowYLocation = 10;
                }
                else
                    textWindowYLocation = 355;
                #endregion

                // This will draw the transparent text window instead of the normal one.
                if (gaystring.Contains("$trans"))
                {
                    int loc = gaystring.IndexOf("$trans");

                    gaystring = gaystring.Remove(loc);
                    strlength = gaystring.Length;

                    // tell not to draw normal window and draw the transparent one.
                    bShowtextWindow = false;
                    bShowTransWindow = true;
                }
                else
                {
                    bShowTransWindow = false;
                }

                int modLength = 0;
                char[] strchar = new char[strlength + modLength];

                StringReader strReader = new StringReader(gaystring);
                int cPointer = 0;
                for (int i = 0; i < strlength; i++)
                {
                    strReader.Read(strchar, cPointer, strlength);
                }

                if (strlength >= 76)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        if (strchar[65 + x] == ' ')
                        {
                            strchar[65 + x] = '\n';
                            x = 1000;
                        }
                    }
                    if (strlength >= 152)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            if (strchar[140 + x] == ' ')
                            {
                                strchar[140 + x] = '\n';
                                x = 1000;
                            }
                        }
                    }
                }

                string forDialog = new string(strchar);

                bTypewritting = true;
                dialouge = null;

                if (MasterScript.Read(scriptreaderx, scriptreadery) != null && bQuestion == false) dialougetr = forDialog;
            }
        }
    }
}
