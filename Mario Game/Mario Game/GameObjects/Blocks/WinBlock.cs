using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using MarioGame.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class WinBlock : Block
    {
        private bool finished;
        public WinBlock(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Pyramid);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario && Mario.GetInstance().IsFinishedState() && !finished)
            {
                finished = true;
                PlayerHUD.GetInstance().AddTimeToPoints();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            sprite.Update(gameTime, graphicsDevice);
        }
    }
}
