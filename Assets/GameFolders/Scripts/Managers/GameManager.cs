using GameFolders.Scripts.Concretes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Fields And Properties

        [SerializeField] int levelCount;
        [SerializeField] int randomLevelLowerLimit;
        [SerializeField] int goldCoefficient;

        private EventData _eventData;
        private GameState _gameState;

        public GameState GameState
        {
            get => _gameState;
            set => _gameState = value;
        }

        public int Level
        {
            get => PlayerPrefs.GetInt("Level") > levelCount
                ? Random.Range(randomLevelLowerLimit, levelCount)
                : PlayerPrefs.GetInt("Level", 1);
            set
            {
                PlayerPrefs.SetInt("RealLevel", value);
                PlayerPrefs.SetInt("Level", value);
            }
        }

        public int Gold
        {
            get => PlayerPrefs.GetInt("Gold");
            set => PlayerPrefs.SetInt("Gold", value);
        }

        public int RealLevel => PlayerPrefs.GetInt("RealLevel", 1);

        public int Score
        {
            get => PlayerPrefs.GetInt("Score");
            set => PlayerPrefs.SetInt("Score", value);
        }

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            Singleton(true);
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnFinish += Finish;
        }

        #endregion

        #region Listening Methods

        private void Finish(bool statu)
        {
            if (statu)
            {
                Level++;
            }
            else
            {
            }
        }

        #endregion

        #region Unique Methods

        public bool Playability()
        {
            return _gameState == GameState.Play;
        }

        public void NextLevel()
        {
            _gameState = GameState.Play;
            SceneManager.LoadScene(Level);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        #endregion
    }
}