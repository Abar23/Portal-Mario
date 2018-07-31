using MarioGame.GameObjects.Player;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    abstract class MarioPowerUpState
    {
        protected Mario mario;

        public abstract void HandleSmallMarioTransition();

        public abstract void HandleSuperMarioTransition();

        public abstract void HandleFireMarioTransition();

        public abstract void HandleDamageMarioTransition();

        public abstract void Update();
    }
}
