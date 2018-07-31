using MarioGame.GameObjects;
using System;
using Microsoft.Xna.Framework;

namespace MarioGame.Physics
{
    class Gravity
    {
        private const float TERMINAL_VELOCITY = 13.0f;

        private float gravityIntensity = 0.5f;

        private bool isGravityEnabled;

        private GameObject obj;

        public Gravity(GameObject obj)
        {
            this.obj = obj;
            this.isGravityEnabled = true;
        }

        public void Update(GameTime gameTime)
        {
            if (this.isGravityEnabled)
            {
                this.obj.YSpeed += this.gravityIntensity * ((float)(gameTime.ElapsedGameTime.Milliseconds * gameTime.ElapsedGameTime.Milliseconds) / 1000);

                if (this.obj.YSpeed > TERMINAL_VELOCITY)
                {
                    this.obj.YSpeed = TERMINAL_VELOCITY;
                }
            }
        }

        public void ChangeGravityIntensity(float newGravityIntensity)
        {
            this.gravityIntensity = newGravityIntensity;
        }

        public void Disable()
        {
            this.isGravityEnabled = false;
        }

        public void Enable()
        {
            this.isGravityEnabled = true;
        }
    }
}
