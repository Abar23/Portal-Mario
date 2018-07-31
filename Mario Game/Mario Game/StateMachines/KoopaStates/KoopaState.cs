using MarioGame.GameObjects.Enemies;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.KoopaStates
{
    abstract class KoopaState
    {
        protected Koopa koopa;

        public abstract void HandleShellTransition();

        public abstract void HandleWalkLeftTransition();

        public abstract void HandleWalkRightTransition();

        public abstract void HandleFeetOutOfShellTransition();

        public void HandleFireBallDeathTransition(bool isFireBallMovingLeft)
        {
            this.koopa.ChangeKoopaState(new KoopaFireBallDeathState(this.koopa, isFireBallMovingLeft));
        }

        public abstract void Update(GraphicsDevice graphicsDevice);
    }
}
