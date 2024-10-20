//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class EventInvoker<Object> : MonoBehaviour where Object : SpawnableObject
//{
//    private Dictionary<string, Action<Transform>> _disabledEvents = new Dictionary<string, Action<Transform>>();
//    private Dictionary<string, Action<Object>> _touchedPlatformEvents = new Dictionary<string, Action<Object>>();

//    public void RegisterDisabledEvent(string name, Action<Transform> disabledEvent)
//    {
//        if (!_disabledEvents.ContainsKey(name))
//        {
//            _disabledEvents[name] = disabledEvent;
//        }
//        else
//        {
//            _disabledEvents[name] += disabledEvent;
//        }
//    }

//    public void RegisterTouchedPlatformEvent(string name, Action<Object> touchedPlatformEvent)
//    {
//        if (!_disabledEvents.ContainsKey(name))
//        {
//            _touchedPlatformEvents[name] = touchedPlatformEvent;
//        }
//        else
//        {
//            _touchedPlatformEvents[name] += touchedPlatformEvent;
//        }
//    }

//    public void UnregisterDisabledEvent(string name, Action<Transform> disabledEvent)
//    {
//        if (_disabledEvents.ContainsKey(name))
//        {
//            _disabledEvents[name] -= disabledEvent;
//        }
//    }

//    public void UnregisterTouchedPlatformEvent(string name, Action<Object> touchedPlatformEvent)
//    {
//        if (_disabledEvents.ContainsKey(name))
//        {
//            _touchedPlatformEvents[name] -= touchedPlatformEvent;
//        }
//    }

//    public void InvokeDisabledEvent(string name, Transform transform)
//    {
//        if (_disabledEvents.ContainsKey(name))
//        {
//            _disabledEvents[name]?.Invoke(transform);
//        }
//    }

//    public void InvokeTouchedPlatformEvent(string name, Object gameObject)
//    {
//        if (_disabledEvents.ContainsKey(name))
//        {
//            _touchedPlatformEvents[name]?.Invoke(gameObject);
//        }
//    }
//}