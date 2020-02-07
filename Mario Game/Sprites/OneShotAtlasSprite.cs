using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    class OneShotAtlasSprite : Sprite
    {
        /// <summary>
        /// Defines the current frame of animation
        /// </summary>
        private int currentFrame;

        /// <summary>
        /// Defines the total frames of the animation
        /// </summary>
        private int totalFrames;

        /// <summary>
        /// Defines the interval between frames in milliseconds
        /// </summary>
        private int intervalBetweenFrames;

        /// <summary>
        /// Defines the counter that keeps track of time passed between frame transitions
        /// </summary>
        private int timeElapsedBetweenFrames;

        private bool isDoneAnimating;

        public bool IsDoneAnimating
        {
            get => this.isDoneAnimating;
        }

        public OneShotAtlasSprite(Texture2D texture, Point dimensions, Point positionOnTextureAtlas, int intervalBetweenFrames, int totalFrames) : base(texture, dimensions, positionOnTextureAtlas)
        {
            this.isDoneAnimating = false;
            this.currentFrame = 0;
            this.totalFrames = totalFrames;
            this.intervalBetweenFrames = intervalBetweenFrames;
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
            if (!this.isDoneAnimating)
            {
                this.timeElapsedBetweenFrames += gameTime.ElapsedGameTime.Milliseconds;

                if (this.timeElapsedBetweenFrames >= this.intervalBetweenFrames)
                {
                    this.timeElapsedBetweenFrames = 0;

                    this.currentFrame++;

                    this.positionOnTextureAtlas.X += this.dimensions.X;

                    if (this.currentFrame == this.totalFrames)
                    {
                        this.isDoneAnimating = !this.isDoneAnimating;
                    }
                }
            }
        }
    }
}
