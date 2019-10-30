using UnityEngine;

public class GuardDroneMovement : ShipMovement
{
    public float radius;
    public float speed;

    protected Transform parentShip;
    protected GuardDroneAttack guardDroneAttack;

    private float time;

    protected void Start()
    {
        guardDroneAttack = GetComponent<GuardDroneAttack>();
        parentShip = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody.MovePosition(CalculatePosition());
    }

    protected void Update()
    {
        time += Time.deltaTime;
        SetMovement(CalculatePosition(), guardDroneAttack.AttackDirection);
    }

    protected new void SetMovement(Vector2 destinationPosition, Vector2 attackDirection)
    {
        rigidbody.MovePosition(Vector2.Lerp(transform.position, destinationPosition, 1f));

        Vector2 direction = attackDirection == Vector2.zero ? (destinationPosition - (Vector2)transform.position).normalized : attackDirection;
        if (direction != Vector2.zero)
            rigidbody.SetRotation(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));
    }

    protected Vector2 CalculatePosition()
    {
        float angle = speed * time * Mathf.Deg2Rad;
        Vector2 localPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return localPosition + (Vector2)parentShip.position;
    }
}