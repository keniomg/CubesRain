using UnityEngine;

namespace ECS.Components
{
    public struct ExplodableComponent
    {
        public float explosionRadius;
        public float explosionForce;
        public ParticleSystem particleSystem;
    }
}