using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects;
using MarioGame.Collision;

namespace MarioGame.StateMachines.PortalStates
{
    abstract class PortalState
    {
        protected Portal portal;

        public abstract void HandleReadyToBeShot();

        public abstract void HandleActiveState();

        public abstract void HandleCollision(GameObject gameObject, CollisionDirection collisionDirection);

        public abstract void Update();
    }
}
