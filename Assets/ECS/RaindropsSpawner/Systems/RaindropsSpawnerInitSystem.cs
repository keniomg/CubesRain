using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;

using UnityEngine;

public class RaindropsSpawnerInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _world;

    private readonly RaindropsSpawnerData _spawnerData;
    private readonly RaindropData _raindropData;
    private readonly Transform _spawnerTransform;
    private readonly SpawnedObjectsData _spawnedObjectsData;

    public RaindropsSpawnerInitSystem(Transform spawnerTransform, RaindropsSpawnerData spawnerData, RaindropData raindropData, SpawnedObjectsData spawnedObjectsData)
    {
        _spawnerTransform = spawnerTransform;
        _spawnerData = spawnerData;
        _raindropData = raindropData;
        _spawnedObjectsData = spawnedObjectsData;
    }

    public void Init()
    {
        var raindropSpawner = _world.NewEntity();

        ref var raindropsSpawnerComponent = ref raindropSpawner.Get<RaindropsSpawnerComponent>();
        raindropsSpawnerComponent.SpawnTransform = _spawnerTransform;
        raindropsSpawnerComponent.SpawnPrefab = _raindropData.Prefab;
        raindropsSpawnerComponent.SpawnDelay = _spawnerData.SpawnDelay;
        raindropsSpawnerComponent.CurrentSpawnDelay = _spawnerData.SpawnDelay;
        raindropsSpawnerComponent.SpawnerLength = _spawnerData.SpawnerLength;
        raindropsSpawnerComponent.SpawnerWidth = _spawnerData.SpawnerWidth;
        raindropsSpawnerComponent.MinimumLifetime = _spawnerData.MinimumLifetime;
        raindropsSpawnerComponent.MaximumLifetime = _spawnerData.MaximumLifetime;
        _spawnedObjectsData.SpawnedRaindropsCount = _spawnedObjectsData.StartRaindropsCount;
        raindropsSpawnerComponent.SpawnedObjectsData = _spawnedObjectsData;
    }
}