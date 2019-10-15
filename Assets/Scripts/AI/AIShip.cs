using UnityEngine;

public class AIShip : Ship
{
    public AIMovement shipMovement { get; private set; }
    public AIHealth shipHealth { get; private set; }
    public AIAttack shipAttack { get; private set; }

    void Start()
    {
        shipMovement = GetComponent<AIMovement>();
        shipHealth = GetComponent<AIHealth>();
        shipAttack = GetComponent<AIAttack>();

        ShipsController.Instance.AddShip(this);
    }
}
