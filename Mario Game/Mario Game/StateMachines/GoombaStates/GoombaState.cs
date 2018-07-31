using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.GoombaStates
{
    abstract class GoombaState
    {
        protected Goomba goomba;

        public abstract void HandleDeathTransistion(GameObject gameObject);

        public abstract void HandleWalkingTransition();

        public abstract void Update(GameTime gameTime, GraphicsDevice graphicsDevice);
    }
}
