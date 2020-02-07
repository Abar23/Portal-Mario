using System;
using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class FloorBlock : Block, IUndergroundBlockEventHandlers
    {
        public FloorBlock(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Floor);
            BlockUpdateEvent.GetInstance().AbovegroundBlockUpdate += this.OnAbovegroundBlobkUpdate;
            BlockUpdateEvent.GetInstance().UndergroundBlockUpdate += this.OnUndergroundBlockUpdate;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        public void OnAbovegroundBlobkUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.Floor);
        }

        public void OnUndergroundBlockUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.UndergroundFloor);
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            sprite.Update(gameTime, graphicsDevice);
        }
    }
}
