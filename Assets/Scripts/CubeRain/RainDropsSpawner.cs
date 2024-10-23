using UnityEngine;
using System.Collections;

public class RaindropsSpawner : ObjectsSpawner<Raindrop>
{
    [SerializeField] private BombsSpawner _bombSpawner;

    private float _widthSpawnArea;
    private float _lengthSpawnArea;

    protected override void Awake()
    {
        int numbersOfSide = 2;
        _widthSpawnArea = transform.localScale.x / numbersOfSide;
        _lengthSpawnArea = transform.localScale.z / numbersOfSide;

        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(SpawnRaindrops());
    }

    protected override void AccompanyGet(Raindrop raindropObject)
    {
        base.AccompanyGet(raindropObject);
        raindropObject.RaindropTouchedPlatform += OnRaindropTouchedPlatform;
    }

    protected override void AccompanyRelease(Raindrop raindropObject)
    {
        base.AccompanyRelease(raindropObject);
        _bombSpawner.OnRaindropDisabled(raindropObject);
        raindropObject.RaindropTouchedPlatform -= OnRaindropTouchedPlatform;
    }

    protected override Vector3 GetSpawnPosition()
    {
        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
        float ySpawnPosition = transform.position.y;
        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
        Vector3 spawnPosition = new(xSpawnPosition, ySpawnPosition, zSpawnPosition);

        return spawnPosition;
    }

    private void OnRaindropTouchedPlatform(Raindrop raindropObject)
    {
        if (!raindropObject.IsObjectAlreadyTouchedPlatform)
        {
            StartCoroutine(LifetimeCountdown(raindropObject));
        }
    }

    private IEnumerator SpawnRaindrops()
    {
        float spawnDelay = 1;
        WaitForSeconds = new(spawnDelay);

        while (true)
        {
            Pool.Get();

            yield return WaitForSeconds;
        }
    }
}