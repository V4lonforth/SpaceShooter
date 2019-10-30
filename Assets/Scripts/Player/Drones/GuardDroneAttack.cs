using UnityEngine;

public class GuardDroneAttack : ShipAttack
{
    public Vector2 AttackDirrection { get; protected set; }

    protected Transform target;
    protected Rigidbody2D targetRigidbody;

    protected void Start()
    {
        FindTarget();
    }

    protected new void Update()
    {
        if (target)
        {
            AttackDirection = (target.position - transform.position).normalized;
            Attack(AttackDirection);
        }
        else
        {
            AttackDirection = Vector2.zero;
            Wait();
            FindTarget();
        }
    }

    protected void FindTarget()
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