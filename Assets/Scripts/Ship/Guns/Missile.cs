using UnityEngine;

public class Missile : Projectile
{
    public LayerMask shipsLayerMask;

    public float maxSpeed;
    public float acceleration;

    public float explosionDamage;
    public float explosionRadius;

    public bool friendlyDamage;

    private Transform target;

    private new void Update()
    {
        base.Update();
        if (target)
        {
            Move(target);
        }
        else
        {
            FindTarget();
        }
    }

    public override void Destroy()
    {
        Explode();
        base.Destroy();
    }

    private void Explode()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, explosionRadius, shipsLayerMask))
        {
            if (friendlyDamage || !collider.CompareTag(ParentTag))
            {
                collider.GetComponent<ShipHealth>().TakeDamage(explosionDamage);
            }
        }
    }

    private void Move(Transform target)
    {
        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity + acceleration * Time.deltaTime * ((Vector2)(target.position - transform.position)).normalized, maxSpeed);
        rigidbody.rotation = Mathf.Atan2(rigidbody.velocity.y, rigidbody.velocity.x) * Mathf.Rad2Deg;
    }

    private void FindTarget()
    {
        if ("Player".Equals(ParentTag))
        {
            float minSqrDistance = float.MaxValue;
            foreach (AIShip ship in ShipsController.Instance.Ships)
            {
                float sqrDistance = (ship.transform.position - transform.position).sqrMagnitude;
                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    target = ship.transform;
                }
            }
        }
        else
        {
            target = ShipsController.Instance.PlayerShip.transform;
        }
    }
}