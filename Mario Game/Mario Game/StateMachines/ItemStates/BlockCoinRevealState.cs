using MarioGame.GameObjects;

namespace MarioGame.StateMachines.ItemStates
{
    class BlockCoinRevealState : ItemState
    {
        private const int CoinHeight = 64;

        private int heightToMoveOutOfBlock;

        private int originalYPositionOfCoin;

        public BlockCoinRevealState(GameObject item)
        {
            this.item = item;
            this.item.IsVisible = true;
            this.item.YSpeed = -5.0f;
            this.originalYPositionOfCoin = (int)this.item.PositionInGame.Y;
            this.heightToMoveOutOfBlock = (int)this.item.PositionInGame.Y - CoinHeight;
        }

        public override void HandleItemRevealTransistion()
        {
        }

        public override void HandleMovingItemTransition()
        {
        }

        public override void Update()
        {
            if (this.item.PositionInGame.Y <= this.heightToMoveOutOfBlock)
            {
                this.item.YSpeed = -this.item.YSpeed;
            }
            else if (this.item.PositionInGame.Y > this.originalYPositionOfCoin)
            {
                this.item.IsVisible = false;
            }
        }
    }
}
