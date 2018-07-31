using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.MarioActionStates
{
    class MarioCrouchState : MarioActionState
    {
        public MarioCrouchState(Mario mario)
        {
            this.mario = mario;
            if (mario.PoweredUp)
            {
                if (this.mario.IsFacingLeft)
                {
                    this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.CrouchLeft);
                }
                else
                {
                    this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.CrouchRight);
                }
            }
            else
            {
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

        public override void HandleCrouchTransition()
        {
        }

        public override void HandleIdleTransition()
        {
            this.mario.ChangeActionState(new MarioIdleState(this.mario));
        }

        public override void HandleJumpTransition(bool trueJump)
        {
            this.mario.ChangeActionState(new MarioIdleState(this.mario));
        }

        public override void HandleWalkTransition(bool walkingDirection)
        {
        }

        public override void UpdateSprite()
        {
            if (mario.PoweredUp)
            {
                if (this.mario.IsFacingLeft)
                {
                    this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.CrouchLeft);
                }
                else
                {
                    this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.CrouchRight);
                }
            }
            else
            {
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
}
