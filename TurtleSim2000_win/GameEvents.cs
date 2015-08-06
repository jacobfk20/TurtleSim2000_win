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
        public string WalkingEvents(ref PlayerData Player)
        {
            string eventname = "0";
            
            #region EMI Events
            // Time Emi is set to be running. (6 - 9 AM) OR (3 - 6 PM)
            if (Player.Time.FullTime >= 600 & Player.Time.FullTime <= 900 || Player.Time.FullTime >= 1500 & Player.Time.FullTime <= 1800)
            {
                // FIRST MEET WITH EMI
                if (!Player.GameSwitches[3])           //if you have already met her, you cannot get it again.
                {
                    Player.GameVariables[10] = Player.Time.Day;
                    eventname = "walk_meetemi";
                    Player.addSocial(1);
                    //VC.addtime(100);
                    Player.addHp(-2);
                    Player.addFat(-1);
                    Player.GameSwitches[3] = true;
                }

                // If you went for a walk, met Emi for first time AND ran away. 
                if (Player.GameSwitches[5] == true & Player.GameSwitches[4] == false && Player.GameSwitches[1] == false)
                {
                    eventname = "walk_meetemi_2";
                    //VC.addtime(100);
                }

                // Standard Walk (AFTER ACCEPTING OVER PHONE)
                if (Player.GameSwitches[1])
                {
                    eventname = "emi_walk_basic";
                }
            }
            #endregion // All events for EMI

            return eventname;
        }

        // All Events for Eating
        public string Eat(ref PlayerData Player)
        {
            string eventname = "0";

            #region Emi Events
            // Eat with Emi for first time
            if (Player.GameSwitches[3] & Player.GameSwitches[4] == false || Player.GameSwitches[5] & Player.GameSwitches[4] == false)
            {
                if (Player.GameVariables[10] + 1 <= Player.Time.Day)
                {
                    eventname = "eat_emi";
                    Player.addSocial(1);
                    //VC.addtime(100);
                    Player.GameSwitches[4] = true;
                }
            }
            #endregion

            return eventname;
        }

        // All Events for going to class
        public string School(ref PlayerData Player)
        {
            string eventname = "0";

            #region Aria Events
            // Going to class for the first time
            if (Player.GameSwitches[6] == false)
            {
                eventname = "aria_MeetFirstTime";
                Player.GameSwitches[6] = true;
            }
            #endregion

            return eventname;
        }

    }
}
