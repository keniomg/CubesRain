using System.Collections;
using UnityEngine.Pool;
using UnityEngine;
using System;

public abstract class ObjectsSpawner<Object> : MonoBehaviour where Object : SpawnableObject
{
    [SerializeField] protected int PoolCapacity;
    [SerializeField] protected int PoolMaximumSize;
    [SerializeField] protected Object SpawnableObject;

    protected ObjectPool<Object> Pool;
    protected WaitForSeconds WaitForSeconds;
    protected float LifetimeLeft;

    public int InstantiatedObjectsCount {get; protected set; }
    public int GettedObjectsCount {get; protected set; }

    public event Action<int> InstantiatedObjectsChanged;
    public event Action<int> GettedObjectsChanged;
    public event Action<int> ActiveObjectsChanged;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<Object>(
            createFunc: () => InstantiateObject(SpawnableObject),
            actionOnGet: (spawnableObject) => AccompanyGet(spawnableObject),
            actionOnRelease: (spawnableObject) => AccompanyRelease(spawnableObject),
            actionOnDestroy: (spawnableObject) => Destroy(spawnableObject),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaximumSize);
        
        InstantiatedObjectsCount = 0;
        GettedObjectsCount = 0;
    }

    protected Object InstantiateObject(Object spawnableObject)
    {
        Object spawnable = Instantiate(spawnableObject);
        InstantiatedObjectsCount++;
        InstantiatedObjectsChanged?.Invoke(InstantiatedObjectsCount);
        return spawnable;
    }

    protected abstract Vector3 GetSpawnPosition();

    protected virtual void AccompanyGet(Object spawnableObject)
    {
        SetPositionAndRotation(spawnableObject);
        SetDefaultVelocity(spawnableObject);
        spawnableObject.gameObject.SetActive(true);
        ActiveObjectsChanged?.Invoke(Pool.CountActive);
        GettedObjectsCount++;
        GettedObjectsChanged?.Invoke(GettedObjectsCount);
    }

    protected virtual void AccompanyRelease(Object spawnableObject)
    {
        spawnableObject.gameObject.SetActive(false);
        ActiveObjectsChanged?.Invoke(Pool.CountActive);
    }

    protected virtual IEnumerator LifetimeCountdown(Object spawnableObject)
    {
        float minimumLifeTime = 2;
        float maximumLifeTime = 5;
        LifetimeLeft = UnityEngine.Random.Range(minimumLifeTime, maximumLifeTime);
        float delayValue = 1;
        WaitForSeconds = new(delayValue);

        while (LifetimeLeft > 0)
        {
            LifetimeLeft--;

            yield return WaitForSeconds;
        }

        Pool.Release(spawnableObject);
    }

    protected void SetDefaultVelocity(Object spawnableObject)
    {
        if (spawnableObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void SetPositionAndRotation(Object spawnableObject)
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Quaternion rotationPosition = GetRotationPosition();
        spawnableObject.transform.SetPositionAndRotation(spawnPosition, rotationPosition);
    }

    protected Quaternion GetRotationPosition()
    {
        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);

        return rotationPosition;
    }
}