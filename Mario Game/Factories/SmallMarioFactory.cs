using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class SmallMarioFactory : IFactory
    {
        private static SmallMarioFactory instance;

        private Texture2D texture;

        public enum SpriteSizesInPixels
        {
            SmallHeight = 80,
            DeadWidth = 48,
            DeadHeight = 80,
            SmallIdleWidth = 48,
            SmallWalkWidth = 48,
            SmallJumpWidth = 48
        }

        private enum SpritePositions
        {
            MarioDeadX = 0,
            IdleRightX = 16 * 24,
            IdleLeftX = 16 * 21,
            WalkRightX = 16 * 27,
            WalkLeftX = 16 * 12,
            SmallJumpRightX = 16 * 36,
            SmallJumpLeftX = 16 * 9,
            SmallMarioY = 0
        }

        private SmallMarioFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static SmallMarioFactory CreateInstance(Texture2D texture)
        {
            return instance = new SmallMarioFactory(texture);
        }

        public static SmallMarioFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {
            switch ((MarioTypes)type)
            {
                case MarioTypes.MarioDead:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.DeadWidth, (int)SpriteSizesInPixels.DeadHeight),
                        new Point((int)SpritePositions.MarioDeadX, (int)SpritePositions.SmallMarioY));

                case MarioTypes.IdleRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallIdleWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.IdleRightX, (int)SpritePositions.SmallMarioY));

                case MarioTypes.IdleLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallIdleWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.IdleLeftX, (int)SpritePositions.SmallMarioY));

                case MarioTypes.WalkRight:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallWalkWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.WalkRightX, (int)SpritePositions.SmallMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.WalkLeft:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallWalkWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.WalkLeftX, (int)SpritePositions.SmallMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.JumpRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallJumpWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.SmallJumpRightX, (int)SpritePositions.SmallMarioY));

                case MarioTypes.JumpLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.SmallJumpWidth, (int)SpriteSizesInPixels.SmallHeight),
                        new Point((int)SpritePositions.SmallJumpLeftX, (int)SpritePositions.SmallMarioY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
