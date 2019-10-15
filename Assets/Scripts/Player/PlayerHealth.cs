using System;

public class PlayerHealth : ShipHealth
{
    private HealthBar healthBar;

    protected new void Start()
    {
        base.Start();

        healthBar = Array.Find(FindObjectsOfType<HealthBar>(), element => "Health".Equals(element.barName));
        healthBar.SetHealth(HealthPoints / maxHealthPoints);
    }

    public override void TakeDamage(float value)
    {
        HealthPoints -= value;

        if (HealthPoints <= 0f)
        {
            healthBar.SetHealth(0f);
        }
        else
        {
            healthBar.SetHealth(HealthPoints / maxHealthPoints);
        }
    }
}