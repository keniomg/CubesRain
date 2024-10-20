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
            spawnerComponent.currentSpawnDelay -= Time.deltaTime;

            if (spawnerComponent.currentSpawnDelay <= 0)
            {
                spawnerComponent.currentSpawnDelay = spawnerComponent.spawnDelay;

                const int NumbersOfSide = 2;
                float spawnPositionX = GetRandomPosition(spawnerComponent.transform.position.x, spawnerComponent.spawnerWidth / NumbersOfSide);
                float spawnPositionY = spawnerComponent.transform.position.y;
                float spawnPositionZ = GetRandomPosition(spawnerComponent.transform.position.z, spawnerComponent.spawnerLength / NumbersOfSide);
                Vector3 spawnPosition = new(spawnPositionX, spawnPositionY, spawnPositionZ);

                var raindropObject = Object.Instantiate(spawnerComponent.spawnPrefab, spawnPosition, Quaternion.identity);

                var raindropEntity = _ecsWorld.NewEntity();
                ref var lifetimeCountdownComponent = ref raindropEntity.Get<LifetimeCountdownComponent>();
                ref var hasGameObjectComponent = ref raindropEntity.Get<HasGameObjectComponent>();
                ref var raindropComponent = ref raindropEntity.Get<RaindropComponent>();

                if (raindropObject.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    raindropComponent.meshRenderer = meshRenderer;
                    raindropEntity.Get<MeshRendererChangableMarker>();
                }

                if (raindropObject.TryGetComponent(out RaindropCollisionHandler raindropCollisionHandler))
                {
                    raindropCollisionHandler.RaindropEntity = raindropEntity;
                }

                lifetimeCountdownComponent.minimumLifetime = spawnerComponent.minimumLifetime;
                lifetimeCountdownComponent.maximumLifetime = spawnerComponent.maximumLifetime;
                hasGameObjectComponent.gameObject = raindropObject;
                raindropComponent.raindropGameObject = raindropObject;
                raindropEntity.Get<ObjectSpawnedEvent>();

                foreach (var spawnedObjectsCounter in _spawnedObjectsCounters)
                {
                    ref var spawnedObjectsCounterComponent = ref _spawnedObjectsCounters.Get1(spawnedObjectsCounter);
                    spawnedObjectsCounterComponent.raindropsSpawned++;
                    spawnerComponent.spawnedObjectsData.spawnedRaindropsCount = spawnedObjectsCounterComponent.raindropsSpawned;
                }
            }
        }
    }

    public float GetRandomPosition(float position, float offset)
    {
        float minimumSpawnPosition = position - offset;
        float maximumSpawnPosition = position + offset;
        float spawnPosition = Random.Range(minimumSpawnPosition, maximumSpawnPosition);

        return spawnPosition;
    }
}