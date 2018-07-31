using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines.ItemStates;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Blocks;
using MarioGame.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class Mushroom : GameObject, IRevealableItem
    {
        private ItemState itemState;

        private Gravity gravity;

        public Mushroom(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.sprite = this.spriteFactory.CreateProduct(ItemTypes.Mushroom);
            this.itemState = new ItemHiddenState(this);
            this.gravity = new Gravity(this);

            this.hitboxOffset = 1;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                Systems.Events.TheInstance.PowerUp();
                Mario.GetInstance().RequestSuperMarioTransition();
                this.IsCollidable = false;
                this.IsVisible = false;
            }
            else if (gameObject is Block)
            {
                if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                {
                    this.XSpeed = -this.XSpeed;
                }
                else if (collisionDirection == CollisionDirection.Bottom)
                {
                    if (((Block) gameObject).YSpeed < 0)
                    {
                        this.YSpeed = -2.0f;
                        this.XSpeed *= -1.0f;
                    }
                    else
                    {
                        this.YSpeed = 0.0f;
                    }
                }
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
