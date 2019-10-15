using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipAttack : MonoBehaviour
{
    private List<Gun> guns;

    public bool hasSpread;
    public float spread;

    protected void Start()
    {
        guns = new List<Gun>(Array.FindAll(gameObject.GetComponentsInChildren<Gun>(), element => !(element is Turret)));
    }

    protected void Attack(Vector2 direction)
    {
        if (hasSpread)
            direction = MathHelpers.AddSpread(direction, spread);
        foreach (Gun gun in guns)
            gun.Attack(direction);
    }
}
