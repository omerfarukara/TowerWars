using GameFolders.Scripts.Concretes;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class GameController : MonoSingleton<GameController>
    {
        // Start Prices
        [Header("Start Prices")]
        [SerializeField] private int incomeStartPrice;
        [SerializeField] private int attackStartPrice;
        [SerializeField] private int productionStartPrice;

        // Start Values
        [Header("Start Values")]
        [SerializeField] private int goldStartValue;
        [SerializeField] private int attackStartValue;
        [SerializeField] private float productionStartValue;

        // Prices Increase Coefficients
        [Header("Prices Increase Coefficients")]
        [SerializeField] private int incomePriceCoefficient;
        [SerializeField] private int attackPriceCoefficient;
        [SerializeField] private int productionPriceCoefficient;

        // Upgrade Coefficients
        [Header("Upgrade Coefficients")]
        [SerializeField] private int goldIncreaseCoefficient;
        [SerializeField] private int attackIncreaseCoefficient;
        [SerializeField] [Range(0, 1)] private float productionIncreaseCoefficient;

        private EventData _eventData;

        private int _gold;

        private int _goldCoefficient;
        private int _attack;
        private float _productionTime;
        private int _incomePrice;
        private int _attackPrice;
        private int _productionPrice;

        public int IncomePrice => _incomePrice;
        public int ProductionPrice => _productionPrice;
        public int AttackPrice => _attackPrice;
        public int Income => _goldCoefficient;
        public float ProductionTime => _productionTime;
        public int Attack => _attack;

        public int Gold
        {
            get => _gold;
            set => _gold = value;
        }

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;
            _incomePrice = incomeStartPrice;
            _attackPrice = attackStartPrice;
            _productionPrice = productionStartPrice;
            _goldCoefficient = goldStartValue;
            _attack = attackStartValue;
            _productionTime = productionStartValue;
            _eventData.OnAttackChanged?.Invoke(_attack);
            _eventData.OnProductionChanged?.Invoke(_productionTime);
        }

        private void OnEnable()
        {
            _eventData.OnRewardCoin += RewardCoin;
        }

        private void Start()
        {
            
        }

        private void OnDisable()
        {
            _eventData.OnRewardCoin -= RewardCoin;
        }

        private void RewardCoin()
        {
            _gold += _goldCoefficient;
        }

        public bool IncreaseIncome()
        {
            if (_gold < _incomePrice) return false;
        
            _gold -= _incomePrice;
            _incomePrice += incomePriceCoefficient;
            _goldCoefficient += goldIncreaseCoefficient;
            
            return true;
        }

        public bool IncreaseAttack()
        {
            if (_gold < _attackPrice) return false;
        
            _gold -= _attackPrice;
            _attackPrice += attackPriceCoefficient;
            _attack += attackIncreaseCoefficient;
            _eventData.OnAttackChanged?.Invoke(_attack);
            return true;
        }

        public bool IncreaseProduction()
        {
            if (_gold < _productionPrice) return false;
        
            _gold -= _productionPrice;
            _productionPrice += productionPriceCoefficient;
            _productionTime -= _productionTime * productionIncreaseCoefficient;
            _eventData.OnProductionChanged?.Invoke(_productionTime);
            return true;
        }
    }
}