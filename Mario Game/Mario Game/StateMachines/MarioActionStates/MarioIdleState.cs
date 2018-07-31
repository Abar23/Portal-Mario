using MarioGame.GameObjects.Player;
using MarioGame.Factories;
namespace MarioGame.StateMachines.MarioActionStates
{
    class MarioIdleState : MarioActionState
    {
        public MarioIdleState(Mario mario)
        {
            this.mario = mario;
            this.mario.XSpeed = 0.0f;
            this.mario.YSpeed = 0.0f;

            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleRight);
            }
        }

        public override void HandleCrouchTransition()
        {
            this.mario.ChangeActionState(new MarioCrouchState(this.mario));
        }

        public override void HandleIdleTransition()
        {
            this.mario.YSpeed = 0.0f;
        }

        public override void HandleJumpTransition(bool trueJump)
        {
            if (this.mario.YSpeed < 1.1)
            {
                this.mario.ChangeActionState(new MarioJumpState(this.mario, trueJump, this));
            }
        }

        public override void HandleWalkTransition(bool walkingDirection)
        {
            if (walkingDirection == this.mario.IsFacingLeft)
            {
                this.mario.ChangeActionState(new MarioWalkState(this.mario));
            }
            else
            {
                this.mario.IsFacingLeft = !this.mario.IsFacingLeft;
                UpdateSprite();
            }
        }

        public override void UpdateSprite()
        {
            this.mario.XSpeed = 0;
            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleRight);
            }
        }
    }
}
