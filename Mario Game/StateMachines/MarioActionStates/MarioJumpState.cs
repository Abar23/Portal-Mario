using MarioGame.Factories;
using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.MarioActionStates
{
    class MarioJumpState : MarioActionState
    {
        private MarioActionState lastState;

        public MarioJumpState(Mario mario, bool jumped, MarioActionState lastState)
        {
            this.mario = mario;
            this.lastState = lastState;
            if (jumped)
            {
                Systems.Events.TheInstance.Jump();
                this.mario.YSpeed = -9.5f;
            }

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
            this.mario.YSpeed = 0;
            this.lastState.UpdateSprite();
            this.mario.ChangeActionState(lastState);
        }

        public override void HandleJumpTransition(bool trueJump)
        {
            if (mario.YSpeed < 0)
            {
                mario.YSpeed = 0;
            }
        }

        public override void HandleWalkTransition(bool walkingDirection)
        {
            if (walkingDirection && this.mario.XSpeed > -1)
            {
                this.mario.XSpeed = -1;
            } else if (!walkingDirection && this.mario.XSpeed < 1)
            {
                this.mario.XSpeed = 1;
            }
        }

        public override void UpdateSprite()
        {
            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.JumpLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.JumpRight);
            }
        }
    }
}
