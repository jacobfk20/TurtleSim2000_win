using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TurtleSim2000_Linux
{
    class Transitions : Microsoft.Xna.Framework.Game
    {
        SpriteBatch spriteBatch;

        // Texture variables
        Texture2D blackFade;
        SpriteFont debugFont;

        // Logic vars
        int TimeDuration;                   // Stores how much time the transition is on screen
        int TimeStore;                      // Stores halfed time
        string TransitionName = "";         // Stores the name of the transition that will be used
        float alpha = 0f;                   // Stores alpha (fading) float variable
        float alphaDiv = 0f;                // Stores how much alpha fading needs to be aplied every frame.

        public Transitions()
        {

        }

        public void loadContent(SpriteBatch spritebt, ContentManager Content)
        {
            // Get main spritebatch!
            spriteBatch = spritebt;

            // Load content here:
            blackFade = Content.Load<Texture2D>("blacktrans");       // Fade in/out of black texture
            debugFont = Content.Load<SpriteFont>("fonts/debugfont");

        }

        // update logic
        public void Update()
        {
            // Update logic goes here:
            if (TransitionName != "")
            {
                if (TransitionName == "FadeBlack")
                {
                    if (TimeDuration > 0)
                    {
                        alpha += alphaDiv * .01f;
                        TimeDuration--;
                    }
                    else
                        if (TimeDuration <= 0 && TimeStore > 0)
                        {
                            alpha -= alphaDiv * .01f;
                            TimeStore--;
                        }
                        else
                        {
                            TimeStore = 0;
                            TimeDuration = 0;
                            TransitionName = "";
                        }
                }
            }
        }

        // Draw transition on screen
        public int Draw()
        {
            if (TransitionName == "FadeBlack") spriteBatch.Draw(blackFade, new Rectangle(0, 0, 800, 600), Color.Black * alpha);
            //priteBatch.DrawString(debugFont, "Time: " + TimeDuration, new Vector2(10, 10), Color.White);
            return 0;
        }

        // Get what transition must be shown
        public void Show(string transitionName, int time)
        {
            TransitionName = transitionName;
            TimeDuration = time /2;
            TimeStore = time /2;
            alphaDiv = 100 / TimeDuration;
        }

        // Get safety time when to change backgrounds
        public bool SafeToSwap()
        {
            if (TimeDuration == 0) return true;
            return false;
        }
    }
}
