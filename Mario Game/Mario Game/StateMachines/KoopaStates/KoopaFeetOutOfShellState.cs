using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.KoopaStates
{
    class KoopaFeetOutOfShellState : KoopaState
    {
        private int transitionTimer;

        public KoopaFeetOutOfShellState(Koopa koopa)
        {
            this.koopa = koopa;
            this.transitionTimer = 0;

            if (this.koopa.isRedKoopa)
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaFeetOutOfShell);
            }
            else
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaFeetOutOfShell);
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
            this.koopa.ChangeKoopaState(new KoopaWalkRightState(this.koopa));
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
            if (this.koopa.XSpeed == 0.0f)
            {
                if (this.transitionTimer <= 120)
                {
                    this.transitionTimer++;
                }

                if (this.transitionTimer > 120)
                {
                    if (this.koopa.isWalkingLeft)
                    {
                        this.HandleWalkLeftTransition();
                    }
                    else
                    {
                        this.HandleWalkRightTransition();
                    }
                }
            }
        }
    }
}
