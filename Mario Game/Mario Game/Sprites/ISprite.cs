using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    interface ISprite
    {
        /// <summary>
        /// Gets the visibility of the sprite
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Draws the sprite to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used to render sprites to the screen</param>
        void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame);

        void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame, Vector2 origin, float angle, SpriteEffects spriteEffects);

        /// <summary>
        /// Updates the position of the sprite
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> of the current program</param>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/> of the current program</param>
        void Update(GameTime gameTime, GraphicsDevice graphicsDevice);

        /// <summary>
        /// Toggles the visibility of the sprite
        /// </summary>
        void ToggleVisibility();

        Point GetDimensions();
    }
}
