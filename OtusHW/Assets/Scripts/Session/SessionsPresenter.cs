using System;
using UI;
using VContainer.Unity;

namespace ATG.Session
{
    public sealed class SessionsPresenter: IStartable, IDisposable
    {
        private readonly SessionsView _sessionsView;
        private readonly SessionsService _service;

        public SessionsPresenter(SessionsView sessionsView, SessionsService service)
        {
            _sessionsView = sessionsView;
            _service = service;
        }
        
        public void Start()
        {
            _service.OnSessionUpdated += _sessionsView.UpdateCurrentSession;
            _service.OnSessionsRecordsUpdated += ShowSessionRecords;
            
            _sessionsView.UpdateCurrentSession(SessionData.Zero());
            
            ShowSessionRecords();
        }
        
        public void Dispose()
        {
            _service.OnSessionUpdated -= _sessionsView.UpdateCurrentSession;
            _service.OnSessionsRecordsUpdated -= ShowSessionRecords;
        }
        
        private void ShowSessionRecords()
        {
            _sessionsView.ClearSessionsRecords();
            foreach (var record in _service.SessionsRecords)
            {
                _sessionsView.AddSessionRecord(record);
            }
        }
    }
}