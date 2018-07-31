using MarioGame.GameObjects;
using MarioGame.Collision;
using MarioGame.View;

namespace MarioGame.Systems
{
    class RewardSystem : GameEventListener
    {
        public void BrickBreak()
        {
        }

        public void BrickBump()
        {
        }

        public void Coin()
        {
            PlayerHUD.GetInstance().AddPoints(200);
        }

        public void FireBallMade()
        {
        }

        public void GameOver()
        {
        }

        public void GoombaDied()
        {
            PlayerHUD.GetInstance().AddPoints(100);
        }

        public void ItemReveal()
        {
        }

        public void Jump()
        {
        }

        public void KoopaDied()
        {
            PlayerHUD.GetInstance().AddPoints(100);
        }

        public void BowserDied()
        {
            PlayerHUD.GetInstance().AddPoints(1000);
        }

        public void BowserFire()
        {

        }

        public void MarioDied()
        {
        }

        public void OneUp()
        {
            PlayerHUD.GetInstance().IncreaseLives();
        }

        public void PortalProjectileCollision(bool blue, GameObject obj, CollisionDirection collisionDirection)
        {
            if (PortalGun.GetInstance().TrackingMarioPosition)
            {
                PortalGun.GetInstance().GeneratePortal(blue, obj, collisionDirection);
            }
        }

        public void PortalProjectileFiring()
        {
        }

        public void PowerUp()
        {
            PlayerHUD.GetInstance().AddPoints(1000);
        }

        public void PowerUpAppears()
        {
        }

        public void Timer()
        {
        }

        public void Warp()
        {
        }

        public void Win()
        {
        }
    }
}
