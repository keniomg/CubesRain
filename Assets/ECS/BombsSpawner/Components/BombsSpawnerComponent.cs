using UnityEngine;

namespace ECS.Components
{
    public struct BombsSpawnerComponent
    {
        public GameObject Prefab;
        public float MinimumLifetime;
        public float MaximumLifetime;
        public SpawnedObjectsData SpawnedObjectsData;
    }
}