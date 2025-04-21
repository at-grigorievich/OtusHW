using ATG.Session;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class SessionsView: MonoBehaviour
    {
        [SerializeField] private TMP_Text currentSessionOutput;

        public void UpdateCurrentSession(SessionData session)
        {
            string output = $"текущая сессия: нач./длит.: {session.StartTime}/{session.Duration.ToStringFormat()}";
            currentSessionOutput.text = output;
        }
    }
}