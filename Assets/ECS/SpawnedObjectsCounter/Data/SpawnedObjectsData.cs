using UnityEngine;

[CreateAssetMenu]
public class SpawnedObjectsData : ScriptableObject
{
    public int SpawnedRaindropsCount;
    public int SpawnedBombsCount;

    [field: SerializeField] public int StartRaindropsCount { get; private set; }
    [field: SerializeField] public int StartBombsCount { get; private set; }
}