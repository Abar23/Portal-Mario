using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.GameObjects
{
    class PyramidBlock : Block, IUndergroundBlockEventHandlers
    {
        public PyramidBlock(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Pyramid);
            BlockUpdateEvent.GetInstance().AbovegroundBlockUpdate += this.OnAbovegroundBlobkUpdate;
            BlockUpdateEvent.GetInstance().UndergroundBlockUpdate += this.OnUndergroundBlockUpdate;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            sprite.Update(gameTime, graphicsDevice);
        }

        public void OnUndergroundBlockUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.UndergroundPyramid);
        }

        public void OnAbovegroundBlobkUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.Pyramid);
        }
    }
}
