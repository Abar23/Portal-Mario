using MarioGame.Collision;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Background
{
    class Flag : GameObject
    {
        public Flag(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.collidable = true;
            this.HitboxType = HitboxTypes.Flag;
            this.sprite = this.spriteFactory.CreateProduct(BackgroundTypes.Flag);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }
    }
}
