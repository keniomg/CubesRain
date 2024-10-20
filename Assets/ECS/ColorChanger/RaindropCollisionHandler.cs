using Leopotam.Ecs;
using UnityEngine;
using ECS.Components;

public class RaindropCollisionHandler : MonoBehaviour
{
    public EcsEntity RaindropEntity;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform))
        {
            RaindropEntity.Get<TouchedPlatformEvent>();
        }
    }
}