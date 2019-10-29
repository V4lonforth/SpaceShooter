using UnityEngine;

public class AIAttack : ShipAttack
{
    public Transform Target { get; protected set; }

    protected void Start()
    {
        FindTarget();
    }

    protected new void Update()
    {
        if (Target)
        {
            AttackDirection = (Target.position - transform.position).normalized;
            Attack(AttackDirection);
        }
        else
            FindTarget();
    }

    protected void FindTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}