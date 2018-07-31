using System;
using System.Collections.Generic;
using MarioGame.Collision;
using MarioGame.Controllers;
using MarioGame.Factories;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects.Player;
using MarioGame.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Other
{
    class CompanionCube : GameObject
    {
        private static HashSet<CompanionCube> cubes = new HashSet<CompanionCube>();

        private static CompanionCube selectedCube;

        Gravity Gravity {
            get;
            set;
        }

        private Point originalPosition;

        public CompanionCube(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.hitboxOffset = 0;
            this.originalPosition = positionInGame;
            this.sprite = spriteFactory.CreateProduct(BlockTypes.CompanionCube);
            Gravity = new Gravity(this);
            cubes.Add(this);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Koopa || gameObject is Goomba || gameObject is PiranhaPlant || gameObject is Bowser || gameObject is BowserFireball)
            {
                if (selectedCube == this) {
                    selectedCube = null;
                }
                this.collidable = false;
                Systems.Events.TheInstance.CompanionDied();
            } else if (collisionDirection == CollisionDirection.Bottom && gameObject.IsSolid)
            {
                this.YSpeed = gameObject.YSpeed;
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (selectedCube == this) {
                double angle = MouseController.DetermineAngleFromMouse(Mario.GetInstance().GetCenter());
                int desiredX = (int) (56 * Math.Cos(angle)) + Mario.GetInstance().GetCenter().X - this.GetHitBox().Width / 2;
                int desiredY = (int)(56 * Math.Sin(angle)) + Mario.GetInstance().GetCenter().Y - this.GetHitBox().Height / 2;
                this.XSpeed = (desiredX - this.positionInGame.X) / 5;
                this.YSpeed = (desiredY - this.positionInGame.Y) / 5;
            }
            else {
                if (XSpeed > 12)
                {
                    XSpeed = 12;
                }
                else if (XSpeed < -12)
                {
                    XSpeed = -12;
                }
                else if (YSpeed < -10)
                {
                    YSpeed = -10;
                }
                this.XSpeed = (XSpeed * .96f);
                this.Gravity.Update(gameTime);
            }

            if (this.positionInGame.Y > 800)
            {
                if (IsCollidable) {
                    Systems.Events.TheInstance.CompanionDied();
                } else
                {
                    collidable = true;
                }
                MarioGame.GetInstance.GetMap().Remove(this);
                this.positionInGame = originalPosition;
                this.XSpeed = 0;
                this.YSpeed = 0;
                MarioGame.GetInstance.GetMap().Add(this);
            }
        }

        public static void SelectCube()
        {
            selectedCube = null;
            int marioX = Mario.GetInstance().GetCenter().X;
            int marioY = Mario.GetInstance().GetCenter().Y;
            foreach (CompanionCube cube in cubes)
            {
                if (cube.IsCollidable && MouseController.MouseOverPosition(cube.GetHitBox()))
                {
                    if ((marioX - cube.positionInGame.X) * (marioX - cube.positionInGame.X) + (marioY - cube.positionInGame.Y) * (marioY - cube.positionInGame.Y) < 10000) {
                        selectedCube = cube;
                    }
                }
            }
        }
    }
}
