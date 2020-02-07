using Microsoft.Xna.Framework;
using MarioGame.GameObjects;
using System.Collections.Generic;

namespace MarioGame.LevelTileMap
{
    class LevelMap
    {
        public const int SECTION_SIZE = 65;

        private int sectionWidth;
        private int sectionHeight;

        private Section[,] sections;

        public LevelMap(int width, int height)
        {
            sectionWidth = (width / SECTION_SIZE) + 1;
            sectionHeight = (height / SECTION_SIZE) + 1;
            this.sections = new Section[sectionWidth, sectionHeight];

            for (int row = 0; row < sectionWidth; row++)
            {
                for (int column = 0; column < sectionHeight; column++)
                {
                    this.sections[row, column] = new Section();
                }
            }
        }

        public void Add(GameObject obj)
        {
            Rectangle hitbox = obj.GetHitBox();
            int x = hitbox.X;
            int y = hitbox.Y;

            int lowerX = GetLowerX(x);
            int upperX = GetUpperX(x);
            int lowerY = GetLowerY(y);
            int upperY = GetUpperY(y);

            sections[lowerX, lowerY].Add(obj);
            sections[upperX, lowerY].Add(obj);
            sections[lowerX, upperY].Add(obj);
            sections[upperX, upperY].Add(obj);
        }

        ///<summary>
        /// Can only remove an obj if its position has not changed since it was added.
        ///</summary>
        public void Remove(GameObject obj)
        {
            Rectangle hitbox = obj.GetHitBox();
            int x = hitbox.X;
            int y = hitbox.Y;

            int lowerX = GetLowerX(x);
            int upperX = GetUpperX(x);
            int lowerY = GetLowerY(y);
            int upperY = GetUpperY(y);

            sections[lowerX, lowerY].Remove(obj);
            sections[upperX, lowerY].Remove(obj);
            sections[lowerX, upperY].Remove(obj);
            sections[upperX, upperY].Remove(obj);
        }

        public HashSet<GameObject> GetNearbyCollidableObjects(GameObject obj)
        {
            HashSet<GameObject> objects = new HashSet<GameObject>();

            Rectangle hitbox = obj.GetHitBox();
            int x = hitbox.X;
            int y = hitbox.Y;

            int lowerX = GetLowerX(x);
            int upperX = GetUpperX(x);
            int lowerY = GetLowerY(y);
            int upperY = GetUpperY(y);

            HashSet<GameObject> sectionObjects = sections[lowerX, lowerY].GetCollidableObjects();
            foreach (GameObject possibleCollidableObj in sectionObjects)
            {
                objects.Add(possibleCollidableObj);
            }

            sectionObjects =  sections[upperX, lowerY].GetCollidableObjects();
            foreach (GameObject possibleCollidableObj in sectionObjects)
            {
                objects.Add(possibleCollidableObj);
            }

            sectionObjects =  sections[lowerX, upperY].GetCollidableObjects();
            foreach (GameObject possibleCollidableObj in sectionObjects)
            {
                objects.Add(possibleCollidableObj);
            }

            sectionObjects =  sections[upperX, upperY].GetCollidableObjects();
            foreach (GameObject possibleCollidableObj in sectionObjects)
            {
                objects.Add(possibleCollidableObj);
            }
            objects.Remove(obj);
            return objects;
        }
        
        private int GetLowerX(int x)
        {
            int lowerX = x / SECTION_SIZE;
            if (lowerX < 0)
            {
                lowerX = 0;
            }
            else if (lowerX >= sectionWidth)
            {
                lowerX = sectionWidth - 1;
            }
            return lowerX;
        }

        private int GetUpperX(int x)
        {
            int upperX = x / SECTION_SIZE + 1;
            if (upperX >= sectionWidth)
            {
                upperX = sectionWidth - 1;
            }
            else if (upperX < 0)
            {
                upperX = 0;
            }
            return upperX;
        }

        private int GetLowerY(int y)
        {
            int lowerY = y / SECTION_SIZE;
            if (lowerY < 0)
            {
                lowerY = 0;
            }
            else if (lowerY >= sectionHeight)
            {
                lowerY = sectionHeight - 1;
            }
            return lowerY;
        }

        private int GetUpperY(int y)
        {
            int upperY = y / SECTION_SIZE + 1;
            if (upperY >= sectionHeight)
            {
                upperY = sectionHeight - 1;
            }
            else if (upperY < 0)
            {
                upperY = 0;
            }
            return upperY;
        }
    }
}
