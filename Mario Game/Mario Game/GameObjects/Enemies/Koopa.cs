using MarioGame.Collision;
using MarioGame.Factories;
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
    class Koopa : GameObject
    {
        private KoopaState koopaStateMachine;

        public Gravity gravity;

        public bool isWalkingLeft;

        public bool isRedKoopa;

        public KoopaState GetState
        {
            get
            {
                return this.koopaStateMachine;
            }
        }

        public Koopa(IFactory spriteFactory, Point position, bool isRedKoopa) : base(spriteFactory, position, new Vector2())
        {
            this.isRedKoopa = isRedKoopa;
            this.gravity = new Gravity(this);
            this.koopaStateMachine = new KoopaWalkLeftState(this);

            this.hitboxColor = Color.Red;
            this.hitboxOffset = -2;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is Mario)
            {
                if (((Mario)gameObject).IsInvincible())
                {
                    this.koopaStateMachine.HandleFireBallDeathTransition(true);
                }
                else if (collisionDirection == CollisionDirection.Top)
                {
                    if (!(this.koopaStateMachine is KoopaShellState))
                    {
                        this.koopaStateMachine.HandleShellTransition();
                    }
                    else
                    {
                        if (this.XSpeed != 0.0f)
                        {
                            this.XSpeed = 0.0f;
                        }
                        else
                        {
                            if (((Mario)gameObject).PositionInGame.X > this.positionInGame.X)
                            {
                                this.XSpeed = -5.5f;
                            }
                            else
                            {
                                this.XSpeed = 5.5f;
                            }
                        }
                    }
                }
                else if (collisionDirection == CollisionDirection.Right)
                {
                    if (this.koopaStateMachine is KoopaShellState && this.XSpeed == 0.0f)
                    {
                        this.XSpeed = -5.5f;
                    }
                    else if (this.koopaStateMachine is KoopaFeetOutOfShellState)
                    {
                        this.koopaStateMachine.HandleShellTransition();
                        this.XSpeed = -5.5f;
                    }
                }
                else if (collisionDirection == CollisionDirection.Left)
                {
                    if (this.koopaStateMachine is KoopaShellState && this.XSpeed == 0.0f)
                    {
                        this.XSpeed = 5.5f;
                    }
                    else if (this.koopaStateMachine is KoopaFeetOutOfShellState)
                    {
                        this.koopaStateMachine.HandleShellTransition();
                        this.XSpeed = 5.5f;
                    }
                }
            }
            else if (gameObject is Block || gameObject is Gate || gameObject is Button)
            {
                if (collisionDirection == CollisionDirection.Bottom || collisionDirection == CollisionDirection.None)
                {
                    if (gameObject is StateBlock && ((StateBlock)gameObject).State is BumpedState)
                    {
                        this.YSpeed = -6;
                        this.koopaStateMachine.HandleFireBallDeathTransition(true);
                    }
                    else
                    {
                        this.YSpeed = 0.0f;
                    }
                }
                else if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                {
                    if (this.koopaStateMachine is KoopaWalkLeftState || this.koopaStateMachine is KoopaWalkRightState)
                    {
                        if (this.isWalkingLeft)
                        {
                            this.koopaStateMachine = new KoopaWalkRightState(this);
                        }
                        else
                        {
                            this.koopaStateMachine = new KoopaWalkLeftState(this);
                        }
                    }
                    else if (!(this.koopaStateMachine is KoopaFireBallDeathState))
                    {
                        this.XSpeed = -this.XSpeed;
                    }
                }
            }
            else if (gameObject is Koopa || gameObject is Goomba)
            {
                if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                {
                    if (this.koopaStateMachine is KoopaWalkLeftState || this.koopaStateMachine is KoopaWalkRightState)
                    {
                        if (this.isWalkingLeft)
                        {
                            this.koopaStateMachine = new KoopaWalkRightState(this);
                        }
                        else
                        {
                            this.koopaStateMachine = new KoopaWalkLeftState(this);
                        }
                    }
                }
            }
            else if (gameObject is FireBall || gameObject is Sword)
            {
                if (gameObject.PositionInGame.X < this.positionInGame.X)
                {
                    this.koopaStateMachine.HandleFireBallDeathTransition(true);
                }
                else
                {
                    this.koopaStateMachine.HandleFireBallDeathTransition(false);
                }
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            this.koopaStateMachine.Update(graphicsDevice);
            this.gravity.Update(gameTime);
            this.Sprite.Update(gameTime, graphicsDevice);
        }

        public void ChangeKoopaState(KoopaState state)
        {
            this.koopaStateMachine = state;

            if (this.koopaStateMachine is KoopaFeetOutOfShellState || this.koopaStateMachine is KoopaShellState)
            {
                this.HitboxType = HitboxTypes.Half;
            }
            else
            {
                this.HitboxType = HitboxTypes.Full;
            }
        }
    }
}
