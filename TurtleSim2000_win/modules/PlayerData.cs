﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurtleSim2000_Linux
{
    class PlayerData
    {
        // Player Name!
        public string Name = "Hush";

        // Player Class schedule
        public struct ClassSchedule
        {
            public string monday;
            public string tuesday;
            public string wednesday;
            public string thursday;
            public string friday;
            public string currentClass;
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
            public int Turns;
            public int Hour;
            public int Minute;
            public int DayOfWeek;
            public int Day;
            public string weekDay;
        }
        public _Time Time;




        public void Update()
        {
            updateClass();
            updateWeekDay();
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

        //Used to add time safely
        public int addTime(int minute)
        {

            return 0;
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

    }
}