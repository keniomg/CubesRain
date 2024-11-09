using UnityEngine;

[CreateAssetMenu]
public class BombSpawnerData : ScriptableObject
{
    [field: SerializeField] public float MinimumLifetime {get; private set; }
    [field: SerializeField] public float MaximumLifetime { get; private set; }
}