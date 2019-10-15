using UnityEngine;

public class PlayerAttack : ShipAttack
{
    private InputController inputController;

    private new void Start()
    {
        inputController = FindObjectOfType<InputController>();
        base.Start();
    }

    private void Update()
    {
        if (inputController.AttackDirection != Vector2.zero)
        {
            Attack(transform.rotation * Vector2.right);
        }
    }
}
