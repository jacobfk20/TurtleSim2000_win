using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace TurtleSim2000_Linux
{
    public class Save
    {
        public struct SaveData
        {
            public bool[] gSwitches;
            public int[] gVariables;

        }

        public SaveData sD = new SaveData();

        int Saves()
        {
            return 1;
        }

        public int SyncData(StorageDevice device, bool[] Switches, int[] Variables)
        {
            SaveData gs = new SaveData();
            gs.gSwitches = Switches;
            gs.gVariables = Variables;

            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer("StorageDemo", null, null);

                device.BeginOpenContainer("GameData", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

            serializer.Serialize(stream, gs);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();

            return 0;
        }

        public void DumpToFile()
        {
            // Sync data real quick!
            
            // open file for writing.
            System.IO.StreamWriter sW = new StreamWriter("save.sav", false);

            // Write game header
            sW.Write("turtlesim gamesave§");
            
            // dump all game variables
            for (int i = 0; i < 500; i++)
            {
                sW.Write(sD.gVariables[i]);
                sW.Write('§');
            }

            // dump all game switches
            for (int i = 0; i < 500; i++)
            {
                if (sD.gSwitches[i] == true) sW.Write('t');
                else
                { 
                    sW.Write('f');
                }

                sW.Write('§');
            }

            // Write end of dump
            sW.Write('ô');

            // close file
            sW.Close();
            
        }

        public void loadFromFile()
        {
            // Check and see if the gamesave file is even there.
            if (SaveFilePresent())
            {
                // open the savegame file
                System.IO.StreamReader sR = new StreamReader("save.sav");

                // Read the whole file into memory.
                char[] saveFile = new char[5000];
                int fileIndex = 0;
                while(sR.EndOfStream == false)
                {
                    saveFile[fileIndex] = Convert.ToChar(sR.Read());
                    fileIndex++;
                }

                // check and make sure this is a turtlesim save
                string header = "";
                for (int i = 0; i < 20; i++)
                {
                    header += saveFile[i];
                }

                if (!header.Contains("turtlesim gamesave"))
                {
                    Console.WriteLine("Save/Load: Coudln't Load save file.  Mismatch header.");
                    return;
                }

                // set index to after header
                int index = 0;
                for (int i = 0; i < 200; i++)
                {
                    if (saveFile[i] == '§')
                   {
                        index = i + 1;
                        i = 200;
                    }
                }

                // Start reading in game variables from file.
                string varVal = "";
                int currentVar = 0;
                for (int i = index; i < saveFile.Length; i++)
                {
                    // null char; writes current string to variables
                    if(saveFile[i] == '§')
                    {
                        int var = Convert.ToInt32(varVal);
                        sD.gVariables[currentVar] = var;
                        currentVar++;
                        varVal = "";
                        if (currentVar > 499) i = saveFile.Length;
                    }
                    else
                        varVal += saveFile[i];
                }

                // close file
                sR.Close();



            }
        }

        /// <summary>
        /// See if there is a save game file to lead.
        /// </summary>
        /// <returns></returns>
        public bool SaveFilePresent()
        {
            return File.Exists("save.sav");
        }

    }
}
