public class AIHealth : ShipHealth
{
    public override void TakeDamage(float value)
    {
        HealthPoints -= value;
        if (HealthPoints <= 0f)
        {
            ShipsController.Instance.Remove(GetComponent<AIShip>());
            Destroy(gameObject);
        }
    }
}
