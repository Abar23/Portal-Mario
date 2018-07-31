using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Factories;
using MarioGame.GameObjects.Enemies;
using MarioGame.Physics;

namespace MarioGame.StateMachines.BowserStates
{
    class BowserDeadState : BowserState
    {
        public BowserDeadState(Bowser bowser)
        {
            Systems.Events.TheInstance.BowserDied();
            this.bowser = bowser;
            this.bowser.YSpeed = 3.0f;
            this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserDead);
            this.bowser.IsCollidable = false;

        }

        public override void HandleIdleTransition()
        {

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
            this.bowser.Sprite = this.bowser.SpriteFactory.CreateProduct(BossTypes.BowserDead);
        }
    }
}
