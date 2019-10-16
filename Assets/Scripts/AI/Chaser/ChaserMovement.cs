using UnityEngine;

public class ChaserMovement : AIMovement
{
    public float distanceToAttack;
    public float distanceToFlee;

    public float rotationSpeed;
    public float angleToAttack;

    private enum AttackStage
    {
        Attack,
        Flee
    }

    public bool CanAttack { get; private set; }

    private AttackStage attackStage;

    private ChaserAttack attack;

    protected new void Start()
    {
        attack = GetComponent<ChaserAttack>();
    }

    protected void Update()
    {
        if (attack.Target)
        {
            Move();
        }
    }

    private void Move()
    {
        switch (attackStage)
        {
            case AttackStage.Attack:
                Vector2 direction = attack.Target.position - transform.position;
                float distance = direction.magnitude;
                float requiredAngle = MathHelpers.Vector2ToDegree(direction);
                float currentAngle = MathHelpers.LerpAngle(rigidbody.rotation, requiredAngle, rotationSpeed * Time.deltaTime);
                CanAttack = Mathf.Abs(requiredAngle - currentAngle) <= angleToAttack;

                Vector2 currentDirection = MathHelpers.DegreeToVector2(currentAngle);
                SetMovement(currentDirection, currentDirection);
                if (distance <= distanceToFlee)
                    attackStage = AttackStage.Flee;
                break;
            case AttackStage.Flee:
                direction = attack.Target.position - transform.position;
                distance = direction.magnitude;
                requiredAngle = MathHelpers.Vector2ToDegree(direction);
                currentAngle = MathHelpers.LerpAngle(rigidbody.rotation, requiredAngle, rotationSpeed * Time.deltaTime);
                CanAttack = false;

                currentDirection = MathHelpers.DegreeToVector2(currentAngle);
                SetMovement(currentDirection, currentDirection);
                if (distance < distanceToAttack)
                    attackStage = AttackStage.Attack;
                break;
        }
    }
}