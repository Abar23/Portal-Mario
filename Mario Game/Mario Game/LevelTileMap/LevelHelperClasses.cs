using System.Collections.Generic;

namespace MarioGame.LevelTileMap
{
    public class Dimensions
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class Entity
    {
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Collidable { get; set; }
        public int InARow { get; set; }
        public int Gap { get; set; }
        public int Layer { get; set; }
        public IReadOnlyCollection<Entity> Entities { get; set; }
        public int PipeWarp { get; set; }
        public int PipeX { get; set; }
        public int PipeY { get; set; }
        public bool IsAboveGround { get; set; }
        public int CheckpointX { get; set; }
        public int CheckpointY { get; set; }
    }

    public class LevelObject
    {
        public Dimensions Dimensions { get; set; }
        public IReadOnlyCollection<Entity> Entities { get; set; }
    }
}
