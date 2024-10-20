using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu]
    public class RaindropsSpawnerData : ScriptableObject
    {
        [field: SerializeField] public float SpawnerWidth { get; private set; }
        [field: SerializeField] public float SpawnerLength { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public float minimumLifetime { get; private set; }
        [field: SerializeField] public float maximumLifetime { get; private set; }
    }
}