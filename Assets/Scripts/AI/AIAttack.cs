using UnityEngine;

public class AIAttack : ShipAttack
{
    public Transform Target { get; protected set; }
    public Vector2 AttackDirection { get; protected set; }

    protected new void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected void Update()
    {
        AttackDirection = (Target.position - transform.position).normalized;
        Attack(AttackDirection);
    }
}