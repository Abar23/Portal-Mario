using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.StateMachines.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class BlockCoin : GameObject, IRevealableItem
    {
        private ItemState itemState;

        public BlockCoin(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(ItemTypes.BlockCoin);
            this.itemState = new ItemHiddenState(this);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                this.IsCollidable = false;
                this.IsVisible = false;
                Systems.Events.TheInstance.Coin();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.itemState.Update();
            sprite.Update(gameTime, graphicsDevice);
        }

        public void ChangeItemState(ItemState state)
        {
            this.itemState = state;
        }

        public void RevealItem()
        {
            this.ChangeItemState(new BlockCoinRevealState(this));
        }
    }
}
