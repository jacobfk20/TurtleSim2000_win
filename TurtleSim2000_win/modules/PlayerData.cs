using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurtleSim2000_Linux
{
    class PlayerData
    {
        // Player Name!
        public string Name = "Hush";

        // Save/Load Object
        Save gLoad;

        // Player Class schedule
        public struct ClassSchedule
        {
            public string monday;
            public string tuesday;
            public string wednesday;
            public string thursday;
            public string friday;
            public string currentClass;
            public int totalHomework;
            public int homeworkOfWeek;
        }
        public ClassSchedule Schedule;

        // Game Switches and Variables:
        public bool[] GameSwitches = new bool[500];//
        public int[] GameVariables = new int[500];//

        // Player's state:
        public struct PlayerState
        {
            public bool bDorm;//
            public bool bMenu;//
            public int HP;
            public int Energy;
            public int Social;
            public int Fat;
            public bool bGameOver;//
        }
        public PlayerState State;

        public struct _Time
        {
            public int FullTime;
            public int Turns;
            public int Hour;
            public int Minute;
            public int DayOfWeek;
            public int Day;
            public bool bPM;
            public string weekDay;
        }
        public _Time Time;




        public void Update()
        {
            updateGameVariables();          // Takes Player.State ints and puts them in GameVariables
            updateClass();                  // Updates what class the player is in today (currentday)
            updateWeekDay();                // Updates what week day (printed) it is.
            //updateFullTime();               // Updates the 24 hour time from Clock.
        }




        /// <summary>
        /// Creates the player's classes randomly. (rng = random number generator, numberOfClasses = duh.)
        /// </summary>
        public void CreateClasses(Random rng, int numberOfClasses = 3)
        {
            // Set a limit of classes to 4.
            if (numberOfClasses > 4) numberOfClasses = 4;

            // Randomly create the class
            string classname = "wut?";
            for (int i = 0; i < numberOfClasses; i++)
            {
                string[] a = { "Advanced ", "Beginer ", "Face ", "Psycho-", "Jar ", "Pizza ", "Mexican ", "Future ", "Nature ", "Couch ", "Baby ", "\"Fuck it\" " };
                string[] b = { "Hugging", "crafting", "weaving", "Economics", "Managment", "Shoe-tieing", "Aerodynamics", "Loving", "Social Services", "Film History", "Programming", "Communication" };
                string[] c = { " Theory", " 101", "", "", "" };

                classname = a[rng.Next(11)] + b[rng.Next(11)] + c[rng.Next(5)];

                // Assign it to any day besides a day that is alredy filled
                if (Schedule.monday == null) Schedule.monday = classname;
                if (Schedule.wednesday == null) Schedule.wednesday = classname;
                if (Schedule.friday == null) Schedule.friday = classname;
                if (Schedule.thursday == null) Schedule.thursday = classname;
            }




        }


        public void loadFromSave()
        {
            // Load from save
            gLoad.loadFromFile();

            // Take public variables from it and put 'em in here!
            GameVariables = gLoad.sD.gVariables;
            GameSwitches = gLoad.sD.gSwitches;

            // Take GameVariables and put them into Player.State and Player.Time
            Time.Day = GameVariables[455];
            Time.Hour = GameVariables[452];
            Time.Minute = GameVariables[453];
            Time.DayOfWeek = GameVariables[454];
            State.HP = GameVariables[486];
            State.Energy = GameVariables[485];
            State.Social = GameVariables[487];
            State.Fat = GameVariables[488];

            
        }




        #region Add Player Stats (Energy, HP, Social, Fat, Time)
        public int addEnergy(int v)
        {
            if (State.Energy + v > 100)
            {
                State.Energy = 100;
            }
            else State.Energy += v;

            if (State.Energy > 120) State.bGameOver = true;
            if (State.Energy <= 0) State.bGameOver = true;
            return State.Energy;
        }

        public int addHp(int v)
        {
            if (State.HP + v > 100)
            {
                State.HP = 100;
            }
            else State.HP += v;

            if (State.HP <= 0) State.bGameOver = true;
            if (State.HP >= 100) State.bGameOver = true;
            return State.HP;
        }

        public int addSocial(int v)
        {

            if (State.Social + v > 100)
            {
                State.Social = 100;
            }
            else State.Social += v;

            if (State.Social > 99) return -1;
            if (State.Social <= 0) return -2;
            return State.Social;

        }

        public int addFat(int v)
        {
            if (State.Fat + v > 100)
            {
                State.Fat = 100;
            }
            else State.Fat += v;

            if (State.Fat > 99) State.bGameOver = true;
            return State.Fat;
        }

        /// <summary>
        /// Adds time safely to the clock.  This function adds via single int.  (30 = 30 minutes) (120 = 120 minutes [2 hours])
        /// </summary>
        public void addTime(int fulltime)
        {
            // Check and make sure the time the user adds is less than an hour and deal with it now.
            if (fulltime < 60)
            {
                // Make sure Minutes wont go over 60.
                if (Time.Minute + fulltime > 59)
                {
                    Time.Hour++;
                    Time.Minute = Time.Minute + fulltime - 60;
                }
                else Time.Minute += fulltime;
                return;
            }

            // I HATE DICKING WITH TIME!!  Here will add the full time into Hours and Minutes
            while (fulltime != 0)
            {
                if (fulltime >= 60)
                {
                    // Does all the shit to add an hour.
                    fulltime = addHour(fulltime);
                }
                else
                {
                    // Adds in the remaining minutes and checks if needs extra hour.
                    if (Time.Minute + fulltime > 59)
                    {
                        fulltime = addHour(fulltime);

                        Time.Minute = Time.Minute + fulltime - 60;
                        fulltime = 0;
                    }
                    else
                    {
                        // Adds in the reamaining minutes
                        Time.Minute += fulltime;
                        fulltime = 0;
                    }
                }
            }

            
        }

        /// <summary>
        ///  Set time from clock.  Don't use this method to add time!  Player Only needs reference to time.
        /// </summary>
        public void setTime(int fulltime)
        {
            Time.FullTime = fulltime;
        }

        /// <summary>
        /// Updates the gameTime in Player Data.  Get time from Clock object
        /// </summary>
        public void setTime(int hour, int minute, bool bPm)
        {
            Time.Hour = hour;
            Time.Minute = minute;
            Time.bPM = bPm;
        }


        #endregion






        // Updates what class is happening today.
        private void updateClass()
        {
            if (Time.DayOfWeek == 1) Schedule.currentClass = Schedule.monday;
            if (Time.DayOfWeek == 2) Schedule.currentClass = Schedule.tuesday;
            if (Time.DayOfWeek == 3) Schedule.currentClass = Schedule.wednesday;
            if (Time.DayOfWeek == 4) Schedule.currentClass = Schedule.thursday;
            if (Time.DayOfWeek == 5) Schedule.currentClass = Schedule.friday;
            if (Time.DayOfWeek == 6) Schedule.currentClass = "";

            // update homework
            if (Time.FullTime >= 1500)
            {
                // For Monday
                if (Time.DayOfWeek == 1 && Schedule.homeworkOfWeek == 0)        // Checks if day is Monday, and if the player hasn't recieved homework yet.
                {
                    Schedule.totalHomework++;
                    Schedule.homeworkOfWeek++;
                }

                // For Tuesday (Major class)
                if (Time.DayOfWeek == 2 && Schedule.homeworkOfWeek == 1)
                {
                    Schedule.totalHomework++;
                    Schedule.homeworkOfWeek++;
                }

                // For Wednesday
                if (Time.DayOfWeek == 3 && Schedule.homeworkOfWeek == 2)
                {
                    Schedule.totalHomework++;
                    Schedule.homeworkOfWeek++;
                }

                // For Thursday (not usually enrolled in a class on thursday.)
                if (Time.DayOfWeek == 4 && Schedule.homeworkOfWeek == 3)
                {
                    Schedule.totalHomework++;
                    Schedule.homeworkOfWeek++;
                }

                // For Friday
                if (Time.DayOfWeek == 5 && Schedule.homeworkOfWeek == 4)
                {
                    Schedule.totalHomework++;
                    Schedule.homeworkOfWeek++;
                    Schedule.homeworkOfWeek = 0;
                }
            }
        }

        // updates what day it is.
        private void updateWeekDay()
        {
            if (Time.DayOfWeek == 1) Time.weekDay = "Monday";
            if (Time.DayOfWeek == 2) Time.weekDay = "Tuesday";
            if (Time.DayOfWeek == 3) Time.weekDay = "Wednesday";
            if (Time.DayOfWeek == 4) Time.weekDay = "Thursday";
            if (Time.DayOfWeek == 5) Time.weekDay = "Friday";
            if (Time.DayOfWeek == 6) Time.weekDay = "Saturday";
            if (Time.DayOfWeek == 7) Time.weekDay = "Sunday";
        }

        // Update player data into game variables for script reference.
        private void updateGameVariables()
        {
            GameVariables[452] = Time.Hour;
            GameVariables[453] = Time.Minute;
            GameVariables[454] = Time.DayOfWeek;
            GameVariables[455] = Time.Day;
            GameVariables[485] = State.Energy;
            GameVariables[486] = State.HP;
            GameVariables[487] = State.Social;
            GameVariables[488] = State.Fat;
        }

        // Updates the 24 hour clock from 12 hour clock.  (used easier for some events)
        private void updateFullTime()
        {
            Time.FullTime = Time.Hour + Time.Minute;
            if (Time.bPM) Time.FullTime += 1200;
        }

        // Safely adds an hour.
        private int addHour(int fulltime)
        {
            // Adding hours
            if (Time.Hour >= 12)
            {
                Time.Hour = 1;
                if (Time.bPM)
                {
                    Time.bPM = false;
                    addDay();
                }
                else Time.bPM = true;
                fulltime -= 60;
            }
            else
            {
                Time.Hour++;
                fulltime -= 60;
            }
            return fulltime;
        }

        private void addDay()
        {
            Time.Day++;
            Time.DayOfWeek++;

            if (Time.DayOfWeek > 7)
            {
                Time.DayOfWeek = 1;
            }
        }
    }
}
