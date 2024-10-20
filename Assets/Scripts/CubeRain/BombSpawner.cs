//using System.Collections;
//using UnityEngine.Pool;
//using System.Collections.Generic;
//using UnityEngine;

//public class BombSpawner : ObjectSpawner<Bomb>
//{
//    [SerializeField] private Bomb _rainDropObject;
//    [SerializeField] private int _poolCapacity;
//    [SerializeField] private int _poolMaxSize;
//    [SerializeField] private RainDropEventInvoker _rainDropEventer;

//    private ObjectPool<Bomb> _pool;
//    private WaitForSeconds _waitForSeconds;

//    protected override void AccompanyGet(Bomb bomb)
//    {
//        base.AccompanyGet(bomb);
//        StartCoroutine(LifeTimeCountDown(bomb));
//    }

//    protected override IEnumerator LifeTimeCountDown(Bomb bomb)
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

//        bomb.gameObject.SetActive(false);
//        _pool.Release(bomb);
//    }

//    protected override Vector3 GetSpawnPosition()
//    {
        
//    }

//    private void SetDefaultVelocity(Bomb bomb)
//    {
//        bomb.TryGetComponent(out Rigidbody rigidbody);
//        rigidbody.velocity = Vector3.zero;
//        rigidbody.angularVelocity = Vector3.zero;
//    }

//    private Quaternion GetRotationPosition()
//    {
//        Quaternion rotationPosition = Quaternion.Euler(0, 0, 0);
//        return rotationPosition;
//    }

//    private void OnRainDropDisabled()
//    {
//        SpawnBombAtPosition();
//    }

//    private void SpawnBombAtPosition()
//    {
//        _pool.Get();
//    }

//    private void ChangeTransparency(Bomb bomb)
//    {

//    }
//}