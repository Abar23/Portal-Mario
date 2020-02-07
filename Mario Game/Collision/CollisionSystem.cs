using MarioGame.Factories;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Player;
using MarioGame.LevelTileMap;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace MarioGame.Collision
{
    class CollisionSystem
    {
        private static CollisionSystem theInstance;

        private IDictionary<GameObject, Point> objectsAndNewPoints;

        private LevelMap map;

        private CollisionSystem(LevelMap map)
        {
            SetMap(map);
        }

        public static CollisionSystem CreateTheInstance(LevelMap map)
        {
            theInstance = new CollisionSystem(map);
            return theInstance;
        }

        public static CollisionSystem GetInstance()
        {
            return theInstance;
        }

        public void SetMap(LevelMap levelMap)
        {
            objectsAndNewPoints = new Dictionary<GameObject, Point>();
            this.map = levelMap;
        }

        public void RequestMovement(GameObject obj, Point newPoint)
        {
            if (!obj.IsCollidable)
            {
                PlaceObject(obj, newPoint); 
            }
            else
            {
                objectsAndNewPoints.Add(obj, newPoint);
            }
        }

        public void Update()
        {
            foreach (KeyValuePair<GameObject, Point> entry in objectsAndNewPoints)
            {
                HashSet<GameObject> possibleObjects = map.GetNearbyCollidableObjects(entry.Key);
                HashSet<GameObject> collidedObjects = new HashSet<GameObject>();
                foreach (GameObject obj in possibleObjects)
                {
                    if (Intersect(entry.Key, entry.Value, obj))
                    {
                        collidedObjects.Add(obj);
                    }
                }
                if (collidedObjects.Count > 0)
                {
                    if (entry.Key.IsSolid)
                    {
                        foreach (GameObject obj in collidedObjects)
                        {
                            CollisionDirection direction = GetDirection(entry.Key, entry.Value, obj);
                            entry.Key.HandleCollision(direction, obj);
                            obj.HandleCollision(GetOppositeDirection(direction), entry.Key);
                        }
                        PlaceObject(entry.Key, entry.Value);
                    }
                    else
                    {
                        HandleNonSolidCollisions(entry.Key, entry.Value, collidedObjects);
                    }
                }
                else
                {
                    PlaceObject(entry.Key, entry.Value);
                }
            }
            objectsAndNewPoints.Clear();
        }

        private void HandleNonSolidCollisions(GameObject obj, Point newPostion, HashSet<GameObject> collidedObjects)
        {
            float requestedXChange = newPostion.X - obj.PositionInGame.X;
            float requestedYChange = newPostion.Y - obj.PositionInGame.Y;
            float minXChange = requestedXChange;
            float minYChange = requestedYChange;
            HashSet<GameObject> actualColliders = new HashSet<GameObject>();

            foreach (GameObject collidable in collidedObjects)
            {
                float xChangeForCollision = minXChange;
                float yChangeForCollision = minYChange;
                CollisionDirection direction = GetDirection(obj, newPostion, collidable);
                if (collidable.IsSolid)
                {
                    if (direction == CollisionDirection.Bottom)
                    {
                        float height = obj.GetHitBox().Height;
                        yChangeForCollision = collidable.GetHitBox().Y - (obj.GetHitBox().Y + height);
                    }
                    else if (direction == CollisionDirection.Top)
                    {
                        float height = collidable.GetHitBox().Height;
                        yChangeForCollision = collidable.GetHitBox().Y + height - obj.GetHitBox().Y;
                    }
                    else if (direction == CollisionDirection.Right)
                    {
                        float width = obj.GetHitBox().Width;
                        xChangeForCollision = collidable.GetHitBox().X - (obj.GetHitBox().X + width);
                    }
                    else if (direction == CollisionDirection.Left)
                    {
                        float width = collidable.GetHitBox().Width;
                        xChangeForCollision = collidable.GetHitBox().X + width - obj.GetHitBox().X;
                    }
                }

                actualColliders.Add(collidable);
                if (direction == CollisionDirection.Bottom || direction == CollisionDirection.Top)
                {
                    minYChange = yChangeForCollision;
                }
                else
                {
                    minXChange = xChangeForCollision;
                }
            }

            bool collisionWithSolid = false;
            foreach (GameObject collidable in actualColliders)
            {
                //Refactor in Sprint 3 to make a type of GameObject that becomes solid when hit from the bottom
                if (collidable.IsSolid
                    || (collidable is HiddenBlock && obj is Mario && GetDirection(obj, newPostion, collidable) == CollisionDirection.Top))
                {
                    collisionWithSolid = true;
                }
                CollisionDirection direction = GetDirection(obj, newPostion, collidable);
                obj.HandleCollision(direction, collidable);
                collidable.HandleCollision(GetOppositeDirection(direction), obj);
            }

            if (collisionWithSolid)
            {
                PlaceObject(obj, new Point(obj.PositionInGame.X + (int)minXChange, obj.PositionInGame.Y + (int)minYChange));
            }
            else
            {
                PlaceObject(obj, newPostion);
            }
        }

        private void PlaceObject(GameObject obj, Point newPoint)
        {
            map.Remove(obj);
            obj.PositionInGame = newPoint;
            map.Add(obj);
        }

        private static bool Intersect(GameObject obj, Point newPoint, GameObject collider)
        {
            Rectangle hitbox1 = obj.GetHitBox(newPoint.X, newPoint.Y);
            Rectangle hitbox2 = collider.GetHitBox();

            return hitbox1.X < hitbox2.X + hitbox2.Width &&
            hitbox1.X + hitbox1.Width > hitbox2.X &&
            hitbox1.Y < hitbox2.Y + hitbox2.Height &&
            hitbox1.Height + hitbox1.Y > hitbox2.Y;
        }

        // Only call if the two objects' hitboxes intersect!
        private CollisionDirection GetDirection(GameObject obj, Point newPoint, GameObject collider)
        {
            Rectangle objOldHitbox = obj.GetHitBox();
            Rectangle objNewHitbox = obj.GetHitBox(newPoint.X, newPoint.Y);
            Rectangle colliderHitbox = collider.GetHitBox();

            //Allows blocks to recognize neighbors
            bool colliderTopBlocked = false;
            bool colliderBottomBlocked = false;
            bool colliderRightBlocked = false;
            bool colliderLeftBlocked = false;

            if (!obj.IsSolid && collider.IsSolid)
            {
                foreach (GameObject possibleSolid in map.GetNearbyCollidableObjects(collider))
                {
                    if (possibleSolid.IsSolid)
                    {
                        Rectangle possibleSolidHitbox = possibleSolid.GetHitBox();
                        if (possibleSolidHitbox.X <= colliderHitbox.X && possibleSolidHitbox.X + possibleSolidHitbox.Width >= colliderHitbox.X + colliderHitbox.Width)
                        {
                            if (Math.Abs(possibleSolidHitbox.Y + possibleSolidHitbox.Height - colliderHitbox.Y) <= 1)
                            {
                                colliderTopBlocked = true;
                            } else if (Math.Abs(colliderHitbox.Y + colliderHitbox.Height - possibleSolidHitbox.Y) <= 1)
                            {
                                colliderBottomBlocked = true;
                            }
                        }
                        else if (possibleSolidHitbox.Y <= colliderHitbox.Y && possibleSolidHitbox.Y + possibleSolidHitbox.Height >= colliderHitbox.Y + colliderHitbox.Height)
                        {
                            if (Math.Abs(possibleSolidHitbox.X + possibleSolidHitbox.Width - colliderHitbox.X) <= 1)
                            {
                                colliderLeftBlocked = true;
                            }
                            else if (Math.Abs(colliderHitbox.X + colliderHitbox.Width - possibleSolidHitbox.X)<= 1)
                            {
                                colliderRightBlocked = true;
                            }
                        }
                    }
                }
            }

            // Determines direction of collision
            CollisionDirection direction = CollisionDirection.None;

            bool bottomCollision = false;
            float yCollisionProportion = -1f;
            if (!colliderTopBlocked && objOldHitbox.Y + objOldHitbox.Height <= colliderHitbox.Y && objNewHitbox.Y + objNewHitbox.Height > colliderHitbox.Y)
            {
                bottomCollision = true;
                yCollisionProportion = ((objNewHitbox.Y + objNewHitbox.Height) - colliderHitbox.Y) / ((float)(objNewHitbox.Y - objOldHitbox.Y));
            }
            else if (!colliderBottomBlocked && objOldHitbox.Y >= colliderHitbox.Y + colliderHitbox.Height && objNewHitbox.Y < colliderHitbox.Y + colliderHitbox.Height)
            {
                yCollisionProportion = ((colliderHitbox.Y + colliderHitbox.Height) - objNewHitbox.Y) / ((float)(objOldHitbox.Y - objNewHitbox.Y));
            }

            bool rightCollision = false;
            float xCollisionProportion = -1f;
            if (!colliderLeftBlocked && objOldHitbox.X + objOldHitbox.Width <= colliderHitbox.X && objNewHitbox.X + objNewHitbox.Width > colliderHitbox.X)
            {
                rightCollision = true;
                xCollisionProportion = ((objNewHitbox.X + objNewHitbox.Width) - colliderHitbox.X) / ((float)(objNewHitbox.X - objOldHitbox.X));
            }
            else if (!colliderRightBlocked && objOldHitbox.X >= colliderHitbox.X + colliderHitbox.Width && objNewHitbox.X < colliderHitbox.X + colliderHitbox.Width)
            {
                xCollisionProportion = ((colliderHitbox.X + colliderHitbox.Width) - objNewHitbox.X) / ((float)(objOldHitbox.X - objNewHitbox.X));
            }

            if (yCollisionProportion != -1 || xCollisionProportion != -1)
            {
                if (yCollisionProportion >= xCollisionProportion)
                {
                    if (bottomCollision)
                    {
                        direction = CollisionDirection.Bottom;
                    }
                    else
                    {
                        direction = CollisionDirection.Top;
                    }
                }
                else
                {
                    if (rightCollision)
                    {
                        direction = CollisionDirection.Right;
                    }
                    else
                    {
                        direction = CollisionDirection.Left;
                    }
                }
            }
            return direction;
        }

        private static CollisionDirection GetOppositeDirection(CollisionDirection direction)
        {
            if (direction == CollisionDirection.Top)
            {
                return CollisionDirection.Bottom;
            }
            else if (direction == CollisionDirection.Bottom)
            {
                return CollisionDirection.Top;
            }
            else if (direction == CollisionDirection.Left)
            {
                return CollisionDirection.Right;
            }
            else if (direction == CollisionDirection.Right)
            {
                return CollisionDirection.Left;
            }
            else
            {
                return CollisionDirection.None;
            }
        }
    }
}
