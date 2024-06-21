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

    private void Awake()
    {
        int numbersOfSide = 2;
        _widthSpawnArea = transform.localScale.x / numbersOfSide;
        _lengthSpawnArea = transform.localScale.z / numbersOfSide;
        _pool = new ObjectPool<RainDropObject>(
        createFunc: () => Instantiate(_rainDropObject),
        actionOnGet: (rainDropObject) => ActionOnGet(rainDropObject),
        actionOnRelease: (rainDropObject) => ActionOnRelease(rainDropObject),
        actionOnDestroy: (rainDropObject) => Destroy(rainDropObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        float spawnDelayValue = 1;
        InvokeRepeating(nameof(GetObject), 0.0f, spawnDelayValue);
    }

    private void ActionOnRelease(RainDropObject rainDropObject)
    {
        rainDropObject.gameObject.SetActive(false);
        rainDropObject.RainDropTouchedPlatform -= OnRainDropTouchedPlatform;
    }

    private void ActionOnGet(RainDropObject rainDropObject)
    {
        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
        float ySpawnPosition = transform.position.y;
        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
        Vector3 spawnPosition = new(xSpawnPosition, ySpawnPosition, zSpawnPosition);
        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);
        rainDropObject.transform.SetPositionAndRotation(spawnPosition, rotationPosition);
        rainDropObject.TryGetComponent(out Rigidbody rigidbody);
        rigidbody.velocity = Vector3.zero;
        rainDropObject.gameObject.SetActive(true);
        rainDropObject.RainDropTouchedPlatform += OnRainDropTouchedPlatform;
    }

    private void OnRainDropTouchedPlatform(RainDropObject rainDropObject)
    {
        StartCoroutine(LifeTimeCountDown(rainDropObject));
    }

    private void GetObject()
    {
        _pool.Get();
    }

    private IEnumerator LifeTimeCountDown(RainDropObject rainDropObject)
    {
        float minimumLifeTime = 2;
        float maximumLifeTime = 5;
        float lifeTimeLeft = Random.Range(minimumLifeTime, maximumLifeTime);
        float delayValue = 1;
        WaitForSeconds waitForSeconds = new(delayValue);

        while (lifeTimeLeft > 0)
        {
            lifeTimeLeft--;
            yield return waitForSeconds;
        }

        rainDropObject.gameObject.SetActive(false);
        _pool.Release(rainDropObject);
    }
}