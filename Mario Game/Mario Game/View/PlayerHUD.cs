using MarioGame.Factories;
using MarioGame.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MarioGame.StateMachines.GameStates;
using MarioGame.Systems;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Background;
using MarioGame.Collision;

namespace MarioGame.View
{
    class PlayerHUD : GameEventListener
    {
        private static PlayerHUD instance;

        private SpriteFont spriteFont;

        private GraphicsDevice graphicsDevice;

        private ISprite coin;

        private ISprite staticMario;

        private int ellapsedTimer;

        private int numberOfLives;

        private int totalPoints;

        private int coinsCollected;

        private int timeRemaining;

        private bool addTimeToPoints;

        private PlayerHUD(SpriteFont font, GraphicsDevice graphics)
        {
            spriteFont = font;
            graphicsDevice = graphics;

            numberOfLives = 3;
            totalPoints = 0;
            timeRemaining = 400;
            coinsCollected = 0;

            this.coin = ItemFactory.GetInstance().CreateProduct(ItemTypes.BlockCoin);
            this.staticMario = SmallMarioFactory.GetInstance().CreateProduct(MarioTypes.IdleLeft);
        }

        internal void AddTimeToPoints()
        {
            addTimeToPoints = true;
        }

        public static PlayerHUD CreateInstance(SpriteFont font, GraphicsDevice graphicsDevice)
        {
            return instance = new PlayerHUD(font, graphicsDevice);
        }

        public static PlayerHUD GetInstance()
        {
            return instance;
        }

        public void IncreaseCoins()
        {
            if (this.coinsCollected == 99)
            {
                this.coinsCollected = 0;
                IncreaseLives();
            }
            else
            {
                this.coinsCollected++;
            }
        }

        public void AddPoints(int val)
        {
            totalPoints += val;
        }

        public void IncreaseLives()
        {
            this.numberOfLives++;
        }

        public void DecreaseLives()
        {
            this.numberOfLives--;
            if (numberOfLives > 0)
            {
                MarioGame.GetInstance.ResetLevel();
            } else
            {
                PlayState.GetInstance().GameOver();
            }
        }

        public int GetTimeRemaining()
        {
            return timeRemaining;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont,
                string.Format("MARIO \n{0:D6}", totalPoints),
                new Vector2(graphicsDevice.Viewport.X + 5,
                graphicsDevice.Viewport.Y + 5),
                Color.White);

            this.coin.Draw(spriteBatch, 
                Color.White,
                new Point(graphicsDevice.Viewport.X + 125, graphicsDevice.Viewport.Y + 5));
            spriteBatch.DrawString(spriteFont,
                string.Format("X{0:D2}",  coinsCollected),
                new Vector2(graphicsDevice.Viewport.X + 145, graphicsDevice.Viewport.Y + 15),
                Color.White);

            spriteBatch.DrawString(spriteFont,
                string.Format("TIME \n {0:D3}", timeRemaining),
                new Vector2(graphicsDevice.Viewport.Width - 100, graphicsDevice.Viewport.Y + 5),
                Color.White);

            this.staticMario.Draw(spriteBatch,
                Color.White,
                new Point(graphicsDevice.Viewport.Width - 255, graphicsDevice.Viewport.Y - 43));
            spriteBatch.DrawString(spriteFont,
                string.Format("X{0:D2}", numberOfLives),
                new Vector2(graphicsDevice.Viewport.Width - 215, graphicsDevice.Viewport.Y + 15),
                Color.White);
        }

        public void ResetPlayerHUD()
        {
            timeRemaining = 400;
        }

        public void FullResetPlayerHUD()
        {
            totalPoints = 0;
            timeRemaining = 400;
            coinsCollected = 0;
            this.numberOfLives = 3;
            this.addTimeToPoints = false;
        }

        public void Update(GameTime gameTime)
        {
            ellapsedTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (addTimeToPoints)
            {
                if (ellapsedTimer >= 60)
                {
                    timeRemaining -= 20;
                    ellapsedTimer = 0;
                    AddPoints(20 * 10);
                    if (timeRemaining <= 0)
                    {
                        AddPoints(timeRemaining * 10);
                        timeRemaining = 0;
                        PlayState.GetInstance().Win();
                    } else {
                        Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.Coin);
                    }
                }
            }
            else if (ellapsedTimer >= 1000)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining--;
                    ellapsedTimer = 0;
                    if (timeRemaining == 75)
                    {
                        Systems.Events.TheInstance.Timer();
                    }
                }
            }

            this.coin.Update(gameTime, this.graphicsDevice);
            this.staticMario.Update(gameTime, this.graphicsDevice);
        }



        // Listening for game events:

        public void BrickBreak()
        {
        }

        public void BrickBump()
        {
        }

        public void Coin()
        {
            IncreaseCoins();
            AddPoints(200);
        }

        public void FireBallMade()
        {
        }

        public void FlagPoleGrabbing(FlagSegment flagSegment)
        {
            AddPoints(flagSegment.GetPoints());
        }

        public void GameOver()
        {
        }

        public void GoombaDied()
        {
            AddPoints(100);
        }

        public void ItemReveal()
        {
        }

        public void Jump()
        {
        }

        public void KoopaDied()
        {
            AddPoints(100);
        }

        public void MarioDied()
        {
        }

        public void OneUp()
        {
            IncreaseLives();
        }

        public void PortalProjectileFiring()
        {
        }

        public void PowerUp()
        {
            AddPoints(1000);
        }

        public void PowerUpAppears()
        {
        }

        public void Timer()
        {
        }

        public void Warp()
        {
        }

        public void Win()
        {
        }

        public void GateOpening(GameObject gameObject)
        {
        }

        public void CompanionDied()
        {
            AddPoints(-1000);
        }

        public void GateClosing()
        {
        }

        public void BowserDied()
        {
        }

        public void BowserFire()
        {
        }

        public void PortalProjectileCollision(bool blue, GameObject obj, CollisionDirection collisionDirection)
        {
        }

        public void PortalOpened()
        {
        }
    }
}
