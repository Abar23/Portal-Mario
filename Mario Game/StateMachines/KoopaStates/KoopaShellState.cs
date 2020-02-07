using MarioGame.GameObjects.Enemies;
using MarioGame.Factories;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Systems;

namespace MarioGame.StateMachines.KoopaStates
{
    class KoopaShellState : KoopaState
    {
        private int transitionTimer;

        public KoopaShellState(Koopa koopa)
        {
            Systems.Events.TheInstance.KoopaDied();
            this.koopa = koopa;
            this.koopa.XSpeed = 0.0f;
            this.transitionTimer = 0;

            if (this.koopa.isRedKoopa)
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.RedKoopaInShell);
            }
            else
            {
                this.koopa.Sprite = this.koopa.SpriteFactory.CreateProduct(EnemyTypes.GreenKoopaInShell);
            }
        }

        public override void HandleFeetOutOfShellTransition()
        {
            this.koopa.ChangeKoopaState(new KoopaFeetOutOfShellState(this.koopa));
        }

        public override void HandleShellTransition()
        {
            this.transitionTimer = 0;
            this.koopa.XSpeed = 0.0f;
        }

        public override void HandleWalkLeftTransition()
        {
        }

        public override void HandleWalkRightTransition()
        {
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
            if (this.koopa.XSpeed == 0.0f)
            {
                if (this.transitionTimer <= 240)
                {
                    this.transitionTimer++;
                }

                if (this.transitionTimer > 240)
                {
                    this.HandleFeetOutOfShellTransition();
                }
            }
        }
    }
}
