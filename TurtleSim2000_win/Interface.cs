using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Design;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;


namespace TurtleSim2000_Linux
{
    class Interface : Microsoft.Xna.Framework.Game
    {

        //STORE GUI TEXTURES
        Texture2D pgbar_filler_left;
        Texture2D pgbar_filler_right;
        Texture2D pgbar_filler_center;
        Texture2D pgbar_right;
        Texture2D pgbar_left;
        Texture2D pgbar_center;
        Texture2D buttonup;
        Texture2D homeworkAlert;
        Texture2D action_menu;

        //FONTS
        SpriteFont debugfont;
        SpriteFont debugfontsmall;
        SpriteFont speechfont;

        //SPRITEBATCH
        SpriteBatch spriteBatch;

        //THIS FUNCTION INITIALIZES THE WHOLE CLASS!  
        //THIS SETS UP SPRITEBATCH AND FONTS!
        public int LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            // grab spritebatch from ref
            spriteBatch = sprBatch;

            // LOAD -> Progress Bar
            this.pgbar_center = Content.Load<Texture2D>("assets/gui/pgbar_center_empty");
            this.pgbar_left = Content.Load<Texture2D>("assets/gui/pgbar_left_empty");
            this.pgbar_right = Content.Load<Texture2D>("assets/gui/pgbar_right_empty");
            this.pgbar_filler_center = Content.Load<Texture2D>("assets/gui/pgbar_fill_center_empty");
            this.pgbar_filler_left = Content.Load<Texture2D>("assets/gui/pgbar_fill_left_empty");
            this.pgbar_filler_right = Content.Load<Texture2D>("assets/gui/pgbar_fill_right_empty");

            // LOAD -> Fonts
            this.debugfont = Content.Load<SpriteFont>("fonts/debugfont");
            this.debugfontsmall = Content.Load<SpriteFont>("fonts/debugfontsmall");
            this.speechfont = Content.Load<SpriteFont>("fonts/speechfont");
            // clockfont = Content.Load<SpriteFont>("fonts/clockfont");

            // LOAD -> Windows and Buttons
            action_menu = Content.Load<Texture2D>("assets/gui/notebook/notebookAsset");
            homeworkAlert = Content.Load<Texture2D>("assets/gui/gui_homeworkelert");
            buttonup = Content.Load<Texture2D>("assets/gui/gui_button_up");

            return 0;
        }

        // Interface Update method.  (Logic goes here)
        public int Update()
        {


            return 0;
        }

        // ---------------------------------------------------------------------------------------------------------------------------
        // --------------------------------------------        PRIMITIVES           --------------------------------------------------
        // ---------------------------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------         WINDOWS             --------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------

        //FORK QUESTION WINDOW
        public int ForkQuestionShow(string[] answers)
        {
            if (action_menu == null) return -1;
            int i = 2;

            spriteBatch.Draw(buttonup, new Rectangle(200, 140, 400, 40), Color.White);
            spriteBatch.DrawString(speechfont, answers[0], new Vector2(210, 145), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(200, 200, 400, 40), Color.White);
            spriteBatch.DrawString(speechfont, answers[1], new Vector2(210, 205), Color.White);

            // loop the rest.
            int y = 200;
            while (answers[i] != null)
            {
                y += 60;
                spriteBatch.Draw(buttonup, new Rectangle(200, y, 400, 40), Color.White);
                spriteBatch.DrawString(speechfont, answers[i], new Vector2(210, y + 5), Color.White);
                i++;
            }
            return 0;
        }

        //CLASS SCHEDULE WINDOW
        public int ClassWindowShow(int Day, string weekday, string s_class, int ClassAttend)
        {
            Color clrClass = Color.Blue;
            if (action_menu == null) return -1;
            if (ClassAttend == 0) clrClass = Color.Blue;
            if (ClassAttend == 1) clrClass = Color.Green;
            if (ClassAttend == 2) clrClass = Color.Red;
            spriteBatch.Draw(action_menu, new Rectangle(500, 340, 400, 400), Color.White);
            spriteBatch.DrawString(debugfont, weekday + "      Day: " + Day, new Vector2(520, 360), Color.Blue);
            spriteBatch.DrawString(speechfont, "Schedule: \n   " + s_class, new Vector2(520, 395), clrClass);
            return 0;
        }

        //PROGRESS BAR  SHOWS A BAR BASED ON X VALUE
        public int ProBarShow(int pbarx, int pbary, int V, string name)
        {
            if (V >= 10)
                {
                    spriteBatch.Draw(pgbar_filler_left, new Rectangle(pbarx, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_left, new Rectangle(pbarx, pbary, 10, 20), Color.White);
                }
                if (V >= 20)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 10, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 10, pbary, 10, 20), Color.White);
                }
                if (V >= 30)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 20, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 20, pbary, 10, 20), Color.White);
                }
                if (V >= 40)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 30, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 30, pbary, 10, 20), Color.White);
                }
                if (V >= 50)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 40, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 40, pbary, 10, 20), Color.White);
                }
                if (V >= 60)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 50, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 50, pbary, 10, 20), Color.White);
                }
                if (V >= 70)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 60, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 60, pbary, 10, 20), Color.White);
                }
                if (V >= 80)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 70, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 70, pbary, 10, 20), Color.White);
                }
                if (V >= 90)
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 80, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_center, new Rectangle(pbarx + 80, pbary, 10, 20), Color.White);
                }
                if (V >= 95)
                {
                    spriteBatch.Draw(pgbar_filler_right, new Rectangle(pbarx + 90, pbary, 10, 20), Color.Green);
                }
                else
                {
                    spriteBatch.Draw(pgbar_filler_right, new Rectangle(pbarx + 90, pbary, 10, 20), Color.White);
                }

                spriteBatch.Draw(pgbar_left, new Rectangle(pbarx, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 10, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 20, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 30, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 40, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 50, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 60, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 70, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_center, new Rectangle(pbarx + 80, pbary, 10, 20), Color.White);
                spriteBatch.Draw(pgbar_right, new Rectangle(pbarx + 90, pbary, 10, 20), Color.White);

                spriteBatch.DrawString(debugfont, V + "%", new Vector2(pbarx + 35, pbary - 3), Color.White);
                spriteBatch.DrawString(debugfont, name, new Vector2(pbarx + 103, pbary - 4), Color.White);

                return 0;
        }

        //THE MAIN ACTION MENU  (THIS IS WHERE ALL THE ACTIONS ARE SHOWN)
        public int ActionMenuShow(int actionmenuscroller, int HomeworkAmount, VariableControl VC)
        {

            Color classC = Color.White;
            Color clrEat = Color.White;

            if (VC.GetTime() <= 1700 & VC.GetTime() >= 800 && VC.GetDayOfWeek() == 1 || VC.GetDayOfWeek() == 3 || VC.GetDayOfWeek() == 5) classC = Color.White;
            else classC = Color.Gray;

            if (VC.GetTime() >= 2100 || VC.GetTime() <= 400) clrEat = Color.Gray;
            else clrEat = Color.White;

            //Action Menu
            spriteBatch.Draw(action_menu, new Rectangle(actionmenuscroller, 120, 300, 400), Color.White);
            spriteBatch.DrawString(debugfont, "Action Select Menu", new Vector2(52 + actionmenuscroller, 160), Color.Black);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 220, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 220, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 260, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 260, 120, 30), clrEat);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 300, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 300, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 340, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 340, 120, 30), classC);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 380, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 380, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(30 + actionmenuscroller, 420, 120, 30), Color.White);
            spriteBatch.Draw(buttonup, new Rectangle(160 + actionmenuscroller, 420, 120, 30), Color.White);
            if (HomeworkAmount >= 1) spriteBatch.Draw(homeworkAlert, new Rectangle(268 + actionmenuscroller, 296, 24, 24), Color.White);

            //Text on buttons
            spriteBatch.DrawString(debugfont, "Sleep", new Vector2(60 + actionmenuscroller, 221), Color.White);
            spriteBatch.DrawString(debugfont, "Text", new Vector2(195 + actionmenuscroller, 221), Color.White);
            spriteBatch.DrawString(debugfont, "TV", new Vector2(78 + actionmenuscroller, 261), Color.White);
            spriteBatch.DrawString(debugfont, "Go Eat", new Vector2(188 + actionmenuscroller, 261), clrEat);
            spriteBatch.DrawString(debugfont, "Xbox", new Vector2(70 + actionmenuscroller, 301), Color.White);
            spriteBatch.DrawString(debugfont, "Homework", new Vector2(175 + actionmenuscroller, 301), Color.White);
            if (HomeworkAmount >= 1) spriteBatch.DrawString(debugfontsmall, "" + HomeworkAmount, new Vector2(278 + actionmenuscroller, 301), Color.White);
            spriteBatch.DrawString(debugfont, "Write", new Vector2(62 + actionmenuscroller, 341), Color.White);
            spriteBatch.DrawString(debugfont, "Class", new Vector2(192 + actionmenuscroller, 341), classC);
            spriteBatch.DrawString(debugfont, "Music", new Vector2(63 + actionmenuscroller, 381), Color.White);
            spriteBatch.DrawString(debugfont, "Porn Time", new Vector2(170 + actionmenuscroller, 381), Color.White);
            spriteBatch.DrawString(debugfont, "Walk", new Vector2(70 + actionmenuscroller, 421), Color.White);
            spriteBatch.DrawString(debugfont, "Save Game", new Vector2(170 + actionmenuscroller, 421), Color.White);

            /*
            if (bMenu == true & actionmenuscroller >= -20 & bGamePad == true)
            {
                spriteBatch.Draw(buttonselector, new Rectangle(SelectorPosX + actionmenuscroller, SelectorPosY, 120, 30), Color.White);
            }
            */
            return 0;
        }

    }
}
