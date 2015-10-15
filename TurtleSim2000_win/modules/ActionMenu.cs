// ===================================================================================================================================
// === ActionMenu (window) - Handles Menu logic and drawing                                                                        ===
// ===================================================================================================================================
// = Built for TurtleSim Engine by Jacob Karleskint (2015) GNUv3 (License should be in root of source.                               =
// = TODO:                                                                                                                           =
// = Add playerdata.schedule and .time to know when to disable certain buttons                                                       =
// = Add homework notifications with playerdata.schedule                                                                             =
// ===================================================================================================================================

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TurtleSim2000_Linux
{
    class ActionMenu
    {
        /// <summary>
        /// To show and activate this window or not.
        /// </summary>
        public bool Show = false;

        /// <summary>
        /// Tells if this module is fully ready for input.
        /// </summary>
        public bool Active = false;

        /// <summary>
        /// X coord value of the action menu for scrolling
        /// </summary>
        public int scroller = 0;                    // Holds scroll (X position) on screen

        int MAXSCROLL = 280;                        // How far to scroll this window.
        int SCROLLSPEED = 10;                       // how fast to scroll this window
        float masterFade = 0f;                      // Holds how much to alpha fade everything.

        int gamepadSelected = 0;                    // Which button the selector is on.
        Controls.Dpad dPad = new Controls.Dpad();   // Dpad struct for comparing and holding what dpad button is pushed.

        int Homework = 0;                           // How much homework the player has.  Just for little pop up bubble

        public Button btnSleep;
        public Button btnText;
        public Button btnTV;
        public Button btnGame;
        public Button btnWrite;
        public Button btnSave;
        public Button btnClass;
        public Button btnHomework;
        public Button btnPorn;
        public Button btnEat;
        public Button btnMusic;
        public Button btnWalk;

        Texture2D background;
        Texture2D homeworkBubble;

        SpriteFont smallFont;
        SpriteFont bigFont;



        public ActionMenu(ContentManager content)
        {
            btnSleep = new Button(content, "Sleep", new Rectangle(-280, 220, 130, 30));
            btnText = new Button(content, "Text", new Rectangle(-140, 220, 130, 30));
            btnTV = new Button(content, "TV", new Rectangle(-280, 260, 130, 30));
            btnEat = new Button(content, "Eat", new Rectangle(-140, 260, 130, 30));
            btnGame = new Button(content, "Play Game", new Rectangle(-280, 300, 130, 30));
            btnClass = new Button(content, "Class", new Rectangle(-140, 300, 130, 30));
            btnWrite = new Button(content, "Write", new Rectangle(-280, 340, 130, 30));
            btnHomework = new Button(content, "Homework", new Rectangle(-140, 340, 130, 30));
            btnMusic = new Button(content, "Music", new Rectangle(-280, 380, 130, 30));
            btnPorn = new Button(content, "Porn", new Rectangle(-140, 380, 130, 30));
            btnWalk = new Button(content, "Walk", new Rectangle(-280, 420, 130, 30));
            btnSave = new Button(content, "Save", new Rectangle(-140, 420, 130, 30));

            // Load in background
            background = content.Load<Texture2D>("assets/gui/notebook/notebookAsset");
            homeworkBubble = content.Load<Texture2D>("assets/gui/gui_homeworkelert");

            smallFont = content.Load<SpriteFont>("fonts/debugfontsmall");
            bigFont = content.Load<SpriteFont>("fonts/debugfont");

        }


        public void Update(int homework)
        {
            // store homework ammount
            Homework = homework;

            // This will fade and scroll the menu IN
            if (Show == true && scroller < MAXSCROLL)
            {
                scroller += SCROLLSPEED;
                masterFade += MAXSCROLL * 0.000115f;

                if (scroller >= MAXSCROLL) Active = true;

                btnSleep.addXposition(SCROLLSPEED);
                btnEat.addXposition(SCROLLSPEED);
                btnGame.addXposition(SCROLLSPEED);
                btnHomework.addXposition(SCROLLSPEED);
                btnMusic.addXposition(SCROLLSPEED);
                btnPorn.addXposition(SCROLLSPEED);
                btnSave.addXposition(SCROLLSPEED);
                btnText.addXposition(SCROLLSPEED);
                btnTV.addXposition(SCROLLSPEED);
                btnWalk.addXposition(SCROLLSPEED);
                btnWrite.addXposition(SCROLLSPEED);
                btnClass.addXposition(SCROLLSPEED);
            }

            // This will fade and scroll the menu OUT
            if (Show == false && scroller > -300)
            {
                scroller -= SCROLLSPEED;
                masterFade -= MAXSCROLL * 0.000115f;
                Active = false;

                btnSleep.addXposition(-SCROLLSPEED);
                btnEat.addXposition(-SCROLLSPEED);
                btnGame.addXposition(-SCROLLSPEED);
                btnHomework.addXposition(-SCROLLSPEED);
                btnMusic.addXposition(-SCROLLSPEED);
                btnPorn.addXposition(-SCROLLSPEED);
                btnSave.addXposition(-SCROLLSPEED);
                btnText.addXposition(-SCROLLSPEED);
                btnTV.addXposition(-SCROLLSPEED);
                btnWalk.addXposition(-SCROLLSPEED);
                btnWrite.addXposition(-SCROLLSPEED);
                btnClass.addXposition(-SCROLLSPEED);
            }


        }


        public void Draw(SpriteBatch sB)
        {
            // Draw the background
            sB.Draw(background, new Rectangle(-300 + scroller, 120, 340, 400), Color.White * masterFade);

            // Draw the buttons
            btnSleep.Draw(sB, masterFade);
            btnText.Draw(sB, masterFade);
            btnTV.Draw(sB, masterFade);
            btnEat.Draw(sB, masterFade);
            btnGame.Draw(sB, masterFade);
            btnClass.Draw(sB, masterFade);
            btnWrite.Draw(sB, masterFade);
            btnHomework.Draw(sB, masterFade);
            btnMusic.Draw(sB, masterFade);
            btnPorn.Draw(sB, masterFade);
            btnWalk.Draw(sB, masterFade);
            btnSave.Draw(sB, masterFade);

            // Draw homework alert
            if (Homework > 0)
            {
                sB.Draw(homeworkBubble, new Rectangle(-25 + scroller, 330, 32, 32), Color.White * masterFade);
                sB.DrawString(smallFont, "" + Homework, new Vector2(-10 + scroller, 340), Color.White * masterFade);
            }
        }


        // Add Controller support here!
        /// <summary>
        /// Updates control logic for buttons.  This will return the eventname of the button pressed.  TODO: Add controller support here!
        /// </summary>
        public string updateHitTest(Point mousepos, bool bClicked)
        {
            string eName = "";

            // Update Button's hittest logic.
            btnSleep.UpdateControls(mousepos, bClicked);
            btnText.UpdateControls(mousepos, bClicked);
            btnTV.UpdateControls(mousepos, bClicked);
            btnEat.UpdateControls(mousepos, bClicked);
            btnGame.UpdateControls(mousepos, bClicked);
            btnClass.UpdateControls(mousepos, bClicked);
            btnWrite.UpdateControls(mousepos, bClicked);
            btnHomework.UpdateControls(mousepos, bClicked);
            btnMusic.UpdateControls(mousepos, bClicked);
            btnPorn.UpdateControls(mousepos, bClicked);
            btnWalk.UpdateControls(mousepos, bClicked);
            btnSave.UpdateControls(mousepos, bClicked);

            // get what button is pushed, and return with eventname
            if (btnSleep.bPressed)
            {
                eName = "sleep";
            }
            if (btnText.bPressed) eName = "text";
            if (btnTV.bPressed) eName = "tv";
            if (btnEat.bPressed) eName = "eat";
            if (btnGame.bPressed) eName = "game";
            if (btnClass.bPressed) eName = "class";
            if (btnWrite.bPressed) eName = "write";
            if (btnHomework.bPressed) eName = "homework";
            if (btnMusic.bPressed) eName = "music";
            if (btnPorn.bPressed) eName = "porn";
            if (btnWalk.bPressed) eName = "walk";
            if (btnSave.bPressed) eName = "savegame";

            return eName;
        }






        private void updateGamePadSelected()
        {

        }

    }
}
