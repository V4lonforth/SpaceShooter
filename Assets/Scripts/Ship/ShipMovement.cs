using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;

    protected new Rigidbody2D rigidbody;

    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void SetMovement(Vector2 movementDirection, Vector2 attackDirection)
    {
        if (movementDirection != Vector2.zero)
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity + acceleration * Time.deltaTime * movementDirection, maxSpeed);
        else if (rigidbody.velocity != Vector2.zero)
        {
            float speed = rigidbody.velocity.magnitude;
            rigidbody.velocity /= speed;
            speed -= acceleration * Time.deltaTime;
            if (speed < 0)
                speed = 0;
            rigidbody.velocity *= speed;
        }

        Vector2 direction = attackDirection == Vector2.zero ? movementDirection : attackDirection;
        if (direction != Vector2.zero)
            rigidbody.SetRotation(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));
    }
}