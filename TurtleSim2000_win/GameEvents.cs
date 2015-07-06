using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurtleSim2000_Linux
{
    class GameEvents
    {
        Random Rando = new Random(494936921);

        // All Events for Walking
        public string WalkingEvents(VariableControl VC, ref int[] gVariables, ref bool[] gSwitches)
        {
            string eventname = "0";
            
            #region EMI Events
            // Time Emi is set to be running. (6 - 9 AM) OR (3 - 6 PM)
            if (VC.GetTime() >= 600 & VC.GetTime() <= 900 || VC.GetTime() >= 1500 & VC.GetTime() <= 1800)
            {
                // FIRST MEET WITH EMI
                if (!gSwitches[3])           //if you have already met her, you cannot get it again.
                {
                    gVariables[10] = VC.GetDay();
                    eventname = "walk_meetemi";
                    VC.addsocial(1);
                    VC.addtime(100);
                    VC.addhp(-2);
                    VC.addfat(-1);
                    gSwitches[3] = true;
                }

                // If you went for a walk, met Emi for first time AND ran away. 
                if (gSwitches[5] == true & gSwitches[4] == false && gSwitches[1] == false)
                {
                    eventname = "walk_meetemi_2";
                    VC.addtime(100);
                }

                // Standard Walk (AFTER ACCEPTING OVER PHONE)
                if (gSwitches[1])
                {
                    eventname = "emi_walk_basic";
                }
            }
            #endregion // All events for EMI

            return eventname;
        }

        // All Events for Eating
        public string Eat(VariableControl VC, ref int[] gVariables, ref bool[] gSwitches)
        {
            string eventname = "0";

            #region Emi Events
            // Eat with Emi for first time
            if (gSwitches[3] & gSwitches[4] == false || gSwitches[5] & gSwitches[4] == false)
            {
                if (gVariables[10] + 1 <= VC.GetDay())
                {
                    eventname = "eat_emi";
                    VC.addsocial(1);
                    VC.addtime(100);
                    gSwitches[4] = true;
                }
            }
            #endregion

            return eventname;
        }

        // All Events for going to class
        public string School(VariableControl VC, ref int[] gVariables, ref bool[] gSwitches)
        {
            string eventname = "0";

            #region Aria Events
            // Going to class for the first time
            if (gSwitches[6] == false)
            {
                eventname = "aria_MeetFirstTime";
                gSwitches[6] = true;
            }
            #endregion

            return eventname;
        }

    }
}
