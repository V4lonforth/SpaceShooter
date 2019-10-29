public class PlayerShip : Ship
{
    public PlayerMovement shipMovement { get; private set; }
    public PlayerHealth shipHealth { get; private set; }
    public PlayerAttack shipAttack { get; private set; }

    private void Start()
    {
        shipMovement = GetComponent<PlayerMovement>();
        shipHealth = GetComponent<PlayerHealth>();
        shipAttack = GetComponent<PlayerAttack>();

        ShipsController.Instance.PlayerShip = this;
    }
}
