using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.BrickBlockStates
{
    abstract class BlockState
    {
        public abstract void HandleUpdate(GameTime gameTime);

        public virtual void HandleCollision(bool marioPoweredUp) { }
    }
}
