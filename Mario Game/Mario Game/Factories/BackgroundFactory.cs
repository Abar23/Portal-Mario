using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class BackgroundFactory : IFactory
    {
        private static BackgroundFactory instance;

        private Texture2D texture;

        public enum BackgroundSizesInPixels
        {
            BigHillWidth = 512,
            BigHillHeight = 130,
            SmallBushWidth = 64,
            SmallBushHeight = 32,
            BigBushWidth = 128,
            BigBushHeight = 32,
            SmallCloudWidth = 64,
            SmallCloudHeight = 48,
            BigCloudWidth = 128,
            BigCloudHeight = 48,
            SmallTreeWidth = 28,
            SmallTreeHeight = 60,
            BigTreeWidth = 32,
            BigTreeHeight = 92,
            PipeHeight = 64,
            MediumPipeHeight = 96,
            PipeSegmentHeight = 64,
            PipeWidth = 64,
            Castle = 160,
            FlagWidth = 48,
            FlagHeight = 48,
            FlagSegmentWidth = 5,
            FlagSegmentHeight = 40,
            BackWidth = 512,
            BackHeight = 480
        }

        private enum BackgroundPositions
        {
            Row1Height = 258,
            HillX = 0,
            HillY = 0,
            SmallBushX = 128,
            BigBushX = 0,
            SmallCloudX = 320,
            BigCloudX = 192,
            SmallTreeX = 416,
            BigTreeX = 384,
            PipeX = 0,
            MediumPipeX = 64,
            PipeSegmentX = 128,
            PipeY = 130,
            CastleX = 0,
            CastleY = 290,
            FlagX = 0,
            FlagY = 450,
            FlagSegmentX = 30,
            FlagSegmentY = 498,
            BackX = 0,
            BackY = 786,
            CopperPipeY = 194
        }

        private BackgroundFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static BackgroundFactory CreateInstance(Texture2D texture)
        {
            return instance = new BackgroundFactory(texture);
        }

        public static BackgroundFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {
            switch ((BackgroundTypes)type)
            {
                case BackgroundTypes.BigHill:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.BigHillWidth, (int)BackgroundSizesInPixels.BigHillHeight),
                                           new Point((int)BackgroundPositions.HillX, (int)BackgroundPositions.HillY));

                case BackgroundTypes.SmallBush:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.SmallBushWidth, (int)BackgroundSizesInPixels.SmallBushHeight),
                                           new Point((int)BackgroundPositions.SmallBushX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.BigBush:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.BigBushWidth, (int)BackgroundSizesInPixels.BigBushHeight),
                                           new Point((int)BackgroundPositions.BigBushX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.SmallCloud:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.SmallCloudWidth, (int)BackgroundSizesInPixels.SmallCloudHeight),
                                           new Point((int)BackgroundPositions.SmallCloudX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.BigCloud:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.BigCloudWidth, (int)BackgroundSizesInPixels.BigCloudHeight),
                                           new Point((int)BackgroundPositions.BigCloudX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.SmallTree:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.SmallTreeWidth, (int)BackgroundSizesInPixels.SmallTreeHeight),
                                           new Point((int)BackgroundPositions.SmallTreeX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.BigTree:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.BigTreeWidth, (int)BackgroundSizesInPixels.BigTreeHeight),
                                           new Point((int)BackgroundPositions.BigTreeX, (int)BackgroundPositions.Row1Height));

                case BackgroundTypes.Pipe:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)BackgroundSizesInPixels.PipeWidth, (int)BackgroundSizesInPixels.PipeHeight),
                                           new Point((int)BackgroundPositions.PipeX, (int)BackgroundPositions.PipeY));

                case BackgroundTypes.CopperPipe:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.PipeWidth, (int)BackgroundSizesInPixels.PipeHeight),
                        new Point((int)BackgroundPositions.PipeX, (int)BackgroundPositions.CopperPipeY));

                case BackgroundTypes.MediumPipe:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.PipeWidth, (int)BackgroundSizesInPixels.MediumPipeHeight),
                        new Point((int)BackgroundPositions.MediumPipeX, (int)BackgroundPositions.PipeY));

                case BackgroundTypes.PipeSegment:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.PipeWidth, (int)BackgroundSizesInPixels.PipeSegmentHeight),
                        new Point((int)BackgroundPositions.PipeSegmentX, (int)BackgroundPositions.PipeY));

                case BackgroundTypes.Castle:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.Castle, (int)BackgroundSizesInPixels.Castle),
                        new Point((int)BackgroundPositions.CastleX, (int)BackgroundPositions.CastleY));

                case BackgroundTypes.BlackBackground:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.BackWidth, (int)BackgroundSizesInPixels.BackHeight),
                        new Point((int)BackgroundPositions.BackX, (int)BackgroundPositions.BackY));

                case BackgroundTypes.Flag:
                    return new OneShotAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.FlagWidth, (int)BackgroundSizesInPixels.FlagHeight),
                        new Point((int)BackgroundPositions.FlagX, (int)BackgroundPositions.FlagY), 
                        100, 
                        5);

                case BackgroundTypes.FlagSegment:
                    return new StaticAtlasSprite(texture,
                        new Point((int)BackgroundSizesInPixels.FlagSegmentWidth, (int)BackgroundSizesInPixels.FlagSegmentHeight),
                        new Point((int)BackgroundPositions.FlagSegmentX, (int)BackgroundPositions.FlagSegmentY));

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
