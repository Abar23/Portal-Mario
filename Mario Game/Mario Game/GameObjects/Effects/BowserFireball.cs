using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Effects
{
    class BowserFireball : GameObject
    {
        public BowserFireball(bool travelingLeft, int positionInGameX, int positionInGameY) : base(null, new Point(), new Vector2())
        {
            this.SpriteFactory = BossFactory.GetInstance();
            this.IsCollidable = true;
            this.SetYPositionInGame = positionInGameY + 32;
            if (travelingLeft)
            {
                this.Sprite = this.SpriteFactory.CreateProduct(BossTypes.FireballLeft);
                this.SetXPositionInGame = positionInGameX;
                this.XSpeed = -6.0f;
            }
            else
            {
                this.Sprite = this.SpriteFactory.CreateProduct(BossTypes.FireballRight);
                this.SetXPositionInGame = positionInGameX + 68;
                this.XSpeed = 6.0f;
            }
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario || gameObject is Block)
            {
                this.XSpeed = 0;
                this.IsVisible = false;
            }
        }
        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (!this.IsVisible)
            {
                this.IsCollidable = false;
            }
            this.Sprite.Update(gameTime, graphicsDevice);
        }
    }
}
