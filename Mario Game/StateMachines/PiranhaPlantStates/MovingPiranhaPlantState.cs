using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects.Player;
using MarioGame.Factories;

namespace MarioGame.StateMachines.PiranhaPlantStates
{
    class MovingPiranhaPlantState : PiranhaState
    {
        private int movementPauseTimer;

        private int heightToMove;

        private int originalYPosition;
        
        public MovingPiranhaPlantState(PiranhaPlant piranhaPlant)
        {
            this.piranhaPlant = piranhaPlant;
            this.piranhaPlant.Sprite = this.piranhaPlant.SpriteFactory.CreateProduct(EnemyTypes.Piranha);
            this.piranhaPlant.IsCollidable = true;
            this.movementPauseTimer = 0;
            this.piranhaPlant.YSpeed = -1.0f;
            this.originalYPosition = this.piranhaPlant.PositionInGame.Y;
            this.heightToMove = this.piranhaPlant.PositionInGame.Y - (int)EnemyFactory.EnemySizesInPixels.PiranhaHeight; 
        }

        public override void HandlePiranhaDeathTransition()
        {
            this.piranhaPlant.ChangePiranhaState(new PiranhaDeathState(this.piranhaPlant));
        }

        public override void Update()
        {
            if (this.piranhaPlant.YSpeed != 0.0f)
            {
                if (this.piranhaPlant.PositionInGame.Y <= this.heightToMove)
                {
                    this.piranhaPlant.YSpeed = 0.0f;
                }
                else if (this.piranhaPlant.PositionInGame.Y > this.originalYPosition)
                {
                    this.piranhaPlant.YSpeed = 0.0f;
                    this.piranhaPlant.IsCollidable = false;
                }
            }
            else
            {
                this.movementPauseTimer++;
                
                if (movementPauseTimer >= 60)
                {
                    if (this.piranhaPlant.PositionInGame.Y <= this.heightToMove)
                    {
                        this.piranhaPlant.YSpeed = 1.0f;
                        this.movementPauseTimer = 0;
                        this.piranhaPlant.IsCollidable = true;
                    }
                    else if (this.piranhaPlant.PositionInGame.Y > this.originalYPosition && !CheckForMario(this.piranhaPlant))
                    {
                        this.piranhaPlant.YSpeed = -1.0f;
                        this.movementPauseTimer = 0;
                        this.piranhaPlant.IsCollidable = true;
                    }
                }
            }
        }

        public static bool CheckForMario(PiranhaPlant piranhaPlant)
        {
            return (Mario.GetInstance().PositionInGame.X <= (piranhaPlant.PositionInGame.X + 2 * (int)EnemyFactory.EnemySizesInPixels.PiranhaWidth)
                && Mario.GetInstance().PositionInGame.X > (piranhaPlant.PositionInGame.X - 2 * (int)EnemyFactory.EnemySizesInPixels.PiranhaWidth));
        }
    }
}
