using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class LifetimeSetterSystem : IEcsRunSystem
{
    private EcsFilter<LifetimeCountdownComponent, ObjectSpawnedMarker> _filter;

    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var lifetimeCountdownComponent = ref _filter.Get1(entity);
            float startLifetime = Random.Range(lifetimeCountdownComponent.MinimumLifetime, lifetimeCountdownComponent.MaximumLifetime);
            lifetimeCountdownComponent.StartLifetime = startLifetime;
            lifetimeCountdownComponent.CurrentLifetime = startLifetime;
            
            _filter.GetEntity(entity).Del<ObjectSpawnedMarker>();
        }
    }
}