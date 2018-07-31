using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class SuperMarioFactory : IFactory
    {
        private static SuperMarioFactory instance;

        private Texture2D texture;

        private enum SpriteSizesInPixels
        {
            LargeWidth = 16 * 3,
            LargeHeight = 16 * 5
        }

        private enum SpritePositions
        {
            IdleRightX = 48 * 8,
            IdleLeftX = 48 * 7,
            WalkRightX = 48 * 9,
            WalkLeftX = 48 * 4,
            LargeJumpRightX = 48 * 13,
            LargeJumpLeftX = 48 * 2,
            LargeCrouchRightX = 48 * 14,
            LargeCrouchLeftX = 48,
            SuperMarioY = 16 * 5
        }

        private SuperMarioFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static SuperMarioFactory CreateInstance(Texture2D texture)
        {
            return instance = new SuperMarioFactory(texture);
        }

        public static SuperMarioFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {
            switch ((MarioTypes)type)
            {
                case MarioTypes.IdleRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.IdleRightX, (int)SpritePositions.SuperMarioY));

                case MarioTypes.IdleLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.IdleLeftX, (int)SpritePositions.SuperMarioY));

                case MarioTypes.WalkRight:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.WalkRightX, (int)SpritePositions.SuperMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.WalkLeft:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.WalkLeftX, (int)SpritePositions.SuperMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.JumpRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeJumpRightX, (int)SpritePositions.SuperMarioY));

                case MarioTypes.JumpLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeJumpLeftX, (int)SpritePositions.SuperMarioY));

                case MarioTypes.CrouchRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeCrouchRightX, (int)SpritePositions.SuperMarioY));

                case MarioTypes.CrouchLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeCrouchLeftX, (int)SpritePositions.SuperMarioY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
