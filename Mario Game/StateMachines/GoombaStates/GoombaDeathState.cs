using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioGame.Systems;

namespace MarioGame.StateMachines.GoombaStates
{
    class GoombaDeathState : GoombaState
    {
        public GoombaDeathState(Goomba goomba, GameObject gameObject)
        {
            Systems.Events.TheInstance.GoombaDied();
            this.goomba = goomba;

            if (gameObject is Mario)
            {
                this.goomba.XSpeed = 0.0f;

                if (!((Mario)gameObject).IsInvincible())
                {
                    this.goomba.GetGravity.Disable();
                    this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaDeath);
                }
                else
                {
                    this.goomba.YSpeed = -2.0f;
                    this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaFireBallDeath);
                }
            }
            else if (gameObject is Sword)
            {
                this.goomba.XSpeed = 0.0f;
                this.goomba.GetGravity.Disable();
                this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaDeath);
            }
            else if (gameObject is FireBall)
            {
                if ((gameObject).PositionInGame.X > this.goomba.PositionInGame.X)
                {
                    this.goomba.XSpeed = -1.0f;
                }
                else
                {
                    this.goomba.XSpeed = 1.0f;
                }
                this.goomba.YSpeed = -2.0f;

                this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaFireBallDeath);
            }
            else
            {
                this.goomba.XSpeed = 0.0f;
                this.goomba.YSpeed = -2.0f;
                this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.GoombaFireBallDeath);
            }

            this.goomba.isGoombaDead = true;
            this.goomba.IsCollidable = false;
        }

        public override void HandleDeathTransistion(GameObject gameObject)
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
