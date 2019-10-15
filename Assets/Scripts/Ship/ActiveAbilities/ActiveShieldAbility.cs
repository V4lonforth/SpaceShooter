using System;
using UnityEngine;

public class ActiveShieldAbility : ActiveAbility
{
    public GameObject shield;

    public float maxShieldPoints;
    public float shieldPointsReductionSpeed;

    public float ShieldPoints { get; private set; }
    public bool Active { get; private set; }

    private HealthBar shieldBar;
    private ShipHealth shipHealth;

    protected new void Start()
    {
        base.Start();
        shipHealth = GetComponent<ShipHealth>();
        shipHealth.ReduceDamage = TakeDamage;

        if (CompareTag("Player"))
            shieldBar = Array.Find(FindObjectsOfType<HealthBar>(), element => "Shield".Equals(element.barName));
    }

    protected new void Update()
    {
        base.Update();
        if (Active)
        {
            ShieldPoints -= shieldPointsReductionSpeed * Time.deltaTime;
            if (ShieldPoints <= 0f)
            {
                ShieldPoints = 0f;
                Active = false;
                shield.SetActive(false);
                SetShieldBarSize(0f);
            }
            else
                SetShieldBarSize(ShieldPoints / maxShieldPoints);
        }
    }

    private void SetShieldBarSize(float shieldPercent)
    {
        if (CompareTag("Player"))
            shieldBar.SetHealth(shieldPercent);
    }

    private float TakeDamage(float damage)
    {
        if (!Active)
            return damage;

        if (damage >= ShieldPoints)
        {
            damage -= ShieldPoints;
            ShieldPoints = 0f;
            return damage;
        }
        else
        {
            ShieldPoints -= damage;
            return 0f;
        }
    }

    protected override void Activate()
    {
        ShieldPoints = maxShieldPoints;
        Active = true;
        shield.SetActive(true);
    }
}