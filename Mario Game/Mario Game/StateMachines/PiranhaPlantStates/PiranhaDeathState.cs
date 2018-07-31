using MarioGame.GameObjects.Enemies;
using MarioGame.Systems;

namespace MarioGame.StateMachines.PiranhaPlantStates
{
    class PiranhaDeathState : PiranhaState
    {
        public PiranhaDeathState(PiranhaPlant piranhaPlant)
        {
            this.piranhaPlant = piranhaPlant;
            this.piranhaPlant.IsCollidable = false;
            this.piranhaPlant.IsVisible = false;
            this.piranhaPlant.XSpeed = 0.0f;
            this.piranhaPlant.YSpeed = 0.0f;
            Systems.Events.TheInstance.KoopaDied();
        }

        public override void HandlePiranhaDeathTransition()
        {
        }

        public override void Update()
        {
        }
    }
}
