using MarioGame.Factories;
using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.MarioActionStates
{
    class MarioWalkState : MarioActionState
    {
        private float acceleration;
        public MarioWalkState(Mario mario)
        {
            this.mario = mario;
            this.acceleration = 0f;

            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.WalkLeft);
                this.mario.XSpeed = -1.0f;
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.WalkRight);
                this.mario.XSpeed = 1.0f;
            }
        }

        public override void HandleCrouchTransition()
        {
        }

        public override void HandleIdleTransition()
        {
            this.mario.ChangeActionState(new MarioIdleState(this.mario));
        }

        public override void HandleJumpTransition(bool trueJump)
        {
            this.mario.ChangeActionState(new MarioJumpState(this.mario, trueJump, this));
        }

        public override void HandleWalkTransition(bool walkingDirection)
        {
            if (walkingDirection != this.mario.IsFacingLeft)
            {
                this.mario.ChangeActionState(new MarioIdleState(this.mario));
            } else
            {
                Accelerate();
            }
        }

        private void Accelerate()
        {
            acceleration += .07f;
            if (acceleration > 3.2f)
            {
                acceleration = 3.2f;
            }
            if (this.mario.IsFacingLeft) {
                this.mario.XSpeed = -(1 + acceleration);
            }
            else
            {
                this.mario.XSpeed = 1 + acceleration;
            }
        }

        public override void UpdateSprite()
        {
            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.WalkLeft);
                acceleration = -this.mario.XSpeed + 1;
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.WalkRight);
                acceleration = this.mario.XSpeed - 1;
            }
        }
    }
}
