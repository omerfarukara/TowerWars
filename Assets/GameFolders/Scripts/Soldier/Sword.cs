using UnityEngine;

namespace GameFolders.Scripts.Soldier
{
    public class Sword : MonoBehaviour
    {
        public float Damage { get; set; }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Soldier soldier))
            {
                soldier.TakeDamage(Damage);
            }
        }
    }
}
