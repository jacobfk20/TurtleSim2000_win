using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TurtleSim2000_Linux
{
    class MusicManager
    {
        Song[] musicList;
        int musicCount = 0;
        int musicIndex = 0;

        bool bPlaying = false;



        public MusicManager(ContentManager content)
        {
            // Get how many songs are in the directory
            string[] tempList = new string[1000];
            tempList = System.IO.Directory.GetFiles("Content/assets/music");

            // rip out unneeded crap
            for (int i = 0; i < tempList.Length; i++)
            {
                if (tempList[i].Contains(".xnb") == false) musicCount++;
                tempList[i] = tempList[i].Remove(0,7);
                tempList[i] = tempList[i].Remove(tempList[i].IndexOf('.'));
            }

            // set amount of music to hold
            musicList = new Song[musicCount];
            
            // Load in the music
            for (int x = 1; x < tempList.Length; x++)
            {
                if (tempList[x] != tempList[x - 1])
                {
                    musicList[musicIndex] = content.Load<Song>("Content" + tempList[x]);
                    musicIndex++;
                }
            }

            // get the real music count
            musicCount = musicIndex;
            musicIndex = 0;
        }

    }
}
