using Leopotam.Ecs;
using ECS.Components;

public class SpawnedObjectsCounterInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _ecsWorld;
    
    public void Init()
    {
        var counterEntity = _ecsWorld.NewEntity();
        counterEntity.Get<SpawnedObjectsCounterComponent>();
    }
}