using System;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Background;
using MarioGame.GameObjects.Player;
using MarioGame.Sounds;
using MarioGame.Collision;

namespace MarioGame.Systems
{
    class SoundSystem : GameEventListener
    {
        public void Coin()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Coin);
        }

        public void GoombaDied()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Stomp);
        }

        public void MarioDied()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.MarioDie);
        }

        public void KoopaDied()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Stomp);
        }

        public void BowserDied()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.BowserFall);
        }

        public void BowserFire()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.BowserFire);
        }

        public void OneUp()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.OneUp);
        }

        public void PowerUp()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.PowerUp);
        }

        public void PowerUpAppears()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.PowerUpAppear);
        }

        public void GameOver()
        {
            MusicPlayer.StopPlayer();
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.GameOver);
        }

        public void BrickBreak()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.BreakBlock);
        }

        public void BrickBump()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Bump);
        }

        public void Jump()
        {
            if (Mario.GetInstance().IsPoweredUp())
            {
                Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.JumpSuper);
            }
            else
            {
                Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.JumpSmall);
            }
        }

        public void Timer()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Warning);
        }

        public void Warp()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Pipe);
        }

        public void ItemReveal()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.PowerUpAppear);
        }

        public void FireBallMade()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Fireball);
        }

        public void Win()
        {
            MusicPlayer.StopPlayer();
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.Win);
        }

        public void PortalProjectileFiring()
        {
            if (PortalGun.GetInstance().TrackingMarioPosition)
            {
                Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.PortalFire);
            }
        }

        public void PortalProjectileCollision(bool blue, GameObject obj, CollisionDirection collisionDirection)
        {
            //Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.Win);
            if (PortalGun.GetInstance().TrackingMarioPosition)
            {
                PortalGun.GeneratePortal(blue, obj, collisionDirection);
            }
        }

        public void PortalOpened()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.OpenPortal);
        }

        public void FlagPoleGrabbing(FlagSegment flagSegment)
        {
        }

        public void GateOpening(GameObject obj)
        {
            if (Math.Abs(Mario.GetInstance().PositionInGame.X - obj.PositionInGame.X) < 1000)
            {
                Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.GateOpen);
            }
        }

        public void CompanionDied()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(SoundEffectNames.CubeDeath);
        }

        public void GateClosing()
        {
            Sounds.SoundPlayer.GetInstance().PlaySoundEffect(Sounds.SoundEffectNames.GateClose);
        }
    }
}
