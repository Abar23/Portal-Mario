using MarioGame.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.BrickBlockStates
{
    class QuestionStillState : BlockState
    {
        private QuestionBlock block;

        public QuestionStillState(QuestionBlock block)
        {
            this.block = block;
        }

        public override void HandleCollision(bool marioPoweredUp)
        {
            block.State = new BumpedState(this.block, this);
        }

        public override void HandleUpdate(GameTime gameTime)
        {
        }
    }
}
