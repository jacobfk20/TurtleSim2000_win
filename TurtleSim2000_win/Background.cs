﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleSim2000_Linux
{
    class Background
    {
        public bool bShowBackground = false;

        string contentPath = "Content/assets/backgrounds/";

        int totalBackgrounds = 0;
        string[] backgroundList;
        string missingBackground = "missing";

        // for screen shaking
        int shakeTime = 20;
        Vector2 oldPos = new Vector2();
        bool bShakeScreen = false;

        // for day/night
        bool bContainsNight = false;
        float alpha = 0f;

        Rectangle backGroundDims;

        // Textures
        Texture2D currentBackground;
        Texture2D currentBackgroundNight;
        string currentBackgroundString;
        string currentBackgroundNightString;

        // Store all backgrounds in their own variables
        Texture2D[] bgList = new Texture2D[500];

        ContentManager contentManager;



        /// <summary>
        /// Inits the background class.  Handles all backgrounds (importing, and drawing)
        /// </summary>
        /// <param name="content">Content manager for importing assets</param>
        public Background(ContentManager content)
        {
            // Write Info about CharaManager to debug:
            Console.WriteLine("=======================================================================");
            Console.WriteLine("===   Background Manager - Importing backgroudns                    ===");
            Console.WriteLine("=======================================================================");
            Console.WriteLine("Looking for backgrounds in location: " + contentPath + "");

            // get all files in /backgrounds
            // Store in a temp array so we can destroy null arrays we don't need.
            string[] bcList = new string[500];
            bcList = Directory.GetFiles(contentPath);

            // Find how many arrays are actually used.
            totalBackgrounds = bcList.Length;

            // Create final list with appropriate array size
            backgroundList = new string[totalBackgrounds];
            backgroundList = bcList;

            // Tell the console what we've done
            Console.WriteLine("*Found " + totalBackgrounds + " backgrounds!");

            // Remove extensions and /content/
            for (int i = 0; i < totalBackgrounds; i++)
            {
                backgroundList[i] = backgroundList[i].Remove(backgroundList[i].Length - 4);
                backgroundList[i] = backgroundList[i].Remove(0, 8);
            }

            Console.WriteLine("Ripped background extensions and Content/");
            Console.WriteLine("=======================================================================");

            // give content manager to class
            contentManager = content;

            // Load all textures in
            for (int x = 0; x < totalBackgrounds; x++)
            {
                bgList[x] = content.Load<Texture2D>(backgroundList[x]);
            }
        }

        public void Update(int time)
        {
            shakeScreen();
            changeBackgroundDayNight(time);
        }

        public void Draw(SpriteBatch sb)
        {
            if (bShowBackground)
            {
                sb.Draw(currentBackground, backGroundDims, Color.White);
                if (bContainsNight) sb.Draw(currentBackgroundNight, backGroundDims, Color.White * alpha);
            }
        }

        /// <summary>
        /// Sets the current background for drawing.  Does all the dirty work.
        /// </summary>
        /// <param name="background">the background string (no path needed)</param>
        /// <returns></returns>
        public int setBackground(string background)
        {
            // add path to background name.
            background = contentPath + background;

            // Remove Content/
            background = background.Remove(0, 8);

            // See if the background is in the list.
            for (int i = 0; i < totalBackgrounds; i++)
            {
                if (backgroundList[i] == background)
                {
                    currentBackgroundString = background;

                    // see if this background has different times of day
                    bContainsNight = false;                                 // zero out variables
                    currentBackgroundNightString = null;

                    for (int x = 0; x < totalBackgrounds; x++)
                    {
                        if (backgroundList[x] == currentBackgroundString + "_night")
                        {
                            bContainsNight = true;
                            currentBackgroundNightString = backgroundList[x];
                            x = totalBackgrounds;
                        }
                    }

                    return i;
                }
            }
            // Couldn't Find background, tell user
            Console.WriteLine("bgMan: Couldn't Find background: " + background);

            // Couldn't find background set as missing texture.
            background = contentPath + missingBackground;
            currentBackgroundString = background;

            return -1;
        }

        /// <summary>
        /// Swaps the background.  This must always be called to change backgrounds.  
        /// Used mostly with transition changes.
        /// </summary>
        public void Swap()
        {
            // Set new background
            if(currentBackgroundString != null) currentBackground = contentManager.Load<Texture2D>(currentBackgroundString);

            // set night version if it is available
            if (bContainsNight && currentBackgroundNightString != null)
                currentBackgroundNight = contentManager.Load<Texture2D>(currentBackgroundNightString);
        }

        /// <summary>
        /// Sets the universal background dimensions.
        /// </summary>
        /// <param name="Width">how long the background is</param>
        /// <param name="Height">how high the background is</param>
        public void setBackgroundDimensions(int Width, int Height)
        {
            backGroundDims.Width = Width;
            backGroundDims.Height = Height;
        }

        public void shakeBackground(int time = 20)
        {
            bShakeScreen = true;
            shakeTime = time;
            oldPos.X = backGroundDims.X;
            oldPos.Y = backGroundDims.Y;
        }


        // Privates

        private void shakeScreen()
        {
            if (bShakeScreen)
            {
                Random ran = new Random();

                // do randomly x and y
                if (ran.Next(3) >= 2)
                {
                    backGroundDims.X += ran.Next(10);
                    backGroundDims.Y += ran.Next(6);
                }
                else
                {
                    backGroundDims.X -= ran.Next(10);
                    backGroundDims.Y -= ran.Next(6);
                }

                // subtract frames
                shakeTime--;

                // if at the end of shake time
                if(shakeTime == 0)
                {
                    bShakeScreen = false;
                    backGroundDims.X = Convert.ToInt32(oldPos.X);
                    backGroundDims.Y = Convert.ToInt32(oldPos.Y);
                }
            }
        }

        private void changeBackgroundDayNight(int time)
        {
            if (time > 1700)
            {
                alpha = time * 0.0004f;
            }
            if (time > 500 && time < 1200)
            {
                alpha = time * -0.0004f;
                // some insane math needs to be done here.  Or some simple algo.
            }
        }
    }
}
