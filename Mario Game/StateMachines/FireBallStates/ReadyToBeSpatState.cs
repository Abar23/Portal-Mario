using MarioGame.GameObjects.Effects;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.FireBallStates
{
    class ReadyToBeSpatState : FireBallState
    {
        public ReadyToBeSpatState(FireBall fireBall)
        {
            this.fireBall = fireBall;
            this.fireBall.IsCollidable = false;
            this.fireBall.XSpeed = 0.0f;
            this.fireBall.YSpeed = 0.0f;
            this.fireBall.gravity.Disable();
            this.fireBall.Sprite = new NullSprite(null, new Point(), new Point());
        }

        public override void HandleBouncingFireBallTransition(float x, float y)
        {
            this.fireBall.ChangeFireBallState(new BouncingFireBallState(this.fireBall, x, y));
        }

        public override void HandleDestroyedFireBallTransition()
        {
        }

        public override void HandleReadyToBeSpatTransition()
        {
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
        }
    }
}
