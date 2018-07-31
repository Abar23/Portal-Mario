using System;
using System.Collections.Generic;
using MarioGame.Commands;
using MarioGame.Controllers;
using MarioGame.Costumes;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.Sounds;
using MarioGame.Sprites;
using MarioGame.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioGame.StateMachines.GameStates
{
    class PlayState : GameState
    {
        private static PlayState instance;

        private IDictionary<Keys, ICommand> keyboardHolds = new Dictionary<Keys, ICommand>();
        private IDictionary<Keys, ICommand> keyboardPresses = new Dictionary<Keys, ICommand>();
        private IDictionary<Buttons, ICommand> gamepadHolds = new Dictionary<Buttons, ICommand>();
        private IDictionary<Buttons, ICommand> gamepadPresses = new Dictionary<Buttons, ICommand>();
        private IDictionary<Keys, ICommand> pausedKeyboardHolds = new Dictionary<Keys, ICommand>();
        private IDictionary<Keys, ICommand> pausedKeyboardPresses = new Dictionary<Keys, ICommand>();
        private IDictionary<Buttons, ICommand> pausedGamepadHolds = new Dictionary<Buttons, ICommand>();
        private IDictionary<Buttons, ICommand> pausedGamepadPresses = new Dictionary<Buttons, ICommand>();

        private GameState gameOverState;
        private GameState winState;

        private Texture2D pausedTexture;

        private const int VirtualWidth = 512;
        private const int VirtualHeight = 480;

        public PlayState(MarioGame game, IController<Keys> keys, IController<Buttons> gamePad) : base(game, keys, gamePad)
        {
            this.pausedTexture = game.Content.Load<Texture2D>("Textures/Paused");

            this.ShouldUpdate = true;

            instance = this;
        }

        public override void SetupControls()
        {
            #region Set Controls
            // Create new controllers and assign game exit commands
            this.keyboardPresses.Add(Keys.Q, new ExitCommand(Game));
            this.keyboardPresses.Add(Keys.P, new PauseCommand(this));
            this.keyboardPresses.Add(Keys.M, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));
            this.keyboardPresses.Add(Keys.C, new ToggleDrawHitboxesCommand(Game));
            this.keyboardPresses.Add(Keys.OemTilde, new ResetLevelCommand(Game));

            this.pausedKeyboardPresses.Add(Keys.Q, new ExitCommand(Game));
            this.pausedKeyboardPresses.Add(Keys.M, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));
            this.pausedKeyboardPresses.Add(Keys.P, new PauseCommand(this));
            this.pausedKeyboardPresses.Add(Keys.F, new ToggleFullscreenCommand(Game));

            this.keyboardPresses.Add(Keys.Space, new JumpCommand(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.W, new JumpCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.S, new CrouchCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.A, new WalkLeftCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.D, new WalkRightCommand(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.E, new CubeCommand());
            this.keyboardPresses.Add(Keys.F, new SpitFireBall(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.R, new SwingSword(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.G, new GetInvincibility(Mario.GetInstance()));
            this.pausedKeyboardPresses.Add(Keys.A, new PreviousCostumeCommand(CostumeChanger.GetInstance()));
            this.pausedKeyboardPresses.Add(Keys.D, new NextCostumeCommand(CostumeChanger.GetInstance()));

            this.keyboardPresses.Add(Keys.Up, new JumpCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.Down, new CrouchCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.Left, new WalkLeftCommand(Mario.GetInstance()));
            this.keyboardHolds.Add(Keys.Right, new WalkRightCommand(Mario.GetInstance()));
            this.pausedKeyboardPresses.Add(Keys.Right, new NextCostumeCommand(CostumeChanger.GetInstance()));
            this.pausedKeyboardPresses.Add(Keys.Left, new PreviousCostumeCommand(CostumeChanger.GetInstance()));

            this.gamepadPresses.Add(Buttons.Start, new ExitCommand(Game));
            this.gamepadPresses.Add(Buttons.RightShoulder, new PauseCommand(this));
            this.gamepadPresses.Add(Buttons.LeftShoulder, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));

            this.pausedGamepadPresses.Add(Buttons.Start, new ExitCommand(Game));
            this.pausedGamepadPresses.Add(Buttons.RightShoulder, new PauseCommand(this));
            this.pausedGamepadPresses.Add(Buttons.LeftShoulder, new MuteCommand(MusicPlayer.GetInstance(), Sounds.SoundPlayer.GetInstance()));
            this.pausedGamepadPresses.Add(Buttons.DPadLeft, new NextCostumeCommand(CostumeChanger.GetInstance()));
            this.pausedGamepadPresses.Add(Buttons.DPadRight, new PreviousCostumeCommand(CostumeChanger.GetInstance()));

            this.gamepadPresses.Add(Buttons.DPadUp, new JumpCommand(Mario.GetInstance()));
            this.gamepadHolds.Add(Buttons.DPadDown, new CrouchCommand(Mario.GetInstance()));
            this.gamepadHolds.Add(Buttons.DPadLeft, new WalkLeftCommand(Mario.GetInstance()));
            this.gamepadHolds.Add(Buttons.DPadRight, new WalkRightCommand(Mario.GetInstance()));
            this.gamepadPresses.Add(Buttons.A, new SpitFireBall(Mario.GetInstance()));

            //Cheat Codes
            this.keyboardPresses.Add(Keys.Y, new GoToSmallMarioCommand(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.U, new GoToSuperMarioCommand(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.I, new GoToFireMarioCommand(Mario.GetInstance()));
            this.keyboardPresses.Add(Keys.O, new DamageMarioCommand(Mario.GetInstance()));

            this.keyboardPresses.Add(Keys.D8, new WinCommand(this));
            this.keyboardPresses.Add(Keys.D9, new GameOverCommand(this));
            #endregion

            PreSetup();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!this.ShouldUpdate)
            {
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);

                ISprite background = BackgroundFactory.GetInstance().CreateProduct(BackgroundTypes.BlackBackground);
                background.Draw(spriteBatch, new Color(100, 100, 100, 175), Point.Zero);

                int x = (VirtualWidth - pausedTexture.Width) / 2;
                int y = (VirtualHeight - pausedTexture.Height) / 5;

                spriteBatch.Draw(pausedTexture, new Rectangle(x, y, pausedTexture.Width, pausedTexture.Height), Color.White);

                Texture2D staticMario = ((Sprite) FireMarioFactory.GetInstance().CreateProduct(MarioTypes.IdleRight)).GetTexture();
                spriteBatch.Draw(staticMario, 
                                 new Vector2((float)((VirtualWidth - 96) * 0.5),
                                             (float)((VirtualHeight - 200) * 0.6)), 
                                 new Rectangle(new Point(384, 161), new Point(47, 80)), Color.White,
                                 0, Vector2.Zero, new Vector2(2, 2), SpriteEffects.None, 0f);

                SpriteFont font = this.Game.Content.Load<SpriteFont>("Font/Mario Game HUD Text");
                string name = CostumeChanger.GetInstance().CurrentName();

                Texture2D leftButton = Game.Content.Load<Texture2D>("Textures/Left");
                Texture2D rightButton = Game.Content.Load<Texture2D>("Textures/Right");

                spriteBatch.Draw(leftButton, new Vector2(125, 250), Color.White);
                spriteBatch.Draw(rightButton, new Vector2(325, 250), Color.White);

                Vector2 strSize = font.MeasureString(name);
                int strWidth = (int) Math.Round(strSize.X);
                int strHeight = (int)Math.Round(strSize.Y);
                Vector2 position = new Vector2
                {
                    X = ((float)(VirtualWidth - strWidth) / 2),
                    Y = ((float)(VirtualHeight - strHeight) / 2) + 125
                };
                spriteBatch.DrawString(font,
                                       string.Format(name),
                                       position,
                                       Color.White);

                spriteBatch.End();
            }

        }

        public override void Dispose()
        {
            this.pausedTexture.Dispose();
        }

        public void TogglePause()
        {
            this.ShouldUpdate = !this.ShouldUpdate;

            if (this.ShouldUpdate)
            {
                this.KeyboardController.SetPressInputCommands(keyboardPresses);
                this.KeyboardController.SetHoldInputCommands(keyboardHolds);

                this.GamepadController.SetPressInputCommands(gamepadPresses);
                this.GamepadController.SetHoldInputCommands(gamepadHolds);
            }
            else
            {
                this.KeyboardController.SetPressInputCommands(pausedKeyboardPresses);
                this.KeyboardController.SetHoldInputCommands(pausedKeyboardHolds);

                this.GamepadController.SetPressInputCommands(pausedGamepadPresses);
                this.GamepadController.SetHoldInputCommands(pausedGamepadHolds);
            }
        }

        public void GameOver()
        {
            if (this.gameOverState == null)
            {
                gameOverState = new GameOverState(this, this.Game, this.KeyboardController, this.GamepadController);
                gameOverState.SetupControls();
            } else
            {
                gameOverState.PreSetup();
            }
            Systems.Events.TheInstance.GameOver();
            MouseController.GetInstance.ClearCommands();
            this.Game.SetState(gameOverState);
        }

        public void Win()
        {
            if (this.winState == null)
            {
                winState = new WinState(this, this.Game, this.KeyboardController, this.GamepadController);
                winState.SetupControls();
            }
            else
            {
                winState.PreSetup();
            }
            PlayerHUD.GetInstance().AddPoints(PlayerHUD.GetInstance().GetTimeRemaining() * 10);
            Systems.Events.TheInstance.Win();
            MouseController.GetInstance.ClearCommands();
            this.Game.SetState(winState);
        }

        public override void PreSetup()
        {
            this.KeyboardController.SetPressInputCommands(keyboardPresses);
            this.KeyboardController.SetHoldInputCommands(keyboardHolds);

            this.GamepadController.SetPressInputCommands(gamepadPresses);
            this.GamepadController.SetHoldInputCommands(gamepadHolds);

            MouseController.GetInstance.SetLeftCommand(new FirePortalCommand(true));
            MouseController.GetInstance.SetRightCommand(new FirePortalCommand(false));
        }

        public static PlayState GetInstance()
        {
            return instance;
        }
    }
}
