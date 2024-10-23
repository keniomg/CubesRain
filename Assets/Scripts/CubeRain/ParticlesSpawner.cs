using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlesSpawner : MonoBehaviour
{
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaximumSize;
    [SerializeField] private ParticleSystem _particleSystem;

    private ObjectPool<ParticleSystem> _pool;
    private Vector3 _spawnPosition;

    public event Action<Vector3> ParticleSystemRequired;

    private void Awake()
    {
        _pool = new ObjectPool<ParticleSystem>(
                createFunc: () => Instantiate(_particleSystem),
                actionOnGet: (particleSystem) => AccompanyGet(particleSystem),
                actionOnRelease: (particleSystem) => AccompanyRelease(particleSystem),
                actionOnDestroy: (particleSystem) => Destroy(particleSystem),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaximumSize);
    }

    private void OnEnable()
    {
        ParticleSystemRequired += OnParticleSystemRequired;
    }

    private void OnDisable()
    {
        ParticleSystemRequired -= OnParticleSystemRequired;
    }

    public void InvokeParticleSystemEvent(Vector3 spawnPosition)
    {
        ParticleSystemRequired?.Invoke(spawnPosition);
    }

    private void AccompanyGet(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
        StartCoroutine(PlayParticle(particleSystem));
    }

    private void AccompanyRelease(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(false);
    }

    private void OnParticleSystemRequired(Vector3 spawnPosition)
    {
        SpawnParticleSystem(spawnPosition);
    }

    private void SpawnParticleSystem(Vector3 spawnPosition)
    {
        _spawnPosition = spawnPosition;
        _pool.Get();
    }

    private IEnumerator PlayParticle(ParticleSystem particleSystem)
    {
        particleSystem.transform.position = _spawnPosition;
        particleSystem.transform.rotation = Quaternion.identity;
        particleSystem.Play();

        while (particleSystem.IsAlive())
        {
            yield return Time.deltaTime;
        }

        _pool.Release(particleSystem);
    }
}