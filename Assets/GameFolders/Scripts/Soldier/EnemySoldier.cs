using System;
using UnityEngine;
using GameFolders.Scripts.Tower;
using UnityEngine.AI;

namespace GameFolders.Scripts.Soldier
{
    public class EnemySoldier : Soldier
    {
        private NavMeshAgent _navMeshAgent;
        private Vector3 _mainTargetPosition;
        private Vector3 _currentTargetPosition;
        
        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void StartTask()
        {
            base.StartTask();
            _mainTargetPosition = PlayerTower.Instance.GetNewPosition();
            _currentTargetPosition = _mainTargetPosition;
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }

        private void Update()
        {
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }
    }
}
