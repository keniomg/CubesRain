using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ECS.Data
{
    [CreateAssetMenu]
    public class BombData : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public float ExplosionForce { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public ParticleSystem ParticleSystem { get; private set; }
    }
}