using MarioGame.GameObjects;
using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.ItemStates
{
    class MovingItemState : ItemState
    {
        public MovingItemState(GameObject item)
        {
            this.item = item;
            this.item.YSpeed = 0.0f;

            if (this.item is OneUp || this.item is Star)
            {
                if (Mario.GetInstance().PositionInGame.X >= this.item.PositionInGame.X)
                {
                    this.item.XSpeed = -1.0f;
                }
                else
                {
                    this.item.XSpeed = 1.0f;
                }
            }
            else if (this.item is Mushroom)
            {
                if (Mario.GetInstance().PositionInGame.X >= this.item.PositionInGame.X)
                {
                    this.item.XSpeed = 1.0f;
                }
                else
                {
                    this.item.XSpeed = -1.0f;
                }
            }
        }

        public override void HandleItemRevealTransistion()
        {
        }

        public override void HandleMovingItemTransition()
        {
            if (this.item is Star)
            {
                IRevealableItem revealableItem = (IRevealableItem)this.item;
                revealableItem.ChangeItemState(new BouncingStarState(this.item));
            }
        }

        public override void Update()
        {
        }
    }
}
