// =====================================================================================
// ============                        Script Compiler                      ============
// =====================================================================================
// Written by Jacob Karleskint for use in TurtleSim Game.  This Class can be used 
// freely with any other code.  All under MIT license.
// -------------------------------------------------------------------------------------
// This class takes in TurtleSim Book files (.tsb) and reads them into a master script
// string array.  usually stored as Masterscript(Script, Line).
// -------------------------------------------------------------------------------------
// updates:
// - 6/9/2014 (jacob karleskint)
//      Now rips out comments.  Any line that starts with '//'
//      Now rips out tabs.  such as "   blah blah blah" becomes "blah blah blah
//---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace TurtleSim2000_Linux
{
    class ScriptCompiler
    {
        bool bDebugMode = false;

        string[,] MasterScript = new string[101, 500];
        string[,] Authors = new string[101, 3];
        int S;  //Script number
        int L;  //Line Number  (used to WRITE TO master)
        int _L;  //used to read FROM pages
        int totlines;  //gets how many lines are total
        int authorslot = 0;             // This keeps authors in order of added

        // For legacy support
        Basic basic = new Basic();      //Declares all basic script pages
        Emi emi = new Emi();            //Declares Emi's script pages
        Minor minor = new Minor();      //Declares all minor chara's pages

        // Constructor  (placeholder; not really needed)
        public ScriptCompiler()
        {
        }

        // This will compile all .tsb files into masterscript
        // Should be ran at game startup will return script count.
        public int Compile()
        {
            int authorslot = 0;

            // Import legacy scripts...
            compileLegacy();    // this is a private function of this class

            // Zero out line and placeholder
            _L = 0;
            L = 0;
            S++;

            // Read books here:
            // First we must get all books that are in the /scripts/books/ folder
            string[] fileList = Directory.GetFiles("Content/scripts/books");
            int len = fileList.Count();     // get how many books are in the array

            // Compile them.
            for (int i = 0; i < len; i++)
            {
                // Check to see if the file is the .tsb extension.
                int getExt = fileList[i].Length;
                string checkExt = fileList[i].Substring(getExt - 3);
                if (checkExt == "tsb")
                {
                    readBook(fileList[i]);
                }
            }

            // Check and read if there is a debug script (should always be last)
            readBook("/TurtleSim Authoring Tools/Content/scripts/debug.tss");

            return S;

        }

        // sends back the line requested from a script
        public string Read(int S, int L)
        {

            return MasterScript[S, L];

        }

        // sends back how many lines are in total.
        public int TotalLines()
        {
            return totlines;
        }

        // sends back rather there is a debug script or not.
        public bool IsDebug()
        {
            return bDebugMode;
        }

        // Reads in scripts from the books line by line.
        private int readBook(string file)
        {
            //int authorslot = 0;
            if (System.IO.File.Exists(file))
            {
                using (System.IO.Stream fileStream = System.IO.File.Open(file, System.IO.FileMode.Open))
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    string line = null;
                    while (true)
                    {
                        line = reader.ReadLine();

                        // Set debugmode to TRUE if a script is called "debug"
                        if (line == "debug") bDebugMode = true;

                        // This will continue to the next script slot as 
                        // break means end of script.
                        if (line == "break")
                        {
                            //MasterScript[S, L] = line;
                            _L++;
                            L = 0;
                            S++;
                            authorslot++;
                        }
                        else
                        {
                            // Checks if at line 1 (author line) and grabs author 
                            // name and stores with script # and name.
                            if (L == 1 && Authors[authorslot, 1] == null)
                            {
                                Authors[authorslot, 0] = Convert.ToString(S);
                                Authors[authorslot, 1] = line;
                                Authors[authorslot, 2] = MasterScript[S, 0];
                                
                            }
                            else
                            {
                                // This adds the line to the master script book.  (unless null or a commented line)
                                if (line != null && line.Contains("//") == false)   // rips out commented lines.
                                {
                                    if (line != "")
                                    {
                                        // This will check if the line has a tab, then rip it out.
                                        char chr = line[0];
                                        if (char.IsWhiteSpace(chr)) line = tabRip(line);

                                        MasterScript[S, L] = line;
                                        L++;
                                        totlines++;
                                        _L++;
                                    }
                                }
                            }
                        }

                        if (line == null)
                            break;
                    }
                }
                //Reset Current Line, Old Line, and increase script #
                _L = 0;
                L = 0;
                S++;
            }
            else
            {
                // debug purposes.
                //S += 1000;
            }
            return S;
        }

        // Rips out tabbing and indents
        private string tabRip(string s)
        {
            //Console.WriteLine(s);
            int len = s.Length;
            string newS = "tab rip error";

            for (int i = 0; i < len; i++)
            {
                if (!char.IsWhiteSpace(s[i]))
                {
                    if (i == 0) i = len;
                    else
                    {
                        newS = s.Substring(i, len - i);
                        //Console.WriteLine(newS);
                        i = len;
                    }
                }
            }

            if (s == newS) newS = s;

            return newS;
        }

        // Compile legacy scripts
        private int compileLegacy()
        {
            //compile Basic scripts into Master Script Book
            while (basic.readline(_L) != "!")
            {
                if (basic.readline(_L) == "break")
                {
                    _L++;
                    L = 0;
                    S++;
                }
                MasterScript[S, L] = basic.readline(_L);
                L++;
                totlines++;
                _L++;

            }

            //compile Emi's Scripts into Master Script Book
            _L = 0;
            L = 0;
            S++;
            while (emi.readline(_L) != "!")
            {
                if (emi.readline(_L) == "break")
                {
                    _L++;
                    L = 0;
                    S++;
                }

                MasterScript[S, L] = emi.readline(_L);
                L++;
                totlines++;
                _L++;
            }

            //compile Minor's Scripts into Master Script Book
            _L = 0;
            L = 0;
            S++;
            while (minor.readline(_L) != "!")
            {
                if (minor.readline(_L) == "break")
                {
                    _L++;
                    L = 0;
                    S++;
                }

                MasterScript[S, L] = minor.readline(_L);
                L++;
                totlines++;
                _L++;
            }

            return 0;
        }

    }
}
