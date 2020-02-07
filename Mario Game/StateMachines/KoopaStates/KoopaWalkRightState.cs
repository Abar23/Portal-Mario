using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.KoopaStates
{
    class KoopaWalkRightState : KoopaState
    {
        public KoopaWalkRightState(Koopa koopa)
        {
            this.koopa = koopa;
            this.koopa.XSpeed = 1.0f;
            this.koopa.isWalkingLeft = false;
            this.koopa.gravity.Enable();

            if (this.koopa.isRedKoopa)
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaWalkRight);
            }
            else
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaWalkRight);
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
            this.koopa.ChangeKoopaState(new KoopaWalkLeftState(this.koopa));
        }

        public override void HandleWalkRightTransition()
        {
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
        }
    }
}
