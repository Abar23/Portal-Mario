using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines.GoombaStates;
using MarioGame.StateMachines.KoopaStates;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Effects;
using MarioGame.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.StateMachines.BrickBlockStates;
using MarioGame.GameObjects.Other;

namespace MarioGame.GameObjects.Enemies
{
    class Goomba : GameObject
    {
        private GoombaState goombaState;

        private Gravity gravity;

        public Gravity GetGravity
        {
            get => this.gravity;
        }

        public bool isGoombaDead
        {
            set
            {
                this.collidable = !value;
            }
        }

        public Goomba(IFactory spriteFactory, Point initialPositionInGame) : this(spriteFactory, initialPositionInGame, new Vector2())
        {
        }

        public Goomba(IFactory spriteFactory, Point initialPositionInGame, Vector2 initialSpeedInGame) : base(spriteFactory, initialPositionInGame, initialSpeedInGame)
        {
            this.isGoombaDead = false;
            this.goombaState = new GoombaWalkingState(this);
            this.sprite = this.spriteFactory.CreateProduct(EnemyTypes.Goomba);

            this.gravity = new Gravity(this);

            this.hitboxColor = Color.Red;
            this.HitboxType = HitboxTypes.Full;
            this.hitboxOffset = 0;
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.goombaState.Update(gameTime, graphicsDevice);
            this.gravity.Update(gameTime);
            this.sprite.Update(gameTime, graphicsDevice);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                if (collisionDirection == CollisionDirection.Top)
                {
                    this.goombaState.HandleDeathTransistion(gameObject);
                }
                else if (((Mario)gameObject).IsInvincible())
                {
                    this.goombaState.HandleDeathTransistion(gameObject);
                }
            }
            else if (gameObject is Block || gameObject is Gate || gameObject is Button)
            {
                if (collisionDirection == CollisionDirection.Bottom)
                {
                    if (gameObject is StateBlock && ((StateBlock)gameObject).State is BumpedState)
                    {
                        this.YSpeed = -6;
                        this.goombaState.HandleDeathTransistion(null);
                    }
                    else
                    {
                        this.YSpeed = 0.0f;
                    }
                }
                else if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                {
                    this.goombaState.HandleWalkingTransition();
                }
            }
            else if (gameObject is Goomba)
            {
                if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                {
                    this.goombaState.HandleWalkingTransition();
                }
            }
            else if (gameObject is Koopa)
            {
                if (((Koopa)gameObject).GetState is KoopaShellState && gameObject.XSpeed != 0.0f)
                {
                    this.goombaState.HandleDeathTransistion(gameObject);
                }
                else
                {
                    this.goombaState.HandleWalkingTransition();
                }
            }
            else if (gameObject is FireBall || gameObject is Sword)
            {
                this.goombaState.HandleDeathTransistion(gameObject);
            }
        }

        public void ChangeGoombaState(GoombaState state)
        {
            this.goombaState = state;
        }
    }
}
