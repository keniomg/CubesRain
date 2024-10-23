using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class Raindrop : SpawnableObject
{
    [SerializeField] private ObjectColorChanger _objectColorChanger;

    private Color _defaultColor;

    public MeshRenderer Renderer { get; private set; }
    public bool IsObjectAlreadyTouchedPlatform { get; private set; }

    public event Action<Raindrop> RaindropTouchedPlatform;

    private void Awake()
    {
        _objectColorChanger.Initialize(this);
        Renderer = GetComponent<MeshRenderer>();
        _defaultColor = Renderer.material.color;
    }

    private void OnEnable()
    {
        Renderer.material.color = _defaultColor;
        IsObjectAlreadyTouchedPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) && IsObjectAlreadyTouchedPlatform == false)
        {
            RaindropTouchedPlatform?.Invoke(this);
            IsObjectAlreadyTouchedPlatform = true;
        }
    }
}