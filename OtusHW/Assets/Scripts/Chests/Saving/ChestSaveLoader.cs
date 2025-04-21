using System.Linq;
using ATG.DateTimers;
using SaveSystem;

namespace ATG.RealtimeChests.Saving
{
    public sealed class ChestSaveLoader: SaveLoader<ChestPresenterPool, ChestSaveData[]>
    {
        protected override string DATA_KEY => "chest-data";
      
        public ChestSaveLoader(ISerializableRepository serializableRepository, ChestPresenterPool dataService) 
            : base(serializableRepository, dataService)
        {
        }
        
        protected override ChestSaveData[] ConvertToData()
        {
            var chests = _dataService.Chests;
            var result = new ChestSaveData[chests.Count];

            int counter = 0;
            
            foreach (var chest in chests)
            {
                result[counter] = new ChestSaveData(chest);
                counter++;
            }

            return result;
        }

        protected override void SetupData(ChestSaveData[] resourcesSet)
        {
            var availableChestsConfigs = _dataService.Configs;

            foreach (var config in availableChestsConfigs)
            {
                ChestSaveData savedData = resourcesSet.FirstOrDefault(res => res.Tag == config.Tag);

                Chest chest = null;
                
                if (savedData.Tag == config.Tag)
                {
                    CooldownTimer timer = new CooldownTimer(
                        config.Cooldown.ToTimeSpan(), 
                        savedData.StartedTime,
                        savedData.FinishedTime);
                    
                    chest = new Chest(savedData.Tag, config.MetaCreator.Create(), config.Reward, timer);
                }
                else
                {
                    chest = config.Create();
                }
                
                _dataService.AddChest(chest);
            }
        }
    }
}