using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class LifetimeSetterSystem : IEcsRunSystem
{
    private EcsFilter<LifetimeCountdownComponent, ObjectSpawnedEvent> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var lifetimeCountdownComponent = ref _filter.Get1(entity);
            float startLifetime = Random.Range(lifetimeCountdownComponent.minimumLifetime, lifetimeCountdownComponent.maximumLifetime);
            lifetimeCountdownComponent.startLifetime = startLifetime;
            lifetimeCountdownComponent.currentLifetime = startLifetime;
            
            _filter.GetEntity(entity).Del<ObjectSpawnedEvent>();
        }
    }
}