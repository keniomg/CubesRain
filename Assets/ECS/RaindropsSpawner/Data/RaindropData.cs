using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu]
    public class RaindropData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}