using MarioGame.Sprites;
using System;

namespace MarioGame.Factories
{
    interface IFactory
    {
        ISprite CreateProduct(Enum type);
    }
}
