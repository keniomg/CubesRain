using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Components
{
    public struct RaindropsSpawnerComponent
    {
        public Transform SpawnTransform;
        public float SpawnerWidth;
        public float SpawnerLength;
        public float SpawnDelay;
        public float CurrentSpawnDelay;
        public GameObject SpawnPrefab;
        public float MinimumLifetime;
        public float MaximumLifetime;
        public SpawnedObjectsData SpawnedObjectsData;
    }
}