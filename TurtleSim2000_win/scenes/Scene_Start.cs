using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Content;

namespace TurtleSim2000_Linux
{
    class Scene_Start
    {
        ContentManager contentManager;

        // Width and height of this scene.
        public int SceneHeight = 480;
        public int SceneWidth = 800;

        // Get game info from main class
        public string GameInfo;
        string newthings = "Eh, check github for changes.";

        // Textures
        Texture2D logo;
        Texture2D messagebox;
        Texture2D bg_gate;
        Texture2D bg_forest;
        Texture2D bg_courtyard;
        Texture2D trans_BlackBars;      // Black bars that draw onto the sides of the backgrounds that scroll *this is polish*

        // Fonts
        SpriteFont nFont;
        SpriteFont debugFont_tiny;

        // Buttons
        public Button btnStart;
        public Button btnContinue;
        public Button btnQuit;

        // Updates for scrolling and scaling
        int logoscaler = 0;
        bool reversescaler = false;
        int bgscroller = 0;
        int bgscrollslowerdowner = 0;

        // Hit detection
        Point MousePos = new Point(1, 1);




        /// <summary>
        /// Creates the Start screen scene. This holds all of its controls as well.
        /// </summary>
        public Scene_Start(ContentManager contentmanager, int screenwidth, int screenheight)
        {
            contentManager = contentmanager;

            // Load textures for this Scene
            logo = contentManager.Load<Texture2D>("assets/gui/logo_2");
            messagebox = contentManager.Load<Texture2D>("assets/gui/messagebox");
            // Intro backgrounds
            bg_courtyard = contentManager.Load<Texture2D>("assets/backgrounds/school_courtyard");
            bg_gate = contentManager.Load<Texture2D>("assets/backgrounds/school_gate");
            bg_forest = contentManager.Load<Texture2D>("assets/backgrounds/school_forest1");
            trans_BlackBars = contentmanager.Load<Texture2D>("assets/gui/trans_blackbars");
            // Fonts
            nFont = contentManager.Load<SpriteFont>("fonts/debugfont");
            debugFont_tiny = contentManager.Load<SpriteFont>("fonts/debugfontsmall");

            // Create scene controls:
            btnStart = new Button(contentManager, "Start Game", new Rectangle(320, 200, 160, 40));
            btnContinue = new Button(contentManager, "Continue", new Rectangle(320, 240, 160, 40));
            btnQuit = new Button(contentManager, "Quit", new Rectangle(320, 280, 160, 40));

        }



        public void Update()
        {
            // Scrolls backgrounds, and scales logo
            bgscrollslowerdowner++;
            if (bgscrollslowerdowner == 3)
            {
                bgscroller += 1;
                if (bgscroller >= 2400) bgscroller = 0;

                if (reversescaler == false)
                {
                    logoscaler++;
                }
                else
                {
                    logoscaler -= 1;
                }
                if (logoscaler == 10) reversescaler = true;
                if (logoscaler == 0) reversescaler = false;

                bgscrollslowerdowner = 0;
                
            }


        }



        public void Draw(SpriteBatch sB)
        {
            // Start SpriteBatch
            sB.Begin();

            // Draw background with scroll
            sB.Draw(bg_gate, new Rectangle(0 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);
            sB.Draw(trans_BlackBars, new Rectangle(0 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);                // Black trans bars
            sB.Draw(bg_forest, new Rectangle(SceneWidth - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);
            sB.Draw(trans_BlackBars, new Rectangle(SceneWidth - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);       // Black trans Bars
            sB.Draw(bg_courtyard, new Rectangle(SceneWidth * 2 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);
            sB.Draw(trans_BlackBars, new Rectangle(SceneWidth * 2 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);   // Black trans bars
            sB.Draw(bg_gate, new Rectangle(SceneWidth * 3 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);
            sB.Draw(trans_BlackBars, new Rectangle(SceneWidth * 3 - bgscroller, 0, SceneWidth, SceneHeight), Color.Gray);   // Black trans bars

            // Draw TurtleSim Logo
            sB.Draw(messagebox, new Rectangle(Convert.ToInt32(240), 0, Convert.ToInt32(320), Convert.ToInt32(180)), Color.White);
            sB.Draw(logo, new Rectangle(Convert.ToInt32(240) - logoscaler, Convert.ToInt32(-20) - logoscaler, Convert.ToInt32(320) + logoscaler + logoscaler, Convert.ToInt32(250) + logoscaler + logoscaler), Color.White);

            // Draw buttons
            btnStart.Draw(sB);
            btnContinue.Draw(sB);
            btnQuit.Draw(sB);

            sB.DrawString(debugFont_tiny, "Added Features:\n" + newthings, new Vector2(10, 180), Color.White);
            sB.DrawString(debugFont_tiny, GameInfo, new Vector2(SceneWidth - 260, SceneHeight - 20), Color.White);
            sB.DrawString(debugFont_tiny, "Produced by Jacob Karleskint and Tclub Games", new Vector2(10, 460), Color.White);

            // End SpriteBatch
            sB.End();
        }



        public void UpdateControls(Point mousepos, bool bClicked)
        {
            MousePos = mousepos;

            // send all this to the buttons
            btnStart.UpdateControls(mousepos, bClicked);
            btnContinue.UpdateControls(mousepos, bClicked);
            btnQuit.UpdateControls(mousepos, bClicked);

        }
    }
}
