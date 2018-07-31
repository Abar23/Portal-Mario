using MarioGame.Costumes;
using MarioGame.GameObjects.Player;
using MarioGame.Sounds;

namespace MarioGame.Commands
{
    abstract class Command<TReceiver> : ICommand
    {
        /// <summary>
        /// Defines the generic receriver of the command
        /// </summary>
        protected TReceiver receiver;

        /// <summary>
        /// Constructs a command using a generic receiver
        /// </summary>
        /// <param name="receiver">The generic receiver object</param>
        protected Command(TReceiver receiver)
        {
            this.receiver = receiver;
        }

        /// <summary>
        /// Executes set action of the given receiver
        /// </summary>
        public abstract void Execute();
    }

    abstract class GameCommand : Command<MarioGame>
    {
        /// <summary>
        /// Constructs a new game command with the given main game class receiver
        /// </summary>
        /// <param name="game">The main game class receiver</param>
        protected GameCommand(MarioGame game) : base(game)
        {
        }
    }

    abstract class MarioCommand : Command<Mario>
    {
        protected MarioCommand(Mario mario) : base(mario)
        {
        }
    }

    abstract class AudioCommand : Command<AudioPlayer>
    {
        protected AudioCommand(AudioPlayer audio) : base(audio)
        {
        }
    }

    abstract class CostumeCommand : Command<CostumeChanger>
    {
        protected CostumeCommand(CostumeChanger costumeChanger) : base(costumeChanger)
        {
        }
    }
}
