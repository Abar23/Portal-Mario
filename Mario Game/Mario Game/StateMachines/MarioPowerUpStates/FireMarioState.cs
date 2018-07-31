using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class FireMarioState : MarioPowerUpState
    {

        public FireMarioState(Mario mario)
        {
            this.mario = mario;
            this.mario.PoweredUp = true;
            this.mario.SpriteFactory = FireMarioFactory.GetInstance();
            this.mario.UpdateActionSprite();
        }

        public override void HandleDamageMarioTransition()
        {
            this.mario.ChangePowerUpState(new SmallMarioState(this.mario, true));
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
