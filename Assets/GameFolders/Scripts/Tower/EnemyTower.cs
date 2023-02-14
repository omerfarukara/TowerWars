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
        [SerializeField] private int soldiersStartHitDamage;
        [SerializeField] private int damageIncreaseCoefficient;
        [SerializeField] [Range(0, 100)] private float levelUpPercentage;
        [SerializeField] private GameObject canvas;

        private EventData _eventData;
        private Spawner _spawner;
        
        private float _currentTime;
        private int _soldiersHitDamage;
        
        public int HitDamage => _soldiersHitDamage;
        
        public float Health { get; set; }

        private void Awake()
        {
            Singleton();
            _spawner = GetComponentInChildren<Spawner>();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void Start()
        {
            _currentTime = upgradeTime;
            _soldiersHitDamage = soldiersStartHitDamage;
            Health = health;
            healthFillImage.fillAmount = Health / health;
            healthText.text = $"{(int)health} / {(int)Health}";
        }

        private void Update()
        {
            _currentTime -= Time.deltaTime;

            if (!(_currentTime <= 0)) return;
            
            _spawner.UpgradeSpawnTimeEnemy(levelUpPercentage);
            _currentTime = upgradeTime;
            _soldiersHitDamage += damageIncreaseCoefficient;
        }

        public void TakeDamage(float damage)
        {
            if (Health <= 0)
            {
                _eventData.OnFinish?.Invoke(true);
                Health = 0;
                healthFillImage.fillAmount = Health / health;
                healthText.text = $"{(int)health} / {(int)Health}";
            }
            else
            {
                Health -= damage;
                healthFillImage.fillAmount = Health / health;
                healthText.text = $"{(int)health} / {(int)Health}";
            }
        }
        
        public Vector3 GetNewPosition()
        {
            return _spawner.GetNewPosition();
        }

        public void CloseCanvas()
        {
            canvas.SetActive(false);
        }
    }
}
