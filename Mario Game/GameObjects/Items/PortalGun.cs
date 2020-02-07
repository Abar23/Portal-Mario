using System.Web.SessionState;
using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.Physics;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Effects;
using MarioGame.StateMachines.ItemStates;
using MarioGame.StateMachines.PortalStates;
using MarioGame.StateMachines.MarioPowerUpStates;
using MarioGame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects
{
    class PortalGun : GameObject, IRevealableItem
    {
        private static PortalGun instance;

        private bool trackMariosPosition;

        private ItemState itemState;

        private Gravity gravity;

        private static Portal bluePortal;

        private static Portal orangePortal;

        public static Portal GetBluePortal
        {
            get => bluePortal;
        }

        public static Portal GetOrangePortal
        {
            get => orangePortal;
        }

        public bool TrackingMarioPosition
        {
            get => this.trackMariosPosition;
        }

        private PortalGun(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.Sprite = this.SpriteFactory.CreateProduct(ItemTypes.PortalGun);
            this.itemState = new ItemHiddenState(this);
            this.gravity = new Gravity(this);
            this.trackMariosPosition = false;
            bluePortal = new Portal(true);
            orangePortal = new Portal(false);
        }

        public static PortalGun CreateInstance(IFactory spriteFactory, Point positionInGame)
        {
            return instance = new PortalGun(spriteFactory, positionInGame);
        }

        public static PortalGun GetInstance()
        {
            return instance;
        }

        public void ChangeItemState(ItemState state)
        {
            this.itemState = state;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                this.trackMariosPosition = true;
                this.IsCollidable = false;
                this.gravity.Disable();
            }   
        }

        public void RevealItem()
        {
            Systems.Events.TheInstance.ItemReveal();
            this.ChangeItemState(new RevealItemState(this));
        }

        public static void GeneratePortal(bool isBluePortal, GameObject obj, CollisionDirection collisionDirection)
        {
            if (isBluePortal)
            {
                if (bluePortal.GetState is ActiveState)
                {
                    bluePortal.HandleReadyToBeShotState();
                }
                bluePortal.HandleCollision(collisionDirection, obj);
            }
            else
            {
                if (orangePortal.GetState is ActiveState)
                {
                    orangePortal.HandleReadyToBeShotState();
                }
                orangePortal.HandleCollision(collisionDirection, obj);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.IsVisible)
            {
                if (this.trackMariosPosition)
                {
                    Point centerOfGun = GetCenterOfPortalGun();
                    float angle = (float)MouseController.DetermineAngleFromMouse(this.positionInGame);
                    if (angle <= 1.7f && angle >= -1.6)
                    {
                        this.sprite.Draw(
                            spriteBatch,
                            tint,
                            this.positionInGame,
                            new Vector2(centerOfGun.X, centerOfGun.Y),
                            angle,
                            SpriteEffects.None);
                    }
                    else
                    {
                        this.sprite.Draw(
                            spriteBatch,
                            tint,
                            this.positionInGame,
                            new Vector2(centerOfGun.X, centerOfGun.Y),
                            angle,
                            SpriteEffects.FlipVertically);
                    }
                }
                else
                {
                    this.sprite.Draw(spriteBatch, Color.White, this.positionInGame);
                }

                bluePortal.Draw(spriteBatch);
                orangePortal.Draw(spriteBatch);
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.sprite.Update(gameTime, graphicsDevice);

            this.itemState.Update();

            if (this.trackMariosPosition)
            {
                this.SetXPositionInGame = Mario.GetInstance().PositionInGame.X + 24;

                if (Mario.GetInstance().GetPowerUpState is SuperMarioState || Mario.GetInstance().GetPowerUpState is FireMarioState)
                {
                    this.SetYPositionInGame = Mario.GetInstance().PositionInGame.Y + 50;
                }
                else
                {
                    this.SetYPositionInGame = Mario.GetInstance().PositionInGame.Y + 68;
                }
            }
        }

        private static Point GetCenterOfPortalGun()
        {
            return new Point((int)ItemFactory.ItemSizesInPixels.PortalGunWidth / 2,
                (int)ItemFactory.ItemSizesInPixels.PortalGunHeight / 2);
        }

        public static void ResetPortals()
        {
            orangePortal.GetState.HandleReadyToBeShot();
            bluePortal.GetState.HandleReadyToBeShot();
        }
    }
}
