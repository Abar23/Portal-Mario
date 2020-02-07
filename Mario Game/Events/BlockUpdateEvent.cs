using System;

namespace MarioGame.Events
{
    class BlockUpdateEvent
    {
        private static BlockUpdateEvent instance;

        public delegate void UndergroundEventHandler(object sender, EventArgs args);
        public delegate void AbovegroundEventHandler(object sender, EventArgs args);

        public event UndergroundEventHandler UndergroundBlockUpdate;
        public event AbovegroundEventHandler AbovegroundBlockUpdate;

        private BlockUpdateEvent()
        {
            this.UndergroundBlockUpdate = null;
        }

        public static BlockUpdateEvent CreateInstance()
        {
            return instance = new BlockUpdateEvent();
        }

        public static BlockUpdateEvent GetInstance()
        {
            return instance;
        }

        public void ChangeBlocksToUndergroundBlocks()
        {
            OnUndergroundBlockUpdate();
        }

        protected virtual void OnUndergroundBlockUpdate()
        {
            if (UndergroundBlockUpdate != null)
            {
                UndergroundBlockUpdate(this, EventArgs.Empty);
            }
        }

        public void ChangeBlocksToAbovegroundBlocks()
        {
            OnAbovegroundBlockUpdate();
        }

        protected virtual void OnAbovegroundBlockUpdate()
        {
            if (AbovegroundBlockUpdate != null)
            {
                AbovegroundBlockUpdate(this, EventArgs.Empty);
            }
        }

        public void ClearSubScribers()
        {
            this.AbovegroundBlockUpdate = null;
            this.UndergroundBlockUpdate = null;
        }
    }
}
