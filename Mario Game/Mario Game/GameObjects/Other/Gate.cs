using System.Collections.Generic;
using MarioGame.Collision;
using MarioGame.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.GameObjects.Other
{
    class Gate : GameObject
    {
        private bool open;
        private HashSet<Button> relatedButtons;

        public Gate(IFactory spriteFactory, Point positionInGame) : base(spriteFactory, positionInGame, new Vector2())
        {
            this.hitboxOffset = 0;
            this.hitboxColor = Color.White;
            this.Sprite = spriteFactory.CreateProduct(BlockTypes.Gate);
            this.relatedButtons = new HashSet<Button>();
            this.solid = true;
        }

        public override void HandleCollision(CollisionDirection collisionDirection, GameObject gameObject)
        {
        }

        public void AddButton(Button button)
        {
            if (!relatedButtons.Contains(button))
            {
                relatedButtons.Add(button);
            }
        }

        protected override void UpdateLocally(GameTime gameTime, GraphicsDevice graphicsDevice)
        {
            bool shouldBeOpen = true;
            foreach (Button button in relatedButtons)
            {
                if (!button.Active())
                {
                    shouldBeOpen = false;
                }
            }
            if (shouldBeOpen && !open)
            {
                open = true;
                Systems.Events.TheInstance.OpenGate(this);
                this.IsSolid = false;
                this.IsCollidable = false;
                this.IsVisible = false;
            } else if (!shouldBeOpen && open)
            {
                open = false;
                Systems.Events.TheInstance.CloseGate();
                this.IsSolid = true;
                this.IsCollidable = true;
                this.IsVisible = true;
            }
        }
    }
}
