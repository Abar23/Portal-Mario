using MarioGame.StateMachines.ItemStates;

namespace MarioGame.GameObjects
{
    interface IRevealableItem
    {
        void ChangeItemState(ItemState state);

        void RevealItem();
    }
}
