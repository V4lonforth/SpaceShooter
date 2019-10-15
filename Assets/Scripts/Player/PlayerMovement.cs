public class PlayerMovement : ShipMovement
{
    private InputController inputController;

    private new void Start()
    {
        inputController = FindObjectOfType<InputController>();
        base.Start();
    }

    private void Update()
    {
        SetMovement(inputController.MovementDirection, inputController.AttackDirection);
    }
}
