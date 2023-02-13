using System;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.SpawnSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Tower
{
    public class EnemyTower : MonoSingleton<EnemyTower>, IDamageable
    {
        [SerializeField] private int health;
        [SerializeField] private Image healthFillImage;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private float upgradeTime;

        private Spawner _spawner;
        
        private float _currentTime;
        
        public float Health { get; set; }

        private void Awake()
        {
            Singleton();
            _spawner = GetComponentInChildren<Spawner>();
        }

        private void Start()
        {
            _currentTime = upgradeTime;
            Health = health;
            healthFillImage.fillAmount = Health / health;
            healthText.text = $"{(int)health} / {(int)Health}";
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;
            
            if (_currentTime <= 0)
            {
                _spawner.UpgradeSpawnTime();
                _currentTime = upgradeTime;
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            healthFillImage.fillAmount = Health / health;
            healthText.text = $"{(int)health} / {(int)Health}";

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
