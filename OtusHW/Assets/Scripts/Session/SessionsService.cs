using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ATG.Session
{
    public sealed class SessionsService: IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        
        private List<Session> _previousSessions;
        
        public Session CurrentSession { get; private set; }

        public SessionData[] SessionsRecords => _previousSessions == null
                ? Array.Empty<SessionData>()
                : _previousSessions.Select(session => session.GetData()).ToArray();
        
        public event Action<SessionData> OnSessionUpdated;
        public event Action OnSessionsRecordsUpdated;
        
        public void Start()
        {
            _previousSessions ??= new List<Session>();
            
            CurrentSession = new Session(_previousSessions.Count,DateTime.Now);
            
            _previousSessions.Add(CurrentSession);
            
            UpdateSessionDuration().Forget();
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void SetupPreviousSessions(SessionData[] previousSessionsRecords)
        {
            _previousSessions = new List<Session>(previousSessionsRecords.Length);

            foreach (var record in previousSessionsRecords)
            {
                _previousSessions.Add(Session.FromData(record));
            }
            
            OnSessionsRecordsUpdated?.Invoke();
        }

        private async UniTask UpdateSessionDuration()
        {
            while (true)
            {
                CurrentSession.UpdateEndTime(DateTime.Now);
                OnSessionUpdated?.Invoke(CurrentSession.GetData());

                await UniTask.Delay(1000, cancellationToken: _cts.Token);
            }
        }
    }
}