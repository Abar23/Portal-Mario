using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class EffectsFactory : IFactory
    {
        private static EffectsFactory instance;

        private Texture2D texture;

        public enum EffectSizesInPixels
        {
            FireBallWidth = 32,
            FireBallHeight = 32,
            FireWorkWidth = 32,
            FireWorkHeight = 32,
            SwordWidth = 64,
            SwordHeight = 14,
            HorizontalPortalWidth = 32,
            VeritcalPortalWidth = 10,
            HorizontalPortalHeight = 10,
            VerticalPortalHeight = 32,
            PortalProjWidth = 12,
            PortalProjHeight = 12
        }

        private enum EffectPositions
        {
            OriginX = 0,
            OriginY = 0,
            FireWorkY = 32,
            SwordY = 70,
            SwordLeftX = 64,

            TopOrangePortalY = 84,
            BottomOrangePortalY = 114,
            LeftOrangePortalY = 156,
            RightOrangePortalY = 220,
            TopBluePortalY = 94,
            BottomBluePortalY = 104,
            LeftBluePortalY = 124,
            RightBluePortalY = 188,
            BlueProjX = 256,
            OrangeProjX = 272
        }

        private EffectsFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static EffectsFactory CreateInstance(Texture2D texture)
        {
            return instance = new EffectsFactory(texture);
        }

        public static EffectsFactory GetInstance()
        {
            return instance;
        }

        public void ChangeTexture(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public ISprite CreateProduct(Enum type)
        {
            switch ((EffectTypes)type)
            {
                case EffectTypes.FireBall:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.FireBallWidth, (int)EffectSizesInPixels.FireBallHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.OriginY),
                        100,
                        8,
                        true);

                case EffectTypes.FireWork:
                    return new OneShotAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.FireWorkWidth, (int)EffectSizesInPixels.FireWorkHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.FireWorkY),
                        50,
                        3);

                case EffectTypes.BluePortalProjectile:
                    return new StaticAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.PortalProjWidth, (int)EffectSizesInPixels.PortalProjHeight),
                        new Point((int)EffectPositions.BlueProjX, (int)EffectPositions.OriginY));

                case EffectTypes.OrangePortalProjectile:
                    return new StaticAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.PortalProjWidth, (int)EffectSizesInPixels.PortalProjHeight),
                        new Point((int)EffectPositions.OrangeProjX, (int)EffectPositions.OriginY));

                case EffectTypes.TopOrangePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.HorizontalPortalWidth, (int)EffectSizesInPixels.HorizontalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.TopOrangePortalY),
                        50,
                        9,
                        false);

                case EffectTypes.BottomOrangePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.HorizontalPortalWidth, (int)EffectSizesInPixels.HorizontalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.BottomOrangePortalY),
                        50,
                        9,
                        false);

                case EffectTypes.LeftOrangePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.VeritcalPortalWidth, (int)EffectSizesInPixels.VerticalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.LeftOrangePortalY),
                        50,
                        19,
                        false);

                case EffectTypes.RightOrangePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.VeritcalPortalWidth, (int)EffectSizesInPixels.VerticalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.RightOrangePortalY),
                        50,
                        19,
                        false);

                case EffectTypes.TopBluePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.HorizontalPortalWidth, (int)EffectSizesInPixels.HorizontalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.TopBluePortalY),
                        50,
                        9,
                        false);

                case EffectTypes.BottomBluePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.HorizontalPortalWidth, (int)EffectSizesInPixels.HorizontalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.BottomBluePortalY),
                        50,
                        9,
                        false);

                case EffectTypes.LeftBluePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.VeritcalPortalWidth, (int)EffectSizesInPixels.VerticalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.LeftBluePortalY),
                        50,
                        19,
                        false);

                case EffectTypes.RightBluePortal:
                    return new AnimatedAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.VeritcalPortalWidth, (int)EffectSizesInPixels.VerticalPortalHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.RightBluePortalY),
                        50,
                        19,
                        false);

                case EffectTypes.SwordRight:
                    return new StaticAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.SwordWidth, (int)EffectSizesInPixels.SwordHeight),
                        new Point((int)EffectPositions.OriginX, (int)EffectPositions.SwordY));

                case EffectTypes.SwordLeft:
                    return new StaticAtlasSprite(texture,
                        new Point((int)EffectSizesInPixels.SwordWidth, (int)EffectSizesInPixels.SwordHeight),
                        new Point((int)EffectPositions.SwordLeftX, (int)EffectPositions.SwordY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
