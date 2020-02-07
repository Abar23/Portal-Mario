using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines;
using MarioGame.StateMachines.ItemStates;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class FireFlower : GameObject, IRevealableItem
    {
        private ItemState itemState;

        public FireFlower(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(ItemTypes.FireFlower);
            this.itemState = new ItemHiddenState(this);
            this.hitboxOffset = -1;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                Systems.Events.TheInstance.PowerUp();
                Mario.GetInstance().RequestFireMarioTransition();
                this.IsVisible = false;
                this.IsCollidable = false;
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
            Systems.Events.TheInstance.ItemReveal();
            this.UpdateSprite();
            this.ChangeItemState(new RevealItemState(this));
        }

        private void UpdateSprite()
        {
            this.sprite = this.spriteFactory.CreateProduct(ItemTypes.FireFlower);
        }
    }
}
