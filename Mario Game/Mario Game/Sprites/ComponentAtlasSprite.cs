using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    class ComponentAtlasSprite : ISprite
    {
        int xOffsetOfBlockComponent;
        int yOffsetOfBlockComponent;
        List<ISprite> sprites;

        public ComponentAtlasSprite(int xOffset, int yOffset, List<ISprite> sprites)
        {
            this.xOffsetOfBlockComponent = xOffset;
            this.yOffsetOfBlockComponent = yOffset;
            this.sprites = sprites;
        }

        public bool IsVisible { get; }

        public void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame)
        {
            int xOffset = 0;
            int yOffset = 0;
            foreach (Sprite sprite in sprites)
            {
                Point position = new Point(positionInGame.X + xOffset, positionInGame.Y + yOffset);
                sprite.Draw(spriteBatch, tint, position);
                xOffset += this.xOffsetOfBlockComponent;
                yOffset += this.yOffsetOfBlockComponent;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame, Vector2 origin, float angle, SpriteEffects spriteEffects)
        {
        }

        public Point GetDimensions()
        {
            return sprites[0].GetDimensions();
        }

        public void ToggleVisibility()
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.ToggleVisibility();
            }
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime, graphicsDevice);
            }
        }
    }
}
