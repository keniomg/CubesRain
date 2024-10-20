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
        ref var bombSpawnerComponent = ref bombSpawnerEntity.Get<BombSpawnerComponent>();
        bombSpawnerComponent.prefab = _bombData.Prefab;
        bombSpawnerComponent.minimumLifetime = _bombSpawnerData.minimumLifetime;
        bombSpawnerComponent.maximumLifetime = _bombSpawnerData.maximumLifetime;
        _spawnedObjectsData.spawnedBombsCount = _spawnedObjectsData.startBombsCount;
        bombSpawnerComponent.spawnedObjectsData = _spawnedObjectsData;
    }   
}