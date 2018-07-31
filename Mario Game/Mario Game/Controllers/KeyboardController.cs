using MarioGame.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MarioGame.Controllers
{
    internal class KeyboardController : IController<Keys>
    {
        private KeyboardState previousState;

        /// <summary>
        /// Defines the dictionary of keys with their corresponding commands
        /// </summary>
        private IDictionary<Keys, ICommand> pressInputCommands;
        private IDictionary<Keys, ICommand> holdInputCommands;

        /// <summary>
        /// Constructs a keyboard controller
        /// </summary>
        public KeyboardController()
        {
            this.previousState = Keyboard.GetState();
            this.pressInputCommands = new Dictionary<Keys, ICommand>();
            this.holdInputCommands = new Dictionary<Keys, ICommand>();
        }

        public void SetHoldInputCommands(IDictionary<Keys, ICommand> inputs) => this.holdInputCommands = inputs;

        public void SetPressInputCommands(IDictionary<Keys, ICommand> inputs) => this.pressInputCommands = inputs;

        /// <summary>
        /// Update the current input of the keyboard
        /// </summary>
        public void UpdateInput()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();

            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();
            foreach (Keys key in keysPressed)
            {
                if (!previousState.IsKeyDown(key) && this.pressInputCommands.ContainsKey(key))
                {
                    this.pressInputCommands[key].Execute();
                } else if (this.holdInputCommands.ContainsKey(key))
                {
                    this.holdInputCommands[key].Execute();
                }
            }

            previousState = currentKeyboardState;
        }
    }
}
