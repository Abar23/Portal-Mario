using MarioGame.Factories;
using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.MarioActionStates
{
    class MarioFinishState : MarioActionState
    {
        public MarioFinishState(Mario mario)
        {
            this.mario = mario;

            this.mario.XSpeed = 0;
            this.mario.YSpeed = 0;

            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.JumpLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.JumpRight);
            }
        }

        public override void HandleCrouchTransition()
        {
        }

        public override void HandleIdleTransition()
        {
        }

        public override void HandleJumpTransition(bool trueJump)
        {
        }

        public override void HandleWalkTransition(bool walkingDirection)
        {
        }

        public override void UpdateSprite()
        {
        }
    }
}
