using MarioGame.Collision;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Blocks
{
    class SmallPipe : Pipe
    {
        public SmallPipe(IFactory spriteFactory, Point positionInGame, Vector2 velocityOfObject) : base(spriteFactory, positionInGame, velocityOfObject)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BackgroundTypes.Pipe);
            this.hitboxColor = Color.DarkBlue;
            this.IsSolid = true;
            this.IsCollidable = true;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }
    }
}
