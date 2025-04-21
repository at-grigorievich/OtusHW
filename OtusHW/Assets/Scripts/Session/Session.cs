using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace ATG.Session
{
    public readonly struct SessionData
    {
        public readonly int SessionID;
        public readonly DateTime StartTime;
        public readonly DateTime EndTime;
        public readonly TimeSpan Duration;

        public SessionData(int sessionID, DateTime startTime, DateTime endTime, TimeSpan duration)
        {
            SessionID = sessionID;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }
        
        public static SessionData Zero() => new SessionData(0, DateTime.Now, DateTime.Now, TimeSpan.Zero);
    }
    
    [Serializable]
    public sealed class Session
    {
        [SerializeField, ReadOnly] public int Index;
        [SerializeField, ReadOnly] public DateTime StartTime;
        [SerializeField, ReadOnly] public DateTime EndTime;
        [SerializeField, ReadOnly] public TimeSpan Duration;

        public Session(int index, DateTime startTime)
        {
            Index = index;
            StartTime = startTime;
            Duration = TimeSpan.Zero;
        }
        
        public Session(int index, DateTime startTime, DateTime endTime)
        {
            Index = index;
            StartTime = startTime;
            EndTime = endTime;
            Duration = endTime - startTime;
        }
        
        public Session(int index, DateTime startTime, DateTime endTime, TimeSpan duration)
        {
            Index = index;
            StartTime = startTime;
            EndTime = endTime;
            Duration = endTime - startTime;
        }

        public void UpdateEndTime(DateTime endTime)
        {
            EndTime = endTime;
            Duration = EndTime - StartTime;
        }
        
        public SessionData GetData() => new SessionData(Index, StartTime, EndTime, Duration);

        public static Session FromData(SessionData data) => 
            new(data.SessionID, data.StartTime, data.EndTime, data.Duration);
    }
}