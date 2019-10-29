using UnityEngine;

public class Missile : Projectile
{
    private enum MissileState
    {
        Launching,
        StandBy,
        Seeking
    }
    private MissileState state;

    public LayerMask shipsLayerMask;

    public float launchingAnimationTime;

    public float maxSpeed;
    public float acceleration;

    public float startRotationSpeed;
    public float rotationAcceleration;
    public float maxRotationSpeed;

    public float explosionDamage;
    public float explosionRadius;

    public bool friendlyDamage;

    private float launchingDeceleration;
    private float rotationSpeed;

    private Transform target;

    public override void Launch(Gun gun, string parentTag, Vector2 movingDirection, float rotation)
    {
        rotationSpeed = startRotationSpeed;
        state = MissileState.Launching;

        Vector2 velocity = gun.Parent.GetComponent<Rigidbody2D>().velocity + MathHelpers.DegreeToVector2(gun.transform.rotation.eulerAngles.z) * speed;
        speed = velocity.magnitude;
        velocity /= speed;

        launchingDeceleration = speed / launchingAnimationTime;

        base.Launch(gun, parentTag, velocity, rotation);
    }

    private new void Update()
    {
        switch (state)
        {
            case MissileState.Seeking:
                if (target)
                    Move(target);
                else
                    state = MissileState.StandBy;
                break;

            case MissileState.Launching:
                float newSpeed = speed - Time.deltaTime * launchingDeceleration;
                if (newSpeed <= 0f)
                {
                    speed = 0.01f;
                    state = MissileState.StandBy;
                    rigidbody.velocity = MathHelpers.DegreeToVector2(transform.rotation.eulerAngles.z) * speed;
                }
                else
                {
                    rigidbody.velocity = rigidbody.velocity / (speed / newSpeed);
                    speed = newSpeed;
                }
                break;

            case MissileState.StandBy:
                newSpeed = speed + acceleration * Time.deltaTime;
                if (newSpeed >= maxSpeed)
                    newSpeed = maxSpeed;
                rigidbody.velocity = rigidbody.velocity / (speed / newSpeed);
                speed = newSpeed;

                FindTarget();
                if (target)
                    state = MissileState.Seeking;
                break;
        }

        base.Update();
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
        rotationSpeed += rotationAcceleration * Time.deltaTime;
        if (rotationSpeed > maxRotationSpeed)
            rotationSpeed = maxRotationSpeed;
        Vector2 direction = target.position - transform.position;
        float requiredAngle = MathHelpers.Vector2ToDegree(direction);
        float currentAngle = MathHelpers.LerpAngle(rigidbody.rotation, requiredAngle, rotationSpeed * Time.deltaTime * Mathf.Sqrt(speed / maxSpeed));
        Vector2 currentDirection = MathHelpers.DegreeToVector2(currentAngle);

        if (Mathf.Approximately(requiredAngle, currentAngle))
            rotationSpeed = startRotationSpeed;

        speed += Time.deltaTime * acceleration;
        if (speed > maxSpeed)
            speed = maxSpeed;

        rigidbody.velocity = currentDirection * speed;
        rigidbody.rotation = currentAngle;
    }

    private void Move1(Transform target)
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