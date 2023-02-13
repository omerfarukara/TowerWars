using System;
using System.Collections.Generic;
using GameFolders.Scripts.Concretes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.SpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [Header("General Settings")] [SerializeField]
        private SpawnObject[] objects;

        [SerializeField] private float yOffset = 0f;

        [Header("Time Settings")] [SerializeField]
        private float spawnTime;

        [SerializeField] [Range(0, 100)] private float levelUpPercentage;

        [Header("Area Border Settings")] [SerializeField]
        private BorderType areaBorderType;

        [ShowIf("areaBorderType", BorderType.Rectangle)] [SerializeField]
        private Vector2 xBorders;

        [ShowIf("areaBorderType", BorderType.Rectangle)] [SerializeField]
        private Vector2 zBorders;

        [ShowIf("areaBorderType", BorderType.Circle)] [SerializeField]
        private float radius;

        [SerializeField] private Color borderGizmosColor = Color.black;

        private readonly Queue<SpawnObject> _pool = new Queue<SpawnObject>();

        protected float _currentSpawnTime;
        private float _spawnTime;

        private void Start()
        {
            _currentSpawnTime = spawnTime;
            SetNewSpawnTime();
        }

        private void Update()
        {
            SpawnTimeControl();
        }

        public void AddQueue(SpawnObject spawnObject)
        {
            _pool.Enqueue(spawnObject);
        }

        public void UpgradeSpawnTime()
        {
            _currentSpawnTime -= _currentSpawnTime * levelUpPercentage;
        }

        private void SetNewSpawnTime()
        {
            _spawnTime = _currentSpawnTime;
        }

        private void SpawnTimeControl()
        {
            _spawnTime -= Time.deltaTime;

            if (_spawnTime <= 0)
            {
                SpawnNewObject();
                SetNewSpawnTime();
            }
        }

        private void SpawnNewObject()
        {
            switch (_pool.Count)
            {
                case >= 1000:
                    throw new Exception("System is overloaded");
                case 0:
                {
                    int randomIndex = Random.Range(0, objects.Length);

                    SpawnObject newSpawnObject = Instantiate(objects[randomIndex]);

                    newSpawnObject.Initialize(this);
                    _pool.Enqueue(newSpawnObject);
                    break;
                }
            }

            SpawnObject spawnObject = _pool.Dequeue();
            spawnObject.gameObject.SetActive(true);
            spawnObject.WakeUp(GetNewPosition());
        }

        public Vector3 GetNewPosition()
        {
            float x, z;

            switch (areaBorderType)
            {
                case BorderType.Rectangle:

                    x = Random.Range(xBorders.x, xBorders.y);
                    z = Random.Range(zBorders.x, zBorders.y);

                    return new Vector3(x, yOffset, z) + transform.position;

                case BorderType.Circle:

                    x = Random.Range(-radius, radius);
                    z = Random.Range(-radius, radius);

                    Vector3 newPosition = new Vector3(x, 0, z);

                    if (Vector3.Distance(newPosition, Vector3.zero) > radius)
                    {
                        float differance = Vector3.Distance(newPosition, Vector3.zero) - radius;

                        newPosition.x = newPosition.x > 0 ? newPosition.x - differance : newPosition.x + differance;
                        newPosition.z = newPosition.z > 0 ? newPosition.z - differance : newPosition.z + differance;
                    }

                    newPosition.y = yOffset;
                    newPosition += transform.position;

                    return newPosition;

                default:
                    return Vector2.zero;
            }
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            switch (areaBorderType)
            {
                case BorderType.Rectangle:
                    float centerX = transform.position.x + (xBorders.x + xBorders.y) / 2;
                    float centerZ = transform.position.z + (zBorders.x + zBorders.y) / 2;
                    Vector3 center = new Vector3(centerX, transform.position.y, centerZ);
                    float sizeX = Mathf.Abs(xBorders.x) + Mathf.Abs(xBorders.y);
                    float sizeZ = Mathf.Abs(zBorders.x) + Mathf.Abs(zBorders.y);
                    Vector3 size = new Vector3(sizeX, 0.01f, sizeZ);
                    Gizmos.color = borderGizmosColor;
                    Gizmos.DrawCube(center, size);
                    break;
                case BorderType.Circle:
                    UnityEditor.Handles.color = borderGizmosColor;
                    UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, radius);
                    break;
            }
#endif
        }
    }
}