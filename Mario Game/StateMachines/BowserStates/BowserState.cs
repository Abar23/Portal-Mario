using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarioGame.GameObjects.Enemies;

namespace MarioGame.StateMachines.BowserStates
{
    abstract class BowserState
    {
        protected Bowser bowser;

        public abstract void HandleIdleTransition();

        public abstract void HandleChargeTransition();

        public abstract void HandleFireBreathTransition();

        public abstract void HandleShellTransition();

        public abstract void HandleDeadTransition();

        public abstract void UpdateSprite();
    }
}
