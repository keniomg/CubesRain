using UnityEngine;

[CreateAssetMenu]
public class SpawnedObjectsData : ScriptableObject
{
    public int spawnedRaindropsCount;
    public int spawnedBombsCount;

    [field: SerializeField] public int startRaindropsCount { get; private set; }
    [field: SerializeField] public int startBombsCount { get; private set; }
}