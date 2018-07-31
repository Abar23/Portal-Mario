using MarioGame.GameObjects;
using MarioGame.GameObjects.Background;
using MarioGame.Collision;

namespace MarioGame.Systems
{
    class Events
    {
        private static Events theInstance = new Events();

        private delegate void EventAction();

        private delegate void CollisionAction(bool datum, GameObject obj, CollisionDirection collisionDirection);

        private delegate void PoleAction(FlagSegment flagSegment);

        private delegate void ObjectEvent(GameObject obj);

        private EventAction goombaDied;
        private EventAction koopaDied;
        private EventAction bowserDied;
        private EventAction bowserFire;
        private EventAction marioDied;
        private EventAction coin;
        private EventAction powerUp;
        private EventAction oneUp;
        private EventAction gameOver;
        private EventAction brickBreak;
        private EventAction brickBump;
        private EventAction jump;
        private EventAction warp;
        private EventAction timer;
        private EventAction itemReveal;
        private EventAction fireballMade;
        private EventAction win;
        private EventAction portalGunFired;
        private CollisionAction portalProjCollision;
        private PoleAction flagPoleGrabbed;
        private ObjectEvent gateOpen;
        private EventAction gateClose;
        private EventAction companionDied;
        private EventAction portalOpened;

        public static Events TheInstance { get => theInstance; }

        private Events() { }

        public void AddListener(GameEventListener listener)
        {
            this.goombaDied += listener.GoombaDied;
            this.koopaDied += listener.KoopaDied;
            this.bowserDied += listener.BowserDied;
            this.bowserFire += listener.BowserFire;
            this.marioDied += listener.MarioDied;
            this.coin += listener.Coin;
            this.powerUp += listener.PowerUp;
            this.oneUp += listener.OneUp;
            this.gameOver += listener.GameOver;
            this.brickBreak += listener.BrickBreak;
            this.brickBump += listener.BrickBump;
            this.jump += listener.Jump;
            this.warp += listener.Warp;
            this.timer += listener.Timer;
            this.itemReveal += listener.ItemReveal;
            this.fireballMade += listener.FireBallMade;
            this.win += listener.Win;
            this.portalGunFired += listener.PortalProjectileFiring;
            this.portalProjCollision += listener.PortalProjectileCollision;
            this.flagPoleGrabbed += listener.FlagPoleGrabbing;
            this.gateOpen += listener.GateOpening;
            this.gateClose += listener.GateClosing;
            this.companionDied += listener.CompanionDied;
            this.portalOpened += listener.PortalOpened;
        }

        public void GoombaDied()
        {
            this.goombaDied.Invoke();
        }

        public void MarioDied()
        {
            this.marioDied.Invoke();
        }

        public void KoopaDied()
        {
            this.koopaDied.Invoke();
        }

        public void BowserDied()
        {
            this.bowserDied.Invoke();
        }

        public void BowserFire()
        {
            this.bowserFire.Invoke();
        }

        public void Coin()
        {
            this.coin.Invoke();
        }

        public void PowerUp()
        {
            this.powerUp.Invoke();
        }

        public void OneUp()
        {
            this.oneUp.Invoke();
        }

        public void GameOver()
        {
            this.gameOver.Invoke();
        }

        public void BrickBreak()
        {
            this.brickBreak.Invoke();
        }

        public void BrickBump() {
            this.brickBump.Invoke();
        }

        public void Jump()
        {
            this.jump.Invoke();
        }

        public void Warp()
        {
            this.warp.Invoke();
        }

        public void Timer()
        {
            this.timer.Invoke();
        }

        public void ItemReveal()
        {
            this.itemReveal.Invoke();
        }

        public void FireBallMade()
        {
            this.fireballMade.Invoke();
        }

        public void Win()
        {
            this.win.Invoke();
        }

        public void PortalProjectileFiring()
        {
            this.portalGunFired.Invoke();
        }

        public void PortalProjCollision(bool blue, GameObject obj, CollisionDirection collisionDirection)
        {
            this.portalProjCollision.Invoke(blue, obj, collisionDirection);
        }

        public void FlagPoleGrab(FlagSegment fs)
        {
            this.flagPoleGrabbed.Invoke(fs);
        }

        public void OpenGate(GameObject obj)
        {
            this.gateOpen.Invoke(obj);
        }

        public void CloseGate()
        {
            this.gateClose.Invoke();
        }

        public void CompanionDied()
        {
            this.companionDied.Invoke();
        }

        public void PortalOpened()
        {
            this.portalOpened.Invoke();
        }
    }
}
