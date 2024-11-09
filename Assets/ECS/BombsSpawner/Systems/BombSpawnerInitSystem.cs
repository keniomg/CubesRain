using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;

public class BombSpawnerInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _world;

    private readonly BombData _bombData;
    private readonly BombSpawnerData _bombSpawnerData;
    private readonly SpawnedObjectsData _spawnedObjectsData;

    public BombSpawnerInitSystem(BombData bombData, BombSpawnerData bombSpawnerData, SpawnedObjectsData spawnedObjectsData)
    {
        _bombData = bombData;
        _bombSpawnerData = bombSpawnerData;
        _spawnedObjectsData = spawnedObjectsData;
    }

    public void Init()
    {
        EcsEntity bombSpawnerEntity = _world.NewEntity();
        ref var bombSpawnerComponent = ref bombSpawnerEntity.Get<BombsSpawnerComponent>();
        bombSpawnerComponent.Prefab = _bombData.Prefab;
        bombSpawnerComponent.MinimumLifetime = _bombSpawnerData.MinimumLifetime;
        bombSpawnerComponent.MaximumLifetime = _bombSpawnerData.MaximumLifetime;
        _spawnedObjectsData.SpawnedBombsCount = _spawnedObjectsData.StartBombsCount;
        bombSpawnerComponent.SpawnedObjectsData = _spawnedObjectsData;
    }   
}