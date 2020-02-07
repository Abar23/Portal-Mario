using MarioGame.Controllers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.StateMachines.GameStates
{
    abstract class GameState
    {
        private IController<Keys> keyboardController;
        private IController<Buttons> gamePadController;

        private MarioGame game;

        protected MarioGame Game
        {
            get => this.game;
        }

        protected IController<Keys> KeyboardController
        {
            get
            {
                return this.keyboardController;
            }
        }

        protected IController<Buttons> GamepadController
        {
            get
            {
                return this.gamePadController;
            }
        }

        protected GameState(MarioGame game, IController<Keys> keys, IController<Buttons> gamePad)
        {
            this.game = game;
            this.keyboardController = keys;
            this.gamePadController = gamePad;
        }

        public bool ShouldUpdate { get; internal set; }

        public abstract void Draw(SpriteBatch batch);

        public abstract void Dispose();

        public abstract void PreSetup();

        public abstract void SetupControls();
    }
}
