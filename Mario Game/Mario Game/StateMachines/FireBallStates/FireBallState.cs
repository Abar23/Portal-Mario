using MarioGame.GameObjects.Effects;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.FireBallStates
{
    abstract class FireBallState
    {
        protected FireBall fireBall;

        public abstract void HandleReadyToBeSpatTransition();

        public abstract void HandleBouncingFireBallTransition(float x, float y);

        public abstract void HandleDestroyedFireBallTransition();

        public abstract void Update(GraphicsDevice graphicsDevice);
    }
}
