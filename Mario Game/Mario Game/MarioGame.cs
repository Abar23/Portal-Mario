using System;
using System.Collections.Generic;
using System.Media;
using MarioGame.Controllers;
using MarioGame.GameObjects;
using MarioGame.LevelTileMap;
using MarioGame.GameObjects.Player;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MarioGame.Collision;
using MarioGame.Costumes;
using MarioGame.Sounds;
using MarioGame.View;
using MarioGame.StateMachines.GameStates;
using MarioGame.Systems;
using Microsoft.Xna.Framework.Media;
using MarioGame.Events;
using Microsoft.Xna.Framework.Audio;
using SoundPlayer = MarioGame.Sounds.SoundPlayer;

namespace MarioGame
{
    class MarioGame : Game
    {
        private static MarioGame instance;

        private bool hitboxesOn;

        readonly GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        PlayerHUD playerHUD;

        BlockUpdateEvent undergroundEvent;

        #region AudioPlayers
        private MusicPlayer music;
        private Sounds.SoundPlayer sounds;
        #endregion

        #region StateMachine
        private GameState state;
        private HashSet<GameState> previousStates;
        #endregion

        #region View
        private Camera camera;
        private List<Layer> layers;
        private RenderTarget2D render;
        private const int VirtualWidth = 512; //internal resolution of game, 512 x 480
        private const int VirtualHeight = 480;
        private const float Ratio = (float)((float)VirtualWidth / (float)VirtualHeight); //aspect ratio
        private int realWidth;  //current resolution game is drawn at
        private int realHeight;
        private int screenXPosition;
        private int screenYPosition;
        #endregion

        #region Controllers
        private IController<Keys> keyboardController;
        private IController<Buttons> gamePadController;
        #endregion

        #region Player
        private Mario mario;
        #endregion

        #region Level
        private Level level;
        private LevelParser levelParser;

        public int PreferredBackBufferWidth { get => this.graphics.PreferredBackBufferWidth; }
        public int PreferredBackBufferHeight { get => this.graphics.PreferredBackBufferHeight; }
        #endregion

        #region Systems
        private SoundSystem soundSystem;
        #endregion

        public int GetScreenXPosition()
        {
            return this.screenXPosition;
        }

        public int GetScreenYPosition()
        {
            return this.screenYPosition;
        }

        public MarioGame()
        {
            this.IsMouseVisible = true;
            instance = this;
            this.graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = VirtualWidth,
                PreferredBackBufferHeight = VirtualHeight
            };
            
            Content.RootDirectory = "Content";

            this.keyboardController = new KeyboardController();
            this.gamePadController = new GamePadController();
            this.previousStates = new HashSet<GameState>();

            soundSystem = new SoundSystem();
            Systems.Events.TheInstance.AddListener(soundSystem);
        }

        public LevelMap GetMap()
        {
            return level.Map;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.camera = Camera.CreateInstance(VirtualWidth, VirtualHeight, new Rectangle(0, 0, 6720, 480));

            this.layers = new List<Layer>
            {
                new Layer(camera) {Parallax = new Vector2(1.0f)},
                new Layer(camera) {Parallax = new Vector2(0.75f)},
                new Layer(camera) {Parallax = new Vector2(0.4f)}
            };

            this.render = new RenderTarget2D(GraphicsDevice,
                            GraphicsDevice.PresentationParameters.BackBufferWidth,
                            GraphicsDevice.PresentationParameters.BackBufferHeight,
                            false,
                            GraphicsDevice.PresentationParameters.BackBufferFormat,
                            DepthFormat.Depth24);

            this.screenXPosition = 0;
            this.screenYPosition = 0;
            this.realWidth = VirtualWidth;
            this.realHeight = VirtualHeight;

            this.undergroundEvent = BlockUpdateEvent.CreateInstance();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new spriteBatch, which can be used to draw textures
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Create Factories
            Texture2D marioAtlas = Content.Load<Texture2D>("Textures/Mario Sprites/Mario Sprite Sheet");
            Texture2D itemsTexture = Content.Load<Texture2D>("Textures/Items Sprites/Items Sprite Sheet");
            Texture2D blockTextureAtlas = Content.Load<Texture2D>("Textures/Blocks");
            Texture2D enemyTextureAtlas = Content.Load<Texture2D>("Textures/Enemy Sprite Sheet");
            Texture2D BowserTextureAtlas = Content.Load<Texture2D>("Textures/BowserSprites");
            Texture2D backgroundTextureAtlas = Content.Load<Texture2D>("Textures/Mario Background Sprite Sheet");
            Texture2D effectsTextureAtlas = Content.Load<Texture2D>("Textures/Effects Sprites/Mario Effects Sprite Sheet");
            SmallMarioFactory.CreateInstance(marioAtlas);
            SuperMarioFactory.CreateInstance(marioAtlas);
            FireMarioFactory.CreateInstance(marioAtlas);
            EnemyFactory.CreateInstance(enemyTextureAtlas);
            BossFactory.CreateInstance(BowserTextureAtlas);
            BlockFactory.CreateInstance(blockTextureAtlas);
            ItemFactory.CreateInstance(itemsTexture);
            BackgroundFactory.CreateInstance(backgroundTextureAtlas);
            EffectsFactory.CreateInstance(effectsTextureAtlas);
            #endregion

            CostumeChanger.CreateInstance(this.Content);

            this.playerHUD = PlayerHUD.CreateInstance(Content.Load<SpriteFont>("Font/Mario Game HUD Text"), this.GraphicsDevice);
            Systems.Events.TheInstance.AddListener(PlayerHUD.GetInstance());
            this.music = MusicPlayer.CreateInstance(Content.Load<Song>("Sounds/backgroundmusic"));
            this.sounds = Sounds.SoundPlayer.CreateInstance();
            this.mario = Mario.CreateTheInstance(SmallMarioFactory.GetInstance(), Point.Zero, Vector2.Zero);
            this.levelParser = new LevelParser(layers, "./LevelTileMap/level_1.json");
            this.level = new Level(this.levelParser);

            CollisionSystem.CreateTheInstance(this.level.Map);

            this.SetState(new PlayState(this, this.keyboardController, this.gamePadController));
            state.SetupControls();

            #region Set Sound Effects
            this.sounds.addSFX(SoundEffectNames.BreakBlock, Content.Load<SoundEffect>("Sounds/smb_breakblock"));
            this.sounds.addSFX(SoundEffectNames.OneUp, Content.Load<SoundEffect>("Sounds/1-up"));
            this.sounds.addSFX(SoundEffectNames.Bump, Content.Load<SoundEffect>("Sounds/smb_bump"));
            this.sounds.addSFX(SoundEffectNames.Coin, Content.Load<SoundEffect>("Sounds/smb_coin"));
            this.sounds.addSFX(SoundEffectNames.Fireball, Content.Load<SoundEffect>("Sounds/fireball"));
            this.sounds.addSFX(SoundEffectNames.Flagpole, Content.Load<SoundEffect>("Sounds/DeMorgansLaws"));
            this.sounds.addSFX(SoundEffectNames.Win, Content.Load<SoundEffect>("Sounds/DeMorgansLaws"));
            this.sounds.addSFX(SoundEffectNames.GameOver, Content.Load<SoundEffect>("Sounds/smb_gameover"));
            this.sounds.addSFX(SoundEffectNames.JumpSmall, Content.Load<SoundEffect>("Sounds/smb_jump-small"));
            this.sounds.addSFX(SoundEffectNames.JumpSuper, Content.Load<SoundEffect>("Sounds/smb_jump-super"));
            this.sounds.addSFX(SoundEffectNames.MarioDie, Content.Load<SoundEffect>("Sounds/smb_mariodies"));
            this.sounds.addSFX(SoundEffectNames.Pipe, Content.Load<SoundEffect>("Sounds/pipe"));
            this.sounds.addSFX(SoundEffectNames.PowerUp, Content.Load<SoundEffect>("Sounds/powerup"));
            this.sounds.addSFX(SoundEffectNames.PowerUpAppear, Content.Load<SoundEffect>("Sounds/powerupappears"));
            this.sounds.addSFX(SoundEffectNames.Stomp, Content.Load<SoundEffect>("Sounds/smb_stomp"));
            this.sounds.addSFX(SoundEffectNames.Warning, Content.Load<SoundEffect>("Sounds/warning"));
            this.sounds.addSFX(SoundEffectNames.TimePoints, Content.Load<SoundEffect>("Sounds/time_points"));
            this.sounds.addSFX(SoundEffectNames.PortalFire, Content.Load<SoundEffect>("Sounds/portal_fire"));
            this.sounds.addSFX(SoundEffectNames.GateOpen, Content.Load<SoundEffect>("Sounds/gate_open"));
            this.sounds.addSFX(SoundEffectNames.GateClose, Content.Load<SoundEffect>("Sounds/gate_close"));
            this.sounds.addSFX(SoundEffectNames.BowserFall, Content.Load<SoundEffect>("Sounds/bowserfall"));
            this.sounds.addSFX(SoundEffectNames.BowserFire, Content.Load<SoundEffect>("Sounds/bowserfire"));
            this.sounds.addSFX(SoundEffectNames.CubeDeath, Content.Load<SoundEffect>("Sounds/turretshotbylaser01"));
            this.sounds.addSFX(SoundEffectNames.OpenPortal, Content.Load<SoundEffect>("Sounds/openportal"));
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Free all textures loaded onto the ContentManager
            Content.Unload();
            Content.Dispose();

            foreach (GameObject gameObject in this.level.Entities)
            {
                gameObject.Dispose();
            }

            foreach (GameState gameState in this.previousStates)
            {
                gameState.Dispose();
            }

            this.spriteBatch.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get input
            this.keyboardController.UpdateInput();
            this.gamePadController.UpdateInput();
            MouseController.GetInstance.UpdateInput();

            if (state.ShouldUpdate) {
                foreach (GameObject gameObject in this.level.Entities)
                {
                    gameObject.Update(gameTime, this.GraphicsDevice);
                }

                camera.LookAt(mario.PositionInGame.ToVector2());

                base.Update(gameTime);

                CollisionSystem.GetInstance().Update();

                this.playerHUD.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.SetRenderTarget(render);
            this.GraphicsDevice.Clear(Color.DodgerBlue);

            layers[2].Draw(this.spriteBatch, hitboxesOn);
            layers[1].Draw(this.spriteBatch, hitboxesOn);
            layers[0].Draw(this.spriteBatch, hitboxesOn);

            base.Draw(gameTime);

            state.Draw(spriteBatch);

            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this.playerHUD.Draw(this.spriteBatch);
            this.spriteBatch.End();

            this.GraphicsDevice.SetRenderTarget(null);

            this.GraphicsDevice.Clear(Color.Black);
            graphics.ApplyChanges();

            this.spriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.PointClamp);

            this.spriteBatch.Draw(render, new Rectangle(screenXPosition, screenYPosition, realWidth, realHeight), Color.White);
            this.spriteBatch.End();
        }

        public void ToggleDrawHitboxes()
        {
            hitboxesOn = !hitboxesOn;
        }

        public void ResetLevel()
        {
            if (this.state is GameOverState || this.state is WinState)
            {
                this.mario.Checkpoint = false;
            }
            this.mario.ResetMario();
            SoundPlayer.GetInstance().StopAllSounds();
            this.music.ResetPlayer();
            CostumeChanger.GetInstance().Index = (new Random()).Next(CostumeChanger.NumCostumes);
            this.playerHUD.ResetPlayerHUD();
            this.undergroundEvent.ClearSubScribers();
            this.camera = Camera.CreateInstance(VirtualWidth, VirtualHeight, new Rectangle(0, 0, 6720, 480));
            this.layers = new List<Layer>
            {
                new Layer(camera) {Parallax = new Vector2(1.0f)},
                new Layer(camera) {Parallax = new Vector2(0.75f)},
                new Layer(camera) {Parallax = new Vector2(0.4f)}
            };
            this.levelParser = new LevelParser(layers, "./LevelTileMap/level_1.json");
            this.level = new Level(this.levelParser);
            CollisionSystem.GetInstance().SetMap(this.level.Map);
        }

        public void FullResetLevel()
        {
            ResetLevel();
            this.playerHUD.FullResetPlayerHUD();
        }

        public void ToggleFullscreen()
        {
            if (this.graphics.IsFullScreen)
            {
                this.graphics.PreferredBackBufferWidth = VirtualWidth;
                this.graphics.PreferredBackBufferHeight = VirtualHeight;
                realWidth = VirtualWidth;
                realHeight = VirtualHeight;
                screenXPosition = 0;
                screenYPosition = 0;
            }
            else
            {
                this.graphics.PreferredBackBufferHeight =  GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                this.graphics.PreferredBackBufferWidth =  GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                //If monitor is Widescreen
                if (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width >
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                {
                    realHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    realWidth = (int)(realHeight * Ratio);
                    screenXPosition = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (realWidth / 2);
                    screenYPosition = 0;
                }
                else //If monitor is Veritcal
                {
                    realWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    realHeight = (int)(realWidth / Ratio);
                    screenXPosition = 0;
                    screenYPosition = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (realHeight / 2);
                }
            }
            
            this.graphics.ToggleFullScreen();
            this.graphics.ApplyChanges();
        }

        public void SetState(GameState newState)
        {
            if (!this.previousStates.Contains(newState))
            {
                this.previousStates.Add(newState);
            }

            this.state = newState;
        }

        public static MarioGame GetInstance
        {
            get => instance;
        }

        public Camera Camera { get => this.camera;}

        public void AddNewGameObject(bool first, GameObject obj)
        {
            level.Add(first, obj);
            layers[0].Add(first, obj);
        }
    }
}
