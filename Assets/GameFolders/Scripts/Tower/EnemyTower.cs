using System;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.SpawnSystem;
using UnityEngine;

namespace GameFolders.Scripts.Tower
{
    public class EnemyTower : MonoSingleton<EnemyTower>, IDamageable
    {
        [SerializeField] private int health;
        
        private Spawner _spawner;
        public int Health { get; set; }

        private void Awake()
        {
            Singleton();
            _spawner = GetComponentInChildren<Spawner>();
        }

        private void Start()
        {
            Health = health;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            
            if (Health <= 0)
            {
                Debug.Log($"Dead {gameObject.name}");
            }
        }

        public Vector3 GetNewPosition()
        {
            return _spawner.GetNewPosition();
        }
    }
}
