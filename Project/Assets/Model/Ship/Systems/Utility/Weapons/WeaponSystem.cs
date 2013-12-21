using UnityEngine;
using System;
using System.Collections;

public class WeaponSystem : UtilitySystem {
    double damage;
    public override int maxLevel { get { return 4; } }

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
        bool doHit = computeHit(target);
        fire(target, doHit);
        return doHit;
    }

    public void fire(Ship target, bool hit)
    {
        if (hit && getStatus() == true)
        {
            for (int i = 0; i < level; i++)
                target.damage(getDamage());
        }
    }

    public virtual double hitProbability(Ship target)
    {
        return 0.0;
    }

    public bool computeHit(Ship target)
    {
        double chance = hitProbability(target);
        System.Random coin = new System.Random(UnityEngine.Random.Range(1000000,1000000000));
        bool toss;
        toss = coin.NextDouble() <= chance;
        return toss;
    }

}
