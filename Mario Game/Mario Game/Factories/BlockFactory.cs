using System;
using System.Collections.Generic;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Factories
{
    class BlockFactory : IFactory
    {
        private static BlockFactory instance;

        private Texture2D texture;

        public enum BlockSizeInGame
        {
            Question = 32,
            Used = 32,
            Brick = 32,
            Floor = 32,
            Pyramid = 32,
            Hidden = 32,
            Piece = 16,
            CompanionCube = 30,
            ButtonWidth = 32,
            ButtonHeight = 9,
            GateWidth = 12,
            GateHeight = 64
        }

        private enum BlockTexturePosition
        {
            Question = 0,
            Used = 5 * 32,
            Brick = 6 * 32,
            Floor = 7 * 32,
            Pyramid = 8 * 32,
            Hidden = 9 * 32,
            Piece = 6 * 32,
            UndergroundBrick = 16 * 32,
            UndergroundFloor = 17 * 32,
            UndergroundPyramidX = 576,
            CompanionCube = 20 * 32,
            UpButton = 21 * 32,
            DownButton = 22 * 32,
            Gate = 23 * 32
        }

        private BlockFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static BlockFactory CreateInstance(Texture2D texture)
        {
            return instance = new BlockFactory(texture);
        }

        public static BlockFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {

            switch ((BlockTypes)type)
            {
                case BlockTypes.Question:
                    return new AnimatedAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Question, (int)BlockSizeInGame.Question),
                                            new Point((int)BlockTexturePosition.Question, 0),
                                            200, 
                                            5, 
                                            false);
                case BlockTypes.Used:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Used, (int)BlockSizeInGame.Used),
                                            new Point((int)BlockTexturePosition.Used, 0));
                case BlockTypes.Brick:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Brick, (int)BlockSizeInGame.Brick),
                                            new Point((int)BlockTexturePosition.Brick, 0));

                case BlockTypes.UndergroundBrick:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Brick, (int)BlockSizeInGame.Brick),
                                            new Point((int)BlockTexturePosition.UndergroundBrick, 0));

                case BlockTypes.Floor:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Floor, (int)BlockSizeInGame.Floor),
                                            new Point((int)BlockTexturePosition.Floor, 0));

                case BlockTypes.UndergroundFloor:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Brick, (int)BlockSizeInGame.Brick),
                                            new Point((int)BlockTexturePosition.UndergroundFloor, 0));

                case BlockTypes.Pyramid:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Pyramid, (int)BlockSizeInGame.Pyramid),
                                            new Point((int)BlockTexturePosition.Pyramid, 0));
                case BlockTypes.UndergroundPyramid:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Pyramid, (int)BlockSizeInGame.Pyramid),
                                            new Point((int)BlockTexturePosition.UndergroundPyramidX, 0));

                case BlockTypes.Hidden:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Hidden, (int)BlockSizeInGame.Hidden),
                                            new Point((int)BlockTexturePosition.Hidden, 0));
                case BlockTypes.CompanionCube:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.CompanionCube, (int)BlockSizeInGame.CompanionCube),
                                            new Point((int)BlockTexturePosition.CompanionCube, 0));
                case BlockTypes.ButtonOut:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.ButtonWidth, (int)BlockSizeInGame.ButtonHeight),
                                            new Point((int)BlockTexturePosition.UpButton, 0));
                case BlockTypes.ButtonPushedIn:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.ButtonWidth, (int)BlockSizeInGame.ButtonHeight),
                                            new Point((int)BlockTexturePosition.DownButton, 0));
                case BlockTypes.Gate:
                    return new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.GateWidth, (int)BlockSizeInGame.GateHeight),
                                            new Point((int)BlockTexturePosition.Gate, 0));
                case BlockTypes.Broken:
                    List<ISprite> sprites = new List<ISprite>();
                    sprites.Add(new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Piece, (int)BlockSizeInGame.Piece),
                                            new Point((int)BlockTexturePosition.Piece, 0)));
                    sprites.Add(new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Piece, (int)BlockSizeInGame.Piece),
                                            new Point((int)BlockTexturePosition.Piece, 0)));
                    sprites.Add(new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Piece, (int)BlockSizeInGame.Piece),
                                            new Point((int)BlockTexturePosition.Piece, 0)));
                    sprites.Add(new StaticAtlasSprite(texture,
                                            new Point((int)BlockSizeInGame.Piece, (int)BlockSizeInGame.Piece),
                                            new Point((int)BlockTexturePosition.Piece, 0)));
                    return new ComponentAtlasSprite(20, 3, sprites);

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
