using System.Globalization;
using ATG.Session;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SessionRecordView : MonoBehaviour
    {
        [SerializeField] private TMP_Text index;
        [SerializeField] private TMP_Text startTime;
        [SerializeField] private TMP_Text endTime;
        [SerializeField] private TMP_Text duration;

        public void SetupRecord(SessionData data)
        {
            index.text = data.SessionID.ToString();
            startTime.text = data.StartTime.ToString(CultureInfo.InvariantCulture);
            endTime.text = data.EndTime.ToString(CultureInfo.InvariantCulture);
            duration.text = data.Duration.ToStringFormat();
        }
    }
}