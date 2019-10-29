public class PlayerMovement : ShipMovement
{
    private InputController inputController;

    private new void Awake()
    {
        inputController = FindObjectOfType<InputController>();
        base.Awake();
    }

    private void Update()
    {
        SetMovement(inputController.MovementDirection, inputController.AttackDirection);
    }
}
