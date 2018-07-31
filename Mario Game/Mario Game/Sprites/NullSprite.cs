using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    class NullSprite : Sprite
    {
        public override void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame, Vector2 origin, float angle, SpriteEffects spriteEffects)
        {
        }

        public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }

        public NullSprite(Texture2D texture, Point dimensions, Point positionOnTextureAtlas) : base(texture, dimensions, positionOnTextureAtlas)
        {
        }
    }
}
