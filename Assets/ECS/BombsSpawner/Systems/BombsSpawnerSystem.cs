using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class BombsSpawnerSystem : IEcsRunSystem
{
    private EcsFilter<BombsSpawnerComponent> _bombSpawners;
    private EcsFilter<DestroyedRaindropPositionComponent> _destroyedRaindrops;
    private EcsFilter<SpawnedObjectsCounterComponent> _spawnedObjectsCounters;

    private EcsWorld _ecsWorld;

    public void Run()
    {
        foreach (var spawner in _bombSpawners)
        {
            ref var spawnerComponent = ref _bombSpawners.Get1(spawner);

            foreach (var destroyedRaindrop in _destroyedRaindrops)
            {
                ref var destroyedRaindropPositionComponent = ref _destroyedRaindrops.Get1(destroyedRaindrop);

                var bombObject = Object.Instantiate(spawnerComponent.Prefab, destroyedRaindropPositionComponent.Position, Quaternion.identity);
                var bombEntity = _ecsWorld.NewEntity();
                
                InitializeBombEntity(bombObject, spawnerComponent, bombEntity);
                _destroyedRaindrops.GetEntity(destroyedRaindrop).Del<DestroyedRaindropPositionComponent>();
                CountSpawnedBombs(spawnerComponent);
            }
        }
    } 
    
    private void InitializeBombEntity(GameObject bombObject, BombsSpawnerComponent spawnerComponent, EcsEntity bombEntity)
    {
        bombEntity.Get<BombMarker>();
        ref var lifetimeCountdownComponent = ref bombEntity.Get<LifetimeCountdownComponent>();
        ref var gameObjectComponent = ref bombEntity.Get<GameObjectComponent>();

        if (bombObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            ref var meshRendererComponent = ref bombEntity.Get<MeshRendererComponent>();
            meshRendererComponent.MeshRenderer = meshRenderer;
            bombEntity.Get<MaterialAlphaComponent>();
        }

        if (bombObject.TryGetComponent(out BombView bombView))
        {
            ref var explodableComponent = ref bombEntity.Get<ExplodableComponent>();

            explodableComponent.ExplosionRadius = bombView.ExplosionRadius;
            explodableComponent.ExplosionForce = bombView.ExplosionForce;
            explodableComponent.ParticleSystem = bombView.ParticleSystem;
        }

        lifetimeCountdownComponent.MinimumLifetime = spawnerComponent.MinimumLifetime;
        lifetimeCountdownComponent.MaximumLifetime = spawnerComponent.MaximumLifetime;
        gameObjectComponent.OwnObject = bombObject;
        bombEntity.Get<ObjectSpawnedMarker>();
    }

    private void CountSpawnedBombs(BombsSpawnerComponent spawnerComponent)
    {
        foreach (var spawnedObjectsCounter in _spawnedObjectsCounters)
        {
            ref var spawnedObjectsCounterComponent = ref _spawnedObjectsCounters.Get1(spawnedObjectsCounter);
            spawnedObjectsCounterComponent.BombsSpawned++;
            spawnerComponent.SpawnedObjectsData.SpawnedBombsCount = spawnedObjectsCounterComponent.BombsSpawned;
        }
    }
}