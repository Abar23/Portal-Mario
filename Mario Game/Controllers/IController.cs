using MarioGame.Commands;
using System;
using System.Collections.Generic;

namespace MarioGame.Controllers
{
    public interface IController<T>
    {
        /// <summary>
        /// Add a new command with a given controller input
        /// </summary>
        /// <param name="input">The input of the controller</param>
        /// <param name="command">The command associated with the input</param>
        void SetHoldInputCommands(IDictionary<T, ICommand> inputs);

        /// <summary>
        /// Add a new command with a given controller input
        /// </summary>
        /// <param name="input">The input of the controller</param>
        /// <param name="command">The command associated with the input</param>
        void SetPressInputCommands(IDictionary<T, ICommand> inputs);

        /// <summary>
        /// Update the current input of the controller
        /// </summary>
        void UpdateInput();
    }
}
