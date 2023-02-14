using System;
using GameFolders.Scripts.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.AD
{
    public class RewardGoldAd : MonoBehaviour
    {
        [SerializeField] private int rewardGold;
        [SerializeField] private TextMeshProUGUI rewardGoldText;
        
        private Button _adWatchButton;

        private void Awake()
        {
            _adWatchButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _adWatchButton.onClick.AddListener(AdWatchButtonClick);
        }

        private void Start()
        {
            rewardGoldText.text = $"${rewardGold}";
        }

        private void AdWatchButtonClick()
        {
            AdManager.Instance.RewardedInterstitialAd.Show(reward => Reward() );
        }

        private void Reward()
        {
            GameController.Instance.Gold += rewardGold;
        }
    }
}
