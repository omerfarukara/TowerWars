using System;
using GameFolders.Scripts.Concretes;
using GameFolders.Scripts.Managers;
using GameFolders.Scripts.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolders.Scripts.Controllers
{
    public class UIController : MonoSingleton<UIController>
    {
        private EventData _eventData;

        [Header("Panels")]
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject[] otherPanels;
    
        [Header("Buttons")]
        [SerializeField] Button nextLevelButton;
        [SerializeField] Button tryAgainButton;

        private void Awake()
        {
            Singleton();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            nextLevelButton.onClick.AddListener(NextLevel);
            tryAgainButton.onClick.AddListener(TryAgain);
            _eventData.OnFinish += Finish;
        }

        private void OnDisable()
        {
            _eventData.OnFinish -= Finish;
        }

        private void Finish(bool statu)
        {
            if (statu)
            {
                victoryPanel.SetActive(true);
            }
            else
            {
                losePanel.SetActive(true);
            }
            
            PlayerTower.Instance.CloseCanvas();
            EnemyTower.Instance.CloseCanvas();
            
            foreach (GameObject otherPanel in otherPanels)
            {
                otherPanel.SetActive(false);
            }
        }

        private void NextLevel()
        {
            GameManager.Instance.NextLevel();
        }

        private void TryAgain()
        {
            GameManager.Instance.TryAgain();
        }
    }
}
