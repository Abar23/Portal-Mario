using MarioGame.Factories;
using MarioGame.GameObjects.Enemies;

namespace MarioGame.StateMachines.BowserStates
{
    class BowserIdleState : BowserState
    {
        public BowserIdleState(Bowser bowser)
        {
            this.bowser = bowser;
            this.bowser.XSpeed = 0;

            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserIdleLeft);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserIdleRight);
            }

        }

        public override void HandleIdleTransition()
        {

        }

        public override void HandleChargeTransition()
        {
            this.bowser.ChangeState(new BowserChargeState(this.bowser));
        }

        public override void HandleFireBreathTransition()
        {

        }

        public override void HandleShellTransition()
        {
            this.bowser.ChangeState(new BowserShellState(this.bowser));
        }

        public override void HandleDeadTransition()
        {
            this.bowser.ChangeState(new BowserDeadState(this.bowser));
        }

        public override void UpdateSprite()
        {
            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserIdleLeft);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserIdleRight);
            }
        }
    }
}
