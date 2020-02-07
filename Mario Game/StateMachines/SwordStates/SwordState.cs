using MarioGame.GameObjects.Effects;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.SwordStates
{
    abstract class SwordState
    {
        protected Sword sword;

        public abstract void HandleReadyToSwingTransition();

        public abstract void HangleSwingingTransition();

        public abstract void Update(GraphicsDevice graphicsDevice);

    }
}
