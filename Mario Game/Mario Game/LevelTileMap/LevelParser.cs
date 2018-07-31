using System;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MarioGame.Costumes;
using MarioGame.Factories;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Blocks;
using MarioGame.View;
using Microsoft.Xna.Framework;
using MarioGame.GameObjects.Background;
using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects.Player;
using MarioGame.GameObjects.Other;

namespace MarioGame.LevelTileMap
{
    internal class LevelParser
    {
        public LevelObject LevelObject;
        private readonly List<Layer> _layers;
        private readonly List<GameObject> _collidableEntities;
        private readonly List<GameObject> _listOfEntities;

        public LevelParser(List<Layer> layers, string level)
        {
            StreamReader sr = new StreamReader(level);
            String line = sr.ReadToEnd();
            LevelObject = new JavaScriptSerializer().Deserialize<LevelObject>(line);
            _layers = layers;

            _collidableEntities = new List<GameObject>();
            _listOfEntities = new List<GameObject>();
        }

        public List<GameObject> GetData()
        {
            foreach (Entity entity in this.LevelObject.Entities)
            {
                AddEntity(entity);
            }

            return this._listOfEntities;
        }

        public List<GameObject> GetCollidableEntities()
        {
            return this._collidableEntities;
        }


        public void AddEntity(Entity entity)
        {
            if (entity.InARow == 0)
            {
                GameObject obj = GetGameObject(entity);
                this._listOfEntities.Add(obj);

                if (obj is Pipe)
                {
                    ((Pipe)obj).Warp = entity.PipeWarp;
                    ((Pipe)obj).WarpX = entity.PipeX;
                    ((Pipe)obj).WarpY = entity.PipeY;
                }

                if (obj is Block && entity.Entities != null)
                {
                    foreach (Entity next in entity.Entities)
                    {
                        GameObject item = GetGameObject(next);
                        this._listOfEntities.Add(item);
                        CheckCollidable(next.Collidable, item);
                        _layers[entity.Layer].Objects.Add(item);
                        ((Block)obj).AddItem((IRevealableItem)item);
                    }
                }

                if (obj is Gate && entity.Entities != null)
                {
                    Gate gate = (Gate)obj;
                    foreach (Entity next in entity.Entities)
                    {
                        GameObject button = GetGameObject(next);
                        this._listOfEntities.Add(button);
                        CheckCollidable(next.Collidable, button);
                        _layers[entity.Layer].Objects.Add(button);
                        gate.AddButton((Button)button);
                    }
                }
                CheckCollidable(entity.Collidable, obj);
                _layers[entity.Layer].Objects.Add(obj);
            }
            else
            {
                for (int i = 0; i < entity.InARow; i++)
                {
                    GameObject next = GetGameObject(entity);
                    int x = entity.X + (i * next.Sprite.GetDimensions().X) + (i * entity.Gap);
                    next.PositionInGame = new Point(x, entity.Y);
                    this._listOfEntities.Add(next);
                    CheckCollidable(entity.Collidable, next);
                    _layers[entity.Layer].Objects.Add(next);
                }
            }
        }

        public void CheckCollidable(bool collidable, GameObject obj)
        {
            if (collidable)
            {
                _collidableEntities.Add(obj);
            }
            else
            {
                obj.IsCollidable = false;
            }
        }

        public static GameObject GetGameObject(Entity entity)
        {
            GameObject obj = CheckCharacters(entity);

            obj = CheckBlocks(entity);
            if (obj != null)
            {
                return obj;
            }

            obj = CheckCharacters(entity);
            if (obj != null)
            {
                return obj;
            }

            obj = CheckItems(entity);
            if (obj != null)
            {
                return obj;
            }

            obj = CheckBackgroundObjects(entity);
            return obj;
        }


        public static GameObject CheckItems(Entity entity)
        {
            switch (entity.Type)
            {
                case "Mushroom":
                    return new Mushroom(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "OneUp":
                    return new OneUp(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Star":
                    return new Star(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "FireFlower":
                    return new FireFlower(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "BlockCoin":
                    return new BlockCoin(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "UndergroundCoin":
                    return new UndergroundCoin(ItemFactory.GetInstance(), new Point(entity.X, entity.Y), entity.IsAboveGround);

                case "PortalGun":
                    return PortalGun.CreateInstance(ItemFactory.GetInstance(), new Point(entity.X, entity.Y));
                
                default:
                    return null;
            }
        }

        public static GameObject CheckCharacters(Entity entity)
        {
            switch (entity.Type)
            {
                case "Player":
                    GameObject mario = Mario.GetInstance();
                    ((Mario) mario).CheckpointPosition = entity.CheckpointX;
                    if (((Mario) mario).Checkpoint)
                    {
                        mario.PositionInGame = new Point(entity.CheckpointX, entity.CheckpointY);
                    }
                    else
                    {
                        mario.PositionInGame = new Point(entity.X, entity.Y);
                    }
                    CostumeChanger.GetInstance().Setup();
                    return mario;

                case "Goomba":
                    return new Goomba(EnemyFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "GreenKoopa":
                    return new Koopa(EnemyFactory.GetInstance(), new Point(entity.X, entity.Y), false);

                case "RedKoopa":
                    return new Koopa(EnemyFactory.GetInstance(), new Point(entity.X, entity.Y), true);

                case "PiranhaPlant":
                    return new PiranhaPlant(EnemyFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Bowser":
                    return new Bowser(BossFactory.GetInstance(), new Point(entity.X, entity.Y));

                default:
                    return null;
            }
        }

        public static GameObject CheckBlocks(Entity entity)
        {
            switch (entity.Type)
            {
                case "BrickBlock":
                    return new BrickBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "FloorBlock":
                    return new FloorBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "HiddenBlock":
                    GameObject block = new QuestionBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));
                    return new HiddenBlock((Block)block);

                case "PyramidBlock":
                    return new PyramidBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "QuestionBlock":
                    return new QuestionBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Pipe":
                    return new SmallPipe(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), new Vector2());

                case "CopperPipe":
                    GameObject obj = new SmallPipe(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), new Vector2());
                    obj.Sprite = BackgroundFactory.GetInstance().CreateProduct(BackgroundTypes.CopperPipe);
                    return obj;

                case "MediumPipe":
                    return new MediumPipe(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), new Vector2());

                case "PipeSegment":
                    return new PipeSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), new Vector2());

                case "Win Block":
                    return new WinBlock(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Gate":
                    return new Gate(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Button":
                    return new Button(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "InvertedButton":
                    return new Button(true, BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "CompanionCube":
                    return new CompanionCube(BlockFactory.GetInstance(), new Point(entity.X, entity.Y));

                default:
                    return null;
            }
        }

        public static GameObject CheckBackgroundObjects(Entity entity)
        {
            switch (entity.Type)
            {
                case "SmallHill":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.SmallHill);

                case "BigHill":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.BigHill);

                case "SmallBush":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.SmallBush);

                case "BigBush":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.BigBush);

                case "SmallCloud":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.SmallCloud);

                case "BigCloud":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.BigCloud);

                case "SmallTree":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.SmallTree);

                case "BigTree":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.BigTree);

                case "Black":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.BlackBackground);

                case "Castle":
                    return new BackgroundObject(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), BackgroundTypes.Castle);

                case "Flag":
                    return new Flag(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y));

                case "Flag Segment 1":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 200);

                case "Flag Segment 2":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 400);

                case "Flag Segment 3":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 600);

                case "Flag Segment 4":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 800);

                case "Flag Segment 5":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 1000);

                case "Flag Segment 6":
                    return new FlagSegment(BackgroundFactory.GetInstance(), new Point(entity.X, entity.Y), 1200);

                default:
                    return null;
            }
        }
    }
}
