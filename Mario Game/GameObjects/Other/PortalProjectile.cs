using MarioGame.Collision;
using MarioGame.Controllers;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Blocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.GameObjects.Effects
{
    class PortalProjectile : GameObject
    {
        private static readonly int SPEED = 10;

        private bool left;

        public PortalProjectile(bool left) : base(EffectsFactory.GetInstance(), Mario.GetInstance().GetCenter(), new Vector2())
        {
            Systems.Events.TheInstance.PortalProjectileFiring();
            double angle = MouseController.DetermineAngleFromMouse(Mario.GetInstance().GetCenter());
            this.XSpeed = (int)(SPEED * Math.Cos(angle));
            this.YSpeed = (int)(SPEED * Math.Sin(angle));

            this.left = left;
            if (this.left) {
                this.sprite = this.spriteFactory.CreateProduct(EffectTypes.BluePortalProjectile);
            } else
            {
                this.sprite = this.spriteFactory.CreateProduct(EffectTypes.OrangePortalProjectile);
            }

            MarioGame.GetInstance.AddNewGameObject(false, this);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Block && !(gameObject is PyramidBlock)) {
                DestroySelf();

                Systems.Events.TheInstance.PortalOpened();

                if (CollisionDirection.Top == collisionDirection)
                {
                    Systems.Events.TheInstance.PortalProjCollision(left, gameObject, CollisionDirection.Bottom);
                }
                else if (CollisionDirection.Bottom == collisionDirection)
                {
                    Systems.Events.TheInstance.PortalProjCollision(left, gameObject, CollisionDirection.Top);
                }
                else if (CollisionDirection.Left == collisionDirection)
                {
                    Systems.Events.TheInstance.PortalProjCollision(left, gameObject, CollisionDirection.Right);
                }
                else if (CollisionDirection.Right == collisionDirection)
                {
                    Systems.Events.TheInstance.PortalProjCollision(left, gameObject, CollisionDirection.Left);
                }
            }
            else if (gameObject.IsSolid)
            {
                DestroySelf();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (positionInGame.Y < -1000 || positionInGame.Y > 2000)
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            this.collidable = false;
            this.IsVisible = false;
            this.XSpeed = 0;
            this.YSpeed = 0;
        }
    }
}
