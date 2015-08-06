// =============================================================================================================
// === Clock Module.  This draws and deals with everything to do with the clock.                             ===
// =============================================================================================================
// = Written By Jacob Karleskint (2015 GNU License) This license should be in the root directoy of source.     =
// =============================================================================================================

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleSim2000_Linux
{
    class Clock
    {
        int X = 650;
        int Y = 5;
        int W = 150;
        int H = 50;

        ContentManager Content;

        Texture2D texClock;
        SpriteFont fontClock;

        public Color ClockColor = Color.Blue;           // The color of the clock's text.  Changeable in game.
        
        public struct _Time
        {
            public int fullTime;
            public int Day;
            public int DayOfWeek;
        }
        public _Time Time;

        bool bHour24 = false;                           // Sets the clock in 24 hour format instead of 12.
        public bool bAddTime = false;                   // Sets the clock to add time (animation)
        public bool bPM = false;                        // If using 12 hour format, this will set time to PM. False = AM.
        
        string TimeColon = ":";                         // Draws the : between hour and minutes.  Animates off and on.
        public string AMPM = "AM";
        string strHour = "04";                          // String form of Hour.  For displaying 0's in front.
        string strMinute = "02";                        // String form of Minute.  ^ that.
        public int Hour = 4;
        public int Minute = 02;
        int addMin = 0;                                 // How many minutes needed to be added (animation)

        int ColonFrames = 0;                            // Adds up and when hits a certain number, will animate :
        int ColonAnimateFrame = 60;                     // What frame to blink the :





        /// <summary>
        /// Creates a new clock object.  Pass ContentManager so it may create its textures.
        /// Must Use Update(), Draw().  Add time with addTime().
        /// </summary>
        public Clock(ContentManager content)
        {
            Content = content;

            texClock = Content.Load<Texture2D>("assets/gui/clock");
            fontClock = Content.Load<SpriteFont>("fonts/clockfont");
        }



        /// <summary>
        /// The Update method in Clock.  This mostly just animates the clock adding time.
        /// </summary>
        public void Update()
        {
            if (bAddTime)
            {
                Minute++;
                addMin++;

                // Add an hour if minutes are over 59
                if (Minute > 59)
                {
                    Hour++;
                    Minute = 0;

                    // Roll over to AM or PM and reset hours (For 12 hour format)
                    if (Hour > 12)
                    {
                        Hour = 1;

                        // Set PM/AM accordingly.
                        if (!bPM) bPM = true;
                        else
                        {
                            Time.Day++;
                            Time.DayOfWeek++;
                            bPM = false;
                        }
                    }
                    
                }
            }

            // Set AM or PM
            if (bPM) AMPM = "PM";
            else AMPM = "AM";

            // Animate ":"
            if (ColonFrames >= ColonAnimateFrame)
            {
                if (TimeColon == ":") TimeColon = " ";
                else TimeColon = ":";

                ColonFrames = 0;
            }

            // Add to colonframes
            ColonFrames++;

            // Take hours and minutes and convert them to strings
            strHour = Hour.ToString();
            strMinute = Minute.ToString();

            // Add 0 if either is single digit.
            if (strHour.Length == 1) strHour = "   " + strHour;
            if (strMinute.Length == 1) strMinute = "0" + strMinute;

            // If We're out of time to add, stop adding time.
            if (addMin <= 0) bAddTime = false;

            // Convert to 24 hour for fullTime
            Time.fullTime = (100 * Hour) + Minute;
            if (bPM) Time.fullTime += 1200;

            // Update Dayofweek
            if (Time.DayOfWeek > 7) Time.DayOfWeek = 1;
            
        }



        /// <summary>
        /// Draws the clock and time onto screen.  Must pass a sprite batch object!
        /// </summary>
        /// <param name="sB"></param>
        public void Draw(SpriteBatch sB)
        {
            // Draw Clock Texture
            sB.Draw(texClock, new Rectangle(X, Y, W, H), Color.White);

            // Draw Time
            sB.DrawString(fontClock, strHour + " ", new Vector2(X + 10, Y + 7), ClockColor);
            sB.DrawString(fontClock, TimeColon, new Vector2(X + 48, Y + 7), ClockColor);
            sB.DrawString(fontClock, "" + strMinute, new Vector2(X + 54, Y + 7), ClockColor);
            sB.DrawString(fontClock, AMPM, new Vector2(X + 92, Y + 7), ClockColor);
        }


        
        /// <summary>
        /// Adds time safely to the clock.  This function adds via single int.  (30 = 30 minutes) (120 = 120 minutes [2 hours])
        /// </summary>
        public void addTime(int minutes)
        {
            addMin += minutes;
            bAddTime = true;
        }

        /// <summary>
        /// Sets the clock straight to a certain time.
        /// </summary>
        public void setTime(int hour, int min)
        {
            Hour = hour;
            Minute = min;
        }



    }
}
