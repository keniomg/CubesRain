using Leopotam.Ecs;
using ECS.Components;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DestroyingSystem : IEcsRunSystem
{
    private EcsFilter<DestroyMarker>.Exclude<RaindropComponent> _destroyingEntities;
    private EcsFilter<DestroyMarker, RaindropComponent> _destroyingRaindrops;

    private EcsWorld _world;

    public void Run()
    {
        foreach (var entity in _destroyingEntities)
        {
            ref var hasGameObjectComponent = ref _destroyingEntities.GetEntity(entity).Get<HasGameObjectComponent>();
            ref var destroyingGameObject = ref hasGameObjectComponent.gameObject;

            if (destroyingGameObject != null)
            {
                Object.Destroy(destroyingGameObject);
            }

            _destroyingEntities.GetEntity(entity).Destroy();
        }

        foreach (var raindrop in _destroyingRaindrops)
        {
            ref var hasGameObjectComponent = ref _destroyingRaindrops.GetEntity(raindrop).Get<HasGameObjectComponent>();
            ref var destroyingGameObject = ref hasGameObjectComponent.gameObject;

            if (destroyingGameObject != null)
            {
                var destroyedEntity = _world.NewEntity();
                ref var raindropDestroyedEvent = ref destroyedEntity.Get<RaindropDestroyedEvent>();
                raindropDestroyedEvent.position = destroyingGameObject.transform.position;

                Object.Destroy(destroyingGameObject);
                _destroyingRaindrops.GetEntity(raindrop).Destroy();
            }
        }
    }
}