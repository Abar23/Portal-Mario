using MarioGame.Factories;
using MarioGame.GameObjects.Enemies;

namespace MarioGame.StateMachines.BowserStates
{
    class BowserChargeState : BowserState
    {
        public BowserChargeState(Bowser bowser)
        {
            this.bowser = bowser;

            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserChargeLeft);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserChargeRight);
            }

        }

        public override void HandleIdleTransition()
        {

        }

        public override void HandleChargeTransition()
        {

        }

        public override void HandleFireBreathTransition()
        {
            this.bowser.ChangeState(new BowserFireBreathState(this.bowser));
        }

        public override void HandleShellTransition()
        {

        }

        public override void HandleDeadTransition()
        {
            this.bowser.ChangeState(new BowserDeadState(this.bowser));
        }

        public override void UpdateSprite()
        {
            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserChargeLeft);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserChargeRight);
            }
        }
    }
}
