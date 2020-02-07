using System;

namespace MarioGame.GameObjects
{
    interface IUndergroundBlockEventHandlers
    {
        void OnUndergroundBlockUpdate(object sender, EventArgs args);

        void OnAbovegroundBlobkUpdate(object sender, EventArgs args);
    }
}
