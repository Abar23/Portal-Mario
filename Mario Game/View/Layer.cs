using System.Collections.Generic;
using MarioGame.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.View
{
    class Layer
    {
        private readonly Camera _camera;

        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Objects = new List<GameObject>();
        }

        public void Add(bool first, GameObject obj)
        {
            if (first)
            {
                Objects.Insert(0, obj);
            }
            else
            {
                Objects.Add(obj);
            }
        }

        public Vector2 Parallax { get; set; }
        public List<GameObject> Objects { get; private set; }

        public void Draw(SpriteBatch spriteBatch, bool hitboxesOn)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera.GetViewMatrix(Parallax));
            foreach (GameObject sprite in Objects)
            {
                sprite.Draw(spriteBatch, hitboxesOn);
            }
            spriteBatch.End();
        }
    }
}
