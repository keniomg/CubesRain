//using System.Collections;
//using UnityEngine.Pool;
//using UnityEngine;

//public abstract class ObjectSpawner<Object> : MonoBehaviour where Object : SpawnableObject
//{
//    [SerializeField] protected EventInvoker<Object> Eventer;
    
//    [SerializeField] private Object _spawnableObject;
//    [SerializeField] private int _poolCapacity;
//    [SerializeField] private int _poolMaxSize;

//    private ObjectPool<Object> _pool;
//    private WaitForSeconds _waitForSeconds;

//    protected virtual void Awake()
//    {
//        _pool = new ObjectPool<Object>(
//            createFunc: () => Instantiate(_spawnableObject),
//            actionOnGet: (spawnableObject) => AccompanyGet(spawnableObject),
//            actionOnRelease: (spawnableObject) => AccompanyRelease(spawnableObject),
//            actionOnDestroy: (spawnableObject) => Destroy(spawnableObject),
//            collectionCheck: true,
//            defaultCapacity: _poolCapacity,
//            maxSize: _poolMaxSize);
//    }

//    protected abstract Vector3 GetSpawnPosition();

//    protected virtual void AccompanyGet(Object spawnableObject)
//    {
//        SetPositionAndRotation(spawnableObject);
//        SetDefaultVelocity(spawnableObject);
//        spawnableObject.gameObject.SetActive(true);
//    }

//    protected virtual void AccompanyRelease(Object spawnableObject)
//    {
//        spawnableObject.gameObject.SetActive(false);
//    }

//    protected virtual IEnumerator LifeTimeCountDown(Object spawnableObject)
//    {
//        float minimumLifeTime = 2;
//        float maximumLifeTime = 5;
//        float lifeTimeLeft = Random.Range(minimumLifeTime, maximumLifeTime);
//        float delayValue = 1;
//        _waitForSeconds = new(delayValue);

//        while (lifeTimeLeft > 0)
//        {
//            lifeTimeLeft--;

//            yield return _waitForSeconds;
//        }

//        spawnableObject.gameObject.SetActive(false);
//        _pool.Release(spawnableObject);
//    }

//    private void SetDefaultVelocity(Object spawnableObject)
//    {
//        if (spawnableObject.TryGetComponent(out Rigidbody rigidbody))
//        {
//            rigidbody.velocity = Vector3.zero;
//            rigidbody.angularVelocity = Vector3.zero;
//        }
//    }

//    private void SetPositionAndRotation(Object spawnableObject)
//    {
//        Vector3 spawnPosition = GetSpawnPosition();
//        Quaternion rotationPosition = GetRotationPosition();
//        spawnableObject.transform.SetPositionAndRotation(spawnPosition, rotationPosition);
//    }

//    private Quaternion GetRotationPosition()
//    {
//        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);

//        return rotationPosition;
//    }
//}