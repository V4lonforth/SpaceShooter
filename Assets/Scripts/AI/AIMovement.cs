using UnityEngine;

public class AIMovement : ShipMovement
{
    public float minDistance;
    public float maxDistance;

    private float rotation;

    protected AIAttack aiAttack;

    protected new void Start()
    {
        base.Start();
        aiAttack = GetComponent<AIAttack>();
    }
}