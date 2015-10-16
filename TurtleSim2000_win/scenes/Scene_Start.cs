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
        public Button btnDemo;
        public Button btnOptions;

        // Other Windows and Scenes
        public Scene_StartOptions sceneOptions;

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
            btnStart = new Button(contentManager, "Start Game", new Rectangle(320, 200, 160, 40), 1);
            btnContinue = new Button(contentManager, "Continue", new Rectangle(320, 240, 160, 40), 2);
            btnQuit = new Button(contentManager, "Quit", new Rectangle(320, 280, 160, 40), 3);
            btnDemo = new Button(contentManager, "Demo", new Rectangle(320, 320, 160, 40), 4);
            btnOptions = new Button(contentmanager, "Options", new Rectangle(320, 360, 160, 40), 5);

            // Other Scenes and Windows
            sceneOptions = new Scene_StartOptions(contentmanager);
        }



        public void Unload()
        {
            logo.Dispose();
            messagebox.Dispose();
            bg_gate.Dispose();
            bg_forest.Dispose();
            bg_courtyard.Dispose();
            trans_BlackBars.Dispose();
            
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

            // For other Scenes
            sceneOptions.Update();

        }



        public void Draw(SpriteBatch sB)
        {
            // Start SpriteBatch
            sB.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Resolution.getTransformationMatrix());

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
            btnDemo.Draw(sB);
            btnOptions.Draw(sB);

            sB.DrawString(debugFont_tiny, "Added Features:\n" + newthings, new Vector2(10, 180), Color.White);
            sB.DrawString(debugFont_tiny, GameInfo, new Vector2(SceneWidth - 260, SceneHeight - 20), Color.White);
            sB.DrawString(debugFont_tiny, "Produced by Jacob Karleskint and Tclub Games", new Vector2(10, 460), Color.White);

            // For Options window
            sceneOptions.Draw(sB);

            // End SpriteBatch
            sB.End();
        }



        public void UpdateControls(Point mousepos, bool bClicked, Controls controller)
        {
            MousePos = mousepos;

            // send all this to the buttons
            if (!sceneOptions.bEnabled)
            {
                btnStart.UpdateControls(mousepos, bClicked, buttonIndex);
                btnContinue.UpdateControls(mousepos, bClicked, buttonIndex);
                btnQuit.UpdateControls(mousepos, bClicked, buttonIndex);
                btnDemo.UpdateControls(mousepos, bClicked, buttonIndex);
                btnOptions.UpdateControls(mousepos, bClicked, buttonIndex);

                if (btnOptions.bPressed) sceneOptions.bEnabled = true;
            }
            else
            {
                // Update Options window logic
                sceneOptions.UpdateControls(mousepos, bClicked);

            }

            if (controller.bGamePad) updateGamePadSelected(controller);
            else
            {
                buttonIndex = 0;
            }
        }


        int buttonIndex = 1;
        private void updateGamePadSelected(Controls controller)
        {
            if (controller.dpad.up) buttonIndex--;
            if (controller.dpad.down) buttonIndex++;

            // make sure focus doesn't go out of button range
            if (buttonIndex > 5) buttonIndex = 1;
            if (buttonIndex < 1) buttonIndex = 5;

        }

    }
}
