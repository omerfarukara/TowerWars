using UnityEngine;

namespace GameFolders.Scripts.SpawnSystem
{
    public abstract class SpawnObject : MonoBehaviour
    {
        protected Spawner Spawner { get; private set; }

        public void Initialize(Spawner spawner)
        {
            Spawner = spawner;
        }

        public void WakeUp(Vector3 spawnPosition)
        {
            transform.position = spawnPosition;
            StartTask();
        }

        protected abstract void StartTask();

        protected virtual void CompleteTask()
        {
            Spawner.AddQueue(this);
            gameObject.SetActive(false);
        }
    }
}