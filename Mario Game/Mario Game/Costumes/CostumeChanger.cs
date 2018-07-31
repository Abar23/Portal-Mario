using System;
using MarioGame.Factories;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.Costumes
{
    internal class CostumeChanger
    {
        public int Index;

        public const int NumCostumes = 24;

        private struct Costume
        {
            public String Name;
            public Texture2D MarioTexture;
            public Texture2D ItemTexture;
            public Texture2D EffectsTexture;
            public float MarioGravityIntensity;
            public int ProjectileNum;
            public float ProjectileGravity;
            public float ProjectileYBounceSpeed;
            public float ProjectileInitYSpeed;
            public float ProjectileInitXSpeed;
            public bool HasSword;
            public float SwordGravity;
        };

        private readonly Costume[] _costumes;

        private static CostumeChanger _instance;

        public static CostumeChanger CreateInstance(ContentManager content)
        {
            return _instance = new CostumeChanger(content);
        }

        public static CostumeChanger GetInstance()
        {
            return _instance;
        }

        public CostumeChanger(ContentManager content)
        {
            this._costumes = new[]
            {
                new Costume()
                {
                    Name = "Default",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/Mario Sprite Sheet"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/Items Sprite Sheet"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/Mario Effects Sprite Sheet"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Baseball",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioBaseball"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/BaseballItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/BaseballEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 1,
                    ProjectileGravity = 0.75f,
                    ProjectileYBounceSpeed = -1f,
                    ProjectileInitXSpeed = 10.0f,
                    ProjectileInitYSpeed = -2.0f,
                    HasSword = true,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name =  "Black Tuxedo",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioBlackTux"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/BlackTuxItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/BlackTuxEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 32,
                    ProjectileGravity = 2.25f,
                    ProjectileYBounceSpeed = -5.0f,
                    ProjectileInitXSpeed = 4.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Caveman",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioCaveman"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/CavemanItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/CavemanEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 1.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 0.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Chef",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioChef"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/ChefItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/ChefEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 13,
                    ProjectileGravity = 1.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Diddy Kong",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioDiddyKong"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/DiddyKongItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/DiddyKongEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Doctor",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioDoctor"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/DoctorItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/DoctorEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 6,
                    ProjectileGravity = 1.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 4.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Dress",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioDress"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/DressItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/DressEffects"),
                    MarioGravityIntensity = 0.95f,
                    ProjectileNum = 1,
                    ProjectileGravity = 0.75f,
                    ProjectileYBounceSpeed = 5.0f,
                    ProjectileInitXSpeed = 2.0f,
                    ProjectileInitYSpeed = -10.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Football",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioFootball"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/FootballItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/FootballEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 1f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = -6.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Gold Mario",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioGold"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/GoldItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/GoldEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 32,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = -1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "King",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioKing"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/KingItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/KingEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 0,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = true,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Knight",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioKnight"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/KnightItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/KnightEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 0,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = true,
                    SwordGravity = 0.75f
                },
                new Costume()
                {
                    Name = "Luigi",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioLuigi"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/LuigiItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/LuigiEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 4,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Mechanic",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioMechanic"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/MechanicItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/MechanicEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 15,
                    ProjectileGravity = 1f,
                    ProjectileYBounceSpeed = -3.0f,
                    ProjectileInitXSpeed = 8.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Metal Mario",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioMetal"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/MetalItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/MetalEffects"),
                    MarioGravityIntensity = 1.2f,
                    ProjectileNum = 0,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = true,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Musician",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioMusician"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/MusicianItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/MusicianEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 50,
                    ProjectileGravity = 2.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 4.0f,
                    ProjectileInitYSpeed = 0.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Pirate",
                    MarioTexture =  content.Load<Texture2D>("Textures/Mario Sprites/MarioPirate"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/PirateItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/PirateEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 0,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = true,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name =  "Resort",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioSunshine"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/SunshineItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SunshineEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = true,
                    SwordGravity = 0.75f
                },
                new Costume()
                {
                    Name = "Samurai",
                    MarioTexture =  content.Load<Texture2D>("Textures/Mario Sprites/MarioSamurai"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/SamuraiItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SamuraiEffects"),
                    MarioGravityIntensity = 0.9f,
                    ProjectileNum = 7,
                    ProjectileGravity = 0.0f,
                    ProjectileYBounceSpeed = -2.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 0.0f,
                    HasSword = true,
                    SwordGravity = 1.5f
                },
                new Costume()
                {
                    Name = "Scientist",
                    MarioTexture =  content.Load<Texture2D>("Textures/Mario Sprites/MarioScientist"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/ScientistItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/ScientistEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 2,
                    ProjectileGravity = 4.25f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Skeleton",
                    MarioTexture =  content.Load<Texture2D>("Textures/Mario Sprites/MarioSkeleton"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/SkeletonItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SkeletonEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 4,
                    ProjectileGravity = 0f,
                    ProjectileYBounceSpeed = -6.0f,
                    ProjectileInitXSpeed = 5.0f,
                    ProjectileInitYSpeed = 0.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Snow Suit",
                    MarioTexture =  content.Load<Texture2D>("Textures/Mario Sprites/MarioSnowsuit"),
                    ItemTexture =  content.Load<Texture2D>("Textures/Items Sprites/SnowsuitItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SnowsuitEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 28,
                    ProjectileGravity = 1f,
                    ProjectileYBounceSpeed = -1.0f,
                    ProjectileInitXSpeed = 8.0f,
                    ProjectileInitYSpeed = -4.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name = "Space Suit",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioSpaceSuit"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/SpacesuitItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SpacesuitEffects"),
                    MarioGravityIntensity = 0.6f,
                    ProjectileNum = 11,
                    ProjectileGravity = 0.6f,
                    ProjectileYBounceSpeed = -9.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = 1.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                },
                new Costume()
                {
                    Name =  "Swim Wear",
                    MarioTexture = content.Load<Texture2D>("Textures/Mario Sprites/MarioSwimwear"),
                    ItemTexture = content.Load<Texture2D>("Textures/Items Sprites/SwimwearItems"),
                    EffectsTexture = content.Load<Texture2D>("Textures/Effects Sprites/SwimwearEffects"),
                    MarioGravityIntensity = 1f,
                    ProjectileNum = 6,
                    ProjectileGravity = 2.25f,
                    ProjectileYBounceSpeed = -2.0f,
                    ProjectileInitXSpeed = 6.0f,
                    ProjectileInitYSpeed = -2.0f,
                    HasSword = false,
                    SwordGravity = 1.0f
                }
            };

            this.Index = (new Random()).Next(NumCostumes);
        }

        public void Setup()
        {
            FireMarioFactory.GetInstance().ChangeTexture(_costumes[Index].MarioTexture);
            Mario.GetInstance().UpdateActionSprite();
            ItemFactory.GetInstance().ChangeTexture(_costumes[Index].ItemTexture);
            Mario.GetInstance().GravityIntensity = _costumes[Index].MarioGravityIntensity;
            Mario.GetInstance().UpdateFireballsAndSword(_costumes[Index].ProjectileNum, _costumes[Index].ProjectileGravity, 
                _costumes[Index].ProjectileYBounceSpeed, _costumes[Index].ProjectileInitXSpeed,
                _costumes[Index].ProjectileInitYSpeed, _costumes[Index].HasSword, _costumes[Index].SwordGravity);
            EffectsFactory.GetInstance().ChangeTexture(_costumes[Index].EffectsTexture);
        }

        public void NextCostume()
        {
            Index++;
            if (Index == _costumes.Length)
            {
                Index = 0;
            }
            FireMarioFactory.GetInstance().ChangeTexture(_costumes[Index].MarioTexture);
            Mario.GetInstance().UpdateActionSprite();
            ItemFactory.GetInstance().ChangeTexture(_costumes[Index].ItemTexture);
            Mario.GetInstance().GravityIntensity = _costumes[Index].MarioGravityIntensity;
            Mario.GetInstance().UpdateFireballsAndSword(_costumes[Index].ProjectileNum, _costumes[Index].ProjectileGravity,
                _costumes[Index].ProjectileYBounceSpeed, _costumes[Index].ProjectileInitXSpeed,
                _costumes[Index].ProjectileInitYSpeed, _costumes[Index].HasSword, _costumes[Index].SwordGravity);
            EffectsFactory.GetInstance().ChangeTexture(_costumes[Index].EffectsTexture);
        }

        public void PreviousCostume()
        {
            if (Index == 0)
            {
                Index = _costumes.Length;
            }
            Index--;

            FireMarioFactory.GetInstance().ChangeTexture(_costumes[Index].MarioTexture);
            Mario.GetInstance().UpdateActionSprite();
            ItemFactory.GetInstance().ChangeTexture(_costumes[Index].ItemTexture);
            Mario.GetInstance().GravityIntensity = _costumes[Index].MarioGravityIntensity;
            Mario.GetInstance().UpdateFireballsAndSword(_costumes[Index].ProjectileNum, _costumes[Index].ProjectileGravity,
                _costumes[Index].ProjectileYBounceSpeed, _costumes[Index].ProjectileInitXSpeed,
                _costumes[Index].ProjectileInitYSpeed, _costumes[Index].HasSword, _costumes[Index].SwordGravity);
            EffectsFactory.GetInstance().ChangeTexture(_costumes[Index].EffectsTexture);
        }

        public String CurrentName()
        {
            return _costumes[Index].Name;
        }
    }
}
