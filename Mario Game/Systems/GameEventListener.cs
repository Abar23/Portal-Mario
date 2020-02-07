using MarioGame.GameObjects;
using MarioGame.GameObjects.Background;
using MarioGame.Collision;

namespace MarioGame.Systems
{
    interface GameEventListener
    {
        void GoombaDied();
        void KoopaDied();
        void BowserDied();
        void BowserFire();
        void MarioDied();
        void Coin();
        void PowerUp();
        void OneUp();
        void PowerUpAppears();
        void GameOver();
        void BrickBreak();
        void BrickBump();
        void Jump();
        void Timer();
        void Warp();
        void ItemReveal();
        void FireBallMade();
        void Win();
        void PortalProjectileFiring();
        void FlagPoleGrabbing(FlagSegment flagSegment);
        void GateOpening(GameObject obj);
        void CompanionDied();
        void GateClosing();
        void PortalProjectileCollision(bool blue, GameObject obj, CollisionDirection collisionDirection);
        void PortalOpened();
    }
}
