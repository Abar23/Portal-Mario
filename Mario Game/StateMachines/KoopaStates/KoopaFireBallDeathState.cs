using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.KoopaStates
{
    class KoopaFireBallDeathState : KoopaState
    {
        public KoopaFireBallDeathState(Koopa koopa, bool isFireBallMovingLeft)
        {
            Systems.Events.TheInstance.KoopaDied();
            this.koopa = koopa;
            this.koopa.gravity.Enable();

            if (isFireBallMovingLeft)
            {
                this.koopa.XSpeed = 1.0f;
            }
            else
            {
                this.koopa.XSpeed = -1.0f;
            }
            this.koopa.YSpeed = -2.0f;

            if (this.koopa.isRedKoopa)
            {
                if (isFireBallMovingLeft)
                {
                    this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaLeftFireBallDeath);
                }
                else
                {
                    this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaRightFireBallDeath);
                }
            }
            else
            {
                if (isFireBallMovingLeft)
                {
                    this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaLeftFireBallDeath);
                }
                else
                {
                    this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaRightFireBallDeath);
                }

            }

            this.koopa.IsCollidable = false;
        }

        public override void HandleFeetOutOfShellTransition()
        {
        }

        public override void HandleShellTransition()
        {
        }

        public override void HandleWalkLeftTransition()
        {
        }

        public override void HandleWalkRightTransition()
        {
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
        }
    }
}
