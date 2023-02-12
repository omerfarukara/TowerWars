using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    private EventData _eventData;
    
    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }
}
