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
        raindropsSpawnerComponent.transform = _spawnerTransform;
        raindropsSpawnerComponent.spawnPrefab = _raindropData.Prefab;
        raindropsSpawnerComponent.spawnDelay = _spawnerData.SpawnDelay;
        raindropsSpawnerComponent.currentSpawnDelay = _spawnerData.SpawnDelay;
        raindropsSpawnerComponent.spawnerLength = _spawnerData.SpawnerLength;
        raindropsSpawnerComponent.spawnerWidth = _spawnerData.SpawnerWidth;
        raindropsSpawnerComponent.minimumLifetime = _spawnerData.minimumLifetime;
        raindropsSpawnerComponent.maximumLifetime = _spawnerData.maximumLifetime;
        _spawnedObjectsData.spawnedRaindropsCount = _spawnedObjectsData.startRaindropsCount;
        raindropsSpawnerComponent.spawnedObjectsData = _spawnedObjectsData;
    }
}