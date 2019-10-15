using UnityEngine;

public class AIAttack : ShipAttack
{
    public Transform Target { get; protected set; }
    public Vector2 AttackDirection { get; protected set; }

    private new void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    private void Update()
    {
        AttackDirection = (Target.position - transform.position).normalized;
        Attack(AttackDirection);
    }
}