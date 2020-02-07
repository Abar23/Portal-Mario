using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines;
using MarioGame.StateMachines.ItemStates;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Player;
using MarioGame.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class Star : GameObject, IRevealableItem
    {
        private const float UpdawardVelocityAfterBounce = -4.0f;

        private ItemState itemState;

        private Gravity gravity;

        public Star(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(ItemTypes.Star);
            this.itemState = new ItemHiddenState(this);

            this.gravity = new Gravity(this);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Block)
            {
                if (collisionDirection == CollisionDirection.Bottom)
                {
                    if (this.itemState is BouncingStarState)
                    {
                        this.YSpeed = UpdawardVelocityAfterBounce;
                    }
                    else if (this.itemState is MovingItemState)
                    {
                        if (this.YSpeed > 1.6f)
                        {
                            this.itemState.HandleMovingItemTransition();
                        }
                        else
                        {
                            this.YSpeed = 0.0f;
                        }
                    }
                }
                else if (collisionDirection != CollisionDirection.Top)
                {
                    this.XSpeed = -this.XSpeed;
                }
            }
            else if (gameObject is Mario)
            {
                this.IsVisible = false;
                this.IsCollidable = false;

                Systems.Events.TheInstance.PowerUp();
                Mario.GetInstance().RequestInvincibility();

                this.XSpeed = 0.0f;
                this.YSpeed = 0.0f;

                this.gravity.Disable();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (!(this.itemState is ItemHiddenState || this.itemState is RevealItemState))
            {
                this.gravity.Update(gameTime);
            }

            this.itemState.Update();
            sprite.Update(gameTime, graphicsDevice);
        }

        public void ChangeItemState(ItemState state)
        {
            this.itemState = state;
        }

        public void RevealItem()
        {
            Systems.Events.TheInstance.ItemReveal();
            this.ChangeItemState(new RevealItemState(this));
        }
    }
}
