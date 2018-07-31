using MarioGame.Collision;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Blocks;
using MarioGame.Physics;

namespace MarioGame.StateMachines.PortalStates
{
    class ActiveState : PortalState
    {
        public ActiveState(Portal portal)
        {
            this.portal = portal;
            this.portal.IsCollidable = true;
            this.portal.IsVisible = true;
            MarioGame.GetInstance.AddNewGameObject(true, this.portal);
        }

        public override void HandleActiveState()
        {

        }

        public override void HandleReadyToBeShot()
        {
            this.portal.ChangePortalState(new ReadyToBeShotState(this.portal));
        }

        public override void HandleCollision(GameObject gameObject, CollisionDirection collisionDirection)
        {
            if (gameObject is Portal)
            {
                if (collisionDirection == CollisionDirection.None)
                {
                    this.HandleReadyToBeShot();
                }
            }
            else if (!(gameObject is Block))
            {
                PortalPhysics.TeleportObject(this.portal, gameObject);
            }
        }

        public override void Update()
        {
            if(PortalGun.GetBluePortal.GetState is ActiveState && PortalGun.GetOrangePortal.GetState is ActiveState)
            {
                this.portal.IsSolid = true;
            }
            else
            {
                this.portal.IsSolid = false;
            }
        }
    }
}
