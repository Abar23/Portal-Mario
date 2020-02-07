using MarioGame.Factories;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Enemies;

namespace MarioGame.StateMachines.BowserStates
{
    class BowserFireBreathState : BowserState
    {
        public BowserFireBreathState(Bowser bowser)
        {
            Systems.Events.TheInstance.BowserFire();
            this.bowser = bowser;

            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserFireLeft);
                this.bowser.fireball = new BowserFireball(true,this.bowser.PositionInGame.X, this.bowser.PositionInGame.Y);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserFireRight);
                this.bowser.fireball = new BowserFireball(false, this.bowser.PositionInGame.X, this.bowser.PositionInGame.Y);
            }
        }

        public override void HandleIdleTransition()
        {
            this.bowser.ChangeState(new BowserIdleState(this.bowser));
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

        }

        public override void HandleDeadTransition()
        {

        }

        public override void UpdateSprite()
        {
            if (this.bowser.IsFacingLeft)
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserFireLeft);
            }
            else
            {
                this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserFireRight);
            }
        }
    }
}
