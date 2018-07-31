using System;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Enemies;
using MarioGame.GameObjects.Player;
using MarioGame.StateMachines.PortalStates;
using MarioGame.Factories;
using MarioGame.Collision;
using MarioGame.GameObjects;

namespace MarioGame.Physics
{
    static class PortalPhysics
    {
        public static void TeleportObject(Portal portal, GameObject gameObject)
        {
            Portal destinationPortal;

            const int SmallMarioHight = 48;

            const int LargeMarioHeight = 64;

            const int Offset = 2;

            if (portal.IsBluePortal)
            {
                destinationPortal = PortalGun.GetOrangePortal;
            }
            else
            {
                destinationPortal = PortalGun.GetBluePortal;
            }

            if (portal.GetState is ActiveState && destinationPortal.GetState is ActiveState && !(gameObject is PiranhaPlant))
            {
                if (destinationPortal.DirectionOfPortal == CollisionDirection.Top)
                {
                    gameObject.SetYPositionInGame = destinationPortal.PositionInGame.Y - destinationPortal.Sprite.GetDimensions().Y - gameObject.Sprite.GetDimensions().Y - Offset;
                    gameObject.SetXPositionInGame = destinationPortal.PositionInGame.X;
                }
                else if (destinationPortal.DirectionOfPortal == CollisionDirection.Bottom)
                {
                    gameObject.SetYPositionInGame = destinationPortal.PositionInGame.Y + destinationPortal.Sprite.GetDimensions().Y + Offset;
                    gameObject.SetXPositionInGame = destinationPortal.PositionInGame.X;

                    if (gameObject is Mario)
                    {
                        if (!((Mario)gameObject).PoweredUp)
                        {
                            gameObject.SetYPositionInGame = gameObject.PositionInGame.Y - SmallMarioHight;
                        }
                        else
                        {
                            gameObject.SetYPositionInGame = gameObject.PositionInGame.Y - ((int)FireMarioFactory.SpriteSizesInPixels.LargeHeight - LargeMarioHeight);

                        }
                    }
                }
                else if (destinationPortal.DirectionOfPortal == CollisionDirection.Left)
                {
                    gameObject.SetYPositionInGame = destinationPortal.PositionInGame.Y + destinationPortal.Sprite.GetDimensions().Y - gameObject.Sprite.GetDimensions().Y;
                    gameObject.SetXPositionInGame = destinationPortal.PositionInGame.X - gameObject.Sprite.GetDimensions().X - Offset;
                }
                else if (destinationPortal.DirectionOfPortal == CollisionDirection.Right)
                {
                    gameObject.SetYPositionInGame = destinationPortal.PositionInGame.Y + destinationPortal.Sprite.GetDimensions().Y - gameObject.Sprite.GetDimensions().Y;
                    gameObject.SetXPositionInGame = destinationPortal.PositionInGame.X + destinationPortal.Sprite.GetDimensions().X + Offset;
                }
                ChangeVelocityBasedOnPortals(portal, destinationPortal, gameObject);
            }
        }

        public static void ChangeVelocityBasedOnPortals(Portal collidedPortal, Portal destinationPortal, GameObject gameObject)
        {
            switch (collidedPortal.DirectionOfPortal)
            {
                case CollisionDirection.Top:
                    if (destinationPortal.DirectionOfPortal == CollisionDirection.Top)
                    {
                        gameObject.YSpeed *= -1;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Left)
                    {
                        gameObject.XSpeed = -gameObject.YSpeed;
                        gameObject.YSpeed = 0.0f;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Right)
                    {
                        gameObject.XSpeed = gameObject.YSpeed;
                        gameObject.YSpeed = 0.0f;
                    }
                    break;

                case CollisionDirection.Bottom:
                    if (destinationPortal.DirectionOfPortal == CollisionDirection.Bottom)
                    {
                        gameObject.YSpeed = -gameObject.YSpeed;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Left)
                    {
                        gameObject.XSpeed = gameObject.YSpeed;
                        gameObject.YSpeed = 0.0f;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Right)
                    {
                        gameObject.XSpeed = -gameObject.YSpeed;
                        gameObject.YSpeed = 0.0f;
                    }
                    break;

                case CollisionDirection.Left:
                    if (destinationPortal.DirectionOfPortal == CollisionDirection.Top)
                    {
                        double temp = Math.Ceiling(gameObject.YSpeed);
                        gameObject.YSpeed = -gameObject.XSpeed;
                        gameObject.XSpeed = (float)temp;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Bottom)
                    {
                        double temp = Math.Ceiling(gameObject.YSpeed);
                        gameObject.YSpeed = gameObject.XSpeed;
                        gameObject.XSpeed = -(float)temp;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Left)
                    {
                        gameObject.XSpeed = -gameObject.XSpeed;
                    }
                    break;

                case CollisionDirection.Right:
                    if (destinationPortal.DirectionOfPortal == CollisionDirection.Top)
                    {
                        double temp = Math.Ceiling(gameObject.YSpeed);
                        gameObject.YSpeed = gameObject.XSpeed;
                        gameObject.XSpeed = -(float)temp;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Bottom)
                    {
                        double temp = Math.Ceiling(gameObject.YSpeed);
                        gameObject.YSpeed = -gameObject.XSpeed;
                        gameObject.XSpeed = (float)temp;
                    }
                    else if (destinationPortal.DirectionOfPortal == CollisionDirection.Right)
                    {
                        gameObject.XSpeed *= -1;
                    }
                    break;
            }
        }
    }
}
