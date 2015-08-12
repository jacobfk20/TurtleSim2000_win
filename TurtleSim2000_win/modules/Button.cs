// =====================================================================================
// === Button Class.  This deals with everything buttons.                            ===
// =====================================================================================
// = Written by Jacob Karleskint 2015 for use in the TurtleSim Engine                  =
// =====================================================================================

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
    class Button
    {
        /// <summary>
        /// The rectangle of the button.  X, Y, Width, and Height
        /// </summary>
        public Rectangle boxDim = new Rectangle(0, 0, 200, 160);

        /// <summary>
        /// This will draw the pressed texture for the button.
        /// </summary>
        public bool bPressed = false;

        // This will hold if the mouse is hovering over this button.
        bool bHover = false;

        // The text to be drawn in the button's body.
        string Text = "Button Text";

        Vector2 textPos;                                // Where to draw the text.  
        int textScale = 0;                              // How much to scale the button's text
        int fontHeight = 28;                            // How tall the average char is.

        ContentManager contentManager;                  // Content manager for loading textures.

        // Textures
        Texture2D texButton;                            // Texture for button main.
        Texture2D texButton_down;                       // Texture for button pushed down.
        SpriteFont texFont;                             // Font texture for button.




        /// <summary>
        /// Creates a new button.  Must pass content manager so it can load its textures.
        /// </summary>
        public Button(ContentManager contentmanager, string text)
        {
            contentManager = contentmanager;

            Text = text;

            contentLoad();      // Loads textures for button

            centerText();       // Centers the text inside the button

        }

        /// <summary>
        /// Creates a new button.  Must pass content manager.  rectangle: Diminsions sets X,Y. Width and Height.
        /// </summary>
        public Button(ContentManager contentmanager, string text, Rectangle diminsions)
        {
            contentManager = contentmanager;
            boxDim = diminsions;

            Text = text;

            contentLoad();

            centerText();       // Center text inside the button.
        }



        /// <summary>
        /// Updates the button's logic.  Must be called in game update loop!
        /// </summary>
        public void Update()
        {

        }



        /// <summary>
        /// Draws the button to screen.  Must be called in game draw loop!
        /// </summary>
        public void Draw(SpriteBatch sB, float alpha = 1f)
        {
            if (!bHover) sB.Draw(texButton, boxDim, Color.White * alpha);
            else sB.Draw(texButton_down, boxDim, Color.White * alpha);
            sB.DrawString(texFont, Text, textPos, Color.White * alpha);
        }



        /// <summary>
        /// Sets the text to a new position in the button.  This is already set to center at creation.
        /// The button text X,Y is anchored to the button's bounding box.  Not the screen.
        /// </summary>
        public void setTextPosition(int x, int y)
        {
            textPos.X += x;
            textPos.Y += y;

        }



        /// <summary>
        /// Sets new text for the button and centers it.
        /// </summary>
        public void setText(string text)
        {
            Text = text;

            centerText();
        }



        /// <summary>
        /// Updates the Control (hittest) for this button.  This determines if the mouse is within it and sends it back via return and bPressed.
        /// </summary>
        /// <returns>True: This button has been pressed.  False: Not pressed.</returns>
        public bool UpdateControls(Point mousePos, bool bClicked)
        {
            // Check if the mouse is hovering over this button
            if (boxDim.Contains(mousePos))
            {
                // Set hover to true.  This can be used to draw a seperate texture of the button.
                bHover = true;
                if (bClicked)
                {
                    // Set global bPressed to true so main class can see that.
                    bPressed = true;
                    return true;
                }
            }
            else
            {
                // The mouse isn't inside the button's bounding box:
                bHover = false;
            }

            return false;
        }




        // Loads in content for textures.
        private void contentLoad()
        {
            texButton = contentManager.Load<Texture2D>("assets/gui/gui_button_up");
            texButton_down = contentManager.Load<Texture2D>("assets/gui/gui_button_down");
            texFont = contentManager.Load<SpriteFont>("fonts/debugfont");
        }


        // Tries to center the text in the button.
        private void centerText()
        {
            // If this is called, then textPos needs to be cleared.
            textPos.X = 0;
            textPos.Y = 0;

            // Set textPos XandY to anchor button bounding box.
            textPos.X += boxDim.X;
            textPos.Y += boxDim.Y;

            // Try and get center of box. Y
            float halfY = boxDim.Height / 2;                        // Gets the button's Height, halves it.
            halfY -= fontHeight / 2;                                // Takes the font's average height, and halves it to get font center.
            textPos.Y += Convert.ToInt32(halfY);                    // Maybe add something here when text gets scaled.
            

            // Try and get center of box for X.
            float halfX = boxDim.Width / 2;
            halfX -= Text.Length * 5.6f;            // For every char subtract string by x amount.
            textPos.X += Convert.ToInt32(halfX);
        }
    }
}
