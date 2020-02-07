using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.StateMachines.MarioActionStates;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class PipeExitState : MarioPowerUpState
    {
        private int ascendDistance;
        private MarioPowerUpState lastPower;
        private MarioActionState lastAction;

        public PipeExitState(Mario mario, MarioActionState lastAction, MarioPowerUpState lastPower)
        {
            Systems.Events.TheInstance.Warp();
            this.mario = mario;
            this.lastPower = lastPower;
            this.lastAction = lastAction;
            this.ascendDistance = (int)this.mario.PositionInGame.Y - mario.Sprite.GetDimensions().Y;
        }

        public override void HandleDamageMarioTransition()
        {
        }

        public override void HandleFireMarioTransition()
        {
        }

        public override void HandleSmallMarioTransition()
        {
        }

        public override void HandleSuperMarioTransition()
        {
        }

        public override void Update()
        {
            this.mario.IsCollidable = false;
            this.mario.gravity.Disable();
            this.mario.XSpeed = 0.0f;
            this.mario.YSpeed = -1.0f;
            this.mario.ChangeActionState(new MarioJumpState(this.mario, false, lastAction));
            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleRight);
            }
            if (this.mario.PositionInGame.Y <= this.ascendDistance)
            {
                this.mario.gravity.Enable();
                this.mario.IsCollidable = true;
                this.mario.ChangeActionState(new MarioIdleState(this.mario));
                this.mario.ChangePowerUpState(lastPower);
            }
        }
    }
}
