using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class RainDropObject : MonoBehaviour
{
    [SerializeField] private ObjectColorChanger _objectColorChanger;

    private Color _defaultColor;

    public event Action<RainDropObject> RainDropTouchedPlatform;

    public MeshRenderer Renderer {get; private set; }
    public bool IsObjectTouchedPlatform {get; private set; }

    private void Awake()
    {
        _objectColorChanger.Initialize(this);
        Renderer = GetComponent<MeshRenderer>();
        _defaultColor = Renderer.material.color;
    }

    private void OnEnable()
    {
        Renderer.material.color = _defaultColor;
        IsObjectTouchedPlatform = false;
    }

    private void OnDisable()
    {
        IsObjectTouchedPlatform = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform) && IsObjectTouchedPlatform == false)
        {
            RainDropTouchedPlatform?.Invoke(this);
            IsObjectTouchedPlatform = true;
        }
    }
}