using System;
using GameFolders.Scripts.Tower;
using UnityEngine;

namespace GameFolders.Scripts.Soldier
{
    public class SoldierMagnet : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Soldier soldier))
            {
                soldier.SetNewTarget(transform.position);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Soldier soldier))
            {
                soldier.ResetTarget();
            }
        }
    }
}
