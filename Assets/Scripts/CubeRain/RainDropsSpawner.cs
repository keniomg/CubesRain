using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class RainDropsSpawner : MonoBehaviour
{
    [SerializeField] private RainDropObject _rainDropObject;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<RainDropObject> _pool;
    private float _widthSpawnArea;
    private float _lengthSpawnArea;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        int numbersOfSide = 2;
        _widthSpawnArea = transform.localScale.x / numbersOfSide;
        _lengthSpawnArea = transform.localScale.z / numbersOfSide;

        _pool = new ObjectPool<RainDropObject>(
            createFunc: () => Instantiate(_rainDropObject),
            actionOnGet: (rainDropObject) => ExecuteActionOnGet(rainDropObject),
            actionOnRelease: (rainDropObject) => ExecuteActionOnRelease(rainDropObject),
            actionOnDestroy: (rainDropObject) => Destroy(rainDropObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(SpawnRainDrops());
    }

    private void ExecuteActionOnRelease(RainDropObject rainDropObject)
    {
        rainDropObject.gameObject.SetActive(false);
        rainDropObject.RainDropTouchedPlatform -= OnRainDropTouchedPlatform;
    }

    private void ExecuteActionOnGet(RainDropObject rainDropObject)
    {
        SetPositionAndRotation(rainDropObject);
        SetDefaultVelocity(rainDropObject);
        rainDropObject.gameObject.SetActive(true);
        rainDropObject.RainDropTouchedPlatform += OnRainDropTouchedPlatform;
    }

    private void SetDefaultVelocity(RainDropObject rainDropObject)
    {
        rainDropObject.TryGetComponent(out Rigidbody rigidbody);
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void SetPositionAndRotation(RainDropObject rainDropObject)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Quaternion rotationPosition = GetRotationPosition();
        rainDropObject.transform.SetPositionAndRotation(spawnPosition, rotationPosition);
    }

    private Vector3 GetSpawnPosition()
    {
        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
        float ySpawnPosition = transform.position.y;
        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
        Vector3 spawnPosition = new(xSpawnPosition, ySpawnPosition, zSpawnPosition);
        return spawnPosition;
    }

    private Quaternion GetRotationPosition()
    {
        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);
        return rotationPosition;
    }

    private void OnRainDropTouchedPlatform(RainDropObject rainDropObject)
    {
        StartCoroutine(LifeTimeCountDown(rainDropObject));
    }

    private IEnumerator SpawnRainDrops()
    {
        float spawnDelay = 1;
        _waitForSeconds = new(spawnDelay);

        while (true)
        {
            _pool.Get();
            yield return _waitForSeconds;
        }
    }

    private IEnumerator LifeTimeCountDown(RainDropObject rainDropObject)
    {
        float minimumLifeTime = 2;
        float maximumLifeTime = 5;
        float lifeTimeLeft = Random.Range(minimumLifeTime, maximumLifeTime);
        float delayValue = 1;
        _waitForSeconds = new(delayValue);
        
        while (lifeTimeLeft > 0)
        {
            lifeTimeLeft--;
            yield return _waitForSeconds;
        }

        rainDropObject.gameObject.SetActive(false);
        _pool.Release(rainDropObject);
    }
}