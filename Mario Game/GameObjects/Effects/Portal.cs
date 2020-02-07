using MarioGame.StateMachines.PortalStates;
using MarioGame.Factories;
using MarioGame.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Effects
{
    class Portal : GameObject
    {
        private PortalState portalState;

        private bool isBluePortal;

        private CollisionDirection directionOfPortal;

        public PortalState GetState
        {
            get => this.portalState;
        }

        public bool IsBluePortal
        {
            get => isBluePortal;
        }

        public CollisionDirection DirectionOfPortal
        {
            get => this.directionOfPortal;
            set => this.directionOfPortal = value;
        }

        public Portal(bool isBluePortal) : base(null, new Point(), new Vector2())
        {
            this.portalState = new ReadyToBeShotState(this);
            this.SpriteFactory = EffectsFactory.GetInstance();
            this.HitboxType = HitboxTypes.Full;
            this.isBluePortal = isBluePortal;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            this.portalState.HandleCollision(gameObject, collisionDirection);
        }

        public void HandleReadyToBeShotState()
        {
            this.portalState.HandleReadyToBeShot();
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.Sprite.Update(gameTime, graphicsDevice);
            this.portalState.Update();
        }

        public void ChangePortalState(PortalState state)
        {
            this.portalState = state;
        }
    }
}
