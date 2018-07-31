using MarioGame.GameObjects;
using MarioGame.GameObjects.Blocks;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.BrickBlockStates
{
    class BumpedState : BlockState
    {
        private bool movingUp;
        private float totalYChange;
        private int originalY;
        private Point position;
        private StateBlock block;
        private BlockState nextState;

        public BumpedState(StateBlock block, BlockState nextState)
        {
            this.position = block.PositionInGame;
            this.block = block;
            this.nextState = nextState;

            this.movingUp = true;
            this.totalYChange = 0;
            this.originalY = this.position.Y;
        }

        public override void HandleUpdate(GameTime gameTime)
        {
            if (movingUp)
            {
                totalYChange -= 4f;
                movingUp = (totalYChange > -20f);
                totalYChange = (movingUp ? totalYChange : -20);
                this.position.Y = originalY + (int)totalYChange;
            }
            else if (totalYChange == 0f)
            {
                block.State = nextState;
            }
            else
            {
                totalYChange += gameTime.ElapsedGameTime.Milliseconds / 2f;
                totalYChange = (totalYChange < 0 ? totalYChange : 0);
                this.position.Y = originalY + (int)totalYChange;
            }
            float dy = this.position.Y - block.PositionInGame.Y;
            this.block.YSpeed = dy;
        }
    }
}
