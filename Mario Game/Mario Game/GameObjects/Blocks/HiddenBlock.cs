using MarioGame.Collision;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class HiddenBlock : Block, IBumpable
    {
        private Block block;

        public HiddenBlock(Block block) : base(block.SpriteFactory, new Point((int)block.PositionInGame.X, (int)block.PositionInGame.Y), new Vector2())
        {
            this.block = block;
            this.block.ToggleVisibility();
            this.solid = false;
        }

        public void Bump()
        {
            Systems.Events.TheInstance.BrickBump();
            if (!this.block.IsVisible)
            {
                ToggleVisibility();
                this.solid = true;
            }
            else if (this.block is IBumpable)
            {
                IBumpable bumpable = (IBumpable)block;
                bumpable.Bump();
            }
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (collisionDirection == CollisionDirection.Bottom && gameObject is Mario)
            {
                Bump();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            block.Update(gameTime, graphicsDevice);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch);
        }

        public override Rectangle GetHitBox()
        {
            return block.GetHitBox();
        }

        public override void ToggleVisibility()
        {
            this.block.ToggleVisibility();
        }

        public override void AddItem(IRevealableItem item)
        {
            this.block.AddItem(item);
        }

        public override bool IsVisible => this.block.IsVisible;
    }
}
