using System.Collections.Generic;
using MarioGame.Commands;
using MarioGame.Controllers;
using MarioGame.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.StateMachines.GameStates
{
    class GameOverState : GameState, IRestartable
    {
        private GameState previousState;
        private IDictionary<Keys, ICommand> keyboardPresses = new Dictionary<Keys, ICommand>();
        private IDictionary<Buttons, ICommand> gamepadPresses = new Dictionary<Buttons, ICommand>();

        private Texture2D splashTexture;

        public GameOverState(GameState previousState, MarioGame game, IController<Keys> keys, IController<Buttons> gamePad) : base(game, keys, gamePad)
        {
            this.previousState = previousState;
            this.splashTexture = game.Content.Load<Texture2D>("Textures/GameOver");
        }

        public override void SetupControls()
        {
            #region Set Controls
            // Create new controllers and assign game exit commands
            this.keyboardPresses.Add(Keys.Q, new ExitCommand(Game));
            this.keyboardPresses.Add(Keys.R, new RestartCommand(this));
            this.keyboardPresses.Add(Keys.M, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));

            this.gamepadPresses.Add(Buttons.Start, new ExitCommand(Game));
            this.gamepadPresses.Add(Buttons.RightShoulder, new RestartCommand(this));
            this.gamepadPresses.Add(Buttons.LeftShoulder, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));
            #endregion

            PreSetup();
        }

        public override void Dispose()
        {
            this.splashTexture.Dispose();
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Begin(samplerState: SamplerState.PointClamp);
            batch.Draw(splashTexture, new Rectangle(0, 0, splashTexture.Width, splashTexture.Height), Color.White);
            batch.End();
        }

        public override void PreSetup()
        {
            this.KeyboardController.SetPressInputCommands(this.keyboardPresses);
            this.GamepadController.SetPressInputCommands(this.gamepadPresses);
        }

        public void Restart()
        {
            this.Game.FullResetLevel();
            this.previousState.PreSetup();
            this.Game.SetState(previousState);
        }
    }
}
