
public class GuardDrone : Ship
{
    public GuardDroneMovement shipMovement { get; private set; }
    public GuardDroneAttack shipAttack { get; private set; }

    private void Start()
    {
        shipMovement = GetComponent<GuardDroneMovement>();
        shipAttack = GetComponent<GuardDroneAttack>();
    }
}