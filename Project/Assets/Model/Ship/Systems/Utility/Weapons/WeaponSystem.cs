using UnityEngine;
using System.Collections;

public class WeaponSystem : UtilitySystem {
    double damage;
	// Use this for initialization
	void Start () {
	
	}

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
}
