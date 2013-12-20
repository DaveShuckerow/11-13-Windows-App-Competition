using UnityEngine;
using System.Collections;

public class TorpedoSystem : WeaponSystem {
    
    public override double hitProbability(Ship target)
    {
        double chance = 0.0;
        int dist = getShip().getPosition().getHexDistance(target.getPosition());
        if (dist <= 3)
        {
            chance = 0.7;
        }
        return chance;
    }
}
