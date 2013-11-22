using UnityEngine;
using System.Collections;

public class ShieldSystem : UtilitySystem
{
    double maxShieldHP;
    double shieldHP;
    double chargeRate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    public ShieldSystem()
    {


    }

   public void setMaxShieldHP(double sHP) // Sets the maximum shield strength
    {
        if (sHP > 0)
        {
            maxShieldHP = sHP;
            shieldHP = maxShieldHP;
        }
        else
        {
            maxShieldHP = 0;
        }
    }

   public double getMaxShieldHP() // Returns the maximum shield strength
    {
        return maxShieldHP;
    }

   public void setShieldHP(double sHP) // Sets the shield strength
    {
        if (sHP <= maxShieldHP)
        {
            if (sHP <= 0)
            {
                shieldHP = 0;
            }
            else
            {
                shieldHP = sHP;
            }
        }
        else if (sHP > maxShieldHP) 
        {
            shieldHP = maxShieldHP;
        }
    }

   public double getShieldHP() // Returns the shield strength
    {
        return shieldHP;
    }

   public void setRecharge(double rate)
   {
       if (rate >= 0)
       {
           chargeRate = rate;
       }
       else
       {
           chargeRate = 0;
       }
    }

   public double getRecharge()
    {
        return chargeRate;
    }

   public void recharge()
    {
        shieldHP = shieldHP + chargeRate;
    }
}
