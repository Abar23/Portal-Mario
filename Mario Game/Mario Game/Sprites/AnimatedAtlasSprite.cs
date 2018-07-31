using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Sprites
{
    class AnimatedAtlasSprite : Sprite
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

        private bool isCyclic;

        private int frameAnimationMovement;

        public AnimatedAtlasSprite(Texture2D texture, Point dimensions, Point positionOnTextureAtlas, int intervalBetweenFrames, int totalFrames, bool isCyclic) : base(texture, dimensions, positionOnTextureAtlas)
        {
            this.currentFrame = 0;
            this.totalFrames = totalFrames;
            this.intervalBetweenFrames = intervalBetweenFrames;
            this.isCyclic = isCyclic;
            this.frameAnimationMovement = 1;
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
            this.timeElapsedBetweenFrames += gameTime.ElapsedGameTime.Milliseconds;

            if (this.timeElapsedBetweenFrames >= this.intervalBetweenFrames)
            {
                this.timeElapsedBetweenFrames = 0;

                this.currentFrame += this.frameAnimationMovement;

                this.positionOnTextureAtlas.X += this.dimensions.X * this.frameAnimationMovement;

                if (this.currentFrame == this.totalFrames)
                {
                    if (this.isCyclic)
                    {
                        this.positionOnTextureAtlas.X -= this.dimensions.X * this.currentFrame;
                        this.currentFrame = 0;
                    }
                    else
                    {
                        this.frameAnimationMovement = -1;
                        this.positionOnTextureAtlas.X -= this.dimensions.X * 2;
                        this.currentFrame += this.frameAnimationMovement * 2;
                    }
                }
                else if (this.currentFrame == 0 && !this.isCyclic)
                {
                    this.frameAnimationMovement = 1;
                }
            }
        }
    }
}
