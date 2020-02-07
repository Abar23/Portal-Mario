using MarioGame.Costumes;
using MarioGame.StateMachines.GameStates;
﻿using MarioGame.Sounds;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Other;

namespace MarioGame.Commands
{
    class ExitCommand : GameCommand
    {
        /// <summary>
        /// Constructs a new command to exit the main game
        /// </summary>
        /// <param name="game">The main game class of the program</param>
        public ExitCommand(MarioGame game) : base(game)
        {
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public override void Execute()
        {
            this.receiver.Exit();
        }
    }

    class ToggleDrawHitboxesCommand : GameCommand
    {
        public ToggleDrawHitboxesCommand(MarioGame game) : base(game)
        {
        }

        public override void Execute()
        {
            this.receiver.ToggleDrawHitboxes();
        }
    }

    class ResetLevelCommand : GameCommand
    {
        public ResetLevelCommand(MarioGame game) : base(game)
        {
        }

        public override void Execute()
        {
            this.receiver.ResetLevel();
        }
    }

    class ToggleFullscreenCommand : GameCommand
    {
        public ToggleFullscreenCommand(MarioGame game) : base(game)
        {
        }

        public override void Execute()
        {
            this.receiver.ToggleFullscreen();
        }
    }

    class PauseCommand : ICommand
    {
        private PlayState state;
        public PauseCommand(PlayState playState)
        {
            this.state = playState;
        }

        public void Execute()
        {
            this.state.TogglePause();
        }
    }


    class RestartCommand : ICommand
    {
        private IRestartable restartable;
        public RestartCommand(IRestartable restartable)
        {
            this.restartable = restartable;
        }

        public void Execute()
        {
            this.restartable.Restart();
        }
    }

    class GameOverCommand : ICommand
    {
        private PlayState state;
        public GameOverCommand(PlayState playState)
        {
            this.state = playState;
        }

        public void Execute()
        {
            this.state.GameOver();
        }
    }

    class WinCommand : ICommand
    {
        private PlayState state;
        public WinCommand(PlayState playState)
        {
            this.state = playState;
        }

        public void Execute()
        {
            this.state.Win();
        }
    }

    class MuteCommand : AudioCommand
    {
        private AudioPlayer audio2;
        public MuteCommand(AudioPlayer audio1, AudioPlayer audio2) : base(audio1)
        {
            this.audio2 = audio2;
        }

        public override void Execute()
        {
            this.receiver.Mute();
            this.audio2.Mute();

        }
    }

    class NextCostumeCommand : CostumeCommand
    {
        public NextCostumeCommand(CostumeChanger costume) : base(costume)
        {
        }

        public override void Execute()
        {
            this.receiver.NextCostume();
        }
    }

    class PreviousCostumeCommand : CostumeCommand
    {
        public PreviousCostumeCommand(CostumeChanger costume) : base(costume)
        {
        }

        public override void Execute()
        {
            this.receiver.PreviousCostume();
        }
    }

    class FirePortalCommand : ICommand
    {
        private bool left;

        public FirePortalCommand(bool left)
        {
            this.left = left;
        }

        public void Execute()
        {
            if (PortalGun.GetInstance().TrackingMarioPosition)
            {
                if (left)
                {
                    new PortalProjectile(true);
                }
                else
                {
                    new PortalProjectile(false);
                }
            }
        }
    }

    class CubeCommand : ICommand
    {
        public void Execute()
        {
            CompanionCube.SelectCube();
        }
    }

    class NullCommand : ICommand
    {
        public NullCommand() { }

        public void Execute() { }
    }
}
