using MarioGame.GameObjects;

namespace MarioGame.StateMachines.ItemStates
{
    class BouncingStarState : ItemState
    {
        public BouncingStarState(GameObject item)
        {
            this.item = item;
            this.item.YSpeed = 1.0f;
        }

        public override void HandleItemRevealTransistion()
        {
        }

        public override void HandleMovingItemTransition()
        {
        }

        public override void Update()
        {
        }
    }
}
