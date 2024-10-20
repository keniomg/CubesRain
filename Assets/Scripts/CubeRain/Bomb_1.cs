//using System;
//using UnityEngine;

//[RequireComponent(typeof(Collider), typeof(Rigidbody))]

//public class Bomb : SpawnableObject
//{
//    [SerializeField] private ObjectColorChanger _objectColorChanger;

//    private RainDropEventInvoker _eventer;
//    private Color _defaultColor;

//    public event Action<Bomb> RainDropTouchedPlatform;
//    public event Action<Transform> RainDropDisabled;

//    public MeshRenderer Renderer {get; private set; }
//    public bool IsObjectTouchedPlatform {get; private set; }

//    private void Awake()
//    {
//        _objectColorChanger.Initialize(this);
//        Renderer = GetComponent<MeshRenderer>();
//        _defaultColor = Renderer.material.color;
//    }

//    private void OnEnable()
//    {
//        Renderer.material.color = _defaultColor;
//        _eventer.RegisterDisabledEvent(name, RainDropDisabled);
//        _eventer.RegisterTouchedPlatformEvent(name, RainDropTouchedPlatform);
//        IsObjectTouchedPlatform = false;
//    }

//    private void OnDisable()
//    {
//        IsObjectTouchedPlatform = true;
//        _eventer.InvokeDisabledEvent(name, transform);
//        _eventer.UnregisterDisabledEvent(name, RainDropDisabled);
//        _eventer.UnregisterTouchedPlatformEvent(name, RainDropTouchedPlatform);
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.collider.TryGetComponent(out Platform platform) && IsObjectTouchedPlatform == false)
//        {
//            _eventer.InvokeTouchedPlatformEvent(name, this);
//            IsObjectTouchedPlatform = true;
//        }
//    }

//    public void InitializeEventer(RainDropEventInvoker eventer)
//    {
//        _eventer = eventer;
//    }
//}