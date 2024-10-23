using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(TransparencyChanger))]

public class Bomb : SpawnableObject
{
    [field: SerializeField] public ParticleSystem Explosion { get; private set; } 

    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private TransparencyChanger _transparencyChanger;

    private void Start()
    {
        _transparencyChanger = GetComponent<TransparencyChanger>();
    }

    private void OnDisable()
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }
}