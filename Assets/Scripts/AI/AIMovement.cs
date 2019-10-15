using UnityEngine;

public class AIMovement : ShipMovement
{
    public float minDistance;
    public float maxDistance;

    private float rotation;
    private Vector2 nextPosition;

    private AIAttack aiAttack;

    private new void Start()
    {
        base.Start();
        aiAttack = GetComponent<AIAttack>();
        nextPosition = transform.position;
    }

    private void Update()
    {
        ChoosePosition();
        Vector2 direction = (nextPosition - (Vector2)transform.position).normalized;
        SetMovement(direction, aiAttack.AttackDirection);
    }

    private void ChoosePosition()
    {
        if (((Vector2)transform.position - nextPosition).sqrMagnitude < 1f)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            nextPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(minDistance, maxDistance);
        }
    }
}