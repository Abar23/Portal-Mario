using MarioGame.Factories;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MarioGame.Collision;
using System;

namespace MarioGame.GameObjects
{
    abstract class GameObject : IDisposable
    {
        protected ISprite sprite;
        
        protected Color tint;

        protected IFactory spriteFactory;

        protected Point positionInGame;

        protected Vector2 speedOfEntity;

        protected bool solid;

        protected bool collidable;

        private HitboxTypes hitboxType;

        protected int hitboxOffset;

        protected Color hitboxColor;

        private Texture2D hitboxTexture;

        private bool isVisible;

        public virtual bool IsVisible
        {
            get => this.isVisible;
            set => this.isVisible = value;
        }

        public bool IsSolid
        {
            get => this.solid;
            set => this.solid = value;
        }

        public bool IsCollidable
        {
            get => this.collidable;
            set => this.collidable = value;
        }

        public Point PositionInGame {
            get => this.positionInGame;
            set => this.positionInGame = value;
        }

        public int SetXPositionInGame
        {
            set => this.positionInGame.X = value;
        }

        public int SetYPositionInGame
        {
            set => this.positionInGame.Y = value;
        }


        public ISprite Sprite
        {
            get => this.sprite;
            set => this.sprite = value;
        }

        public IFactory SpriteFactory
        {
            get => this.spriteFactory;
            set => this.spriteFactory = value;
        }

        public float XSpeed
        {
            get => this.speedOfEntity.X;
            set => this.speedOfEntity.X = value;
        }

        public float YSpeed
        {
            get => this.speedOfEntity.Y;
            set => this.speedOfEntity.Y = value;
        }

        public HitboxTypes HitboxType
        {
            set => this.hitboxType = value;
        }

        protected GameObject(IFactory spriteFactory, Point positionInGame, Vector2 velocityOfObject)
        {
            this.spriteFactory = spriteFactory;
            this.positionInGame = positionInGame;
            this.speedOfEntity = velocityOfObject;
            this.isVisible = true;

            this.tint = Color.White;

            this.solid = false;

            hitboxType = HitboxTypes.Full;
            hitboxOffset = 2;
            this.hitboxColor = Color.ForestGreen;
            this.collidable = true;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.isVisible)
            {
                this.sprite.Draw(spriteBatch, tint, new Point(positionInGame.X, positionInGame.Y));
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool drawHitbox)
        {
            this.Draw(spriteBatch);
            if (this.collidable && drawHitbox)
            {
                if (hitboxTexture == null) {
                    hitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                    hitboxTexture.SetData<Color>(new Color[] { hitboxColor });
                }
                Rectangle hitbox = GetHitBox();
                spriteBatch.Draw(hitboxTexture, new Rectangle(hitbox.X, hitbox.Y, 1, hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTexture, new Rectangle(hitbox.X, hitbox.Y, hitbox.Width, 1), Color.White);
                spriteBatch.Draw(hitboxTexture, new Rectangle(hitbox.X + hitbox.Width - 1, hitbox.Y, 1, hitbox.Height), Color.White);
                spriteBatch.Draw(hitboxTexture, new Rectangle(hitbox.X, hitbox.Y + hitbox.Height - 1, hitbox.Width, 1), Color.White);
            }
        }

        public virtual Rectangle GetHitBox()
        {
            return GetHitBox(positionInGame.X, positionInGame.Y);
        }

        public virtual Rectangle GetHitBox(int x, int y)
        {
            Rectangle hitbox = new Rectangle();
            Point dimensions = sprite.GetDimensions();
            if (hitboxType == HitboxTypes.SmallMario) {
                int smallMarioWidth = 24;
                hitbox.X = x + 8;
                hitbox.Width = smallMarioWidth + 2 * hitboxOffset;
                int smallMarioHeight = 32;
                hitbox.Y = y + 48;
                hitbox.Height = smallMarioHeight + 2 * hitboxOffset;
            }
            else if (hitboxType == HitboxTypes.FullMario)
            {
                int fullMarioWidth = 32;
                hitbox.X = x + 8;
                hitbox.Width = fullMarioWidth + 2 * hitboxOffset;
                int fullMarioHeight = 64;
                hitbox.Y = y + 16;
                hitbox.Height = fullMarioHeight + 2 * hitboxOffset;
            }
            else if (hitboxType == HitboxTypes.CrouchedMario)
            {
                int fullMarioWidth = 32;
                hitbox.X = x + 8;
                hitbox.Width = fullMarioWidth + 2 * hitboxOffset;
                int crouchedMarioHeight = 44;
                hitbox.Y = y + 36;
                hitbox.Height = crouchedMarioHeight + 2 * hitboxOffset;
            }
            else if (hitboxType == HitboxTypes.Flag)
            {
                hitbox.X = x + 31;
                hitbox.Width = 4;

                hitbox.Y = y;
                hitbox.Height = dimensions.Y;
            }
            else {
                hitbox.X = x - hitboxOffset;
                hitbox.Width = dimensions.X + 2 * hitboxOffset;
                if (hitboxType == HitboxTypes.Half)
                {
                    hitbox.Y = y + dimensions.Y / 2 - hitboxOffset;
                    hitbox.Height = dimensions.Y / 2 + 2 * hitboxOffset;
                }
                else if (hitboxType == HitboxTypes.TwoThirds)
                {
                    hitbox.Y = y + dimensions.Y / 3 - hitboxOffset;
                    hitbox.Height = 2 * dimensions.Y / 3 + 2 * hitboxOffset + 1;
                }
                else
                {
                    hitbox.Y = y - hitboxOffset;
                    hitbox.Height = dimensions.Y + 2 * hitboxOffset;
                }
            }
            return hitbox;
        }

        public abstract void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject);

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            UpdateLocally(gameTime, graphicsDevice);

            CollisionSystem collisionSystem = CollisionSystem.GetInstance();
            if (this.XSpeed != 0 || this.YSpeed != 0)
            {
                collisionSystem.RequestMovement(this, new Point(positionInGame.X + (int)this.XSpeed, positionInGame.Y + (int)this.YSpeed));
            }
        }

        protected abstract void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice);

        public void Dispose()
        {
            if (hitboxTexture != null)
            {
                this.hitboxTexture.Dispose();
            }
        }
    }
}