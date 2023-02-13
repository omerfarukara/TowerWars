using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.SpawnSystem;
using GameFolders.Scripts.Tower;
using UnityEngine;
using UnityEngine.AI;

namespace GameFolders.Scripts.Soldier
{
    public class Soldier : SpawnObject, IDamageable
    {
        [SerializeField] private BelongsTo belongsTo;
        [SerializeField] private int health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float stayGroundTimeWhenDeath;
        [SerializeField] private float attackRange;
        [SerializeField] private float castRange;
        [SerializeField] private float hitDamage;
        [SerializeField] private float damagePerSecond;
        
        private Animator _animator;
        private EventData _eventData;
        private NavMeshAgent _navMeshAgent;
        private TriggerArea _triggerArea;
        private Sword _sword;
        private Coroutine _checkCoroutine;
        
        private Vector3 _mainTargetPosition;
        private Vector3 _currentTargetPosition;
        private bool _isDead;
        private float _defaultMoveSpeed;
        private bool _isAttacking;
        private bool _isAttackingTower;

        public bool IsDead => _isDead;
        public float Health { get; set; }

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _eventData = Resources.Load("EventData") as EventData;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _triggerArea = GetComponentInChildren<TriggerArea>();
            _sword = GetComponentInChildren<Sword>();
        }

        private void Start()
        {
            _sword.Damage = hitDamage;
            _triggerArea.transform.localScale = Vector3.one * castRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (belongsTo == BelongsTo.Player)
            {
                if (other.TryGetComponent(out EnemyTower enemyTower))
                {
                    _isAttackingTower = true;
                }
            }
            else
            {
                if (other.TryGetComponent(out PlayerTower playerTower))
                {
                    _isAttackingTower = true;

                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (belongsTo == BelongsTo.Player)
            {
                if (other.TryGetComponent(out EnemyTower enemyTower))
                {
                    enemyTower.TakeDamage(damagePerSecond * Time.deltaTime);
                    _navMeshAgent.speed = 0;
                    _animator.SetTrigger("Attack");
                }
            }
            else
            {
                if (other.TryGetComponent(out PlayerTower playerTower))
                {
                    playerTower.TakeDamage(damagePerSecond * Time.deltaTime);
                    _navMeshAgent.speed = 0;
                    _animator.SetTrigger("Attack");
                }
            }
        }

        protected override void StartTask()
        {
            _navMeshAgent.speed = moveSpeed;
            Health = health;
            _isAttacking = false;
            _isDead = false;
            _isAttackingTower = false;
            _mainTargetPosition = belongsTo == BelongsTo.Enemy ? PlayerTower.Instance.GetNewPosition() : EnemyTower.Instance.GetNewPosition();
            _currentTargetPosition = _mainTargetPosition;
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
            
            if (Health <= 0)
            {
                StartCoroutine(DeathCoroutine());
            }
            else
            {
                _animator.SetTrigger("Knock");
            }
        }
        
        public void SetNewTarget(Soldier soldier)
        {
            if (_isAttacking || _isDead || _isAttackingTower) return;

            StartCoroutine(AttackEnemy(soldier));
        }

        public void SetNewTarget(Vector3 position)
        {
            if (_isAttacking || _isDead) return;

            _currentTargetPosition = position;
            _navMeshAgent.SetDestination(_currentTargetPosition);
            
            if (_checkCoroutine != null)
            {
                StopCoroutine(_checkCoroutine);
            }
            
            _checkCoroutine = StartCoroutine(Check());
        }

        public void ResetTarget()
        {
            if (_isAttacking || _isDead) return;

            _currentTargetPosition = _mainTargetPosition;
            _navMeshAgent.SetDestination(_currentTargetPosition);
        }

        IEnumerator AttackEnemy(Soldier soldier)
        {
            _isAttacking = true;
            
            while (!soldier.IsDead)
            {
                if (_isDead || _isAttackingTower) yield break;

                if (Vector3.Distance(transform.position, soldier.transform.position) <= attackRange)
                {
                    _navMeshAgent.speed = 0;
                    transform.LookAt(soldier.transform);
                    _animator.SetTrigger("Attack");
                }
                else
                {
                    _currentTargetPosition = soldier.transform.position;
                    _navMeshAgent.SetDestination(_currentTargetPosition);
                }
                
                yield return null;
            }

            if (!_isDead || _isAttackingTower)
            {
                _isAttacking = false;
            
                _currentTargetPosition = _mainTargetPosition;
                _navMeshAgent.SetDestination(_currentTargetPosition);
                _navMeshAgent.speed = moveSpeed;
                _animator.SetTrigger("Walk");
            }
        }

        IEnumerator DeathCoroutine()
        {
            _isDead = true;
            _animator.SetTrigger("Death");
            _navMeshAgent.speed = 0;

            yield return new WaitForSeconds(0.1f);
            _animator.SetTrigger("Death");
            yield return new WaitForSeconds(stayGroundTimeWhenDeath - 0.1f);
            CompleteTask();
        }

        IEnumerator Check()
        {
            yield return new WaitForSeconds(0.1f);
            
            if (_navMeshAgent.velocity.sqrMagnitude < 0.5f)
            {
                ResetTarget();
            }
        }
    }
}
