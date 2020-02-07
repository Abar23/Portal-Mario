using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Systems;

namespace MarioGame.StateMachines.GoombaStates
{
    class GoombaFireBallDeathTransition : GoombaState
    {
        public GoombaFireBallDeathTransition(Goomba goomba, bool isFireBallMovingLeft)
        {
            Events.theInstance.GoombaDied();
            this.goomba = goomba;

            if (isFireBallMovingLeft)
            {
                this.goomba.XSpeed = 1.0f;
            }
            else
            {
                this.goomba.XSpeed = -1.0f;
            }
            this.goomba.YSpeed = -2.0f;

            this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaFireBallDeath);

            this.goomba.IsCollidable = false;
            this.goomba.isGoombaDead = true;
        }

        public override void HandleDeathTransistion()
        {
        }

        public override void HandleFireBallDeathTransition(bool fireBallDirection)
        {
        }

        public override void HandleWalkingTransition()
        {
        }

        public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }
    }
}
