using MarioGame.GameObjects;

namespace MarioGame.StateMachines.ItemStates
{
    class ItemHiddenState : ItemState
    {
        public ItemHiddenState(GameObject item)
        {
            this.item = item;
            this.item.IsVisible = false;
            this.item.IsCollidable = false;
        }

        public override void HandleItemRevealTransistion()
        {
            IRevealableItem revealableItem = (IRevealableItem)this.item;

            if (this.item is BlockCoin)
            {
                revealableItem.ChangeItemState(new BlockCoinRevealState(this.item));
            }
            else
            {
                revealableItem.ChangeItemState(new RevealItemState(this.item));
            }
        }

        public override void HandleMovingItemTransition()
        {
        }

        public override void Update()
        {
        }
    }
}
