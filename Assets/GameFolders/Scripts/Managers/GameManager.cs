using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using GameAnalyticsSDK;

public class GameManager : MonoSingleton<GameManager>
{
    #region Fields And Properties

    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;
    [SerializeField] int goldCoefficient;

    private EventData _eventData;
    GameState _gameState = GameState.Play;

    public GameState GameState
    {
        get => _gameState;
        set =>  _gameState = value;
    }

    public int Level
    {
        get => PlayerPrefs.GetInt("Level") > levelCount ? Random.Range(randomLevelLowerLimit, levelCount) : PlayerPrefs.GetInt("Level",1);
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
        GameAnalytics.Initialize();
        GameAnalytics.NewDesignEvent("Game Start");
    }
    
    private void OnEnable()
    {
        _eventData.OnFinish += Finish;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(Level);
        }
    }
    
    private void OnDisable()
    {
        _eventData.OnFinish -= Finish;
    }

    #endregion

    #region Listening Methods

    private void Finish(bool statu)
    {
        if (statu)
        {

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

    #endregion


}
