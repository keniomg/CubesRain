using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class ExploderSystem : IEcsRunSystem
{
    private EcsFilter<ExplodableComponent, HasGameObjectComponent, DestroyMarker> _explodableEntities;
    private EcsWorld _world;

    public void Run()
    {
        foreach (var explodable in _explodableEntities)
        {
            ref var explodableComponent = ref _explodableEntities.Get1(explodable);
            ref var hasGameObjectComponent = ref _explodableEntities.Get2(explodable);

            var explodableObject = hasGameObjectComponent.gameObject;

            if (explodableObject != null)
            {
                var hits = Physics.OverlapSphere(explodableObject.transform.position, explodableComponent.explosionRadius);

                var particleSystem = Object.Instantiate(explodableComponent.particleSystem, explodableObject.transform.position, Quaternion.identity);
                var particleSystemEntity = _world.NewEntity();
                particleSystemEntity.Get<ParticleSystemComponent>().particleSystem = particleSystem;
                particleSystemEntity.Get<HasGameObjectComponent>().gameObject = particleSystem.gameObject;

                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent(out Rigidbody rigidbody))
                    {
                        rigidbody.AddExplosionForce(explodableComponent.explosionForce, explodableObject.transform.position, explodableComponent.explosionRadius, 0, ForceMode.Impulse);
                    }
                }
            }

            var explodableEntity = _explodableEntities.GetEntity(explodable);
            explodableEntity.Del<ExplodableComponent>();
        }
    }
}