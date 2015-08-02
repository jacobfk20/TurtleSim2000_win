using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TurtleSim2000_Linux
{
    class ProgressBar
    {
        ContentManager Content;

        /// <summary>
        /// This sets the X, Y, Width, and Height of the Progress bar.
        /// </summary>
        public Rectangle boxDim = new Rectangle(0, 0, 180, 40);

        Vector2 TextPos;

        string Text = "";
        int Value = 0;                              // Value of probar
        int oValue = 0;                             // Old value for non-percentage animation
        int nValue = 0;                             // New value for non-percentage animation
        int PValue = 0;                             // Percent of 100
        int OldValue = 0;                           // Old value of bar, for animation.
        int NewValue = 0;                           // The value to be added to the bar after animation
        int MaxValue = 100;                         // Max value.

        bool bAddNewValue = false;                  // Sets to animate the new value being filled.
        bool bAddNewNonPercentValue = false;        // Sets to animate the new value being written in text non-percentage
        bool setAsPercent = false;

        // Textures
        Texture2D bar_back;
        Texture2D bar_siding;
        Texture2D bar_fill;

        // Fonts
        SpriteFont font;




        /// <summary>
        /// Creates A new progress bar.  Pass ContentManager so it may load its textures.
        /// Call (Draw().  Update() not needed, but can be used.) (set as percent will set x value of 100%
        /// </summary>
        public ProgressBar(ContentManager content, string name, Rectangle rectangle, int maxValue = 100, bool SetAsPercent = true)
        {
            Content = content;
            setAsPercent = SetAsPercent;
            MaxValue = maxValue;

            // Sets the name
            Text = name;

            // Sets the diminsions.
            boxDim = rectangle;



            // Load content
            bar_back = Content.Load<Texture2D>("assets/gui/bar_backlayer");
            bar_siding = Content.Load<Texture2D>("assets/gui/bar_edges");
            bar_fill = Content.Load<Texture2D>("assets/gui/bar_progress");

            font = Content.Load<SpriteFont>("fonts/debugfont");

            // Set the position for text
            TextPos.X = boxDim.X + bar_siding.Width + 8;
            TextPos.Y = boxDim.Y + 1;
        }




        public void Update()
        {
            // Animate the bar
            if (bAddNewValue)
            {
                // For subtracting value
                if (OldValue > NewValue)
                {
                    PValue--;
                    if (PValue <= NewValue)
                    {
                        PValue = NewValue;
                        OldValue = 0;
                        bAddNewValue = false;
                    }
                }
                // For Adding value
                if (OldValue < NewValue)
                {
                    PValue++;
                    if (PValue >= NewValue)
                    {
                        PValue = NewValue;
                        OldValue = 0;
                        bAddNewValue = false;
                    }
                }
            }

            // For adding up the numbers when not doing percentage mode
            if (bAddNewNonPercentValue)
            {
                // For Subtracting Value:
                if (oValue > nValue)
                {
                    Value--;
                    if (Value <= nValue)
                    {
                        Value = nValue;
                        oValue = 0;
                        bAddNewNonPercentValue = false;
                    }
                }
                // For Adding Value:
                if (oValue < nValue)
                {
                    Value++;
                    if (Value >= nValue)
                    {
                        Value = nValue;
                        oValue = 0;
                        bAddNewNonPercentValue = false;
                    }
                }
            }
        }



        /// <summary>
        /// Draws the progress bar.
        /// </summary>
        /// <param name="sB"></param>
        public void Draw(SpriteBatch sB)
        {
            // Draw the backdrop of the bar.
            sB.Draw(bar_back, new Rectangle(boxDim.X, boxDim.Y, bar_back.Width + 6, bar_back.Height - 6), Color.White);

            // Draw in the total progress based on V
            int spacer = 0;

            // Fill the bar
            for (int i = 0; i < PValue; i++)
            {
                sB.Draw(bar_fill, new Rectangle(boxDim.X + 4 + spacer, boxDim.Y + 3, bar_fill.Width, bar_fill.Height - 6), Color.White);
                spacer += 2;
            }

            // Draw the progress inside the bar.
            if (setAsPercent) sB.DrawString(font, PValue + "%", new Vector2(boxDim.X + 85, boxDim.Y), Color.White);
            else sB.DrawString(font, Value + "/" + MaxValue, new Vector2(boxDim.X + 85, boxDim.Y), Color.White);

            // Draw the name of the bar
            if (Text != null) sB.DrawString(font, Text, TextPos, Color.White);


            // Draw the siding
            sB.Draw(bar_siding, new Rectangle(boxDim.X, boxDim.Y, bar_siding.Width + 6, bar_siding.Height - 6), Color.White);
        }



        /// <summary>
        /// Safely Adds a new value to the progress bar.
        /// </summary>
        public void addValue(int value)
        {
            value = Value + value;
            checkValue(value);
        }


        /// <summary>
        /// Safely Sets the value in the progress bar.
        /// </summary>
        public void setValue(int value)
        {
            if (value != Value)
            {
                checkValue(value);
            }
        }


        /// <summary>
        /// Increases the max value.
        /// </summary>
        public void setMaxValue(int value)
        {
            MaxValue = value;
        }


        // Checks and Safely adds the new value to the bar.
        private void checkValue(int value)
        {
            // Makes sure the new value isn't over the max value.
            if (value > MaxValue) value = MaxValue;
            if (value < 0) value = 0;

            if (setAsPercent)
            {
                // set new value:
                Value = value;
            }
            else
            {
                // Put Old Value into oValue
                oValue = Value;
                // Put new value into nValue
                nValue = value;

                // set to draw the numbers one by one
                bAddNewNonPercentValue = true;
            }

            // Get percentage out of 100.
            OldValue = PValue;
            double hold = Convert.ToDouble(value) / Convert.ToDouble(MaxValue) * 100d;
            NewValue = Convert.ToInt32(hold);

            // Set to animate new value in
            bAddNewValue = true;
           
        }


    }
}
