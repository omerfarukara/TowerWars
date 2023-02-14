using System;
using UnityEngine;

namespace GameFolders.Scripts.Concretes
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnPlay;
        public Action<bool> OnFinish;
        public Action OnRewardCoin;
        public Action<int> OnAttackChanged;
        public Action<float> OnProductionChanged;
    }
}
