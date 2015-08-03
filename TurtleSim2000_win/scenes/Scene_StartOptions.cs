using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TurtleSim2000_Linux
{
    class Scene_StartOptions
    {
        public int SceneHeight = 320;
        public int SceneWidth = 400;
        public int X = 200;
        public int Y = 140;

        float AlphaBlend = 1f;

        public bool bEnabled = false;                                           // This will enable this window and draw all modules within it

        ContentManager Content;

        Texture2D WindowBack;
        SpriteFont font;

        public Button btnFullScreen;
        public Button btnExit;


        public Scene_StartOptions(ContentManager content)
        {
            WindowBack = content.Load<Texture2D>("assets/gui/messagebox");
            font = content.Load<SpriteFont>("fonts/debugfont");

            Content = content;

            // Add buttons
            btnFullScreen = new Button(Content, "Fullscreen", new Rectangle(X + 40, Y + 100, 140, 40));
            btnExit = new Button(Content, "Return", new Rectangle(X + 120, Y + 180, 140, 40));
        }



        public void Update()
        {
            if (bEnabled)
            {
                btnExit.Update();
                btnFullScreen.Update();
            }
        }



        public void Draw(SpriteBatch sB)
        {
            if (bEnabled)
            {
                // Draw the background
                sB.Draw(WindowBack, new Rectangle(X, Y, SceneWidth, SceneHeight), Color.White * AlphaBlend);

                // Draw Text
                sB.DrawString(font, "Options and Settings Menu", new Vector2(X + 20, Y + 30), Color.White * AlphaBlend);

                // Draw Options
                // For Full screen:
                sB.DrawString(font, "Toggle Fullscreen On/Off", new Vector2(X + 20, Y + 60), Color.White * AlphaBlend);
                btnFullScreen.Draw(sB);

                // For Exit:
                btnExit.Draw(sB);
            }
        }



        public void UpdateControls(Point mousepos, bool bClicked)
        {
            if (bEnabled)
            {
                // Update button control logic and hittest
                btnFullScreen.UpdateControls(mousepos, bClicked);
                btnExit.UpdateControls(mousepos, bClicked);

                // for local button logic:
            }

        }


    }
}
