using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    public List<GunGroup> guns;
    private int currentGroup;

    public bool gunsDischargeSynchronized;

    public bool hasSpread;
    public float spread;

    public Vector2 AttackDirection { get; protected set; }

    protected void Awake()
    {
        foreach (GunGroup gunGroup in guns)
            gunGroup.StartReloading();
    }

    protected void Update()
    {
        AttackDirection = MathHelpers.DegreeToVector2(transform.rotation.eulerAngles.z);
        Attack(AttackDirection);
    }

    protected void Attack(Vector2 direction)
    {
        if (hasSpread)
            direction = MathHelpers.AddDegreeSpread(direction, spread);
        if (gunsDischargeSynchronized)
        {
            if (guns[currentGroup].Attack(direction))
                currentGroup = (currentGroup + 1) % guns.Count;
        }
        else
        {
            foreach (GunGroup gunGroup in guns)
                gunGroup.Attack(direction);
        }
    }

    protected void Wait()
    {
        if (gunsDischargeSynchronized)
        {
            if (currentGroup != 0)
                Attack(AttackDirection);
        }
        else
        {
            foreach (GunGroup gunGroup in guns)
                gunGroup.Wait();
        }
    }
}
