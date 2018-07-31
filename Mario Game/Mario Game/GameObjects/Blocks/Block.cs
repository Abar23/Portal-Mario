using MarioGame.Factories;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MarioGame.GameObjects.Blocks
{
    abstract class Block : GameObject
    {
        protected Queue<IRevealableItem> items;

        public Block(IFactory spriteFactory, Point positionInGame, Vector2 velocityOfObject) : base(spriteFactory, positionInGame, velocityOfObject)
        {
            this.hitboxColor = Color.DarkBlue;
            this.hitboxOffset = 0;
            this.solid = true;
            items = new Queue<IRevealableItem>();
        }

        public virtual void ToggleVisibility()
        {
            this.sprite.ToggleVisibility();
        }

        public virtual void AddItem(IRevealableItem item) {
            items.Enqueue(item);
        }

        public bool HasNoItems()
        {
            return items.Count == 0;
        }
    }
}
