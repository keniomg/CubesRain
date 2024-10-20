using UnityEngine;

namespace ECS.Components
{
    public struct BombSpawnerComponent
    {
        public GameObject prefab;
        public float minimumLifetime;
        public float maximumLifetime;
        public SpawnedObjectsData spawnedObjectsData;
    }
}