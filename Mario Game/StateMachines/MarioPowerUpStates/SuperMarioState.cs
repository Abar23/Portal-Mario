using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class SuperMarioState : MarioPowerUpState
    {
        private int fireMarioTransitionCounter;

        bool wasSmallMario;

        public SuperMarioState(Mario mario, bool wasSmallMario)
        {
            this.mario = mario;
            this.mario.PoweredUp = true;
            this.mario.SpriteFactory = SuperMarioFactory.GetInstance();
            this.mario.UpdateActionSprite();
            this.fireMarioTransitionCounter = 0;
            this.wasSmallMario = wasSmallMario;
        }

        public override void HandleDamageMarioTransition()
        {
            this.mario.ChangePowerUpState(new SmallMarioState(this.mario, true));
        }

        public override void HandleFireMarioTransition()
        {
            if (this.fireMarioTransitionCounter == 120 || this.wasSmallMario)
            {
                this.mario.ChangePowerUpState(new FireMarioState(this.mario));
            }
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
            if (this.fireMarioTransitionCounter < 120)
            {
                this.fireMarioTransitionCounter++;
            }
        }
    }
}
