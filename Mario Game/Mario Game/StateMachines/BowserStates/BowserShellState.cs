using MarioGame.Factories;
using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.BowserStates
{
    class BowserShellState : BowserState
    {
        public BowserShellState(Bowser bowser)
        {
            this.bowser = bowser;
            this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserShell);
            this.bowser.HitboxType = HitboxTypes.Half;
            if (this.bowser.IsFacingLeft)
            {
                this.bowser.XSpeed = -6.0f;
            }
            else
            {
                this.bowser.XSpeed = 6.0f;
            }
        }

        public override void HandleIdleTransition()
        {
            this.bowser.HitboxType = HitboxTypes.Full;

            if (Mario.GetInstance().PositionInGame.X > this.bowser.PositionInGame.X)
            {
                this.bowser.IsFacingLeft = false;
            }
            else
            {
                this.bowser.IsFacingLeft = true;
            }

            this.bowser.ChangeState(new BowserIdleState(this.bowser));
        }

        public override void HandleChargeTransition()
        {

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
            this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserShell);
        }
    }
}
