using MarioGame.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.BrickBlockStates
{
    class BrickStillState : BlockState
    {
        private bool moveUp;
        private BrickBlock block;

        public BrickStillState(BrickBlock block)
        {
            this.moveUp = false;
            this.block = block;
        }

        public override void HandleCollision(bool input)
        {
            moveUp = !input;
            if (!moveUp)
            {
                block.State = new BumpedState(this.block, new BrickBrokenState(this.block));
            }
        }

        public override void HandleUpdate(GameTime gameTime)
        {
            if (moveUp)
            {
                this.moveUp = false;
                block.State = new BumpedState(this.block, this);
            }
        }
    }
}
