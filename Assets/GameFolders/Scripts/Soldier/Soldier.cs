using System;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.SpawnSystem;
using UnityEngine;

namespace GameFolders.Scripts.Soldier
{
    public abstract class Soldier : SpawnObject, IDamageable
    {
        protected Animator _animator;
        protected EventData _eventData;

        public int Health { get; set; }

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
            _eventData = Resources.Load("EventData") as EventData;
        }

        protected override void StartTask()
        {
            _animator.SetTrigger("Idle");
        }

        protected override void CompleteTask()
        {
        
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            
            if (Health <= 0)
            {
                Debug.Log($"Dead {gameObject.name}");
            }
        }
    }
}
