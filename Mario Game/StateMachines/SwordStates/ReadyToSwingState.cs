using MarioGame.GameObjects.Effects;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.SwordStates
{
    class ReadyToSwingState : SwordState
    {
        public ReadyToSwingState(Sword sword)
        {
            this.sword = sword;
            this.sword.IsCollidable = false;
            this.sword.XSpeed = 0.0f;
            this.sword.YSpeed = 0.0f;
            this.sword.gravity.Disable();
            this.sword.Sprite = new NullSprite(null, new Point(), new Point());
        }

        public override void HandleReadyToSwingTransition()
        {
        }

        public override void HangleSwingingTransition()
        {
            this.sword.ChangeSwordState(new SwingingState(this.sword));
        }

        public override void Update(GraphicsDevice graphicsDevice)
        {
        }
    }
}
