using System;
using MarioGame.Collision;
using MarioGame.Factories;
using MarioGame.StateMachines.GoombaStates;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Blocks;
using MarioGame.GameObjects.Effects;
using MarioGame.Physics;
using MarioGame.StateMachines.BowserStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.StateMachines.BrickBlockStates;
using MarioGame.View;

namespace MarioGame.GameObjects.Enemies
{
    class Bowser : GameObject
    {
        private BowserState bowserState;
        private bool prevStateShell;
        private int burst;
        private int transitionTimer;
        private int health;
        public BowserFireball fireball;
        private Gravity gravity;

        public bool IsFacingLeft { get; set; }

        public Bowser(IFactory spriteFactory, Point initialPositionInGame) : this(spriteFactory, initialPositionInGame, new Vector2())
        {
        }

        public Bowser(IFactory spriteFactory, Point initialPositionInGame, Vector2 initialSpeedInGame) : base(spriteFactory, initialPositionInGame, initialSpeedInGame)
        {
            this.bowserState = new BowserIdleState(this);
            this.prevStateShell = true;
            this.fireball = new BowserFireball(true,-100,0);
            this.burst = 1;
            this.transitionTimer = 0;
            this.health = 31;
            this.IsFacingLeft = true;

            this.hitboxOffset = -1;
            this.hitboxColor = Color.Red;
            this.HitboxType = HitboxTypes.Full;
            this.gravity = new Gravity(this);
            this.IsCollidable = true;
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            //plus 500 pixels so that it will still update when mario is at one end of the arena
            if (this.PositionInGame.X + (int) BossFactory.BossSizesInPixels.BowserWidth + 500 >=
                Camera.AquireInstance().Position.X
                && this.PositionInGame.X-500 <= (Camera.AquireInstance().Position.X + Camera.AquireInstance().VirtualWidth))
            {
                this.gravity.Update(gameTime);
                this.sprite.Update(gameTime, graphicsDevice);
                this.fireball.Update(gameTime, graphicsDevice);
                if (this.transitionTimer <= 100)
                {
                    this.transitionTimer++;
                }

                if (this.transitionTimer > 100)
                {
                    this.transitionTimer = 0;
                    if (this.bowserState is BowserChargeState)
                    {
                        this.burst++;
                        this.bowserState.HandleFireBreathTransition();
                    }
                    else if (this.bowserState is BowserFireBreathState)
                    {
                        if (this.burst > 3)
                        {
                            this.burst = 1;
                            this.prevStateShell = false;
                            this.bowserState.HandleIdleTransition();
                        }
                        else
                        {
                            this.bowserState.HandleChargeTransition();
                        }
                    }
                    else if (this.bowserState is BowserShellState)
                    {
                        this.prevStateShell = true;
                        this.bowserState.HandleIdleTransition();
                    }
                    else if (this.bowserState is BowserIdleState)
                    {
                        if (this.prevStateShell)
                        {
                            this.bowserState.HandleChargeTransition();
                        }
                        else
                        {
                            this.bowserState.HandleShellTransition();
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            this.fireball.Draw(spriteBatch);
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
            if (gameObject is FireBall || gameObject is Sword)
            {
                if ((this.bowserState is BowserChargeState || this.bowserState is BowserIdleState))
                {
                    if ((collisionDirection == CollisionDirection.Left && this.IsFacingLeft) ||
                        (collisionDirection == CollisionDirection.Right && !this.IsFacingLeft))
                    {
                        this.health--;
                        if (this.health == 0)
                        {
                            this.bowserState.HandleDeadTransition();
                        }
                    }
                }
            }
            else if (gameObject is Mario && ((Mario)gameObject).IsInvincible())
            {
                this.bowserState.HandleDeadTransition();
            }
            else if (gameObject is Block)
            {
                if (!(collisionDirection == CollisionDirection.Bottom))
                {
                    this.YSpeed = 0.0f;
                }
            }

            if (collisionDirection == CollisionDirection.Left || collisionDirection == CollisionDirection.Right)
            {
                this.XSpeed *= -1;
            }
        }

        public void ChangeState(BowserState state)
        {
            this.bowserState = state;
        }
    }
}
