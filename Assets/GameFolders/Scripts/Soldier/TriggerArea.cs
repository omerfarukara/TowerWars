using UnityEngine;

namespace GameFolders.Scripts.Soldier
{
    public class TriggerArea : MonoBehaviour
    {
        private Soldier _soldier;

        private void Awake()
        {
            _soldier = GetComponentInParent<Soldier>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Soldier soldier))
            {
                _soldier.SetNewTarget(soldier);
            }
        }
    }
}
