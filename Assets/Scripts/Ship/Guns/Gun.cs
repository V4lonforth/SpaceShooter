using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;

    public float attacksPerSecond;
    public float startDelay;

    private float timeToAttack;
    private Ship parent;

    protected void Start()
    {
        timeToAttack = startDelay;
        parent = GetComponentInParent<Ship>();
    }

    public void Attack(Vector2 direction)
    {
        timeToAttack -= Time.deltaTime;
        if (timeToAttack <= 0f)
        {
            GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
            proj.GetComponent<Projectile>().Launch(parent.tag, direction);

            timeToAttack += 1f / attacksPerSecond;
        }
    }

    protected Vector2 CalculateTargetPosition(Vector2 shooterPosition, float projectileSpeed, Vector2 targetPosition, Vector2 targetVelocity)
    {
        Vector2 direction = targetPosition - shooterPosition;
        float startDistance = direction.magnitude;
        float targetSpeed = targetVelocity.magnitude;
        float cosinus = Vector2.Dot(targetVelocity, -direction) / (startDistance * targetSpeed);
        float sqrSpeedDiff = projectileSpeed * projectileSpeed - targetSpeed * targetSpeed;
        float projectedTargetSpeed = targetSpeed * cosinus;
        float time = startDistance * (Mathf.Sqrt(sqrSpeedDiff + projectedTargetSpeed * projectedTargetSpeed) - projectedTargetSpeed) / sqrSpeedDiff;
        return targetPosition + targetVelocity * time;
    }
}
