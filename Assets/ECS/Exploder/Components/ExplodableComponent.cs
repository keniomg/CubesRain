using UnityEngine;

namespace ECS.Components
{
    public struct ExplodableComponent
    {
        public float ExplosionRadius;
        public float ExplosionForce;
        public ParticleSystem ParticleSystem;
    }
}