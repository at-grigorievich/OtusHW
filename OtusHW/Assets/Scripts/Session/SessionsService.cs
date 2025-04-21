using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.Session
{
    public sealed class SessionsService: IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        
        private List<Session> _previousSessions;
        
        public Session CurrentSession { get; private set; }

        public event Action<SessionData> OnSessionUpdated;

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

        public void SetupPreviousSessions(Session[] previousSessions)
        {
            _previousSessions = new List<Session>(previousSessions);
        }

        private async UniTask UpdateSessionDuration()
        {
            while (true)
            {
                CurrentSession.UpdateEndTime(DateTime.Now);
                OnSessionUpdated?.Invoke(CurrentSession.GetData());
                
                Debug.Log("asfasffas");
                await UniTask.Delay(1000, cancellationToken: _cts.Token);
            }
        }
    }
}