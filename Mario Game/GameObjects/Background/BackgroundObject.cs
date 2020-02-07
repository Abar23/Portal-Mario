using MarioGame.Collision;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Background
{
    class BackgroundObject : GameObject
    {
        public BackgroundObject(IFactory spriteFactory, Point positionInGame, BackgroundTypes type) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(type);
        }

        public override Rectangle GetHitBox()
        {
            return new Rectangle();
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }
    }
}
