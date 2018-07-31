using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class ItemFactory : IFactory
    {
        private static ItemFactory instance;

        private Texture2D texture;

        public enum ItemSizesInPixels
        {
            MushroomWidth = 32,
            MushroomHeight = 32,
            OneUpWidth = 32,
            OneUpHeight = 32,
            FireFlowerWidth = 32,
            FireFlowerHeight = 32,
            StarWidth = 28,
            StarHeight = 32,
            BlockCoinWidth = 16,
            BlockCoinHeight = 28,
            UndergroundCoinWidth = 20,
            UndergroundCoinHeight = 28,
            PortalGunWidth = 42,
            PortalGunHeight = 24
        }

        private enum ItemPositions
        {
            Origin = 0,
            OneUpY = 32,
            FireFlowerY = 64,
            StarY = 96,
            BlockCoinY = 128,
            UndergroundCoinY = 156,
            PortalGunY = 184
        }

        private ItemFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static ItemFactory CreateInstance(Texture2D texture)
        {
            return instance = new ItemFactory(texture);
        }

        public static ItemFactory GetInstance()
        {
            return instance;
        }

        public void ChangeTexture(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public ISprite CreateProduct(Enum type)
        {

            switch ((ItemTypes)type)
            {
                case ItemTypes.Mushroom:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.MushroomWidth, (int)ItemSizesInPixels.MushroomHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.Origin));

                case ItemTypes.OneUp:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.OneUpWidth, (int)ItemSizesInPixels.OneUpHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.OneUpY));

                case ItemTypes.FireFlower:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.FireFlowerWidth, (int)ItemSizesInPixels.FireFlowerHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.FireFlowerY),
                                           200,
                                           4,
                                           false);

                case ItemTypes.Star:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.StarWidth, (int)ItemSizesInPixels.StarHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.StarY),
                                           100,
                                           4,
                                           false);

                case ItemTypes.BlockCoin:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.BlockCoinWidth, (int)ItemSizesInPixels.BlockCoinHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.BlockCoinY),
                                           150,
                                           4,
                                           true);

                case ItemTypes.UndergroundCoin:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.UndergroundCoinWidth,(int)ItemSizesInPixels.UndergroundCoinHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.UndergroundCoinY),
                                           150,
                                           3,
                                           false);

                case ItemTypes.PortalGun:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)ItemSizesInPixels.PortalGunWidth, (int)ItemSizesInPixels.PortalGunHeight),
                                           new Point((int)ItemPositions.Origin, (int)ItemPositions.PortalGunY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
