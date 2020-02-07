using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    abstract class Sprite : ISprite
    {
        /// <summary>
        /// Defines the texture of the sprite
        /// </summary>
        protected Texture2D texture;

        /// <summary>
        /// Defines the width and height of the sprite
        /// </summary>
        protected Point dimensions;

        protected Point positionOnTextureAtlas;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class
        /// </summary>
        /// <param name="texture">The 2D texture for the sprite</param>
        /// <param name="dimensionsInGame">The <see cref="Point"/> for width and height of the sprite</param>
        protected Sprite(Texture2D texture, Point dimensions, Point positionOnTextureAtlas)
        {
            this.texture = texture;
            this.dimensions = dimensions;
            this.positionOnTextureAtlas = positionOnTextureAtlas;
            this.IsVisible = true;
        }

        /// <summary>
        /// Gets the visibility of the sprite
        /// </summary>
        public bool IsVisible { get; protected set; }

        /// <summary>
        /// Draws the sprite to the screen
        /// </summary>
        /// <param name="spriteBatch">The <see cref="SpriteBatch"/> used to render sprites to the screen</param>
        public abstract void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame);

        public abstract void Draw(SpriteBatch spriteBatch, Color tint, Point positionInGame, Vector2 origin, float angle, SpriteEffects spriteEffects);

        /// <summary>
        /// Updates the position of the sprite
        /// </summary>
        /// <param name="gameTime">The <see cref="GameTime"/> of the current program</param>
        /// <param name="graphicsDevice">The <see cref="GraphicsDevice"/> of the current program</param>
        public abstract void Update(GameTime gameTime, GraphicsDevice graphicsDevice);

        /// <summary>
        /// Toggles the visibility of the sprite
        /// </summary>
        public void ToggleVisibility()
        {
            this.IsVisible = !this.IsVisible;
        }

        public Point GetDimensions()
        {
            return dimensions;
        }

        public Texture2D GetTexture()
        {
            return this.texture;
        }
    }
}
