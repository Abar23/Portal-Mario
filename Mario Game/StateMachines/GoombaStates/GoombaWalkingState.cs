using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects;
using MarioGame.Factories;
using MarioGame.View;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.GoombaStates
{
    class GoombaWalkingState : GoombaState
    {
        public GoombaWalkingState(Goomba goomba)
        {
            this.goomba = goomba;
            this.goomba.Sprite = this.goomba.SpriteFactory.CreateProduct(EnemyTypes.Goomba);
        }

        public override void HandleDeathTransistion(GameObject gameObject)
        {
            this.goomba.ChangeGoombaState(new GoombaDeathState(this.goomba, gameObject));
        }

        public override void HandleWalkingTransition()
        {
            this.goomba.XSpeed *= -1;
        }

        public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (this.goomba.XSpeed == 0.0f)
            {
                if (this.goomba.PositionInGame.X + (int)EnemyFactory.EnemySizesInPixels.GoombaWidth >= Camera.AquireInstance().Position.X
                    && this.goomba.PositionInGame.X <= (Camera.AquireInstance().Position.X + Camera.AquireInstance().VirtualWidth))
                {
                    if (Mario.GetInstance().PositionInGame.X > this.goomba.PositionInGame.X)
                    {
                        this.goomba.XSpeed = 1.0f;
                    }
                    else
                    {
                        this.goomba.XSpeed = -1.0f;
                    }
                }
            }
        }
    }
}
