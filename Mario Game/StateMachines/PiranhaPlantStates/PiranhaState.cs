using MarioGame.GameObjects.Enemies;

namespace MarioGame.StateMachines.PiranhaPlantStates
{
    abstract class PiranhaState
    {
        protected PiranhaPlant piranhaPlant;

        public abstract void HandlePiranhaDeathTransition();

        public abstract void Update();
    }
}
