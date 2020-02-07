using MarioGame.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using MarioGame.View;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MarioGame.Controllers
{
    public class MouseController
    {
        private static MouseController instance;

        private MouseState previousState;

        private ICommand leftCommand;
        private ICommand rightCommand;
        
        private MouseController()
        {
            this.leftCommand = new NullCommand();
            this.rightCommand = new NullCommand();
        }

        public static MouseController GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MouseController();
                }
                return instance;
            }
        }

        public void SetLeftCommand(ICommand command)
        {
            this.leftCommand = command;
        }

        public void SetRightCommand(ICommand command)
        {
            this.rightCommand = command;
        }

        public void ClearCommands()
        {
            this.leftCommand = new NullCommand();
            this.rightCommand = new NullCommand();
        }

        public void UpdateInput()
        {
            MouseState currentState = Mouse.GetState();
            if (currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                leftCommand.Execute();
            }
            if (currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released)
            {
                rightCommand.Execute();
            }
            this.previousState = currentState;
        }

        public static double DetermineAngleFromMouse(Point point)
        {
            double xScale = (MarioGame.GetInstance.PreferredBackBufferWidth - MarioGame.GetInstance.GetScreenXPosition() * 2) / (double)Camera.AquireInstance().VirtualWidth;
            double yScale = (MarioGame.GetInstance.PreferredBackBufferHeight - MarioGame.GetInstance.GetScreenYPosition() * 2) / (double)Camera.AquireInstance().VirtualHeight;

            double angle = Math.Atan2(Mouse.GetState().Y - (point.Y) * yScale - MarioGame.GetInstance.GetScreenYPosition(), Mouse.GetState().X - ((double)point.X - (double)MarioGame.GetInstance.Camera.Position.X) * xScale - MarioGame.GetInstance.GetScreenXPosition());
            return angle;
        }

        public static bool MouseOverPosition(Rectangle hitbox)
        {
            double xScale = (MarioGame.GetInstance.PreferredBackBufferWidth - MarioGame.GetInstance.GetScreenXPosition() * 2) / (double)Camera.AquireInstance().VirtualWidth;
            double yScale = (MarioGame.GetInstance.PreferredBackBufferHeight - MarioGame.GetInstance.GetScreenYPosition() * 2) / (double)Camera.AquireInstance().VirtualHeight;

            return Mouse.GetState().X >= (hitbox.X - MarioGame.GetInstance.Camera.Position.X) * xScale + MarioGame.GetInstance.GetScreenXPosition()
                && Mouse.GetState().X <= (hitbox.X - MarioGame.GetInstance.Camera.Position.X + hitbox.Width) * xScale + MarioGame.GetInstance.GetScreenXPosition()
                && Mouse.GetState().Y >= hitbox.Y * yScale + MarioGame.GetInstance.GetScreenYPosition()
                && Mouse.GetState().Y <= (hitbox.Y + hitbox.Height) * yScale + MarioGame.GetInstance.GetScreenYPosition();
        }
    }
}
