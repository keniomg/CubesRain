using Leopotam.Ecs;
using ECS.Components;

public class ParticlesCleanerInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _ecsWorld;
    
    public void Init()
    {
        var particlesCleanerEntity = _ecsWorld.NewEntity();
        particlesCleanerEntity.Get<ParticleCleanerComponent>();
    }
}