using UnityEngine;

public class Turret : Gun
{
    private Transform target;
    private Rigidbody2D targetRigidbody;

    public bool hasSpread;
    public float spread;

    private void Update()
    {
        if (target)
            Attack();
        else
            FindTarget();
    }

    public void Attack()
    {
        Vector2 direction = CalculateTargetPosition(transform.position, projectile.GetComponent<Projectile>().speed, target.position, targetRigidbody.velocity) - (Vector2)transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Attack(hasSpread ? MathHelpers.AddDegreeSpread(direction, spread) : direction);
    }

    private void FindTarget()
    {
        float minSqrDistance = float.MaxValue;
        foreach (AIShip ship in ShipsController.Instance.Ships)
        {
            float sqrDistance = (ship.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                target = ship.transform;
                targetRigidbody = target.GetComponent<Rigidbody2D>();
            }
        }
    }
}