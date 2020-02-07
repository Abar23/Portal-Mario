using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Enemies;
using MarioGame.Physics;
using MarioGame.GameObjects.Effects;
using MarioGame.StateMachines.KoopaStates;
using MarioGame.StateMachines.MarioActionStates;
using MarioGame.StateMachines.MarioPowerUpStates;
using MarioGame.StateMachines.FireBallStates;
using MarioGame.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.GameObjects.Background;
using MarioGame.GameObjects.Other;

namespace MarioGame.GameObjects.Player
{
    class Mario : GameObject
    {
        private static Mario theInstance;

        private bool crouching = false, rightBlocked = false, leftBlocked = false;

        private int invincibilityTimer = 0;

        private int colorCounter = 0;

        public Gravity gravity;

        private MarioActionState marioActionState;

        private MarioPowerUpState marioPowerUpState;

        private GameObject sword;
        private bool hasSword;
        private GameObject[] spittingFireBalls;
        private float x, y;

        public float GravityIntensity { get; set; }

        public int CheckpointPosition { get; set; }

        public bool Checkpoint { get; set; }

        public bool IsFacingLeft { get; set; }

        public bool PoweredUp { get; set; }

        public bool IsMarioDead { get; set; }

        public MarioPowerUpState GetPowerUpState
        {
            get => this.marioPowerUpState;
        }

        private Mario(IFactory spriteFactory, Point initialPositionInGame, Vector2 initialSpeedInGame) : base(spriteFactory, initialPositionInGame, initialSpeedInGame)
        {
            this.IsFacingLeft = false;
            this.Checkpoint = false;

            this.marioActionState = new MarioIdleState(this);
            this.marioPowerUpState = new SmallMarioState(this, false);

            this.sword = new Sword();

            this.spittingFireBalls = new FireBall[2];
            for (int i = 0; i < 2; i++)
            {
                this.spittingFireBalls[i] = new FireBall();
            }

            HitboxType = HitboxTypes.SmallMario;
            this.hitboxColor = Color.Yellow;
            this.hitboxOffset = 0;

            this.gravity = new Gravity(this);
            this.gravity.ChangeGravityIntensity(1f);
            GravityIntensity = 1f;
        }

        internal bool IsInvincible()
        {
            return invincibilityTimer > 0;
        }

        public static Mario CreateTheInstance(IFactory spriteFactory, Point initialPositionInGame, Vector2 initialSpeedInGame)
        {
            theInstance = new Mario(spriteFactory, initialPositionInGame, initialSpeedInGame);
            return theInstance;
        }

        public static Mario GetInstance()
        {
            return theInstance;
        }

        private void ChangeGravityItensity()
        {
            if (this.marioPowerUpState is FireMarioState)
            {
                this.gravity.ChangeGravityIntensity(GravityIntensity);
            }
            else
            {
                this.gravity.ChangeGravityIntensity(1f);
            }
        }

        public void UpdateFireballsAndSword(int numFireBalls, float fireBallGravity, float ySpeedOfObject, float xInit, float yInit, bool hasASword, float swordGravity)
        {
            this.spittingFireBalls = new FireBall[numFireBalls];
            for (int i = 0; i < numFireBalls; i++)
            {
                this.spittingFireBalls[i] = new FireBall();
                ((FireBall)this.spittingFireBalls[i]).ChangeGravityAndYSpeed(fireBallGravity, ySpeedOfObject);
            }

            this.x = xInit;
            this.y = yInit;
            this.hasSword = hasASword;
            ((Sword) this.sword).ChangeGravity(swordGravity);
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            if (this.PositionInGame.Y > 480 || PlayerHUD.GetInstance().GetTimeRemaining() <= 0)
            {
                if (marioPowerUpState is FireMarioState || marioPowerUpState is SuperMarioState)
                {
                    this.marioPowerUpState.HandleSmallMarioTransition();
                }
                this.marioPowerUpState.HandleDamageMarioTransition();
                PlayerHUD.GetInstance().DecreaseLives();
            }

            if (this.PositionInGame.X > this.CheckpointPosition)
            {
                Checkpoint = true;
            }


            if (!this.IsMarioDead)
            {
                if (YSpeed > 1.1)
                {
                    this.marioActionState.HandleJumpTransition(false);
                }
                else if (marioActionState is MarioWalkState)
                {
                    XSpeed *= .96f;
                    if (XSpeed * XSpeed < .9)
                    {
                        this.marioActionState.HandleIdleTransition();
                    }
                }
                else if (marioActionState is MarioCrouchState && !crouching)
                {
                    marioActionState.HandleIdleTransition();
                }
                crouching = false;
                if (invincibilityTimer > 0)
                {
                    if (invincibilityTimer < 70 && (invincibilityTimer / 10 % 2 == 0))
                    {
                        this.tint = Color.White;
                    }
                    else
                    {
                        this.tint = Color.Purple;
                    }
                }
                else if (colorCounter > 0)
                {
                    colorCounter--;
                }
                else
                {
                    this.tint = Color.White;
                }

                foreach (GameObject obj in this.spittingFireBalls)
                {
                    obj.Update(gameTime, graphicsDevice);
                }

                sword.Update(gameTime, graphicsDevice);

                if (this.positionInGame.X + (int)XSpeed +12 <= 0 || this.positionInGame.X + (int)XSpeed >= 10000 - this.GetHitBox().Width)
                {
                    this.XSpeed = 0.0f;
                }

                if (invincibilityTimer > 0)
                {
                    invincibilityTimer--;
                }
            }

            this.ChangeGravityItensity();
            this.marioPowerUpState.Update();
            this.gravity.Update(gameTime);
            this.sprite.Update(gameTime, graphicsDevice);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (GameObject fireBall in spittingFireBalls)
            {
                fireBall.Draw(spriteBatch);
            }
            sword.Draw(spriteBatch);
        }

        public bool IsPoweredUp()
        {
            return this.PoweredUp;
        }

        public void RequestSmallMarioTransition()
        {
            this.marioPowerUpState.HandleSmallMarioTransition();

            if (this.marioActionState is MarioCrouchState)
            {
                this.marioActionState.HandleJumpTransition(false);
            }
        }

        public void RequestSuperMarioTransition()
        {
            this.marioPowerUpState.HandleSuperMarioTransition();
        }

        public void RequestFireMarioTransition()
        {
            this.marioPowerUpState.HandleFireMarioTransition();
        }

        public void RequestDamageMarioTransition()
        {
            this.marioPowerUpState.HandleDamageMarioTransition();

            if (this.marioActionState is MarioCrouchState)
            {
                this.marioActionState.HandleJumpTransition(false);
            }
        }

        public void RequestJumpTransition()
        {
            if (!this.IsMarioDead)
            {
                this.marioActionState.HandleJumpTransition(true);
            }
        }

        public void RequestCrouchTransition()
        {
            if (!this.IsMarioDead)
            {
                crouching = true;
                if (!(marioActionState is MarioCrouchState))
                {
                    marioActionState.HandleCrouchTransition();
                }
            }
        }

        public void RequestWalkLeft()
        {
            if (marioActionState is MarioJumpState)
            {
                leftBlocked = false;
            }
            if (!this.IsMarioDead && !leftBlocked)
            {
                rightBlocked = false;
                this.marioActionState.HandleWalkTransition(true);
            }
        }

        public void RequestWalkRight()
        {
            if (marioActionState is MarioJumpState)
            {
                rightBlocked = false;
            }
            if (!this.IsMarioDead && !rightBlocked)
            {
                leftBlocked = false;
                this.marioActionState.HandleWalkTransition(false);
            }
        }

        public void RequestInvincibility()
        {
            invincibilityTimer = 360;
        }

        public void RequestToSpitFire()
        {
            if (!this.IsMarioDead && this.marioPowerUpState is FireMarioState)
            {
                foreach (FireBall fireBall in this.spittingFireBalls)
                {
                    if (fireBall.GetFireBallState is ReadyToBeSpatState)
                    {
                        fireBall.SpitFireBall(x, y);
                        Systems.Events.TheInstance.FireBallMade();

                        break;
                    }
                }
            }
        }

        public void RequestToSwingSword()
        {
            if (!this.IsMarioDead && this.marioPowerUpState is FireMarioState)
            {
                if (hasSword)
                {
                    ((Sword)(this.sword)).SwingSword();
                }
            }
        }

        public void ChangeActionState(MarioActionState state)
        {
            this.marioActionState = state;
            if (this.marioPowerUpState is SmallMarioState)
            {
                HitboxType = HitboxTypes.SmallMario;
            }
            else if (state is MarioCrouchState)
            {
                HitboxType = HitboxTypes.CrouchedMario;
            }
            else if (this.marioPowerUpState is FireMarioState || this.marioPowerUpState is SuperMarioState)
            {
                HitboxType = HitboxTypes.FullMario;
            }
            else
            {
                HitboxType = HitboxTypes.Full;
            }
        }

        public void ChangePowerUpState(MarioPowerUpState state)
        {
            this.marioPowerUpState = state;
            this.collidable = !(state is DeadMarioState);
            if (state is SmallMarioState)
            {
                HitboxType = HitboxTypes.SmallMario;
            }
            else if (this.marioActionState is MarioCrouchState)
            {
                HitboxType = HitboxTypes.CrouchedMario;
            }
            else if (this.marioPowerUpState is FireMarioState || this.marioPowerUpState is SuperMarioState)
            {
                HitboxType = HitboxTypes.FullMario;
            }
            else
            {
                HitboxType = HitboxTypes.Full;
            }
        }

        public void UpdateActionSprite()
        {
            this.marioActionState.UpdateSprite();
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
         {
            if (!this.IsMarioDead)
            {
                if (gameObject is Pipe && collisionDirection == CollisionDirection.Bottom && marioActionState is MarioCrouchState)
                {
                    //If Mario is centered on the Pipe
                    if (this.positionInGame.X + this.Sprite.GetDimensions().X / 2 > 
                            gameObject.PositionInGame.X + (gameObject.Sprite.GetDimensions().X / 2) - 10 &&
                        this.positionInGame.X + this.Sprite.GetDimensions().X / 2 < 
                            gameObject.PositionInGame.X + (gameObject.Sprite.GetDimensions().X / 2) + 10)
                    {
                        if (((Pipe)gameObject).Warp > 0)
                        {
                            this.marioPowerUpState = new PipeEnterState(this, ((Pipe)gameObject).WarpX, ((Pipe)gameObject).WarpY,
                                    marioActionState, marioPowerUpState);
                        }
                    }
                }

                if (gameObject is Portal)
                {
                    System.Diagnostics.Debug.WriteLine("IsBluePortal: " + ((Portal)gameObject).IsBluePortal);
                }
                if (!(gameObject is FloorBlock) && invincibilityTimer == 0 && collisionDirection != CollisionDirection.None)
                {
                    colorCounter = 8;
                    //this.tint = Color.Green;
                }

                if (invincibilityTimer == 0 && gameObject is Bowser && gameObject.IsCollidable)
                {
                    if (!this.IsMarioDead)
                    {
                        this.marioPowerUpState.HandleDamageMarioTransition();
                    }
                }

                if (invincibilityTimer == 0 && gameObject is BowserFireball && gameObject.IsCollidable)
                {
                    if (!this.IsMarioDead)
                    {
                        this.marioPowerUpState.HandleDamageMarioTransition();
                    }
                }

                if (invincibilityTimer == 0 && gameObject is Goomba && gameObject.IsCollidable)
                {
                    if (collisionDirection != CollisionDirection.Bottom)
                    {
                        if (!this.IsMarioDead)
                        {
                            this.marioPowerUpState.HandleDamageMarioTransition();
                        }
                    }
                    else
                    {
                        YSpeed = -5f;
                    }
                }

                if (invincibilityTimer == 0 && gameObject is Koopa && gameObject.IsCollidable)
                {
                    Koopa koopa = (Koopa)gameObject;

                    if (!(koopa.GetState is KoopaShellState || koopa.GetState is KoopaFeetOutOfShellState) && collisionDirection != CollisionDirection.Bottom)
                    {
                        if (!this.IsMarioDead)
                        {
                            this.marioPowerUpState.HandleDamageMarioTransition();
                        }
                    }
                    else if (koopa.GetState is KoopaShellState)
                    {
                        if ((collisionDirection == CollisionDirection.Right || collisionDirection == CollisionDirection.Left) && koopa.XSpeed != 0.0f)
                        {
                            this.marioPowerUpState.HandleDamageMarioTransition();
                        }
                        else if (collisionDirection == CollisionDirection.Bottom)
                        {
                            YSpeed = -5f;
                        }
                    }
                    else if (collisionDirection == CollisionDirection.Bottom)
                    {
                        YSpeed = -5f;
                    }
                }
                else if (gameObject is HiddenBlock && !gameObject.IsSolid)
                {
                    if (collisionDirection == CollisionDirection.Top)
                    {
                        this.marioActionState.HandleJumpTransition(false);
                    }
                }
                else if (invincibilityTimer == 0 && gameObject is PiranhaPlant && gameObject.IsCollidable)
                {
                    if (!this.IsMarioDead)
                    {
                        this.marioPowerUpState.HandleDamageMarioTransition();
                    }
                }
                else if (gameObject is Block || gameObject is Gate || gameObject is Button)
                {
                    if (collisionDirection == CollisionDirection.Bottom)
                    {
                        if (marioActionState is MarioJumpState)
                        {
                            this.marioActionState.HandleIdleTransition();
                        }
                        else
                        {
                            this.YSpeed = 0;
                        }
                    }
                    else if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
                    {
                        if (marioActionState is MarioJumpState)
                        {
                            XSpeed = 0;
                        }
                        else
                        {
                            this.marioActionState.HandleIdleTransition();
                            if (collisionDirection == CollisionDirection.Left)
                            {
                                leftBlocked = true;
                            }
                            else
                            {
                                rightBlocked = true;
                            }
                        }
                    }
                    else if (collisionDirection == CollisionDirection.Top)
                    {
                        this.marioActionState.HandleJumpTransition(false);
                    }
                    else
                    {
                        YSpeed = 0;
                    }
                }
                else if (gameObject is Flag && !(this.marioActionState is MarioFinishState))
                {
                    PlayerHUD.GetInstance().IncreaseLives();
                    this.marioActionState.HandleFinishTransition();
                }
                else if (gameObject is FlagSegment && !(this.marioActionState is MarioFinishState))
                {
                    FlagSegment segment = (FlagSegment)gameObject;
                    Systems.Events.TheInstance.FlagPoleGrab(segment);
                    this.marioActionState.HandleFinishTransition();
                }
            }
        }

        public void ResetMario()
        {
            this.marioActionState = new MarioIdleState(this);
            this.marioPowerUpState = new SmallMarioState(this, false);
            this.gravity.Enable();
            this.Sprite = SmallMarioFactory.GetInstance().CreateProduct(MarioTypes.IdleRight);
            this.gravity.Enable();
            this.IsCollidable = true;
            this.IsFacingLeft = false;
            this.IsMarioDead = false;
            this.PoweredUp = false;
            crouching = false;
            rightBlocked = false;
            leftBlocked = false;
            invincibilityTimer = 0;
            HitboxType = HitboxTypes.SmallMario;
            this.hitboxColor = Color.Yellow;
            this.hitboxOffset = 0;
            foreach (FireBall fireBall in this.spittingFireBalls)
            {
                fireBall.ResetFireBall();
            }
            ((Sword)this.sword).ResetSword();
        }

        public Point GetCenter()
        {
            Rectangle hitbox = GetHitBox();
            return new Point(hitbox.X + hitbox.Width / 2, hitbox.Y + hitbox.Height / 2);
        }

        public bool IsFinishedState()
        {
            return this.marioActionState is MarioFinishState;
        }
    }
}
