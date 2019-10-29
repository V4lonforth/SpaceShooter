using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GunGroup
{
    public List<Gun> guns;
    public float reloadTime;

    public bool hasSpread;
    public float spread;

    private float timeToShoot;

    public void StartReloading()
    {
        timeToShoot = reloadTime;
    }

    public bool Attack(Vector2 direction)
    {
        timeToShoot -= Time.deltaTime;
        if (timeToShoot <= 0f)
        {
            if (hasSpread)
                direction = MathHelpers.AddDegreeSpread(direction, spread);
            foreach (Gun gun in guns)
                gun.Attack(direction);
            StartReloading();
            return true;
        }
        return false;
    }

    public void Wait()
    {
        timeToShoot -= Time.deltaTime;
    }
}