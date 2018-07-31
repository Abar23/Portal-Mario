using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Factories;
using MarioGame.StateMachines.BrickBlockStates;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using MarioGame.Collision;
using System;
using MarioGame.Events;

namespace MarioGame.GameObjects
{
    class BrickBlock : StateBlock, IBumpable, IUndergroundBlockEventHandlers
    {
        private bool unused = true;

        public BrickBlock(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Brick);
            State = new BrickStillState(this);
            BlockUpdateEvent.GetInstance().AbovegroundBlockUpdate += this.OnAbovegroundBlobkUpdate;
            BlockUpdateEvent.GetInstance().UndergroundBlockUpdate += this.OnUndergroundBlockUpdate;
        }

        public void Bump()
        {
            Systems.Events.TheInstance.BrickBump();
            if (!HasNoItems())
            {
                IRevealableItem item = items.Dequeue();
                item.RevealItem();
                if (item is BlockCoin)
                {
                    Systems.Events.TheInstance.Coin();
                }
                State.HandleCollision(false);
                if (HasNoItems())
                {
                    unused = false;
                    this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Used);
                }
            }
            else {
                Mario player = Mario.GetInstance();
                if (player.IsPoweredUp() && unused)
                {
                    this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Broken);
                    Systems.Events.TheInstance.BrickBreak();
                }
                State.HandleCollision(player.IsPoweredUp() && unused);
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            State.HandleUpdate(gameTime);
            this.sprite.Update(gameTime, graphicsDevice);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (collisionDirection == CollisionDirection.Bottom && gameObject is Mario)
            {
                Bump();
            }
        }

        public void OnUndergroundBlockUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.UndergroundBrick);
        }

        public void OnAbovegroundBlobkUpdate(object sender, EventArgs args)
        {
            this.Sprite = this.SpriteFactory.CreateProduct(BlockTypes.Brick);
        }
    }
}
