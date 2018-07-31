using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Player;
using MarioGame.View;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.FireBallStates
{
    class BouncingFireBallState : FireBallState
    {
        const int YPositionToMariosMouth = 32;

        public BouncingFireBallState(FireBall fireBall, float xSpeed, float ySpeed)
        {
            this.fireBall = fireBall;
            this.fireBall.IsCollidable = true;
            this.fireBall.Sprite = this.fireBall.SpriteFactory.CreateProduct(EffectTypes.FireBall);

            this.fireBall.YSpeed = ySpeed;
            if (Mario.GetInstance().IsFacingLeft)
            {
                this.fireBall.XSpeed = xSpeed * -1;
                this.fireBall.SetXPositionInGame = Mario.GetInstance().PositionInGame.X;
            }
            else
            {
                this.fireBall.XSpeed = xSpeed;
                this.fireBall.SetXPositionInGame = (int)FireMarioFactory.SpriteSizesInPixels.LargeWidth + Mario.GetInstance().PositionInGame.X - 16;
            }
            this.fireBall.SetYPositionInGame = Mario.GetInstance().PositionInGame.Y + YPositionToMariosMouth;

            this.fireBall.gravity.Enable();
        }

        public override void HandleBouncingFireBallTransition(float x, float y)
        {
        }

        public override void HandleDestroyedFireBallTransition()
        {
            this.fireBall.ChangeFireBallState(new DestroyedFireBallState(this.fireBall));
        }

        public override void HandleReadyToBeSpatTransition()
        {
            this.fireBall.ChangeFireBallState(new ReadyToBeSpatState(this.fireBall));
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
            
            if (this.fireBall.PositionInGame.Y < -64 
                || this.fireBall.PositionInGame.Y > Camera.AquireInstance().VirtualHeight 
                || this.fireBall.PositionInGame.X < 0)
            {
                this.HandleReadyToBeSpatTransition();
            }
        }
    }
}
