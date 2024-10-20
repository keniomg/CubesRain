using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Components
{
    public struct RaindropsSpawnerComponent
    {
        public Transform transform;
        public float spawnerWidth;
        public float spawnerLength;
        public float spawnDelay;
        public float currentSpawnDelay;
        public GameObject spawnPrefab;
        public float minimumLifetime;
        public float maximumLifetime;
        public SpawnedObjectsData spawnedObjectsData;
    }
}