using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using MarioGame.StateMachines.BrickBlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class QuestionBlock : StateBlock, IBumpable
    {
        public QuestionBlock(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Question);
            State = new QuestionStillState(this);
        }

        public void Bump()
        {
            Systems.Events.TheInstance.BrickBump();
            if (!HasNoItems()) {
                IRevealableItem item = items.Dequeue();
                item.RevealItem();
                if (item is BlockCoin)
                {
                    Systems.Events.TheInstance.Coin();
                }
            }
            State.HandleCollision(false);
            if (HasNoItems()) {
                this.sprite = this.spriteFactory.CreateProduct(BlockTypes.Used);
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
            sprite.Update(gameTime, graphicsDevice);
            State.HandleUpdate(gameTime);
        }
    }
}
