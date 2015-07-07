﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TurtleSim2000_Linux
{
    class CharaManager
    {

        // Globals
        string[,] charaList = new string[100,500];
        string[] directoryList;

        int MISSINGCHARA = 199;

        int[] activeCharas = new int[5];        // Which charas are to be drawn. 6 is max.
        int drawnCharas = 0;                    // how many charas are currently being drawn on screen.
        Color universalColor = Color.White;     // Sets the color for all charas

        ContentManager contentManager;

        Chara[] charaArray = new Chara[200];

        int charaProfiles = 0;
        int charaPoses = 0;

        // The max amount of chara on screen & drawing layers
        int MAX_LAYERS = 5;

        // Constructor for manager.
        public CharaManager(ContentManager contentmanager)
        {
            // Get Content Manager
            contentManager = contentmanager;

            // Write Info about CharaManager to debug:
            Console.WriteLine("=======================================================================");
            Console.WriteLine("===   CharaManager - Importing All Chara data                       ===");
            Console.WriteLine("=======================================================================");

            // Look at the asset directory and look chara folders
            directoryList = Directory.GetDirectories("Content/assets/chara");
            
            // go through directories and bring in .xnb assets ([chara folder, chara pose])
            for (int i = 0; i < directoryList.Length; i++)
            {
                charaProfiles++;
                // Store directory in temp array
                string[] tempList = Directory.GetFiles(directoryList[i]);

                // See if there is a settings.chara file and send it to the chara object
                string fileSettingsDir = "0";
                for (int u = 0; u < tempList.Length; u++)
                {
                    if (tempList[u].Contains("settings.chara"))
                    {
                        fileSettingsDir = tempList[u];
                    }
                }

                // Figure out the current chara's name
                int pos = directoryList[i].LastIndexOf('\\');
                string charName = directoryList[i].Substring(pos);
                charName = charName.Remove(0, 1);

                // Rip out full path of chara poses to better file them.
                for (int b = 0; b < tempList.Length; b++)
                {
                    // Remove up to pose name
                    pos = tempList[b].LastIndexOf('\\');
                    tempList[b] = tempList[b].Remove(0, pos + 1);

                    // Remove file extension (.png)
                    pos = tempList[b].LastIndexOf('.');
                    tempList[b] = tempList[b].Remove(pos, 4);
                    //Console.WriteLine(pos + " " + tempList[b]);
                }

                // Write temp array to new chara object
                charaArray[i] = new Chara(tempList, charName, i, fileSettingsDir, contentmanager);

                // write temp array to master
                for (int x = 0; x < tempList.Length; x++)
                {
                    charaList[i, x] = tempList[x];
                    //Console.WriteLine("CharaPose Found: " + charaList[i, x]);
                    charaPoses++;
                }
            }

            // Init activeChara List
            for (int i = 0; i < activeCharas.Length; i++)
            {
                activeCharas[i] = -1;
            }

            // output the amount of profiles and poses.
            Console.WriteLine("There are " + charaProfiles + " chara profiles, and " + charaPoses + " Chara poses.");
            Console.WriteLine("=======================================================================");

        }

        public void Update()
        {
            moveChara();
            transChara();
        }

        
        public void Draw(SpriteBatch sb)
        {
            // Draw active chara in order on layer data (stored in chara object)
            for (int i = 0; i < MAX_LAYERS; i++)
            {
                int currentChar = activeCharas[i];
                if (currentChar != -1)      // sees if the slot is not empty
                {
                    if (charaArray[currentChar].bDrawMe)        // Sees if this chara wants to be drawn.
                    {
                        // If transitioning, draw old pose as we draw and fade in the new one.
                        if (charaArray[currentChar].bTransMe && charaArray[currentChar].bNewShow == false)
                        {
                            sb.Draw(charaArray[currentChar].getTexture(false), charaArray[currentChar].charaPos, universalColor);
                        }

                        sb.Draw(charaArray[currentChar].getTexture(), charaArray[currentChar].charaPos, universalColor * charaArray[i].transAlphaNew);
                    }
                }
            }
        }

        /// <summary>
        /// Deals with almost everything when it comes to drawing a chara on screen.  Simple.
        /// </summary>
        /// <param name="charaname">Chara name</param>
        /// <param name="pose">Pose of chara to draw</param>
        /// <param name="effect">Transition effect</param>
        public void Show(string charaname, string pose, string effect = "alpha")
        {
            int cID = getCharaID(charaname);

            // See if this chara is already drawn to screen
            if (charaArray[cID].bDrawMe == false)
            {
                
                // setup char's pose and draw order.
                charaArray[cID].setPose(pose);
                charaArray[cID].setDrawOrder(drawnCharas + 1);
                drawnCharas++;

                // index chara to active list
                // See which slot is empty
                for (int i = 0; i < MAX_LAYERS; i++)
                {
                    if (activeCharas[i] == -1)
                    {
                        activeCharas[i] = cID;
                        i = MAX_LAYERS;
                    }
                }
                // Transition it in:
                charaArray[cID].bTransMe = true;

                // Set that this char is ready to be drawn.
                charaArray[cID].bDrawMe = true;
            }
            // if the chara is already drawn.
            else
            {
                charaArray[cID].setPose(pose);
                charaArray[cID].transAlphaNew = 0;
                charaArray[cID].bTransMe = true;
                charaArray[cID].bNewShow = false;
            }

            // Output this to console
            Console.WriteLine("cMan: Showing " + charaArray[cID].getName() + " with pose: " + pose);

        }

        /// <summary>
        /// Moves the chara in a specific direction for an x amount of pixels.
        /// </summary>
        /// <param name="chara">Chara name to move.</param>
        /// <param name="direction">What direction to move the char. up down left right</param>
        /// <param name="speed">speed of which how fast to move said char.</param>
        /// <param name="pixels">amount of pixels to move the char.</param>
        public void Move(string chara, string direction, int speed, int pixels = 0)
        {
            int id = getCharaID(chara);
            charaArray[id].moveSpeed = speed;
            charaArray[id].moveDirection = direction;
            charaArray[id].moveAmount = pixels;
            charaArray[id].bMoveMe = true;
        }

        /// <summary>
        /// Boolean set to darken all chara on screen.  (true: darken) (false: normal)
        /// </summary>
        /// <param name="bDark">True to darken chara</param>
        public void setDarkenChara(bool bDark)
        {
            if (bDark)
            {
                universalColor = Color.Gray;
            }
            else
            {
                universalColor = Color.White;
            }
        }

        /// <summary>
        /// Get the current location of any chara (drawn or not) on screen.
        /// </summary>
        /// <param name="charaName">The name of the chara you want to get.</param>
        /// <returns></returns>
        public Vector2 getCharaLocation(string charaName)
        {
            int cID = getCharaID(charaName);

            return new Vector2(charaArray[cID].charaPos.X, charaArray[cID].charaPos.Y);
        }


        // ------------------------------------------------------------------- privates ---------------------------------------------------------------------

        private int getCharaID(string charaname)
        {
            for (int i = 0; i < charaProfiles; i++)
            {
                if (charaArray[i].getName() == charaname) return charaArray[i].getID();
            }
            return MISSINGCHARA;
        }

        private void moveChara()
        {
            for (int i = 0; i < MAX_LAYERS; i++)
            {
                if (charaArray[i].bMoveMe)
                {
                    if (charaArray[i].moveDirection == "right") charaArray[i].charaPos.X += charaArray[i].moveSpeed;
                    if (charaArray[i].moveDirection == "left") charaArray[i].charaPos.X -= charaArray[i].moveSpeed;
                    if (charaArray[i].moveDirection == "up") charaArray[i].charaPos.Y -= charaArray[i].moveSpeed;
                    if (charaArray[i].moveDirection == "down") charaArray[i].charaPos.Y += charaArray[i].moveSpeed;

                    charaArray[i].moveAmount -= charaArray[i].moveSpeed;

                    if (charaArray[i].moveAmount <= 0)
                    {
                        charaArray[i].bMoveMe = false;
                        charaArray[i].moveAmount = 0;
                    }
                }
            }
        }

        private void transChara()
        {
            for (int i = 0; i < MAX_LAYERS; i++)
            {
                if (charaArray[i].bTransMe)
                {
                    charaArray[i].transAlphaNew += 0.05f;
                    if (charaArray[i].transAlphaNew >= 1.0f)
                    {
                        charaArray[i].bTransMe = false;
                        
                    }
                }
            }
        }

    }
}
