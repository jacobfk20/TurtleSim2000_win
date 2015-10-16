using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TurtleSim2000_Linux
{
    class Scene_Questions
    {
        /// <summary>
        /// Holds all the button information for a fork question
        /// </summary>
        public Button[] btnList;

        public bool Show = false;

        public bool Active = false;

        int buttonIndex = 1;
        int buttonAmount = 0;

        int currentAnswerAmount = 0;                    // How many answers are available currently

        ContentManager Content;




        /// <summary>
        /// Creates a Question Scene.  Needs content manager.
        /// </summary>
        public Scene_Questions(ContentManager content, int ButtonAmount = 5)
        {
            Content = content;
            buttonAmount = ButtonAmount;

            btnList = new Button[ButtonAmount];

            // Set up all the buttons
            int spacer = 50;
            for (int i = 1; i < ButtonAmount; i++)
            {
                btnList[i] = new Button(content, "0", new Rectangle(200, spacer, 500, 40), i);
                spacer += 50;
            }

        }


        /// <summary>
        ///  Draws the Question Scene to screen.
        /// </summary>
        public void Draw(SpriteBatch sB)
        {
            for (int i = 1; i < currentAnswerAmount; i++)
            {
               btnList[i].Draw(sB);
            }
        }


        public void setAnswers(string[] answers)
        {
            int i = 1;
            while (answers[i - 1] != null)
            {
                btnList[i].setText(answers[i - 1]);
                btnList[i].setTextPosition(-240, 0);
                i++;
            }

            // keep how many answers there are.
            currentAnswerAmount = i;
        }


        public int updateControls(Controls controller)
        {
            // if this scene is active and shown, then update controls
            if (Active && Show)
            {

                // update button controls
                for (int i = 1; i < buttonAmount; i++)
                {
                    btnList[i].UpdateControls(controller.MousePos, controller.bClicked, buttonIndex);
                }

                // update gamepad movement and buttonFocus
                if (controller.bGamePad) updateGamePadSelected(controller);
                else
                {
                    buttonIndex = 0;
                }

                // return selected button
                for (int i = 1; i < buttonAmount; i++)
                {
                    if (btnList[i].bPressed) return i;
                }

            }

            return 0;

        }





        private void updateGamePadSelected(Controls controller)
        {
            if (controller.dpad.up) buttonIndex--;
            if (controller.dpad.down) buttonIndex++;

            // make sure focus doesn't go out of button range
            if (buttonIndex > currentAnswerAmount - 1) buttonIndex = 1;
            if (buttonIndex < 1) buttonIndex = currentAnswerAmount -1;

        }



    }
}
