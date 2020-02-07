using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class FireMarioFactory : IFactory
    {
        private static FireMarioFactory instance;

        private Texture2D texture;

        public enum SpriteSizesInPixels
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
            FireMarioY = 16 * 10,
            ThrowRightX = 48 * 15,
            ThrowLeftX = 0
        }

        private FireMarioFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static FireMarioFactory CreateInstance(Texture2D texture)
        {
            return instance = new FireMarioFactory(texture);
        }

        public static FireMarioFactory GetInstance()
        {
            return instance;
        }

        public void ChangeTexture(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public ISprite CreateProduct(Enum type)
        {
            switch ((MarioTypes)type)
            {
                case MarioTypes.IdleRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.IdleRightX, (int)SpritePositions.FireMarioY));

                case MarioTypes.IdleLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.IdleLeftX, (int)SpritePositions.FireMarioY));

                case MarioTypes.WalkRight:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.WalkRightX, (int)SpritePositions.FireMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.WalkLeft:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.WalkLeftX, (int)SpritePositions.FireMarioY),
                        100,
                        3,
                        false);

                case MarioTypes.JumpRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeJumpRightX, (int)SpritePositions.FireMarioY));

                case MarioTypes.JumpLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeJumpLeftX, (int)SpritePositions.FireMarioY));

                case MarioTypes.CrouchRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeCrouchRightX, (int)SpritePositions.FireMarioY));

                case MarioTypes.CrouchLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.LargeCrouchLeftX, (int)SpritePositions.FireMarioY));

                case MarioTypes.FireThrowRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.ThrowRightX, (int)SpritePositions.FireMarioY));

                case MarioTypes.FireThrowLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)SpriteSizesInPixels.LargeWidth, (int)SpriteSizesInPixels.LargeHeight),
                        new Point((int)SpritePositions.ThrowLeftX, (int)SpritePositions.FireMarioY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
