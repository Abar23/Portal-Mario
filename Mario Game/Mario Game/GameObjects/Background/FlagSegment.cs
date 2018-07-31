using MarioGame.Collision;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Background
{
    class FlagSegment : GameObject
    {
        private int points;

        public FlagSegment(IFactory spriteFactory, Point positionInGame, int points) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.hitboxOffset = -1;
            this.collidable = true;
            this.sprite = this.spriteFactory.CreateProduct(BackgroundTypes.FlagSegment);
            this.points = points;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }

        public int GetPoints()
        {
            return points;
        }
    }
}
