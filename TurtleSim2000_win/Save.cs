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
    }
}
