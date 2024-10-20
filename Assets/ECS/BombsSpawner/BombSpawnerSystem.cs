using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

public class BombSpawnerSystem : IEcsRunSystem
{
    private EcsFilter<BombSpawnerComponent> _bombSpawners;
    private EcsFilter<RaindropDestroyedEvent> _destroyedRaindrops;
    private EcsFilter<SpawnedObjectsCounterComponent> _spawnedObjectsCounters;

    private EcsWorld _ecsWorld;

    public void Run()
    {
        foreach (var spawner in _bombSpawners)
        {
            ref var spawnerComponent = ref _bombSpawners.Get1(spawner);

            foreach (var destroyedRaindrop in _destroyedRaindrops)
            {
                ref var destroyedRaindropEvent = ref _destroyedRaindrops.Get1(destroyedRaindrop);

                var bombObject = Object.Instantiate(spawnerComponent.prefab, destroyedRaindropEvent.position, Quaternion.identity);
                var bombEntity = _ecsWorld.NewEntity();
                bombEntity.Get<BombComponent>();
                ref var lifetimeCountdownComponent = ref bombEntity.Get<LifetimeCountdownComponent>();
                ref var hasGameObjectComponent = ref bombEntity.Get<HasGameObjectComponent>();

                if (bombObject.TryGetComponent(out BombView bombView))
                {
                    ref var explodableComponent = ref bombEntity.Get<ExplodableComponent>();

                    explodableComponent.explosionRadius = bombView.ExplosionRadius;
                    explodableComponent.explosionForce = bombView.ExplosionForce;
                    explodableComponent.particleSystem = bombView.ParticleSystem;
                }

                if (bombObject.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    ref var transparencyCountdownComponent = ref bombEntity.Get<TransparencyCountdownComponent>();
                    transparencyCountdownComponent.meshRenderer = meshRenderer;
                    bombEntity.Get<MeshRendererChangableMarker>();
                }

                lifetimeCountdownComponent.minimumLifetime = spawnerComponent.minimumLifetime;
                lifetimeCountdownComponent.maximumLifetime = spawnerComponent.maximumLifetime;
                hasGameObjectComponent.gameObject = bombObject;
                bombEntity.Get<ObjectSpawnedEvent>();
                _destroyedRaindrops.GetEntity(destroyedRaindrop).Del<RaindropDestroyedEvent>();

                foreach (var spawnedObjectsCounter in _spawnedObjectsCounters)
                {
                    ref var spawnedObjectsCounterComponent = ref _spawnedObjectsCounters.Get1(spawnedObjectsCounter);
                    spawnedObjectsCounterComponent.bombsSpawned++;
                    spawnerComponent.spawnedObjectsData.spawnedBombsCount = spawnedObjectsCounterComponent.bombsSpawned;
                }
            }
        }
    }   
}