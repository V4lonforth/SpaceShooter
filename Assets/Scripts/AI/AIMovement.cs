using UnityEngine;

public class AIMovement : ShipMovement
{
    protected AIAttack aiAttack;

    protected new void Start()
    {
        base.Start();
        aiAttack = GetComponent<AIAttack>();
    }
}