using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BombSpawnerData : ScriptableObject
{
    [field: SerializeField] public float minimumLifetime {get; private set; }
    [field: SerializeField] public float maximumLifetime { get; private set; }
}