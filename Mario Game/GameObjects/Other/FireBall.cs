using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines.FireBallStates;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Enemies;
using MarioGame.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Effects
{
    class FireBall : GameObject
    {
        private FireBallState fireBallState;

        public Gravity gravity;

        private float ySpeed = -9.0f;

        public FireBallState GetFireBallState
        {
            get => this.fireBallState;
        }

        public FireBall() : base(EffectsFactory.GetInstance(), new Point(), new Vector2())
        {
            this.gravity = new Gravity(this);
            this.gravity.ChangeGravityIntensity(4.25f);
            this.fireBallState = new ReadyToBeSpatState(this);
            this.hitboxOffset = -4;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Block && !(gameObject is HiddenBlock))
            {
                if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right || collisionDirection == CollisionDirection.None)
                {
                    this.fireBallState.HandleDestroyedFireBallTransition();
                }
                else if (collisionDirection == CollisionDirection.Top)
                {
                    this.YSpeed = this.ySpeed * -1;
                }
                else if (collisionDirection == CollisionDirection.Bottom)
                {
                    this.YSpeed = this.ySpeed;
                }
            }
            else if (gameObject is Koopa || gameObject is Goomba || gameObject is PiranhaPlant || gameObject is Bowser)
            {
                this.fireBallState.HandleDestroyedFireBallTransition();
            }
        }

        public void SpitFireBall(float x, float y)
        {
            this.fireBallState.HandleBouncingFireBallTransition(x, y);
        }

        public void ResetFireBall()
        {
            this.fireBallState.HandleReadyToBeSpatTransition();
        }

        public void ChangeGravityAndYSpeed(float gravityIntensity, float y)
        {
            this.gravity.ChangeGravityIntensity(gravityIntensity);
            this.ySpeed = y;
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.gravity.Update(gameTime);
            this.fireBallState.Update(graphicsDevice);
            this.Sprite.Update(gameTime, graphicsDevice);
        }

        public void ChangeFireBallState(FireBallState state)
        {
            this.fireBallState = state;
        }
    }
}
