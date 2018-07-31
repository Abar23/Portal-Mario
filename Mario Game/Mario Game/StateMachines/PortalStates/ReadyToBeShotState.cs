using MarioGame.Collision;
using MarioGame.GameObjects;
using MarioGame.GameObjects.Effects;
using MarioGame.GameObjects.Blocks;
using MarioGame.Factories;
using MarioGame.Sprites;
using Microsoft.Xna.Framework;

namespace MarioGame.StateMachines.PortalStates
{
    class ReadyToBeShotState : PortalState
    {
        public ReadyToBeShotState(Portal portal)
        {
            this.portal = portal;
            this.portal.DirectionOfPortal = CollisionDirection.None;
            this.portal.IsVisible = false;
            this.portal.Sprite = new NullSprite(null, new Point(), new Point());
        }

        public override void HandleActiveState()
        {
            this.portal.ChangePortalState(new ActiveState(this.portal));
        }

        public override void HandleReadyToBeShot()
        {
            
        }

        public override void HandleCollision(GameObject gameObject, CollisionDirection collisionDirection)
        {
            if (gameObject is Block)
            {
                if(collisionDirection == CollisionDirection.Top)
                {
                    this.portal.DirectionOfPortal = CollisionDirection.Top;

                    if (this.portal.IsBluePortal)
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.TopBluePortal);
                    }
                    else
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.TopOrangePortal);
                    }

                    if (gameObject is Pipe)
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X + ((int)BackgroundFactory.BackgroundSizesInPixels.PipeWidth / 4),
                            gameObject.PositionInGame.Y - (int)EffectsFactory.EffectSizesInPixels.HorizontalPortalHeight);
                    }
                    else
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X, gameObject.PositionInGame.Y - (int)EffectsFactory.EffectSizesInPixels.HorizontalPortalHeight);
                    }
                }
                else if (collisionDirection == CollisionDirection.Bottom)
                {
                    this.portal.DirectionOfPortal = CollisionDirection.Bottom;

                    if (this.portal.IsBluePortal)
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.BottomBluePortal);
                    }
                    else
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.BottomOrangePortal);
                    }
                    this.portal.PositionInGame = new Point(gameObject.PositionInGame.X, gameObject.PositionInGame.Y + (int)BlockFactory.BlockSizeInGame.Brick);
                }
                else if (collisionDirection == CollisionDirection.Left)
                {
                    this.portal.DirectionOfPortal = CollisionDirection.Left;

                    if (this.portal.IsBluePortal)
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.LeftBluePortal);
                    }
                    else
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.LeftOrangePortal);
                    }

                    if (gameObject is SmallPipe || gameObject is PipeSegment)
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X - (int)EffectsFactory.EffectSizesInPixels.HorizontalPortalHeight,
                            gameObject.PositionInGame.Y + ((int)BackgroundFactory.BackgroundSizesInPixels.PipeHeight) / 4);
                    }
                    else if (gameObject is MediumPipe)
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X - (int)EffectsFactory.EffectSizesInPixels.HorizontalPortalHeight,
                            gameObject.PositionInGame.Y + ((int)BackgroundFactory.BackgroundSizesInPixels.MediumPipeHeight) / 2);
                    }
                    else
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X - (int)EffectsFactory.EffectSizesInPixels.HorizontalPortalHeight, gameObject.PositionInGame.Y);
                    }
                }
                else if (collisionDirection == CollisionDirection.Right)
                {
                    this.portal.DirectionOfPortal = CollisionDirection.Right;

                    if (this.portal.IsBluePortal)
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.RightBluePortal);
                    }
                    else
                    {
                        this.portal.Sprite = this.portal.SpriteFactory.CreateProduct(EffectTypes.RightOrangePortal);
                    }

                    if (gameObject is SmallPipe || gameObject is PipeSegment)
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X + (int)BackgroundFactory.BackgroundSizesInPixels.PipeWidth,
                            gameObject.PositionInGame.Y + ((int)BackgroundFactory.BackgroundSizesInPixels.PipeHeight) / 4);
                    }
                    else if (gameObject is MediumPipe)
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X + (int)BackgroundFactory.BackgroundSizesInPixels.PipeWidth,
                            gameObject.PositionInGame.Y + ((int)BackgroundFactory.BackgroundSizesInPixels.MediumPipeHeight) / 2);
                    }
                    else
                    {
                        this.portal.PositionInGame = new Point(gameObject.PositionInGame.X + (int)BlockFactory.BlockSizeInGame.Brick, gameObject.PositionInGame.Y);
                    }
                }

                this.HandleActiveState();
            }
        }

        public override void Update()
        {
        }
    }
}
