using MarioGame.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MarioGame.Controllers
{
    internal class GamePadController : IController<Buttons>
    {
        /// <summary>
        /// Defines the previous state of the game pad
        /// </summary>
        private GamePadState previousGamePadState;

        /// <summary>
        /// Defines the dictionary of buttons with their corresponding commands
        /// </summary>
        private IDictionary<Buttons, ICommand> pressInputCommands;
        private IDictionary<Buttons, ICommand> holdInputCommands;

        /// <summary>
        /// Constructs a game pad controller
        /// </summary>
        public GamePadController()
        {
            this.previousGamePadState = GamePad.GetState(PlayerIndex.One);
            this.pressInputCommands = new Dictionary<Buttons, ICommand>();
            this.holdInputCommands = new Dictionary<Buttons, ICommand>();
        }

        public void SetHoldInputCommands(IDictionary<Buttons, ICommand> inputs) => this.holdInputCommands = inputs;

        public void SetPressInputCommands(IDictionary<Buttons, ICommand> inputs) => this.pressInputCommands = inputs;

        /// <summary>
        /// Update the current input of the game pad
        /// </summary>
        public void UpdateInput()
        {
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            List<Buttons> buttonsPressed = GetPressedButtons(currentGamePadState);
            foreach (Buttons button in buttonsPressed)
            {
                if (!previousGamePadState.IsButtonDown(button) && this.pressInputCommands.ContainsKey(button))
                {
                    this.pressInputCommands[button].Execute();
                } else if (this.holdInputCommands.ContainsKey(button))
                {
                    this.holdInputCommands[button].Execute();
                }
            }
            this.previousGamePadState = currentGamePadState;
        }

        /// <summary>
        /// The GetPressedButtons
        /// </summary>
        /// <param name="gamePadState">The <see cref="GamePadState"/></param>
        /// <returns>The <see cref="List{Buttons}"/></returns>
        private static List<Buttons> GetPressedButtons(GamePadState gamePadState)
        {
            List<Buttons> pressedButtons = new List<Buttons>();

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (gamePadState.IsButtonDown(button))
                {
                    pressedButtons.Add(button);
                }
            }

            return pressedButtons;
        }
    }
}
