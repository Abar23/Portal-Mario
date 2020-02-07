using MarioGame.GameObjects;

namespace MarioGame.StateMachines.ItemStates
{
    abstract class ItemState
    {
        protected GameObject item;

        public abstract void HandleItemRevealTransistion();

        public abstract void HandleMovingItemTransition();

        public abstract void Update();
    }
}
