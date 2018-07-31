using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework;

namespace MarioGame.Commands
{
    class WalkRightCommand : MarioCommand
    {
        public WalkRightCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestWalkRight();
        }
    }

    class WalkLeftCommand : MarioCommand
    {
        public WalkLeftCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestWalkLeft();
        }
    }

    class JumpCommand : MarioCommand
    {
        public JumpCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestJumpTransition();
        }
    }

    class CrouchCommand : MarioCommand
    {
        public CrouchCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestCrouchTransition();
        }
    }

    class GoToSmallMarioCommand : MarioCommand
    {
        public GoToSmallMarioCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestSmallMarioTransition();
        }
    }

    class GoToSuperMarioCommand : MarioCommand
    {
        public GoToSuperMarioCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestSmallMarioTransition();
            this.receiver.RequestSuperMarioTransition();
        }
    }

    class GoToFireMarioCommand : MarioCommand
    {
        public GoToFireMarioCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestFireMarioTransition();
            this.receiver.RequestFireMarioTransition();
        }
    }

    class DamageMarioCommand : MarioCommand
    {
        public DamageMarioCommand(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestDamageMarioTransition();
        }
    }

    class SpitFireBall : MarioCommand
    {
        public SpitFireBall(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestToSpitFire();
        }
    }

    class SwingSword : MarioCommand
    {
        public SwingSword(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestToSwingSword();
        }
    }

    class GetInvincibility : MarioCommand
    {
        public GetInvincibility(Mario mario) : base(mario)
        {
        }

        public override void Execute()
        {
            this.receiver.RequestInvincibility();
        }
    }
}
