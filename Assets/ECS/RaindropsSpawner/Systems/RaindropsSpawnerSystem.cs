using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class RaindropsSpawnerSystem : IEcsRunSystem
{
    private EcsFilter<RaindropsSpawnerComponent> _raindropsSpawners;
    private EcsFilter<SpawnedObjectsCounterComponent> _spawnedObjectsCounters;

    private EcsWorld _ecsWorld;

    public void Run()
    {
        foreach (var entity in _raindropsSpawners)
        {
            ref var spawnerComponent = ref _raindropsSpawners.Get1(entity);
            spawnerComponent.CurrentSpawnDelay -= Time.deltaTime;

            if (spawnerComponent.CurrentSpawnDelay <= 0)
            {
                spawnerComponent.CurrentSpawnDelay = spawnerComponent.SpawnDelay;

                Vector3 spawnPosition = GetSpawnPosition(spawnerComponent);

                var raindropObject = Object.Instantiate(spawnerComponent.SpawnPrefab, spawnPosition, Quaternion.identity);
                var raindropEntity = _ecsWorld.NewEntity();

                InitializeRaindropEntity(spawnerComponent, raindropEntity, raindropObject);
                CountSpawnedRaindrops(spawnerComponent);
            }
        }
    }

    private float GetRandomPosition(float position, float offset)
    {
        float minimumSpawnPosition = position - offset;
        float maximumSpawnPosition = position + offset;
        float spawnPosition = Random.Range(minimumSpawnPosition, maximumSpawnPosition);

        return spawnPosition;
    }

    private void InitializeRaindropEntity(RaindropsSpawnerComponent spawnerComponent, EcsEntity raindropEntity, GameObject raindropObject)
    {
        raindropEntity.Get<RaindropMarker>();
        ref var lifetimeCountdownComponent = ref raindropEntity.Get<LifetimeCountdownComponent>();
        ref var gameObjectComponent = ref raindropEntity.Get<GameObjectComponent>();

        if (raindropObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            ref var meshRendererComponent = ref raindropEntity.Get<MeshRendererComponent>();
            meshRendererComponent.MeshRenderer = meshRenderer;
        }

        if (raindropObject.TryGetComponent(out RaindropCollisionHandler raindropCollisionHandler))
        {
            raindropCollisionHandler.RaindropEntity = raindropEntity;
        }

        lifetimeCountdownComponent.MinimumLifetime = spawnerComponent.MinimumLifetime;
        lifetimeCountdownComponent.MaximumLifetime = spawnerComponent.MaximumLifetime;
        gameObjectComponent.OwnObject = raindropObject;
        raindropEntity.Get<ObjectSpawnedMarker>();
    }

    private Vector3 GetSpawnPosition(RaindropsSpawnerComponent spawnerComponent)
    {
        const int NumbersOfSide = 2;

        float spawnPositionX = GetRandomPosition(spawnerComponent.SpawnTransform.position.x, spawnerComponent.SpawnerWidth / NumbersOfSide);
        float spawnPositionY = spawnerComponent.SpawnTransform.position.y;
        float spawnPositionZ = GetRandomPosition(spawnerComponent.SpawnTransform.position.z, spawnerComponent.SpawnerLength / NumbersOfSide);
        Vector3 spawnPosition = new(spawnPositionX, spawnPositionY, spawnPositionZ);

        return spawnPosition;
    }

    private void CountSpawnedRaindrops(RaindropsSpawnerComponent spawnerComponent)
    {
        foreach (var spawnedObjectsCounter in _spawnedObjectsCounters)
        {
            ref var spawnedObjectsCounterComponent = ref _spawnedObjectsCounters.Get1(spawnedObjectsCounter);
            spawnedObjectsCounterComponent.RaindropsSpawned++;
            spawnerComponent.SpawnedObjectsData.SpawnedRaindropsCount = spawnedObjectsCounterComponent.RaindropsSpawned;
        }
    }
}