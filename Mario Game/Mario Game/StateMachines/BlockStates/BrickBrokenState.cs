using MarioGame.GameObjects;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.BrickBlockStates
{
    class BrickBrokenState : BlockState
    {
        private BrickBlock block;

        public BrickBrokenState(BrickBlock block)
        {
            this.block = block;
        }

        public override void HandleUpdate(GameTime gameTime)
        {
            this.block.IsCollidable = false;
            this.block.YSpeed = 5;
        }
    }
}
