using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class RainDropObject : MonoBehaviour
{
    [SerializeField] private ObjectColorChanger _objectColorChanger;

    public event Action RainDropTouchedPlatform;

    private bool _isObjectTouchedPlatform;

    public MeshRenderer Renderer {get; private set; }

    public MeshRenderer GetMeshRenderer()
    {
        return Renderer;
    }

    private void Awake()
    {
        _objectColorChanger.Initialize(this);
        Renderer = GetComponent<MeshRenderer>();
        _isObjectTouchedPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) && _isObjectTouchedPlatform == false)
        {
            RainDropTouchedPlatform?.Invoke();
            StartCoroutine(LifeTimeCountDown());
            _isObjectTouchedPlatform = true;
        }
    }

    private IEnumerator LifeTimeCountDown()
    {
        float minimumLifeTime = 2;
        float maximumLifeTime = 5;
        float lifeTimeLeft = UnityEngine.Random.Range(minimumLifeTime, maximumLifeTime);
        float delayValue = 1;

        while (lifeTimeLeft > 0)
        {
            lifeTimeLeft--;
            yield return new WaitForSeconds(delayValue);
        }

        Destroy(gameObject);
    }
}