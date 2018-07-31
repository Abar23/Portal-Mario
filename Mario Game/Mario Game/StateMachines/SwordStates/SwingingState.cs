using MarioGame.Factories;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Player;
using Microsoft.Xna.Framework.Graphics;

namespace MarioGame.StateMachines.SwordStates
{
    class SwingingState : SwordState
    {
        const int YPositionToMariosMouth = 16;

        public SwingingState(Sword sword)
        {
            this.sword = sword;
            this.sword.IsCollidable = true;

            if (Mario.GetInstance().IsFacingLeft)
            {
                this.sword.Sprite = this.sword.SpriteFactory.CreateProduct(EffectTypes.SwordLeft);
                this.sword.SetXPositionInGame = Mario.GetInstance().PositionInGame.X - (int)EffectsFactory.EffectSizesInPixels.SwordWidth;
            }
            else
            {
                this.sword.Sprite = this.sword.SpriteFactory.CreateProduct(EffectTypes.SwordRight);
                this.sword.SetXPositionInGame = Mario.GetInstance().PositionInGame.X +
                                                (int) FireMarioFactory.SpriteSizesInPixels.LargeWidth - 16;
            }

            this.sword.SetYPositionInGame = Mario.GetInstance().PositionInGame.Y + YPositionToMariosMouth;

            this.sword.gravity.Enable();
        }

        public override void HandleReadyToSwingTransition()
        {
            this.sword.ChangeSwordState(new ReadyToSwingState(this.sword));
        }

        public override void HangleSwingingTransition()
        {
        }


        public override void Update(GraphicsDevice graphicsDevice)
        {
            if (Mario.GetInstance().IsFacingLeft)
            {
                this.sword.Sprite = this.sword.SpriteFactory.CreateProduct(EffectTypes.SwordLeft);
                this.sword.SetXPositionInGame = Mario.GetInstance().PositionInGame.X - (int)EffectsFactory.EffectSizesInPixels.SwordWidth + 12;
            }
            else
            {
                this.sword.Sprite = this.sword.SpriteFactory.CreateProduct(EffectTypes.SwordRight);
                this.sword.SetXPositionInGame = Mario.GetInstance().PositionInGame.X +
                                                (int)FireMarioFactory.SpriteSizesInPixels.LargeWidth - 16;
            }


            if (this.sword.PositionInGame.Y > Mario.GetInstance().PositionInGame.Y +
                (int) FireMarioFactory.SpriteSizesInPixels.LargeHeight - 20)
            {
                this.HandleReadyToSwingTransition();
            }
        }
    }
}
