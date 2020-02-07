using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class BossFactory : IFactory
    {
        private static BossFactory instance;

        private Texture2D texture;

        public enum BossSizesInPixels
        {
            BowserHeight = 80,
            BowserWidth = 64,
            FireballHeight = 32,
            FireballWidth = 48
        }

        private enum BossPositions
        {
            Zero = 0,
            BowserRightX = 64,
            BowserChargeY = 80,
            BowserFireY = 160,
            BowserShellY = 240,
            FireballLeftY = 320,
            FireballRightY = 352,
            BowserDeadX = 128
        }

        private BossFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static BossFactory CreateInstance(Texture2D texture)
        {
            return instance = new BossFactory(texture);
        }

        public static BossFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {

            switch ((BossTypes)type)
            {
                case BossTypes.BowserIdleLeft:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                                           new Point((int)BossPositions.Zero, (int)BossPositions.Zero));

                case BossTypes.BowserIdleRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.BowserRightX, (int)BossPositions.Zero));

                case BossTypes.BowserChargeLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.Zero, (int)BossPositions.BowserChargeY));

                case BossTypes.BowserChargeRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.BowserRightX, (int)BossPositions.BowserChargeY));

                case BossTypes.BowserFireLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.Zero, (int)BossPositions.BowserFireY));

                case BossTypes.BowserFireRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.BowserRightX, (int)BossPositions.BowserFireY));

                case BossTypes.BowserShell:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.Zero, (int)BossPositions.BowserShellY),
                        50,
                        6,
                        true);

                case BossTypes.FireballLeft:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.FireballWidth, (int)BossSizesInPixels.FireballHeight),
                        new Point((int)BossPositions.Zero, (int)BossPositions.FireballLeftY),
                        50,
                        3,
                        false);

                case BossTypes.FireballRight:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.FireballWidth, (int)BossSizesInPixels.FireballHeight),
                        new Point((int)BossPositions.Zero, (int)BossPositions.FireballRightY),
                        50,
                        3,
                        false);

                case BossTypes.BowserDead:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BossSizesInPixels.BowserWidth, (int)BossSizesInPixels.BowserHeight),
                        new Point((int)BossPositions.BowserDeadX, (int)BossPositions.Zero));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
