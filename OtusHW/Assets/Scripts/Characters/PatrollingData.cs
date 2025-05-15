using UnityEngine;
using UnityEngine.Serialization;

namespace ATG.Characters
{
    public class PatrollingData: MonoBehaviour
    {
        public float StopUntil;
        public Transform[] PatrolPoints;
        
        public Transform CurrentPatrolPoint;
        public Bot Bot;
        public float Period = 5f;

        private void Awake()
        {
            CurrentPatrolPoint = PatrolPoints[0];
        }

        public void UpdateFromNode()
        {
            if(StopUntil > Time.time) return;
            
            Bot.Player.DoMove(CurrentPatrolPoint);
            var sDist = (CurrentPatrolPoint.position - transform.position).sqrMagnitude;
            
            if (sDist < 1)
            {
                CurrentPatrolPoint = PatrolPoints[Random.Range(0, PatrolPoints.Length)];
                StopUntil = Time.time + Period;
            }
        }
    }
}