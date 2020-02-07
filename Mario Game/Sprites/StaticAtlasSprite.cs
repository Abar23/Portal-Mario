using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    class StaticAtlasSprite : Sprite
    {
        public StaticAtlasSprite(Texture2D texture, Point dimensions, Point positionOnTextureAtlas) : base(texture, dimensions, positionOnTextureAtlas)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame)
        {
            if (this.IsVisible)
            {
                Rectangle sourceRectangle = new Rectangle(this.positionOnTextureAtlas, this.dimensions);
                Rectangle destinationRectangle = new Rectangle(positionInGame, this.dimensions);

                spriteBatch.Draw(this.texture, destinationRectangle, sourceRectangle, tint);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame, Vector2 origin, float angle, SpriteEffects spriteEffects)
        {
            if (this.IsVisible)
            {
                Rectangle sourceRectangle = new Rectangle(this.positionOnTextureAtlas, this.dimensions);
                Rectangle destinationRectangle = new Rectangle(positionInGame, this.dimensions);

                spriteBatch.Draw(this.texture, destinationRectangle, sourceRectangle, tint, angle, origin, spriteEffects, 0.0f);
            }
        }

        public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
        }
    }
}
