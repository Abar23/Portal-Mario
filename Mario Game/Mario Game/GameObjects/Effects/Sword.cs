using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Enemies;
using MarioGame.Physics;
using MarioGame.StateMachines.SwordStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Effects
{
    class Sword : GameObject
    {
        public Gravity gravity;

        public SwordState SwordState { get; set; }

        public Sword() : base(null, new Point(), new Vector2())
        {
            this.gravity = new Gravity(this);
            this.gravity.ChangeGravityIntensity(1.0f);
            this.SpriteFactory = EffectsFactory.GetInstance();
            this.SwordState = new ReadyToSwingState(this);
            this.hitboxOffset = 0;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Block && !(gameObject is HiddenBlock))
            {
                this.SwordState.HandleReadyToSwingTransition();
            }
        }

        public void SwingSword()
        {
            this.SwordState.HangleSwingingTransition();
        }

        public void ResetSword()
        {
            this.SwordState.HandleReadyToSwingTransition();
        }

        public void ChangeGravity(float gravityIntensity)
        {
            this.gravity.ChangeGravityIntensity(gravityIntensity);
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.gravity.Update(gameTime);
            this.SwordState.Update(graphicsDevice);
            this.Sprite.Update(gameTime, graphicsDevice);
        }

        public void ChangeSwordState(SwordState state)
        {
            this.SwordState = state;
        }
        
    }
}
