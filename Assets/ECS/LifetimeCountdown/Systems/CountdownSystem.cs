using UnityEngine;
using Leopotam.Ecs;
using ECS.Components;

public class CountdownSystem : IEcsRunSystem
{
    private EcsFilter<LifetimeCountdownComponent> _filter;

    public void Run()
    {
        foreach (var countdowner in _filter)
        {
            ref var lifetimeCountdownComponent = ref _filter.Get1(countdowner);
            lifetimeCountdownComponent.CurrentLifetime -= Time.deltaTime;

            if (lifetimeCountdownComponent.CurrentLifetime <= 0)
            {
                _filter.GetEntity(countdowner).Get<DestroyMarker>();
                _filter.GetEntity(countdowner).Del<LifetimeCountdownComponent>();
            }
        }
    }
}