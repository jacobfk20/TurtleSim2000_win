using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleSim2000_Linux
{
    class Stamps
    {

        Texture2D bgWindow;
        SpriteFont Font;
        string stampName = "";
        int Scroller = 0;         //used to move the popup window up and down.
        int ShowTime = 0;         //keeps track how long stamp window has been on screen.
        int ShowTimeMax = 300;    //How long it should stay on screen.
        bool bShow = false;       //Used to tell the update function to do its thang.

        //Contructor  (Gets font and background texture)
        public void Content(Texture2D window, SpriteFont font)
        {
            bgWindow = window;
            Font = font;
        }

        public int Popup(string stampname)
        {
            Texture2D thumb = null;
            if (thumb == null) thumb = bgWindow;
            stampName = stampname;

            ShowTime = 0;
            Scroller = 0;
            bShow = true;

            return 0;
        }

        public void update()
        {
            if (bShow)
            {
                if (Scroller < 160) Scroller += 3;
                else ShowTime++;

                if (ShowTime >= ShowTimeMax)
                {
                    Scroller -= 4;
                }

                if (Scroller <= -1)
                {
                    bShow = false;
                }

            }
        }

        public void draw(SpriteBatch spritebatch)
        {
            if (bShow)
            {
                spritebatch.Draw(bgWindow, new Rectangle(450, 500 - Scroller, 390, 160), Color.Black);
                spritebatch.DrawString(Font, "New Stamp! \n" + stampName, new Vector2(470, 520 - Scroller), Color.White);
            }
        }
    }
}
