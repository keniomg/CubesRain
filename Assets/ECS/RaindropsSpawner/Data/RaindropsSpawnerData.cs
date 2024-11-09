using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu]
    public class RaindropsSpawnerData : ScriptableObject
    {
        [field: SerializeField] public float SpawnerWidth { get; private set; }
        [field: SerializeField] public float SpawnerLength { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public float MinimumLifetime { get; private set; }
        [field: SerializeField] public float MaximumLifetime { get; private set; }
    }
}