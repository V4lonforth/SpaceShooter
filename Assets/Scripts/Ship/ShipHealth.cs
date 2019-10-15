using System;
using UnityEngine;

public abstract class ShipHealth : MonoBehaviour
{
    public Func<float, float> ReduceDamage;

    public float maxHealthPoints;

    public float HealthPoints { get; protected set; }

    protected void Start()
    {
        HealthPoints = maxHealthPoints;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if (!gameObject.CompareTag(projectile.ParentTag))
        {
            projectile.Destroy();
            TakeDamage(ReduceDamage == null ? projectile.damage : ReduceDamage(projectile.damage));
        }
    }

    public abstract void TakeDamage(float value);
}
