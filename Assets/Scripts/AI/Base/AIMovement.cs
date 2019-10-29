public class AIMovement : ShipMovement
{
    protected AIAttack aiAttack;

    protected new void Awake()
    {
        base.Awake();
        aiAttack = GetComponent<AIAttack>();
    }
}