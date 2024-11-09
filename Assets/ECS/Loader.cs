using ECS.Data;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private BombData _bombData;
    [SerializeField] private RaindropData _raindropData;
    [SerializeField] private RaindropsSpawnerData _spawnerData;
    [SerializeField] private ColorChangerData _colorChangerData;
    [SerializeField] private SpawnedObjectsData _spawnedObjectsData;
    [SerializeField] private Transform _spawnerTransform;
    [SerializeField] private BombSpawnerData _bombSpawnerData;

    private EcsWorld _world;
    private EcsSystems _fixedUpdateSystems;
    private EcsSystems _updateSystems;

    private void Start()
    {
        _world = new EcsWorld();
        _fixedUpdateSystems = new EcsSystems(_world);
        _updateSystems = new EcsSystems(_world);

        _fixedUpdateSystems.Add(new RaindropsSpawnerInitSystem(_spawnerTransform, _spawnerData, _raindropData, _spawnedObjectsData));
        _fixedUpdateSystems.Add(new ColorChangerInitSystem(_colorChangerData));
        _fixedUpdateSystems.Add(new BombSpawnerInitSystem(_bombData, _bombSpawnerData, _spawnedObjectsData));
        _fixedUpdateSystems.Add(new ParticlesCleanerInitSystem());
        _fixedUpdateSystems.Add(new SpawnedObjectsCounterInitSystem());

        _fixedUpdateSystems.Add(new RaindropsSpawnerSystem());
        _fixedUpdateSystems.Add(new BombsSpawnerSystem());
        _fixedUpdateSystems.Add(new LifetimeSetterSystem());
        _fixedUpdateSystems.Add(new CountdownSystem());
        _fixedUpdateSystems.Add(new TransparencyCountdownSystem());
        _fixedUpdateSystems.Add(new ExploderSystem());
        _fixedUpdateSystems.Add(new DestroyingSystem());
        _fixedUpdateSystems.Add(new ParticlesCleanerSystem());

        _updateSystems.Add(new ColorChangerSystem());

        _fixedUpdateSystems.Init();
        _updateSystems.Init();
    }

    private void Update()
    {
        _updateSystems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystems?.Run();
    }

    private void OnDestroy()
    {
        _fixedUpdateSystems?.Destroy();
        _updateSystems?.Destroy();
        _world?.Destroy();
    }
}