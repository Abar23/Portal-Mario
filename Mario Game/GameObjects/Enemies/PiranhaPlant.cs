using MarioGame.Factories;
using MarioGame.GameObjects.Effects;
using MarioGame.StateMachines.PiranhaPlantStates;
using MarioGame.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.GameObjects.Player;

namespace MarioGame.GameObjects.Enemies
{
    class PiranhaPlant : GameObject
    {
        private PiranhaState piranhaState;

        public PiranhaPlant(IFactory spriteFactory, Point position) : base(spriteFactory, position, new Vector2())
        {
            this.piranhaState = new MovingPiranhaPlantState(this);
            this.hitboxColor = Color.Red;
            this.HitboxType = HitboxTypes.Full;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is FireBall || gameObject is Sword || (gameObject is Mario && ((Mario)gameObject).IsInvincible()))
            {
                this.piranhaState.HandlePiranhaDeathTransition();
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.Sprite.Update(gameTime, graphicsDevice);
            this.piranhaState.Update();
        }

        public void ChangePiranhaState(PiranhaState state)
        {
            this.piranhaState = state;
        }
    }
}
