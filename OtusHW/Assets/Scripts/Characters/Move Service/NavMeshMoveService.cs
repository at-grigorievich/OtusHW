using System;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Move
{
    [Serializable]
    public sealed class NavMeshMoveServiceFactory
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float baseSpeed;
        
        public IMoveableService Create() => new NavMeshMoveService(navMeshAgent, baseSpeed);
    }
    
    public class NavMeshMoveService: IMoveableService
    {
        private readonly NavMeshAgent _agent;
        private readonly float _baseSpeed;
        
        public Vector3 CurrentPosition => _agent.transform.position;
        
        public float Speed => _agent.speed;

        public NavMeshMoveService(NavMeshAgent agent, float baseSpeed)
        {
            _agent = agent;
            _baseSpeed = baseSpeed;
        }
        
        public void MoveTo(Vector3 point)
        {
            if (!_agent.enabled || !_agent.isOnNavMesh)
            {
                Debug.LogWarning("NavMeshAgent отключен или не на NavMesh.");
                return;
            }

            _agent.isStopped = false;
            _agent.speed = _baseSpeed;
            _agent.SetDestination(point);
        }

        public void Stop()
        {
            if (!_agent.enabled) return;

            _agent.isStopped = true;
            _agent.ResetPath();
        }
    }
}