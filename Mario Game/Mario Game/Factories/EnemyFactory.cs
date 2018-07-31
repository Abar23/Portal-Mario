using MarioGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MarioGame.Factories
{
    class EnemyFactory : IFactory
    {
        private static EnemyFactory instance;

        private Texture2D texture;

        public enum EnemySizesInPixels
        {
            RedKoopaWidth = 32,
            RedKoopaHeight = 48,
            GreenKoopaWidth = 32,
            GreenKoopaHeight = 48,
            GoombaWidth = 32,
            GoombaHeight = 32,
            PiranhaHeight = 48,
            PiranhaWidth = 32
        }

        private enum EnemyPositions
        {
            RedKoopaWalkLeftX = 0,
            RedKoopaWalkRightX = 128,
            RedKoopaInShellX = 96,
            RedKoopaFeetOutOfShellX = 64,
            RedKoopaY = 0,
            RedKoopaLeftFireBallDeath = 192,
            RedKoopaRightFireBallDeath = 224,
            GreenKoopaWalkLeftX = 0,
            GreenKoopaWalkRightX = 128,
            GreenKoopaInShellX = 96,
            GreenKoopaFeetOutOfShellX = 64,
            GreenKoopaLeftFireBallDeath = 192,
            GreenKoopaRightFireBallDeath = 224,
            GreenKoopaY = 48,
            GoombaWalkX = 0,
            GoombaDeathX = 64,
            GoombaY = 96,
            GoombaFireBallDeathX = 128,
            PiranhaX = 0,
            PiranhaY = 128
        }

        private EnemyFactory(Texture2D texture)
        {
            this.texture = texture;
        }

        public static EnemyFactory CreateInstance(Texture2D texture)
        {
            return instance = new EnemyFactory(texture);
        }

        public static EnemyFactory GetInstance()
        {
            return instance;
        }

        public ISprite CreateProduct(Enum type)
        {

            switch ((EnemyTypes)type)
            {
                case EnemyTypes.Goomba:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GoombaWidth, (int)EnemySizesInPixels.GoombaHeight),
                                           new Point((int)EnemyPositions.GoombaWalkX, (int)EnemyPositions.GoombaY),
                                           100,
                                           2,
                                           true);

                case EnemyTypes.GoombaDeath:
                    return new OneShotAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GoombaHeight, (int)EnemySizesInPixels.GoombaWidth),
                                           new Point((int)EnemyPositions.GoombaDeathX, (int)EnemyPositions.GoombaY),
                                           100,
                                           1);

                case EnemyTypes.GoombaFireBallDeath:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GoombaHeight, (int)EnemySizesInPixels.GoombaWidth),
                                           new Point((int)EnemyPositions.GoombaFireBallDeathX, (int)EnemyPositions.GoombaY));

                case EnemyTypes.GreenKoopaWalkLeft:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaWalkLeftX, (int)EnemyPositions.GreenKoopaY),
                                           100,
                                           2,
                                           true);

                case EnemyTypes.GreenKoopaWalkRight:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaWalkRightX, (int)EnemyPositions.GreenKoopaY),
                                           100,
                                           2,
                                           true);

                case EnemyTypes.GreenKoopaInShell:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaInShellX, (int)EnemyPositions.GreenKoopaY));

                case EnemyTypes.GreenKoopaFeetOutOfShell:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaFeetOutOfShellX, (int)EnemyPositions.GreenKoopaY));

                case EnemyTypes.GreenKoopaLeftFireBallDeath:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaLeftFireBallDeath, (int)EnemyPositions.GreenKoopaY));

                case EnemyTypes.GreenKoopaRightFireBallDeath:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.GreenKoopaRightFireBallDeath, (int)EnemyPositions.GreenKoopaY));

                case EnemyTypes.RedKoopaWalkLeft:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.RedKoopaWidth, (int)EnemySizesInPixels.RedKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaWalkLeftX, (int)EnemyPositions.RedKoopaY),
                                           100,
                                           2,
                                           true);

                case EnemyTypes.RedKoopaWalkRight:
                    return new AnimatedAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.RedKoopaWidth, (int)EnemySizesInPixels.RedKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaWalkRightX, (int)EnemyPositions.RedKoopaY),
                                           100,
                                           2,
                                           true);

                case EnemyTypes.RedKoopaInShell:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.RedKoopaWidth, (int)EnemySizesInPixels.RedKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaInShellX, (int)EnemyPositions.RedKoopaY));

                case EnemyTypes.RedKoopaFeetOutOfShell:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.RedKoopaWidth, (int)EnemySizesInPixels.RedKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaFeetOutOfShellX, (int)EnemyPositions.RedKoopaY));

                case EnemyTypes.RedKoopaLeftFireBallDeath:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaLeftFireBallDeath, (int)EnemyPositions.RedKoopaY));

                case EnemyTypes.RedKoopaRightFireBallDeath:
                    return new StaticAtlasSprite(texture,
                                           new Point((int)EnemySizesInPixels.GreenKoopaWidth, (int)EnemySizesInPixels.GreenKoopaHeight),
                                           new Point((int)EnemyPositions.RedKoopaRightFireBallDeath, (int)EnemyPositions.RedKoopaY));

                case EnemyTypes.Piranha:
                    return new AnimatedAtlasSprite(texture,
                       new Point((int)EnemySizesInPixels.PiranhaWidth, (int)EnemySizesInPixels.PiranhaHeight),
                       new Point((int)EnemyPositions.PiranhaX, (int)EnemyPositions.PiranhaY),
                       200,
                       2,
                       true);

                default:
                    return new NullSprite(null, new Point(), new Point());
            }
        }
    }
}
