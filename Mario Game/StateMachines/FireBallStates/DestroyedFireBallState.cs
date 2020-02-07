using MarioGame.GameObjects.Effects;
using MarioGame.Sprites;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.FireBallStates
{
    class DestroyedFireBallState : FireBallState
    {
        private OneShotAtlasSprite fireWorks;

        public DestroyedFireBallState(FireBall fireBall)
        {
            this.fireBall = fireBall;
            this.fireBall.gravity.Disable();
            this.fireBall.XSpeed = 0.0f;
            this.fireBall.YSpeed = 0.0f;
            this.fireBall.Sprite = this.fireBall.SpriteFactory.CreateProduct(EffectTypes.FireWork);
            this.fireBall.IsCollidable = false;
            this.fireWorks = (OneShotAtlasSprite)this.fireBall.Sprite;
        }

        public override void HandleBouncingFireBallTransition(float x, float y)
        {
        }

        public override void HandleDestroyedFireBallTransition()
        {
        }

        public override void HandleReadyToBeSpatTransition()
        {
            this.fireBall.ChangeFireBallState(new ReadyToBeSpatState(this.fireBall));
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
            if (this.fireWorks.IsDoneAnimating)
            {
                this.HandleReadyToBeSpatTransition();
            }
        }
    }
}
