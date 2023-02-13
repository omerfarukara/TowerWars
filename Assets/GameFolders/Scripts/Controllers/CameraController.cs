using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.Concretes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private EventData _eventData;

    private void Awake()
    {
        _eventData = Resources.Load("EventData") as EventData;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
