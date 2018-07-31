using System.Collections.Generic;
using MarioGame.GameObjects;

namespace MarioGame.LevelTileMap
{
    class Level
    {
        private LevelMap levelMap;

        private List<GameObject> entities;

        public LevelMap Map => levelMap;

        public List<GameObject> Entities => entities;

        public Level(LevelParser parser)
        {
            this.levelMap = new LevelMap(parser.LevelObject.Dimensions.Width, parser.LevelObject.Dimensions.Height);
            this.entities = parser.GetData();

            foreach (GameObject gameObject in parser.GetCollidableEntities())
            {
                this.levelMap.Add(gameObject);
            }
        }

        public void Add(bool first, GameObject obj)
        {
            if (first)
            {
                entities.Insert(0, obj);
            } else
            {
                entities.Add(obj);
            }
            levelMap.Add(obj);
        }
    }
}
