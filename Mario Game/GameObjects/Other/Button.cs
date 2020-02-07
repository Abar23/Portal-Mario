using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Effects;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Other
{
    class Button : GameObject
    {
        public bool PushedIn { get; private set; }
        private int counter;
        private bool inverted;
        private ISprite pushedIn;
        private ISprite pushedOut;

        public Button(IFactory spriteFactory, Point positionInGame) : this(false, spriteFactory, positionInGame)
        {
        }

        public Button(bool inverted, IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.inverted = inverted;
            this.hitboxOffset = 0;
            this.hitboxColor = Color.White;
            this.sprite = this.spriteFactory.CreateProduct(BlockTypes.ButtonOut);
            this.solid = true;
            this.pushedIn = this.spriteFactory.CreateProduct(BlockTypes.ButtonPushedIn);
            this.pushedOut = this.spriteFactory.CreateProduct(BlockTypes.ButtonOut);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if ((collisionDirection == CollisionDirection.Top || collisionDirection == CollisionDirection.None) && !(gameObject is PortalProjectile)) {
                counter = 0;
                if (!PushedIn) {
                    PushedIn = true;
                    this.sprite = pushedIn;
                }
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (++counter > 10) {
                PushedIn = false;
                this.sprite = pushedOut;
            }
        }

        public bool Active()
        {
            if (inverted)
            {
                return !PushedIn;
            }

            return PushedIn;
        }
    }
}
