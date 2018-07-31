using System.Collections.Generic;
using MarioGame.Factories;
using Microsoft.Xna.Framework;

namespace MarioGame.GameObjects.Blocks
{
    abstract class Pipe : Block
    {
        private int warp;
        private int warpX;
        private int warpY;

        public int Warp
        {
            get => this.warp;
            set => this.warp = value;
        }

        public int WarpX
        {
            get => this.warpX;
            set => this.warpX = value;
        }

        public int WarpY
        {
            get => this.warpY;
            set => this.warpY = value;
        }

        public Pipe(IFactory spriteFactory, Point positionInGame, Vector2 velocityOfObject) : base(spriteFactory, positionInGame, velocityOfObject)
        {
            this.hitboxColor = Color.DarkBlue;
            this.hitboxOffset = 0;
            this.solid = true;
            Warp = 0;
            items = new Queue<IRevealableItem>();
        }
    }
}
