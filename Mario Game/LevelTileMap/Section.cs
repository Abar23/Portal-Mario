using System.Collections.Generic;
using MarioGame.GameObjects;

namespace MarioGame.LevelTileMap
{
    class Section
    {
        private HashSet<GameObject> gameObjects;

        public Section()
        {
            this.gameObjects = new HashSet<GameObject>();
        }

        public void Add(GameObject obj)
        {
            this.gameObjects.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            this.gameObjects.Remove(obj);
        }

        public HashSet<GameObject> GetCollidableObjects()
        {
            HashSet<GameObject> objects = new HashSet<GameObject>();
            foreach (GameObject obj in gameObjects)
            {
                if (obj.IsCollidable)
                {
                    objects.Add(obj);
                }
            }
            return objects;
        }
    }
}
