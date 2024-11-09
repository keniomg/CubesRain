using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class ExploderSystem : IEcsRunSystem
{
    private EcsFilter<ExplodableComponent, GameObjectComponent, DestroyMarker> _explodableEntities;
    private EcsWorld _world;

    public void Run()
    {
        foreach (var explodable in _explodableEntities)
        {
            ref var explodableComponent = ref _explodableEntities.Get1(explodable);
            ref var hasGameObjectComponent = ref _explodableEntities.Get2(explodable);

            var explodableObject = hasGameObjectComponent.OwnObject;

            if (explodableObject != null)
            {
                var hits = Physics.OverlapSphere(explodableObject.transform.position, explodableComponent.ExplosionRadius);

                var particleSystem = Object.Instantiate(explodableComponent.ParticleSystem, explodableObject.transform.position, Quaternion.identity);
                var particleSystemEntity = _world.NewEntity();
                particleSystemEntity.Get<ParticleSystemComponent>().ParticleSystem = particleSystem;
                particleSystemEntity.Get<GameObjectComponent>().OwnObject = particleSystem.gameObject;

                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent(out Rigidbody rigidbody))
                    {
                        rigidbody.AddExplosionForce(explodableComponent.ExplosionForce, explodableObject.transform.position, explodableComponent.ExplosionRadius, 0, ForceMode.Impulse);
                    }
                }
            }

            var explodableEntity = _explodableEntities.GetEntity(explodable);
            explodableEntity.Del<ExplodableComponent>();
        }
    }
}