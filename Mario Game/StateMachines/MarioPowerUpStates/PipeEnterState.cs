using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using MarioGame.StateMachines.MarioActionStates;
using MarioGame.View;
using Microsoft.Xna.Framework;
using MarioGame.Events;
using MarioGame.GameObjects;

namespace MarioGame.StateMachines.MarioPowerUpStates
{
    class PipeEnterState : MarioPowerUpState
    {
        private int descendDistance = 64;
        private MarioPowerUpState lastPower;
        private MarioActionState lastAction;
        private int warpX;
        private int warpY;

        public PipeEnterState(Mario mario, int warpX, int warpY, MarioActionState lastAction, MarioPowerUpState lastPower)
        {
            Systems.Events.TheInstance.Warp();
            this.mario = mario;
            this.lastPower = lastPower;
            this.lastAction = lastAction;
            this.descendDistance = this.mario.PositionInGame.Y + this.descendDistance;
            this.warpX = warpX;
            this.warpY = warpY;
        }

        public override void HandleDamageMarioTransition()
        {
        }

        public override void HandleFireMarioTransition()
        {
        }

        public override void HandleSmallMarioTransition()
        {
        }

        public override void HandleSuperMarioTransition()
        {
        }

        public override void Update()
        {
            this.mario.IsCollidable = false;
            this.mario.gravity.Disable();
            this.mario.YSpeed = 1.0f;
            this.mario.XSpeed = 0.0f;
            this.mario.ChangeActionState(new MarioJumpState(this.mario, false, lastAction));
            if (this.mario.IsFacingLeft)
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleLeft);
            }
            else
            {
                this.mario.Sprite = this.mario.SpriteFactory.CreateProduct(MarioTypes.IdleRight);
            }

            if (this.mario.PositionInGame.Y >= this.descendDistance)
            {
                this.mario.gravity.Enable();

                this.mario.PositionInGame = new Point(warpX, warpY);
                if (warpX > Camera.AquireInstance().LevelWidth + 512)
                {
                    int width = Camera.AquireInstance().VirtualWidth;
                    int height = Camera.AquireInstance().VirtualHeight;
                    Camera.AquireInstance().Limits = new Rectangle(Camera.AquireInstance().LevelWidth + width, 0, width * 10, height);
                    BlockUpdateEvent.GetInstance().ChangeBlocksToUndergroundBlocks();
                    PortalGun.ResetPortals();
                }
                else if (warpX > Camera.AquireInstance().LevelWidth)
                {
                    int width = Camera.AquireInstance().VirtualWidth;
                    int height = Camera.AquireInstance().VirtualHeight;
                    Camera.AquireInstance().Limits = new Rectangle(Camera.AquireInstance().LevelWidth, 0, width, height);
                    BlockUpdateEvent.GetInstance().ChangeBlocksToUndergroundBlocks();
                    PortalGun.ResetPortals();
                }
                else
                {
                    int height = Camera.AquireInstance().VirtualHeight;
                    Camera.AquireInstance().Limits = new Rectangle(0, 0, Camera.AquireInstance().LevelWidth, height);
                    BlockUpdateEvent.GetInstance().ChangeBlocksToAbovegroundBlocks();
                    PortalGun.ResetPortals();
                }

                this.mario.ChangeActionState(new MarioJumpState(this.mario, false, lastAction));
                this.mario.ChangePowerUpState(new PipeExitState(this.mario, lastAction, lastPower));

                this.mario.IsCollidable = true;
            }
        }
    }
}
