using UnityEngine;

public class PlayerAttack : ShipAttack
{
    private InputController inputController;

    protected new void Awake()
    {
        base.Awake();
        inputController = FindObjectOfType<InputController>();
    }

    protected new void Update()
    {
        AttackDirection = inputController.AttackDirection;
        if (AttackDirection != Vector2.zero)
            Attack(AttackDirection);
        else
            Wait();
    }
}