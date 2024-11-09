using ECS.Components;
using Leopotam.Ecs;

public class ParticlesCleanerSystem : IEcsRunSystem
{
    private EcsFilter<ParticleSystemComponent> _filter;
    
    public void Run()
    {
        foreach (var entity in _filter)
        {
            ref var particleSystemComponent = ref _filter.Get1(entity);

            if (particleSystemComponent.ParticleSystem.isStopped)
            {
                _filter.GetEntity(entity).Get<DestroyMarker>();
            }
        }
    }
}