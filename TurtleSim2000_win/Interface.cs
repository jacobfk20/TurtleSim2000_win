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
        Texture2D pBar_backlayer;
        Texture2D pBar_edges;
        Texture2D pBar_progress;

        Texture2D buttonup;
        Texture2D homeworkAlert;
        Texture2D action_menu;
        Texture2D select_arrow;

        //FONTS
        SpriteFont debugfont;
        SpriteFont debugfontsmall;
        SpriteFont speechfont;

        //SPRITEBATCH
        SpriteBatch spriteBatch;

        // ints
        public int hoveredAnswer = 0;
        int animateFrames = 0;
        int fqArrowFrames = 0;

        // bool
        public bool bGamePad = false;

        //THIS FUNCTION INITIALIZES THE WHOLE CLASS!  
        //THIS SETS UP SPRITEBATCH AND FONTS!
        public int LoadContent(ContentManager Content, SpriteBatch sprBatch)
        {
            // grab spritebatch from ref
            spriteBatch = sprBatch;

            // LOAD -> New Progress Bar assets
            this.pBar_backlayer = Content.Load<Texture2D>("assets/gui/bar_backlayer");
            this.pBar_edges = Content.Load<Texture2D>("assets/gui/bar_edges");
            this.pBar_progress = Content.Load<Texture2D>("assets/gui/bar_progress");

            // LOAD -> Fonts
            this.debugfont = Content.Load<SpriteFont>("fonts/debugfont");
            this.debugfontsmall = Content.Load<SpriteFont>("fonts/debugfontsmall");
            this.speechfont = Content.Load<SpriteFont>("fonts/speechfont");
            // clockfont = Content.Load<SpriteFont>("fonts/clockfont");

            // LOAD -> Windows and Buttons
            action_menu = Content.Load<Texture2D>("assets/gui/notebook/notebookAsset");
            homeworkAlert = Content.Load<Texture2D>("assets/gui/gui_homeworkelert");
            buttonup = Content.Load<Texture2D>("assets/gui/gui_button_up");

            // LOAD -> menu stuff
            select_arrow = Content.Load<Texture2D>("assets/gui/Gui_Arrow");

            return 0;
        }

        // Interface Update method.  (Logic goes here)
        public int Update()
        {
            fqArrowFrames++;
            if (fqArrowFrames > 10) fqArrowFrames = 0;

            return 0;
        }




        //----------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------         WINDOWS             --------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------

        //FORK QUESTION WINDOW
        public int ForkQuestionShow(string[] answers, int selectedAnswer = 0)
        {
            if (action_menu == null) return -1;
            int i = 2;

            // common Y coord
            int y = 140;

            // Draw first two question boxes with text
            spriteBatch.Draw(buttonup, new Rectangle(200, y, 400, 40), Color.White);
            spriteBatch.DrawString(speechfont, answers[0], new Vector2(210, 145), Color.White);

            y += 60;

            spriteBatch.Draw(buttonup, new Rectangle(200, y, 400, 40), Color.White);
            spriteBatch.DrawString(speechfont, answers[1], new Vector2(210, 205), Color.White);

            // loop the rest.
            y = 200;
            while (answers[i] != null)
            {
                y += 60;
                spriteBatch.Draw(buttonup, new Rectangle(200, y, 400, 40), Color.White);
                spriteBatch.DrawString(speechfont, answers[i], new Vector2(210, y + 5), Color.White);
                i++;
            }

            // Show the arrow if using controller
            if (bGamePad)
            {
                // get y for all possible answers.
                int[] gY = { 140, 200, 260, 320, 380 };

                // draw arrow outside of the box.
                spriteBatch.Draw(select_arrow, new Rectangle(160, gY[hoveredAnswer] + fqArrowFrames, 32, 32), Color.White);
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

        /// <summary>
        /// Draws a progress bar with given X and Y coords and a Value
        /// </summary>
        /// <param name="pbarx">X coord on screen</param>
        /// <param name="pbary">Y coord on screen</param>
        /// <param name="V">Value to show</param>
        /// <param name="name">What this bar is called. (can be left blank)</param>
        /// <returns></returns>
        public int ProBarShow(int pbarx, int pbary, int V, string name = "")
        {
            // Draw the backlayer first.
            spriteBatch.Draw(pBar_backlayer, new Rectangle(pbarx, pbary, pBar_backlayer.Width +6, pBar_backlayer.Height - 6), Color.White);

            // Draw in the total progress based on V
            int spacer = 0;
            for (int i = 0; i < V; i++)
            {
                spriteBatch.Draw(pBar_progress, new Rectangle(pbarx + 4 + spacer, pbary + 3, pBar_progress.Width, pBar_progress.Height - 6), Color.White);
                spacer += 2;
            }

            // Draw the progress inside the bar.
            spriteBatch.DrawString(debugfont, V + "", new Vector2(pbarx + 85, pbary), Color.White);

            // Draw the edges of the bar
            spriteBatch.Draw(pBar_edges, new Rectangle(pbarx, pbary, pBar_edges.Width + 6, pBar_edges.Height - 6), Color.White);

            // Draw the name of the bar
            spriteBatch.DrawString(debugfont, name, new Vector2(pbarx + 210, pbary), Color.White);

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



        // Private
    }
}
