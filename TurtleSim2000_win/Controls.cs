using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleSim2000_Linux
{
    class Controls
    {
        public bool bClicked = false;
        public bool bClicking = false;
        public bool bGamePad = false;

        public struct Dpad
        {
            public bool up;
            public bool upHold;
            public int upHoldCount;
            public bool down;
            public bool downHold;
            public int downHoldCount;
            public bool left;
            public bool leftHold;
            public int leftHoldCount;
            public bool right;
            public bool rightHold;
            public int rightHoldCount;
        }

        public Dpad dpad;

        public Point MousePos = new Point(1, 1);
        Point oldMousePos = new Point(1, 1);

        Matrix ScreenMatrix;


        public Controls()
        {
        }



        public void Update(bool activeWindow, Matrix screenMatrix)
        {
            // Disable bclicked
            bClicked = false;
            
            // set matrix
            ScreenMatrix = screenMatrix;

            // Update Mouse/Keyboard/Gamepad.
            if (activeWindow) updateMouseKeyboard();
            //if (bGamePad) updateGamepad();

            // See if there is a gamepad connected.
            //if (GamePad.GetState(PlayerIndex.One).IsConnected) bGamePad = true;
            //else bGamePad = false;
        }




        private void updateMouseKeyboard()
        {
            // Get Mouse and keyboard states.
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();
            

            // Get where the mouse is on screen X,Y point.
            MousePos = new Point(mouseState.X, mouseState.Y);

            // if Mouse moves; turn off gamepad
            if (MousePos != oldMousePos)
            {
                bGamePad = false;
            }
            oldMousePos = MousePos;

            // Adjust mouse pos to virtual screen
            Vector2 scaledMouse;
            scaledMouse.X = MousePos.X;
            scaledMouse.Y = MousePos.Y;

            scaledMouse.X /= ScreenMatrix.Scale.X;
            scaledMouse.Y /= ScreenMatrix.Scale.Y;

            MousePos.X = Convert.ToInt32(scaledMouse.X);
            MousePos.Y = Convert.ToInt32(scaledMouse.Y);

            // Logic to see if the player clicked.
            if (mouseState.LeftButton == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Space)) bClicking = true;
            if (mouseState.LeftButton == ButtonState.Released && keyboardState.IsKeyUp(Keys.Space))
            {
                if (bClicking == true)
                {
                    bGamePad = false;       // Turn off gamepad use and for drawing non-gamepad icons.
                    bClicked = true;
                    bClicking = false;
                }
            }

            // Logic for skip button.
            if (keyboardState.IsKeyDown(Keys.LeftControl))
            {
                bClicked = true;
            }


            // Keyboard logic
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                bGamePad = true;
                dpad.upHold = true;
                dpad.upHoldCount++;
                if (dpad.upHoldCount == 2) dpad.up = true;
                else dpad.up = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Up) && dpad.upHold == true)
            {
                dpad.upHold = false;
                dpad.upHoldCount = 0;
                dpad.up = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                bGamePad = true;
                dpad.downHold = true;
                dpad.downHoldCount++;
                if (dpad.downHoldCount == 2) dpad.down = true;
                else dpad.down = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Down) && dpad.downHold == true)
            {
                dpad.downHold = false;
                dpad.downHoldCount = 0;
                dpad.down = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                bGamePad = true;
                dpad.leftHold = true;
                dpad.leftHoldCount++;
                if (dpad.leftHoldCount == 2) dpad.left = true;
                else dpad.left = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Left) && dpad.leftHold == true)
            {
                dpad.leftHold = false;
                dpad.leftHoldCount = 0;
                dpad.left = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                bGamePad = true;
                dpad.rightHold = true;
                dpad.rightHoldCount++;
                if (dpad.rightHoldCount == 2) dpad.right = true;
                else dpad.right = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right) && dpad.rightHold == true)
            {
                dpad.rightHold = false;
                dpad.rightHoldCount = 0;
                dpad.right = false;
            }

        }




        private void updateGamepad()
        {
            //for pushing A to click
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) bClicking = true;
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Released & bClicking == true)
            {
                bGamePad = true;        // Turn of gamepad use and for drawing gamepad icons.
                bClicked = true;
                bClicking = false;
            }

            //D-pad controls
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed) dpad.downHold = true;
            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Released & dpad.downHold == true)
            {
                dpad.down = true;
                dpad.downHold = false;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed) dpad.upHold = true;
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Released & dpad.upHold == true)
            {
                dpad.up = true;
                dpad.upHold = false;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed) dpad.leftHold = true;
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Released & dpad.leftHold == true)
            {
                dpad.left = true;
                dpad.leftHold = false;
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed) dpad.rightHold = true;
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Released & dpad.rightHold == true)
            {
                dpad.right = true;
                dpad.rightHold = false;
            }

        }

    }
}
