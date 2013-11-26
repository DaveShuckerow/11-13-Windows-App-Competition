using UnityEngine;
using System.Collections;

public class WeaponSystem : UtilitySystem {
    double damage;

	// Use this for initialization
    public WeaponSystem()
    {

    }

    public void setDamage(double dam)
    {
        damage = dam;
    }

    public double getDamage()
    {
        return damage;
    }

    public bool fire(Ship target)
    {
        if (!getStatus())
            return false;
        fire(target, true);
        return true;
    }

    public void fire(Ship target, bool hit)
    {
        if (hit && getStatus() == true)
        {
            target.damage(getDamage());
        }
    }
}
