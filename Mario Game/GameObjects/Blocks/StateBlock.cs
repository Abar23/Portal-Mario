using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.Factories;
using MarioGame.StateMachines.BrickBlockStates;
using Microsoft.Xna.Framework;

namespace MarioGame.GameObjects.Blocks
{
    abstract class StateBlock : Block
    {
        public BlockState State { get; set; }

        public StateBlock(IFactory spriteFactory, Point positionInGame, Vector2 velocityOfObject) : base(spriteFactory, positionInGame, velocityOfObject)
        { }
    }
}
