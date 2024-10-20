using ECS.Data;
using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private BombData _bombData;
    
    public float ExplosionRadius {  get; private set; }
    public float ExplosionForce { get; private set; }
    public ParticleSystem ParticleSystem {get; private set; }

    private void Awake()
    {
        if (_bombData != null)
        {
            ExplosionRadius = _bombData.ExplosionRadius;
            ExplosionForce = _bombData.ExplosionForce;
            ParticleSystem = _bombData.ParticleSystem;
        }
    }
}
