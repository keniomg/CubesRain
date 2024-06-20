using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

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

    private void ActionOnRelease(RainDropObject rainDropObject)
    {
        rainDropObject.gameObject.SetActive(false);

    }

    private void ActionOnGet(RainDropObject rainDropObject)
    {
        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
        float ySpawnPosition = transform.position.y;
        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
        Vector3 spawnPosition = new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition);
        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);
        rainDropObject.transform.position = spawnPosition;
        rainDropObject.transform.rotation = rotationPosition;

        rainDropObject.TryGetComponent(out Rigidbody rigidbody);
        rigidbody.velocity = Vector3.zero;
        rainDropObject.gameObject.SetActive(true);
    }

    private void GetObject()
    {
        _pool.Get();
    }

    private void Start()
    {
        float spawnDelayValue = 1;
        InvokeRepeating(nameof(GetObject), 0.0f, spawnDelayValue);
    }
}