using System;

namespace ATG.RealtimeChests.Saving
{
    public struct ChestSaveData
    {
        public ChestType Tag;
        public DateTime StartedTime;
        public DateTime FinishedTime;

        public ChestSaveData(Chest chest)
        {
            Tag = chest.Tag;
            StartedTime = chest.Timer.StartedTime;
            FinishedTime = chest.Timer.FinishedTime;
        }
    }
}