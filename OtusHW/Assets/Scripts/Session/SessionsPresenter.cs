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
            _sessionsView.UpdateCurrentSession(SessionData.Zero());
        }

        public void Dispose()
        {
            _service.OnSessionUpdated -= _sessionsView.UpdateCurrentSession;
        }
    }
}