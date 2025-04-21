using SaveSystem;

namespace ATG.Session
{
    public sealed class SessionsSaveLoader: SaveLoader<SessionsService, SessionData[]>
    {
        public SessionsSaveLoader(ISerializableRepository serializableRepository, SessionsService dataService) 
            : base(serializableRepository, dataService)
        {
        }

        protected override string DATA_KEY => "sessions-data";
        protected override SessionData[] ConvertToData()
        {
            return _dataService.SessionsRecords;
        }

        protected override void SetupData(SessionData[] resourcesSet)
        {
            _dataService.SetupPreviousSessions(resourcesSet);
        }
    }
}