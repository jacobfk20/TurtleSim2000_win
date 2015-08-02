using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleSim2000_Linux
{
    class VariableControl
    {
        Texture2D clock_tex;
        SpriteFont clockfont;

        //TODO WITH CLOCK AND TIME
        int Time = 500;
        int Day;
        int DayofWeek = 0;
        int TimeToAdd = 0;
        int FakeTime = 0;
        bool bAddtime = false;
        string weekday = "Monday";

        //TODO WITH PLAYER VARIABLES
        int Energy;
        int HP;
        int social;
        int Fat;

        bool bGameOver = false;

        public string CreateClass(Random r)
        {
            string classname;
            string[] a = { "Advanced ", "Beginer ", "Face ", "Psycho-", "Jar ", "Pizza ", "Mexican ", "Future ", "Nature", "Couch", "Baby", "\"Fuck it\"" };
            string[] b = { "Hugging", "crafting", "weaving", "Economics", "Managment", "Shoe-tieing", "Aerodynamics", "Loving", "Social Services", "Film History", "Programming", "Communication"};
            string[] c = { " Theory", " 101", "", "", "" };
            
            classname = a[r.Next(11)] + b[r.Next(11)] + c[r.Next(5)];

            return classname;
        }

        //INITs the Variable Controls Functions.  Specify textures and fonts here.
        public int Init(Texture2D clock_Texture, SpriteFont ClockFont)
        {
            clock_tex = clock_Texture;
            clockfont = ClockFont;
            return 0;
        }

        //UPDATE FUNCTION to get variable changes from the main.  USE REFERENCE!
        public int GameStateUpdate(ref bool bGameover)
        {
            bGameover = bGameOver;
            return 0;
        }

        //Used to add time safely
        public int addtime(int v)
        {
    
            int oldt;
            if (v >= 1)
            {
                if (Time + v > 2359)
                {
                    oldt = Time + v - 2400;
                    Time = oldt;
                    Day++;
                    if (DayofWeek >= 7) DayofWeek = 1;
                    else DayofWeek++;
                }
                else
                {
                    TimeToAdd += v;
                    FakeTime += v + Time;
                    bAddtime = true;
                }


                if (DayofWeek == 1) weekday = "Monday";
                if (DayofWeek == 2) weekday = "Tuesday";
                if (DayofWeek == 3) weekday = "Wednesday";
                if (DayofWeek == 4) weekday = "Thursday";
                if (DayofWeek == 5) weekday = "Friday";
                if (DayofWeek == 6) weekday = "Saturday";
                if (DayofWeek == 7) weekday = "Sunday";
            }
            if (v == -1)
            {
                if (TimeToAdd <= 0)
                {
                    TimeToAdd = 0;
                    FakeTime = 0;
                    bAddtime = false;
                }
                else
                {
                    Time += 10;
                    TimeToAdd -= 10;
                }

            }
            return 0;
        }

        //formats time so it is readable and draws it
        public int Clock(SpriteBatch spriteBatch)
        {

            int nTime;
            bool bPM = false;

            if (Time >= 1300)
            {
                nTime = Time - 1200;
                bPM = true;
            }
            else
            {
                bPM = false;
                nTime = Time;
            }

            if (nTime == 0) nTime = 1200;

            if (bAddtime == true) addtime(-1);

            spriteBatch.Draw(clock_tex, new Rectangle(650, 5, 150, 50), Color.White);

            if (bPM == true) spriteBatch.DrawString(clockfont, nTime + " PM", new Vector2(666, 12), Color.Red);
            else
                spriteBatch.DrawString(clockfont, nTime + "  AM", new Vector2(666, 12), Color.Red);
            return 0;

        }

        public void setValuesFromLoad(int energy, int hp, int _social, int fat, int time, int day, int dayofweek, string week_day)
        {
            Energy = energy;
            HP = hp;
            social = _social;
            Fat = fat;
            Time = time;
            Day = day;
            DayofWeek = dayofweek;
            weekday = week_day;
        }



        //ADD Functions.  These Safely add value to their respected variable.
        public int addenergy(int v)
        {
            if (Energy + v > 100)
            {
                Energy = 100;
            }
            else Energy += v;

            if (Energy > 120) bGameOver = true;
            if (Energy <= 0) bGameOver = true;
            return Energy;
        }

        public int addhp(int v)
        {
            if (HP + v > 100)
            {
                HP = 100;
            }
            else HP += v;

            if (HP <= 0) bGameOver = true;
            if (HP >= 100) bGameOver = true;
            return HP;
        }

        public int addsocial(int v)
        {

            if (social + v > 100)
            {
                social = 100;
            }
            else social += v;

            if (social > 99) return -1;
            if (social <= 0) return -2;
            return social;

        }

        public int addfat(int v)
        {
            if (Fat + v > 100)
            {
                Fat = 100;
            }
            else Fat += v;

            if (Fat > 99) bGameOver = true;
            return Fat;
        }



        //GET FUNCTIONS.  JUST RETURNS THE VARIABLE WHEN NEEDED.
        public int GetFat()
        {
            return Fat;
        }

        public int GetTime()
        {
            return Time;
        }

        public int GetSocial()
        {
            return social;
        }

        public int GetEnergy()
        {
            return Energy;
        }

        public int GetHP()
        {
            return HP;
        }

        public int GetDay()
        {
            return Day;
        }

        public int GetDayOfWeek()
        {
            return DayofWeek;
        }

        public string GetWeekDay()
        {
            return weekday;
        }

    }
}
