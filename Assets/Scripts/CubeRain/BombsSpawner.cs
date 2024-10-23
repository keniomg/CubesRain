using System.Collections;
using UnityEngine;

public class BombsSpawner : ObjectsSpawner<Bomb>
{
    [SerializeField] private ParticlesSpawner _particlesSpawner;

    private Vector3 _spawnPosition;

    public void OnRaindropDisabled(Raindrop raindrop)
    {
        _spawnPosition = raindrop.transform.position;
        SpawnBomb();
    }

    protected override void AccompanyGet(Bomb spawnableObject)
    {
        base.AccompanyGet(spawnableObject);
        StartCoroutine(LifetimeCountdown(spawnableObject));
        ChangeTransparency(spawnableObject);
    }

    protected override IEnumerator LifetimeCountdown(Bomb spawnableObject)
    {
        float minimumLifeTime = 2;
        float maximumLifeTime = 5;
        LifetimeLeft = Random.Range(minimumLifeTime, maximumLifeTime);
        float delayValue = 1;
        WaitForSeconds = new(delayValue);

        while (LifetimeLeft > 0)
        {
            LifetimeLeft--;

            yield return WaitForSeconds;
        }

        _particlesSpawner.InvokeParticleSystemEvent(spawnableObject.transform.position);
        Pool.Release(spawnableObject);
    }

    protected override Vector3 GetSpawnPosition()
    {
        return _spawnPosition;
    }

    private void SpawnBomb()
    {
        Pool.Get();
    }

    private void ChangeTransparency(Bomb spawnableObject)
    {
        if (spawnableObject.TryGetComponent(out TransparencyChanger transparencyChanger))
        {
            transparencyChanger.StartTransparencyCountdown(LifetimeLeft);
        }
    }
}