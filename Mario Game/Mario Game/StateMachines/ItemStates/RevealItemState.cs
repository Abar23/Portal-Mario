using MarioGame.GameObjects;

namespace MarioGame.StateMachines.ItemStates
{
    class RevealItemState : ItemState
    {
        private const int BlockHeight = 34;

        private int heightToMoveOutOfBlock;

        public RevealItemState(GameObject item)
        {
            this.item = item;
            this.item.IsVisible = true;
            this.item.YSpeed = -4.0f;
            this.heightToMoveOutOfBlock = this.item.PositionInGame.Y - BlockHeight;
        }

        public override void HandleItemRevealTransistion()
        {
        }

        public override void HandleMovingItemTransition()
        {
            IRevealableItem revealableItem = (IRevealableItem)this.item;

            if (this.item is Mushroom || this.item is OneUp || this.item is Star)
            {
                revealableItem.ChangeItemState(new MovingItemState(this.item));
            }
        }

        public override void Update()
        {
            if (this.item.PositionInGame.Y <= this.heightToMoveOutOfBlock)
            {
                this.item.YSpeed = 0.0f;
                this.item.IsCollidable = this.item.IsVisible;
                this.HandleMovingItemTransition();
            }
        }
    }
}
