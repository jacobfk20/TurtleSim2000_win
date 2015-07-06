// TODO:  Keep loaded textures in memory then dispose of them when the chara is taken off screen.


using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace TurtleSim2000_Linux
{
    public class Chara
    {
        string charaName;
        int charaID;
        string[] charaPoses;
        int poseCount = 0;
        int drawOrder = 0;      // What layer this chara should be drawn on.

        ContentManager contentManager;

        public Rectangle charaPos = new Rectangle();        // Stores chara XYWH

        public bool bDrawMe = false;   // Draw this chara or not.
        public bool bMoveMe = false;   // true when this char is set to be moved on screen.
        public bool bTransMe = false;  // true when this char needs to be transitioned to another char.

        // Vars for moving:
        public int moveAmount = 0;      // How far this char needs to be moved.
        public int moveSpeed = 2;       // How fast this char will move
        public string moveDirection;           // What direction this char is moving

        // Vars for showing:
        string currentPose = "missing";  // Which pose this char is currently in
        string oldPose = "missing";      // Old pose before changed to new.  Used for transitions.
        string missingPose = "missing";  // What to draw if the pose isn't findable.

        // Vars for showing:transitions:
        public float transAlphaNew = 0;
        public float transAlphaOld = 0;


        /// <summary>
        /// Creates a new character object.  Holds Name, Poses, and Coords on screen.  Also handles drawing to screen
        /// </summary>
        /// <param name="poseList">Imports an array of poses this chara has (must be link to directory)</param>
        /// <param name="charaname">The name of the chara which will be used for reference</param>
        /// <param name="charid">The ID of this chara.  Must be unique.</param>
        public Chara(string[] poseList, string charaname, int charid, string pathtosettingsfile, ContentManager contentmanager)
        {
            charaPoses = poseList;
            charaName = charaname;
            charaID = charid;
            contentManager = contentmanager;

            poseCount = poseList.Length;

            Console.WriteLine("Chara: " + charaName + " has been added to charaManager with " + poseCount + " poses.");

            // see if there is a settings file for this chara.
            if (pathtosettingsfile != "0")
            {
                getFileSettings(pathtosettingsfile);
            }
            else
            {
                // Just use defaults
                charaPos.X = 100;
                charaPos.Y = -100;
            }
        }


        /// <summary>
        /// Set a new pose for this chara.  Handles if charapose is missing
        /// </summary>
        /// <param name="newPose">new chara pose</param>
        /// <returns>current pose (if missing, returns missing)</returns>
        public string setPose(string newPose)
        {
            for (int i = 0; i < charaPoses.Length; i++)
            {
                if (newPose == charaPoses[i])
                {
                    oldPose = currentPose;
                    currentPose = newPose;
                    return currentPose;
                }
            }
            // If pose wasn't found.  Set it to the MissingPose slate
            currentPose = missingPose;
            Console.WriteLine("The pose: " + newPose + ". Does not exist for chara: " + charaName);
            return currentPose;
        }

        #region Gets and Sets
        /// <summary>
        /// Gets the current pose of this chara.
        /// </summary>
        /// <returns>Current Pose</returns>
        public string getPose()
        {
            return currentPose;
        }

        /// <summary>
        /// Get's the layer the chara is drawn on.
        /// </summary>
        /// <returns>drawOrder</returns>
        public int getDrawOrder()
        {
            return drawOrder;
        }

        /// <summary>
        /// Change the chara's draw layer.  If you want to have this chara be on top
        /// then just make their layerID be higher.  Higher int = higher draw order.
        /// </summary>
        /// <param name="newLayerID">The new layer ID you want this chara to be drawn on</param>
        /// <returns></returns>
        public int setDrawOrder(int newLayerID)
        {
            if (newLayerID >= 0)
            {
                drawOrder = newLayerID;
            }
            return newLayerID;
        }

        /// <summary>
        /// Get the chara's Unique ID.
        /// </summary>
        /// <returns>Chara ID</returns>
        public int getID()
        {
            return charaID;
        }

        /// <summary>
        /// Get's the chara's name.
        /// </summary>
        /// <returns>charaName</returns>
        public string getName()
        {
            return charaName;
        }

        /// <summary>
        /// Gets the current pose texture from chara (True for new pose texture.  False for old texture)
        /// </summary>
        /// <returns> texture</returns>
        public Texture2D getTexture(bool bNewTex = true)
        {
            string currentTex;

            if (bNewTex)
            {
                currentTex = charaName + "/" + currentPose;
            }
            else
            {
                currentTex = charaName + "/" + oldPose;
            }
            Texture2D poseTexture = contentManager.Load<Texture2D>("assets/chara/" + currentTex + "");

            // get width and heighth of texture for correct chara drawing
            charaPos.Width = poseTexture.Width;
            charaPos.Height = poseTexture.Height;
            return poseTexture;
        }
        #endregion

        // Privates
        private void getFileSettings(string pathtosettingsfile)
        {
            string[] settingsList = new string[5];
            int index = 0;
            using (System.IO.Stream fileStream = System.IO.File.Open(pathtosettingsfile, System.IO.FileMode.Open))
            using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
            {
                bool quit = false;
                while (quit == false)
                {
                    string line = reader.ReadLine();

                    // if line contains ';' then ignore that line.
                    if (line.Contains(';') == false)
                    {
                        settingsList[index] = line;
                        index++;
                    }
                    if (reader.EndOfStream) quit = true;
                }
            }

            // Apply settings to chara
            // CharaName: Overrides what charamanager pulls from folder name
            if (settingsList[0].Length > 0) charaName = settingsList[0];

            // Starting Position for X coord: if set at 0 we'll just go with defualt x pos.
            if (settingsList[1].Length > 0) charaPos.X = Convert.ToInt32(settingsList[1]);

            // Starting Position for Y Coord: if set at 0 we'll just go with default y pos.
            if (settingsList[2].Length > 0) charaPos.Y = Convert.ToInt32(settingsList[2]);

            // Missing pose replace: what to draw if the pose specified couldn't be found.
            if (settingsList[3].Length > 0) missingPose = settingsList[3];
            currentPose = missingPose;
            oldPose = missingPose;

            Console.WriteLine("  +" + charaName + " Has a settings.chara. X:" + charaPos.X + " Y:" + charaPos.Y + " " + settingsList[3].Length);
        }

    }
}
