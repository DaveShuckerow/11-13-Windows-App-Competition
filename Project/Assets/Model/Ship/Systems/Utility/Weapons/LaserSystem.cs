using UnityEngine;
using System.Collections;

// A beam weapon with 
public class LaserSystem : WeaponSystem {

    public LaserSystem()
    {
        setDamage(1);
    }

    public override double hitProbability(Ship target)
    {
        double chance = 0.0;
        int dist = getShip().getPosition().getHexDistance(target.getPosition());
        if (dist <= 3)
        {
            // Return 0.9 at 1 dist, 0.7 at 2, and 0.5 at 3.
            chance = 0.5 + 0.2 * (3-dist);
        }
        return chance;
    }
}
