using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class UndergroundCoin : GameObject
    {

        public UndergroundCoin(IFactory spriteFactory, Point positionInGame, bool isAboveGround) : base(spriteFactory, positionInGame, new Vector2())
        {
            if (isAboveGround)
            {
                this.sprite = this.spriteFactory.CreateProduct(ItemTypes.BlockCoin);
            }
            else
            {
                this.sprite = this.spriteFactory.CreateProduct(ItemTypes.UndergroundCoin);
            }

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
            sprite.Update(gameTime, graphicsDevice);
        }
    }
}
