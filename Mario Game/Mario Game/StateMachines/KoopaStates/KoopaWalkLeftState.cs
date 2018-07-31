using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.View;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.KoopaStates
{
    class KoopaWalkLeftState : KoopaState
    {
        public KoopaWalkLeftState(Koopa koopa)
        {
            this.koopa = koopa;
            this.koopa.XSpeed *= -1;
            this.koopa.isWalkingLeft = true;
            this.koopa.gravity.Enable();

            if (this.koopa.isRedKoopa)
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaWalkLeft);
            }
            else
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaWalkLeft);
            }
        }

        public override void HandleFeetOutOfShellTransition()
        {
        }

        public override void HandleShellTransition()
        {
            this.koopa.ChangeKoopaState(new KoopaShellState(this.koopa));
        }

        public override void HandleWalkLeftTransition()
        {
        }

        public override void HandleWalkRightTransition()
        {
            this.koopa.ChangeKoopaState(new KoopaWalkRightState(this.koopa));
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
            if (this.koopa.XSpeed == 0.0f)
            {
                if (this.koopa.PositionInGame.X + (int)EnemyFactory.EnemySizesInPixels.GoombaWidth >= Camera.AquireInstance().Position.X 
                    && this.koopa.PositionInGame.X <= (Camera.AquireInstance().Position.X + Camera.AquireInstance().VirtualWidth))
                {
                    if (Mario.GetInstance().PositionInGame.X > this.koopa.PositionInGame.X)
                    {
                        this.koopa.ChangeKoopaState(new KoopaWalkRightState(this.koopa));
                    }
                    else
                    {
                        this.koopa.XSpeed = -1.0f;
                    }
                }
            }
        }
    }
}
