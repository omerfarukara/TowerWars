using System;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Interfaces;
using GameFolders.Scripts.SpawnSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Tower
{
    public class PlayerTower : MonoSingleton<PlayerTower>, IDamageable
    {
        [SerializeField] private int health;
        [SerializeField] private Image healthFillImage;
        [SerializeField] protected TextMeshProUGUI healthText;

        private EventData _eventData;
        private Spawner _spawner;
        public float Health { get; set; }

        private void Awake()
        {
            Singleton();
            _spawner = GetComponentInChildren<Spawner>();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnProductionChanged += UpgradeSpawnTime;
        }

        private void Start()
        {
            Health = health;
            healthFillImage.fillAmount = Health / health;
            healthText.text = $"{(int)health} / {(int)Health}"; ;
        }
        
        private void OnDisable()
        {
            _eventData.OnProductionChanged -= UpgradeSpawnTime;
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

        private void UpgradeSpawnTime(float newSpawnTime)
        {
            _spawner.UpgradeSpawnTime(newSpawnTime);
        }
    }
}
