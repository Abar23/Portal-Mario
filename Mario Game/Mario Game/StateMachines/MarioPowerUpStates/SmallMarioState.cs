using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class SmallMarioState : MarioPowerUpState
    {
        private bool wasDamaged;

        private int transitionCounter;

        public SmallMarioState(Mario mario, bool wasDamaged)
        {
            this.mario = mario;
            this.mario.PoweredUp = false;
            this.mario.IsMarioDead = false;
            this.mario.SpriteFactory = SmallMarioFactory.GetInstance();
            this.mario.UpdateActionSprite();
            this.wasDamaged = wasDamaged;
            this.transitionCounter = 0;
        }

        public override void HandleDamageMarioTransition()
        {
            if(!this.wasDamaged || this.transitionCounter == 120)
            {
                this.mario.ChangePowerUpState(new DeadMarioState(this.mario));
            }
        }

        public override void HandleFireMarioTransition()
        {
            this.mario.ChangePowerUpState(new FireMarioState(this.mario));
        }

        public override void HandleSmallMarioTransition()
        {
        }

        public override void HandleSuperMarioTransition()
        {
            this.mario.ChangePowerUpState(new SuperMarioState(this.mario, true));
        }

        public override void Update()
        {
            if(this.transitionCounter < 120)
            {
                this.transitionCounter++;
            }
        }
    }
}
