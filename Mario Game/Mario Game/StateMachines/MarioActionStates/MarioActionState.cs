using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.MarioActionStates
{
    abstract class MarioActionState
    {
        protected Mario mario;

        public abstract void HandleIdleTransition();

        public abstract void HandleCrouchTransition();

        public abstract void HandleJumpTransition(bool trueJump);

        public abstract void HandleWalkTransition(bool walkingDirection);

        public void HandleFinishTransition()
        {
            this.mario.ChangeActionState(new MarioFinishState(this.mario));
        }

        public abstract void UpdateSprite();
    }
}
