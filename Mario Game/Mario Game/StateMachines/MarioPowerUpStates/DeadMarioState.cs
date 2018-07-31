using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class DeadMarioState : MarioPowerUpState
    {
        public DeadMarioState(Mario mario)
        {
            this.mario = mario;
            this.mario.IsCollidable = false;
            this.mario.YSpeed = -8.0f;
            this.mario.XSpeed = 0.0f;
            this.mario.Sprite = SmallMarioFactory.GetInstance().CreateProduct(MarioTypes.MarioDead);
            this.mario.IsMarioDead = true;
            Systems.Events.TheInstance.MarioDied();
        }

        public override void HandleDamageMarioTransition()
        {
        }

        public override void HandleFireMarioTransition()
        {
        }

        public override void HandleSmallMarioTransition()
        {
            this.mario.ChangePowerUpState(new SmallMarioState(this.mario, false));
        }

        public override void HandleSuperMarioTransition()
        {
        }

        public override void Update()
        {
        }
    }
}
