using System.Collections.Generic;
using ATG.Session;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class SessionsView: MonoBehaviour
    {
        [SerializeField] private TMP_Text currentSessionOutput;
        [SerializeField] private SessionRecordView sessionRecordViewPrefab;
        [SerializeField] private RectTransform sessionRecordViewRoot;
        
        private List<SessionRecordView> _sessionRecordInstances = new List<SessionRecordView>();
        
        public void UpdateCurrentSession(SessionData session)
        {
            string output = $"текущая сессия: нач./длит.: {session.StartTime}/{session.Duration.ToStringFormat()}";
            currentSessionOutput.text = output;
        }
        
        public void AddSessionRecord(SessionData record)
        {
            var instance = Instantiate(sessionRecordViewPrefab, sessionRecordViewRoot);
            instance.SetupRecord(record);
            
            _sessionRecordInstances.Add(instance);
        }
        
        public void ClearSessionsRecords()
        {
            foreach (var instance in _sessionRecordInstances)
            {
                Destroy(instance.gameObject);
            }
            _sessionRecordInstances.Clear();
        }
    }
}