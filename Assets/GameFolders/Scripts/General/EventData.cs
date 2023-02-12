using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
public class EventData : ScriptableObject
{
    public Action OnPlay;
    public Action<bool> OnFinish;
}
