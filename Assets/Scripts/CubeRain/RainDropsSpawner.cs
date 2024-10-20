//using UnityEngine;
//using UnityEngine.Pool;
//using System.Collections;

//public class RainDropsSpawner : ObjectSpawner<Bomb>
//{
//    [SerializeField] private int _poolCapacity;
//    [SerializeField] private int _poolMaxSize;
//    [SerializeField] private BombSpawner _bombSpawner;

//    private ObjectPool<Bomb> _pool;
//    private float _widthSpawnArea;
//    private float _lengthSpawnArea;
//    private WaitForSeconds _waitForSeconds;

//    protected override void Awake()
//    {
//        int numbersOfSide = 2;
//        _widthSpawnArea = transform.localScale.x / numbersOfSide;
//        _lengthSpawnArea = transform.localScale.z / numbersOfSide;

//        base.Awake();
//    }

//    private void Start()
//    {
//        StartCoroutine(SpawnRainDrops());
//    }

//    protected override void AccompanyGet(Bomb rainDropObject)
//    {
//        base.AccompanyGet(rainDropObject);
//        Eventer.RegisterTouchedPlatformEvent(rainDropObject.name, OnRainDropTouchedPlatform);
//    }

//    protected override void AccompanyRelease(Bomb rainDropObject)
//    {
//        base.AccompanyRelease(rainDropObject);
//        Eventer.UnregisterTouchedPlatformEvent(rainDropObject.name, OnRainDropTouchedPlatform);
//    }

//    protected override Vector3 GetSpawnPosition()
//    {
//        float xSpawnPosition = Random.Range(transform.position.x - _widthSpawnArea, transform.position.x + _widthSpawnArea);
//        float ySpawnPosition = transform.position.y;
//        float zSpawnPosition = Random.Range(transform.position.z - _lengthSpawnArea, transform.position.z + _lengthSpawnArea);
//        Vector3 spawnPosition = new(xSpawnPosition, ySpawnPosition, zSpawnPosition);

//        return spawnPosition;
//    }

//    private void OnRainDropTouchedPlatform(Bomb rainDropObject)
//    {
//        StartCoroutine(LifeTimeCountDown(rainDropObject));
//    }

//    private IEnumerator SpawnRainDrops()
//    {
//        float spawnDelay = 1;
//        _waitForSeconds = new(spawnDelay);

//        while (true)
//        {
//            _pool.Get();

//            yield return _waitForSeconds;
//        }
//    }
//}